using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL
{
    public partial class Usuario : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {                
                var oUsuario = CargarUsuarioDesdeControles();
                bool loResutado = new BLL.UsuarioBLL().AltaUsuario(oUsuario);

                if (loResutado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeUsuarioSuccessAlta, "Alta Usuario"));
                    LimpiarCampos();
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
            Session.Remove(Enums.Session.ImagenUsuario.ToString());
            Response.Redirect("UsuarioListado.aspx", false);
        }

        protected void BtnSubirImagen_Click(object sender, EventArgs e)
        {            
            int loTamanioImagen = fuploadImagen.PostedFile.ContentLength;

            if (loTamanioImagen == 0)
                return;
            
            byte[] loImagenOriginal = new byte[loTamanioImagen];
            
            fuploadImagen.PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);           

            Session.Add(Enums.Session.ImagenUsuario.ToString(), loImagenOriginal);
            
            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loImagenOriginal);
            imgPreview.ImageUrl = loImagenDataURL64;
        }

        protected void BtnLimpiarImagen_Click(object sender, EventArgs e)
        {
            imgPreview.ImageUrl = "~/AdminDashboard/img/perfil_default.png";            
            Session.Remove(Enums.Session.ImagenUsuario.ToString());
        }

        protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        #endregion

        #region Métodos Privados

        private void CargarRoles()
        {
            var oRol = new BLL.RolBLL();

            try
            {
                ddlRol.DataSource = oRol.ObtenerRoles();
                ddlRol.DataTextField = "DESCRIPCION";
                ddlRol.DataValueField = "ID_ROL";
                ddlRol.DataBind();
                ddlRol.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Usuario CargarUsuarioDesdeControles()
        {
            var oUsuario = new BLL.DAL.Usuario
            {
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                NOMBRE_USUARIO = txtNombreUsuario.Text,
                CONTRASENIA = txtContraseniaConfirmacion.Text,
                FECHA_ALTA = DateTime.Now
            };

            if (!String.IsNullOrEmpty(ddlRol.SelectedValue))
                oUsuario.ID_ROL = Convert.ToInt32(ddlRol.SelectedValue);         
          
            if ((byte[])Session[Enums.Session.ImagenUsuario.ToString()] != null)
                oUsuario.AVATAR = (byte[])Session[Enums.Session.ImagenUsuario.ToString()];

            return oUsuario;
        }

        private void LimpiarCampos()
        {
            FormUsuario.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormUsuario.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);            
            Session.Remove(Enums.Session.ImagenUsuario.ToString());
            imgPreview.ImageUrl = "~/AdminDashboard/img/perfil_default.png";
        }

        #endregion
    }
}