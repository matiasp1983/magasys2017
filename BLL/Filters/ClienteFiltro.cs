using System;

namespace BLL.Filters
{
    public class ClienteFiltro
    {
        #region Propiedades

        public int Id_cliente { get; set; }
        public string Alias { get; set; }
        public int Tipo_documento { get; set; }
        public int Nro_documento { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }

        #endregion
    }
}
