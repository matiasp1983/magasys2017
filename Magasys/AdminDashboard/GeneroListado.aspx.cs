using System;
using System.Web.UI.WebControls;
using BLL.Common;
using BLL.Filters;
using System.Web.UI.HtmlControls;
using NLog;
using System.Linq;

namespace PL.AdminDashboard
{
    public partial class GeneroListado : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            OcultarDivsMensajes();

            if (!Page.IsPostBack)
            {
                CargarGrillaGeneros();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            CargarGrillaGeneros();
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void LsvGeneros_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType != ListViewItemType.DataItem) return;

                var loIdGenero = ((BLL.DAL.Genero)e.Item.DataItem).ID_GENERO.ToString();
                var loNombre = ((BLL.DAL.Genero)e.Item.DataItem).NOMBRE.ToString();

                //HtmlButton btnVisualizar = ((HtmlButton)e.Item.FindControl("btnVisualizar"));
                //btnVisualizar.Attributes.Add("value", loIdGenero);

                HtmlButton btnModificar = ((HtmlButton)e.Item.FindControl("btnModificar"));
                btnModificar.Attributes.Add("value", loIdGenero);

                HiddenField hdIdGeneroBaja = ((HiddenField)e.Item.FindControl("hdIdGeneroBaja"));
                // Se concatena el IdProveedor y el CUIT:
                hdIdGeneroBaja.Value = string.Format("{0},{1}", loIdGenero, loNombre);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Genero.aspx", false);
        }

        protected void BtnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                var oGenero = new BLL.GeneroBLL().ObtenerGenero(Convert.ToInt64(((HtmlButton)sender).Attributes["value"]));
                Session.Add(Enums.Session.Genero.ToString(), oGenero);
                Response.Redirect("GeneroEditar.aspx", false);
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        protected void BtnBaja_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (!String.IsNullOrEmpty(hdIdProveedorBaja.Value))
            //    {
            //        var loIdProveedor = Convert.ToInt64(hdIdProveedorBaja.Value);
            //        var oProveedor = new BLL.ProveedorBLL();
            //        if (oProveedor.BajaProveedor(loIdProveedor))
            //        {
            //            CargarGrillaProveedores();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger loLogger = LogManager.GetCurrentClassLogger();
            //    loLogger.Error(ex);
            //}
        }

        #endregion

        #region Metodos Privados

        private void OcultarDivsMensajes()
        {
            dvMensajeLsvGeneros.Visible = false;
        }

        private GeneroFiltro CargarGeneroFiltro()
        {
            GeneroFiltro oGeneroFiltro = null;

            oGeneroFiltro = new GeneroFiltro();

            if (string.IsNullOrEmpty(txtCodigo.Text))
                    oGeneroFiltro.Id_Genero = 0;
                else
                {
                    int loIdGenero;
                    bool loResultado = int.TryParse(txtCodigo.Text, out loIdGenero);
                    if (loResultado)
                        oGeneroFiltro.Id_Genero = loIdGenero;
                    else
                        oGeneroFiltro.Id_Genero = -1;
                }

            if (!String.IsNullOrEmpty(txtNombre.Text))
                oGeneroFiltro.Nombre = txtNombre.Text;
            

            return oGeneroFiltro;
        }

        private void CargarGrillaGeneros()
        {
            try
            {
                var oGeneroFiltro = CargarGeneroFiltro();

                if (oGeneroFiltro != null)
                {
                    var lstGeneros = new BLL.GeneroBLL().ObtenerGeneros(oGeneroFiltro);

                    if (lstGeneros != null && lstGeneros.Count > 0)
                        lsvGeneros.DataSource = lstGeneros;
                    else
                    {
                        dvMensajeLsvGeneros.InnerHtml = MessageManager.Info(dvMensajeLsvGeneros, Message.MsjeListadoGenerosFiltrarTotalSinResultados, false);
                        dvMensajeLsvGeneros.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lsvGeneros.DataSource = null;
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }

            lsvGeneros.DataBind();
        }

        private void LimpiarCampos()
        {
            FormGeneroListado.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            CargarGrillaGeneros();
        }

        #endregion
    }
}