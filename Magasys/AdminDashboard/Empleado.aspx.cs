using BLL.Common;
using NLog;
using System;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Empleado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                CargarTiposDocumento();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oEmpleado = CargarEmpleadoDesdeControles();
            var bEsNuevoEmpleado = new BLL.EmpleadoBLL().ConsultarExistenciaEmpleado(oEmpleado.TIPO_DOCUMENTO, oEmpleado.NRO_DOCUMENTO);
            if (!bEsNuevoEmpleado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeEmpleadoExiste));
                return;
            }

            try
            {
                if (oEmpleado != null)
                {
                    var loResultado = new BLL.EmpleadoBLL().AltaEmpleado(oEmpleado);

                    if (loResultado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeEmpleadoSuccessAlta, "Alta Empleado", "Empleado.aspx"));
                        LimpiarCampos();
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEmpleadoFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEmpleadoFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEmpleadoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpleadoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private static bool ValidaCuit(string cuit)
        {
            //Validar que el CUIT sea numérico
            Int64 locuit = 0;
            if (!Int64.TryParse(cuit, out locuit)) return false;

            //Validar que el CUIT sea positivo
            if (long.Parse(cuit) <= 0) return false;

            //Validar que el CUIT conste de 11 cifras
            if (cuit.Length != 11) return false;

            var loDigitoCalcu = Utilities.CalcularDigitoCuit(cuit);
            var loParseSubStr = int.Parse(cuit.Substring(10));
            return loDigitoCalcu == loParseSubStr;
        }

        private void CargarTiposDocumento()
        {
            var oTipoDocumento = new BLL.TipoDocumentoBLL();

            try
            {
                ddlTipoDocumento.DataSource = oTipoDocumento.ObtenerTiposDocumento();
                ddlTipoDocumento.DataTextField = "DESCRIPCION";
                ddlTipoDocumento.DataValueField = "ID_TIPO_DOCUMENTO";
                ddlTipoDocumento.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Empleado CargarEmpleadoDesdeControles()
        {
            var oEmpleado = new BLL.DAL.Empleado
            {
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1,
                CUIT = txtCuit.Text,
                TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text),
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                TELEFONO_MOVIL = txtTelefonoMovil.Text,
                EMAIL = txtEmail.Text
            };

            if (!String.IsNullOrEmpty(txtTelefonoFijo.Text))
                oEmpleado.TELEFONO_FIJO = txtTelefonoFijo.Text;
            else
                oEmpleado.TELEFONO_FIJO = null;

            if (!String.IsNullOrEmpty(hdCalle.Value))
                oEmpleado.CALLE = hdCalle.Value;
            else
                oEmpleado.CALLE = null;

            if (!String.IsNullOrEmpty(hdNumero.Value))
                oEmpleado.NUMERO = Convert.ToInt32(hdNumero.Value);
            else
                oEmpleado.NUMERO = null;

            if (!String.IsNullOrEmpty(txtPiso.Text))
                oEmpleado.PISO = txtPiso.Text;
            else
                oEmpleado.PISO = null;

            if (!String.IsNullOrEmpty(txtDepartamento.Text))
                oEmpleado.DEPARTAMENTO = txtDepartamento.Text;
            else
                oEmpleado.DEPARTAMENTO = null;

            if (!String.IsNullOrEmpty(hdLocalidad.Value))
                oEmpleado.LOCALIDAD = hdLocalidad.Value;
            else
                oEmpleado.LOCALIDAD = null;

            if (!String.IsNullOrEmpty(hdProvincia.Value))
                oEmpleado.PROVINCIA = hdProvincia.Value;
            else
                oEmpleado.PROVINCIA = null;

            if (!String.IsNullOrEmpty(hdCalle.Value) && !String.IsNullOrEmpty(hdBarrio.Value))
                oEmpleado.BARRIO = hdBarrio.Value;
            else if (!String.IsNullOrEmpty(hdCalle.Value) && !String.IsNullOrEmpty(txtBarrio.Text))
                oEmpleado.BARRIO = txtBarrio.Text;
            else if (String.IsNullOrEmpty(txtBarrio.Text))
                oEmpleado.BARRIO = null;

            if (!String.IsNullOrEmpty(hdCodigoPostal.Value))
                oEmpleado.CODIGO_POSTAL = hdCodigoPostal.Value;
            else
                oEmpleado.CODIGO_POSTAL = null;

            if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                oEmpleado.DIRECCION_MAPS = hdIdDireccionMaps.Value;
            else
                oEmpleado.DIRECCION_MAPS = null;

            return oEmpleado;
        }

        private void LimpiarCampos()
        {
            FormEmpleado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormEmpleado.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
        }

        #endregion

        #region Métodos Públicos

        [WebMethod]
        public static bool ValidarCuitProveedor(string pCuit)
        {
            if (ValidaCuit(pCuit))
            {
                return new BLL.EmpleadoBLL().ConsultarExistenciaCuit(pCuit);
            }

            return false;
        }

        #endregion
    }
}