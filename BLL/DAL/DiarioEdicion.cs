
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
    
public partial class DiarioEdicion
{

    public int ID_EDICION { get; set; }

    public int COD_DIA_SEMANA { get; set; }

    public Nullable<int> NUMERO { get; set; }

    public System.DateTime FECHA { get; set; }

    public double PRECIO { get; set; }



    public virtual DiarioDiaSemana DiarioDiaSemana { get; set; }

}

}
