
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
    
public partial class Pelicula
{

    public decimal ID_PELICULA { get; set; }

    public int COD_PRODUCTO { get; set; }

    public decimal ANIO { get; set; }

    public double PRECIO { get; set; }



    public virtual Producto Producto { get; set; }

}

}
