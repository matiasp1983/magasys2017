
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
    
public partial class ReservaEdicion
{

    public int ID_RESERVA_EDICION { get; set; }

    public System.DateTime FECHA { get; set; }

    public int COD_RESERVA { get; set; }

    public int COD_PROD_EDICION { get; set; }

    public int COD_ESTADO { get; set; }



    public virtual Estado Estado { get; set; }

    public virtual ProductoEdicion ProductoEdicion { get; set; }

    public virtual Reserva Reserva { get; set; }

}

}
