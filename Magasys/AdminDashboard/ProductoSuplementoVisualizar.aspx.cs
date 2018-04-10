using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoSuplementoVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoSuplemento();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoSuplementoEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoSuplemento.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoSuplemento()
        {
            try
            {
                if (Session[Enums.Session.ProductoSuplemento.ToString()] != null)
                {
                    var oProductoSuplemento = (BLL.ProductoSuplemento)Session[Enums.Session.ProductoSuplemento.ToString()];

                    if (oProductoSuplemento.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoSuplemento.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoSuplemento.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoSuplemento.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoSuplemento.NOMBRE;
                    txtDescripcion.Text = oProductoSuplemento.DESCRIPCION;
                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoSuplemento.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoSuplemento.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;

                    if (oProductoSuplemento.ID_DIA_SEMANA.HasValue)
                    {
                        var loDiaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(oProductoSuplemento.ID_DIA_SEMANA);
                        if (loDiaSemana != null)
                            txtDiaDeEntregaSuplemento.Text = loDiaSemana.NOMBRE;
                    }

                    var loProductoDiario = new BLL.DiarioBLL().ObtenerDiarioPorIdDiario(oProductoSuplemento.COD_DIARIO);
                    txtDiarioSuplemento.Text = loProductoDiario.NOMBRE;
                    txtPrecioSuplemento.Text = oProductoSuplemento.PRECIO.ToString();
                    txtCantidadDeEntregaSuplemento.Text = oProductoSuplemento.CANTIDAD_DE_ENTREGAS.ToString();
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

        #endregion
    }
}