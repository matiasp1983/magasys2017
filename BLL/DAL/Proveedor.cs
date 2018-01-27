
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
    
public partial class Proveedor
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Proveedor()
    {

        this.Productoes = new HashSet<Producto>();

    }


    public decimal ID_PROVEEDOR { get; set; }

    public System.DateTime FECHA_ALTA { get; set; }

    public string CUIT { get; set; }

    public string RAZON_SOCIAL { get; set; }

    public string NOMBRE { get; set; }

    public string APELLIDO { get; set; }

    public string TELEFONO_MOVIL { get; set; }

    public string TELEFONO_FIJO { get; set; }

    public string EMAIL { get; set; }

    public string CALLE { get; set; }

    public decimal NUMERO { get; set; }

    public string PISO { get; set; }

    public string DEPARTAMENTO { get; set; }

    public string BARRIO { get; set; }

    public string CODIGO_POSTAL { get; set; }

    public decimal ID_PROVINCIA { get; set; }

    public decimal ID_LOCALIDAD { get; set; }

    public Nullable<System.DateTime> FECHA_BAJA { get; set; }



    public virtual Localidad Localidad { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Producto> Productoes { get; set; }

    public virtual Provincia Provincia { get; set; }

}

}
