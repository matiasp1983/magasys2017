﻿using BLL.Common;
using NLog;
using System;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class RegistrarUsuario : System.Web.UI.Page
	{
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposDocumento();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var loTipoDocumento = 0;
                var loNroDocumento = 0;                               

                if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue))
                    loTipoDocumento = Convert.ToInt32(ddlTipoDocumento.SelectedValue);

                loNroDocumento = Convert.ToInt32(txtNroDocumento.Text);

                var oCliente = new BLL.ClienteBLL().ObtenerCliente(loTipoDocumento, loNroDocumento);

                if (oCliente == null)
                {
                    oCliente = new BLL.DAL.Cliente
                    {
                        TIPO_DOCUMENTO = loTipoDocumento,
                        NRO_DOCUMENTO = loNroDocumento,
                        FECHA_ALTA = DateTime.Now,
                        COD_ESTADO = 1,
                        NOMBRE = txtNombre.Text,
                        APELLIDO = txtApellido.Text,
                        TELEFONO_MOVIL = txtTelefonoMovil.Text,
                        EMAIL = txtEmail.Text
                    };           
                    
                    oCliente = new BLL.ClienteBLL().AltaClienteReturnCliente(oCliente);
                }
               
                var oUsuario = new BLL.DAL.Usuario
                {
                    NOMBRE = txtNombre.Text,
                    APELLIDO = txtApellido.Text,
                    NOMBRE_USUARIO = txtNombreUsuario.Text,
                    CONTRASENIA = txtContraseniaConfirmacion.Text,
                    FECHA_ALTA = DateTime.Now,
                    COD_ESTADO = 1,
                    ID_ROL = 3,
                    RECUPERAR_CONTRASENIA = oCliente.ID_CLIENTE.ToString()
                };

                imgPreview.ImageUrl = "img/perfil_default.png";

                if (fuploadImagen.PostedFile.ContentLength != 0)
                {
                    int loTamanioImagen = fuploadImagen.PostedFile.ContentLength;
                    byte[] loImagenOriginal = new byte[loTamanioImagen];
                    fuploadImagen.PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);
                    oUsuario.AVATAR = loImagenOriginal;
                }

                oUsuario = new BLL.UsuarioBLL().AltaUsuarioReturnUsuario(oUsuario);

                if (oUsuario != null)
                {
                    oCliente.COD_USUARIO = oUsuario.ID_USUARIO;

                    if(new BLL.ClienteBLL().ModificarCliente(oCliente)) {

                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeUsuarioSuccessAlta, "Alta Usuario", "Index.aspx"));
                        LimpiarCampos();
                    }
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeUsuarioFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeUsuarioFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("Login.aspx", false);
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

        private void LimpiarCampos()
        {
            FormRegistrarUsuario.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormRegistrarUsuario.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            imgPreview.ImageUrl = "~/AdminDashboard/img/perfil_default.png";
        }

        #endregion

        #region Métodos Públicos

        [WebMethod]
        public static bool ValidarNombreUsuario(string pNombreUsuario)
        {
            return new BLL.UsuarioBLL().ConsultarExistenciaNombreUsuario(pNombreUsuario);
        }

        #endregion
    }
}