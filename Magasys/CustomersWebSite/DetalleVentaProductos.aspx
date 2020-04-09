<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleVentaProductos.aspx.cs" Inherits="PL.CustomersWebSite.DetalleVentaProductos" %>
<%@ MasterType VirtualPath="~/CustomersWebSite/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormDetalleVentaProductos" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Información General</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Código de Venta</label>
                                            <asp:TextBox ID="txtCodigoVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Fecha de Venta</label>
                                            <asp:TextBox ID="txtFechaVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Forma de Pago</label>
                                            <asp:TextBox ID="txtFormaPago" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Estado</label>
                                            <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-title">
                                <h5>Items de la Venta</h5>
                            </div>
                            <div class="ibox-content">
                                <br />

                                <asp:ListView ID="lsvDetalleVenta" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th>C&oacute;digo Edici&oacute;n</th>
                                                    <th>Edici&oacute;n</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Precio Unitario</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
                                                    <th data-hide="phone,tablet">Subtotal</th>
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
                                                <asp:Label ID="lblCodigoEdicion" runat="server" Text='<%#Eval("COD_EDICION").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%#Eval("PRECIO_UNITARIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblSubtotal" runat="server" Text='<%#Eval("SUBTOTAL").ToString()%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h3 class="col-sm-4 control-label font-bold">Total $</h3>
                                            <div class="col-sm-1 control-label font-bold">
                                                <h2>
                                                    <asp:Label ID="lblTotal" runat="server" MaxLength="5" autocomplete="off"></asp:Label></h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            
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
