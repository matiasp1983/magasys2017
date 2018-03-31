using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoRevistaVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoDiario();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoRevistaEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoDiario.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoDiario()
        {
            try
            {
                //if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                //{
                //    var oProductoDiario = (BLL.ProductoDiario)Session[Enums.Session.ProductoDiario.ToString()];

                //    if (oProductoDiario.ID_PRODUCTO > 0)
                //        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                //    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                //        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                //    txtNombre.Text = oProductoDiario.NOMBRE;
                //    txtDescripcion.Text = oProductoDiario.DESCRIPCION;
                //    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoDiario.COD_PROVEEDOR);
                //    if (loProveedor != null)
                //        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                //    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoDiario.COD_GENERO);
                //    if (loGenero != null)
                //        txtGenero.Text = loGenero.NOMBRE;                    
                //    if (oProductoDiario.PRECIO_LUNES.HasValue)
                //        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_LUNES.Value.ToString();
                //    if (oProductoDiario.PRECIO_MARTES.HasValue)
                //        txtPrecioMartesDiario.Text = oProductoDiario.PRECIO_MARTES.Value.ToString();
                //    if (oProductoDiario.PRECIO_MIERCOLES.HasValue)
                //        txtPrecioMiercolesDiario.Text = oProductoDiario.PRECIO_MIERCOLES.Value.ToString();
                //    if (oProductoDiario.PRECIO_JUEVES.HasValue)
                //        txtPrecioJuevesDiario.Text = oProductoDiario.PRECIO_JUEVES.Value.ToString();
                //    if (oProductoDiario.PRECIO_VIERNES.HasValue)
                //        txtPrecioViernesDiario.Text = oProductoDiario.PRECIO_VIERNES.Value.ToString();
                //    if (oProductoDiario.PRECIO_SABADO.HasValue)
                //        txtPrecioSabadoDiario.Text = oProductoDiario.PRECIO_SABADO.Value.ToString();
                //    if (oProductoDiario.PRECIO_DOMINGO.HasValue)
                //        txtPrecioDomingoDiario.Text = oProductoDiario.PRECIO_DOMINGO.Value.ToString();
                //}
                //else
                //    Response.Redirect("ProductoListado.aspx", false);
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