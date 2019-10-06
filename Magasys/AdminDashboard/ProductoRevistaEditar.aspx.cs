using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ProductoRevistaEditar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoRevistaDesdeSession();
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
                var oRevista = CargarRevistaDesdeControles();

                if (oProducto != null && oRevista != null)
                {
                    loResutado = new BLL.RevistaBLL().ModificarRevista(oProducto, oRevista);

                    if (loResutado)
                    {
                        Session.Remove(Enums.Session.Imagen.ToString());
                        Session.Remove(Enums.Session.ProductoRevista.ToString());
                        Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessModificacion, "Modificación Producto Revista", "ProductoListado.aspx"));
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
            Session.Remove(Enums.Session.ProductoRevista.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoRevistaDesdeSession()
        {
            try
            {
                if (Session[Enums.Session.ProductoRevista.ToString()] != null)
                {
                    var oProductoRevista = (BLL.ProductoRevista)Session[Enums.Session.ProductoRevista.ToString()];

                    if (oProductoRevista.ID_PRODUCTO > 0)
                        txtCodigo.Text = oProductoRevista.ID_PRODUCTO.ToString();
                    if (!String.IsNullOrEmpty(oProductoRevista.FECHA_ALTA.ToString()))
                        txtFechaAlta.Text = oProductoRevista.FECHA_ALTA.ToString("dd/MM/yyyy");
                    txtNombre.Text = oProductoRevista.NOMBRE;
                    txtDescripcion.Text = oProductoRevista.DESCRIPCION;
                    CargarProveedor(oProductoRevista.COD_PROVEEDOR);
                    CargarGenero(oProductoRevista.COD_GENERO);
                    CargarDiasDeSemana(oProductoRevista.ID_DIA_SEMANA);
                    CargarPeriodicidades(oProductoRevista.COD_PERIODICIDAD);
                    txtPrecioRevista.Text = oProductoRevista.PRECIO.ToString();

                    if (oProductoRevista.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoRevista.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;
                        txtTitulo.Text = oProductoRevista.IMAGEN.NOMBRE;
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
            if (Session[Enums.Session.ProductoRevista.ToString()] == null)
                return null;

            var oProducto = new BLL.DAL.Producto
            {
                ID_PRODUCTO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).ID_PRODUCTO,
                FECHA_ALTA = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).FECHA_ALTA,
                COD_ESTADO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).COD_ESTADO,
                COD_TIPO_PRODUCTO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).COD_TIPO_PRODUCTO,
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
            else if (imgPreview.ImageUrl != "~/AdminDashboard/img/preview_icons.png" && ((BLL.ProductoRevista)Session[Enums.Session.ProductoRevista.ToString()]).IMAGEN != null) // Mantenemos la imagen
                oProducto.COD_IMAGEN = ((BLL.ProductoRevista)Session[Enums.Session.ProductoRevista.ToString()]).IMAGEN.ID_IMAGEN;

            return oProducto;
        }

        private BLL.DAL.Revista CargarRevistaDesdeControles()
        {
            if (Session[Enums.Session.ProductoRevista.ToString()] == null)
                return null;

            var oRevista = new BLL.DAL.Revista
            {
                COD_PRODUCTO = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).ID_PRODUCTO,
                ID_REVISTA = ((BLL.ProductoRevista)base.Session[Enums.Session.ProductoRevista.ToString()]).ID_REVISTA,
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadRevista.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioRevista.Text)
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaRevista.SelectedValue))
                oRevista.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaRevista.SelectedValue);
            else
                oRevista.ID_DIA_SEMANA = null;

            return oRevista;
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

        private void CargarDiasDeSemana(long? idDiaSemana)
        {
            var oDiaSemana = new BLL.DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaRevista.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaRevista.DataTextField = "NOMBRE";
                ddlDiaDeEntregaRevista.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaRevista.DataBind();
                ddlDiaDeEntregaRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idDiaSemana.HasValue)
                    ddlDiaDeEntregaRevista.SelectedValue = idDiaSemana.ToString();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarPeriodicidades(long idPeriodicidad)
        {
            var oPeriodicidad = new BLL.PeriodicidadBLL();

            try
            {
                var lstPeriodicidades = oPeriodicidad.ObtenerPeriodicidades();

                ddlPeriodicidadRevista.DataSource = lstPeriodicidades;
                ddlPeriodicidadRevista.DataTextField = "NOMBRE";
                ddlPeriodicidadRevista.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadRevista.DataBind();
                ddlPeriodicidadRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                if (idPeriodicidad == 0)
                    ddlPeriodicidadRevista.SelectedIndex = 0;
                else
                {
                    ddlPeriodicidadRevista.SelectedValue = idPeriodicidad.ToString();
                }
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