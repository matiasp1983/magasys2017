using BLL.Common;
using NLog;
using System;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProveedorEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProveedorDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oProveedor = CargarProveedorDesdeControles();

            try
            {
                if (oProveedor != null)
                {
                    oProveedor.CUIT = oProveedor.CUIT.ToString().Replace("-", String.Empty).Trim();

                    var loResutado = new BLL.ProveedorBLL().ModificarProveedor(oProveedor);

                    if (loResutado)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProveedorSuccessModificacion, "Modificación Proveedor", "ProveedorListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.Proveedor.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Proveedor.ToString());
            Response.Redirect("ProveedorListado.aspx", false);
        }

        protected void DdlProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvincia.SelectedValue != String.Empty)
            {
                var idProvincia = Convert.ToInt64(ddlProvincia.SelectedValue);
                CargarLocalidades(idProvincia);
            }
            else
            {
                ddlLocalidad.Items.Clear();
            }
        }

        #endregion

        #region Métodos Privados

        private void CargarProveedorDesdeSession()
        {
            try
            {
                var oProveedor = new BLL.DAL.Proveedor();

                if (Session[Enums.Session.Proveedor.ToString()] != null)
                {
                    oProveedor = (BLL.DAL.Proveedor)Session[Enums.Session.Proveedor.ToString()];
                    if (oProveedor.ID_PROVEEDOR > 0)
                        txtCodigo.Text = oProveedor.ID_PROVEEDOR.ToString();
                    if (!String.IsNullOrEmpty(oProveedor.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProveedor.FECHA_ALTA.ToString("dd/MM/yyyy");
                    if (!String.IsNullOrEmpty(oProveedor.CUIT))
                    {
                        txtCuit.MaxLength = 13;
                        txtCuit.Text = Convert.ToInt64(oProveedor.CUIT).ToString("##-########-#");
                    }
                    txtRazonSocial.Text = oProveedor.RAZON_SOCIAL;
                    txtNombre.Text = oProveedor.NOMBRE.ToString();
                    txtApellido.Text = oProveedor.APELLIDO.ToString();
                    txtTelefonoMovil.Text = oProveedor.TELEFONO_MOVIL;
                    txtTelefonoFijo.Text = oProveedor.TELEFONO_FIJO;
                    txtEmail.Text = oProveedor.EMAIL;
                    txtCalle.Text = oProveedor.CALLE;
                    if (!String.IsNullOrEmpty(oProveedor.NUMERO.ToString()))
                        txtNumero.Text = oProveedor.NUMERO.ToString();
                    txtPiso.Text = oProveedor.PISO;
                    txtDepartamento.Text = oProveedor.DEPARTAMENTO;
                    txtBarrio.Text = oProveedor.BARRIO;
                    txtCodigoPostal.Text = oProveedor.CODIGO_POSTAL;
                    CargarProvincias(oProveedor.ID_PROVINCIA, oProveedor.ID_LOCALIDAD);
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Proveedor CargarProveedorDesdeControles()
        {
            var oProveedor = new BLL.DAL.Proveedor();

            if (Session[Enums.Session.Proveedor.ToString()] != null)
            {
                oProveedor.ID_PROVEEDOR = ((BLL.DAL.Proveedor)base.Session[Enums.Session.Proveedor.ToString()]).ID_PROVEEDOR;
                oProveedor.FECHA_ALTA = ((BLL.DAL.Proveedor)base.Session[Enums.Session.Proveedor.ToString()]).FECHA_ALTA;
                oProveedor.COD_ESTADO = ((BLL.DAL.Proveedor)base.Session[Enums.Session.Proveedor.ToString()]).COD_ESTADO;
            }

            oProveedor.CUIT = txtCuit.Text;
            oProveedor.RAZON_SOCIAL = txtRazonSocial.Text;
            oProveedor.NOMBRE = txtNombre.Text;
            oProveedor.APELLIDO = txtApellido.Text;
            oProveedor.TELEFONO_MOVIL = txtTelefonoMovil.Text;

            if (!String.IsNullOrEmpty(txtTelefonoFijo.Text))
                oProveedor.TELEFONO_FIJO = txtTelefonoFijo.Text;
            else
                oProveedor.TELEFONO_FIJO = null;

            oProveedor.EMAIL = txtEmail.Text;
            oProveedor.CALLE = txtCalle.Text;
            oProveedor.NUMERO = Convert.ToInt32(txtNumero.Text);

            if (!String.IsNullOrEmpty(txtPiso.Text))
                oProveedor.PISO = txtPiso.Text;
            else
                oProveedor.PISO = null;

            if (!String.IsNullOrEmpty(txtDepartamento.Text))
                oProveedor.DEPARTAMENTO = txtDepartamento.Text;
            else
                oProveedor.DEPARTAMENTO = null;

            if (!String.IsNullOrEmpty(txtBarrio.Text))
                oProveedor.BARRIO = txtBarrio.Text;
            else
                oProveedor.BARRIO = null;

            oProveedor.CODIGO_POSTAL = txtCodigoPostal.Text;
            oProveedor.ID_PROVINCIA = Convert.ToInt32(ddlProvincia.SelectedValue);
            oProveedor.ID_LOCALIDAD = Convert.ToInt32(ddlLocalidad.SelectedValue);

            return oProveedor;
        }

        private void CargarProvincias(long idProvincia, long idLocalidad)
        {
            var oProvincia = new BLL.ProvinciaBLL();

            try
            {
                ddlProvincia.DataSource = oProvincia.ObtenerProvincias();
                ddlProvincia.DataTextField = "NOMBRE";
                ddlProvincia.DataValueField = "ID_PROVINCIA";
                ddlProvincia.DataBind();
                ddlProvincia.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlProvincia.SelectedValue = idProvincia.ToString();
                CargarLocalidades(idProvincia, idLocalidad);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarLocalidades(long idProvincia, long idLocalidad = 0)
        {
            var oLocalidad = new BLL.LocalidadBLL();

            try
            {
                ddlLocalidad.DataSource = oLocalidad.ObtenerLocalidades(idProvincia);
                ddlLocalidad.DataTextField = "NOMBRE";
                ddlLocalidad.DataValueField = "ID_LOCALIDAD";
                ddlLocalidad.DataBind();
                ddlLocalidad.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlLocalidad.SelectedValue = idLocalidad.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void LimpiarCampos()
        {
            FormProveedorEditar.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProveedorEditar.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
        }

        #endregion
    }
}