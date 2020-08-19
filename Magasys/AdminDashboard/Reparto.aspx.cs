using BLL.Common;
using BLL.Filters;
using NLog;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using BLL;

namespace PL.AdminDashboard
{
    public partial class Reparto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            dvMensajelsvReserva.Visible = false;

            if (!Page.IsPostBack)
            {
                CargarGrillaReservas();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaReservas();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnGenerarHojaRuta_Click(object sender, EventArgs e)
        {
            if (lsvReserva.Controls.Count == 0)
            {
                dvMensajelsvReserva.InnerHtml = MessageManager.Info(dvMensajelsvReserva, Message.MsjeListadoReservaFiltroSinResultados, false);
                dvMensajelsvReserva.Visible = true;
                return;
            }

            try
            {
                List<BLL.DAL.Cliente> lstCliente = new List<BLL.DAL.Cliente>();
                List<ReservaEdicionReparto> lstReservaEdicionReparto = new List<ReservaEdicionReparto>();

                Session.Remove(Enums.Session.ClientesHojaDeRuta.ToString());
                Session.Remove(Enums.Session.ReservasHojaDeRuta.ToString());

                var oNegocio = new NegocioBLL().ObtenerNegocio();

                if (oNegocio == null || String.IsNullOrEmpty(oNegocio.DIRECCION_MAPS))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoDireccionNegocio));
                    return;
                }

                foreach (var loItem in lsvReserva.Items)
                {
                    if (((HtmlInputCheckBox)loItem.Controls[1]).Checked)
                    {
                        if (!String.IsNullOrEmpty(((Label)loItem.Controls[15]).Text))
                        {
                            ReservaEdicionReparto oReservaEdicionReparto = new ReservaEdicionReparto
                            {
                                CLIENTE = ((Label)loItem.Controls[3]).Text,
                                PRODUCTO = ((Label)loItem.Controls[7]).Text,
                                EDICION = ((Label)loItem.Controls[9]).Text,
                                CODIGO_EDICION = Convert.ToInt32(((Label)loItem.Controls[19]).Text),
                                CODIGO_CLIENTE = ((Label)loItem.Controls[13]).Text,
                                DIRECCION_MAPS = ((Label)loItem.Controls[15]).Text,
                                CLIENTE_NOMBRE = ((Label)loItem.Controls[17]).Text,
                                ID_RESERVA_EDICION = Convert.ToInt32(((Label)loItem.Controls[11]).Text)
                            };

                            lstReservaEdicionReparto.Add(oReservaEdicionReparto);

                            BLL.DAL.Cliente oCliente = new BLL.DAL.Cliente();
                            oCliente.ID_CLIENTE = Convert.ToInt32(((Label)loItem.Controls[13]).Text);
                            oCliente.DIRECCION_MAPS = ((Label)loItem.Controls[15]).Text;
                            var loExiste = lstCliente.Where(p => p.ID_CLIENTE == oCliente.ID_CLIENTE).ToList().Count == 1;
                            if (!loExiste)
                                lstCliente.Add(oCliente);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoClienteSinDireccion + ((Label)loItem.Controls[17]).Text));
                            return;
                        }
                    }
                }

                if (lstCliente.Count > 0 && lstReservaEdicionReparto.Count > 0)
                {
                    Session.Add(Enums.Session.ClientesHojaDeRuta.ToString(), lstCliente);
                    Session.Add(Enums.Session.ReservasHojaDeRuta.ToString(), lstReservaEdicionReparto);
                    Response.Redirect("HojaDeRuta.aspx", false);
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeRepartoInfo));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private ReservaFiltro CargarReservaFiltro()
        {
            ReservaFiltro oReservaFiltro = new ReservaFiltro();

            if (!String.IsNullOrEmpty(txtProducto.Text))
                oReservaFiltro.NOMBRE_PRODUCTO = txtProducto.Text;

            if (!String.IsNullOrEmpty(txtEdicion.Text))
                oReservaFiltro.EDICION = txtEdicion.Text;

            if (!String.IsNullOrEmpty(txtNombreEdicion.Text))
                oReservaFiltro.NOMBRE_EDICION = txtNombreEdicion.Text;

            return oReservaFiltro;
        }

        private void CargarGrillaReservas()
        {
            try
            {
                var oReservaFiltro = CargarReservaFiltro();

                var lstReserva = new ReservaEdicionBLL().ObtenerReservaEdicionConEnvioDomicilio(oReservaFiltro);

                if (lstReserva != null && lstReserva.Count > 0)
                    lsvReserva.DataSource = lstReserva;
                else
                {
                    dvMensajelsvReserva.InnerHtml = MessageManager.Info(dvMensajelsvReserva, Message.MsjeListadoReservaFiltroSinResultados, false);
                    dvMensajelsvReserva.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lsvReserva.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvReserva.DataBind();
        }

        private void LimpiarCampos()
        {
            FormReparto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            CargarGrillaReservas();
        }

        #endregion
    }
}