<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConfirmarReservas.aspx.cs" Inherits="PL.AdminDashboard.ConfirmarReservas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormReservaEdicion" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Confirmar Reservas</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li>Reserva
                    </li>
                    <li class="active">
                        <strong>Confirmar Reservas</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <br />
                                <input type="text" class="form-control input-sm m-b-xs" id="filter"
                                    placeholder="Buscar ...">
                                <asp:ListView ID="lsvReservaEdicion" runat="server" Visible="true" OnItemDataBound="lsvReservaEdicion_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15" data-filter="#filter">
                                            <thead>
                                                <tr>
                                                    <td class="text-left">
                                                        <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                    </td>
                                                    <th class="text-left">Reserva</th>
                                                    <th data-hide="phone,tablet">Fecha</th>
                                                    <th>Cliente</th>
                                                    <th>Producto</th>
                                                    <th data-hide="phone,tablet">Tipo de reserva</th>
                                                    <th class="text-left" data-sort-ignore="true">Acci&oacute;n</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="30">
                                                        <ul class="pagination pull-right"></ul>
                                                    </td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="text-left">
                                                <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" visible="true" />
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblFecha" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblProdEdi" runat="server" Text='<%#Eval("TIPO_RESERVA").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <a id="anclaEdicion" class="btn btn-outline btn-success btn-xl" data-toggle="modal" title="Edición" onclick="CargarEdicion(this);" runat="server"><i class="fa fa-search"></i>
                                                        <asp:HiddenField ID="hdEdicion" runat="server" />
                                                    </a>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="dvMensajeLsvReservas" runat="server" />
                                <div class="modal inmodal fade" id="ModalEdicion" tabindex="-1" role="dialog" aria-hidden="true">
                                    <div class="modal-dialog modal-sm">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                                <h4 class="modal-title">
                                                    <asp:Label ID="lblEdicion" runat="server"></asp:Label></h4>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-info center-block" data-dismiss="modal">Cerrar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" style="margin-bottom: 10px">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
                                    </div>
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
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {
                LoadIChecks();
                LoadFootable();
            });
        }

        function LoadIChecks() {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green'
            });
            var checked = false;
            $('#check-all').on('click', function () {
                if (checked == false) {
                    $('.i-checks').prop('checked', true).iCheck('update');
                    $('.i-checks').each(function () { });
                    checked = true;
                } else {
                    $('.i-checks').prop('checked', false).iCheck('update');
                    checked = false;
                }
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function CargarEdicion(control) {
            if (window.jQuery) {
                 $('#<%=lblEdicion.ClientID%>').text(control.lastElementChild.defaultValue);
            }
        }

    </script>
</asp:Content>
