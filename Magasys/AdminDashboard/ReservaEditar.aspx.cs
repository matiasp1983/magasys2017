using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class ReservaEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarReservaDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(txtFechaFin.Text) && Convert.ToDateTime(txtFechaFin.Text) < Convert.ToDateTime(txtFechaInicio.Text))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaFechaInicioMayorQueFechaFin)); //"La Fecha de fin debe ser mayor que la Fecha de inicio."
                    return;
                }

                if (Session[Enums.Session.IdReserva.ToString()] != null)
                {
                    var lvIdReserva = Convert.ToInt32(Session[Enums.Session.IdReserva.ToString()]);

                    using (var repReserva = new Repository<BLL.DAL.Reserva>())
                    {
                        var oReserva = repReserva.Find(p => p.ID_RESERVA == lvIdReserva);

                        if (oReserva.COD_TIPO_RESERVA == 1 && String.IsNullOrEmpty(txtFechaFin.Text))
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaUnicaFechafin)); //"Para la reserva Única, el campo Fecha de fin es requerido."
                            return;
                        }

                        if (rdbEnvioDomicilio.Checked == true && String.IsNullOrEmpty(oReserva.Cliente.DIRECCION_MAPS))
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeReservaDireccionRequerida)); //"La forma de entrega “Envío a Domicilio” requiere que el cliente complete los datos de la dirección."
                            return;
                        }
                    }

                    var loReserva = CargarReservaDesdeControles();

                    if (loReserva != null)
                    {
                        var loResutado = new BLL.ReservaBLL().ModificarReserva(loReserva);
                        if (loResutado)
                        {
                            Session.Remove(Enums.Session.IdReserva.ToString());
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeReservaSuccessModificacion, "Modificación Reserva", "ReservaListado.aspx"));
                        }
                        else
                            Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));
                    }
                }
                else
                    Response.Redirect("ReservaListado.aspx", false);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeReservaFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdReserva.ToString());
            Response.Redirect("ReservaListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarReservaDesdeSession()
        {
            BLL.DAL.Reserva oReserva = null;

            try
            {
                if (Session[Enums.Session.IdReserva.ToString()] != null)
                {
                    var lvIdReserva = Convert.ToInt32(Session[Enums.Session.IdReserva.ToString()]);

                    using (var repReserva = new Repository<BLL.DAL.Reserva>())
                    {
                        oReserva = repReserva.Find(p => p.ID_RESERVA == lvIdReserva);

                        txtCodigo.Text = oReserva.ID_RESERVA.ToString();
                        txtFechaAlta.Text = oReserva.FECHA.ToString("dd/MM/yyyy");
                        txtFechaInicio.Text = Convert.ToDateTime(oReserva.FECHA_INICIO).ToString("dd/MM/yyyy");
                        if (!String.IsNullOrEmpty(oReserva.FECHA_FIN.ToString()))
                            txtFechaFin.Text = Convert.ToDateTime(oReserva.FECHA_FIN).ToString("dd/MM/yyyy");
                        if (oReserva.ENVIO_DOMICILIO == null)
                            rdbRetiraEnLocal.Checked = true; // "Retira en Local"
                        else
                            rdbEnvioDomicilio.Checked = true; // "Envío a Domicilio"
                        txtTipoReserva.Text = oReserva.TipoReserva.DESCRIPCION;
                        txtEstado.Text = oReserva.Estado.NOMBRE;
                        txtTipoDocumento.Text = oReserva.Cliente.TipoDocumento.DESCRIPCION;
                        txtNumeroDocumento.Text = oReserva.Cliente.NRO_DOCUMENTO.ToString();
                        txtNombre.Text = oReserva.Cliente.NOMBRE;
                        txtApellido.Text = oReserva.Cliente.APELLIDO;
                        txtNombreProducto.Text = oReserva.Producto.NOMBRE;
                        txtDescripcionProducto.Text = oReserva.Producto.DESCRIPCION;
                    }
                }
                else
                    Response.Redirect("ReservaListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Reserva CargarReservaDesdeControles()
        {
            BLL.DAL.Reserva loReserva = new BLL.DAL.Reserva();

            var lvIdReserva = Convert.ToInt32(Session[Enums.Session.IdReserva.ToString()]);

            using (var repReserva = new Repository<BLL.DAL.Reserva>())
            {
                var oReserva = repReserva.Find(p => p.ID_RESERVA == lvIdReserva);

                if (rdbRetiraEnLocal.Checked == true && oReserva.ENVIO_DOMICILIO != null)
                    oReserva.ENVIO_DOMICILIO = null;
                else if (rdbEnvioDomicilio.Checked == true && oReserva.ENVIO_DOMICILIO == null)
                    oReserva.ENVIO_DOMICILIO = "X";
                oReserva.FECHA_INICIO = Convert.ToDateTime(txtFechaInicio.Text);
                if (!String.IsNullOrEmpty(txtFechaFin.Text))
                    oReserva.FECHA_FIN = Convert.ToDateTime(txtFechaFin.Text);
                else if (String.IsNullOrEmpty(txtFechaFin.Text) && oReserva.FECHA_FIN != null)
                    oReserva.FECHA_FIN = null;

                loReserva = oReserva;
            }

            return loReserva;
        }

        #endregion

    }
}