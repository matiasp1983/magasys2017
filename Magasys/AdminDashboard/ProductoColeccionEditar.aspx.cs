using BLL.Common;
using NLog;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoColeccionEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoColeccionDesdeSession();
        }                

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oColeccion = CargarColeccionDesdeControles();

                if (oProducto != null && oColeccion != null)
                {
                    loResutado = new BLL.ColeccionBLL().ModificarColeccion(oProducto, oColeccion);

                    if (loResutado)
                    {                        
                        Session.Remove(Enums.Session.ProductoColeccion.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Colección", "ProductoListado.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
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
            Session.Remove(Enums.Session.ProductoColeccion.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoColeccionDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoColeccion.ToString()] != null)
                {
                    var oProductoColeccion = (BLL.ProductoColeccion)Session[Enums.Session.ProductoColeccion.ToString()];

                    if (oProductoColeccion.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoColeccion.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoColeccion.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoColeccion.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoColeccion.NOMBRE;
                    txtDescripcion.Text = oProductoColeccion.DESCRIPCION;
                    CargarProveedor(oProductoColeccion.COD_PROVEEDOR);
                    CargarGenero(oProductoColeccion.COD_GENERO);
                    CargarDiasDeSemana(oProductoColeccion.ID_DIA_SEMANA);
                    CargarPeriodicidades(oProductoColeccion.COD_PERIODICIDAD);
                    txtCantidadDeEntregaColeccion.Text = oProductoColeccion.CANTIDAD_DE_ENTREGAS.ToString();

                    if (oProductoColeccion.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoColeccion.IMAGEN.IMAGEN1);
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
            if (Session[Enums.Session.ProductoColeccion.ToString()] == null)
                return null;

            var oProducto = new BLL.DAL.Producto
            {
                ID_PRODUCTO = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).ID_PRODUCTO,
                FECHA_ALTA = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).FECHA_ALTA,
                COD_ESTADO = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).COD_ESTADO,
                COD_TIPO_PRODUCTO = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).COD_TIPO_PRODUCTO,
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
                    if (((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).IMAGEN != null)
                    {
                        oProducto.Imagen = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).IMAGEN;
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProducto.Imagen.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                    }
                }
            }

            return oProducto;
        }

        private BLL.DAL.Coleccion CargarColeccionDesdeControles()
        {
            if (Session[Enums.Session.ProductoColeccion.ToString()] == null)
                return null;

            var oColeccion = new BLL.DAL.Coleccion
            {
                COD_PRODUCTO = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).ID_PRODUCTO,
                ID_COLECCION = ((BLL.ProductoColeccion)base.Session[Enums.Session.ProductoColeccion.ToString()]).ID_COLECCION,
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadColeccion.SelectedValue),
                CANTIDAD_ENTREGAS = Convert.ToInt32(txtCantidadDeEntregaColeccion.Text)
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaColeccion.SelectedValue))
                oColeccion.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaColeccion.SelectedValue);
            else
                oColeccion.ID_DIA_SEMANA = null;

            return oColeccion;
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

        private void CargarDiasDeSemana(long? idDiaSemana)
        {
            var oDiaSemana = new BLL.DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaColeccion.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaColeccion.DataTextField = "NOMBRE";
                ddlDiaDeEntregaColeccion.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaColeccion.DataBind();
                ddlDiaDeEntregaColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idDiaSemana.HasValue)
                    ddlDiaDeEntregaColeccion.SelectedValue = idDiaSemana.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarPeriodicidades(long idPeriodicidad)
        {
            var oPeriodicidad = new BLL.PeriodicidadBLL();

            try
            {
                var lstPeriodicidades = oPeriodicidad.ObtenerPeriodicidades();

                ddlPeriodicidadColeccion.DataSource = lstPeriodicidades;
                ddlPeriodicidadColeccion.DataTextField = "NOMBRE";
                ddlPeriodicidadColeccion.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadColeccion.DataBind();
                ddlPeriodicidadColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idPeriodicidad == 0)
                    ddlPeriodicidadColeccion.SelectedIndex = 0;
                else
                {
                    ddlPeriodicidadColeccion.SelectedValue = idPeriodicidad.ToString();
                }
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