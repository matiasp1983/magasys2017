<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="VisualizarDeudas.aspx.cs" Inherits="PL.AdminDashboard.VisualizarDeudas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Visualizar Deudas</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Clientes
                </li>
                <li class="active">
                    <strong>Deudas</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormVisualizarDeudas" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Deudas</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group" id="dpFechaDesde">
                                            <label class="control-label">Fecha Venta Desde</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaVentaDesde" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group" id="dpFechaHasta">
                                            <label class="control-label">Fecha Venta Hasta</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaVentaHasta" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Código de Venta</label>
                                            <asp:TextBox ID="txtCodigoVenta" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">      
                                <div class="ibox-title">
                                    <h5>Clientes</h5>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Documento</label>
                                            <div id="divTipoDocumento">
                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Número de Documento</label>
                                            <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre</label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Apellido</label>
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Alias</label>
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                            <asp:ListView ID="lsvDeudas" runat="server" OnItemDataBound="LsvDeudas_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left">Código venta</th>
                                                <th class="text-left">Fecha venta</th>
                                                <th>Cliente</th>
                                                 <th data-hide="phone,tablet">Total</th>
                                                <th class="text-right" data-sort-ignore="true">Acci&oacute;n</th>
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
                                            <asp:Label ID="lblCodigoVenta" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaVenta" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                        </td>    
                                        <td>
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("CLIENTE").ToString()%>'></asp:Label>
                                        </td>        
                                        <td class="text-left">
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                        </td>                                          
                                        <td class="text-right">
                                            <div class="btn-group">
                                                <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>                                              
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvDeudas" runat="server"/>                          
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
                LoadDatePicker();
                LoadFootable();  
                Select2();
            });
        }

        function LoadDatePicker() {
            $('#dpFechaDesde .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaHasta .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function Select2() {
            $(".select2_tipodocumento").select2(
            {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
            });
        }
    </script>
</asp:Content>
