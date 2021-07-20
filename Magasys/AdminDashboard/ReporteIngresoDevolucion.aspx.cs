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
        public static List<object> ObtenerRatioIngresosDevoluciones(string[] pProductos, string pTipoProducto, string pFechaDesde, string pFechaHasta)
        {
            List<object> chartData = new List<object>();
            DateTime fechaDesde = DateTime.Today;
            DateTime fechaHasta = DateTime.Today.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            ObtenerDevolucionesPorTipoProducto_Result oDevolucion = null;
            List<ValorRatio> lstDatos = null;
            var loProductos = string.Empty;
            int contador = 0;
            double ratioValor = 0;

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
                var lstDevoluciones = new ProductoBLL().ObtenerDevolucionesPorTipoProducto(pTipoProducto, loProductos, fechaDesde);

                if (lstIngresos.Count > 0 && lstDevoluciones.Count > 0) // VER SI MOSTRAR MENSAJE CUANDO NO HAY DATOS PARA MOSTRAR .....
                {
                    lstDatos = new List<ValorRatio>();

                    foreach (var item in lstIngresos)
                    {
                        if (lstDevoluciones.Exists(p => p.COD_PRODUCTO == item.COD_PRODUCTO) == true)
                            oDevolucion = lstDevoluciones.Where(p => p.COD_PRODUCTO == item.COD_PRODUCTO).First();
                        else
                            continue;

                        ratioValor = Math.Round((double)Convert.ToInt32(oDevolucion.CANTIDAD) / Convert.ToInt32(item.CANTIDAD), 2);

                        ValorRatio oDatos = new ValorRatio()
                        {
                            COD_PRODUCTO = oDevolucion.COD_PRODUCTO,
                            VALOR = ratioValor.ToString()
                        };

                        if (pTipoProducto == "1")
                            oDatos.NOMBRE = $"{ oDevolucion.NOMBRE } - { oDevolucion.DESCRIPCION }";
                        else
                            oDatos.NOMBRE = oDevolucion.NOMBRE;

                        lstDatos.Add(oDatos);
                    }

                    if (lstDatos.Count > 0)
                    {
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
            return chartData;
        }
    }
}