﻿using BLL;
using BLL.Common;
using System;

namespace PL.AdminDashboard
{
    public partial class Logout : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[AdminDashboardSessionBLL.DefaultSessionsId.Usuario.ToString()] != null)
            {
                if (new LoginBLL().CerrarSessionAdminDashboard())
                {
                    Session[AdminDashboardSessionBLL.DefaultSessionsId.Usuario.ToString()] = null;                    
                    Session.Abandon();
                    Response.Redirect("Login.aspx", false);
                }
            }
        }

        #endregion
    }
}