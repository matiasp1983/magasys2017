using BLL.Common;
using NLog;
using System;
using BLL.DAL;
using System.Collections.Generic;
using BLL.Filters;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

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
                //Session.Remove(Enums.Session.DevolucionProducto.ToString());
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            try
            {
                if (Session[Enums.Session.DevolucionProducto.ToString()] != null)
                {
                    var loDevolucionProducto = (BLL.DetalleDevolucion)Session[Enums.Session.DevolucionProducto.ToString()];
                    BLL.DAL.ProductoDevolucion oProductoDevolucion = new BLL.DAL.ProductoDevolucion()
                    {
                        FECHA = DateTime.Now,
                        COD_ESTADO = 1,
                    };
                    List<BLL.DAL.DetalleProductoDevolucion> lstDetalleProductoDevolucion = new List<BLL.DAL.DetalleProductoDevolucion>();

                    BLL.DAL.DetalleProductoDevolucion oDetalleProductoDevolucion = new BLL.DAL.DetalleProductoDevolucion
                    {
                        COD_PRODUCTO_EDICION = loDevolucionProducto.COD_PRODUCTO_EDICION
                    };
                    ActualizarCantidad();
                    oDetalleProductoDevolucion.CANTIDAD = loDevolucionProducto.CANTIDAD;
                    lstDetalleProductoDevolucion.Add(oDetalleProductoDevolucion);

                    // Se actualizan las cantidades y estado del producto
                    if (oDetalleProductoDevolucion.CANTIDAD == 0)
                    {
                        loResutado = false;
                    }
                    else
                    { 
                        loResutado = new BLL.ProductoEdicionBLL().ActualizarCantidadDisponible(oDetalleProductoDevolucion.COD_PRODUCTO_EDICION, oDetalleProductoDevolucion.CANTIDAD, DateTime.Now);

                        foreach (var loItem in lsvReservaEdicion.Items)
                        {
                            if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                            {
                                // Se anulan las reservas marcadas
                                int codReserva = Convert.ToInt32(((Label)loItem.Controls[3]).Text);
                                BLL.DAL.ReservaEdicion oReservaEdicion = new BLL.ReservaEdicionBLL().ObtenerReservaEdicion(codReserva);
                                oReservaEdicion.COD_ESTADO = 12;
                                new BLL.ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicion);
                            }
                        }


                        loResutado = new BLL.ProductoDevolucionBLL().AltaDevolucion(oProductoDevolucion, lstDetalleProductoDevolucion);
                    }
                    if (loResutado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeDevolucionSuccessAlta, "", "Index.aspx"));
                        Session.Remove(Enums.Session.DevolucionProducto.ToString());
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeDevolucionFailure));
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            
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

        private void ActualizarCantidad()
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
        }

        #endregion

        protected void lsvReservaEdicion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarCantidad();
        }

    }
}