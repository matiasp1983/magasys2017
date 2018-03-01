using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoRevistaEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoRevistaDesdeSession();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oRevista = CargarRevistaDesdeControles();

                loResutado = new BLL.RevistaBLL().ModificarRevista(oProducto, oRevista);

                if (loResutado)
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Revista", "ProductoListado.aspx"));
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            Session.Remove(Enums.Session.ProductoRevista.ToString());
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoRevista.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoRevistaDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoRevista.ToString()] != null)
                {
                    var oProductoDiario = (BLL.ProductoRevista)Session[Enums.Session.ProductoRevista.ToString()];

                    if (oProductoDiario.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoDiario.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoDiario.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoDiario.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoDiario.NOMBRE;
                    txtDescripcion.Text = oProductoDiario.DESCRIPCION;
                    CargarProveedor(oProductoDiario.COD_PROVEEDOR);
                    CargarGenero(oProductoDiario.COD_GENERO);
                    CargarDiasDeSemana(oProductoDiario.ID_DIA_SEMANA);
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

            if (Session[Enums.Session.ProductoRevista.ToString()] != null)
            {
                oProducto.ID_PRODUCTO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).ID_PRODUCTO;
                oProducto.FECHA_ALTA = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).FECHA_ALTA;
                oProducto.COD_ESTADO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).COD_ESTADO;
                oProducto.COD_TIPO_PRODUCTO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).COD_TIPO_PRODUCTO;
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

        private BLL.DAL.Revista CargarRevistaDesdeControles()
        {
            var oRevista = new BLL.DAL.Revista
            {
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadRevista.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioRevista.Text)
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaRevista.SelectedValue))
                oRevista.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaRevista.SelectedValue);
            else
                oRevista.ID_DIA_SEMANA = null;

            return oRevista;
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

        private void CargarDiasDeSemana(long? idDiaSemana)
        {
            var oDiaSemana = new BLL.DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaRevista.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaRevista.DataTextField = "NOMBRE";
                ddlDiaDeEntregaRevista.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaRevista.DataBind();
                ddlDiaDeEntregaRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idDiaSemana.HasValue)
                    ddlPeriodicidadRevista.SelectedValue = idDiaSemana.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarPeriodicidades(long idPeriodicidad)
        {
            var oPeriodicidad = new BLL.PeriodicidadBLL();

            try
            {
                var lstPeriodicidades = oPeriodicidad.ObtenerPeriodicidades();

                ddlPeriodicidadRevista.DataSource = lstPeriodicidades;
                ddlPeriodicidadRevista.DataTextField = "NOMBRE";
                ddlPeriodicidadRevista.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadRevista.DataBind();
                ddlPeriodicidadRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idPeriodicidad == 0)
                    ddlPeriodicidadRevista.SelectedIndex = 0;
                else
                {
                    ddlPeriodicidadRevista.SelectedValue = idPeriodicidad.ToString();
                }
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