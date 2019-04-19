using System;
using NLog;
using BLL.DAL;
using BLL.Common;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

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
                CargarProvincias();
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
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));
                }
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

        private void CargarProvincias()
        {
            var oProvincia = new BLL.ProvinciaBLL();

            try
            {
                ddlProvincia.DataSource = oProvincia.ObtenerProvincias();
                ddlProvincia.DataTextField = "NOMBRE";
                ddlProvincia.DataValueField = "ID_PROVINCIA";
                ddlProvincia.DataBind();
                ddlProvincia.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlProvincia.SelectedIndex = 0;
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

            if (!String.IsNullOrEmpty(txtCalle.Text))
                oCliente.CALLE = txtCalle.Text;
            else
                oCliente.CALLE = null;

            if (!String.IsNullOrEmpty(txtNumero.Text))
                oCliente.NUMERO = Convert.ToInt32(txtNumero.Text);
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

            if (!String.IsNullOrEmpty(txtBarrio.Text))
                oCliente.BARRIO = txtBarrio.Text;
            else
                oCliente.BARRIO = null;

            if (!String.IsNullOrEmpty(txtCodigoPostal.Text))
                oCliente.CODIGO_POSTAL = txtCodigoPostal.Text;
            else
                oCliente.CODIGO_POSTAL = null;

            if (!String.IsNullOrEmpty(ddlProvincia.SelectedValue))
                oCliente.ID_PROVINCIA = Convert.ToInt32(ddlProvincia.SelectedValue);
            else
                oCliente.ID_PROVINCIA = null;

            if (!String.IsNullOrEmpty(hfdidLocalidad.Value))
                oCliente.ID_LOCALIDAD = Convert.ToInt32(hfdidLocalidad.Value);
            else
                oCliente.ID_LOCALIDAD = null;

            return oCliente;
        }

        [WebMethod]
        public static ArrayList CargarLocalidades(string idProvincia)
        {
            var oLocalidad = new BLL.LocalidadBLL();
            ArrayList lstLocalidadesResultado = null;
            List<Localidad> lstLocalides;

            try
            {
                if (!String.IsNullOrEmpty(idProvincia))
                {
                    lstLocalides = oLocalidad.ObtenerLocalidades(Convert.ToInt64(idProvincia));
                    lstLocalidadesResultado = new ArrayList
                    {
                        new ItemOptionLocalidad() { Value = string.Empty, Text = string.Empty }
                    };

                    foreach (var item in lstLocalides)
                    {
                        lstLocalidadesResultado.Add(new ItemOptionLocalidad() { Value = item.ID_LOCALIDAD.ToString(), Text = item.NOMBRE });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            return lstLocalidadesResultado;
        }

        #endregion

    }
}