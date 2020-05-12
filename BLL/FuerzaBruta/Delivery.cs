using System;

namespace BLL.FuerzaBruta
{
    public class Delivery
    {
        #region Propiedades

        public DAL.Cliente CLIENTE { get; set; }
        public int COD_PRDUCTO { get; set; }
        public int COD_EDICION { get; set; }

        #endregion

        public Delivery()
        {
            CLIENTE = new DAL.Cliente();
        }
    }
}
