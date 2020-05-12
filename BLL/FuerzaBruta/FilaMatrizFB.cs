using System.Collections.Generic;

namespace BLL.FuerzaBruta
{
    public class FilaMatrizFB
    {
        #region Propiedades

        public List<NodoFB> NODOS { get; set; }
        public List<NodoDistanciaFB> NODOS_DISTANCIA { get; set; }
        public float COSTO_TOTAL_DISTANCIA { get; set; }
        //public float COSTO_TOTAL_TIEMPO { get; set; }

        #endregion

        #region MyRegion

        public FilaMatrizFB()
        {
            NODOS = new List<NodoFB>();
            NODOS_DISTANCIA = new List<NodoDistanciaFB>();
            COSTO_TOTAL_DISTANCIA = 0;
        }

        public void calcularCostosTotales()
        {
            foreach (var nodo in NODOS_DISTANCIA)
            {
                COSTO_TOTAL_DISTANCIA += nodo.DISTANCIA;
                //costoTotalTiempo += nodo.tiempo;
            }
        }

        #endregion
    }
}
