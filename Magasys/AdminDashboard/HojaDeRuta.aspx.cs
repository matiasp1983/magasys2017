using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class HojaDeRuta : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                EstrategiaFuerzaBruta();
            }
        }

        #endregion

        #region Métodos Privados

        private void EstrategiaFuerzaBruta()
        {
            try
            {
                if (Session[Enums.Session.ClientesHojaDeRuta.ToString()] != null && Session[Enums.Session.ReservasHojaDeRuta.ToString()] != null)
                {
                    // Modo de envío por defecto Auto
                    BLL.FuerzaBruta.EstrategiaFuerzaBruta oEstrategiaFuerzaBruta = new BLL.FuerzaBruta.EstrategiaFuerzaBruta("Driving");

                    //oEstrategiaFuerzaBruta.Ejecutar((List<BLL.DAL.Cliente>)(Session[Enums.Session.ClientesHojaDeRuta.ToString()]));

                    // eliminar session
                }
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