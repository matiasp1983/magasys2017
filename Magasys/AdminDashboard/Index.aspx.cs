using BLL;
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
                CargarAnio();
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

        private void CargarAnio()
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
        public static List<object> ObtenerVentasPorProducto(string[] pAnios, string pTipoProducto)
        {
            string loAnio = string.Empty;
            int loCantidadAnios = 0;
            int loContador = 0;

            List<object> chartData = new List<object>();

            if (!(pAnios.Length == 0) && !string.IsNullOrEmpty(pTipoProducto))
            {
                var loAnios = string.Empty;
                var loListAnios = pAnios.ToList();
                var loAniosTemp = string.Empty;

                foreach (var item in loListAnios)
                    loAniosTemp += string.Format("{0},", item);

                loAnios = loAniosTemp.Remove(loAniosTemp.Length - 1);

                var loProducto = new ProductoBLL();
                var lstDatos = loProducto.ProductosMasVendidos(loAnios, pTipoProducto);

                if (lstDatos.Count > 0)
                {
                    foreach (var item in lstDatos)
                    {
                        if (loAnio == item.ANIO.ToString())
                            continue;
                        loAnio = item.ANIO.ToString();
                        loCantidadAnios++;
                    }

                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[0])[0] = "Mes";
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[1])[0] = "Enero";
                    for (int i = 1; i < ((object[])chartData[1]).Length; i++)
                    {
                        ((object[])chartData[1])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[2])[0] = "Febrero";
                    for (int i = 1; i < ((object[])chartData[2]).Length; i++)
                    {
                        ((object[])chartData[2])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[3])[0] = "Marzo";
                    for (int i = 1; i < ((object[])chartData[3]).Length; i++)
                    {
                        ((object[])chartData[3])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[4])[0] = "Abril";
                    for (int i = 1; i < ((object[])chartData[4]).Length; i++)
                    {
                        ((object[])chartData[4])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[5])[0] = "Mayo";
                    for (int i = 1; i < ((object[])chartData[5]).Length; i++)
                    {
                        ((object[])chartData[5])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[6])[0] = "Junio";
                    for (int i = 1; i < ((object[])chartData[6]).Length; i++)
                    {
                        ((object[])chartData[6])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[7])[0] = "Julio";
                    for (int i = 1; i < ((object[])chartData[7]).Length; i++)
                    {
                        ((object[])chartData[7])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[8])[0] = "Agosto";
                    for (int i = 1; i < ((object[])chartData[8]).Length; i++)
                    {
                        ((object[])chartData[8])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[9])[0] = "Septiembre";
                    for (int i = 1; i < ((object[])chartData[9]).Length; i++)
                    {
                        ((object[])chartData[9])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[10])[0] = "Octubre";
                    for (int i = 1; i < ((object[])chartData[10]).Length; i++)
                    {
                        ((object[])chartData[10])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[11])[0] = "Noviembre";
                    for (int i = 1; i < ((object[])chartData[11]).Length; i++)
                    {
                        ((object[])chartData[11])[i] = 0;
                    }
                    chartData.Add(new object[loCantidadAnios + 1]);
                    ((object[])chartData[12])[0] = "Diciembre";
                    for (int i = 1; i < ((object[])chartData[12]).Length; i++)
                    {
                        ((object[])chartData[12])[i] = 0;
                    }

                    loAnio = string.Empty;

                    foreach (var item in lstDatos)
                    {
                        if (loAnio != item.ANIO.ToString())
                        {
                            loAnio = item.ANIO.ToString();
                            loContador++;
                            ((object[])chartData[0])[loContador] = item.ANIO.ToString();
                        }
                        ((object[])chartData[Convert.ToInt32(item.MES)])[loContador] = item.CANTIDAD;
                    }
                }
            }

            return chartData;
        }
    }
}