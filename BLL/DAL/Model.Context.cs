﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace BLL.DAL
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Entity.Core.Objects;
using System.Linq;


public partial class MAGASYSEntities : DbContext
{
    public MAGASYSEntities()
        : base("name=MAGASYSEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Anio> Anio { get; set; }

    public virtual DbSet<Cliente> Cliente { get; set; }

    public virtual DbSet<Cobro> Cobro { get; set; }

    public virtual DbSet<Coleccion> Coleccion { get; set; }

    public virtual DbSet<DetalleCobro> DetalleCobro { get; set; }

    public virtual DbSet<DetalleProductoDevolucion> DetalleProductoDevolucion { get; set; }

    public virtual DbSet<DetalleProductoIngreso> DetalleProductoIngreso { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<DiarioDiaSemana> DiarioDiaSemana { get; set; }

    public virtual DbSet<DiaSemana> DiaSemana { get; set; }

    public virtual DbSet<Empleado> Empleado { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<FormaPago> FormaPago { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Imagen> Imagen { get; set; }

    public virtual DbSet<Libro> Libro { get; set; }

    public virtual DbSet<Localidad> Localidad { get; set; }

    public virtual DbSet<Mensaje> Mensaje { get; set; }

    public virtual DbSet<Mes> Mes { get; set; }

    public virtual DbSet<Negocio> Negocio { get; set; }

    public virtual DbSet<Pelicula> Pelicula { get; set; }

    public virtual DbSet<Periodicidad> Periodicidad { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ProductoDevolucion> ProductoDevolucion { get; set; }

    public virtual DbSet<ProductoEdicion> ProductoEdicion { get; set; }

    public virtual DbSet<ProductoIngreso> ProductoIngreso { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Reparto> Reparto { get; set; }

    public virtual DbSet<Reserva> Reserva { get; set; }

    public virtual DbSet<ReservaEdicion> ReservaEdicion { get; set; }

    public virtual DbSet<Revista> Revista { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Suplemento> Suplemento { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }

    public virtual DbSet<TipoProducto> TipoProducto { get; set; }

    public virtual DbSet<TipoReserva> TipoReserva { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }


    [DbFunction("MAGASYSEntities", "fn_Split")]
    public virtual IQueryable<fn_Split_Result> fn_Split(string text, string delimiter)
    {

        var textParameter = text != null ?
            new ObjectParameter("text", text) :
            new ObjectParameter("text", typeof(string));


        var delimiterParameter = delimiter != null ?
            new ObjectParameter("delimiter", delimiter) :
            new ObjectParameter("delimiter", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<fn_Split_Result>("[MAGASYSEntities].[fn_Split](@text, @delimiter)", textParameter, delimiterParameter);
    }


    public virtual ObjectResult<ProductoMasVendido_Result> ProductoMasVendido(string anio, string codTipoProducto)
    {

        var anioParameter = anio != null ?
            new ObjectParameter("anio", anio) :
            new ObjectParameter("anio", typeof(string));


        var codTipoProductoParameter = codTipoProducto != null ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductoMasVendido_Result>("ProductoMasVendido", anioParameter, codTipoProductoParameter);
    }


    public virtual ObjectResult<ProductosDevueltosXAnio_Result> ProductosDevueltosXAnio(string anio, string codTipoProducto)
    {

        var anioParameter = anio != null ?
            new ObjectParameter("anio", anio) :
            new ObjectParameter("anio", typeof(string));


        var codTipoProductoParameter = codTipoProducto != null ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ProductosDevueltosXAnio_Result>("ProductosDevueltosXAnio", anioParameter, codTipoProductoParameter);
    }


    public virtual ObjectResult<VentaAnualXTipoProducto_Result> VentaAnualXTipoProducto(string anio, string codTipoProducto)
    {

        var anioParameter = anio != null ?
            new ObjectParameter("anio", anio) :
            new ObjectParameter("anio", typeof(string));


        var codTipoProductoParameter = codTipoProducto != null ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentaAnualXTipoProducto_Result>("VentaAnualXTipoProducto", anioParameter, codTipoProductoParameter);
    }


    public virtual int EliminarNotificacionesPorCiente(Nullable<int> idCliente)
    {

        var idClienteParameter = idCliente.HasValue ?
            new ObjectParameter("IdCliente", idCliente) :
            new ObjectParameter("IdCliente", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EliminarNotificacionesPorCiente", idClienteParameter);
    }


    public virtual ObjectResult<ObtenerVentasPorTipoProductoPorAnio_Result> ObtenerVentasPorTipoProductoPorAnio(Nullable<int> codTipoProducto, string producto, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
    {

        var codTipoProductoParameter = codTipoProducto.HasValue ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(int));


        var productoParameter = producto != null ?
            new ObjectParameter("Producto", producto) :
            new ObjectParameter("Producto", typeof(string));


        var fechaDesdeParameter = fechaDesde.HasValue ?
            new ObjectParameter("FechaDesde", fechaDesde) :
            new ObjectParameter("FechaDesde", typeof(System.DateTime));


        var fechaHastaParameter = fechaHasta.HasValue ?
            new ObjectParameter("FechaHasta", fechaHasta) :
            new ObjectParameter("FechaHasta", typeof(System.DateTime));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerVentasPorTipoProductoPorAnio_Result>("ObtenerVentasPorTipoProductoPorAnio", codTipoProductoParameter, productoParameter, fechaDesdeParameter, fechaHastaParameter);
    }


    public virtual ObjectResult<ObtenerVentasPorTipoProductoPorProductos_Result> ObtenerVentasPorTipoProductoPorProductos(Nullable<int> codTipoProducto, string producto, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
    {

        var codTipoProductoParameter = codTipoProducto.HasValue ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(int));


        var productoParameter = producto != null ?
            new ObjectParameter("Producto", producto) :
            new ObjectParameter("Producto", typeof(string));


        var fechaDesdeParameter = fechaDesde.HasValue ?
            new ObjectParameter("FechaDesde", fechaDesde) :
            new ObjectParameter("FechaDesde", typeof(System.DateTime));


        var fechaHastaParameter = fechaHasta.HasValue ?
            new ObjectParameter("FechaHasta", fechaHasta) :
            new ObjectParameter("FechaHasta", typeof(System.DateTime));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerVentasPorTipoProductoPorProductos_Result>("ObtenerVentasPorTipoProductoPorProductos", codTipoProductoParameter, productoParameter, fechaDesdeParameter, fechaHastaParameter);
    }


    public virtual ObjectResult<ObtenerVentasPorTipoProductoPorProductosPieChart_Result> ObtenerVentasPorTipoProductoPorProductosPieChart(Nullable<int> codTipoProducto, string producto, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
    {

        var codTipoProductoParameter = codTipoProducto.HasValue ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(int));


        var productoParameter = producto != null ?
            new ObjectParameter("Producto", producto) :
            new ObjectParameter("Producto", typeof(string));


        var fechaDesdeParameter = fechaDesde.HasValue ?
            new ObjectParameter("FechaDesde", fechaDesde) :
            new ObjectParameter("FechaDesde", typeof(System.DateTime));


        var fechaHastaParameter = fechaHasta.HasValue ?
            new ObjectParameter("FechaHasta", fechaHasta) :
            new ObjectParameter("FechaHasta", typeof(System.DateTime));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerVentasPorTipoProductoPorProductosPieChart_Result>("ObtenerVentasPorTipoProductoPorProductosPieChart", codTipoProductoParameter, productoParameter, fechaDesdeParameter, fechaHastaParameter);
    }


    public virtual ObjectResult<ObtenerDevolucionesPorTipoProducto1_Result> ObtenerDevolucionesPorTipoProducto(string codEdicion)
    {

        var codEdicionParameter = codEdicion != null ?
            new ObjectParameter("CodEdicion", codEdicion) :
            new ObjectParameter("CodEdicion", typeof(string));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerDevolucionesPorTipoProducto1_Result>("ObtenerDevolucionesPorTipoProducto", codEdicionParameter);
    }


    public virtual ObjectResult<ObtenerIngresosPorTipoProducto1_Result> ObtenerIngresosPorTipoProducto(Nullable<int> codTipoProducto, string producto, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
    {

        var codTipoProductoParameter = codTipoProducto.HasValue ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(int));


        var productoParameter = producto != null ?
            new ObjectParameter("Producto", producto) :
            new ObjectParameter("Producto", typeof(string));


        var fechaDesdeParameter = fechaDesde.HasValue ?
            new ObjectParameter("FechaDesde", fechaDesde) :
            new ObjectParameter("FechaDesde", typeof(System.DateTime));


        var fechaHastaParameter = fechaHasta.HasValue ?
            new ObjectParameter("FechaHasta", fechaHasta) :
            new ObjectParameter("FechaHasta", typeof(System.DateTime));


            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerIngresosPorTipoProducto1_Result>("ObtenerIngresosPorTipoProducto", codTipoProductoParameter, productoParameter, fechaDesdeParameter, fechaHastaParameter);
    }


    public virtual ObjectResult<ObtenerDevolucionesPorTipoProducto1_Result> ObtenerDevolucionesPorTipoProducto1(string codEdicion)
    {

        var codEdicionParameter = codEdicion != null ?
            new ObjectParameter("CodEdicion", codEdicion) :
            new ObjectParameter("CodEdicion", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerDevolucionesPorTipoProducto1_Result>("ObtenerDevolucionesPorTipoProducto1", codEdicionParameter);
    }


    public virtual ObjectResult<ObtenerIngresosPorTipoProducto1_Result> ObtenerIngresosPorTipoProducto1(Nullable<int> codTipoProducto, string producto, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
    {

        var codTipoProductoParameter = codTipoProducto.HasValue ?
            new ObjectParameter("CodTipoProducto", codTipoProducto) :
            new ObjectParameter("CodTipoProducto", typeof(int));


        var productoParameter = producto != null ?
            new ObjectParameter("Producto", producto) :
            new ObjectParameter("Producto", typeof(string));


        var fechaDesdeParameter = fechaDesde.HasValue ?
            new ObjectParameter("FechaDesde", fechaDesde) :
            new ObjectParameter("FechaDesde", typeof(System.DateTime));


        var fechaHastaParameter = fechaHasta.HasValue ?
            new ObjectParameter("FechaHasta", fechaHasta) :
            new ObjectParameter("FechaHasta", typeof(System.DateTime));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ObtenerIngresosPorTipoProducto1_Result>("ObtenerIngresosPorTipoProducto1", codTipoProductoParameter, productoParameter, fechaDesdeParameter, fechaHastaParameter);
    }

}

}

