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

    public virtual DbSet<Coleccion> Coleccion { get; set; }

    public virtual DbSet<DetalleProductoIngreso> DetalleProductoIngreso { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<Diario> Diario { get; set; }

    public virtual DbSet<DiarioDiaSemana> DiarioDiaSemana { get; set; }

    public virtual DbSet<DiaSemana> DiaSemana { get; set; }

    public virtual DbSet<Estado> Estado { get; set; }

    public virtual DbSet<Genero> Genero { get; set; }

    public virtual DbSet<Libro> Libro { get; set; }

    public virtual DbSet<Localidad> Localidad { get; set; }

    public virtual DbSet<Pelicula> Pelicula { get; set; }

    public virtual DbSet<Periodicidad> Periodicidad { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ProductoEdicion> ProductoEdicion { get; set; }

    public virtual DbSet<ProductoIngreso> ProductoIngreso { get; set; }

    public virtual DbSet<Proveedor> Proveedor { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Revista> Revista { get; set; }

    public virtual DbSet<Suplemento> Suplemento { get; set; }

    public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }

    public virtual DbSet<TipoProducto> TipoProducto { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

}

}

