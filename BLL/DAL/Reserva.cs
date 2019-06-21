
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
    
public partial class Reserva
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Reserva()
    {

        this.ReservaEdicion = new HashSet<ReservaEdicion>();

    }


    public int ID_RESERVA { get; set; }

    public System.DateTime FECHA { get; set; }

    public int COD_ESTADO { get; set; }

    public int COD_CLIENTE { get; set; }

    public Nullable<System.DateTime> FECHA_INICIO { get; set; }

    public Nullable<System.DateTime> FECHA_FIN { get; set; }

    public int COD_TIPO_RESERVA { get; set; }

    public int COD_PRODUCTO { get; set; }

    public string ENVIO_DOMICILIO { get; set; }



    public virtual Cliente Cliente { get; set; }

    public virtual Estado Estado { get; set; }

    public virtual Producto Producto { get; set; }

    public virtual TipoReserva TipoReserva { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ReservaEdicion> ReservaEdicion { get; set; }

}

}
