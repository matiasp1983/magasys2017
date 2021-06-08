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
                <div class="ibox-title">
                    <h5>Reservas Periódicas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <br />
                    <input type="text" class="form-control input-sm m-b-xs" id="filter"
                            placeholder="Buscar ...">
                    <br />
                    <asp:ListView ID="lsvReservaPeriodica" runat="server" Visible="true">
                        <LayoutTemplate>
                            <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15" data-filter=#filter>
                                <thead>
                                    <tr>                                                    
                                        <td class="text-left">                                                        
                                            <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                        </td>
                                        <th class="text-left">Reserva</th>
                                        <th data-hide="phone,tablet">Fecha</th>
                                        <th>Cliente</th>
                                        <th>Producto</th>
                                        <th data-hide="phone,tablet">Tipo producto</th>
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
                                    <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" Visible="true"/>
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
                                    <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>' Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div id="dvMensajeLsvReservasPriodicas" runat="server" />
                    <asp:HiddenField ID="hfCodigoReserva" runat="server"/>
                    <div class="form-group" style="margin-bottom: 10px">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="BtnGuardarReservaPeriodica_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Reservas Únicas</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <br />
                    <input type="text" class="form-control input-sm m-b-xs" id="filter2"
                            placeholder="Buscar ...">
                    <br />
                    <asp:ListView ID="lsvReservaUnica" runat="server" Visible="true">
                        <LayoutTemplate>
                            <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15" data-filter=#filter2>
                                <thead>
                                    <tr>                                                    
                                        <td class="text-left">                                                        
                                            <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                        </td>
                                        <th class="text-left">Reserva</th>
                                        <th data-hide="phone,tablet">Fecha</th>
                                        <th>Cliente</th>
                                        <th>Edición</th>
                                        <th>Producto</th>
                                        <th data-hide="phone,tablet">Tipo producto</th>
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
                                    <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" Visible="true"/>
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
                                    <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>' Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                    <div id="dvMensajeLsvReservasUnicas" runat="server" />
                    <div class="form-group" style="margin-bottom: 10px">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardarUnica" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="BtnGuardarReservaUnica_Click" />
                            <asp:Button ID="btnCancelarUnica" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
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
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green'
                }).on('ifChecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    var codigo = e.currentTarget.id.split('&')[1] + ";";

                    if (isChecked == true) {
                        if ($('#<%=hfCodigoReserva.ClientID%>').val() == "") {
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigo);
                        } else {
                            var codigoAnterior = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigoAnterior + codigo);
                        }
                    }
                });

                $('.i-checks').on('ifUnchecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    var codigo = e.currentTarget.id.split('&')[1] + ";";                   

                    if (isChecked == false) {
                        if ($('#<%=hfCodigoReserva.ClientID%>').val() != "") {
                            var mm = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(mm.replace(codigo,"").trim());
                        }
                    }                    
                });                

                var checked = false;

                $('#check-all').on('click', function () {
                    if (checked == false) {
                        $('.i-checks').prop('checked', true).iCheck('update');
                        $('#<%=hfCodigoReserva.ClientID%>').val('');

                        $('.i-checks').each(function () {                            
                            $('#<%=hfCodigoReserva.ClientID%>').val($('#<%=hfCodigoReserva.ClientID%>').val()+(this.id.split('&')[1] + ";"));
                        });                        

                        checked = true;
                    } else {
                        $('.i-checks').prop('checked', false).iCheck('update');
                        $('#<%=hfCodigoReserva.ClientID%>').val('');
                        checked = false;
                    }

                });
                LoadFootable();
            });
        }        

        function LoadFootable() {
            $('.footable').footable();
        }

    </script>
</asp:Content>