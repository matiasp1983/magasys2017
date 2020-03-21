﻿using BLL.Common;
using NLog;
using System;

namespace PL.AdminDashboard
{
    public partial class ProductoLibroVisualizar : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProductoLibro();
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoLibroEditar.aspx", false);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.ProductoLibro.ToString());
            Response.Redirect("ProductoListado.aspx", false);
        }

        #endregion

        #region Métodos Privados

        private void CargarProductoLibro()
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
                    var loProveedor = new BLL.ProveedorBLL().ObtenerProveedor(oProductoLibro.COD_PROVEEDOR);
                    if (loProveedor != null)
                        txtProveedor.Text = loProveedor.RAZON_SOCIAL;
                    var loGenero = new BLL.GeneroBLL().ObtenerGenero(oProductoLibro.COD_GENERO);
                    if (loGenero != null)
                        txtGenero.Text = loGenero.NOMBRE;
                    txtAutorLibro.Text = oProductoLibro.AUTOR;
                    txtAnioEdicionLibro.Text = oProductoLibro.ANIO.ToString();
                    txtEditorialLibro.Text = oProductoLibro.EDITORIAL;
                    txtPrecioLibro.Text = oProductoLibro.PRECIO.ToString();

                    if (oProductoLibro.IMAGEN != null)
                    {
                        // Covertir la iamgen a un base 64 para mostrarlo en un dato binario
                        string loImagenDataURL64 = "data:image/jpg;base64," + Convert.ToBase64String(oProductoLibro.IMAGEN.IMAGEN1);
                        imgPreview.ImageUrl = loImagenDataURL64;                        
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

        #endregion
    }
}