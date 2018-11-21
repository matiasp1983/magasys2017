using System;
using System.Collections.Generic;
using BLL.Common;
using BLL.DAL;

namespace PL.AdminDashboard
{
    public partial class DetalleProductoDevoluciones : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDetalleDevolucion();
        }

        #endregion

        #region Métodos Privados

        private void CargarDetalleDevolucion()
        {
            List<BLL.DetalleProductoDevolucionListado> lstDetalleProductoDevolucionListado = null;
            BLL.DetalleProductoDevolucionListado oDetalleProductoDevolucionListado = null;

            lsvDetalleDevolucion.DataSource = null;
            lsvDetalleDevolucion.Visible = false;

            if (Convert.ToInt32(Session[Enums.Session.IdProductoDevolucion.ToString()]) > 0)
            {
                using (var loRepProductoDevolucion = new Repository<BLL.DAL.ProductoDevolucion>())
                {
                    var loIdProductoDevolucion = Convert.ToInt32(Session[Enums.Session.IdProductoDevolucion.ToString()]);

                    var loProductoDevolucion = loRepProductoDevolucion.Find(p => p.COD_ESTADO == 1 && p.ID_PRODUCTO_DEVOLUCION == loIdProductoDevolucion);

                    if (loProductoDevolucion != null)
                    {
                        lstDetalleProductoDevolucionListado = new List<BLL.DetalleProductoDevolucionListado>();

                        foreach (var loDetalleProductoDevolucion in loProductoDevolucion.DetalleProductoDevolucion)
                        {
                            oDetalleProductoDevolucionListado = new BLL.DetalleProductoDevolucionListado
                            {
                                ID_PRODUCTO_DEVOLUCION = loDetalleProductoDevolucion.COD_PRODUCTO_DEVOLUCION,
                                COD_EDICION = loDetalleProductoDevolucion.COD_PRODUCTO_EDICION,
                                EDICION = loDetalleProductoDevolucion.ProductoEdicion.EDICION,
                                TIPO_PRODUCTO = loDetalleProductoDevolucion.ProductoEdicion.Producto.TipoProducto.DESCRIPCION,
                                NOMBRE_PRODUCTO = loDetalleProductoDevolucion.ProductoEdicion.Producto.NOMBRE,
                                CANTIDAD = loDetalleProductoDevolucion.CANTIDAD
                            };

                            lstDetalleProductoDevolucionListado.Add(oDetalleProductoDevolucionListado);
                        }

                        if (lstDetalleProductoDevolucionListado.Count > 0)
                        {
                            lsvDetalleDevolucion.Visible = true;
                            lsvDetalleDevolucion.DataSource = lstDetalleProductoDevolucionListado;
                            lsvDetalleDevolucion.DataBind();
                        }
                    }
                }
            }

            Session.Remove(Enums.Session.IdProductoDevolucion.ToString());
        }

        #endregion
    }
}