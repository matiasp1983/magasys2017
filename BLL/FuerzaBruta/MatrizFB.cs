using System.Collections.Generic;

namespace BLL.FuerzaBruta
{
    public class MatrizFB
    {
        #region Propiedades

        public List<FilaMatrizFB> COMBINACIONES_POSIBLES { get; set; }

        #endregion

        public MatrizFB()
        {
            COMBINACIONES_POSIBLES = new List<FilaMatrizFB>();
        }
    }
}
