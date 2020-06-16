using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Web.UI;

namespace PL.AdminDashboard
{
    public partial class EmpleadoEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarEmpleado();
        }

        protected void BtnBorrarDireccion_Click(object sender, EventArgs e)
        {
            txtCalle.Text = String.Empty;
            hdCalle.Value = String.Empty;
            txtNumero.Text = String.Empty;
            hdNumero.Value = String.Empty;
            txtPiso.Text = String.Empty;
            txtDepartamento.Text = String.Empty;
            txtLocalidad.Text = String.Empty;
            hdLocalidad.Value = String.Empty;
            txtProvincia.Text = String.Empty;
            hdProvincia.Value = String.Empty;
            txtBarrio.Text = String.Empty;
            hdBarrio.Value = String.Empty;
            txtCodigoPostal.Text = String.Empty;
            hdCodigoPostal.Value = String.Empty;
            hdIdDireccionMaps.Value = String.Empty;
            hdLatitud.Value = String.Empty;
            hdLongitud.Value = String.Empty;
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oEmpleado = CargarEmpleadoDesdeControles();

            try
            {
                if (oEmpleado != null)
                {
                    var loResutado = new BLL.EmpleadoBLL().ModificarEmpleado(oEmpleado);

                    if (loResutado)
                    {
                        Session.Remove(Enums.Session.IdEmpleado.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeEmpleadoSuccessModificacion, "Modificación Empleado", "EmpleadoListado.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEmpleadoFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeEmpleadoSinModificaciones));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeEmpleadoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
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

        private BLL.DAL.Empleado CargarEmpleadoDesdeControles()
        {
            BLL.DAL.Empleado loEmpleado = new BLL.DAL.Empleado();
            bool bModificado = false;

            var loIdEmpleado = Convert.ToInt32(Session[Enums.Session.IdEmpleado.ToString()]);

            using (var loRepEmpleado = new Repository<BLL.DAL.Empleado>())
            {
                var loEmpleadoAux = loRepEmpleado.Find(p => p.ID_EMPLEADO == loIdEmpleado);

                if (loEmpleadoAux != null)
                {
                    // Nombre
                    if (loEmpleadoAux.NOMBRE != txtNombre.Text)
                    {
                        loEmpleadoAux.NOMBRE = txtNombre.Text;
                        bModificado = true;
                    }

                    // Apellido
                    if (loEmpleadoAux.APELLIDO != txtApellido.Text)
                    {
                        loEmpleadoAux.APELLIDO = txtApellido.Text;
                        bModificado = true;
                    }

                    // Email
                    if (!String.IsNullOrEmpty(txtEmail.Text) && loEmpleadoAux.EMAIL != txtEmail.Text)
                    {
                        loEmpleadoAux.EMAIL = txtEmail.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtEmail.Text) && loEmpleadoAux.EMAIL != null)
                    {
                        loEmpleadoAux.EMAIL = null;
                        bModificado = true;
                    }

                    // Teléfono Móvil
                    if (loEmpleadoAux.TELEFONO_MOVIL != txtTelefonoMovil.Text)
                    {
                        loEmpleadoAux.TELEFONO_MOVIL = txtTelefonoMovil.Text;
                        bModificado = true;
                    }

                    // Teléfono Fijo
                    if (!String.IsNullOrEmpty(txtTelefonoFijo.Text) && loEmpleadoAux.TELEFONO_FIJO != txtTelefonoFijo.Text)
                    {
                        loEmpleadoAux.TELEFONO_FIJO = txtTelefonoFijo.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtTelefonoFijo.Text) && loEmpleadoAux.TELEFONO_FIJO != null)
                    {
                        loEmpleadoAux.TELEFONO_FIJO = null;
                        bModificado = true;
                    }

                    // Calle
                    if (!String.IsNullOrEmpty(hdCalle.Value) && loEmpleadoAux.CALLE != hdCalle.Value)
                    {
                        loEmpleadoAux.CALLE = hdCalle.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtCalle.Text) && loEmpleadoAux.CALLE != txtCalle.Text)
                    {
                        loEmpleadoAux.CALLE = txtCalle.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCalle.Text) && loEmpleadoAux.CALLE != null)
                    {
                        loEmpleadoAux.CALLE = null;
                        bModificado = true;
                    }

                    // Número
                    if (!String.IsNullOrEmpty(hdNumero.Value) && loEmpleadoAux.NUMERO != Convert.ToInt32(hdNumero.Value))
                    {
                        loEmpleadoAux.NUMERO = Convert.ToInt32(hdNumero.Value);
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtNumero.Text) && loEmpleadoAux.NUMERO != Convert.ToInt32(txtNumero.Text))
                    {
                        loEmpleadoAux.NUMERO = Convert.ToInt32(txtNumero.Text);
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtNumero.Text) && loEmpleadoAux.NUMERO != null)
                    {
                        loEmpleadoAux.NUMERO = null;
                        bModificado = true;
                    }

                    // Localidad
                    if (!String.IsNullOrEmpty(hdLocalidad.Value) && loEmpleadoAux.LOCALIDAD != hdLocalidad.Value)
                    {
                        loEmpleadoAux.LOCALIDAD = hdLocalidad.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtLocalidad.Text) && loEmpleadoAux.LOCALIDAD != txtLocalidad.Text)
                    {
                        loEmpleadoAux.LOCALIDAD = txtLocalidad.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtLocalidad.Text) && loEmpleadoAux.LOCALIDAD != null)
                    {
                        loEmpleadoAux.LOCALIDAD = null;
                        bModificado = true;
                    }

                    // Provincia
                    if (!String.IsNullOrEmpty(hdProvincia.Value) && loEmpleadoAux.PROVINCIA != hdProvincia.Value)
                    {
                        loEmpleadoAux.PROVINCIA = hdProvincia.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtProvincia.Text) && loEmpleadoAux.PROVINCIA != txtProvincia.Text)
                    {
                        loEmpleadoAux.PROVINCIA = txtProvincia.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtProvincia.Text) && loEmpleadoAux.PROVINCIA != null)
                    {
                        loEmpleadoAux.PROVINCIA = null;
                        bModificado = true;
                    }

                    // Piso
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtPiso.Text) && loEmpleadoAux.PISO != txtPiso.Text)
                    {
                        loEmpleadoAux.PISO = txtPiso.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtPiso.Text) && loEmpleadoAux.PISO != null)
                    {
                        loEmpleadoAux.PISO = null;
                        bModificado = true;
                    }

                    // Departamento
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtDepartamento.Text) && loEmpleadoAux.DEPARTAMENTO != txtDepartamento.Text)
                    {
                        loEmpleadoAux.DEPARTAMENTO = txtDepartamento.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtDepartamento.Text) && loEmpleadoAux.DEPARTAMENTO != null)
                    {
                        loEmpleadoAux.DEPARTAMENTO = null;
                        bModificado = true;
                    }

                    // Barrio
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(hdBarrio.Value) && loEmpleadoAux.BARRIO != hdBarrio.Value)
                    {
                        loEmpleadoAux.BARRIO = hdBarrio.Value;
                        bModificado = true;
                    }
                    else if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtBarrio.Text) && loEmpleadoAux.BARRIO != txtBarrio.Text)
                    {
                        loEmpleadoAux.BARRIO = txtBarrio.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCalle.Text) && String.IsNullOrEmpty(txtBarrio.Text) && loEmpleadoAux.BARRIO != null)
                    {
                        loEmpleadoAux.BARRIO = null;
                        bModificado = true;
                    }

                    // Código Postal
                    if (!String.IsNullOrEmpty(hdCodigoPostal.Value) && loEmpleadoAux.CODIGO_POSTAL != hdCodigoPostal.Value)
                    {
                        loEmpleadoAux.CODIGO_POSTAL = hdCodigoPostal.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtCodigoPostal.Text) && loEmpleadoAux.CODIGO_POSTAL != txtCodigoPostal.Text)
                    {
                        loEmpleadoAux.CODIGO_POSTAL = txtCodigoPostal.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCodigoPostal.Text) && loEmpleadoAux.CODIGO_POSTAL != null)
                    {
                        loEmpleadoAux.CODIGO_POSTAL = null;
                        bModificado = true;
                    }

                    // Dirección Maps
                    if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                        loEmpleadoAux.DIRECCION_MAPS = hdIdDireccionMaps.Value;
                    else if (loEmpleadoAux.CALLE == null)
                        loEmpleadoAux.DIRECCION_MAPS = null;

                    // Pregunta si el cliente ha sido modificado
                    if (bModificado)
                        loEmpleado = loEmpleadoAux;
                    else
                        loEmpleado = null;
                }
            }
            return loEmpleado;
        }

        #endregion
    }
}