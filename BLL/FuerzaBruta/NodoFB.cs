namespace BLL.FuerzaBruta
{
    public class NodoFB
    {
        #region Propiedades

        public DAL.Cliente CLIENTE { get; set; }

        #endregion

        public NodoFB()
        {
            CLIENTE = new DAL.Cliente();
        }
    }
}
