using System;
using BLL.Common;
using BLL.DAL;
using BLL;
using System.Collections.Generic;

namespace PL.AdminDashboard
{
    public partial class DetalleProductoIngresos : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDetalleProductoIngreso();
        }

        #endregion

        #region Métodos Privados

        private void CargarDetalleProductoIngreso()
        {
            ProductoEdicion oProductoEdicion = null;
            List<DiarioEdicion> lstDiarioEdicion = null;
            DiarioEdicion oDiarioEdicion = null;
            List<RevistaEdicion> lstRevistaEdicion = null;
            RevistaEdicion oRevistaEdicion = null;
            List<ColeccionEdicion> lstColeccionEdicion = null;
            ColeccionEdicion oColeccionEdicion = null;
            List<LibroEdicion> lstLibroEdicion = null;
            LibroEdicion oLibroEdicion = null;
            List<SuplementoEdicion> lstSuplementoEdicion = null;
            SuplementoEdicion oSuplementoEdicion = null;
            List<PeliculaEdicion> lstPeliculaEdicion = null;
            PeliculaEdicion oPeliculaEdicion = null;
            lsvDiarios.DataSource = lsvRevistas.DataSource = lsvColecciones.DataSource = lsvLibros.DataSource = null;
            string loNombre;

            lsvDiarios.Visible = false;
            lsvRevistas.Visible = false;
            lsvColecciones.Visible = false;
            lsvLibros.Visible = false;
            lsvSuplementos.Visible = false;
            lsvPeliculas.Visible = false;


            if (Convert.ToInt32(Session[Enums.Session.IdIngresoProductos.ToString()]) > 0)
            {

                using (var loRepProductoIngreso = new Repository<BLL.DAL.ProductoIngreso>())
                {
                    var loIdIngresoProductos = Convert.ToInt32(Session[Enums.Session.IdIngresoProductos.ToString()]);

                    var loProductoIngreso = loRepProductoIngreso.Find(p => p.COD_ESTADO == 1 && p.ID_INGRESO_PRODUCTOS == loIdIngresoProductos);

                    if (loProductoIngreso != null)
                    {
                        lstDiarioEdicion = new List<DiarioEdicion>();
                        lstRevistaEdicion = new List<RevistaEdicion>();
                        lstColeccionEdicion = new List<ColeccionEdicion>();
                        lstLibroEdicion = new List<LibroEdicion>();
                        lstSuplementoEdicion = new List<SuplementoEdicion>();
                        lstPeliculaEdicion = new List<PeliculaEdicion>();

                        txtProveedor.Text = loProductoIngreso.Proveedor.RAZON_SOCIAL;
                        txtFechaIngresoProducto.Text = loProductoIngreso.FECHA.ToString("dd/MM/yyyy");

                        foreach (var loDetalleProductoIngreso in loProductoIngreso.DetalleProductoIngreso)
                        {
                            oProductoEdicion = loDetalleProductoIngreso.ProductoEdicion;
                            loNombre = oProductoEdicion.Producto.NOMBRE;

                            switch (oProductoEdicion.COD_TIPO_PRODUCTO)
                            {
                                case 1:

                                    oDiarioEdicion = new DiarioEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = "$ " + oProductoEdicion.PRECIO.ToString(),
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oDiarioEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);

                                    break;

                                case 2:

                                    oRevistaEdicion = new RevistaEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = "$ " + oProductoEdicion.PRECIO,
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };
                                    if (oProductoEdicion.DESCRIPCION != null)
                                        oRevistaEdicion.DESCRIPCION = oProductoEdicion.DESCRIPCION;
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oRevistaEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);

                                    break;

                                case 3:

                                    oColeccionEdicion = new ColeccionEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = "$ " + oProductoEdicion.PRECIO,
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };
                                    if (oProductoEdicion.DESCRIPCION != null)
                                        oColeccionEdicion.DESCRIPCION = oProductoEdicion.DESCRIPCION;
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oColeccionEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);


                                    break;

                                case 4:

                                    oLibroEdicion = new LibroEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = "$ " + oProductoEdicion.PRECIO.ToString(),
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };

                                    using (var loRepLibro = new Repository<Libro>())
                                        oLibroEdicion.AUTOR = loRepLibro.Find(p => p.COD_PRODUCTO == oProductoEdicion.COD_PRODUCTO).AUTOR;

                                    if (oProductoEdicion.DESCRIPCION != null)
                                        oLibroEdicion.DESCRIPCION = oProductoEdicion.DESCRIPCION;
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oLibroEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);

                                    break;

                                case 5:

                                    oSuplementoEdicion = new SuplementoEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = "$ " + oProductoEdicion.PRECIO.ToString(),
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };

                                    if (oProductoEdicion.FECHA_EDICION != null)
                                        oSuplementoEdicion.FECHA_EDICION = oProductoEdicion.FECHA_EDICION;
                                    if (oProductoEdicion.DESCRIPCION != null)
                                        oSuplementoEdicion.DESCRIPCION = oProductoEdicion.DESCRIPCION;
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oSuplementoEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);

                                    break;

                                case 6:

                                    oPeliculaEdicion = new PeliculaEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = "$ " + oProductoEdicion.PRECIO.ToString(),
                                        CANTIDAD_DISPONIBLE = loDetalleProductoIngreso.CANTIDAD_UNIDADES
                                    };


                                    if (oProductoEdicion.FECHA_EDICION != null)
                                        oPeliculaEdicion.FECHA_EDICION = oProductoEdicion.FECHA_EDICION;
                                    if (oProductoEdicion.DESCRIPCION != null)
                                        oPeliculaEdicion.DESCRIPCION = oProductoEdicion.DESCRIPCION;
                                    if (loDetalleProductoIngreso.FECHA_DEVOLUCION != null)
                                        oPeliculaEdicion.FECHA_DEVOLUCION = Convert.ToDateTime(loDetalleProductoIngreso.FECHA_DEVOLUCION);

                                    break;
                            }

                            if (oDiarioEdicion != null)
                                lstDiarioEdicion.Add(oDiarioEdicion);
                            else if (oRevistaEdicion != null)
                                lstRevistaEdicion.Add(oRevistaEdicion);
                            else if (oColeccionEdicion != null)
                                lstColeccionEdicion.Add(oColeccionEdicion);
                            else if (oLibroEdicion != null)
                                lstLibroEdicion.Add(oLibroEdicion);
                            else if (oSuplementoEdicion != null)
                                lstSuplementoEdicion.Add(oSuplementoEdicion);
                            else if (oPeliculaEdicion != null)
                                lstPeliculaEdicion.Add(oPeliculaEdicion);

                        }

                        if (lstDiarioEdicion.Count > 0)
                        {
                            lsvDiarios.Visible = true;
                            lsvDiarios.DataSource = lstDiarioEdicion;
                            lsvDiarios.DataBind();
                        }

                        if (lstRevistaEdicion.Count > 0)
                        {
                            lsvRevistas.Visible = true;
                            lsvRevistas.DataSource = lstRevistaEdicion;
                            lsvRevistas.DataBind();
                        }

                        if (lstColeccionEdicion.Count > 0)
                        {
                            lsvColecciones.Visible = true;
                            lsvColecciones.DataSource = lstColeccionEdicion;
                            lsvColecciones.DataBind();
                        }

                        if (lstLibroEdicion.Count > 0)
                        {
                            lsvLibros.Visible = true;
                            lsvLibros.DataSource = lstLibroEdicion;
                            lsvLibros.DataBind();
                        }

                        if (lstSuplementoEdicion.Count > 0)
                        {
                            lsvSuplementos.Visible = true;
                            lsvSuplementos.DataSource = lstSuplementoEdicion;
                            lsvSuplementos.DataBind();
                        }

                        if (lstPeliculaEdicion.Count > 0)
                        {
                            lsvPeliculas.Visible = true;
                            lsvPeliculas.DataSource = lstPeliculaEdicion;
                            lsvPeliculas.DataBind();
                        }

                    }
                }
            }
        }

        #endregion
    }
}