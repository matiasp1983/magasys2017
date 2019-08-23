using System;

namespace BLL.Filters
{
    public class CobroFiltro
    {
        #region Propiedades

        public int ID_COBRO { get; set; }
        public DateTime? FECHACOBRODESDE { get; set; }
        public DateTime? FECHACOBROHASTA { get; set; }
        public int COD_ESTADO { get; set; }
        public int TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public string ALIAS { get; set; }

        #endregion
    }
}
