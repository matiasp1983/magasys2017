<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaEdicion.aspx.cs" Inherits="PL.AdminDashboard.ReservaEdicion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormReservaEdicion" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Reservas</h2>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox-content m-b-sm border-bottom">
                            <div class="p-xs">
                                <div class="pull-left m-r-md">
                                    <i class="fa fa-newspaper-o text-navy mid-icon"></i>
                                </div>
                                <h2>Confirmar reservas</h2>
                                <span>Los siguientes clientes tiene reservas del producto ingresado, seleccione las reservas que desea confirmar.</span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">                                       
                                        <h5>Ingreso</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins"><asp:Label ID="lblCantidadIngresada" runat="server" Text=''></asp:Label></h1>
                                        <small>Ingresos totales</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <span class="label label-info pull-right">Confirmadas</span>
                                        <h5>Reservas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins"><asp:Label ID="lblReservasSeleccionadas" runat="server" Text=''></asp:Label></h1>
                                        <small>Reservas seleccionadas</small>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="ibox float-e-margins">
                                    <div class="ibox-title">
                                        <span class="label label-primary pull-right">Total</span>
                                        <h5>Reservas</h5>
                                    </div>
                                    <div class="ibox-content">
                                        <h1 class="no-margins"><asp:Label ID="lblReservasTotales" runat="server" Text=''></asp:Label></h1>
                                        <small>Reservas totales</small>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ibox">
                            <div class="ibox-content">
                                <br />
                                <asp:ListView ID="lsvReservaEdicion" runat="server" Visible="true">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>                                                    
                                                    <td>                                                        
                                                        <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                    </td>
                                                    <th>Reserva</th>
                                                    <th>Código cliente</th>
                                                    <th>Nombre del Cliente</th>
                                                    <th>Producto</th>
                                                    <th>Código Producto</th>
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
                                            <td>                                                
                                                <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" />
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("CLIENTE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("PRODUCTO")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblProdEdi" runat="server" Text='<%#Eval("CODPRODUCTOEDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("CODPRODUCTOEDICION").ToString().Remove(50).TrimEnd()):Eval("CODPRODUCTOEDICION")%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div class="form-group" style="margin-bottom: 10px">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
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
        var loContReservasSeleccionadas = 0;

        if (window.jQuery) {
            $(document).ready(function () {    
                
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green'
                }).on('ifChecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    //var codigo = e.currentTarget.id.split('&')[1] + ";";

                    if (isChecked == true) {
<%--                        if ($('#<%=hfCodigoReserva.ClientID%>').val() == "") {
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigo);
                        } else {
                            var codigoAnterior = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigoAnterior + codigo);
                        }--%>

                        if ($('#<%=lblReservasSeleccionadas.ClientID%>').text() != "") {
                            loContReservasSeleccionadas = parseInt($('#<%=lblReservasSeleccionadas.ClientID%>').text());
                        }                        
                        loContReservasSeleccionadas++;
                        $('#<%=lblReservasSeleccionadas.ClientID%>').text(loContReservasSeleccionadas);
                    }
                });

                $('.i-checks').on('ifUnchecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    //var codigo = e.currentTarget.id.split('&')[1] + ";";                   

                    if (isChecked == false) {
<%--                        if ($('#<%=hfCodigoReserva.ClientID%>').val() != "") {
                            var mm = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(mm.replace(codigo,"").trim());
                        }--%>

                        loContReservasSeleccionadas = parseInt($('#<%=lblReservasSeleccionadas.ClientID%>').text());
                        loContReservasSeleccionadas--;
                        $('#<%=lblReservasSeleccionadas.ClientID%>').text(loContReservasSeleccionadas);
                    }                    
                });                

                var checked = false;

                $('#check-all').on('click', function () {
                    if (checked == false) {
                        //$('.i-checks').prop('checked', true).iCheck('update');
                       <%-- $('#<%=hfCodigoReserva.ClientID%>').val('');--%>
<%--                        $('.i-checks').each(function () {                            
                            $('#<%=hfCodigoReserva.ClientID%>').val($('#<%=hfCodigoReserva.ClientID%>').val()+(this.id.split('&')[1] + ";"));
                        });   --%>                     
                        $('.i-checks').iCheck('check');
                        checked = true;
                    } else {
                        //$('.i-checks').prop('checked', false).iCheck('update');
                        <%--$('#<%=hfCodigoReserva.ClientID%>').val('');--%>

                        $('.i-checks').iCheck('uncheck');
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
