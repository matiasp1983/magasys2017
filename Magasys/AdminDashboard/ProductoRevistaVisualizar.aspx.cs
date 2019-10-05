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
                CargarProductoRevista();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoRevistaEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoRevista.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoRevista()
        {
            try
            {
                if (Session[Enums.Session.ProductoRevista.ToString()] != null)
                {
                    var oProductoRevista = (BLL.ProductoRevista)Session[Enums.Session.ProductoRevista.ToString()];

                    if (oProductoRevista.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoRevista.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoRevista.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoRevista.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoRevista.NOMBRE;
                    txtDescripcion.Text = oProductoRevista.DESCRIPCION;
                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoRevista.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoRevista.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;

                    if (oProductoRevista.ID_DIA_SEMANA.HasValue)
                    {
                        var loDiaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(oProductoRevista.ID_DIA_SEMANA);
                        if (loDiaSemana != null)
                            txtDiaDeEntregaRevista.Text = loDiaSemana.NOMBRE;
                    }

                    var loPeriodicidad = new BLL.PeriodicidadBLL().ObtenerPeriodicidad(oProductoRevista.COD_PERIODICIDAD);
                    if (loPeriodicidad != null)
                        txtPeriodicidadRevista.Text = loPeriodicidad.NOMBRE;

                    if (oProductoRevista.PRECIO > 0)
                        txtPrecioRevista.Text = oProductoRevista.PRECIO.ToString();

                    if (oProductoRevista.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoRevista.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                        lblTitulo.Text = oProductoRevista.IMAGEN.NOMBRE;
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