<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConsultarRepartos.aspx.cs" Inherits="PL.AdminDashboard.ConsultarRepartos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Reparto</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Entrega / Reparto
                </li>
                <li class="active">
                    <strong>Consultar repartos</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormConsultaReparto" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-content">
                            <asp:ListView ID="lsvReparto" runat="server">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="5">
                                        <thead>
                                            <tr>
                                                <th class="text-left">Cliente</th>
                                                <th data-hide="phone,tablet">Dirección Origen</th>
                                                <th data-hide="phone,tablet">Dirección Destino</th>
                                                <th class="text-left">Edición</th>
                                                <th class="text-left">Producto</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="8">
                                                    <ul class="pagination pull-right"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("CLIENTE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblOrigen" runat="server" Text='<%#Eval("ORIGEN")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDestino" runat="server" Text='<%#Eval("DESTINO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProducto" runat="server" Text='<%#Eval("PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD")%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvReparto" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {
                LoadFootable();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }
    </script>
</asp:Content>
