﻿using BLL.Common;
using NLog;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoLibroEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoLibroDesdeSession();
        }

        protected void BtnSubir_Click(object sender, EventArgs e)
        {
            // Obtener tamaño de la imagen seleccionada
            int loTamanioImagen = fuploadImagen.PostedFile.ContentLength;

            if (loTamanioImagen == 0)
                return;

            // Obtener tamaño de la imagen en byte
            byte[] loImagenOriginal = new byte[loTamanioImagen];

            //// Asociar byte a imagen
            fuploadImagen.PostedFile.InputStream.Read(loImagenOriginal, 0, loTamanioImagen);

            var oImagen = new BLL.DAL.Imagen
            {
                IMAGEN1 = loImagenOriginal,
                NOMBRE = txtTitulo.Text
            };

            Session.Add(Enums.Session.Imagen.ToString(), oImagen);

            // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
            string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(loImagenOriginal);
            imgPreview.ImageUrl = loImagenDataURL64;
        }

        protected void BtnLimpiarImagen_Click(object sender, EventArgs e)
        {
            imgPreview.ImageUrl = "~/AdminDashboard/img/preview_icons.png";
            txtTitulo.Text = String.Empty;
            Session.Remove(Enums.Session.Imagen.ToString());
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;

                var oProducto = CargarProductoDesdeControles();
                var oLibro = CargarLibroDesdeControles();

                if (oProducto != null && oLibro != null)
                {
                    loResutado = new BLL.LibroBLL().ModificarLibro(oProducto, oLibro);

                    if (loResutado)
                    {
                        Session.Remove(Enums.Session.Imagen.ToString());
                        Session.Remove(Enums.Session.ProductoLibro.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Libro", "ProductoListado.aspx"));
                    }
                    else
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
                }
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));

                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Imagen.ToString());
            Session.Remove(Enums.Session.ProductoLibro.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoLibroDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoLibro.ToString()] != null)
                {
                    var oProductoLibro = (BLL.ProductoLibro)Session[Enums.Session.ProductoLibro.ToString()];

                    if (oProductoLibro.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoLibro.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoLibro.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoLibro.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoLibro.NOMBRE;
                    txtDescripcion.Text = oProductoLibro.DESCRIPCION;
                    CargarProveedor(oProductoLibro.COD_PROVEEDOR);
                    CargarGenero(oProductoLibro.COD_GENERO);
                    txtAutorLibro.Text = oProductoLibro.AUTOR;
                    CargarAnio(oProductoLibro.ANIO);
                    txtEditorialLibro.Text = oProductoLibro.EDITORIAL;
                    txtPrecioLibro.Text = oProductoLibro.PRECIO.ToString();

                    if (oProductoLibro.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoLibro.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                        txtTitulo.Text = oProductoLibro.IMAGEN.NOMBRE;
                    }
                }
                else
                    Response.Redirect("ProductoListado.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private BLL.DAL.Producto CargarProductoDesdeControles()
        {
            if (Session[Enums.Session.ProductoLibro.ToString()] == null)
                return null;

            var oProducto = new BLL.DAL.Producto
            {
                ID_PRODUCTO = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).ID_PRODUCTO,
                FECHA_ALTA = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).FECHA_ALTA,
                COD_ESTADO = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).COD_ESTADO,
                COD_TIPO_PRODUCTO = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).COD_TIPO_PRODUCTO,
                NOMBRE = txtNombre.Text,
                COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue),
                COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue)
            };

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;

            // Cargar Imagen
            if ((BLL.DAL.Imagen)Session[Enums.Session.Imagen.ToString()] != null)  // Nueva Imagen
                oProducto.Imagen = (BLL.DAL.Imagen)Session[Enums.Session.Imagen.ToString()];
            else if (imgPreview.ImageUrl != "~/AdminDashboard/img/preview_icons.png" && ((BLL.ProductoLibro)Session[Enums.Session.ProductoLibro.ToString()]).IMAGEN != null) // Mantenemos la imagen
                oProducto.COD_IMAGEN = ((BLL.ProductoLibro)Session[Enums.Session.ProductoLibro.ToString()]).IMAGEN.ID_IMAGEN;

            return oProducto;
        }

        private BLL.DAL.Libro CargarLibroDesdeControles()
        {
            if (Session[Enums.Session.ProductoLibro.ToString()] == null)
                return null;

            var oLibro = new BLL.DAL.Libro
            {
                COD_PRODUCTO = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).ID_PRODUCTO,
                ID_LIBRO = ((BLL.ProductoLibro)base.Session[Enums.Session.ProductoLibro.ToString()]).ID_LIBRO,
                EDITORIAL = txtEditorialLibro.Text,
                AUTOR = txtAutorLibro.Text,
                ANIO = Convert.ToInt32(ddlAnioEdicionLibro.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioLibro.Text)
            };

            return oLibro;
        }

        private void CargarProveedor(long idProveedor)
        {
            var oProveedor = new BLL.ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idProveedor == 0)
                    ddlProveedor.SelectedIndex = 0;
                else
                {
                    ddlProveedor.SelectedValue = idProveedor.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarGenero(long idGenero)
        {
            var oGenero = new BLL.GeneroBLL();

            try
            {
                ddlGenero.DataSource = oGenero.ObtenerGeneros();
                ddlGenero.DataTextField = "NOMBRE";
                ddlGenero.DataValueField = "ID_GENERO";
                ddlGenero.DataBind();
                ddlGenero.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idGenero == 0)
                    ddlGenero.SelectedIndex = 0;
                else
                {
                    ddlGenero.SelectedValue = idGenero.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarAnio(int anio)
        {
            var oAnio = new BLL.AnioBLL();

            try
            {
                ddlAnioEdicionLibro.DataSource = oAnio.ObtenerAnios();
                ddlAnioEdicionLibro.DataTextField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataValueField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataBind();
                ddlAnioEdicionLibro.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlAnioEdicionLibro.SelectedValue = anio.ToString();

            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion
    }
}