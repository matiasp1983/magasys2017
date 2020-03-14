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
    public partial class UsuarioVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CargarUsuarioDesdeSession();
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuarioEditar.aspx", false);
        }               

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ImagenUsuario.ToString());
            Response.Redirect("UsuarioListado.aspx", false);
        }      

        #endregion

        #region Métodos Privados                

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

                    var loRol = new BLL.RolBLL().ObtenerRol(oUsuario.ID_ROL);
                    if (loRol != null)
                        txtRol.Text = loRol.DESCRIPCION;

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

        #endregion       
    }
}