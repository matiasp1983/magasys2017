using BLL.DAL;
using System;

namespace BLL
{
    public class RevistaBLL
    {
        #region Métodos Públicos

        public bool AltaRevista(Producto oProducto, Revista oRevista)
        {
            var bRes = false;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    bRes = loRepProducto.Create(oProducto) != null;

                    if (bRes)
                    {
                        using (var loRepRevista = new Repository<Revista>())
                        {
                            bRes = false;
                            oRevista.COD_PRODUCTO = oProducto.ID_PRODUCTO;
                            bRes = loRepRevista.Create(oRevista) != null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (!bRes)
                {
                    if (oProducto.ID_PRODUCTO != 0)
                    {
                        using (var loRepProducto = new Repository<Producto>())
                        { 
                            loRepProducto.Delete(oProducto);
                        }
                    }
                }
                throw;
            }

            return bRes;
        }

        #endregion
    }
}
