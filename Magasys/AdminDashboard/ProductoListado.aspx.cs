using System;

namespace PL.AdminDashboard
{
    public partial class ProductoListado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Diario Editar*/
            Session[BLL.Common.Enums.Session.ProductoDiario.ToString()] = new BLL.DiarioBLL().ObtenerDiario(64);
            Response.Redirect("ProductoDiarioEditar.aspx", false);
        }

        protected void Button11_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Diario Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoDiario.ToString()] = new BLL.DiarioBLL().ObtenerDiario(64);
            Response.Redirect("ProductoDiarioVisualizar.aspx", false);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Revista Editar*/
            Session[BLL.Common.Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(22);
            Response.Redirect("ProductoRevistaEditar.aspx", false);
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Revista Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(22);
            Response.Redirect("ProductoRevistaVisualizar.aspx", false);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Colección Editar*/
            Session[BLL.Common.Enums.Session.ProductoColeccion.ToString()] = new BLL.ColeccionBLL().ObtenerColeccion(27);
            Response.Redirect("ProductoColeccionEditar.aspx", false);
        }

        protected void Button33_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Colección Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoColeccion.ToString()] = new BLL.ColeccionBLL().ObtenerColeccion(27);
            Response.Redirect("ProductoColeccionVisualizar.aspx", false);
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Libro Editar*/
            Session[BLL.Common.Enums.Session.ProductoLibro.ToString()] = new BLL.LibroBLL().ObtenerLibro(73);
            Response.Redirect("ProductoLibroEditar.aspx", false);
        }

        protected void Button44_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Colección Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoLibro.ToString()] = new BLL.LibroBLL().ObtenerLibro(73);
            Response.Redirect("ProductoLibroVisualizar.aspx", false);
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Suplemento Editar*/
            Session[BLL.Common.Enums.Session.ProductoSuplemento.ToString()] = new BLL.SuplementoBLL().ObtenerSuplemento(74);
            Response.Redirect("ProductoSuplementoEditar.aspx", false);
        }

        protected void Button55_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Suplemento Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoSuplemento.ToString()] = new BLL.SuplementoBLL().ObtenerSuplemento(74);
            Response.Redirect("ProductoSuplementoVisualizar.aspx", false);
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Película Editar*/
            Session[BLL.Common.Enums.Session.ProductoPelicula.ToString()] = new BLL.PeliculaBLL().ObtenerPelicula(76);
            Response.Redirect("ProductoPeliculaEditar.aspx", false);
        }

        protected void Button66_Click(object sender, EventArgs e)
        {
            /*Prueba con Producto Película Visualizar*/
            Session[BLL.Common.Enums.Session.ProductoPelicula.ToString()] = new BLL.PeliculaBLL().ObtenerPelicula(76);
            Response.Redirect("ProductoPeliculaVisualizar.aspx", false);
        }
    }
}