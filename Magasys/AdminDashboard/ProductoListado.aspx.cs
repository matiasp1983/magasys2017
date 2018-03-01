using System;

namespace PL.AdminDashboard
{
    public partial class ProductoListado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session[BLL.Common.Enums.Session.ProductoRevista.ToString()] = new BLL.RevistaBLL().ObtenerRevista(20);
            Response.Redirect("ProductoRevistaEditar.aspx", false);
        }
    }
}