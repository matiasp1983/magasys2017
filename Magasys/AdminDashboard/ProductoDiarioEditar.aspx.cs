using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
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

                loResutado = new BLL.DiarioBLL().ModificarDiario(oProducto, lstDiarioDiasSemanas);

                if (loResutado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Diario", "ProductoListado.aspx"));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.ProductoDiario.ToString());
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
                    var oProductoDiario = (BLL.ProductoDiario)Session[Enums.Session.ProductoDiario.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoDiario.NOMBRE;
                    txtDescripcion.Text = oProductoDiario.DESCRIPCION;
                    CargarProveedor(oProductoDiario.COD_PROVEEDOR);
                    CargarGenero(oProductoDiario.COD_GENERO);
                    if (oProductoDiario.PRECIO_LUNES.HasValue)
                        txtPrecioLunesDiario.Text = oProductoDiario.PRECIO_LUNES.Value.ToString();
                    if (oProductoDiario.PRECIO_MARTES.HasValue)
                        txtPrecioMartesDiario.Text = oProductoDiario.PRECIO_MARTES.Value.ToString();
                    if (oProductoDiario.PRECIO_MIERCOLES.HasValue)
                        txtPrecioMiercolesDiario.Text = oProductoDiario.PRECIO_MIERCOLES.Value.ToString();
                    if (oProductoDiario.PRECIO_JUEVES.HasValue)
                        txtPrecioJuevesDiario.Text = oProductoDiario.PRECIO_JUEVES.Value.ToString();
                    if (oProductoDiario.PRECIO_VIERNES.HasValue)
                        txtPrecioViernesDiario.Text = oProductoDiario.PRECIO_VIERNES.Value.ToString();
                    if (oProductoDiario.PRECIO_SABADO.HasValue)
                        txtPrecioSabadoDiario.Text = oProductoDiario.PRECIO_SABADO.Value.ToString();
                    if (oProductoDiario.PRECIO_DOMINGO.HasValue)
                        txtPrecioDomingoDiario.Text = oProductoDiario.PRECIO_DOMINGO.Value.ToString();
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
            var oProducto = new BLL.DAL.Producto();

            if (Session[Enums.Session.ProductoDiario.ToString()] != null)
            {
                oProducto.ID_PRODUCTO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_PRODUCTO;
                oProducto.FECHA_ALTA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).FECHA_ALTA;
                oProducto.COD_ESTADO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).COD_ESTADO;
                oProducto.COD_TIPO_PRODUCTO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).COD_TIPO_PRODUCTO;
            }

            oProducto.NOMBRE = txtNombre.Text;
            oProducto.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
            oProducto.COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue);

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
                var oDiarioDiaSemana = new BLL.DAL.DiarioDiaSemana();

                var oDiaSemana = new BLL.DiaSemanaBLL().ObtenerDiaSemana(ObtenerParteDeNombreIDTexbox(loTxtPrecioDiario.ID.ToString()));

                if (Session[Enums.Session.ProductoDiario.ToString()] != null)
                {
                    switch (oDiaSemana.NOMBRE)
                    {
                        case "Lunes":
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_LUNES;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;                            
                            break;                            
                        case "Martes":                            
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_MARTES;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                        case "Miércoles":
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_MIERCOLES;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                        case "Jueves":
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_JUEVES;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                        case "Viernes":
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_VIERNES;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                        case "Sábado":
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_SABADO;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                        default:
                            oDiarioDiaSemana.ID_DIARIO_DIA_SEMANA = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO_DIA_SEMANA_DOMINGO;
                            oDiarioDiaSemana.ID_DIA_SEMANA = oDiaSemana.ID_DIA_SEMANA;
                            break;
                    }
                    
                    oDiarioDiaSemana.COD_DIARIO = ((BLL.ProductoDiario)base.Session[Enums.Session.ProductoDiario.ToString()]).ID_DIARIO;
                }                

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
            var oProveedor = new BLL.ProveedorBLL();

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
            var oGenero = new BLL.GeneroBLL();

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