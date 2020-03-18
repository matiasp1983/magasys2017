using BLL.Common;
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
            Response.Redirect("Login.aspx", false);
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

        private BLL.DAL.Usuario CargarUsuarioDesdeControles()
        {
            var oUsuario = new BLL.DAL.Usuario
            {
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                NOMBRE_USUARIO = txtNombreUsuario.Text,
                CONTRASENIA = txtContraseniaConfirmacion.Text,
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1
            };

            if (!String.IsNullOrEmpty(ddlTipoDocumento.SelectedValue))
                oUsuario.ID_ROL = Convert.ToInt32(ddlTipoDocumento.SelectedValue);

            if ((byte[])Session[Enums.Session.ImagenUsuario.ToString()] != null)
                oUsuario.AVATAR = (byte[])Session[Enums.Session.ImagenUsuario.ToString()];

            return oUsuario;
        }

        private void LimpiarCampos()
        {
            FormRegistrarUsuario.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormRegistrarUsuario.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            Session.Remove(Enums.Session.ImagenUsuario.ToString());
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