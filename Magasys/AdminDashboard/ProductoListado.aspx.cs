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
    }
}