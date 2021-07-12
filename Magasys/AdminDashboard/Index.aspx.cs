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
    public partial class Index : System.Web.UI.Page
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
        public static List<object> ObtenerVentasPorProducto(string[] pProductos, string pTipoProducto, string pOperacion)
        {
            List<object> chartData = new List<object>();
            List<ObtenerVentasPorTipoProductoPorProductos_Result> lstDatosAux = null;
            int cantidadProductos = 0;
            int contador = 0;
            int contadorFechas = 0;
            DateTime fechaVenta = DateTime.MinValue;
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var loProductos = string.Empty;

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

                var lstDatos = new ProductoBLL().ObtenerVentasPorTipoProductoPorProductos(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstDatos.Count > 0)
                {
                    lstDatosAux = new List<ObtenerVentasPorTipoProductoPorProductos_Result>();

                    foreach (var item in lstDatos)
                    {
                        if (!lstDatosAux.Exists(p => p.ID_PRODUCTO == item.ID_PRODUCTO))
                        {
                            ObtenerVentasPorTipoProductoPorProductos_Result oDatosAux = new ObtenerVentasPorTipoProductoPorProductos_Result();
                            oDatosAux = item;
                            if (item.COD_TIPO_PRODUCTO == 1)
                                oDatosAux.NOMBRE = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                            lstDatosAux.Add(oDatosAux);
                        }
                    }

                    lstDatosAux = lstDatosAux.OrderBy(p => p.ID_PRODUCTO).ToList();
                    cantidadProductos = lstDatosAux.Count;
                    chartData.Add(new object[cantidadProductos + 1]);
                    ((object[])chartData[0])[0] = "Fecha";

                    foreach (var item in lstDatosAux)
                    {
                        contador++;
                        ((object[])chartData[0])[contador] = item.NOMBRE;
                    }

                    contador = 0;

                    foreach (var item in lstDatos)
                    {
                        if (fechaVenta != Convert.ToDateTime(item.FECHA_VENTA))
                        {
                            contadorFechas++;
                            fechaVenta = Convert.ToDateTime(item.FECHA_VENTA);
                            chartData.Add(new object[cantidadProductos + 1]);
                            ((object[])chartData[contadorFechas])[0] = fechaVenta.ToString("dd-MMM-yyyy");
                        }

                        contador = 0;
                        foreach (var itemAux in lstDatosAux)
                        {
                            contador++;
                            if (itemAux.ID_PRODUCTO == item.ID_PRODUCTO)
                                ((object[])chartData[contadorFechas])[contador] = item.CANTIDAD;
                        }
                    }
                }
            }
            return chartData;
        }

        [WebMethod]
        public static List<object> ObtenerVentasPorFiltro(string[] pProductos, string pTipoProducto, string pFechaDesde, string pFechaHasta)
        {
            List<object> chartData = new List<object>();
            List<ObtenerVentasPorTipoProductoPorProductos_Result> lstDatosAux = null;
            List<ObtenerVentasPorTipoProductoPorAnio_Result> lstDatosPorAnioAux = null;
            int cantidadProductos = 0;
            int contador = 0;
            int contadorFechas = 0;
            int anio = 0;
            DateTime fechaVenta = DateTime.MinValue;
            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MinValue;
            List<ObtenerVentasPorTipoProductoPorProductos_Result> lstDatos = new List<ObtenerVentasPorTipoProductoPorProductos_Result>();
            List<ObtenerVentasPorTipoProductoPorAnio_Result> lstDatosPorAnio = new List<ObtenerVentasPorTipoProductoPorAnio_Result>();
            var loProductos = string.Empty;

            if (!String.IsNullOrEmpty(pFechaDesde) && !String.IsNullOrEmpty(pFechaHasta))
            {
                foreach (var item in pProductos.ToList())
                {
                    if (!String.IsNullOrEmpty(item))
                        loProductos += string.Format("{0},", item);
                }

                if (!String.IsNullOrEmpty(loProductos))
                    loProductos = loProductos.Remove(loProductos.Length - 1);

                fechaDesde = Convert.ToDateTime(pFechaDesde);
                fechaHasta = Convert.ToDateTime(pFechaHasta).Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (fechaDesde.Year == fechaHasta.Year)
                    lstDatos = new ProductoBLL().ObtenerVentasPorTipoProductoPorProductos(pTipoProducto, loProductos, fechaDesde, fechaHasta);
                else
                    lstDatosPorAnio = new ProductoBLL().ObtenerVentasPorTipoProductoPorAnio(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstDatos.Count > 0)
                {
                    lstDatosAux = new List<ObtenerVentasPorTipoProductoPorProductos_Result>();

                    foreach (var item in lstDatos)
                    {
                        if (!lstDatosAux.Exists(p => p.ID_PRODUCTO == item.ID_PRODUCTO))
                        {
                            ObtenerVentasPorTipoProductoPorProductos_Result oDatosAux = new ObtenerVentasPorTipoProductoPorProductos_Result();
                            oDatosAux = item;
                            if (item.COD_TIPO_PRODUCTO == 1)
                                oDatosAux.NOMBRE = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                            lstDatosAux.Add(oDatosAux);
                        }
                    }

                    lstDatosAux = lstDatosAux.OrderBy(p => p.ID_PRODUCTO).ToList();
                    cantidadProductos = lstDatosAux.Count;
                    chartData.Add(new object[cantidadProductos + 1]);
                    ((object[])chartData[0])[0] = "Fecha";

                    foreach (var item in lstDatosAux)
                    {
                        contador++;
                        ((object[])chartData[0])[contador] = item.NOMBRE;
                    }

                    contador = 0;

                    foreach (var item in lstDatos)
                    {
                        if (fechaVenta != Convert.ToDateTime(item.FECHA_VENTA))
                        {
                            contadorFechas++;
                            fechaVenta = Convert.ToDateTime(item.FECHA_VENTA);
                            chartData.Add(new object[cantidadProductos + 1]);
                            ((object[])chartData[contadorFechas])[0] = fechaVenta.ToString("dd-MMM-yyyy");
                        }

                        contador = 0;
                        foreach (var itemAux in lstDatosAux)
                        {
                            contador++;
                            if (itemAux.ID_PRODUCTO == item.ID_PRODUCTO)
                                ((object[])chartData[contadorFechas])[contador] = item.CANTIDAD;
                        }
                    }
                }

                if (lstDatosPorAnio.Count > 0)
                {
                    lstDatosPorAnioAux = new List<ObtenerVentasPorTipoProductoPorAnio_Result>();

                    foreach (var item in lstDatosPorAnio)
                    {
                        if (!lstDatosPorAnioAux.Exists(p => p.ID_PRODUCTO == item.ID_PRODUCTO))
                        {
                            ObtenerVentasPorTipoProductoPorAnio_Result oDatosAux = new ObtenerVentasPorTipoProductoPorAnio_Result();
                            oDatosAux = item;
                            if (item.COD_TIPO_PRODUCTO == 1)
                                oDatosAux.NOMBRE = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                            lstDatosPorAnioAux.Add(oDatosAux);
                        }
                    }

                    lstDatosPorAnioAux = lstDatosPorAnioAux.OrderBy(p => p.ID_PRODUCTO).ToList();
                    cantidadProductos = lstDatosPorAnioAux.Count;
                    chartData.Add(new object[cantidadProductos + 1]);
                    ((object[])chartData[0])[0] = "Año";

                    foreach (var item in lstDatosPorAnioAux)
                    {
                        contador++;
                        ((object[])chartData[0])[contador] = item.NOMBRE;
                    }

                    contador = 0;

                    foreach (var item in lstDatosPorAnio)
                    {
                        if (anio != Convert.ToInt32(item.ANIO))
                        {
                            contadorFechas++;
                            anio = Convert.ToInt32(item.ANIO);
                            chartData.Add(new object[cantidadProductos + 1]);
                            ((object[])chartData[contadorFechas])[0] = item.ANIO.ToString();
                        }

                        contador = 0;
                        foreach (var itemAux in lstDatosPorAnioAux)
                        {
                            contador++;
                            if (itemAux.ID_PRODUCTO == item.ID_PRODUCTO)
                                ((object[])chartData[contadorFechas])[contador] = item.CANTIDAD;
                        }
                    }
                }
            }

            return chartData;
        }

        [WebMethod]
        public static List<object> ObtenerVentasPorProductoPieChart(string[] pProductos, string pTipoProducto, string pOperacion)
        {
            List<object> chartData = new List<object>();;
            int contador = 0;
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            var loProductos = string.Empty;

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

                var lstDatos = new ProductoBLL().ObtenerVentasPorTipoProductoPorProductosPieChart(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstDatos.Count > 0)
                {
                    chartData.Add(new object[2]);
                    ((object[])chartData[0])[0] = "Producto";
                    ((object[])chartData[0])[1] = "Cantidad";

                    foreach (var item in lstDatos)
                    {
                        contador++;
                        chartData.Add(new object[2]);

                        if (item.COD_TIPO_PRODUCTO == 1)
                            ((object[])chartData[contador])[0] = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                        else
                            ((object[])chartData[contador])[0] = item.NOMBRE;

                        ((object[])chartData[contador])[1] = item.CANTIDAD;
                    }
                }
            }
            return chartData;
        }

        [WebMethod]
        public static List<object> ObtenerVentasPorProductoPieChartFiltro(string[] pProductos, string pTipoProducto, string pFechaDesde, string pFechaHasta)
        {
            List<object> chartData = new List<object>(); ;
            int contador = 0;
            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MinValue;
            var loProductos = string.Empty;

            if (!String.IsNullOrEmpty(pFechaDesde) && !String.IsNullOrEmpty(pFechaHasta))
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

                var lstDatos = new ProductoBLL().ObtenerVentasPorTipoProductoPorProductosPieChart(pTipoProducto, loProductos, fechaDesde, fechaHasta);

                if (lstDatos.Count > 0)
                {
                    chartData.Add(new object[2]);
                    ((object[])chartData[0])[0] = "Producto";
                    ((object[])chartData[0])[1] = "Cantidad";

                    foreach (var item in lstDatos)
                    {
                        contador++;
                        chartData.Add(new object[2]);

                        if (item.COD_TIPO_PRODUCTO == 1)
                            ((object[])chartData[contador])[0] = $"{ item.NOMBRE } - { item.DESCRIPCION }";
                        else
                            ((object[])chartData[contador])[0] = item.NOMBRE;

                        ((object[])chartData[contador])[1] = item.CANTIDAD;
                    }
                }
            }
            return chartData;
        }
    }
}