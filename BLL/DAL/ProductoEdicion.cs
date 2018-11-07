
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
    using System.Collections.Generic;
    
public partial class ProductoEdicion
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ProductoEdicion()
    {

        this.DetalleProductoIngreso = new HashSet<DetalleProductoIngreso>();

        this.DetalleVenta = new HashSet<DetalleVenta>();

        this.DetalleProductoDevolucion = new HashSet<DetalleProductoDevolucion>();

    }


    public int ID_PRODUCTO_EDICION { get; set; }

    public int COD_PRODUCTO { get; set; }

    public int COD_TIPO_PRODUCTO { get; set; }

    public string EDICION { get; set; }

    public Nullable<System.DateTime> FECHA_EDICION { get; set; }

    public int COD_ESTADO { get; set; }

    public string NOMBRE { get; set; }

    public string DESCRIPCION { get; set; }

    public int CANTIDAD_DISPONIBLE { get; set; }

    public double PRECIO { get; set; }

    public Nullable<System.DateTime> FECHA_DEVOLUCION_REAL { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleProductoIngreso> DetalleProductoIngreso { get; set; }

    public virtual Estado Estado { get; set; }

    public virtual Producto Producto { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleProductoDevolucion> DetalleProductoDevolucion { get; set; }

}

}
