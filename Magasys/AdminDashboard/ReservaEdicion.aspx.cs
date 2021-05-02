using BLL;
using System;
using System.Collections.Generic;
using System.Web.UI;
using BLL.Common;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NLog;

namespace PL.AdminDashboard
{
    public partial class ReservaEdicion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblReservasSeleccionadas.Text = "0";
                CargarGrilla();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            bool loResutado = false;
            int lvCantidadReservasPorProductoEdicion = 0;
            string lvNombreProducto = String.Empty;
            List<ReservaClienteListado> lstReservasConfirmar = (List<ReservaClienteListado>)lsvReservaEdicion.DataSource;
            List<BLL.DAL.ProductoEdicion> lstProductoEdicion = new List<BLL.DAL.ProductoEdicion>();

            try
            {
                // Controlar si hay suficiente stock para las reservas
                foreach (var loItem in lsvReservaEdicion.Items)
                {
                    // Obtener stock de los ProductoEdicion
                    // hacer un listado de ProductoEdicion con su respectivo stock
                    var oProductoEdicion = new ProductoEdicionBLL().ObtenerProductoEdicionPorId(Convert.ToInt32(((Label)loItem.Controls[13]).Text));
                    if (oProductoEdicion != null && !(lstProductoEdicion.Exists(x => x.ID_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)))
                        lstProductoEdicion.Add(oProductoEdicion);
                }

                // Recorrer nuevo listado y contar cuantas reservas hay y si alcanza el stock.
                foreach (var item in lstProductoEdicion)
                {
                    lvCantidadReservasPorProductoEdicion = 0;

                    foreach (var loItem in lsvReservaEdicion.Items)
                    {
                        if (((HtmlInputCheckBox)loItem.Controls[1]).Checked && (Convert.ToInt32(((Label)loItem.Controls[13]).Text) == item.ID_PRODUCTO_EDICION))
                        {
                            lvCantidadReservasPorProductoEdicion++;
                            lvNombreProducto = ((Label)loItem.Controls[9]).Text;
                        }
                    }

                    if (lvCantidadReservasPorProductoEdicion > item.CANTIDAD_DISPONIBLE)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal($"{Message.MsjeNoHaySuficienteStockParaReservar} {lvNombreProducto}"));
                        return;
                    }
                }

                foreach (var loItem in lsvReservaEdicion.Items)
                {
                    // Consultar si existe una ReservaEdicion con estado 18 (sin Stock) para la Reserva y el PorductoEdicion del registro procesado.
                    var oReservaEdicionSinStockORegistrada = new ReservaEdicionBLL().ObtenerReservaEdicionEstadoSinStockORegistrada(Convert.ToInt32(((Label)loItem.Controls[3]).Text), Convert.ToInt32(((Label)loItem.Controls[13]).Text));

                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        if (oReservaEdicionSinStockORegistrada != null)
                        {
                            // Actualizar ReservaEdicion
                            oReservaEdicionSinStockORegistrada.FECHA = DateTime.Now;
                            oReservaEdicionSinStockORegistrada.COD_ESTADO = 15;
                            loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicionSinStockORegistrada);
                            if (!loResutado)
                                break;
                        }
                        else
                        {
                            // Alta de ReservaEdicion
                            BLL.DAL.ReservaEdicion oReservaConfirmada = new BLL.DAL.ReservaEdicion()
                            {
                                FECHA = DateTime.Now,
                                COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                                COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                                COD_ESTADO = 15
                            };

                            loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                            if (!loResutado)
                                break;
                        }

                        // Actualizar stock (se reserva stock)
                        loResutado = new ProductoEdicionBLL().ActualizarCantidadDisponible(Convert.ToInt32(((Label)loItem.Controls[13]).Text), 1);
                        if (!loResutado)
                            break;

                        // Informar al Cliente que la edición fue reservada
                        BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[5]).Text),
                            DESCRIPCION = "La edición " + ((Label)loItem.Controls[11]).Text + " del producto '" + ((Label)loItem.Controls[9]).Text + "' se encuentra reservada.",
                            TIPO_MENSAJE = "success-element",
                            FECHA_REGISTRO_MENSAJE = DateTime.Now
                        };

                        loResutado = new MensajeBLL().AltaMensaje(oMensaje);
                        if (!loResutado)
                            break;
                    }
                    else
                    {
                        // Informar al Cliente que la edicón no será reservada (por falta de stock u otro motivo)
                        if (oReservaEdicionSinStockORegistrada == null || (oReservaEdicionSinStockORegistrada != null && oReservaEdicionSinStockORegistrada.COD_ESTADO == 10))
                        {
                            if (oReservaEdicionSinStockORegistrada == null)
                            {
                                // Alta de ReservaEdicion
                                BLL.DAL.ReservaEdicion oReservaConfirmada = new BLL.DAL.ReservaEdicion()
                                {
                                    FECHA = DateTime.Now,
                                    COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                                    COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[13]).Text),
                                    COD_ESTADO = 18
                                };

                                loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                            }
                            else
                            {
                                oReservaEdicionSinStockORegistrada.FECHA = DateTime.Now;
                                oReservaEdicionSinStockORegistrada.COD_ESTADO = 18;
                                loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicionSinStockORegistrada);
                            }

                            if (!loResutado)
                                break;

                            BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                            {
                                COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[5]).Text),
                                DESCRIPCION = "La edición " + ((Label)loItem.Controls[11]).Text + " del producto '" + ((Label)loItem.Controls[9]).Text + "' no fue reservada.",
                                TIPO_MENSAJE = "warning-element",
                                FECHA_REGISTRO_MENSAJE = DateTime.Now
                            };

                            var loResultadoMensaje = new MensajeBLL().AltaMensaje(oMensaje);
                        }
                    }
                }

                if (loResutado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoIngresoSuccessAlta, "Alta de Ingreso de productos", "ProductoIngreso.aspx"));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
            Session.Remove(Enums.Session.CantidadProductoIngresado.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
            Session.Remove(Enums.Session.CantidadProductoIngresado.ToString());
            Response.Redirect("ProductoIngreso.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            if (Session[Enums.Session.ListadoReservaConfirmar.ToString()] != null)
            {
                ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
                List<ReservaClienteListado> lstReservasConfirmar = MapListViewToListObject(lsvReservas);
                lblCantidadIngresada.Text = Session[Enums.Session.CantidadProductoIngresado.ToString()].ToString(); // Indica la catidad total de productos ingresados.
                lblReservasTotales.Text = lstReservasConfirmar.Count.ToString();
                lsvReservaEdicion.DataSource = lstReservasConfirmar;
                lsvReservaEdicion.DataBind();
            }
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            return lstReservas;
        }

        #endregion
    }
}