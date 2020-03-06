﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using BLL.Common;
using BLL.DAL;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace PL.AdminDashboard
{
    public partial class ReservaEdicion : System.Web.UI.Page
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                CargarGrilla();
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            ListView lsvReservas = new ListView();
            List<ReservaClienteListado> lstReservas = new List<BLL.ReservaClienteListado>();

            foreach (var loItem in lsvReservaEdicion.Items)
            {
                if (((HtmlInputCheckBox)loItem.Controls[2]).Checked)
                {
                    ReservaClienteListado oReservaEdicion = new BLL.ReservaClienteListado
                    {
                        ID_RESERVA = Convert.ToInt32(((Label)loItem.Controls[4]).Text),
                        PRODUCTO = ((Label)loItem.Controls[8]).Text,
                        CLIENTE = ((Label)loItem.Controls[6]).Text
                    };

                    lstReservas.Add(oReservaEdicion);
                }
            }
            Session.Add(Enums.Session.ListadoVenta.ToString(), lsvReservas);
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region Métodos Privados

        private void CargarGrilla() // EL SIGUIENTE CÓDIGO ES DE EJEMPLO PARA PODER VISUALIZAR LA GRILLA!!
        {
            ListView lsvReservas = (ListView)Session[Enums.Session.ListadoReservaConfirmar.ToString()];
            List<ReservaClienteListado> lstReservasConfirmar = MapListViewToListObject(lsvReservas);

            lsvReservaEdicion.DataSource = lstReservasConfirmar;
            lsvReservaEdicion.DataBind();
            Session.Remove(Enums.Session.ListadoReservaConfirmar.ToString());
        }

        private List<ReservaClienteListado> MapListViewToListObject(object v)
        {
            throw new NotImplementedException();
        }

        private List<ReservaClienteListado> MapListViewToListObject(ListView pListView)
        {
            bool loResultado = false;

            List<ReservaClienteListado> lstReservas = (List<ReservaClienteListado>)pListView.DataSource;

            if (loResultado)
                lstReservas = null;

            return lstReservas;
        }

        #endregion
    }
}