using BLL.Common;
using NLog;
using System;
using BLL.DAL;
using System.Collections.Generic;
using BLL.Filters;
using System.Linq;
using System.Web.UI.HtmlControls;

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
                    List<BLL.ReservaEdicionListado> lstReservaListado = new BLL.ReservaEdicionBLL().ObtenerReservaEdicionPorProdEdicion((long)loDevolucionProducto.COD_PRODUCTO_EDICION);
                    

                   
                    if (lstReservaListado.Any())
                    {
                        lsvReservaEdicion.DataSource = lstReservaListado;
                        lsvReservaEdicion.DataBind();
                        lsvReservaEdicion.Visible = true;
                        dvMensajeLsvReservaEdicion.Visible = true;
                        loDevolucionProducto.CANTIDAD = loDevolucionProducto.CANTIDAD - lstReservaListado.Count();
                        lblCantidadProductosDevolver.Text = loDevolucionProducto.CANTIDAD.ToString();
                    }
                    else
                    {
                        // Las siguientes dos líneas se deben llamar o agregar cuando no hay registros para mostrar en lsvReservaEdicion:
                        dvMensajeLsvReservaEdicion.InnerHtml = MessageManager.Info(dvMensajeLsvReservaEdicion, Message.MsjeEntregaProductoFiltroSinResultados, false);
                        dvMensajeLsvReservaEdicion.Visible = true;
                    }



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

        protected void lsvReservaEdicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int marcados = 0;
            foreach (var loItem in lsvReservaEdicion.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                {

                }
                else
                {
                    marcados = marcados + 1;
                }
            }
            var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
            loDevolucionProducto.CANTIDAD = loDevolucionProducto.STOCK - marcados;
            lblCantidadProductosDevolver.Text = loDevolucionProducto.CANTIDAD.ToString();
            this.Page_Load(sender, e);
        }
    }
}