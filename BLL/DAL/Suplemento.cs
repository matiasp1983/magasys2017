
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
    
public partial class Suplemento
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Suplemento()
    {

        this.SuplementoEdicions = new HashSet<SuplementoEdicion>();

    }


    public int ID_SUPLEMENTO { get; set; }

    public int COD_PRODUCTO { get; set; }

    public Nullable<int> ID_DIA_SEMANA { get; set; }

    public int CANTIDAD_ENTREGAS { get; set; }

    public double PRECIO { get; set; }

    public int COD_DIARIO { get; set; }



    public virtual Diario Diario { get; set; }

    public virtual DiaSemana DiaSemana { get; set; }

    public virtual Producto Producto { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<SuplementoEdicion> SuplementoEdicions { get; set; }

}

}
