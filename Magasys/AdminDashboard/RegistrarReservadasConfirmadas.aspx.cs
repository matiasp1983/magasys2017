using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class RegistrarReservadasConfirmadas : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajeLsvReservaEdicion.Visible = false;

            if (!IsPostBack)
            {
                CargarProducto();
                Session.Remove(Enums.Session.DevolucionProducto.ToString());
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoDevolucion.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProducto()
        {
            try
            {
                if (Session[Enums.Session.DevolucionProducto.ToString()] != null)
                {
                    var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
                    txtNombre.Text = loDevolucionProducto.NOMBRE;
                    txtTipoProducto.Text = loDevolucionProducto.TIPO_PRODUCTO;
                    txtEdicion.Text = loDevolucionProducto.EDICION;
                    txtStock.Text = loDevolucionProducto.STOCK.ToString();
                    lblCantidadProductosDevolver.Text = loDevolucionProducto.CANTIDAD.ToString();

                    //Aquí se debe cagar la grilla con las reservas: lsvReservaEdicion

                    // Las siguientes dos líneas se deben llamar o agregar cuando no hay registros para mostrar en lsvReservaEdicion:
                    dvMensajeLsvReservaEdicion.InnerHtml = MessageManager.Info(dvMensajeLsvReservaEdicion, Message.MsjeEntregaProductoFiltroSinResultados, false);
                    dvMensajeLsvReservaEdicion.Visible = true;

                }
                else
                    Response.Redirect("ProductoDevolucion.aspx", false);
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