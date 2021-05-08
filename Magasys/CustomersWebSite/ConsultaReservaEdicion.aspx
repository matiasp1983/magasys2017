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
                                <asp:ListView ID="lsvReservaEdicion" runat="server" OnItemDataBound="LsvReservas_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th>Código reserva edición</th>
                                                    <th>Edición</th>
                                                    <th data-hide="phone,tablet" >Nombre</th>
                                                    <th data-hide="phone,tablet" >Descripción</th>
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
                                            <td class="text-right">
                                                <a class="btn btn-white btn-xs" data-toggle="modal" data-target="#ModalReservaAnular" title="Eliminar" onclick="CargarIdReservaAnular(this);">
                                                    <asp:HiddenField ID="hdIdReservaEdicion" runat="server" />Anular
                                                </a>  
                                            </td>
                                        </tr>
                                    </ItemTemplate>                       
                                </asp:ListView>
                                <div id="ModalReservaAnular" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                    <div class="modal-dialog">
                                        <div class="modal-content animated bounce">
                                            <div class="modal-body">
                                                <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #86caf8;">
                                                    <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #86caf8;"></span>
                                                    <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #86caf8;"></span>
                                                </div>
                                                <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que desea anular la Reserva?</h2>
                                                <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                                </p>
                                            </div>
                                            <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                                <asp:Button ID="btnCancelarAnular" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                    Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                    data-dismiss="modal" />
                                                <asp:Button ID="btnAnular" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #558bdd"
                                                    OnClick="BtnAnular_Click" />
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdIdReservaEdicionAnular" runat="server" Value="" />
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
        var hdIdReservaEdicionAnular = '#<%=hdIdReservaEdicionAnular.ClientID%>';
    </script>
    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {
                LoadFootable();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function CargarIdReservaAnular(control) {
            if (window.jQuery) {
                var loIdReservaEdicion = control.lastElementChild.defaultValue;
                $(hdIdReservaEdicionAnular).val(loIdReservaEdicion);
            }
        }
    </script>
</asp:Content>