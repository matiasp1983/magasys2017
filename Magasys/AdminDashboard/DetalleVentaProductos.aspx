<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleVentaProductos.aspx.cs" Inherits="PL.AdminDashboard.DetalleVentaProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormDetalleVentaProductos" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
           <div class="col-lg-10">
               <h2>Detalle Venta</h2>
               <ol class="breadcrumb">
                   <li>
                   <a href="Index.aspx">Principal</a>
                   </li>
                   <li>Venta
                   </li>
                   <li class="active">
                       <strong>Detalle de Venta</strong>
                   </li>
                </ol>
           </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código de Venta</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigoVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Venta</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Forma de Pago</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFormaPago" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Estado</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Cliente</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
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
                            <h2>Items de la Venta</h2>
                        </div>
                        <div class="ibox-content">
                            <br />

                            <asp:ListView ID="lsvDetalleVenta" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <%--<th class="text-left">C&oacute;digo</th>--%>
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
<%--                                        <td class="text-left">
                                            <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                        </td>--%>
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
                                        <h2 class="col-sm-4 control-label font-bold">Total $</h2>
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
