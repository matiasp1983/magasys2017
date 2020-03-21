using BLL.Common;
using NLog;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoDiarioEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoDiarioDesdeSession();
        }                

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oDiarioDiaSemana = CargarDiarioDesdeControles();

                loResutado = new BLL.DiarioBLL().ModificarDiario(oProducto, oDiarioDiaSemana);

                if (loResutado)
                {                    
                    Session.Remove(Enums.Session.ProductoDiario.ToString());
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Diario", "ProductoListado.aspx"));
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
            Session.Remove(Enums.Session.ProductoDiario.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoDiarioDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                {
                    var oProductoDiario = (BLL.ProductoDiario)Session[Enums.Session.ProductoDiario.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoDiario.NOMBRE;
                    txtDescripcion.Text = oProductoDiario.DESCRIPCION;
                    CargarProveedor(oProductoDiario.COD_PROVEEDOR);
                    CargarGenero(oProductoDiario.COD_GENERO);
                    BLL.DAL.DiaSemana diaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(oProductoDiario.COD_DIA_SEMAMA);
                    txtDiaDeLaSemanaDiario.Text = diaSemana.NOMBRE;
                    if (oProductoDiario.PRECIO.HasValue)
                        txtPrecioDiario.Text = oProductoDiario.PRECIO.Value.ToString();

                    if (oProductoDiario.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoDiario.IMAGEN.IMAGEN1);
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
            var oProducto = new BLL.DAL.Producto();

            if (Session[Enums.Session.ProductoDiario.ToString()] != null)
            {
                oProducto.ID_PRODUCTO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_PRODUCTO;
                oProducto.FECHA_ALTA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).FECHA_ALTA;
                oProducto.COD_ESTADO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).COD_ESTADO;
                oProducto.COD_TIPO_PRODUCTO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).COD_TIPO_PRODUCTO;
            }

            oProducto.NOMBRE = txtNombre.Text;
            oProducto.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
            oProducto.COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue);

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
                    if (((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).IMAGEN != null)
                    {
                        var loSessionActual = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]);
                        oProducto.COD_IMAGEN = loSessionActual.IMAGEN.ID_IMAGEN;                        
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loSessionActual.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                    }
                }
            }

            return oProducto;
        }

        private BLL.DAL.DiarioDiaSemana CargarDiarioDesdeControles()
        {
            BLL.DAL.DiarioDiaSemana oDiarioDiaSemana = new BLL.DAL.DiarioDiaSemana();

            var oDiaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(txtDiaDeLaSemanaDiario.Text);
            if (!String.IsNullOrEmpty(txtPrecioDiario.Text))
                oDiarioDiaSemana.PRECIO = Convert.ToDouble(txtPrecioDiario.Text);
            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMAMA;
            oDiarioDiaSemana.COD_DIARIO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO;

            return oDiarioDiaSemana;
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

        private string ObtenerParteDeNombreIDTexbox(string pCadena)
        {
            String loStringInicial = "txtPrecio";
            String loStringFinal = "Diario";

            String loNuevoString = pCadena.Substring(0, pCadena.LastIndexOf(loStringFinal));
            int loIniciaString = loNuevoString.LastIndexOf(loStringInicial) + loStringInicial.Length;
            int loCortar = loNuevoString.Length - loIniciaString;
            loNuevoString = loNuevoString.Substring(loIniciaString, loCortar);

            return loNuevoString;
        }

        #endregion
    }
}