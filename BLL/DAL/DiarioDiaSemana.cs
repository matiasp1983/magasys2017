
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
    
public partial class DiarioDiaSemana
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public DiarioDiaSemana()
    {

        this.DiarioEdicions = new HashSet<DiarioEdicion>();

    }


    public decimal ID_DIARIO_DIA_SEMANA { get; set; }

    public decimal COD_DIARIO { get; set; }

    public int ID_DIA_SEMANA { get; set; }

    public Nullable<double> PRECIO { get; set; }



    public virtual Diario Diario { get; set; }

    public virtual DiaSemana DiaSemana { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<DiarioEdicion> DiarioEdicions { get; set; }

}

}
