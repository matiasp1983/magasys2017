using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Filters
{
    public class EmpleadoFiltro
    {
        #region Propiedades

        public int Tipo_documento { get; set; }
        public int Nro_documento { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }

        #endregion
    }
}
