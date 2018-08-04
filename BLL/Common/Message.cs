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
        public const string MsjeProductoIngresoFailure = "El ingreso de productos no se pudo guardar";
        public const string MsjeListadoProductoIngresoFiltrarTotalSinResultados = "No se encontraron Ingresos de productos para la búsqueda seleccionada.";
        public const string MsjeListadoProductoIngresoFechaDesdeMayorQueFechaHasta = "La Fecha Hasta debe ser mayor que la Fecha Desde.";
        public const string MsjeListadoProductoIngresoCargarProducto = "Debe cargar al menos un producto.";

        #endregion
    }
}
