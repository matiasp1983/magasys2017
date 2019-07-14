using System;

namespace BLL.Filters
{
    public class ReservaFiltro
    {
        #region Propiedades

        public DateTime? FECHAINICIORESERVADESDE { get; set; }
        public DateTime? FECHAINICIORESERVAHASTA { get; set; }
        public DateTime? FECHAFINRESERVADESDE { get; set; }
        public DateTime? FECHAFINRESERVAHASTA { get; set; }
        public int COD_TIPO_RESERVA { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public int COD_ESTADO { get; set; }
        public String COD_FORMA_ENTREGA { get; set; }
        public int TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public string ALIAS { get; set; }

        #endregion
    }
}
