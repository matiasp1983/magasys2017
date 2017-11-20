using BLL.Common;
using System;

namespace PL.AdminDashboard
{
    public partial class Producto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove(Enums.Session.Producto.ToString());
            Response.Redirect("ProductoListado.aspx");
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {

        }
    }
}