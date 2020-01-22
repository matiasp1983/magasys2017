﻿namespace BLL.Common
{
    public class Enums
    {
        #region Enums

        public enum Session
        {
            Proveedor,
            ProductoDiario,
            ProductoRevista,
            ProductoColeccion,
            ProductoLibro,
            ProductoSuplemento,
            ProductoPelicula,
            IdIngresoProductos,
            IdVenta,
            DetalleIngresoProductos,
            Cliente,
            IdProductoDevolucion,
            AltaVentaAltaCliente,
            IdCliente,
            ListadoVenta,
            Venta,
            AltaReservaAltaCliente,
            Genero,
            IdReserva,
            IdClienteCobro,
            CobroVisualizarIdVenta,
            IdCobro,
            Imagen
        }        

        #endregion
    }

    public static class TipoUsuario
    {
        public const int Administrador = 1;
        public const int Empleado = 2;
        public const int Cliente = 3;
    }
}