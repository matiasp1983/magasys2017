<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="PL.CustomersWebSite.Notificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormNotificaciones" runat="server">
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox" id="ibox1">
                        <div class="ibox-title">
                            <h5>Notificaciones</h5>
                        </div>
                        <div class="ibox-content">
                            <p>
                                Te informamos las novedades sobre el estado de tus reservas.
                            </p>
                            <div class="sk-spinner sk-spinner-double-bounce">
                                <div class="sk-double-bounce1"></div>
                                <div class="sk-double-bounce2"></div>
                            </div>
                            <ul class="sortable-list connectList agile-list ui-sortable" id="todo">
                                <asp:ListView ID="lsvNotificaciones" runat="server">
                                    <ItemTemplate>
                                        <div class="ibox">
                                            <li class="<%#Eval("TIPO_MENSAJE").ToString()%>" id="task1">                                                   
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("DESCRIPCION").ToString()%>'></asp:Label>
                                                <div class="agile-detail">
                                                    <a href="#" class="pull-right btn btn-xs btn-white">Visto</a>
                                                    <i class="fa fa-clock-o"></i> <asp:Label ID="lblFechaRegistroMensaje" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA_REGISTRO_MENSAJE")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                                </div>
                                            </li>
                                        </div>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="dvMensajeLsvNotificaciones" runat="server" />
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
