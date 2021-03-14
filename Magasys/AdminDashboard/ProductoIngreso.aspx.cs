using System;
using System.Web.UI.WebControls;
using BLL.Common;
using BLL.Filters;
using NLog;
using System.Linq;
using BLL.DAL;
using System.Collections.Generic;

namespace PL.AdminDashboard
{
    public partial class ProductoIngreso : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarTiposProducto();
                CargarProveedores();
                btnGuardar.Visible = false;
                btnCancelar.Visible = false;
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            lsvDiarios.DataSource = lsvRevistas.DataSource = lsvColecciones.DataSource = lsvLibros.DataSource = lsvSuplementos.DataSource = lsvPeliculas.DataSource = null;

            lsvDiarios.DataBind();
            lsvRevistas.DataBind();
            lsvColecciones.DataBind();
            lsvLibros.DataBind();
            lsvSuplementos.DataBind();
            lsvPeliculas.DataBind();

            CargarGrillaProductos();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Producto.aspx", false);
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (GuardarEdicion())
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoIngresoSuccessAlta, "Alta de Ingreso de productos"));
                    LimpiarCampos();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoFailure));
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
            LimpiarCampos();
        }

        #endregion

        #region Métodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvProductos.Visible = false;
        }

        private void CargarTiposProducto()
        {
            var oTipoProducto = new BLL.TipoProductoBLL();

            try
            {
                ddlTipoProducto.DataSource = oTipoProducto.ObtenerTiposProducto();
                ddlTipoProducto.DataTextField = "DESCRIPCION";
                ddlTipoProducto.DataValueField = "ID_TIPO_PRODUCTO";
                ddlTipoProducto.DataBind();
                ddlTipoProducto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarProveedores()
        {
            var oProveedor = new BLL.ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private ProductoFiltro CargarProductoFiltro()
        {
            var oProductoFiltro = new ProductoFiltro();

            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
                oProductoFiltro.CodTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);

            if (!String.IsNullOrEmpty(ddlProveedor.SelectedValue))
                oProductoFiltro.CodProveedor = Convert.ToInt32(ddlProveedor.SelectedValue);

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oProductoFiltro.NombreProducto = txtNombre.Text;

            return oProductoFiltro;
        }

        private void CargarGrillaProductos()
        {
            try
            {
                var oProductoFiltro = CargarProductoFiltro();

                switch (oProductoFiltro.CodTipoProducto)
                {
                    case 1:
                        lsvDiarios.Visible = true;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        bool bIngresoDiario = false;
                        if (chkIngresoProductoDeOtroDia.Checked) bIngresoDiario = true;
                        var lstDiarios = new BLL.DiarioBLL().ObtenerDiariosParaEdicion(oProductoFiltro, bIngresoDiario);

                        if (lstDiarios != null && lstDiarios.Count > 0)
                        {
                            lsvDiarios.DataSource = lstDiarios;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true; ;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvDiarios.DataBind();
                        break;

                    case 2:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = true;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstRevistas = new BLL.RevistaBLL().ObtenerRevistasParaEdicion(oProductoFiltro);
                        if (lstRevistas != null && lstRevistas.Count > 0)
                        {
                            lsvRevistas.DataSource = lstRevistas;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvRevistas.DataBind();
                        break;

                    case 3:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = true;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstColecciones = new BLL.ColeccionBLL().ObtenerColeccionesParaEdicion(oProductoFiltro);
                        if (lstColecciones != null && lstColecciones.Count > 0)
                        {
                            lsvColecciones.DataSource = lstColecciones;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvColecciones.DataBind();
                        break;

                    case 4:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = true;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = false;

                        var lstLibros = new BLL.LibroBLL().ObtenerLibrosParaEdicion(oProductoFiltro);
                        if (lstLibros != null && lstLibros.Count > 0)
                        {
                            lsvLibros.DataSource = lstLibros;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvLibros.DataBind();
                        break;

                    case 5:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = true;
                        lsvPeliculas.Visible = false;

                        bool bIngresoSuplemento = false;
                        if (chkIngresoProductoDeOtroDia.Checked) bIngresoSuplemento = true;
                        var lstSuplementos = new BLL.SuplementoBLL().ObtenerSuplementosParaEdicion(oProductoFiltro, bIngresoSuplemento);
                        if (lstSuplementos != null && lstSuplementos.Count > 0)
                        {
                            lsvSuplementos.DataSource = lstSuplementos;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvSuplementos.DataBind();
                        break;

                    case 6:
                        lsvDiarios.Visible = false;
                        lsvRevistas.Visible = false;
                        lsvColecciones.Visible = false;
                        lsvLibros.Visible = false;
                        lsvSuplementos.Visible = false;
                        lsvPeliculas.Visible = true;

                        var lstPeliculas = new BLL.PeliculaBLL().ObtenerPeliculasParaEdicion(oProductoFiltro);
                        if (lstPeliculas != null && lstPeliculas.Count > 0)
                        {
                            lsvPeliculas.DataSource = lstPeliculas;
                            btnGuardar.Visible = true;
                            btnCancelar.Visible = true;
                        }
                        else
                        {
                            dvMensajeLsvProductos.InnerHtml = MessageManager.Info(dvMensajeLsvProductos, Message.MsjeListadoProductoFiltrarTotalSinResultados, false);
                            dvMensajeLsvProductos.Visible = true;
                        }

                        lsvPeliculas.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                lsvDiarios.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private bool GuardarEdicion()
        {
            bool loResutado = false;

            switch (ddlTipoProducto.SelectedValue)
            {
                case "1":
                    // Diario
                    loResutado = ActualizarIngresoDiarios();
                    break;

                case "2":
                    // Revista
                    loResutado = ActualizarIngresoRevistas();
                    break;

                case "3":
                    // Colección
                    loResutado = ActualizarIngresoColecciones();
                    break;

                case "4":
                    // Libro
                    loResutado = ActualizarIngresoLibros();
                    break;

                case "5":
                    // Suplemento
                    loResutado = ActualizarIngresoSuplementos();
                    break;

                default:
                    // Película
                    loResutado = ActualizarIngresoPeliculas();
                    break;
            }
            return loResutado;
        }

        private bool ActualizarIngresoDiarios()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvDiarios.Items)
                {
                    ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvDiarios.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente              
                        continue;
                    }

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

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();
                foreach (var loItem in lsvDiarios.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[17]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[17]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[7]).Text);
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[9]).Text);
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva != null)
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[15]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();
                if (lstReservasConfirmar.Any())
                {
                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);
                    Response.Redirect("ReservaEdicion.aspx", false);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private bool ActualizarIngresoRevistas()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvRevistas.Items)
                {
                    ((TextBox)loItem.Controls[7]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvRevistas.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente
                        continue;
                    }

                    // Controlar campos obligatorios: Fecha Edición, Precio, Cantidad de unidades
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                    {
                        if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text))
                            ((TextBox)loItem.Controls[7]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                        if (String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                        if (String.IsNullOrEmpty(((TextBox)loItem.Controls[13]).Text))
                            ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                        return false;
                    }

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();

                foreach (var loItem in lsvRevistas.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[5]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[17]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[17]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[5]).Text);
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);

                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva.Any())
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[15]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();

                if (lstReservasConfirmar.Any())
                {

                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);

                    Response.Redirect("ReservaEdicion.aspx", false);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private bool ActualizarIngresoColecciones()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvColecciones.Items)
                {
                    ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvColecciones.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente
                        continue;
                    }

                    // Controlar campos obligatorios: Precio, Cantidad de unidades
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text) || String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                    {
                        if (String.IsNullOrEmpty(((TextBox)loItem.Controls[9]).Text))
                            ((TextBox)loItem.Controls[9]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                        if (String.IsNullOrEmpty(((TextBox)loItem.Controls[11]).Text))
                            ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#cc5965");

                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoIngresoCampObligatorio));
                        return false;
                    }

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();

                foreach (var loItem in lsvColecciones.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[5]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[15]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[15]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[5]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[9]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[11]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[7]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[9]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[11]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[7]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva.Any())
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[11]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[13]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[13]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();
                if (lstReservasConfirmar.Any())
                {

                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);
                    Response.Redirect("ReservaEdicion.aspx", false);
                }


            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private bool ActualizarIngresoLibros()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvLibros.Items)
                {
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvLibros.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente
                        continue;
                    }

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

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();

                foreach (var loItem in lsvLibros.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[7]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[17]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[17]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[7]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva.Any())
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[15]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();
                if (lstReservasConfirmar.Any())
                {

                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);
                    Response.Redirect("ReservaEdicion.aspx", false);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private bool ActualizarIngresoSuplementos()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvSuplementos.Items)
                {
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvSuplementos.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente
                        continue;
                    }

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

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();

                foreach (var loItem in lsvSuplementos.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[5]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[17]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[17]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[5]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva.Any())
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[15]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();
                if (lstReservasConfirmar.Any())
                {

                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);
                    Response.Redirect("ReservaEdicion.aspx", false);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private bool ActualizarIngresoPeliculas()
        {
            bool loResutado = false;
            bool loGrabarEdicion = false;
            List<DetalleProductoIngreso> lstDetalleProductoIngreso = null;
            List<BLL.ReservaClienteListado> lstReservasConfirmar = new List<BLL.ReservaClienteListado>();
            ProductoEdicion oProductoEdicion = null;
            BLL.DAL.ProductoIngreso oProductoIngreso = null;
            BLL.DAL.Imagen oImagen = null;

            try
            {
                // Eliminar bordes rojos de las celdas
                foreach (var loItem in lsvPeliculas.Items)
                {
                    ((TextBox)loItem.Controls[11]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                    ((TextBox)loItem.Controls[13]).BorderColor = System.Drawing.ColorTranslator.FromHtml("#e5e6e7");
                }

                foreach (var loItem in lsvPeliculas.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente
                        continue;
                    }

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

                    loGrabarEdicion = true;
                }

                if (!loGrabarEdicion)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeListadoProductoIngresoCargarProducto));
                    return false;
                }

                lstDetalleProductoIngreso = new List<DetalleProductoIngreso>();

                foreach (var loItem in lsvPeliculas.Items)
                {
                    if (String.IsNullOrEmpty(((TextBox)loItem.Controls[5]).Text))
                    {   // Si el nro. de edición viene vació no se procesa el registro y se continúa con el siguiente.
                        continue;
                    }

                    var oDetalleProductoIngreso = new DetalleProductoIngreso();

                    // Buscar nro. edición, si existe se actualiza la cantidad y los datos que se carguen, caso contrario, se crea la edición.
                    oProductoEdicion = new BLL.ProductoEdicionBLL().ConsultarExistenciaEdicion(Convert.ToInt32(((Label)loItem.Controls[1]).Text), Convert.ToString(((TextBox)loItem.Controls[5]).Text), Convert.ToInt32(ddlTipoProducto.SelectedValue));

                    // Obtener tamaño de la IMAGEN seleccionada
                    int loTamanioImagen = ((FileUpload)loItem.Controls[17]).PostedFile.ContentLength;

                    if (loTamanioImagen > 0)
                    {
                        // Obtener tamaño de la IMAGEN en byte
                        byte[] loImagenOriginal = new byte[loTamanioImagen];

                        //// Asociar byte a IMAGEN
                        ((FileUpload)loItem.Controls[17]).PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

                        oImagen = new Imagen();
                        oImagen.IMAGEN1 = loImagenOriginal;
                    }

                    if (oProductoEdicion == null)
                    {
                        // Alta de Producto Edición
                        oProductoEdicion = new ProductoEdicion();
                        oProductoEdicion.COD_PRODUCTO = Convert.ToInt32(((Label)loItem.Controls[1]).Text);
                        oProductoEdicion.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                        oProductoEdicion.EDICION = Convert.ToString(((TextBox)loItem.Controls[5]).Text);
                        oProductoEdicion.COD_ESTADO = 1;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = new BLL.ProductoEdicionBLL().AltaProductoEdicion(oProductoEdicion);
                        if (oDetalleProductoIngreso.COD_PRODUCTO_EDICION == 0)
                        {
                            loResutado = false;
                            return loResutado;
                        }
                    }
                    else
                    {
                        // Modificación de Producto Edición
                        oDetalleProductoIngreso.COD_PRODUCTO_EDICION = oProductoEdicion.ID_PRODUCTO_EDICION;
                        oProductoEdicion.PRECIO = Convert.ToDouble(((TextBox)loItem.Controls[11]).Text);
                        oProductoEdicion.CANTIDAD_DISPONIBLE = oProductoEdicion.CANTIDAD_DISPONIBLE + Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[7]).Text)))
                            oProductoEdicion.FECHA_EDICION = Convert.ToDateTime(((TextBox)loItem.Controls[7]).Text);
                        if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[9]).Text)))
                            oProductoEdicion.DESCRIPCION = Convert.ToString(((TextBox)loItem.Controls[9]).Text);
                        if (oImagen != null) // Asociar de la Imagen
                            oProductoEdicion.Imagen = oImagen;
                        loResutado = new BLL.ProductoEdicionBLL().ModificarProductoEdicion(oProductoEdicion);
                        if (loResutado == false)
                            return loResutado;
                    }

                    // Consultar si existen reservas para ese producto

                    var oReservaFiltro = CargarReservaFiltro(Convert.ToString(((Label)loItem.Controls[3]).Text), Convert.ToString(((TextBox)loItem.Controls[7]).Text));
                    var lstReserva = new BLL.ReservaBLL().ObtenerReservaEdicionPorProducto(oReservaFiltro);
                    if (lstReserva.Any())
                    {
                        // Agregar los resultados en una lista con TODAS las reservas porque asi como está borra los anteriores

                        foreach (var loReserva in lstReserva)
                        {
                            BLL.ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                            {
                                ID_RESERVA = loReserva.ID_RESERVA,
                                PRODUCTO = loReserva.NOMBRE_PRODUCTO,
                                CLIENTE = loReserva.NOMBRE_CLIENTE,
                                CODPRODUCTOEDICION = oProductoEdicion.ID_PRODUCTO_EDICION
                            };

                            lstReservasConfirmar.Add(oReservaEdicion);
                        }
                    }

                    oDetalleProductoIngreso.CANTIDAD_UNIDADES = Convert.ToInt32(((TextBox)loItem.Controls[13]).Text);
                    if (!String.IsNullOrEmpty(Convert.ToString(((TextBox)loItem.Controls[15]).Text)))
                        oDetalleProductoIngreso.FECHA_DEVOLUCION = Convert.ToDateTime(((TextBox)loItem.Controls[15]).Text);
                    lstDetalleProductoIngreso.Add(oDetalleProductoIngreso);
                }

                if (lstDetalleProductoIngreso.Count > 0)
                {
                    oProductoIngreso = new BLL.DAL.ProductoIngreso();
                    oProductoIngreso.FECHA = DateTime.Now;
                    oProductoIngreso.COD_ESTADO = 1;
                    oProductoIngreso.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
                    oProductoIngreso.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                    loResutado = new BLL.ProductoIngresoBLL().AltaIngreso(oProductoIngreso, lstDetalleProductoIngreso);
                }

                // Llamar pantalla para Confirmar las reservas

                ListView lsvReservas = new ListView();
                if (lstReservasConfirmar.Any())
                {

                    lsvReservas.DataSource = lstReservasConfirmar;
                    Session.Add(Enums.Session.ListadoReservaConfirmar.ToString(), lsvReservas);
                    Response.Redirect("ReservaEdicion.aspx", false);
                }

            }
            catch (Exception)
            {
                throw;
            }

            return loResutado;
        }

        private void LimpiarCampos()
        {
            FormProductoIngreso.Controls.OfType<DropDownList>().ToList().ForEach(x => x.SelectedIndex = -1);
            txtNombre.Text = String.Empty;
            lsvDiarios.Visible = false;
            lsvRevistas.Visible = false;
            lsvColecciones.Visible = false;
            lsvLibros.Visible = false;
            lsvSuplementos.Visible = false;
            lsvPeliculas.Visible = false;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;
            chkIngresoProductoDeOtroDia.Checked = false;
        }

        private ReservaFiltro CargarReservaFiltro(String nombre, String edicion)
        {
            ReservaFiltro oReservaFiltro = new ReservaFiltro();

            oReservaFiltro.NOMBRE_PRODUCTO = nombre;
            oReservaFiltro.COD_ESTADO = 7;
            oReservaFiltro.COD_TIPO_RESERVA = 2;
            oReservaFiltro.COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue);
            oReservaFiltro.COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue);
            oReservaFiltro.EDICION = edicion;

            return oReservaFiltro;
        }

        private List<BLL.ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            bool loResultado = false;

            List<BLL.ReservaClienteListado> lstReservas = (List<BLL.ReservaClienteListado>)pListView.DataSource;

            if (loResultado)
                lstReservas = null;

            return lstReservas;
        }

        #endregion        
    }
}