using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoSuplementoEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoSuplementoDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oSuplemento = CargarSuplementoDesdeControles();

                if (oProducto != null && oSuplemento != null)
                {
                    loResutado = new BLL.SuplementoBLL().ModificarSuplemento(oProducto, oSuplemento);

                    if (loResutado)
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Suplemento", "ProductoListado.aspx"));
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.ProductoSuplemento.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoSuplemento.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoSuplementoDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoSuplemento.ToString()] != null)
                {
                    var oProductoSuplemento = (BLL.ProductoSuplemento)Session[Enums.Session.ProductoSuplemento.ToString()];

                    if (oProductoSuplemento.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoSuplemento.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoSuplemento.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoSuplemento.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoSuplemento.NOMBRE;
                    txtDescripcion.Text = oProductoSuplemento.DESCRIPCION;
                    CargarProveedor(oProductoSuplemento.COD_PROVEEDOR);
                    CargarGenero(oProductoSuplemento.COD_GENERO);
                    CargarDiaDeSemana(oProductoSuplemento.ID_DIA_SEMANA);
                    CargarDiario(oProductoSuplemento.COD_DIARIO);
                    txtPrecioSuplemento.Text = oProductoSuplemento.PRECIO.ToString();
                    txtCantidadDeEntregaSuplemento.Text = oProductoSuplemento.CANTIDAD_DE_ENTREGAS.ToString();
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
            if (Session[Enums.Session.ProductoSuplemento.ToString()] != null)
            {
                oProducto.ID_PRODUCTO = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).ID_PRODUCTO;
                oProducto.FECHA_ALTA = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).FECHA_ALTA;
                oProducto.COD_ESTADO = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).COD_ESTADO;
                oProducto.COD_TIPO_PRODUCTO = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).COD_TIPO_PRODUCTO;
                oProducto.NOMBRE = txtNombre.Text;
                oProducto.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                oProducto.COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue);
            }

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;

            return oProducto;
        }

        private BLL.DAL.Suplemento CargarSuplementoDesdeControles()
        {
            var oSuplemento = new BLL.DAL.Suplemento();
            if (Session[Enums.Session.ProductoSuplemento.ToString()] != null)
            {
                //var oDiario = new BLL.DiarioBLL().ObtenerDiario(((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).ID_PRODUCTO);
                oSuplemento.ID_SUPLEMENTO = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).ID_SUPLEMENTO;
                oSuplemento.COD_PRODUCTO = ((BLL.ProductoSuplemento)base.Session[Enums.Session.ProductoSuplemento.ToString()]).ID_PRODUCTO;
            }
            var oDiarioSuplemento = new BLL.DiarioBLL().ObtenerDiario(Convert.ToInt32(ddlDiarioSuplemento.SelectedValue));
                oSuplemento.COD_DIARIO = oDiarioSuplemento.ID_DIARIO_DIA_SEMAMA;
                oSuplemento.PRECIO = Convert.ToDouble(txtPrecioSuplemento.Text);
                oSuplemento.CANTIDAD_ENTREGAS = Convert.ToInt32(txtCantidadDeEntregaSuplemento.Text);
                            
                if (!String.IsNullOrEmpty(ddlDiaDeEntregaSuplemento.SelectedValue))
                    oSuplemento.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaSuplemento.SelectedValue);
                else
                    oSuplemento.ID_DIA_SEMANA = null;
            
            return oSuplemento;
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

        private void CargarDiaDeSemana(long? idDiaSemana)
        {
            var oDiaSemana = new BLL.DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaSuplemento.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaSuplemento.DataTextField = "NOMBRE";
                ddlDiaDeEntregaSuplemento.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaSuplemento.DataBind();
                ddlDiaDeEntregaSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idDiaSemana.HasValue)
                    ddlDiaDeEntregaSuplemento.SelectedValue = idDiaSemana.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarDiario(long idDiario)
        {
            var oDiario = new BLL.DiarioBLL();

            try
            {
                var lstDiarios = oDiario.ObtenerDiarios();

                ddlDiarioSuplemento.DataSource = lstDiarios;
                ddlDiarioSuplemento.DataTextField = "NOMBRE";
                ddlDiarioSuplemento.DataValueField = "ID_DIARIO";
                ddlDiarioSuplemento.DataBind();
                ddlDiarioSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlDiarioSuplemento.SelectedValue = idDiario.ToString();

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