using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoDiarioEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoDiarioDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var lstDiarioDiasSemanas = CargarDiarioDesdeControles();

                loResutado = new DiarioBLL().ModificarDiario(oProducto, lstDiarioDiasSemanas);

                if (loResutado)                
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto", "ProductoListado.aspx"));                
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoDiario.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoDiarioDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                {
                    var oProductoDiario = (ProductoDiario)Session[Enums.Session.ProductoDiario.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoDiario.NOMBRE;
                    CargarProveedor(oProductoDiario.COD_PROVEEDOR);
                    CargarGenero(oProductoDiario.COD_GENERO);
                    if (oProductoDiario.PRECIO_LUNES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_LUNES.Value.ToString();
                    if (oProductoDiario.PRECIO_MARTES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_MARTES.Value.ToString();
                    if (oProductoDiario.PRECIO_MIERCOLES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_MIERCOLES.Value.ToString();
                    if (oProductoDiario.PRECIO_JUEVES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_JUEVES.Value.ToString();
                    if (oProductoDiario.PRECIO_VIERNES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_JUEVES.Value.ToString();
                    if (oProductoDiario.PRECIO_SABADO.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_SABADO.Value.ToString();
                    if (oProductoDiario.PRECIO_DOMINGO.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_SABADO.Value.ToString();
                }
                else
                    Response.Redirect("ProductoListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Producto CargarProductoDesdeControles()
        {
            var oProducto = new BLL.DAL.Producto
            {
                ID_PRODUCTO = Convert.ToInt32(txtCodigo.Text),
                NOMBRE = txtNombre.Text,
                COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue),
                COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue)
            };

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;

            return oProducto;
        }

        private List<BLL.DAL.DiarioDiaSemana> CargarDiarioDesdeControles()
        {
            List<BLL.DAL.DiarioDiaSemana> lstDiarioDiasSemanas = new List<BLL.DAL.DiarioDiaSemana>();

            foreach (var loTxtPrecioDiario in divDiario.Controls.OfType<TextBox>().ToList())
            {
                var oDiarioDiaSemana = new BLL.DAL.DiarioDiaSemana
                {
                    ID_DIA_SEMANA = new DiaSemanaBLL().ObtenerDiaSemana(ObtenerParteDeNombreIDTexbox(loTxtPrecioDiario.ID.ToString())).ID_DIA_SEMANA
                };

                if (!String.IsNullOrEmpty(loTxtPrecioDiario.Text))
                    oDiarioDiaSemana.PRECIO = Convert.ToDouble(loTxtPrecioDiario.Text);
                else
                    oDiarioDiaSemana.PRECIO = null;

                lstDiarioDiasSemanas.Add(oDiarioDiaSemana);
            }

            return lstDiarioDiasSemanas;
        }

        private void CargarProveedor(long idProveedor)
        {
            var oProveedor = new ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idProveedor == 0)
                    ddlProveedor.SelectedIndex = 0;
                else
                {
                    ddlProveedor.SelectedValue = idProveedor.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarGenero(long idGenero)
        {
            var oGenero = new GeneroBLL();

            try
            {
                ddlGenero.DataSource = oGenero.ObtenerGeneros();
                ddlGenero.DataTextField = "NOMBRE";
                ddlGenero.DataValueField = "ID_GENERO";
                ddlGenero.DataBind();
                ddlGenero.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idGenero == 0)
                    ddlGenero.SelectedIndex = 0;
                else
                {
                    ddlGenero.SelectedValue = idGenero.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private string ObtenerParteDeNombreIDTexbox(string pCadena)
        {
            String loStringInicial = "txtPrecio";
            String loStringFinal = "Diario";

            String loNuevoString = pCadena.Substring(0, pCadena.LastIndexOf(loStringFinal));
            int loIniciaString = loNuevoString.LastIndexOf(loStringInicial) + loStringInicial.Length;
            int loCortar = loNuevoString.Length - loIniciaString;
            loNuevoString = loNuevoString.Substring(loIniciaString, loCortar);

            return loNuevoString;
        }

        #endregion
    }
}