using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BLL.Common;
using NLog;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class GeneroEditar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGeneroDesdeSession();
        }

        #region Eventos

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oGenero = CargarGeneroDesdeControles();

            try
            {
                if (oGenero != null)
                {
                    var loResutado = new BLL.GeneroBLL().ModificarGenero(oGenero);

                    if (loResutado)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeGeneroSuccessModificacion, "Modificación Genero", "GeneroListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));
                }

                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));
                }

            }
            
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.Genero.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneroListado.aspx", false);
        }

        #endregion

        #region Metodos Privados

        private BLL.DAL.Genero CargarGeneroDesdeControles()
        {
            var oGenero = new BLL.DAL.Genero();

            if (Session[Enums.Session.Genero.ToString()] != null)
            {
                oGenero.ID_GENERO = ((BLL.DAL.Genero)base.Session[Enums.Session.Genero.ToString()]).ID_GENERO;
            }

            oGenero.NOMBRE = txtNombre.Text;



            return oGenero;
        }

        private void CargarGeneroDesdeSession()
        {
            try
            {
                var oGenero = new BLL.DAL.Genero();

                if (Session[Enums.Session.Genero.ToString()] != null)
                {
                    oGenero = (BLL.DAL.Genero)Session[Enums.Session.Genero.ToString()];
                    if (oGenero.ID_GENERO > 0)
                        txtID.Text = oGenero.ID_GENERO.ToString();
                    txtNombre.Text = oGenero.NOMBRE.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void LimpiarCampos()
        {
            FormGenero.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
        }

        #endregion

    }
}
