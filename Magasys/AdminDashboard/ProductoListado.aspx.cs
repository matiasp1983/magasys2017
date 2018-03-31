using System;

namespace PL.AdminDashboard
{
    public partial class ProductoListado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*Prueba con Producto*/
            /*Session[BLL.Common.Enums.Session.ProductoDiario.ToString()] = new BLL.DiarioBLL().ObtenerDiario(64);
            Response.Redirect("ProductoDiarioEditar.aspx", false);
            Response.Redirect("ProductoDiarioVisualizar.aspx", false);*/

            /*Prueba con Revista*/
            Session[BLL.Common.Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(20);
            Response.Redirect("ProductoRevistaEditar.aspx", false);
        }
    }
}