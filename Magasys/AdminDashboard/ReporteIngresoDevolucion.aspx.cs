using BLL;
using BLL.DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReporteIngresoDevolucion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTiposProducto();
                CargarProducto();
            }
        }

        protected void TipoProductoSeleccionado(object sender, EventArgs e)
        {
            CargarProducto();
        }

        #endregion

        #region Métodos Privados

        private void CargarTiposProducto()
        {
            var oTipoProducto = new TipoProductoBLL();

            try
            {
                ddlTipoProducto.DataSource = oTipoProducto.ObtenerTiposProducto();
                ddlTipoProducto.DataTextField = "DESCRIPCION";
                ddlTipoProducto.DataValueField = "ID_TIPO_PRODUCTO";
                ddlTipoProducto.DataBind();
                ddlTipoProducto.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarProducto()
        {
            if (!String.IsNullOrEmpty(ddlTipoProducto.SelectedValue))
            {
                int codTipoProducto = Convert.ToInt32(ddlTipoProducto.SelectedValue);
                ddlProducto.DataSource = new ProductoBLL().ObtenerProductosXTipoProducto(codTipoProducto);
                ddlProducto.DataTextField = "NOMBRE";
                ddlProducto.DataValueField = "ID_PRODUCTO";
                ddlProducto.DataBind();
                ddlProducto.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
        }

        #endregion

        [WebMethod]
        public static List<object> ObtenerRatioIngresosDevolucionesPorFiltro(string[] pProductos, string pTipoProducto, string pFechaDesde, string pFechaHasta)
        {
            List<object> chartData = new List<object>();
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<ValorRatio> lstDatos = null;
            var loProductos = string.Empty;
            var loCodEdiciones = string.Empty;
            int codProducto = 0;
            int contRatio = 0;
            double acumRatio = 0;
            double valorRatio = 0;
            int contador = 0;
            int contadorDevoluciones = 0;
            int codProductoAux = 0;
            string nombreProductoAux = string.Empty;
            string DescripProductoAux = string.Empty;

            if (!string.IsNullOrEmpty(pTipoProducto) && !String.IsNullOrEmpty(pFechaDesde) && !String.IsNullOrEmpty(pFechaHasta))
            {
                fechaDesde = Convert.ToDateTime(pFechaDesde);
                fechaHasta = Convert.ToDateTime(pFechaHasta).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                foreach (var item in pProductos.ToList())
                {
                    if (!String.IsNullOrEmpty(item))
                        loProductos += string.Format("{0},", item);
                }

                if (!String.IsNullOrEmpty(loProductos))
                    loProductos = loProductos.Remove(loProductos.Length - 1);

                var lstIngresos = new ProductoBLL().ObtenerIngresosPorTipoProducto(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstIngresos.Count > 0) // VER SI MOSTRAR MENSAJE CUANDO NO HAY DATOS PARA MOSTRAR .....
                {
                    foreach (var item in lstIngresos)
                    {
                        loCodEdiciones += string.Format("{0},", item.ID_PRODUCTO_EDICION);
                    }

                    if (!String.IsNullOrEmpty(loCodEdiciones))
                        loCodEdiciones = loCodEdiciones.Remove(loCodEdiciones.Length - 1);

                    var lstDevoluciones = new ProductoBLL().ObtenerDevolucionesPorTipoProducto(loCodEdiciones, fechaDesde);

                    if (lstDevoluciones.Count > 0)
                    {
                        lstDatos = new List<ValorRatio>();

                        foreach (var item in lstDevoluciones)
                        {
                            if (codProducto != item.COD_PRODUCTO)
                            {
                                codProducto = item.COD_PRODUCTO;

                                if (acumRatio > 0)
                                {
                                    ValorRatio oDatos = new ValorRatio();
                                    oDatos.VALOR = Math.Round((double)acumRatio / contRatio, 2);
                                    oDatos.COD_PRODUCTO = codProductoAux;
                                    if (pTipoProducto == "1")
                                        oDatos.NOMBRE = $"{ nombreProductoAux } - { DescripProductoAux }";
                                    else
                                        oDatos.NOMBRE = nombreProductoAux;

                                    lstDatos.Add(oDatos);
                                    acumRatio = 0;
                                    contRatio = 0;
                                }

                                codProductoAux = item.COD_PRODUCTO;
                                nombreProductoAux = item.NOMBRE;
                                DescripProductoAux = item.DESCRIPCION;
                            }

                            var ingresoProducto = lstIngresos.Find(p => p.ID_PRODUCTO_EDICION == item.ID_PRODUCTO_EDICION);
                            valorRatio = Math.Round((double)Convert.ToInt32(item.CANTIDAD) / Convert.ToInt32(ingresoProducto.CANTIDAD), 2);
                            acumRatio = acumRatio + valorRatio;
                            contRatio++;
                            contadorDevoluciones++;
                            if (contadorDevoluciones == lstDevoluciones.Count && acumRatio > 0)
                            {
                                ValorRatio oDatos = new ValorRatio();
                                oDatos.VALOR = Math.Round((double)acumRatio / contRatio, 2);
                                oDatos.COD_PRODUCTO = item.COD_PRODUCTO;
                                if (pTipoProducto == "1")
                                    oDatos.NOMBRE = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                                else
                                    oDatos.NOMBRE = item.NOMBRE;
                                lstDatos.Add(oDatos);
                            }
                        }

                        if (lstDatos.Count > 0)
                        {
                            lstDatos = lstDatos.OrderBy(p => p.VALOR).ToList();
                            chartData.Add(new object[2]);
                            ((object[])chartData[0])[0] = "Productos";
                            ((object[])chartData[0])[1] = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToInt32(pTipoProducto)).DESCRIPCION;

                            foreach (var item in lstDatos)
                            {
                                contador++;
                                chartData.Add(new object[2]);
                                ((object[])chartData[contador])[0] = item.NOMBRE;
                                ((object[])chartData[contador])[1] = item.VALOR;
                            }
                        }
                    }
                }
            }
            return chartData;
        }

        [WebMethod]
        public static List<object> ObtenerRatioIngresosDevoluciones(string[] pProductos, string pTipoProducto, string pOperacion)
        {
            List<object> chartData = new List<object>();
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            List<ValorRatio> lstDatos = null;
            var loProductos = string.Empty;
            var loCodEdiciones = string.Empty;
            int codProducto = 0;
            int contRatio = 0;
            double acumRatio = 0;
            double valorRatio = 0;
            int contador = 0;
            int contadorDevoluciones = 0;
            int codProductoAux = 0;
            string nombreProductoAux = string.Empty;
            string DescripProductoAux = string.Empty;

            if (!string.IsNullOrEmpty(pTipoProducto) && !String.IsNullOrEmpty(pOperacion))
            {

                switch (pOperacion)
                {
                    case "7dias":
                        fechaDesde = DateTime.Today.AddDays(-7);
                        break;
                    case "EsteMes":
                        fechaDesde = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                        break;
                    case "30dias":
                        fechaDesde = DateTime.Today.AddDays(-30);
                        break;
                    case "EsteAnio":
                        fechaDesde = new DateTime(DateTime.Now.Year, 1, 1);
                        break;
                }

                foreach (var item in pProductos.ToList())
                {
                    if (!String.IsNullOrEmpty(item))
                        loProductos += string.Format("{0},", item);
                }

                if (!String.IsNullOrEmpty(loProductos))
                    loProductos = loProductos.Remove(loProductos.Length - 1);

                var lstIngresos = new ProductoBLL().ObtenerIngresosPorTipoProducto(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstIngresos.Count > 0) // VER SI MOSTRAR MENSAJE CUANDO NO HAY DATOS PARA MOSTRAR .....
                {
                    foreach (var item in lstIngresos)
                    {
                        loCodEdiciones += string.Format("{0},", item.ID_PRODUCTO_EDICION);
                    }

                    if (!String.IsNullOrEmpty(loCodEdiciones))
                        loCodEdiciones = loCodEdiciones.Remove(loCodEdiciones.Length - 1);

                    var lstDevoluciones = new ProductoBLL().ObtenerDevolucionesPorTipoProducto(loCodEdiciones, fechaDesde);

                    if (lstDevoluciones.Count > 0)
                    {
                        lstDatos = new List<ValorRatio>();

                        foreach (var item in lstDevoluciones)
                        {
                            if (codProducto != item.COD_PRODUCTO)
                            {
                                codProducto = item.COD_PRODUCTO;

                                if (acumRatio > 0)
                                {
                                    ValorRatio oDatos = new ValorRatio();
                                    oDatos.VALOR = Math.Round((double)acumRatio / contRatio, 2);
                                    oDatos.COD_PRODUCTO = codProductoAux;
                                    if (pTipoProducto == "1")
                                        oDatos.NOMBRE = $"{ nombreProductoAux } - { DescripProductoAux }";
                                    else
                                        oDatos.NOMBRE = nombreProductoAux;

                                    lstDatos.Add(oDatos);
                                    acumRatio = 0;
                                    contRatio = 0;
                                }

                                codProductoAux = item.COD_PRODUCTO;
                                nombreProductoAux = item.NOMBRE;
                                DescripProductoAux = item.DESCRIPCION;
                            }

                            var ingresoProducto = lstIngresos.Find(p => p.ID_PRODUCTO_EDICION == item.ID_PRODUCTO_EDICION);
                            valorRatio = Math.Round((double)Convert.ToInt32(item.CANTIDAD) / Convert.ToInt32(ingresoProducto.CANTIDAD), 2);
                            acumRatio = acumRatio + valorRatio;
                            contRatio++;
                            contadorDevoluciones++;
                            if (contadorDevoluciones == lstDevoluciones.Count && acumRatio > 0)
                            {
                                ValorRatio oDatos = new ValorRatio();
                                oDatos.VALOR = Math.Round((double)acumRatio / contRatio, 2);
                                oDatos.COD_PRODUCTO = item.COD_PRODUCTO;
                                if (pTipoProducto == "1")
                                    oDatos.NOMBRE = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                                else
                                    oDatos.NOMBRE = item.NOMBRE;
                                lstDatos.Add(oDatos);
                            }
                        }

                        if (lstDatos.Count > 0)
                        {
                            lstDatos = lstDatos.OrderBy(p => p.VALOR).ToList();
                            chartData.Add(new object[2]);
                            ((object[])chartData[0])[0] = "Productos";
                            ((object[])chartData[0])[1] = new TipoProductoBLL().ObtenerTipoProducto(Convert.ToInt32(pTipoProducto)).DESCRIPCION;

                            foreach (var item in lstDatos)
                            {
                                contador++;
                                chartData.Add(new object[2]);
                                ((object[])chartData[contador])[0] = item.NOMBRE;
                                ((object[])chartData[contador])[1] = item.VALOR;
                            }
                        }
                    }
                }
            }
            return chartData;
        }
    }
}