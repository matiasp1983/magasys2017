using BLL.DAL;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class DiarioBLL
    {
        #region Métodos Públicos

        public List<DiarioProducto> ObtenerDiarios()
        {
            List<Diario> lstDiarios = null;
            List<DiarioProducto> lstDiariosProducto = null;

            try
            {
                using (var loRepDiario = new Repository<Diario>())
                {
                    lstDiarios = loRepDiario.FindAll();
                }

                using (var loRepProducto = new Repository<Producto>())
                {
                    foreach (var loItemDiario in lstDiarios)
                    {
                        DiarioProducto oDiarioProducto = new DiarioProducto
                        {
                            ID_DIARIO = new Diario
                            {
                                ID_DIARIO = loItemDiario.ID_DIARIO
                            }.ID_DIARIO,
                            NOMBRE = new Producto
                            {
                                DESCRIPCION = loRepProducto.Find(x => x.ID_PRODUCTO == loItemDiario.COD_PRODUCTO).NOMBRE
                            }.DESCRIPCION
                        };

                        lstDiariosProducto = new List<DiarioProducto>
                        {
                            oDiarioProducto
                        };
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstDiariosProducto;
        }

        #endregion
    }

    #region Clases

    public class DiarioProducto
    {
        public int ID_DIARIO { get; set; }
        public string NOMBRE { get; set; }
    } 

    #endregion
}
