
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
    
public partial class IngresoProducto
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public IngresoProducto()
    {

        this.DetalleIngresoProducto = new HashSet<DetalleIngresoProducto>();

    }


    public int ID_INGRESO_PRODUCTOS { get; set; }

    public System.DateTime FECHA { get; set; }

    public int COD_ESTADO { get; set; }

    public int COD_PROVEEDOR { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleIngresoProducto> DetalleIngresoProducto { get; set; }

    public virtual Estado Estado { get; set; }

    public virtual Proveedor Proveedor { get; set; }

}

}
