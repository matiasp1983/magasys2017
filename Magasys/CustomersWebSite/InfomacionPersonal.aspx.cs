﻿using BLL.Common;
using NLog;
using System;
using System.Web.UI;

namespace PL.CustomersWebSite
{
    public partial class InformacionPersonal : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCantidadDePedidosDesdeSession();
                CargarInformacionPersonalDesdeSession();
            }
        }

        protected void BtnBorrarDireccion_Click(object sender, EventArgs e)
        {
            txtCalle.Text = string.Empty;
            hdCalle.Value = string.Empty;
            txtNumero.Text = string.Empty;
            hdNumero.Value = string.Empty;
            txtPiso.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtLocalidad.Text = string.Empty;
            hdLocalidad.Value = string.Empty;
            txtProvincia.Text = string.Empty;
            hdProvincia.Value = string.Empty;
            txtBarrio.Text = string.Empty;
            hdBarrio.Value = string.Empty;
            txtCodigoPostal.Text = string.Empty;
            hdCodigoPostal.Value = string.Empty;
            hdIdDireccionMaps.Value = string.Empty;
            hdLatitud.Value = String.Empty;
            hdLongitud.Value = String.Empty;
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oUsuario = CargarUsuarioDesdeControles();
            var oCliente = CargarClienteDesdeControles();

            try
            {
                if (oUsuario != null && oCliente != null)
                {
                    var loResutadoUsuario = new BLL.UsuarioBLL().ModificarUsuario(oUsuario);
                    var loResutadoCliente = new BLL.ClienteBLL().ModificarCliente(oCliente);

                    if (loResutadoUsuario && loResutadoCliente)
                    {
                        ActualizarSessionUsuario(oUsuario);
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeInformacionPersonalSuccessModificacion, "Modificación Información Personal", "Index.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeInformacionPersonal));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeInformacionPersonal));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Index.aspx", false);
        }

        protected void btnCambiarContrasenia_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(txtContraseniaNueva.Text) && !string.IsNullOrEmpty(txtContraseniaNuevaConfirmar.Text)) && txtContraseniaNueva.Text.Equals(txtContraseniaNuevaConfirmar.Text))
                txtContrasenia.Attributes["value"] = txtContraseniaNueva.Text;
        }

        #endregion

        #region Métodos Privados

        private void CargarInformacionPersonalDesdeSession()
        {
            try
            {
                if (Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
                {
                    var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
                    var oCliente = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

                    if (oUsuario != null && oCliente != null)
                    {
                        txtCliente.Text = oCliente.ID_CLIENTE.ToString();
                        txtFechaAlta.Text = oCliente.FECHA_ALTA.ToString("dd/MM/yyyy");
                        txtTipoDocumento.Text = new BLL.TipoDocumentoBLL().ObtenerTipoDocumento(oCliente.TIPO_DOCUMENTO).DESCRIPCION;
                        txtNroDocumento.Text = oCliente.NRO_DOCUMENTO.ToString();
                        txtNombre.Text = oCliente.NOMBRE;
                        txtApellido.Text = oCliente.APELLIDO;
                        if (oCliente.ALIAS != null)
                            txtAlias.Text = oCliente.ALIAS;
                        if (oCliente.EMAIL != null)
                            txtEmail.Text = oCliente.EMAIL;
                        txtTelefonoMovil.Text = oCliente.TELEFONO_MOVIL;
                        if (oCliente.TELEFONO_FIJO != null)
                            txtTelefonoFijo.Text = oCliente.TELEFONO_FIJO;
                        if (oCliente.CALLE != null)
                            txtCalle.Text = oCliente.CALLE;
                        if (oCliente.NUMERO != null)
                            txtNumero.Text = oCliente.NUMERO.ToString();
                        if (oCliente.PISO != null)
                            txtPiso.Text = oCliente.PISO;
                        if (oCliente.DEPARTAMENTO != null)
                            txtDepartamento.Text = oCliente.DEPARTAMENTO;
                        if (oCliente.LOCALIDAD != null)
                            txtLocalidad.Text = oCliente.LOCALIDAD;
                        if (oCliente.PROVINCIA != null)
                            txtProvincia.Text = oCliente.PROVINCIA;
                        if (oCliente.BARRIO != null)
                            txtBarrio.Text = oCliente.BARRIO;
                        if (oCliente.CODIGO_POSTAL != null)
                            txtCodigoPostal.Text = oCliente.CODIGO_POSTAL;

                        txtContrasenia.Attributes["value"] = oUsuario.CONTRASENIA;

                        if (oUsuario.AVATAR != null)
                        {
                            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oUsuario.AVATAR);
                            imgPreview.ImageUrl = loImagenDataURL64;
                        }
                    }
                }
                else
                    Response.Redirect("Index.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Cliente CargarClienteDesdeControles()
        {
            var oCliente = new BLL.DAL.Cliente();

            var oUsuario = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oClienteSession = new BLL.ClienteBLL().ObtenerClientePorUsuario(oUsuario.ID_USUARIO);

            if (oClienteSession != null)
            {
                oCliente.ID_CLIENTE = oClienteSession.ID_CLIENTE;
                oCliente.FECHA_ALTA = oClienteSession.FECHA_ALTA;
                oCliente.COD_ESTADO = oClienteSession.COD_ESTADO;
                oCliente.TIPO_DOCUMENTO = oClienteSession.TIPO_DOCUMENTO;
                oCliente.NRO_DOCUMENTO = oClienteSession.NRO_DOCUMENTO;
                oCliente.COD_USUARIO = oClienteSession.COD_USUARIO;
            }

            if (!string.IsNullOrEmpty(txtNombre.Text))
                oCliente.NOMBRE = txtNombre.Text;

            if (!string.IsNullOrEmpty(txtApellido.Text))
                oCliente.APELLIDO = txtApellido.Text;

            if (!string.IsNullOrEmpty(txtAlias.Text))
                oCliente.ALIAS = txtAlias.Text;

            if (!string.IsNullOrEmpty(txtEmail.Text))
                oCliente.EMAIL = txtEmail.Text;

            if (!string.IsNullOrEmpty(txtTelefonoMovil.Text))
                oCliente.TELEFONO_MOVIL = txtTelefonoMovil.Text;

            if (!string.IsNullOrEmpty(txtTelefonoFijo.Text))
                oCliente.TELEFONO_FIJO = txtTelefonoFijo.Text;

            if (!string.IsNullOrEmpty(hdCalle.Value))
                oCliente.CALLE = hdCalle.Value;
            else if (!string.IsNullOrEmpty(txtCalle.Text))
                oCliente.CALLE = txtCalle.Text;

            if (!string.IsNullOrEmpty(hdNumero.Value))
                oCliente.NUMERO = Convert.ToInt32(hdNumero.Value);
            else if (!string.IsNullOrEmpty(txtNumero.Text))
                oCliente.NUMERO = Convert.ToInt32(txtNumero.Text);

            if (!string.IsNullOrEmpty(txtPiso.Text))
                oCliente.PISO = txtPiso.Text;

            if (!string.IsNullOrEmpty(txtDepartamento.Text))
                oCliente.DEPARTAMENTO = txtDepartamento.Text;

            if (!string.IsNullOrEmpty(hdLocalidad.Value))
                oCliente.LOCALIDAD = hdLocalidad.Value;
            else if (!string.IsNullOrEmpty(txtLocalidad.Text))
                oCliente.LOCALIDAD = txtLocalidad.Text;

            if (!string.IsNullOrEmpty(hdProvincia.Value))
                oCliente.PROVINCIA = hdProvincia.Value;
            else if (!string.IsNullOrEmpty(txtProvincia.Text))
                oCliente.PROVINCIA = txtProvincia.Text;

            if (!string.IsNullOrEmpty(hdBarrio.Value))
                oCliente.BARRIO = hdBarrio.Value;
            else if (!string.IsNullOrEmpty(txtBarrio.Text))
                oCliente.BARRIO = txtBarrio.Text;

            if (!string.IsNullOrEmpty(hdCodigoPostal.Value))
                oCliente.CODIGO_POSTAL = hdCodigoPostal.Value;
            else if (!string.IsNullOrEmpty(txtCodigoPostal.Text))
                oCliente.CODIGO_POSTAL = txtCodigoPostal.Text;

            // Dirección Maps
            if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                oCliente.DIRECCION_MAPS = hdIdDireccionMaps.Value;
            else
                oCliente.DIRECCION_MAPS = null;

            // Latitud Maps
            if (!String.IsNullOrEmpty(hdLatitud.Value))
                oCliente.LATITUD = Convert.ToDouble(hdLatitud.Value);
            else
                oCliente.LATITUD = null;

            // Longitud Maps
            if (!String.IsNullOrEmpty(hdLongitud.Value))
                oCliente.LONGITUD = Convert.ToDouble(hdLongitud.Value);
            else
                oCliente.LONGITUD = null;

            return oCliente;
        }

        private BLL.DAL.Usuario CargarUsuarioDesdeControles()
        {
            var oUsuarioSession = (BLL.DAL.Usuario)Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()];
            var oUsuario = new BLL.DAL.Usuario
            {
                ID_USUARIO = oUsuarioSession.ID_USUARIO,
                FECHA_ALTA = oUsuarioSession.FECHA_ALTA,
                COD_ESTADO = oUsuarioSession.COD_ESTADO,
                NOMBRE_USUARIO = oUsuarioSession.NOMBRE_USUARIO,
                ID_ROL = oUsuarioSession.ID_ROL,
                RECUPERAR_CONTRASENIA = oUsuarioSession.RECUPERAR_CONTRASENIA,
                CONTRASENIA = txtContrasenia.Attributes["value"]
            };

            if (!string.IsNullOrEmpty(txtNombre.Text))
                oUsuario.NOMBRE = txtNombre.Text;

            if (!string.IsNullOrEmpty(txtApellido.Text))
                oUsuario.APELLIDO = txtApellido.Text;

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
                    if (oUsuarioSession.AVATAR != null)
                    {
                        oUsuario.AVATAR = oUsuarioSession.AVATAR;
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oUsuario.AVATAR);
                        imgPreview.ImageUrl = loImagenDataURL64;
                    }
                }
            }

            return oUsuario;
        }

        private void CargarCantidadDePedidosDesdeSession()
        {
            Master.CantidadDePedidos = Convert.ToInt32(Session[Enums.Session.CantidadDePedidos.ToString()]);
        }

        private void ActualizarSessionUsuario(BLL.DAL.Usuario oUsuario)
        {
            Session[CustomersWebSiteSessionBLL.DefaultSessionsId.Usuario.ToString()] = oUsuario;
        }

        #endregion
    }
}