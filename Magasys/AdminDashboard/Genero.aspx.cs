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
    public partial class Genero : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oGenero = CargarGeneroDesdeControles();
            var bEsNuevoNombre = new BLL.GeneroBLL().ConsultarExistenciaGenero(oGenero.NOMBRE);
            if (!bEsNuevoNombre)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeNombreGeneroExist));
                return;
            }

            try
            {
                if (oGenero != null)
                {
                    var loResultado = new BLL.GeneroBLL().AltaGenero(oGenero);

                    if (loResultado)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeGeneroSuccessAlta, "Alta Genero", "GeneroListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeGeneroFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GeneroListado.aspx", false);
        }

        #endregion

        #region Metodos Privados

        private BLL.DAL.Genero CargarGeneroDesdeControles()
        {
            var oGenero = new BLL.DAL.Genero
            {
                ID_GENERO = 0,
                NOMBRE = txtNombre.Text

            };

            return oGenero;
        }

        #endregion
    }
}