<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConsultaReservaEdicion.aspx.cs" Inherits="PL.CustomersWebSite.ConsultaReservaEdicion" %>
<%@ MasterType VirtualPath="~/CustomersWebSite/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormConsultaReservaEdicion" runat="server">
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
                                            <label class="control-label">Código</label>   
                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Estado</label>   
                                            <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Fecha de Inicio</label>   
                                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Fecha de Fin</label>   
                                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" disabled="" autocomplete="off" MaxLength="10"></asp:TextBox> 
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Nombre producto</label>   
                                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Descripción producto</label>   
                                            <asp:TextBox ID="txtDescripcionProducto" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Tipo de Reserva</label>   
                                            <asp:TextBox ID="txtTipoReserva" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Forma de Entrega</label>   
                                            <asp:TextBox ID="txtFormaEntrega" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Ediciones</h5>
                            </div>
                            <div class="ibox-content">
                                <asp:ListView ID="lsvReservaEdicion" runat="server">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th>Código reserva edición</th>
                                                    <th>Edición</th>
                                                    <th data-hide="phone,tablet" >Nombre</th>
                                                    <th>Descripción</th>
                                                    <th>Estado</th>   
                                                    <th>Precio</th>                                                                                                  
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
                                                <asp:Label ID="lblCodEdicion" runat="server" Text='<%#(Eval("ID_RESERVA_EDICION") != null) ? Eval("ID_RESERVA_EDICION"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblEdicion" runat="server" Text='<%#(Eval("EDICION") != null) ? Eval("EDICION"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblNombreEdicion" runat="server" Text='<%#(Eval("NOMBRE_EDICION") != null) ? Eval("NOMBRE_EDICION"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblDescripcionEdicion" runat="server" Text='<%#(Eval("DESC_EDICION") != null) ? Eval("DESC_EDICION"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblEstado" runat="server" Text='<%#(Eval("ESTADO") != null) ? Eval("ESTADO"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblPrecioEdicion" runat="server" Text='<%#(Eval("PRECIO_EDICION") != null) ? Eval("PRECIO_EDICION"):null%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>                       
                                </asp:ListView>
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