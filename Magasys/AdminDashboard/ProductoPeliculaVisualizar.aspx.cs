using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoPeliculaVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoPelicula();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoPeliculaEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoPelicula.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoPelicula()
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
                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoPelicula.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoPelicula.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;
                    txtAnioDeEstrenoPelicula.Text = oProductoPelicula.ANIO.ToString();
                    txtPrecioPelicula.Text = oProductoPelicula.PRECIO.ToString();

                    if (oProductoPelicula.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoPelicula.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                        lblTitulo.Text = oProductoPelicula.IMAGEN.NOMBRE;
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