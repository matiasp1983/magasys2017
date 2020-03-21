using BLL.Common;
using NLog;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoPeliculaEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoPeliculaDesdeSession();
        }                

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oPelicula = CargarPeliculaDesdeControles();

                if (oProducto != null && oPelicula != null)
                {
                    loResutado = new BLL.PeliculaBLL().ModificarPelicula(oProducto, oPelicula);

                    if (loResutado)
                    {                        
                        Session.Remove(Enums.Session.ProductoPelicula.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Película", "ProductoListado.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {            
            Session.Remove(Enums.Session.ProductoPelicula.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoPeliculaDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoPelicula.ToString()] != null)
                {
                    var oProductoPelicula = (BLL.ProductoPelicula)Session[Enums.Session.ProductoPelicula.ToString()];

                    if (oProductoPelicula.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoPelicula.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoPelicula.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoPelicula.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoPelicula.NOMBRE;
                    txtDescripcion.Text = oProductoPelicula.DESCRIPCION;
                    CargarProveedor(oProductoPelicula.COD_PROVEEDOR);
                    CargarGenero(oProductoPelicula.COD_GENERO);
                    CargarAnio(oProductoPelicula.ANIO);
                    txtPrecioPelicula.Text = oProductoPelicula.PRECIO.ToString();

                    if (oProductoPelicula.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoPelicula.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;                        
                    }
                }
                else
                    Response.Redirect("ProductoListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Producto CargarProductoDesdeControles()
        {
            if (Session[Enums.Session.ProductoPelicula.ToString()] == null)
                return null;

            var oProducto = new BLL.DAL.Producto
            {
                ID_PRODUCTO = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).ID_PRODUCTO,
                FECHA_ALTA = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).FECHA_ALTA,
                COD_ESTADO = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).COD_ESTADO,
                COD_TIPO_PRODUCTO = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).COD_TIPO_PRODUCTO,
                NOMBRE = txtNombre.Text,
                COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue),
                COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue)
            };

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;            

            imgPreview.ImageUrl = "img/preview_icons.png";

            if (fuploadImagen.PostedFile.ContentLength != 0)
            {
                int loTamanioImagen = fuploadImagen.PostedFile.ContentLength;
                byte[] loImagenOriginal = new byte[loTamanioImagen];
                fuploadImagen.PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                var oImagen = new BLL.DAL.Imagen
                {
                    IMAGEN1 = loImagenOriginal
                };

                oProducto.Imagen = oImagen;

                string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loImagenOriginal);
                imgPreview.ImageUrl = loImagenDataURL64;
            }
            else
            {
                if (hdfLimpiariarImagen.Value.Equals("false"))
                {
                    if (((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).IMAGEN != null)
                    {
                        var loSessionActual = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]);
                        oProducto.COD_IMAGEN = loSessionActual.IMAGEN.ID_IMAGEN;                        
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loSessionActual.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                    }
                }
            }

            return oProducto;
        }

        private BLL.DAL.Pelicula CargarPeliculaDesdeControles()
        {
            if (Session[Enums.Session.ProductoPelicula.ToString()] == null)
                return null;

            var oPelicula = new BLL.DAL.Pelicula
            {
                COD_PRODUCTO = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).ID_PRODUCTO,
                ID_PELICULA = ((BLL.ProductoPelicula)base.Session[Enums.Session.ProductoPelicula.ToString()]).ID_PELICULA,
                ANIO = Convert.ToInt32(ddlAnioDeEstrenoPelicula.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioPelicula.Text),
            };

            return oPelicula;
        }

        private void CargarProveedor(long idProveedor)
        {
            var oProveedor = new BLL.ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idProveedor == 0)
                    ddlProveedor.SelectedIndex = 0;
                else
                {
                    ddlProveedor.SelectedValue = idProveedor.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarGenero(long idGenero)
        {
            var oGenero = new BLL.GeneroBLL();

            try
            {
                ddlGenero.DataSource = oGenero.ObtenerGeneros();
                ddlGenero.DataTextField = "NOMBRE";
                ddlGenero.DataValueField = "ID_GENERO";
                ddlGenero.DataBind();
                ddlGenero.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idGenero == 0)
                    ddlGenero.SelectedIndex = 0;
                else
                {
                    ddlGenero.SelectedValue = idGenero.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarAnio(int anio)
        {
            var oAnio = new BLL.AnioBLL();

            try
            {
                var lstAnios = oAnio.ObtenerAnios();

                ddlAnioDeEstrenoPelicula.DataSource = lstAnios;
                ddlAnioDeEstrenoPelicula.DataTextField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataValueField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataBind();
                ddlAnioDeEstrenoPelicula.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlAnioDeEstrenoPelicula.SelectedValue = anio.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion
    }
}