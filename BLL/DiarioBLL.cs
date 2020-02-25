using BLL.DAL;
using BLL.Filters;
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
            DiarioDiaSemana oDiario = null;
            ProductoDiario oProductoDiario = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    oProducto = loRepProducto.Find(x => x.ID_PRODUCTO == idProducto);

                    using (var loRepDiario = new Repository<DiarioDiaSemana>())
                    {
                        oDiario = loRepDiario.Find(x => x.COD_DIARIO == oProducto.ID_PRODUCTO);

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
                            ID_DIARIO = oDiario.COD_DIARIO
                        };
                        oProductoDiario.ID_DIARIO_DIA_SEMAMA = oDiario.ID_DIARIO_DIA_SEMANA;
                        oProductoDiario.PRECIO = oDiario.PRECIO;
                        oProductoDiario.COD_DIA_SEMAMA = oDiario.ID_DIA_SEMANA;

                        if (oProducto.COD_IMAGEN != null)
                            oProductoDiario.IMAGEN = oProducto.Imagen;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProductoDiario;
        }

        public Producto ObtenerDiarioPorIdDiario(int idDiario)
        {
            Producto oProducto = null;

            try
            {
                using (var rep = new Repository<DiarioDiaSemana>())
                {
                    var oDiario = rep.Find(p => p.ID_DIARIO_DIA_SEMANA == idDiario);

                    if (oDiario != null)
                        oProducto = oDiario.Producto;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return oProducto;
        }

        public List<ProductoDiario> ObtenerDiarios()
        {
            List<DiarioDiaSemana> lstDiarios = null;
            List<ProductoDiario> lstDiariosProducto = null;

            try
            {
                using (var loRepDiario = new Repository<DiarioDiaSemana>())
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
                            ID_DIARIO = loItemDiario.COD_DIARIO,
                            NOMBRE = new Producto
                            {
                                DESCRIPCION = loRepProducto.Find(x => x.ID_PRODUCTO == loItemDiario.COD_DIARIO).NOMBRE
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

        public List<DiarioEdicion> ObtenerDiariosParaEdicion(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProductos = null;
            List<DiarioEdicion> lstDiarioEdicion = null;
            List<DiarioDiaSemana> lstDiasSemana = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProductos = loRepProducto.Search(p => p.FECHA_BAJA == null && p.COD_ESTADO == 1);

                    if (oProductoFiltro.CodProveedor > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    if (oProductoFiltro.CodTipoProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));
                }

                DiarioEdicion oDiarioEdicion;
                lstDiarioEdicion = new List<DiarioEdicion>();

                foreach (var loProducto in lstProductos)
                {
                    int loCodigoDiario;

                    using (var loRepDiario = new Repository<DiarioDiaSemana>())
                        loCodigoDiario = loRepDiario.Find(p => p.COD_DIARIO == loProducto.ID_PRODUCTO).COD_DIARIO;

                    using (var loRepDiarioDiaSemana = new Repository<DiarioDiaSemana>())
                        lstDiasSemana = loRepDiarioDiaSemana.Search(p => p.COD_DIARIO == loCodigoDiario);

                    foreach (var loDiasSemana in lstDiasSemana)
                    {
                        oDiarioEdicion = new DiarioEdicion
                        {
                            COD_PRODUCTO = loProducto.ID_PRODUCTO,
                            NOMBRE = loProducto.NOMBRE,
                            COD_DIARIO = loCodigoDiario
                        };

                        using (var loRepDiaSemana = new Repository<DiaSemana>())
                            oDiarioEdicion.DIA_SEMANA = loRepDiaSemana.Find(p => p.ID_DIA_SEMANA == loDiasSemana.ID_DIA_SEMANA).NOMBRE;

                        lstDiarioEdicion.Add(oDiarioEdicion);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstDiarioEdicion;
        }

        public List<DiarioEdicion> ObtenerDiariosEdicion(ProductoFiltro oProductoFiltro)
        {
            List<DiarioEdicion> lstDiarioEdicion = null;
            List<ProductoEdicion> lstProductoEdicion = null;

            try
            {
                using (var loRepProductoEdicion = new Repository<ProductoEdicion>())
                {
                    lstProductoEdicion = loRepProductoEdicion.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto && p.Producto.COD_PROVEEDOR == oProductoFiltro.CodProveedor && p.CANTIDAD_DISPONIBLE > 0);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => p.EDICION.ToUpper().Contains(oProductoFiltro.NombreEdicion.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionEdicion) && lstProductoEdicion.Count > 0)
                        lstProductoEdicion = lstProductoEdicion.FindAll(p => !string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionEdicion.ToUpper()));

                    DiarioEdicion oDiarioEdicion;
                    lstDiarioEdicion = new List<DiarioEdicion>();

                    foreach (var loProductoEdicion in lstProductoEdicion)
                    {
                        // Filtro por Nombre de Producto
                        if ((String.IsNullOrEmpty(oProductoFiltro.NombreProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && loProductoEdicion.Producto.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper())))
                        {
                            // Filtro por Descripción del Producto
                            if ((String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto)) || (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && !string.IsNullOrEmpty(loProductoEdicion.Producto.DESCRIPCION) && loProductoEdicion.Producto.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper())))
                            {
                                oDiarioEdicion = new DiarioEdicion
                                {
                                    COD_PRODUCTO = loProductoEdicion.COD_PRODUCTO,
                                    COD_PRODUCTO_EDICION = loProductoEdicion.ID_PRODUCTO_EDICION,
                                    NOMBRE = loProductoEdicion.Producto.NOMBRE, //nombre del Producto
                                    TIPO_PRODUCTO = loProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                    EDICION = loProductoEdicion.EDICION,
                                    FECHA_EDICION = Convert.ToDateTime(loProductoEdicion.FECHA_EDICION),
                                    PRECIO = "$" + loProductoEdicion.PRECIO.ToString(),
                                    CANTIDAD_DISPONIBLE = loProductoEdicion.CANTIDAD_DISPONIBLE
                                };

                                lstDiarioEdicion.Add(oDiarioEdicion);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstDiarioEdicion;
        }

        public bool AltaDiario(Producto oProducto, DiarioDiaSemana loDiarioDiasSemana)
        {
            var bRes = false;

            try
            {
                oProducto.DiarioDiaSemana.Add(loDiarioDiasSemana);

                using (var loRepProducto = new Repository<Producto>())
                    bRes = loRepProducto.Create(oProducto) != null;

                oProducto.DiarioDiaSemana.Remove(loDiarioDiasSemana);
            }
            catch (Exception)
            {
                throw;
            }

            return bRes;
        }

        public bool ModificarDiario(Producto oProducto, DiarioDiaSemana oDiarioDiaSemana)
        {
            var bRes = false;

            try
            {
                using (TransactionScope loTransactionScope = new TransactionScope())
                {
                    if (oProducto.Imagen != null)
                    {
                        using (var loRepImagen = new Repository<Imagen>())
                        {
                            var loImangen = loRepImagen.Create(oProducto.Imagen);
                            oProducto.COD_IMAGEN = loImangen.ID_IMAGEN;
                        }
                    }

                    using (var loRepProducto = new Repository<Producto>())
                    {
                        bRes = loRepProducto.Update(oProducto);

                        if (bRes)
                        {
                            using (var loRepDiarioDiaSemana = new Repository<DiarioDiaSemana>())
                            {
                                bRes = loRepDiarioDiaSemana.Update(oDiarioDiaSemana);
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
        public int COD_DIA_SEMAMA { get; set; }
        public int ID_DIARIO_DIA_SEMAMA { get; set; }
        public double? PRECIO { get; set; }
        public BLL.DAL.Imagen IMAGEN { get; set; }
    }

    public class DiarioEdicion
    {
        public int COD_PRODUCTO { get; set; }
        public int COD_PRODUCTO_EDICION { get; set; }
        public int COD_DIARIO { get; set; }
        public string NOMBRE { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string DIA_SEMANA { get; set; }
        public string EDICION { get; set; }
        public System.DateTime FECHA_EDICION { get; set; }
        public string PRECIO { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public int CANTIDAD { get; set; }
        public System.DateTime? FECHA_DEVOLUCION { get; set; }
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public string TITULO { get; set; }
    }

    #endregion
}
