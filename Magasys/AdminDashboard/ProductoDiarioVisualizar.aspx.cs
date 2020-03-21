using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoDiarioVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoDiario();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoDiarioEditar.aspx", false);
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
                if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                {
                    var oProductoDiario = (BLL.ProductoDiario)Session[Enums.Session.ProductoDiario.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoDiario.NOMBRE;

                    if (oProductoDiario.DESCRIPCION != null)
                        txtDescripcion.Text = oProductoDiario.DESCRIPCION;

                    BLL.DAL.DiaSemana diaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(oProductoDiario.COD_DIA_SEMAMA);
                    txtDiaDeLaSemana.Text = diaSemana.NOMBRE;

                    if (oProductoDiario.PRECIO != null)
                        txtPrecioDiario.Text = oProductoDiario.PRECIO.Value.ToString();

                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoDiario.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoDiario.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;

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

        #endregion
    }
}