using System;

namespace BLL.Filters
{
    public class VentaFiltro
    {
        #region Propiedades

        public int ID_VENTA { get; set; }
        public DateTime? FECHAVENTADESDE { get; set; }
        public DateTime? FECHAVENTAHASTA { get; set; }
        public int COD_FORMA_PAGO { get; set; }
        public int COD_ESTADO { get; set; }
        public int TIPO_DOCUMENTO { get; set; }
        public int NRO_DOCUMENTO { get; set; }
        public string APELLIDO { get; set; }
        public string NOMBRE { get; set; }
        public string ALIAS { get; set; }

        #endregion
    }
}
