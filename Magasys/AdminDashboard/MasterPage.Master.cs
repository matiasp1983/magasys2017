using System;
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
                MenuPrincipal();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        #region Métodos Privados

        private void MenuPrincipal()
        {
            String loActivePage = Request.RawUrl;
            if (loActivePage.Contains("Index.aspx"))
            {
                liPrincipal.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Proveedor.aspx"))
            {
                liProveedores.Attributes["class"] = "active";
                liAltaProveedor.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProveedorListado.aspx"))
            {
                liProveedores.Attributes["class"] = "active";
                liProveedorListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Producto.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liAltaProducto.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoListado.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liProductoListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Genero.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liAltaGenero.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("GeneroListado.aspx"))
            {
                liProductos.Attributes["class"] = "active";
                liGeneroListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Cliente.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liAltaCliente.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ClienteListado.aspx"))
            {
                liClientes.Attributes["class"] = "active";
                liClienteListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoIngreso.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoIngreso.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoIngresoListado.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoIngresoListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Venta.aspx"))
            {
                liVenta.Attributes["class"] = "active";
                liRegistrarVenta.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("VentaListado.aspx"))
            {
                liVenta.Attributes["class"] = "active";
                liVentaListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoDevolucion.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoDevolucion.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ProductoDevolucionListado.aspx"))
            {
                liDeposito.Attributes["class"] = "active";
                liProductoDevolucionListado.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("Reserva.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liRegistrarReserva.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ReservaFinalizar.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liReservaFinalizar.Attributes["class"] = "active";
            }
            if (loActivePage.Contains("ReservaListado.aspx"))
            {
                liReserva.Attributes["class"] = "active";
                liReservaListado.Attributes["class"] = "active";
            }
        }

        #endregion
    }
}