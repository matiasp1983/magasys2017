using System;
using NLog;
using BLL.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class Cliente : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTiposDocumento();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oCliente = CargarClienteDesdeControles();
            var bEsNuevoCliente = new BLL.ClienteBLL().ConsultarExistenciaCliente(oCliente.TIPO_DOCUMENTO, oCliente.NRO_DOCUMENTO);
            if (!bEsNuevoCliente)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienterExiste));
                return;
            }

            try
            {
                if (oCliente != null)
                {
                    var loResultado = new BLL.ClienteBLL().AltaCliente(oCliente);

                    if (loResultado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeClienteSuccessAlta, "Alta Cliente"));
                        Session.Add(Enums.Session.Cliente.ToString(), oCliente);
                        LimpiarCampos();
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnVolver_Click(object sender, EventArgs e)
        {
            if (Session[Enums.Session.AltaVentaAltaCliente.ToString()] != null)
            {
                Response.Redirect("Venta.aspx", false);
            }
        }

        #endregion

        #region Métodos Privados

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

        private BLL.DAL.Cliente CargarClienteDesdeControles()
        {
            var oCliente = new BLL.DAL.Cliente
            {
                ID_CLIENTE = 0,
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1,
                TIPO_DOCUMENTO = Convert.ToInt32(ddlTipoDocumento.SelectedValue),
                NRO_DOCUMENTO = Convert.ToInt32(txtNroDocumento.Text),
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                TELEFONO_MOVIL = txtTelefonoMovil.Text
            };

            if (!String.IsNullOrEmpty(txtAlias.Text))
                oCliente.ALIAS = txtAlias.Text;
            else
                oCliente.ALIAS = null;

            if (!String.IsNullOrEmpty(txtTelefonoFijo.Text))
                oCliente.TELEFONO_FIJO = txtTelefonoFijo.Text;
            else
                oCliente.TELEFONO_FIJO = null;

            if (!String.IsNullOrEmpty(txtEmail.Text))
                oCliente.EMAIL = txtEmail.Text;
            else
                oCliente.EMAIL = null;

            if (!String.IsNullOrEmpty(hdCalle.Value))
                oCliente.CALLE = hdCalle.Value;
            else
                oCliente.CALLE = null;

            if (!String.IsNullOrEmpty(hdNumero.Value))
                oCliente.NUMERO = Convert.ToInt32(hdNumero.Value);
            else
                oCliente.NUMERO = null;

            if (!String.IsNullOrEmpty(txtPiso.Text))
                oCliente.PISO = txtPiso.Text;
            else
                oCliente.PISO = null;

            if (!String.IsNullOrEmpty(txtDepartamento.Text))
                oCliente.DEPARTAMENTO = txtDepartamento.Text;
            else
                oCliente.DEPARTAMENTO = null;

            if (!String.IsNullOrEmpty(hdLocalidad.Value))
                oCliente.LOCALIDAD = hdLocalidad.Value;
            else
                oCliente.LOCALIDAD = null;

            if (!String.IsNullOrEmpty(hdProvincia.Value))
                oCliente.PROVINCIA = hdProvincia.Value;
            else
                oCliente.PROVINCIA = null;

            if (!String.IsNullOrEmpty(hdBarrio.Value))
                oCliente.BARRIO = hdBarrio.Value;
            else
                oCliente.BARRIO = null;

            if (!String.IsNullOrEmpty(hdCodigoPostal.Value))
                oCliente.CODIGO_POSTAL = hdCodigoPostal.Value;
            else
                oCliente.CODIGO_POSTAL = null;

            if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                oCliente.DIRECCION_MAPS = hdIdDireccionMaps.Value;
            else
                oCliente.DIRECCION_MAPS = null;

            return oCliente;
        }

        private void LimpiarCampos()
        {
            FormCliente.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
        }

        #endregion

    }
}