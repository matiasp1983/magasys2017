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
            List<ReservaClienteListado> lstReservasConfirmar = (List<ReservaClienteListado>)lsvReservaEdicion.DataSource;
            try
            {
                foreach (var loItem in lsvReservaEdicion.Items)
                {
                    // Consultar si existe una ReservaEdicion para la Reserva y el PorductoEdicion con estado 18 (sin Stock).
                    var oReservaEdicionSinStock = new ReservaEdicionBLL().ObtenerReservaEdicionEstadoSinStock(Convert.ToInt32(((Label)loItem.Controls[3]).Text), Convert.ToInt32(((Label)loItem.Controls[11]).Text));

                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        if (oReservaEdicionSinStock != null)
                        {
                            // Actualizar ReservaEdicion
                            oReservaEdicionSinStock.FECHA = DateTime.Now;
                            oReservaEdicionSinStock.COD_ESTADO = 15;
                            loResutado = new ReservaEdicionBLL().ModificarReservaEdidion(oReservaEdicionSinStock);
                            if (!loResutado)
                                break;
                        }
                        else
                        {
                            // Alta de ReservaEdicion
                            BLL.DAL.ReservaEdicion oReservaConfirmada = new BLL.DAL.ReservaEdicion()
                            {
                                //ID_RESERVA_EDICION = new ReservaEdicionBLL().ObtenerProximaReserva(),   // probar si funciona sin esto!!!
                                FECHA = DateTime.Now,
                                COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                                COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[11]).Text),
                                COD_ESTADO = 15
                            };

                            loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                            if (!loResutado)
                                break;
                        }

                        // Informar al Cliente que la edición fue reservada
                        BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                        {
                            COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[5]).Text),
                            DESCRIPCION = "La edición " + ((Label)loItem.Controls[13]).Text + " del producto '" + ((Label)loItem.Controls[9]).Text + "' se encuentra reservada.",
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
                        if (oReservaEdicionSinStock == null)
                        {
                            // Alta de ReservaEdicion
                            BLL.DAL.ReservaEdicion oReservaConfirmada = new BLL.DAL.ReservaEdicion()
                            {
                                FECHA = DateTime.Now,
                                COD_RESERVA = Convert.ToInt32(((Label)loItem.Controls[3]).Text),
                                COD_PROD_EDICION = Convert.ToInt32(((Label)loItem.Controls[11]).Text),
                                COD_ESTADO = 18
                            };

                            loResutado = new ReservaEdicionBLL().AltaReservaEdicion(oReservaConfirmada);
                            if (!loResutado)
                                break;

                            BLL.DAL.Mensaje oMensaje = new BLL.DAL.Mensaje()
                            {
                                COD_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[5]).Text),
                                DESCRIPCION = "La edición " + ((Label)loItem.Controls[13]).Text + " del producto '" + ((Label)loItem.Controls[9]).Text + "' no fue reservada.",
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
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
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
                lblCantidadIngresada.Text = "40";
                lblReservasTotales.Text = "120";
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