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
            if (!IsPostBack)
                CargarProvincias();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var oProveedor = CargarProveedorDesdeControles();

            try
            {
                if (oProveedor != null)
                {
                    var loResultado = new BLL.ProveedorBLL().AltaProveedor(oProveedor);

                    if (loResultado)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProveedorSuccessAlta, "Alta Proveedor"));
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
            oProveedor.CALLE = txtCalle.Text;

            if (!String.IsNullOrEmpty(txtNumero.Text))
                oProveedor.NUMERO = Convert.ToInt32(txtNumero.Text);
            else
                oProveedor.NUMERO = 0;

            if (!String.IsNullOrEmpty(txtPiso.Text))
                oProveedor.PISO = txtPiso.Text;
            else
                oProveedor.PISO = null;

            if (!String.IsNullOrEmpty(txtDepartamento.Text))
                oProveedor.DEPARTAMENTO = txtDepartamento.Text;
            else
                oProveedor.DEPARTAMENTO = null;

            if (!String.IsNullOrEmpty(txtBarrio.Text))
                oProveedor.BARRIO = txtBarrio.Text;
            else
                oProveedor.BARRIO = null;

            oProveedor.CODIGO_POSTAL = txtCodigoPostal.Text;

            if (!String.IsNullOrEmpty(ddlProvincia.SelectedValue))            
                oProveedor.ID_PROVINCIA = Convert.ToInt32(ddlProvincia.SelectedValue);            
            else            
                oProveedor.ID_PROVINCIA = 0;

            if (!String.IsNullOrEmpty(hfdidLocalidad.Value))                            
                oProveedor.ID_LOCALIDAD = Convert.ToInt32(hfdidLocalidad.Value);                        
            else             
                oProveedor.ID_LOCALIDAD = 0;

            return oProveedor;
        }

        private void CargarProvincias()
        {
            var oProvincia = new BLL.ProvinciaBLL();

            try
            {
                ddlProvincia.DataSource = oProvincia.ObtenerProvincias();
                ddlProvincia.DataTextField = "NOMBRE";
                ddlProvincia.DataValueField = "ID_PROVINCIA";
                ddlProvincia.DataBind();
                ddlProvincia.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                ddlProvincia.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
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

        [WebMethod]
        public static ArrayList CargarLocalidades(string idProvincia)
        {
            var oLocalidad = new BLL.LocalidadBLL();
            ArrayList lstLocalidadesResultado = null;
            List<Localidad> lstLocalides;

            try
            {
                if (!String.IsNullOrEmpty(idProvincia))
                {
                    lstLocalides = oLocalidad.ObtenerLocalidades(Convert.ToInt64(idProvincia));
                    lstLocalidadesResultado = new ArrayList
                    {
                        new ItemOptionLocalidad() { Value = string.Empty, Text = string.Empty }
                    };

                    foreach (var item in lstLocalides)
                    {
                        lstLocalidadesResultado.Add(new ItemOptionLocalidad() { Value = item.ID_LOCALIDAD.ToString(), Text = item.NOMBRE });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            return lstLocalidadesResultado;
        }

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