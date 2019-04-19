namespace BLL.Common
{
    public class Message
    {
        #region Mensajes Sistema

        public const string MsjeSistemaError = "Error de Sistema: {0}";

        #endregion

        #region Mensajes Proveedor

        public const string MsjeProveedorSuccessAlta = "El proveedor se guardó correctamente.";
        public const string MsjeProveedorSuccessModificacion = "El proveedor se modificó correctamente.";
        public const string MsjeProveedorFailure = "El proveedor no se pudo guardar.";
        public const string MsjeCuitProveedorVacio = "El CUIT no puede estar vacío.";
        public const string MsjeCuitProveedorFailure = "El CUIT ingresado no es válido.";
        public const string MsjeCuitProveedorExist = "El CUIT ingresado ya existe.";
        public const string MsjeRazonSocialProveedorExist = "La razón social ingresada ya existe.";
        public const string MsjeListadoProveedorFiltrarTotalSinResultados = "No se encontraron Proveedores para la búsqueda seleccionada.";
        public const string MsjeListadoProveedorFechaDesdeMayorQueFechaHasta = "La Fecha Hasta debe ser mayor que la Fecha Desde.";

        #endregion

        #region Mensajes Producto

        public const string MsjeProductoSuccessAlta = "El producto se guardó correctamente.";
        public const string MsjeProductoSuccessModificacion = "El producto se modificó correctamente.";
        public const string MsjeProductoFailure = "El producto no se pudo guardar.";
        public const string MsjeListadoProductoFiltrarTotalSinResultados = "No se encontraron Productos para la búsqueda seleccionada.";

        #endregion

        #region Mensajes Producto Ingreso

        public const string MsjeProductoIngresoSuccessAlta = "El ingreso de productos se guardó correctamente.";
        public const string MsjeProductoIngresoSuccessModificacion = "El ingreso de productos se modificó correctamente.";
        public const string MsjeProductoIngresoCampObligatorio = "Debe completar los campos obligatorios.";
        public const string MsjeProductoIngresoSinCambios = "No se realizaron modificaciones.";
        public const string MsjeProductoIngresoFailure = "El ingreso de productos no se pudo guardar";
        public const string MsjeListadoProductoIngresoFiltrarTotalSinResultados = "No se encontraron Ingresos de productos para la búsqueda seleccionada.";
        public const string MsjeListadoFechaDesdeMayorQueFechaHasta = "La Fecha Hasta debe ser mayor que la Fecha Desde.";
        public const string MsjeListadoProductoIngresoCargarProducto = "Debe cargar al menos un producto.";

        #endregion

        #region Mensajes Cliente

        public const string MsjeClienteBuscarClienteSinResultados = "No se encontró el Cliente para la búsqueda seleccionada.";
        public const string MsjeClienteBuscarClienteSinNroDocumento = "Debe completar el Nro. de Documento.";
        public const string MsjeClienterExiste = "El Cliente ya existe.";
        public const string MsjeClienteSuccessAlta = "El cliente se guardó correctamente.";
        public const string MsjeClienteFailure = "El cliente no se pudo guardar.";

        #endregion

        #region Venta

        public const string MsjeVentaCampoCantidadObligatorio = "Debe completar la Cantidad.";
        public const string MsjeVentaStockInsuficiente = "No hay suficiente stock.";
        public const string MsjeVentaSuccessAlta = "La venta se guardó correctamente.";
        public const string MsjeVentaFailure = "La venta no se pudo guardar.";
        public const string MsjeVentaCantidadInvalida = "La cantidad ingresada no es válida";
        public const string MsjeVentaAviso = "Para una venta de Contado debe indicar Pagado Sí.";
        public const string MsjeListadoVentaFiltrarTotalSinResultados = "No se encontraron Ventas para la búsqueda seleccionada.";
        public const string MsjeVentaAnularFailure = "La venta no se pudo anular.";
        public const string MsjeVentaAnularOk = "La venta se pudo anular.";
        public const string MsjeVentaValidaCtaCorriente = "Debe seleccionar un cliente";
        public const string MsjeVentaValidaClienteCtaCorriente = "Los datos del cliente no son válidos";

        #endregion

        #region MensajesDevolucion

        public const string MsjeListadoDevolucionSinResultados = "No se encontraron Productos para la búsqueda seleccionada.";
        public const string MsjeDevolucionSuccessAlta = "La devolución se guardó correctamente.";
        public const string MsjeDevolucionFailure = "La devolución no se pudo guardar.";
        public const string MsjeListadoDevolucionListadoSinResultados = "No se encontraron Devoluciones de productos para la búsqueda seleccionada.";
        public const string MsjeDevolucionAnularOk = "La devolución se pudo anular.";
        public const string MsjeDevolucionAnularFailure = "La devolución no se pudo anular.";
        #endregion
    }
}
