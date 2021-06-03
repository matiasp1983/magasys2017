<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reparto.aspx.cs" Inherits="PL.AdminDashboard.Reparto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
    <link href="css/plugins/switchery/switchery.css" rel="stylesheet">  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Listado de Reparto</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Entrega / Reparto
                </li>
                <li class="active">
                    <strong>Generar listado de reparto</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormReparto" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar reservas</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Producto</label>
                                            <asp:TextBox ID="txtProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Edición</label>
                                            <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre de Edición</label>
                                            <asp:TextBox ID="txtNombreEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div style="text-align: right; padding-right: 15px;">
                                        <button type="button" id="btnBuscar" runat="server" class="ladda-button btn btn-success" onserverclick="BtnBuscar_Click">
                                            <i class="fa fa-search"></i>&nbsp;&nbsp;<span>Buscar</span>
                                        </button>
                                        <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                            <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                                        </button>
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
                        <div class="ibox-content">
                            <div style="text-align: right;">                                
                                <label class="m-r"><input id="chkRegresarAlKiosco" runat="server" type="checkbox" class="js-switch" checked />&nbsp;&nbsp;&nbsp;Regresar al Kiosco</label>&nbsp;
                                <button type="button" id="btnNuevo" runat="server" class="ladda-button btn btn-info" onserverclick="BtnGenerarHojaRuta_Click">
                                    <i class="fa fa-map-o"></i>&nbsp;&nbsp;<span>Generar hoja de ruta</span>
                                </button>                                
                            </div>
                            <br />
                            <input type="text" class="form-control input-sm m-b-xs" id="filter"
                                        placeholder="Buscar ...">  
                            <asp:ListView ID="lsvReserva" runat="server">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15" data-filter=#filter>
                                        <thead>
                                            <tr>
                                                <td>
                                                    <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                </td>                                                
                                                <th class="text-left">Cliente</th>
                                                 <th data-hide="phone,tablet">Tipo producto</th>
                                                <th data-hide="phone,tablet">Producto</th>
                                                <th data-hide="phone,tablet">Edición</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="12">
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
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("CLIENTE").ToString().Length > 50 ? String.Format("{0}...", Eval("CLIENTE").ToString().Remove(50).TrimEnd()):Eval("CLIENTE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblIdReservaEdicion" runat="server" Text='<%#Eval("ID_RESERVA_EDICION")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCodigoCliente" runat="server" Text='<%#Eval("CODIGO_CLIENTE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDireccionMaps" runat="server" Text='<%#Eval("DIRECCION_MAPS")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblClienteNombre" runat="server" Text='<%#Eval("CLIENTE_NOMBRE")%>' Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCodigoProductoEdicion" runat="server" Text='<%#Eval("CODIGO_EDICION")%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajelsvReserva" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>
    <!-- Switchery -->
   <script src="js/plugins/switchery/switchery.js"></script>

    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {
                LoadJsSwitch();
                LoadIchecks();
                LoadFootable();
            });
        }        

        function LoadFootable() {
            $('.footable').footable();
        }

        function LoadJsSwitch() {
            var elem = document.querySelector('.js-switch');
            var switchery = new Switchery(elem);
        }

        function LoadIchecks() {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green'
            });
            
            var checked = false;

            $('#check-all').on('click', function () {
                if (checked == false) {
                    $('.i-checks').prop('checked', true).iCheck('update');
                    checked = true;
                } else {
                    $('.i-checks').prop('checked', false).iCheck('update');
                    checked = false;
                }
            });
        }

    </script>
</asp:Content>
