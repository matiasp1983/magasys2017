﻿using BLL.DAL;
using BLL.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public class ProductoBLL
    {
        #region Métodos Públicos

        public List<ProductoListado> ObtenerProductos(ProductoFiltro oProductoFiltro)
        {

            List<Producto> lstProductos = null;
            List<ProductoListado> lstProductoListado = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProductos = loRepProducto.FindAll().OrderByDescending(p => p.ID_PRODUCTO).ToList();

                    if (oProductoFiltro.IdProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.ID_PRODUCTO.ToString().Contains(oProductoFiltro.IdProducto.ToString()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));

                    if (oProductoFiltro.CodTipoProducto > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (oProductoFiltro.CodEstado > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_ESTADO == oProductoFiltro.CodEstado);

                    if (oProductoFiltro.CodProveedor > 0 && lstProductos.Count > 0)
                        lstProductos = lstProductos.FindAll(p => p.COD_PROVEEDOR == oProductoFiltro.CodProveedor);

                    ProductoListado oProductoListado;
                    lstProductoListado = new List<ProductoListado>();

                    foreach (var loProducto in lstProductos)
                    {
                        oProductoListado = new ProductoListado
                        {
                            ID_PRODUCTO = loProducto.ID_PRODUCTO,
                            NOMBRE = loProducto.NOMBRE,
                            COD_ESTADO = loProducto.COD_ESTADO,
                            COD_GENERO = loProducto.COD_GENERO,
                            COD_PROVEEDOR = loProducto.COD_GENERO,
                            COD_TIPO_PRODUCTO = loProducto.COD_TIPO_PRODUCTO
                        };

                        using (var loRepEstadoProducto = new Repository<Estado>())
                            oProductoListado.DESC_ESTADO = loRepEstadoProducto.Find(p => p.ID_ESTADO == loProducto.COD_ESTADO).NOMBRE;

                        using (var loRepGenero = new Repository<Genero>())
                            oProductoListado.DESC_GENERO = loRepGenero.Find(p => p.ID_GENERO == loProducto.COD_GENERO).NOMBRE;

                        using (var loRepProveedor = new Repository<Proveedor>())
                            oProductoListado.DESC_PROVEEDOR = loRepProveedor.Find(p => p.ID_PROVEEDOR == loProducto.COD_PROVEEDOR).RAZON_SOCIAL;

                        using (var loRepTipoProducto = new Repository<TipoProducto>())
                            oProductoListado.DESC_TIPO_PRODUCTO = loRepTipoProducto.Find(p => p.ID_TIPO_PRODUCTO == loProducto.COD_TIPO_PRODUCTO).DESCRIPCION;

                        lstProductoListado.Add(oProductoListado);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return lstProductoListado;
        }

        public List<ProductoListado> ObtenerProductosPorTipoProducto(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProducto = null;
            List<ProductoListado> lstProductoListado = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProducto = loRepProducto.Search(p => p.COD_ESTADO == 1 && p.COD_PROVEEDOR == oProductoFiltro.CodProveedor && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => !string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper()));

                    ProductoListado oProductoListado;
                    lstProductoListado = new List<ProductoListado>();

                    foreach (var loProducto in lstProducto)
                    {
                        oProductoListado = new ProductoListado
                        {
                            ID_PRODUCTO = loProducto.ID_PRODUCTO,
                            NOMBRE = loProducto.NOMBRE,
                            DESC_TIPO_PRODUCTO = loProducto.TipoProducto.DESCRIPCION
                        };

                        if (!String.IsNullOrEmpty(loProducto.DESCRIPCION))
                            oProductoListado.DESCRIPCION = loProducto.DESCRIPCION;

                        lstProductoListado.Add(oProductoListado);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstProductoListado;
        }

        public List<ProductoListado> ObtenerProductosConImagen(ProductoFiltro oProductoFiltro)
        {
            List<Producto> lstProducto = null;
            List<ProductoListado> lstProductoListado = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProducto = loRepProducto.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => !string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper()));

                    if (oProductoFiltro.CodEstado > 0 && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => p.COD_GENERO == oProductoFiltro.CodGenero);

                    ProductoListado oProductoListado;
                    lstProductoListado = new List<ProductoListado>();

                    foreach (var loProducto in lstProducto)
                    {
                        oProductoListado = new ProductoListado
                        {
                            ID_PRODUCTO = loProducto.ID_PRODUCTO,
                            NOMBRE = loProducto.NOMBRE,
                            DESC_TIPO_PRODUCTO = loProducto.TipoProducto.DESCRIPCION
                        };

                        if (!String.IsNullOrEmpty(loProducto.DESCRIPCION))
                            oProductoListado.DESCRIPCION = loProducto.DESCRIPCION;

                        lstProductoListado.Add(oProductoListado);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lstProductoListado;
        }

        public List<ProductoCustomersWebSite> ObtenerProductosSeleccionados(ProductoFiltro oProductoFiltro)
        {
            List<ProductoCustomersWebSite> lstProductoCustomersWebSite = null;
            List<Producto> lstProducto = null;

            try
            {
                using (var loRepProducto = new Repository<Producto>())
                {
                    lstProducto = loRepProducto.Search(p => p.COD_ESTADO == 1 && p.COD_TIPO_PRODUCTO == oProductoFiltro.CodTipoProducto);

                    if (oProductoFiltro.CodGenero > 0 && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => p.COD_GENERO == oProductoFiltro.CodGenero);

                    if (!String.IsNullOrEmpty(oProductoFiltro.NombreProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => p.NOMBRE.ToUpper().Contains(oProductoFiltro.NombreProducto.ToUpper()));

                    if (!String.IsNullOrEmpty(oProductoFiltro.DescripcionProducto) && lstProducto.Count > 0)
                        lstProducto = lstProducto.FindAll(p => (!string.IsNullOrEmpty(p.DESCRIPCION) && p.DESCRIPCION.ToUpper().Contains(oProductoFiltro.DescripcionProducto.ToUpper())));

                    ProductoCustomersWebSite oProductoCustomersWebSite;
                    lstProductoCustomersWebSite = new List<ProductoCustomersWebSite>();

                    foreach (var loProduto in lstProducto)
                    {
                        oProductoCustomersWebSite = new ProductoCustomersWebSite
                        {
                            COD_PRODUCTO = loProduto.ID_PRODUCTO,
                            NOMBRE_PRODUCTO = loProduto.NOMBRE.ToUpper(),
                            COD_TIPO_PRODUCTO = loProduto.COD_TIPO_PRODUCTO,
                            TIPO_PRODUCTO = loProduto.TipoProducto.DESCRIPCION
                        };

                        oProductoCustomersWebSite.IMAGEN = new System.Web.UI.WebControls.Image();

                        if (loProduto.Imagen != null)
                        {
                            // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loProduto.Imagen.IMAGEN1);
                            oProductoCustomersWebSite.IMAGEN.ImageUrl = loImagenDataURL64;
                        }

                        if (!String.IsNullOrEmpty(loProduto.DESCRIPCION))
                            oProductoCustomersWebSite.DESCRIPCION = loProduto.DESCRIPCION;

                        // Obener los precios por tipo de producto
                        switch (loProduto.COD_TIPO_PRODUCTO)
                        {
                            case 1:
                                var oProductoDiario = new DiarioBLL().ObtenerDiario(loProduto.ID_PRODUCTO);
                                if (oProductoDiario.PRECIO != null)
                                    oProductoCustomersWebSite.PRECIO = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", oProductoDiario.PRECIO);
                                break;

                            case 4:
                                var oProductoLibro = new LibroBLL().ObtenerLibro(loProduto.ID_PRODUCTO);
                                if (!String.IsNullOrEmpty(oProductoLibro.EDITORIAL))
                                    oProductoCustomersWebSite.EDITORIAL = "Editorial: " + oProductoLibro.EDITORIAL;
                                if (!String.IsNullOrEmpty(oProductoLibro.AUTOR))
                                    oProductoCustomersWebSite.AUTOR = "Autor: " + oProductoLibro.AUTOR;
                                if (oProductoLibro.ANIO > 0)
                                    oProductoCustomersWebSite.ANIO = "Año: " + oProductoLibro.ANIO.ToString();
                                oProductoCustomersWebSite.PRECIO = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", oProductoLibro.PRECIO);
                                break;

                            case 5:
                                var oProductoSuplemento = new SuplementoBLL().ObtenerSuplemento(loProduto.ID_PRODUCTO);
                                oProductoCustomersWebSite.NOMBRE_DIARIO = "Diario: " + oProductoSuplemento.NOMBRE_DIARIO;

                                break;

                            case 6:
                                var oProductoPelicula = new PeliculaBLL().ObtenerPelicula(loProduto.ID_PRODUCTO);
                                if (oProductoPelicula.ANIO > 0)
                                    oProductoCustomersWebSite.ANIO = "Año: " + oProductoPelicula.ANIO.ToString();
                                oProductoCustomersWebSite.PRECIO = string.Format(System.Globalization.CultureInfo.GetCultureInfo("de-DE"), "{0:0.00}", oProductoPelicula.PRECIO);

                                break;

                            default:
                                break;
                        }

                        if (oProductoCustomersWebSite.PRECIO == null)
                            oProductoCustomersWebSite.PRECIO = "0,00";

                        lstProductoCustomersWebSite.Add(oProductoCustomersWebSite);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProductoCustomersWebSite;
        }

        #endregion
    }

    #region Clases

    public class ProductoListado
    {
        public int ID_PRODUCTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_ESTADO { get; set; }
        public string DESC_ESTADO { get; set; }
        public int COD_GENERO { get; set; }
        public string DESC_GENERO { get; set; }
        public int COD_PROVEEDOR { get; set; }
        public string DESC_PROVEEDOR { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public string DESC_TIPO_PRODUCTO { get; set; }
        public byte IMAGEN { get; set; }
    }

    public class ProductoCustomersWebSite
    {
        public System.Web.UI.WebControls.Image IMAGEN { get; set; }
        public int COD_PRODUCTO { get; set; }
        public string NOMBRE_PRODUCTO { get; set; }
        public string DESCRIPCION { get; set; }
        public int COD_TIPO_PRODUCTO { get; set; }
        public string TIPO_PRODUCTO { get; set; }
        public string EDICION { get; set; }
        public int CANTIDAD_DISPONIBLE { get; set; }
        public string PRECIO { get; set; }
        public string EDITORIAL { get; set; }
        public string AUTOR { get; set; }
        public string ANIO { get; set; }
        public string NOMBRE_DIARIO { get; set; }
    }

    #endregion

}
