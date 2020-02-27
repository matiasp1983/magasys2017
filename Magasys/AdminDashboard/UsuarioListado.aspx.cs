using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class UsuarioListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarRoles();
                CargarGrillaUsuarios();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaUsuarios();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvUsuarios_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdUsuario = ((BLL.UsuarioListado)e.Item.DataItem).ID_USUARIO.ToString();
                var loNombreUsuario = ((BLL.UsuarioListado)e.Item.DataItem).NOMBRE_USUARIO.ToString();

                HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                btnVisualizar.Attributes.Add("value", loIdUsuario);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdUsuario);

                HiddenField hdIdUsuarioBaja = ((HiddenField)e.Item.FindControl("hdIdUsuarioBaja"));

                hdIdUsuarioBaja.Value = string.Format("{0},{1}", loIdUsuario, loNombreUsuario);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuario.aspx", false);
        }

        protected void BtnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var oUsuario = new BLL.UsuarioBLL().ObtenerUsuario(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Usuario.ToString(), oUsuario);
                Response.Redirect("UsuarioVisualizar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                var oUsuario = new BLL.UsuarioBLL().ObtenerUsuario(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Usuario.ToString(), oUsuario);
                Response.Redirect("UsuarioEditar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnBaja_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(hdIdUsuarioBajaModal.Value))
                {
                    var loIdUsuario = Convert.ToInt64(hdIdUsuarioBajaModal.Value);
                    var oUsuario = new BLL.UsuarioBLL();
                    if (oUsuario.BajaUsuario(loIdUsuario))
                    {
                        CargarGrillaUsuarios();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
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

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvUsuarios.Visible = false;
        }

        private UsuarioFiltro CargarUsuarioFiltro()
        {
            UsuarioFiltro oUsuarioFiltro = new UsuarioFiltro();           

            if (string.IsNullOrEmpty(txtCodigo.Text))
                oUsuarioFiltro.IdUsuario = 0;
            else
            {
                long loIdUsuario;
                bool loResultado = long.TryParse(txtCodigo.Text, out loIdUsuario);
                if (loResultado)
                    oUsuarioFiltro.IdUsuario = loIdUsuario;
                else
                    oUsuarioFiltro.IdUsuario = -1;
            }
            if (!String.IsNullOrEmpty(txtNombreUsuario.Text))
                oUsuarioFiltro.NombreUsuario = txtNombreUsuario.Text;            

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oUsuarioFiltro.Nombre = txtNombre.Text;

            if (!String.IsNullOrEmpty(txtApellido.Text))
                oUsuarioFiltro.Apellido = txtApellido.Text;

            if (!String.IsNullOrEmpty(ddlRol.SelectedValue))
                oUsuarioFiltro.IdRol = Convert.ToInt32(ddlRol.SelectedValue);

            return oUsuarioFiltro;
        }

        private void CargarGrillaUsuarios()
        {
            try
            {
                var oUsuarioFiltro = CargarUsuarioFiltro();

                if (oUsuarioFiltro != null)
                {
                    var lstUsuarios = new BLL.UsuarioBLL().ObtenerUsuarios(oUsuarioFiltro);

                    if (lstUsuarios != null && lstUsuarios.Count > 0)
                        lsvUsuarios.DataSource = lstUsuarios;
                    else
                    {
                        dvMensajeLsvUsuarios.InnerHtml = MessageManager.Info(dvMensajeLsvUsuarios, Message.MsjeListadoUsuarioFiltrarTotalSinResultados, false);
                        dvMensajeLsvUsuarios.Visible = true;
                    }
                }                
            }
            catch (Exception ex)
            {
                lsvUsuarios.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvUsuarios.DataBind();
        }

        private void LimpiarCampos()
        {
            FormUsuarioListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormUsuarioListado.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            CargarGrillaUsuarios();
        }

        #endregion        
    }
}