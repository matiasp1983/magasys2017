<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarReservadasConfirmadas.aspx.cs" Inherits="PL.AdminDashboard.RegistrarReservadasConfirmadas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormRegistrarReservadasConfirmadas" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Devolución de Productos</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li class="active">
                        <strong>Devolución</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Información General</h2>
                </div>
                <div class="ibox-content">
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
                                <label class="col-sm-2 control-label">Tipo de producto</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTipoProducto" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Edición</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Stock</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3">
                <div class="widget style1 lazur-bg">
                    <div class="p-m">
                        <h1 class="m-xs"><asp:Label ID="lblCantidadProductosDevolver" runat="server" Text=''></asp:Label></h1>
                        <h3 class="font-bold no-margins">
                            Productos
                        </h3>
                        <small>Cantidad de productos seleccionados para devolver.</small>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox-content">
                            <div class="text-center">
                                <div class="alert alert-success">
                                    <a class="alert-link" href="#">Seleccione las reservas que desea cancelar.</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <br />
                                <asp:ListView ID="lsvReservaEdicion" runat="server" Visible="true" OnSelectedIndexChanged="lsvReservaEdicion_SelectedIndexChanged">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>                                                    
                                                    <td>                                                        
                                                        <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                    </td>
                                                    <th>Reserva</th>
                                                    <th>Nombre del Cliente</th>
                                                    <th>Producto</th>
                                                    <th>Cod Producto</th>
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
                                                <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA_EDICION").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LblProdEdi" runat="server" Text='<%#Eval("COD_EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("COD_EDICION").ToString().Remove(50).TrimEnd()):Eval("COD_EDICION")%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="dvMensajeLsvReservaEdicion" runat="server" />
                                <asp:HiddenField ID="hfCodigoReserva" runat="server"/>
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
