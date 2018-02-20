using BLL;
using BLL.Common;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace PL.AdminDashboard
{
    public partial class Producto : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProveedores();
                CargarGeneros();
                CargarTiposProducto();
                CargarDiasDeSemana();
                CargarPeriodicidades();                
                CargarAnios();
                CargarDiarios();
            }           
        }

        protected void DdlTipoProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            OcultarDivTipoProducto();

            switch (((DropDownList)sender).SelectedItem.Text)
            {
                case "Revista":
                    divRevista.Visible = true;
                    break;
                case "Colección":
                    divColeccion.Visible = true;
                    break;
                case "Libro":
                    divLibro.Visible = true;
                    break;
                case "Suplemento":
                    CargarDiarios();
                    divSuplemento.Visible = true;                    
                    break;
                case "Película":
                    divPelicula.Visible = true;
                    break;
                default:
                    divDiario.Visible = true;
                    break;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                bool loResutado = false;
                var oProducto = CargarProductoDesdeControles();

                switch (ddlTipoProducto.SelectedItem.Text)
                {
                    case "Revista":
                        var oRevista = CargarRevistaDesdeControles();
                        loResutado = new RevistaBLL().AltaRevista(oProducto, oRevista);
                        break;
                    case "Colección":
                        var oColeccion = CargarColeccionDesdeControles();
                        loResutado = new ColeccionBLL().AltaColeccion(oProducto, oColeccion);
                        break;
                    case "Libro":
                        var oLibro = CargarLibroDesdeControles();
                        loResutado = new LibroBLL().AltaLibro(oProducto, oLibro);
                        break;
                    case "Suplemento":
                        var oSuplemento = CargarSuplementoDesdeControles();
                        loResutado = new SuplementoBLL().AltaSuplemento(oProducto, oSuplemento);
                        break;
                    case "Película":
                        var oPelicula = CargarPeliculaDesdeControles();
                        loResutado = new PeliculaBLL().AltaPelicula(oProducto, oPelicula);
                        break;
                    default:
                        var lstDiarioDiasSemanas = CargarDiarioDesdeControles();
                        loResutado = new DiarioBLL().AltaDiario(oProducto, lstDiarioDiasSemanas);
                        break;
                }

                if (loResutado)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.SuccessModal(Message.MsjeProductoSuccessAlta, "Alta Producto"));
                    LimpiarCampos();
                }
                else
                    Page.ClientScript.RegisterStartupScript(GetType(), "Modal", MessageManager.WarningModal(Message.MsjeProductoFailure));
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
            Response.Redirect("ProductoListado.aspx");
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        #endregion

        #region Métodos Privados

        private void CargarProveedores()
        {
            var oProveedor = new ProveedorBLL();

            try
            {
                ddlProveedor.DataSource = oProveedor.ObtenerProveedores();
                ddlProveedor.DataTextField = "RAZON_SOCIAL";
                ddlProveedor.DataValueField = "ID_PROVEEDOR";
                ddlProveedor.DataBind();
                ddlProveedor.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarGeneros()
        {
            var oGenero = new GeneroBLL();

            try
            {
                ddlGenero.DataSource = oGenero.ObtenerGeneros();
                ddlGenero.DataTextField = "NOMBRE";
                ddlGenero.DataValueField = "ID_GENERO";
                ddlGenero.DataBind();
                ddlGenero.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarTiposProducto()
        {
            var oTipoProducto = new TipoProductoBLL();

            try
            {
                ddlTipoProducto.DataSource = oTipoProducto.ObtenerTiposProducto();
                ddlTipoProducto.DataTextField = "DESCRIPCION";
                ddlTipoProducto.DataValueField = "ID_TIPO_PRODUCTO";
                ddlTipoProducto.DataBind();
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarDiasDeSemana()
        {
            var oDiaSemana = new DiaSemanaBLL();

            try
            {
                var lstDiasDeSemana = oDiaSemana.ObtenerDiasDeSemana();

                ddlDiaDeEntregaRevista.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaRevista.DataTextField = "NOMBRE";
                ddlDiaDeEntregaRevista.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaRevista.DataBind();
                ddlDiaDeEntregaRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlDiaDeEntregaColeccion.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaColeccion.DataTextField = "NOMBRE";
                ddlDiaDeEntregaColeccion.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaColeccion.DataBind();
                ddlDiaDeEntregaColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlDiaDeEntregaSuplemento.DataSource = lstDiasDeSemana;
                ddlDiaDeEntregaSuplemento.DataTextField = "NOMBRE";
                ddlDiaDeEntregaSuplemento.DataValueField = "ID_DIA_SEMANA";
                ddlDiaDeEntregaSuplemento.DataBind();
                ddlDiaDeEntregaSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarPeriodicidades()
        {
            var oPeriodicidad = new PeriodicidadBLL();

            try
            {
                var lstPeriodicidades = oPeriodicidad.ObtenerPeriodicidades();

                ddlPeriodicidadRevista.DataSource = lstPeriodicidades;
                ddlPeriodicidadRevista.DataTextField = "NOMBRE";
                ddlPeriodicidadRevista.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadRevista.DataBind();
                ddlPeriodicidadRevista.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlPeriodicidadColeccion.DataSource = lstPeriodicidades;
                ddlPeriodicidadColeccion.DataTextField = "NOMBRE";
                ddlPeriodicidadColeccion.DataValueField = "ID_PERIODICIDAD";
                ddlPeriodicidadColeccion.DataBind();
                ddlPeriodicidadColeccion.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarDiarios()
        {
            var oDiario = new DiarioBLL();

            try
            {
                var lstDiarios = oDiario.ObtenerDiarios();

                ddlDiarioSuplemento.DataSource = lstDiarios;
                ddlDiarioSuplemento.DataTextField = "NOMBRE";
                ddlDiarioSuplemento.DataValueField = "ID_DIARIO";
                ddlDiarioSuplemento.DataBind();
                ddlDiarioSuplemento.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void CargarAnios()
        {
            var oAnio = new AnioBLL();

            try
            {
                var lstAnios = oAnio.ObtenerAnios();

                ddlAnioEdicionLibro.DataSource = lstAnios;
                ddlAnioEdicionLibro.DataTextField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataValueField = "DESCRIPCION";
                ddlAnioEdicionLibro.DataBind();
                ddlAnioEdicionLibro.Items.Insert(0, new ListItem(String.Empty, String.Empty));

                ddlAnioDeEstrenoPelicula.DataSource = lstAnios;
                ddlAnioDeEstrenoPelicula.DataTextField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataValueField = "DESCRIPCION";
                ddlAnioDeEstrenoPelicula.DataBind();
                ddlAnioDeEstrenoPelicula.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            }
            catch (Exception ex)
            {
                Logger loLogger = LogManager.GetCurrentClassLogger();
                loLogger.Error(ex);
            }
        }

        private void OcultarDivTipoProducto(bool pDesdeBotonLimpiar = false)
        {
            divRevista.Visible = divColeccion.Visible = divLibro.Visible = divSuplemento.Visible = divPelicula.Visible = false;

            if (!pDesdeBotonLimpiar)
                divDiario.Visible = false;
            else
                divDiario.Visible = true;
        }

        private BLL.DAL.Producto CargarProductoDesdeControles()
        {
            var oProducto = new BLL.DAL.Producto
            {
                NOMBRE = txtNombre.Text,
                COD_PROVEEDOR = Convert.ToInt32(ddlProveedor.SelectedValue),
                COD_GENERO = Convert.ToInt32(ddlGenero.SelectedValue),
                FECHA_ALTA = DateTime.Now,
                COD_ESTADO = 1,
                COD_TIPO_PRODUCTO = Convert.ToInt32(ddlTipoProducto.SelectedValue)
            };

            if (!String.IsNullOrEmpty(txtDescripcion.Text))
                oProducto.DESCRIPCION = txtDescripcion.Text;
            else
                oProducto.DESCRIPCION = null;

            return oProducto;
        }

        private BLL.DAL.Revista CargarRevistaDesdeControles()
        {
            var oRevista = new BLL.DAL.Revista
            {
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadRevista.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioRevista.Text)
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaRevista.SelectedValue))
                oRevista.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaRevista.SelectedValue);
            else
                oRevista.ID_DIA_SEMANA = null;

            return oRevista;
        }

        private BLL.DAL.Coleccion CargarColeccionDesdeControles()
        {
            var oColeccion = new BLL.DAL.Coleccion
            {
                COD_PERIODICIDAD = Convert.ToInt32(ddlPeriodicidadColeccion.SelectedValue)
            };

            if (!String.IsNullOrEmpty(ddlDiaDeEntregaColeccion.SelectedValue))
                oColeccion.ID_DIA_SEMANA = Convert.ToInt32(ddlDiaDeEntregaColeccion.SelectedValue);
            else
                oColeccion.ID_DIA_SEMANA = null;

            if (!String.IsNullOrEmpty(txtCantidadDeEntregaColeccion.Text))
                oColeccion.CANTIDAD_ENTREGAS = Convert.ToInt32(txtCantidadDeEntregaColeccion.Text);
            else
                oColeccion.CANTIDAD_ENTREGAS = null;

            return oColeccion;
        }

        private BLL.DAL.Libro CargarLibroDesdeControles()
        {
            var oLibro = new BLL.DAL.Libro
            {
                EDITORIAL = txtEditorialLibro.Text,
                AUTOR = txtAutorLibro.Text,
                PRECIO = Convert.ToDouble(txtPrecioLibro.Text)
            };

            if (!String.IsNullOrEmpty(ddlAnioEdicionLibro.SelectedValue))
                oLibro.ANIO = Convert.ToInt32(ddlAnioEdicionLibro.SelectedValue);
            else
                oLibro.ANIO = null;

            return oLibro;
        }

        private BLL.DAL.Suplemento CargarSuplementoDesdeControles()
        {
            var oSuplemento = new BLL.DAL.Suplemento
            {
                COD_DIARIO = Convert.ToInt32(ddlDiarioSuplemento.SelectedValue)
            };

            if (!String.IsNullOrEmpty(txtCantidadDeEntregaSuplemento.Text))
                oSuplemento.CANTIDAD_ENTREGAS = Convert.ToInt32(txtCantidadDeEntregaSuplemento.Text);
            else
                oSuplemento.CANTIDAD_ENTREGAS = null;

            if (!String.IsNullOrEmpty(txtPrecioSuplemento.Text))
                oSuplemento.PRECIO = Convert.ToDouble(txtPrecioSuplemento.Text);
            else
                oSuplemento.PRECIO = null;

            return oSuplemento;
        }

        private BLL.DAL.Pelicula CargarPeliculaDesdeControles()
        {
            var oPelicula = new BLL.DAL.Pelicula
            {
                ANIO = Convert.ToInt32(ddlAnioDeEstrenoPelicula.SelectedValue),
                PRECIO = Convert.ToDouble(txtPrecioPelicula.Text)
            };

            return oPelicula;
        }

        private List<BLL.DAL.DiarioDiaSemana> CargarDiarioDesdeControles()
        {
            List<BLL.DAL.DiarioDiaSemana> lstDiarioDiasSemanas = new List<BLL.DAL.DiarioDiaSemana>();

            foreach (var loTxtPrecioDiario in divDiario.Controls.OfType<TextBox>().ToList())
            {
                var oDiarioDiaSemana = new BLL.DAL.DiarioDiaSemana
                {
                    ID_DIA_SEMANA = new DiaSemanaBLL().ObtenerDiaSemana(ObtenerParteDeNombreIDTexbox(loTxtPrecioDiario.ID.ToString())).ID_DIA_SEMANA
                };

                if (!String.IsNullOrEmpty(loTxtPrecioDiario.Text))
                    oDiarioDiaSemana.PRECIO = Convert.ToDouble(loTxtPrecioDiario.Text);
                else
                    oDiarioDiaSemana.PRECIO = null;

                lstDiarioDiasSemanas.Add(oDiarioDiaSemana);
            }

            return lstDiarioDiasSemanas;
        }

        private void LimpiarCampos()
        {
            FormProducto.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            FormProducto.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divDiario.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divRevista.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divRevista.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divColeccion.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divColeccion.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divLibro.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divLibro.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divSuplemento.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divSuplemento.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            divPelicula.Controls.OfType<DropDownList>().ToList().ForEach(y => y.SelectedIndex = 0);
            divPelicula.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = String.Empty);
            OcultarDivTipoProducto(true);
        }

        private string ObtenerParteDeNombreIDTexbox(string pCadena)
        {
            String loStringInicial = "txtPrecio";
            String loStringFinal = "Diario";

            String loNuevoString = pCadena.Substring(0, pCadena.LastIndexOf(loStringFinal));
            int loIniciaString = loNuevoString.LastIndexOf(loStringInicial) + loStringInicial.Length;
            int loCortar = loNuevoString.Length - loIniciaString;
            loNuevoString = loNuevoString.Substring(loIniciaString, loCortar);

            return loNuevoString;
        }

        #endregion
    }
}