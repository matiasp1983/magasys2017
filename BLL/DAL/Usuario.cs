
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
    
public partial class Usuario
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Usuario()
    {

        this.Cliente = new HashSet<Cliente>();

        this.Empleado = new HashSet<Empleado>();

    }


    public int ID_USUARIO { get; set; }

    public string NOMBRE_USUARIO { get; set; }

    public string NOMBRE { get; set; }

    public string APELLIDO { get; set; }

    public string CONTRASENIA { get; set; }

    public byte[] AVATAR { get; set; }

    public System.DateTime FECHA_ALTA { get; set; }

    public Nullable<System.DateTime> FECHA_BAJA { get; set; }

    public int ID_ROL { get; set; }

    public int COD_ESTADO { get; set; }

    public string RECUPERAR_CONTRASENIA { get; set; }

    public string EMAIL { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Cliente> Cliente { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Empleado> Empleado { get; set; }

    public virtual Estado Estado { get; set; }

    public virtual Rol Rol { get; set; }

}

}
