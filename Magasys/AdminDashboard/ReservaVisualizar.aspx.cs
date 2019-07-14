using BLL.Common;
using BLL.DAL;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ReservaVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarReserva();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReservaEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdReserva.ToString());
            Response.Redirect("ReservaListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarReserva()
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
                        txtFechaIni.Text = Convert.ToDateTime(oReserva.FECHA_INICIO).ToString("dd/MM/yyyy");
                        if (!String.IsNullOrEmpty(oReserva.FECHA_FIN.ToString()))
                            txtFechaFin.Text = Convert.ToDateTime(oReserva.FECHA_FIN).ToString("dd/MM/yyyy");
                        txtTipoReserva.Text = oReserva.TipoReserva.DESCRIPCION;
                        if (oReserva.ENVIO_DOMICILIO == null)
                            txtFormaEntrega.Text = "Retira en Local";
                        else
                            txtFormaEntrega.Text = "Envío a Domicilio";
                        txtEstado.Text = oReserva.Estado.NOMBRE;
                        txtTipoDocumento.Text = oReserva.Cliente.TipoDocumento.DESCRIPCION;
                        txtNumeroDocumento.Text = oReserva.Cliente.NRO_DOCUMENTO.ToString();
                        txtNombre.Text = oReserva.Cliente.NOMBRE;
                        txtApellido.Text = oReserva.Cliente.APELLIDO;
                        txtNombreProducto.Text = oReserva.Producto.NOMBRE;
                        txtDescripcionProducto.Text = oReserva.Producto.DESCRIPCION;
                    }
                    if (oReserva.COD_ESTADO == 8 || oReserva.COD_ESTADO == 9) //Las reservas con estado Finalizada o Anulada no se pueden Editar
                        btnModificar.Visible = false;
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

        #endregion
    }
}