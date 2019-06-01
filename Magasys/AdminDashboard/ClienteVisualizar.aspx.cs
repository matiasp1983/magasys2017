using BLL.Common;
using BLL.DAL;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ClienteVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCliente();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Cliente.ToString());
            Response.Redirect("ClienteListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarCliente()
        {
            try
            {
                if (Session[Enums.Session.IdCliente.ToString()] != null)
                {
                    var loIdCliente = Convert.ToInt32(Session[Enums.Session.IdCliente.ToString()]);

                    if (loIdCliente > 0)
                    {
                        using (var loRepCliente = new Repository<BLL.DAL.Cliente>())
                        {
                            var loCliente = loRepCliente.Find(p => p.ID_CLIENTE == loIdCliente);

                            if (loCliente != null)
                            {
                                txtCodigo.Text = loCliente.ID_CLIENTE.ToString();
                                txtFechaAlta.Text = loCliente.FECHA_ALTA.ToString("dd/MM/yyyy");
                                txtTipoDocumento.Text = loCliente.TipoDocumento.DESCRIPCION;
                                txtNroDocumento.Text = loCliente.NRO_DOCUMENTO.ToString();
                                txtNombre.Text = loCliente.NOMBRE;
                                txtApellido.Text = loCliente.APELLIDO;
                                if (loCliente.ALIAS != null)
                                    txtAlias.Text = loCliente.ALIAS;
                                if (loCliente.EMAIL != null)
                                    txtEmail.Text = loCliente.EMAIL;
                                txtTelefonoMovil.Text = loCliente.TELEFONO_MOVIL;
                                if (loCliente.TELEFONO_FIJO != null)
                                    txtTelefonoFijo.Text = loCliente.TELEFONO_FIJO;
                                if (loCliente.CALLE != null)
                                    txtCalle.Text = loCliente.CALLE;
                                if (loCliente.NUMERO != null)
                                    txtNumero.Text = loCliente.NUMERO.ToString();
                                if (loCliente.PISO != null)
                                    txtPiso.Text = loCliente.PISO;
                                if (loCliente.DEPARTAMENTO != null)
                                    txtDepartamento.Text = loCliente.DEPARTAMENTO;
                                if (loCliente.LOCALIDAD != null)
                                    txtLocalidad.Text = loCliente.LOCALIDAD;
                                if (loCliente.PROVINCIA != null)
                                    txtProvincia.Text = loCliente.PROVINCIA;
                                if (loCliente.BARRIO != null)
                                    txtBarrio.Text = loCliente.BARRIO;
                                if (loCliente.CODIGO_POSTAL != null)
                                    txtCodigoPostal.Text = loCliente.CODIGO_POSTAL;
                            }
                        }
                    }
                }
                else
                    Response.Redirect("ClienteListado.aspx", false);
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