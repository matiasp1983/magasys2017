using BLL.DAL;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace BLL
{
    public class DiarioBLL
    {
        #region Métodos Públicos

        public ProductoDiario ObtenerDiario(long idProducto)
        {
            Producto oProducto = null;
            Diario oDiario = null;
            List<DiarioDiaSemana> lstDiarioDiaSemana = null;
            ProductoDiario oProductoDiario = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepDiario = new Repository<Diario>())
                    {
                        oDiario = loRepDiario.Find(x => x.COD_PRODUCTO == oProducto.ID_PRODUCTO);

                        using (var loRepDiarioDiaSemana = new Repository<DiarioDiaSemana>())
                        {
                            lstDiarioDiaSemana = loRepDiarioDiaSemana.Search(x => x.COD_DIARIO == oDiario.ID_DIARIO);

                            oProductoDiario = new ProductoDiario
                            {
                                ID_PRODUCTO = oProducto.ID_PRODUCTO,
                                FECHA_ALTA = oProducto.FECHA_ALTA,
                                NOMBRE = oProducto.NOMBRE,
                                DESCRIPCION = oProducto.DESCRIPCION,
                                COD_ESTADO = oProducto.COD_ESTADO,
                                COD_GENERO = oProducto.COD_GENERO,
                                COD_PROVEEDOR = oProducto.COD_PROVEEDOR,
                                COD_TIPO_PRODUCTO = oProducto.COD_TIPO_PRODUCTO,
                                ID_DIARIO = oDiario.ID_DIARIO
                            };

                            foreach (var loDiarioDiaSemana in lstDiarioDiaSemana)
                            {
                                using (var loRepDiaSemana = new Repository<DiaSemana>())
                                {
                                    String loDiaSemana = loRepDiaSemana.Find(x => x.ID_DIA_SEMANA == loDiarioDiaSemana.ID_DIA_SEMANA).NOMBRE;

                                    switch (loDiaSemana)
                                    {
                                        case "Lunes":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_LUNES = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_LUNES = loDiarioDiaSemana.PRECIO;
                                            break;
                                        case "Martes":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_MARTES = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_MARTES = loDiarioDiaSemana.PRECIO;
                                            break;
                                        case "Miércoles":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_MIERCOLES = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_MIERCOLES = loDiarioDiaSemana.PRECIO;
                                            break;
                                        case "Jueves":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_JUEVES = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_JUEVES = loDiarioDiaSemana.PRECIO;
                                            break;
                                        case "Viernes":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_VIERNES = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_VIERNES = loDiarioDiaSemana.PRECIO;
                                            break;
                                        case "Sábado":
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_SABADO = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_SABADO = loDiarioDiaSemana.PRECIO;
                                            break;
                                        default:
                                            oProductoDiario.ID_DIARIO_DIA_SEMANA_DOMINGO = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA;
                                            oProductoDiario.PRECIO_DOMINGO = loDiarioDiaSemana.PRECIO;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoDiario;
        }

        public ProductoDiario ObtenerDiarioPorIdDiario(int idDiario)
        {
            ProductoDiario oProductoDiario = null;
            Diario oDiario = null;

            try
            {
                using (var rep = new Repository<Diario>())
                {
                    oDiario = rep.Find(p => p.ID_DIARIO == idDiario);
                }
                if (oDiario != null)
                {
                    oProductoDiario = ObtenerDiario(oDiario.COD_PRODUCTO);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoDiario;
        }

        public List<ProductoDiario> ObtenerDiarios()
        {
            List<Diario> lstDiarios = null;
            List<ProductoDiario> lstDiariosProducto = null;

            try
            {
                using (var loRepDiario = new Repository<Diario>())
                {
                    lstDiarios = loRepDiario.FindAll();
                }

                using (var loRepProducto = new Repository<Producto>())
                {
                    lstDiariosProducto = new List<ProductoDiario>();

                    foreach (var loItemDiario in lstDiarios)
                    {
                        ProductoDiario oProductoDiario = new ProductoDiario
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

                        lstDiariosProducto.Add(oProductoDiario);
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

        public bool ModificarDiario(Producto oProducto, List<DiarioDiaSemana> lstDiarioDiasSemanas)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    using (var loRepProducto = new Repository<Producto>())
                    {
                        bRes = loRepProducto.Update(oProducto);

                        if (bRes)
                        {
                            using (var loRepDiarioDiaSemana = new Repository<DiarioDiaSemana>())
                            {
                                foreach (var loDiarioDiaSemana in lstDiarioDiasSemanas)
                                {
                                    var oDiarioDiaSemana = new DiarioDiaSemana
                                    {
                                        ID_DIARIO_DIA_SEMANA = loDiarioDiaSemana.ID_DIARIO_DIA_SEMANA,
                                        COD_DIARIO = loDiarioDiaSemana.COD_DIARIO,
                                        ID_DIA_SEMANA = loDiarioDiaSemana.ID_DIA_SEMANA,
                                        PRECIO = loDiarioDiaSemana.PRECIO
                                    };

                                    bRes = loRepDiarioDiaSemana.Update(oDiarioDiaSemana);
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

        #endregion
    }

    #region Clases

    public class ProductoDiario
    {
        public int ID_PRODUCTO { get; set; }
        public DateTime FECHA_ALTA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public int ID_DIARIO { get; set; }
        public int ID_DIARIO_DIA_SEMANA_LUNES { get; set; }
        public int ID_DIARIO_DIA_SEMANA_MARTES { get; set; }
        public int ID_DIARIO_DIA_SEMANA_MIERCOLES { get; set; }
        public int ID_DIARIO_DIA_SEMANA_JUEVES { get; set; }
        public int ID_DIARIO_DIA_SEMANA_VIERNES { get; set; }
        public int ID_DIARIO_DIA_SEMANA_SABADO { get; set; }
        public int ID_DIARIO_DIA_SEMANA_DOMINGO { get; set; }
        public double? PRECIO_LUNES { get; set; }
        public double? PRECIO_MARTES { get; set; }
        public double? PRECIO_MIERCOLES { get; set; }
        public double? PRECIO_JUEVES { get; set; }
        public double? PRECIO_VIERNES { get; set; }
        public double? PRECIO_SABADO { get; set; }
        public double? PRECIO_DOMINGO { get; set; }
    }

    #endregion
}
