using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProveedorVisualizar : System.Web.UI.Page
    {
        #region Enventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProveedor();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProveedorEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Proveedor.ToString());
            Response.Redirect("ProveedorListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProveedor()
        {
            try
            {
                if (Session[Enums.Session.Proveedor.ToString()] != null)
                {
                    var oProveedor = (BLL.DAL.Proveedor)Session[Enums.Session.Proveedor.ToString()];
                    if (oProveedor.ID_PROVEEDOR > 0)
                        txtCodigo.Text = oProveedor.ID_PROVEEDOR.ToString();
                    if (!String.IsNullOrEmpty(oProveedor.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProveedor.FECHA_ALTA.ToString("dd/MM/yyyy");
                    if (!String.IsNullOrEmpty(oProveedor.CUIT))
                        txtCuit.Text = Convert.ToInt64(oProveedor.CUIT).ToString("##-########-#");
                    txtRazonSocial.Text = oProveedor.RAZON_SOCIAL;
                    txtNombre.Text = oProveedor.NOMBRE.ToString();
                    txtApellido.Text = oProveedor.APELLIDO.ToString();
                    txtTelefonoMovil.Text = oProveedor.TELEFONO_MOVIL;
                    txtTelefonoFijo.Text = oProveedor.TELEFONO_FIJO;
                    txtEmail.Text = oProveedor.EMAIL;
                    txtCalle.Text = oProveedor.CALLE;
                    txtNumero.Text = oProveedor.NUMERO.ToString();
                    txtPiso.Text = oProveedor.PISO;
                    txtDepartamento.Text = oProveedor.DEPARTAMENTO;
                    var loProvincia = new BLL.ProvinciaBLL().ObtenerProvincia(oProveedor.ID_PROVINCIA);
                    if (loProvincia != null)
                        txtProvincia.Text = loProvincia.NOMBRE;
                    var loLocalidad = new BLL.LocalidadBLL().ObtenerLocalidad(oProveedor.ID_LOCALIDAD);
                    if (loLocalidad != null)
                        txtLocalidad.Text = loLocalidad.NOMBRE;
                    txtBarrio.Text = oProveedor.BARRIO;
                    txtCodigoPostal.Text = oProveedor.CODIGO_POSTAL;
                }
                else
                    Response.Redirect("ProveedorListado.aspx", false);
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