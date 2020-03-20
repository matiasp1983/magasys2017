using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.CustomersWebSite
{
    public partial class InfoPersonal : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCliente();
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
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oCliente = CargarClienteDesdeControles();

            try
            {
                if (oCliente != null)
                {
                    var loResutado = new BLL.ClienteBLL().ModificarCliente(oCliente);

                    if (loResutado)
                    {
                        //Session.Remove(Enums.Session.IdCliente.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeClienteSuccessModificacion, "Modificación Cliente", "ClienteListado.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.InfoModal(Message.MsjeClienteSinModificaciones));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeClienteFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            //Session.Remove(Enums.Session.IdCliente.ToString());
            //Response.Redirect("ClienteListado.aspx", false);
        }

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtContraseniaNueva.Text) && !string.IsNullOrEmpty(txtContraseniaNuevaConfirmar.Text)) && txtContraseniaNueva.Text.Equals(txtContraseniaNuevaConfirmar.Text))
                txtContrasenia.Attributes["value"] = txtContraseniaNueva.Text;
        }

        #endregion

        #region Métodos Privados

        private void CargarCliente()
        {
            try
            {
                //if (Session[Enums.Session.IdCliente.ToString()] != null)
                //{
                //var loIdCliente = Convert.ToInt32(Session[Enums.Session.IdCliente.ToString()]);
                var loIdCliente = 1;

                if (loIdCliente > 0)
                {
                    using (var loRepCliente = new Repository<Cliente>())
                    {
                        var loCliente = loRepCliente.Find(p => p.ID_CLIENTE == loIdCliente);

                        if (loCliente != null)
                        {
                            txtCliente.Text = loCliente.ID_CLIENTE.ToString();
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
                //}
                //else
                //    Response.Redirect("ClienteListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private Cliente CargarClienteDesdeControles()
        {
            Cliente loCliente = new Cliente();
            bool bModificado = false;

            //var loIdCliente = Convert.ToInt32(Session[Enums.Session.IdCliente.ToString()]);
            var loIdCliente = 1;

            using (var loRepCliente = new Repository<Cliente>())
            {
                var loClienteAux = loRepCliente.Find(p => p.ID_CLIENTE == loIdCliente);

                if (loClienteAux != null)
                {
                    // Nombre
                    if (loClienteAux.NOMBRE != txtNombre.Text)
                    {
                        loClienteAux.NOMBRE = txtNombre.Text;
                        bModificado = true;
                    }

                    // Apellido
                    if (loClienteAux.APELLIDO != txtApellido.Text)
                    {
                        loClienteAux.APELLIDO = txtApellido.Text;
                        bModificado = true;
                    }

                    // Alias
                    if (!String.IsNullOrEmpty(txtAlias.Text) && loClienteAux.ALIAS != txtAlias.Text)
                    {
                        loClienteAux.ALIAS = txtAlias.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtAlias.Text) && loClienteAux.ALIAS != null)
                    {
                        loClienteAux.ALIAS = null;
                        bModificado = true;
                    }

                    // Email
                    if (!String.IsNullOrEmpty(txtEmail.Text) && loClienteAux.EMAIL != txtEmail.Text)
                    {
                        loClienteAux.EMAIL = txtEmail.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtEmail.Text) && loClienteAux.EMAIL != null)
                    {
                        loClienteAux.EMAIL = null;
                        bModificado = true;
                    }

                    // Teléfono Móvil
                    if (loClienteAux.TELEFONO_MOVIL != txtTelefonoMovil.Text)
                    {
                        loClienteAux.TELEFONO_MOVIL = txtTelefonoMovil.Text;
                        bModificado = true;
                    }

                    // Teléfono Fijo
                    if (!String.IsNullOrEmpty(txtTelefonoFijo.Text) && loClienteAux.TELEFONO_FIJO != txtTelefonoFijo.Text)
                    {
                        loClienteAux.TELEFONO_FIJO = txtTelefonoFijo.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtTelefonoFijo.Text) && loClienteAux.TELEFONO_FIJO != null)
                    {
                        loClienteAux.TELEFONO_FIJO = null;
                        bModificado = true;
                    }

                    // Calle
                    if (!String.IsNullOrEmpty(hdCalle.Value) && loClienteAux.CALLE != hdCalle.Value)
                    {
                        loClienteAux.CALLE = hdCalle.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtCalle.Text) && loClienteAux.CALLE != txtCalle.Text)
                    {
                        loClienteAux.CALLE = txtCalle.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCalle.Text) && loClienteAux.CALLE != null)
                    {
                        loClienteAux.CALLE = null;
                        bModificado = true;
                    }

                    // Número
                    if (!String.IsNullOrEmpty(hdNumero.Value) && loClienteAux.NUMERO != Convert.ToInt32(hdNumero.Value))
                    {
                        loClienteAux.NUMERO = Convert.ToInt32(hdNumero.Value);
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtNumero.Text) && loClienteAux.NUMERO != Convert.ToInt32(txtNumero.Text))
                    {
                        loClienteAux.NUMERO = Convert.ToInt32(txtNumero.Text);
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtNumero.Text) && loClienteAux.NUMERO != null)
                    {
                        loClienteAux.NUMERO = null;
                        bModificado = true;
                    }

                    // Localidad
                    if (!String.IsNullOrEmpty(hdLocalidad.Value) && loClienteAux.LOCALIDAD != hdLocalidad.Value)
                    {
                        loClienteAux.LOCALIDAD = hdLocalidad.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtLocalidad.Text) && loClienteAux.LOCALIDAD != txtLocalidad.Text)
                    {
                        loClienteAux.LOCALIDAD = txtLocalidad.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtLocalidad.Text) && loClienteAux.LOCALIDAD != null)
                    {
                        loClienteAux.LOCALIDAD = null;
                        bModificado = true;
                    }

                    // Provincia
                    if (!String.IsNullOrEmpty(hdProvincia.Value) && loClienteAux.PROVINCIA != hdProvincia.Value)
                    {
                        loClienteAux.PROVINCIA = hdProvincia.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtProvincia.Text) && loClienteAux.PROVINCIA != txtProvincia.Text)
                    {
                        loClienteAux.PROVINCIA = txtProvincia.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtProvincia.Text) && loClienteAux.PROVINCIA != null)
                    {
                        loClienteAux.PROVINCIA = null;
                        bModificado = true;
                    }

                    // Piso
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtPiso.Text) && loClienteAux.PISO != txtPiso.Text)
                    {
                        loClienteAux.PISO = txtPiso.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtPiso.Text) && loClienteAux.PISO != null)
                    {
                        loClienteAux.PISO = null;
                        bModificado = true;
                    }

                    // Departamento
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtDepartamento.Text) && loClienteAux.DEPARTAMENTO != txtDepartamento.Text)
                    {
                        loClienteAux.DEPARTAMENTO = txtDepartamento.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtDepartamento.Text) && loClienteAux.DEPARTAMENTO != null)
                    {
                        loClienteAux.DEPARTAMENTO = null;
                        bModificado = true;
                    }

                    // Barrio
                    if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(hdBarrio.Value) && loClienteAux.BARRIO != hdBarrio.Value)
                    {
                        loClienteAux.BARRIO = hdBarrio.Value;
                        bModificado = true;
                    }
                    else if ((!String.IsNullOrEmpty(hdCalle.Value) || !String.IsNullOrEmpty(txtCalle.Text)) && !String.IsNullOrEmpty(txtBarrio.Text) && loClienteAux.BARRIO != txtBarrio.Text)
                    {
                        loClienteAux.BARRIO = txtBarrio.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCalle.Text) && String.IsNullOrEmpty(txtBarrio.Text) && loClienteAux.BARRIO != null)
                    {
                        loClienteAux.BARRIO = null;
                        bModificado = true;
                    }

                    // Código Postal
                    if (!String.IsNullOrEmpty(hdCodigoPostal.Value) && loClienteAux.CODIGO_POSTAL != hdCodigoPostal.Value)
                    {
                        loClienteAux.CODIGO_POSTAL = hdCodigoPostal.Value;
                        bModificado = true;
                    }
                    else if (!String.IsNullOrEmpty(txtCodigoPostal.Text) && loClienteAux.CODIGO_POSTAL != txtCodigoPostal.Text)
                    {
                        loClienteAux.CODIGO_POSTAL = txtCodigoPostal.Text;
                        bModificado = true;
                    }
                    else if (String.IsNullOrEmpty(txtCodigoPostal.Text) && loClienteAux.CODIGO_POSTAL != null)
                    {
                        loClienteAux.CODIGO_POSTAL = null;
                        bModificado = true;
                    }

                    // Dirección Maps
                    if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                        loClienteAux.DIRECCION_MAPS = hdIdDireccionMaps.Value;
                    else if (loClienteAux.CALLE == null)
                        loClienteAux.DIRECCION_MAPS = null;

                    // Pregunta si el cliente ha sido modificado
                    if (bModificado)
                        loCliente = loClienteAux;
                    else
                        loCliente = null;
                }
            }

            /*Inicio - Usuario*/

            var oUsuario = new BLL.DAL.Usuario();

            imgPreview.ImageUrl = "img/perfil_default.png";

            if (fuploadImagen.PostedFile.ContentLength != 0)
            {
                int loTamanioImagen = fuploadImagen.PostedFile.ContentLength;
                byte[] loImagenOriginal = new byte[loTamanioImagen];
                fuploadImagen.PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);
                oUsuario.AVATAR = loImagenOriginal;
                string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loImagenOriginal);
                imgPreview.ImageUrl = loImagenDataURL64;
            }
            else
            {
                if (hdfLimpiariarImagen.Value.Equals("false"))
                {
                    if (((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).AVATAR != null)
                    {
                        oUsuario.AVATAR = ((BLL.DAL.Usuario)base.Session[Enums.Session.Usuario.ToString()]).AVATAR;
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oUsuario.AVATAR);
                        imgPreview.ImageUrl = loImagenDataURL64;
                    }
                }
            }

            /*Fin - Usuario*/

            return loCliente;
        }

        #endregion
    }
}