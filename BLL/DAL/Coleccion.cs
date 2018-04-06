
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
    
public partial class Coleccion
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Coleccion()
    {

        this.ColeccionEntregas = new HashSet<ColeccionEntrega>();

    }


    public int ID_COLECCION { get; set; }

    public int COD_PRODUCTO { get; set; }

    public int COD_PERIODICIDAD { get; set; }

    public Nullable<int> ID_DIA_SEMANA { get; set; }

    public int CANTIDAD_ENTREGAS { get; set; }



    public virtual DiaSemana DiaSemana { get; set; }

    public virtual Periodicidad Periodicidad { get; set; }

    public virtual Producto Producto { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ColeccionEntrega> ColeccionEntregas { get; set; }

}

}
