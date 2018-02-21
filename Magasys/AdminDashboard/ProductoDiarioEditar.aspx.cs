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
    public partial class ProductoDiarioEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoDiarioDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados

        private void CargarProductoDiarioDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                {
                    var oProductoDiario = (BLL.DAL.Producto)Session[Enums.Session.ProductoDiario.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                }
                else
                    Response.Redirect("ProductoListado.aspx", false);
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