using BLL.Common;
using BLL.DAL;
using NLog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Proveedor : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oProveedor = CargarProveedorDesdeControles();
            var bEsNuevaRazonSocial = new BLL.ProveedorBLL().ConsultarExistenciaRazonSocial(oProveedor.RAZON_SOCIAL);
            if (!bEsNuevaRazonSocial)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeRazonSocialProveedorExist));
                return;
            }

            try
            {
                if (oProveedor != null)
                {
                    var loResultado = new BLL.ProveedorBLL().AltaProveedor(oProveedor);

                    if (loResultado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProveedorSuccessAlta, "Alta Proveedor", "Proveedor.aspx"));
                        LimpiarCampos();
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProveedorFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProveedorListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private BLL.DAL.Proveedor CargarProveedorDesdeControles()
        {
            var oProveedor = new BLL.DAL.Proveedor
            {
                ID_PROVEEDOR = 0,
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1,
                CUIT = txtCuit.Text,
                RAZON_SOCIAL = txtRazonSocial.Text,
                NOMBRE = txtNombre.Text,
                APELLIDO = txtApellido.Text,
                TELEFONO_MOVIL = txtTelefonoMovil.Text
            };

            if (!String.IsNullOrEmpty(txtTelefonoFijo.Text))
                oProveedor.TELEFONO_FIJO = txtTelefonoFijo.Text;
            else
                oProveedor.TELEFONO_FIJO = null;

            oProveedor.EMAIL = txtEmail.Text;

            if (!String.IsNullOrEmpty(hdCalle.Value))
                oProveedor.CALLE = hdCalle.Value;
            else
                oProveedor.CALLE = null;

            if (!String.IsNullOrEmpty(hdNumero.Value))
                oProveedor.NUMERO = Convert.ToInt32(hdNumero.Value);
            else
                oProveedor.NUMERO = null;

            if (!String.IsNullOrEmpty(txtPiso.Text))
                oProveedor.PISO = txtPiso.Text;
            else
                oProveedor.PISO = null;

            if (!String.IsNullOrEmpty(txtDepartamento.Text))
                oProveedor.DEPARTAMENTO = txtDepartamento.Text;
            else
                oProveedor.DEPARTAMENTO = null;

            if (!String.IsNullOrEmpty(hdLocalidad.Value))
                oProveedor.LOCALIDAD = hdLocalidad.Value;
            else
                oProveedor.LOCALIDAD = null;

            if (!String.IsNullOrEmpty(hdProvincia.Value))
                oProveedor.PROVINCIA = hdProvincia.Value;
            else
                oProveedor.PROVINCIA = null;

            if (!String.IsNullOrEmpty(hdCalle.Value) && !String.IsNullOrEmpty(hdBarrio.Value))
                oProveedor.BARRIO = hdBarrio.Value;
            else if (!String.IsNullOrEmpty(hdCalle.Value) && !String.IsNullOrEmpty(txtBarrio.Text))
                oProveedor.BARRIO = txtBarrio.Text;
            else if (String.IsNullOrEmpty(txtBarrio.Text))
                oProveedor.BARRIO = null;

            if (!String.IsNullOrEmpty(hdCodigoPostal.Value))
                oProveedor.CODIGO_POSTAL = hdCodigoPostal.Value;
            else
                oProveedor.CODIGO_POSTAL = null;

            if (!String.IsNullOrEmpty(hdIdDireccionMaps.Value))
                oProveedor.DIRECCION_MAPS = hdIdDireccionMaps.Value;
            else
                oProveedor.DIRECCION_MAPS = null;

            return oProveedor;
        }

        private static bool ValidaCuit(string cuit)
        {
            //Validar que el CUIT sea numérico
            Int64 locuit = 0;
            if (!Int64.TryParse(cuit, out locuit)) return false;

            //Validar que el CUIT sea positivo
            if (long.Parse(cuit) <= 0) return false;

            //Validar que el CUIT conste de 11 cifras
            if (cuit.Length != 11) return false;

            var loDigitoCalcu = Utilities.CalcularDigitoCuit(cuit);
            var loParseSubStr = int.Parse(cuit.Substring(10));
            return loDigitoCalcu == loParseSubStr;
        }

        private void LimpiarCampos()
        {
            FormProveedor.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProveedor.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
        }

        #endregion

        #region Métodos Públicos

        [WebMethod]
        public static bool ValidarCuitProveedor(string pCuit)
        {
            if (ValidaCuit(pCuit))
            {
                return new BLL.ProveedorBLL().ConsultarExistenciaCuit(pCuit);
            }

            return false;
        }

        #endregion
    }

    #region Clases

    public class ItemOptionLocalidad
    {
        public String Value { get; set; }
        public String Text { get; set; }
    }

    #endregion
}