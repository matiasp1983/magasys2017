<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleProductoDevoluciones.aspx.cs" Inherits="PL.AdminDashboard.DetalleProductoDevoluciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Detalle de Devoluciones de Productos</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Devoluciones
                </li>
                <li class="active">
                    <strong>Detalle de Devolución</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormDetalleDevolucionProductos" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-content">
                            <br />

                            <asp:ListView ID="lsvDetalleDevolucion" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>C&oacute;digo Edici&oacute;n</th>
                                                <th>Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Tipo de Producto</th> 
                                                <th data-hide="phone,tablet">Nombre del Producto</th> 
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
                                        <td class="text-left">
                                            <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("ID_PRODUCTO_DEVOLUCION").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblCodigoEdicion" runat="server" Text='<%#Eval("COD_EDICION").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblNombreProducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="hr-line-dashed"></div>
                            <div class="form-group" style="margin-bottom: 50px">
                                <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                <div class="col-xs-12 col-md-4" style="text-align: right">
                                    <a href="javascript:history.go(-1)" class="btn btn-primary">Volver</a>
                                </div>
                            </div>
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
