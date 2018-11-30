using System;
using BLL.Common;
using BLL.DAL;
using BLL;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using NLog;

namespace PL.AdminDashboard
{
    public partial class DetalleProductoIngresosEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lsvDiarios.DataSource = lsvRevistas.DataSource = lsvColecciones.DataSource = lsvLibros.DataSource = lsvSuplementos.DataSource = lsvPeliculas.DataSource = null;

                lsvDiarios.DataBind();
                lsvRevistas.DataBind();
                lsvColecciones.DataBind();
                lsvLibros.DataBind();
                lsvSuplementos.DataBind();
                lsvPeliculas.DataBind();

                CargarDetalleProductoIngreso();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GuardarEdicion())
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoIngresoSuccessModificacion, "Modificación de Ingreso de productos"));
                    Session.Remove(Enums.Session.IdIngresoProductos.ToString());
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoFailure));
                    Session.Remove(Enums.Session.IdIngresoProductos.ToString());
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.IdIngresoProductos.ToString());
            Session.Remove(Enums.Session.DetalleIngresoProductos.ToString());
            Response.Redirect("ProductoIngresoListado.aspx", false);
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
                using (var loRepDetalleProductoIngreso = new Repository<DetalleProductoIngreso>())
                {
                    var loIdIngresoProductos = Convert.ToInt32(Session[Enums.Session.IdIngresoProductos.ToString()]);

                    var lstDetalleProductoIngreso = loRepDetalleProductoIngreso.Search(p => p.COD_ESTADO == 1 && p.COD_INGRESO_PRODUCTO == loIdIngresoProductos);

                    if (lstDetalleProductoIngreso.Count > 0)
                    {
                        Session.Add(Enums.Session.DetalleIngresoProductos.ToString(), lstDetalleProductoIngreso);

                        lstDiarioEdicion = new List<DiarioEdicion>();
                        lstRevistaEdicion = new List<RevistaEdicion>();
                        lstColeccionEdicion = new List<ColeccionEdicion>();
                        lstLibroEdicion = new List<LibroEdicion>();
                        lstSuplementoEdicion = new List<SuplementoEdicion>();
                        lstPeliculaEdicion = new List<PeliculaEdicion>();

                        foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                        {
                            oProductoEdicion = loDetalleProductoIngreso.ProductoEdicion;
                            loNombre = oProductoEdicion.Producto.NOMBRE;
                            var loTipoProductoDescripcion = oProductoEdicion.Producto.TipoProducto.DESCRIPCION;

                            switch (oProductoEdicion.COD_TIPO_PRODUCTO)
                            {
                                case 1:

                                    oDiarioEdicion = new DiarioEdicion
                                    {
                                        COD_PRODUCTO = oProductoEdicion.COD_PRODUCTO,
                                        NOMBRE = loNombre,
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        FECHA_EDICION = Convert.ToDateTime(oProductoEdicion.FECHA_EDICION),
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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
                                        TIPO_PRODUCTO = loTipoProductoDescripcion,
                                        EDICION = oProductoEdicion.EDICION,
                                        PRECIO = oProductoEdicion.PRECIO.ToString(),
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

        private bool GuardarEdicion()
        {
            bool loResutado = false;

            if (lsvDiarios.Items.Count > 0)
            {
                loResutado = ModificarIngresoDiarios();
            }
            else if (lsvRevistas.Items.Count > 0)
            {
                loResutado = ModificarIngresoRevistas();
            }
            else if (lsvColecciones.Items.Count > 0)
            {
                loResutado = ModificarIngresoColecciones();
            }
            else if (lsvLibros.Items.Count > 0)
            {
                loResutado = ModificarIngresoLibros();
            }
            else if (lsvSuplementos.Items.Count > 0)
            {
                loResutado = ModificarIngresoSuplementos();
            }
            else if (lsvPeliculas.Items.Count > 0)
            {
                loResutado = ModificarIngresoPeliculas();
            }

            return loResutado;
        }

        private bool ModificarIngresoDiarios()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<System.DateTime> loFechaDevolucion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvDiarios.Items)
            {
                ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvDiarios.Items)
            {
                // Controlar campos obligatorios: Fecha Edición, Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                        ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                        ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvDiarios.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[7]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición DIARIO
                    if (oProductoEdicion.FECHA_EDICION != Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text))
                    {
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[11]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[13]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        private bool ModificarIngresoRevistas()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<System.DateTime> loFechaDevolucion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvRevistas.Items)
            {
                ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvRevistas.Items)
            {
                // Controlar campos obligatorios: Fecha Edición, Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                        ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvRevistas.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[7]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición REVISTA
                    if (oProductoEdicion.FECHA_EDICION != Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text))
                    {
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[15]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if ((oProductoEdicion.DESCRIPCION == null && !String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text)) || (oProductoEdicion.DESCRIPCION != null && Convert.ToString(oProductoEdicion.DESCRIPCION) != Convert.ToString(((TextBox)loItem.Controls[11]).Text)))
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[11]).Text);
                        else
                            oProductoEdicion.DESCRIPCION = null;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[17]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[17]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        private bool ModificarIngresoColecciones()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<System.DateTime> loFechaDevolucion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvColecciones.Items)
            {
                ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvColecciones.Items)
            {
                // Controlar campos obligatorios: Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                        ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvColecciones.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[7]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición COLECCIÓN
                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[11]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[13]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if ((oProductoEdicion.DESCRIPCION == null && !String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text)) || (oProductoEdicion.DESCRIPCION != null && Convert.ToString(oProductoEdicion.DESCRIPCION) != Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        else
                            oProductoEdicion.DESCRIPCION = null;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        private bool ModificarIngresoLibros()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<DateTime> loFechaDevolucion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvLibros.Items)
            {
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvLibros.Items)
            {
                // Controlar campos obligatorios: Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvLibros.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[9]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición LIBRO
                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[15]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if ((oProductoEdicion.DESCRIPCION == null && !String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text)) || (oProductoEdicion.DESCRIPCION != null && Convert.ToString(oProductoEdicion.DESCRIPCION) != Convert.ToString(((TextBox)loItem.Controls[11]).Text)))
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[11]).Text);
                        else
                            oProductoEdicion.DESCRIPCION = null;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[17]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[17]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        private bool ModificarIngresoSuplementos()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<DateTime> loFechaDevolucion;
            Nullable<DateTime> loFechaEdicion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvSuplementos.Items)
            {
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvSuplementos.Items)
            {
                // Controlar campos obligatorios: Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvSuplementos.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                loFechaEdicion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[7]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición SUPLEMENTO
                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                        loFechaEdicion = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);

                    if ((oProductoEdicion.FECHA_EDICION != loFechaEdicion))
                    {
                        oProductoEdicion.FECHA_EDICION = loFechaEdicion;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[15]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if ((oProductoEdicion.DESCRIPCION == null && !String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text)) || (oProductoEdicion.DESCRIPCION != null && Convert.ToString(oProductoEdicion.DESCRIPCION) != Convert.ToString(((TextBox)loItem.Controls[11]).Text)))
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[11]).Text);
                        else
                            oProductoEdicion.DESCRIPCION = null;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[17]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[17]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        private bool ModificarIngresoPeliculas()
        {
            bool loResutado = false;
            bool loDetalleModificado = false;
            bool loProductoEdicionModificado = false;
            bool loModificado = false;
            Nullable<DateTime> loFechaDevolucion;
            Nullable<DateTime> loFechaEdicion;
            ProductoEdicion oProductoEdicion = null;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            DetalleProductoIngreso oDetalleProductoIngreso = null;

            lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
            lstDetalleProductoIngreso = (List<DetalleProductoIngreso>)Session[Enums.Session.DetalleIngresoProductos.ToString()];

            // Eliminar bordes rojos de las celdas
            foreach (var loItem in lsvPeliculas.Items)
            {
                ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
            }

            foreach (var loItem in lsvPeliculas.Items)
            {
                // Controlar campos obligatorios: Precio, Cantidad de unidades
                if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                        ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[15]).Text))
                        ((TextBox)loItem.Controls[15]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                    return false;
                }
            }

            foreach (var loItem in lsvPeliculas.Items)
            {
                loProductoEdicionModificado = loDetalleModificado = false;
                loFechaDevolucion = null;
                loFechaEdicion = null;
                var loTipoProducto = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToString(((Label)loItem.Controls[5]).Text));

                // Buscar por nro. edición y tipo de producto.
                oProductoEdicion = new ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToString(((Label)loItem.Controls[7]).Text), loTipoProducto.ID_TIPO_PRODUCTO);

                if (oProductoEdicion != null)
                {
                    foreach (var loDetalleProductoIngreso in lstDetalleProductoIngreso)
                    {
                        if (loDetalleProductoIngreso.COD_PRODUCTO_EDICION == oProductoEdicion.ID_PRODUCTO_EDICION)
                        {
                            oDetalleProductoIngreso = new DetalleProductoIngreso();
                            oDetalleProductoIngreso = loDetalleProductoIngreso;
                            break;
                        }
                    }

                    // Modificación de Producto Edición PELÍCULA
                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                        loFechaEdicion = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);

                    if ((oProductoEdicion.FECHA_EDICION != loFechaEdicion))
                    {
                        oProductoEdicion.FECHA_EDICION = loFechaEdicion;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (oProductoEdicion.PRECIO != Convert.ToDouble(((TextBox)loItem.Controls[13]).Text))
                    {
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[13]).Text);
                        loProductoEdicionModificado = loModificado = true;
                    }

                    // Calcular stock y actualizar detalle Producto Ingreso
                    if (oDetalleProductoIngreso.CANTIDAD_UNIDADES != Convert.ToInt32(((TextBox)loItem.Controls[15]).Text))
                    {
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + (Convert.ToInt32(((TextBox)loItem.Controls[15]).Text) - oDetalleProductoIngreso.CANTIDAD_UNIDADES);
                        oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[15]).Text);
                        loProductoEdicionModificado = loDetalleModificado = loModificado = true;
                    }

                    if ((oProductoEdicion.DESCRIPCION == null && !String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text)) || (oProductoEdicion.DESCRIPCION != null && Convert.ToString(oProductoEdicion.DESCRIPCION) != Convert.ToString(((TextBox)loItem.Controls[11]).Text)))
                    {
                        if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[11]).Text);
                        else
                            oProductoEdicion.DESCRIPCION = null;
                        loProductoEdicionModificado = loModificado = true;
                    }

                    if (loProductoEdicionModificado)
                    {
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    if (!String.IsNullOrEmpty(((TextBox)loItem.Controls[17]).Text))
                        loFechaDevolucion = Convert.ToDateTime(((TextBox)loItem.Controls[17]).Text);

                    if ((oDetalleProductoIngreso.FECHA_DEVOLUCION != loFechaDevolucion))
                    {
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = loFechaDevolucion;
                        loDetalleModificado = loModificado = true;
                    }

                    if (loDetalleModificado)
                        loResutado = new ProductoIngresoBLL().ModificarDetalleProductoIngreso(oDetalleProductoIngreso);
                }
            }

            if (!loModificado)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoSinCambios));
            }

            return loResutado;
        }

        #endregion
    }
}