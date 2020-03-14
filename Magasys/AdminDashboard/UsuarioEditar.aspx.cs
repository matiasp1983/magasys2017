using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL
{
    public partial class UsuarioEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CargarUsuarioDesdeSession();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                var oUsuario = CargarUsuarioDesdeControles();

                if (oUsuario != null)
                {
                    bool loResutado = new BLL.UsuarioBLL().ModificarUsuario(oUsuario);

                    if (loResutado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeUsuarioSuccessModificacion, "Modificación Usuario","UsuarioListado.aspx"));                        
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeUsuarioFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeUsuarioFailure));
                }

                Session.Remove(Enums.Session.Usuario.ToString());
                Session.Remove(Enums.Session.ImagenUsuario.ToString());               
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

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtContraseniaNueva.Text) && !string.IsNullOrEmpty(txtContraseniaNuevaConfirmar.Text)) && txtContraseniaNueva.Text.Equals(txtContraseniaNuevaConfirmar.Text))
                txtContrasenia.Attributes["value"] = txtContraseniaNueva.Text;
        }

        #endregion

        #region Métodos Privados        

        private BLL.DAL.Usuario CargarUsuarioDesdeControles()
        {
            var oUsuario = new BLL.DAL.Usuario
            {
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                CONTRASENIA = txtContrasenia.Attributes["value"]
            };

            if (Session[Enums.Session.Usuario.ToString()] != null)
            {
                oUsuario.ID_USUARIO = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).ID_USUARIO;
                oUsuario.FECHA_ALTA = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).FECHA_ALTA;
                oUsuario.NOMBRE_USUARIO = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).NOMBRE_USUARIO;
                oUsuario.COD_ESTADO = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).COD_ESTADO;
            }

            if (!String.IsNullOrEmpty(ddlRol.SelectedValue))
                oUsuario.ID_ROL = Convert.ToInt32(ddlRol.SelectedValue);

            if ((byte[])Session[Enums.Session.ImagenUsuario.ToString()] != null)
                oUsuario.AVATAR = (byte[])Session[Enums.Session.ImagenUsuario.ToString()];
            else
            {
                if (((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).AVATAR != null)
                {
                    oUsuario.AVATAR = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).AVATAR;
                }
            }

            return oUsuario;
        }

        private void CargarUsuarioDesdeSession()
        {
            try
            {
                var oUsuario = new BLL.DAL.Usuario();

                if (Session[Enums.Session.Usuario.ToString()] != null)
                {
                    oUsuario = (BLL.DAL.Usuario)Session[Enums.Session.Usuario.ToString()];
                    if (oUsuario.ID_USUARIO > 0)
                        txtCodigo.Text = oUsuario.ID_USUARIO.ToString();
                    if (!String.IsNullOrEmpty(oUsuario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oUsuario.FECHA_ALTA.ToString("dd/MM/yyyy");                    
                    
                    txtNombre.Text = oUsuario.NOMBRE.ToString();
                    txtApellido.Text = oUsuario.APELLIDO.ToString();
                    txtNombreUsuario.Text = oUsuario.NOMBRE_USUARIO;

                    txtContrasenia.Attributes["value"] = oUsuario.CONTRASENIA;                    

                    CargarRoles(oUsuario.ID_ROL);

                    if (oUsuario.AVATAR != null)
                    {                        
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oUsuario.AVATAR);
                        imgPreview.ImageUrl = loImagenDataURL64;                        
                    }
                }
                else
                    Response.Redirect("UsuarioListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarRoles(long idRol)
        {
            var oRol = new BLL.RolBLL();

            try
            {
                ddlRol.DataSource = oRol.ObtenerRoles();
                ddlRol.DataTextField = "DESCRIPCION";
                ddlRol.DataValueField = "ID_ROL";
                ddlRol.DataBind();
                ddlRol.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlRol.SelectedValue = idRol.ToString();
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