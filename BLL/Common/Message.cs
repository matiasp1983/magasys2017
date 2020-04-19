namespace BLL.Common
{
    public class Message
    {
        #region Mensajes Sistema

        public const string MsjeSistemaError = "Error de Sistema: {0}";

        #endregion

        #region Mensajes Generales

        public const string MsjeSinResultados = "No se encontraron datos para la búsqueda seleccionada.";

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
        public const string MsjeClienteSuccessAlta = "El cliente se registró correctamente.";
        public const string MsjeClienteFailure = "El cliente no se pudo guardar.";
        public const string MsjeListadoClienteFiltrarTotalSinResultados = "No se encontraron Clientes para la búsqueda seleccionada.";
        public const string MsjeClienteSuccessModificacion = "El cliente se modificó correctamente.";
        public const string MsjeClienteSinModificaciones = "No se realizaron modificaciones en el Cliente.";
        public const string MsjeClienteObligatorio = "Debe completar el Cliente.";

        #endregion

        #region Mensajes Venta

        public const string MsjeVentaCampoCantidadObligatorio = "Debe completar la Cantidad.";
        public const string MsjeVentaStockInsuficiente = "No hay suficiente stock.";
        public const string MsjeVentaSuccessAlta = "La venta se registró correctamente.";
        public const string MsjeVentaFailure = "La venta no se pudo registrar.";
        public const string MsjeVentaCantidadInvalida = "La cantidad ingresada no es válida.";
        public const string MsjeListadoVentaFiltrarTotalSinResultados = "No se encontraron Ventas para la búsqueda seleccionada.";
        public const string MsjeVentaAnularFailure = "La venta no se pudo anular.";
        public const string MsjeVentaAnularOk = "La venta se pudo anular.";
        public const string MsjeVentaValidaCtaCorriente = "Debe seleccionar un cliente.";
        public const string MsjeVentaValidaClienteCtaCorriente = "Los datos del cliente no son válidos.";
        public const string MsjeListadoVentaACuentaSinResultados = "No se encontraron Ventas para cobrar.";

        #endregion

        #region Mensajes Devolucion

        public const string MsjeListadoDevolucionSinResultados = "No se encontraron Productos para la búsqueda seleccionada.";
        public const string MsjeDevolucionSuccessAlta = "La devolución se guardó correctamente.";
        public const string MsjeDevolucionFailure = "La devolución no se pudo guardar.";
        public const string MsjeListadoDevolucionListadoSinResultados = "No se encontraron Devoluciones de productos para la búsqueda seleccionada.";
        public const string MsjeDevolucionAnularOk = "La devolución se pudo anular.";
        public const string MsjeDevolucionAnularFailure = "La devolución no se pudo anular.";

        #endregion

        #region Mensajes Reserva

        public const string MsjeReservaSuccessAlta = "La reserva se registró correctamente.";
        public const string MsjeReservaSuccessModificacion = "La reserva se actualizó correctamente.";
        public const string MsjeReservaFailure = "La reserva no se pudo registrar.";
        public const string MsjeReservaDireccionRequerida = "La forma de entrega “Envío a Domicilio” requiere que el cliente complete los datos de la dirección.";
        public const string MsjeReservaSeleccionDeProducto = "Debe seleccionar un producto.";
        public const string MsjeReservaFechaInicioMayorQueFechaFin = "La Fecha de fin debe ser mayor que la Fecha de inicio.";
        public const string MsjeListadoReservaFiltroSinResultados = "No se encontraron reservas para la búsqueda seleccionada.";
        public const string MsjeReservaUnicaFechafin = "Para la reserva Única, el campo Fecha de fin es requerido.";
        public const string MsjeReservaNoSePuedeAnular = "La reserva está Finalizada, no se puede Anular.";
        public const string MsjeReservaAnulada = "La reserva ya se encuentra Anulada.";
        public const string MsjeReservaAnularReservaEntregada = "El producto fue entregado, no se puede anular.";
        public const string MsjeReservaAnularOk = "La reserva fue anulada.";
        public const string MsjeReservaAnularFailure = "La reserva no se pudo anular.";
        public const string MsjeReservaFinalizadaNoEditar = "La reserva Finalizada no se puede modificar.";
        public const string MsjeReservaAnuladaNoEditar = "La reserva Anulada no se puede modificar.";
        public const string MsjeReservaNoExiste = "La reserva ingresada no existe.";
        public const string MsjeReservaNoSePuedeFinalizar = "La reserva no se pudo finalizar, no cuenta con el estado requerido.";
        public const string MsjeReservaErrorFinalizar = "La reserva no se pudo finalizar.";
        public const string MsjeReservaNoCaducada = "La reserva no ha caducado.";
        public const string MsjeReservaProcesamientoFinalizado = "El procesamiento finalizó con éxito.";
        public const string MsjeReservaSinCaducar = "No hay reservas para procesar.";
        public const string MsjeReservaCargarCodigo = "Se debe completar el código de la reserva.";

        #endregion

        #region Mensajes Genero

        public const string MsjeGeneroSuccessAlta = "El Género se guardó correctamente.";
        public const string MsjeGeneroFailure = "El Género no se pudo guardar.";
        public const string MsjeNombreGeneroExist = "El Nombre de Género ingresado ya existe.";
        public const string MsjeListadoGenerosFiltrarTotalSinResultados = "No se encontraron Géneros para la búsqueda seleccionada.";
        public const string MsjeGeneroSuccessModificacion = "El Género se modificó correctamente.";

        #endregion

        #region Mensajes Deudas

        public const string MsjeVisualizarDeudasSinResultados = "No se encontraron deudas para la búsqueda seleccionada.";

        #endregion

        #region Mensajes Cobro

        public const string MsjeCobroSuccessAlta = "El cobro se registró correctamente.";
        public const string MsjeCobroFailure = "El cobro no se pudo registrar.";
        public const string MsjeListadoCobroFiltrarTotalSinResultados = "No se encontraron Cobros para la búsqueda seleccionada.";
        public const string MsjeCobroAnularOk = "El cobro se pudo anular.";
        public const string MsjeCobroAnularFailure = "El cobro no se pudo anular.";

        #endregion

        #region Mensajes Entrega Producto

        public const string MsjeEntregaProductoFiltroSinResultados = "No se encontraron reservas confirmadas para la búsqueda seleccionada.";

        #endregion

        #region Mensajes Login

        public const string MsjeLoginUsuarioYOContraseniaVacios = "Usuario y/o Contraseña no pueden estar vacíos.";
        public const string MsjeLoginUsuarioIncorrecto = "Usuario y/o Contraseña incorrecto.";

        #endregion

        #region Mensajes Usuario

        public const string MsjeUsuarioSuccessAlta = "El usuario se guardó correctamente.";
        public const string MsjeUsuarioSuccessModificacion = "El usuario se modificó correctamente.";
        public const string MsjeUsuarioFailure = "El usuario no se pudo guardar.";
        public const string MsjeListadoUsuarioFiltrarTotalSinResultados = "No se encontraron Usuarios para la búsqueda seleccionada.";

        #endregion

        #region Mensajes ForgotPassword

        public const string MsjeForgotPasswordEmailVacio = "Email no pueden estar vacío.";
        public const string MsjeForgotPasswordIncorrecto = "Formato de email incorrecto.";
        public const string MsjeForgotPasswordSuccess = "Te enviaremos un email con instrucciones para restrablecer tu contraseña. Si no lo ves en tu carpeta de Recibidos, revisá el correo no deseado.";
        public const string MsjeForgotPasswordFailure = "El Email no se pudo enviar.";
        public const string MsjeForgotPasswordFailureNoCliente = "El dato ingresado no corresponde a un cliente de Magasys.";

        #endregion

        #region Mensajes RestorePassword

        public const string MsjeRestorePasswordContraseniaNuevaConfirmarContraseniaVacios = "Contraseña Nueva y/o su Confirmación no pueden estar vacíos.";
        public const string MsjeRestorePasswordLength = "Usa 8 caracteres o más para tu contraseña.";
        public const string MsjeRestorePasswordCoincidencia = "Las contraseñas no coinciden. Vuelva a intentarlo.";
        public const string MsjeMsjeRestorePasswordSuccessAlta = "La contraseña se guardó correctamente.";
        public const string MsjeMsjeRestorePasswordFailure = "La contraseña no se pudo guardar.";

        #endregion

        #region Mensajes InformacionPersonal

        public const string MsjeInformacionPersonalSuccessModificacion = "La información personal se modificó correctamente.";
        public const string MsjeInformacionPersonal = "La información personal no se pudo guardar.";

        #endregion

        #region Mensajes Entrega

        public const string MsjeEntregalSuccess = "La Entrega se Registró correctamente.";
        public const string MsjeEntregaFailure = "La Entrega  no se pudo guardar.";

        #endregion


    }
}
