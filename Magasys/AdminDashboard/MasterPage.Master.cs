using System;
using System.Globalization;
using BLL.Common;
using NLog;

namespace PL.AdminDashboard
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                BLL.DAL.Usuario loUsuario = null;

                if (!IsPostBack)
                {
                    if (Session[AdminDashboardSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
                    {
                        TextInfo loText = new CultureInfo("es-AR", false).TextInfo;
                        loUsuario = (BLL.DAL.Usuario)Session[AdminDashboardSessionBLL.DefaultSessionsId.Usuario.ToString()];
                        lblUsuarioLogout.Text = loText.ToUpper(loUsuario.APELLIDO + " " + loUsuario.NOMBRE).ToString();

                        if (loUsuario.AVATAR != null)
                        {
                            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loUsuario.AVATAR);
                            imgPerfil.ImageUrl = loImagenDataURL64;
                        }

                        Response.ClearHeaders();
                        Response.AddHeader("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
                        Response.AddHeader("Pragma", "no-cache");
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", true);
                    }
                }

                MenuPrincipal(loUsuario);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session[AdminDashboardSessionBLL.DefaultSessionsId.Usuario.ToString()] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
        }

        #endregion

        #region Métodos Privados

        private void MenuPrincipal(BLL.DAL.Usuario pUsuario)
        {
            String loActivePage = Request.Url.Segments[2];
            if (loActivePage.Equals("Index.aspx"))
            {
                liPrincipal.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ReporteVentaAnualTipoProducto.aspx"))
            {
                liReportes.Attributes["class"] = "active";
                liReporteVentaAnualTipoProducto.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Proveedor.aspx"))
            {
                liProveedores.Attributes["class"] = "active";
                liAltaProveedor.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProveedorListado.aspx"))
            {
                liProveedores.Attributes["class"] = "active";
                liProveedorListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Producto.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liAltaProducto.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProductoListado.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liProductoListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ConsultarStock.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liConsultarStock.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Genero.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liAltaGenero.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("GeneroListado.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liGeneroListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Cliente.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liAltaCliente.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ClienteListado.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liClienteListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("VisualizarDeudas.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liVisualizarDeudas.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Cobro.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liRegistrarCobro.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("CobroListado.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liCobroListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProductoIngreso.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoIngreso.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProductoIngresoListado.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoIngresoListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Venta.aspx"))
            {
                liVenta.Attributes["class"] = "active";
                liRegistrarVenta.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("VentaListado.aspx"))
            {
                liVenta.Attributes["class"] = "active";
                liVentaListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProductoDevolucion.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoDevolucion.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ProductoDevolucionListado.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoDevolucionListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Reserva.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liRegistrarReserva.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ReservaFinalizar.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liReservaFinalizar.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ReservaListado.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liReservaListado.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("RegistrarEntregaProducto.aspx"))
            {
                liEntregaReparto.Attributes["class"] = "active";
                liRegistrarEntregaProducto.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("Reparto.aspx"))
            {
                liEntregaReparto.Attributes["class"] = "active";
                liListadoReparto.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("GestionReparto.aspx"))
            {
                liEntregaReparto.Attributes["class"] = "active";
                liGestionReparto.Attributes["class"] = "active";
            }
            if (loActivePage.Equals("ConsultarRepartos.aspx"))
            {
                liEntregaReparto.Attributes["class"] = "active";
                liConsultarRepartos.Attributes["class"] = "active";
            }

            if (pUsuario != null)
            {
                if (pUsuario.ID_ROL.Equals(RolUsuario.Administrador))
                {
                    liUsuarios.Visible = true;
                    liNegocio.Visible = true;
                    liEmpleado.Visible = true;

                    if (loActivePage.Equals("Usuario.aspx"))
                    {
                        liUsuarios.Attributes["class"] = "active";
                        liAltaUsuario.Attributes["class"] = "active";
                    }

                    if (loActivePage.Equals("UsuarioListado.aspx"))
                    {
                        liUsuarios.Attributes["class"] = "active";
                        liUsuarioListado.Attributes["class"] = "active";
                    }

                    if (loActivePage.Equals("Empleado.aspx"))
                    {
                        liEmpleado.Attributes["class"] = "active";
                        liAltaEmpleado.Attributes["class"] = "active";
                    }

                    if (loActivePage.Equals("EmpleadoListado.aspx"))
                    {
                        liEmpleado.Attributes["class"] = "active";
                        liEmpleadoListado.Attributes["class"] = "active";
                    }

                    if (loActivePage.Equals("Negocio.aspx"))
                    {
                        liNegocio.Attributes["class"] = "active";
                        liInfoNegocio.Attributes["class"] = "active";
                    }
                }
            }
        }

        #endregion
    }
}