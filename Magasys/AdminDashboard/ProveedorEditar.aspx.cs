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

        protected void BtnBorrarDireccion_Click(object sender, EventArgs e)
        {
            txtCalle.Text = String.Empty;
            hdCalle.Value = String.Empty;
            txtNumero.Text = String.Empty;
            hdNumero.Value = String.Empty;
            txtPiso.Text = String.Empty;
            txtDepartamento.Text = String.Empty;
            txtLocalidad.Text = String.Empty;
            hdLocalidad.Value = String.Empty;
            txtProvincia.Text = String.Empty;
            hdProvincia.Value = String.Empty;
            txtBarrio.Text = String.Empty;
            hdBarrio.Value = String.Empty;
            txtCodigoPostal.Text = String.Empty;
            hdCodigoPostal.Value = String.Empty;
            hdIdDireccionMaps.Value = String.Empty;
            hdLatitud.Value = String.Empty;
            hdLongitud.Value = String.Empty;
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
                    if (oProveedor.CALLE != null)
                        txtCalle.Text = oProveedor.CALLE;
                    if (oProveedor.NUMERO != null)
                        txtNumero.Text = oProveedor.NUMERO.ToString();
                    if (oProveedor.PISO != null)
                        txtPiso.Text = oProveedor.PISO;
                    if (oProveedor.DEPARTAMENTO != null)
                        txtDepartamento.Text = oProveedor.DEPARTAMENTO;
                    if (oProveedor.LOCALIDAD != null)
                        txtLocalidad.Text = oProveedor.LOCALIDAD;
                    if (oProveedor.PROVINCIA != null)
                        txtProvincia.Text = oProveedor.PROVINCIA;
                    if (oProveedor.BARRIO != null)
                        txtBarrio.Text = oProveedor.BARRIO;
                    if (oProveedor.CODIGO_POSTAL != null)
                        txtCodigoPostal.Text = oProveedor.CODIGO_POSTAL;
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

            // Calle
            if (!String.IsNullOrEmpty(hdCalle.Value) && oProveedor.CALLE != hdCalle.Value)
            {
                oProveedor.CALLE = hdCalle.Value;
            }
            else if (!String.IsNullOrEmpty(txtCalle.Text) && oProveedor.CALLE != txtCalle.Text)
            {
                oProveedor.CALLE = txtCalle.Text;
            }
            else if (String.IsNullOrEmpty(txtCalle.Text) && oProveedor.CALLE != null)
            {
                oProveedor.CALLE = null;
            }

            // Número
            if (!String.IsNullOrEmpty(hdNumero.Value) && oProveedor.NUMERO != Convert.ToInt32(hdNumero.Value))
            {
                oProveedor.NUMERO = Convert.ToInt32(hdNumero.Value);
            }
            else if (!String.IsNullOrEmpty(txtNumero.Text) && oProveedor.NUMERO != Convert.ToInt32(txtNumero.Text))
            {
                oProveedor.NUMERO = Convert.ToInt32(txtNumero.Text);
            }
            else if (String.IsNullOrEmpty(txtNumero.Text) && oProveedor.NUMERO != null)
            {
                oProveedor.NUMERO = null;
            }

            // Localidad
            if (!String.IsNullOrEmpty(hdLocalidad.Value) && oProveedor.LOCALIDAD != hdLocalidad.Value)
            {
                oProveedor.LOCALIDAD = hdLocalidad.Value;
            }
            else if (!String.IsNullOrEmpty(txtLocalidad.Text) && oProveedor.LOCALIDAD != txtLocalidad.Text)
            {
                oProveedor.LOCALIDAD = txtLocalidad.Text;
            }
            else if (String.IsNullOrEmpty(txtLocalidad.Text) && oProveedor.LOCALIDAD != null)
            {
                oProveedor.LOCALIDAD = null;
            }

            // Provincia
            if (!String.IsNullOrEmpty(hdProvincia.Value) && oProveedor.PROVINCIA != hdProvincia.Value)
            {
                oProveedor.PROVINCIA = hdProvincia.Value;
            }
            else if (!String.IsNullOrEmpty(txtProvincia.Text) && oProveedor.PROVINCIA != txtProvincia.Text)
            {
                oProveedor.PROVINCIA = txtProvincia.Text;
            }
            else if (String.IsNullOrEmpty(txtProvincia.Text) && oProveedor.PROVINCIA != null)
            {
                oProveedor.PROVINCIA = null;
            }

            // Piso
            if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtPiso.Text) && oProveedor.PISO != txtPiso.Text)
            {
                oProveedor.PISO = txtPiso.Text;
            }
            else if (String.IsNullOrEmpty(txtPiso.Text) && oProveedor.PISO != null)
            {
                oProveedor.PISO = null;
            }

            // Departamento
            if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtDepartamento.Text) && oProveedor.DEPARTAMENTO != txtDepartamento.Text)
            {
                oProveedor.DEPARTAMENTO = txtDepartamento.Text;
            }
            else if (String.IsNullOrEmpty(txtDepartamento.Text) && oProveedor.DEPARTAMENTO != null)
            {
                oProveedor.DEPARTAMENTO = null;
            }

            // Barrio
            if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(hdBarrio.Value) && oProveedor.BARRIO != hdBarrio.Value)
            {
                oProveedor.BARRIO = hdBarrio.Value;
            }
            else if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtBarrio.Text) && oProveedor.BARRIO != txtBarrio.Text)
            {
                oProveedor.BARRIO = txtBarrio.Text;
            }
            else if (String.IsNullOrEmpty(txtCalle.Text) && String.IsNullOrEmpty(txtBarrio.Text) && oProveedor.BARRIO != null)
            {
                oProveedor.BARRIO = null;
            }

            // Código Postal
            if (!String.IsNullOrEmpty(hdCodigoPostal.Value) && oProveedor.CODIGO_POSTAL != hdCodigoPostal.Value)
            {
                oProveedor.CODIGO_POSTAL = hdCodigoPostal.Value;
            }
            else if (!String.IsNullOrEmpty(txtCodigoPostal.Text) && oProveedor.CODIGO_POSTAL != txtCodigoPostal.Text)
            {
                oProveedor.CODIGO_POSTAL = txtCodigoPostal.Text;
            }
            else if (String.IsNullOrEmpty(txtCodigoPostal.Text) && oProveedor.CODIGO_POSTAL != null)
            {
                oProveedor.CODIGO_POSTAL = null;
            }

            // Dirección Maps
            if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                oProveedor.DIRECCION_MAPS = hdIdDireccionMaps.Value;
            else if (oProveedor.CALLE == null)
                oProveedor.DIRECCION_MAPS = null;

            return oProveedor;
        }

        private void LimpiarCampos()
        {
            FormProveedorEditar.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProveedorEditar.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
        }

        #endregion
    }
}