using BLL.Common;
using BLL.DAL;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class EmpleadoVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarEmpleado();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EmpleadoEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdEmpleado.ToString());
            Response.Redirect("EmpleadoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarEmpleado()
        {
            try
            {
                if (Session[Enums.Session.IdEmpleado.ToString()] != null)
                {
                    var loIdEmpleado = Convert.ToInt32(Session[Enums.Session.IdEmpleado.ToString()]);

                    if (loIdEmpleado > 0)
                    {
                        using (var loRepEmpleado = new Repository<BLL.DAL.Empleado>())
                        {
                            var loEmpleado = loRepEmpleado.Find(p => p.ID_EMPLEADO == loIdEmpleado);

                            if (loEmpleado != null)
                            {
                                txtCodigo.Text = loEmpleado.ID_EMPLEADO.ToString();
                                txtFechaAlta.Text = loEmpleado.FECHA_ALTA.ToString("dd/MM/yyyy");
                                txtTipoDocumento.Text = loEmpleado.TipoDocumento.DESCRIPCION;
                                txtNroDocumento.Text = loEmpleado.NRO_DOCUMENTO.ToString();
                                txtNombre.Text = loEmpleado.NOMBRE;
                                txtApellido.Text = loEmpleado.APELLIDO;
                                txtCuil.Text = loEmpleado.CUIL;
                                if (loEmpleado.EMAIL != null)
                                    txtEmail.Text = loEmpleado.EMAIL;
                                txtTelefonoMovil.Text = loEmpleado.TELEFONO_MOVIL;
                                if (loEmpleado.TELEFONO_FIJO != null)
                                    txtTelefonoFijo.Text = loEmpleado.TELEFONO_FIJO;
                                if (loEmpleado.CALLE != null)
                                    txtCalle.Text = loEmpleado.CALLE;
                                if (loEmpleado.NUMERO != null)
                                    txtNumero.Text = loEmpleado.NUMERO.ToString();
                                if (loEmpleado.PISO != null)
                                    txtPiso.Text = loEmpleado.PISO;
                                if (loEmpleado.DEPARTAMENTO != null)
                                    txtDepartamento.Text = loEmpleado.DEPARTAMENTO;
                                if (loEmpleado.LOCALIDAD != null)
                                    txtLocalidad.Text = loEmpleado.LOCALIDAD;
                                if (loEmpleado.PROVINCIA != null)
                                    txtProvincia.Text = loEmpleado.PROVINCIA;
                                if (loEmpleado.BARRIO != null)
                                    txtBarrio.Text = loEmpleado.BARRIO;
                                if (loEmpleado.CODIGO_POSTAL != null)
                                    txtCodigoPostal.Text = loEmpleado.CODIGO_POSTAL;
                            }
                        }
                    }
                }
                else
                    Response.Redirect("EmpleadoListado.aspx", false);
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