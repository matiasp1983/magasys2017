using BLL;
using NLog;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class ReporteVentaAnualTipoProducto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CargarTiposProducto();
                CargarAnios();
            }
        }

        #endregion

        #region Métodos Privados

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

        private void CargarAnios()
        {
            var oAnio = new BLL.AnioBLL();

            try
            {
                ddlAnio.DataSource = oAnio.ObtenerAnios(2018);
                ddlAnio.DataTextField = "DESCRIPCION";
                ddlAnio.DataValueField = "DESCRIPCION";
                ddlAnio.DataBind();
                ddlAnio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        #endregion

        [WebMethod]
        public static List<object> ObtenerVentaAnualPorTipoProducto(string pAnio, string pTipoProducto)
        {
            string loNombre = string.Empty;
            int loCantidadProductos = 0;
            int loContador = 0;

            List<object> chartData = new List<object>();

            if (!string.IsNullOrEmpty(pAnio) && !string.IsNullOrEmpty(pTipoProducto))
            {
                var loProducto = new ProductoBLL();
                var lstDatos = loProducto.VentaAnualPorTipoProducto(pAnio, pTipoProducto);

                if (lstDatos.Count > 0)
                {
                    foreach (var item in lstDatos)
                    {
                        if (loNombre == item.NOMBRE.ToString())
                            continue;
                        loNombre = item.NOMBRE.ToString();
                        loCantidadProductos++;
                    }

                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[0])[0] = "Mes";
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[1])[0] = "Enero";
                    for (int i = 1; i < ((object[])chartData[1]).Length; i++)
                    {
                        ((object[])chartData[1])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[2])[0] = "Febrero";
                    for (int i = 1; i < ((object[])chartData[2]).Length; i++)
                    {
                        ((object[])chartData[2])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[3])[0] = "Marzo";
                    for (int i = 1; i < ((object[])chartData[3]).Length; i++)
                    {
                        ((object[])chartData[3])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[4])[0] = "Abril";
                    for (int i = 1; i < ((object[])chartData[4]).Length; i++)
                    {
                        ((object[])chartData[4])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[5])[0] = "Mayo";
                    for (int i = 1; i < ((object[])chartData[5]).Length; i++)
                    {
                        ((object[])chartData[5])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[6])[0] = "Junio";
                    for (int i = 1; i < ((object[])chartData[6]).Length; i++)
                    {
                        ((object[])chartData[6])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[7])[0] = "Julio";
                    for (int i = 1; i < ((object[])chartData[7]).Length; i++)
                    {
                        ((object[])chartData[7])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[8])[0] = "Agosto";
                    for (int i = 1; i < ((object[])chartData[8]).Length; i++)
                    {
                        ((object[])chartData[8])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[9])[0] = "Septiembre";
                    for (int i = 1; i < ((object[])chartData[9]).Length; i++)
                    {
                        ((object[])chartData[9])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[10])[0] = "Octubre";
                    for (int i = 1; i < ((object[])chartData[10]).Length; i++)
                    {
                        ((object[])chartData[10])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[11])[0] = "Noviembre";
                    for (int i = 1; i < ((object[])chartData[11]).Length; i++)
                    {
                        ((object[])chartData[11])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadProductos + 1]);
                    ((object[])chartData[12])[0] = "Diciembre";
                    for (int i = 1; i < ((object[])chartData[12]).Length; i++)
                    {
                        ((object[])chartData[12])[i] = 0;
                    }

                    loNombre = string.Empty;

                    foreach (var item in lstDatos)
                    {
                        if (loNombre != item.NOMBRE.ToString())
                        {
                            loNombre = item.NOMBRE.ToString();
                            loContador++;
                            ((object[])chartData[0])[loContador] = item.NOMBRE.ToString().ToUpper();
                        }
                        ((object[])chartData[Convert.ToInt32(item.MES)])[loContador] = item.CANTIDAD;
                    }
                }
            }

            return chartData;
        }
    }
}