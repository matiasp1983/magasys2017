using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoColeccionVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoColeccion();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoColeccionEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoColeccion.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoColeccion()
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
                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoColeccion.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoColeccion.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;

                    if (oProductoColeccion.ID_DIA_SEMANA.HasValue)
                    {
                        var loDiaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(oProductoColeccion.ID_DIA_SEMANA);
                        if (loDiaSemana != null)
                            txtDiaDeEntregaColeccion.Text = loDiaSemana.NOMBRE;
                    }

                    var loPeriodicidad = new BLL.PeriodicidadBLL().ObtenerPeriodicidad(oProductoColeccion.COD_PERIODICIDAD);
                    if (loPeriodicidad != null)
                        txtPeriodicidadColeccion.Text = loPeriodicidad.NOMBRE;

                    if (oProductoColeccion.CANTIDAD_DE_ENTREGAS > 0)
                        txtCantidadDeEntregaColeccion.Text = oProductoColeccion.CANTIDAD_DE_ENTREGAS.ToString();

                    if (oProductoColeccion.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoColeccion.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                        lblTitulo.Text = oProductoColeccion.IMAGEN.NOMBRE;
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