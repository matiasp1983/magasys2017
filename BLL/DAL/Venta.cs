
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
    
public partial class Venta
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Venta()
    {

        this.DetalleVenta = new HashSet<DetalleVenta>();

    }


    public int ID_VENTA { get; set; }

    public System.DateTime FECHA { get; set; }

    public int COD_ESTADO { get; set; }

    public double TOTAL { get; set; }

    public Nullable<int> COD_CLIENTE { get; set; }

    public int COD_FORMA_PAGO { get; set; }



    public virtual Cliente Cliente { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }

    public virtual FormaPago FormaPago { get; set; }

    public virtual Estado Estado { get; set; }

}

}
