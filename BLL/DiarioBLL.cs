using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL
{
    public class DiarioBLL
    {
        #region Métodos Públicos

        public bool AltaDiario(Producto oProducto, List<DiarioDiaSemana> lstDiarioDiasSemanas)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    using (var loRepProducto = new Repository<Producto>())
                    {
                        bRes = loRepProducto.Create(oProducto) != null;

                        if (bRes)
                        {
                            using (var loRepDiario = new Repository<Diario>())
                            {
                                var oDiario = new Diario
                                {
                                    COD_PRODUCTO = oProducto.ID_PRODUCTO
                                };

                                bRes = loRepDiario.Create(oDiario) != null;

                                if (bRes)
                                {
                                    using (var loRepDiarioDiaSemana = new Repository<DiarioDiaSemana>())
                                    {
                                        var oDiarioDiaSemana = new DiarioDiaSemana
                                        {
                                            COD_DIARIO = oDiario.ID_DIARIO
                                        };

                                        foreach (var loDiarioDiaSemana in lstDiarioDiasSemanas)
                                        {
                                            oDiarioDiaSemana.ID_DIA_SEMANA = loDiarioDiaSemana.ID_DIA_SEMANA;
                                            oDiarioDiaSemana.PRECIO = loDiarioDiaSemana.PRECIO;
                                            bRes = loRepDiarioDiaSemana.Create(oDiarioDiaSemana) != null;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    loTransactionScope.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

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
                    lstDiariosProducto = new List<DiarioProducto>();

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

                        lstDiariosProducto.Add(oDiarioProducto);
                    }

                    if (lstDiariosProducto.Count > 0)                    
                        lstDiariosProducto.Sort((x, y) => String.Compare(x.NOMBRE, y.NOMBRE));
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
