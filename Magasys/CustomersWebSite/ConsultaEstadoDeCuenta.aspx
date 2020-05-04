<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="ConsultaEstadoDeCuenta.aspx.cs" Inherits="PL.CustomersWebSite.ConsultaEstadoDeCuenta" %>
<%@ MasterType VirtualPath="~/CustomersWebSite/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormCuentaCorriente" runat="server">
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
                                            <label class="control-label">Forma de Pago</label>   
                                            <div id="divFormaPago">
                                                <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="select2_formapago form-control"></asp:DropDownList>                                            
                                            </div>   
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="control-label">Estado</label>
                                            <div id="divEstado">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select2_estado form-control"></asp:DropDownList>                                            
                                            </div>  
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group" id="dpFechaInicio">
                                            <label class="control-label">Fecha Desde</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group" id="dpFechaFin">
                                            <label class="control-label">Fecha Hasta</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
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
                                <asp:ListView ID="lsvCuentaCorriente" runat="server" OnItemDataBound="LsvCuentaCorriente_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>                                                    
                                                    <th>Fecha</th>
                                                    <th>C&oacute;digo</th>
                                                    <th>Estado</th>
                                                    <th data-hide="phone,tablet">Forma de Pago</th>
                                                    <th data-hide="phone,tablet">Importe</th>
                                                    <th class="text-right">Acci&oacute;n</th>
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
                                                <asp:Label ID="lblFechaAlta" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCodigo" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("ESTADO").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("FORMA_PAGO").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblImporte" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                            </td>  
                                            <td class="text-right">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>                                              
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>                       
                                </asp:ListView>
                                <div id="dvMensajeCuentaCorriente" runat="server" />
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
                LoadDatePicker();
                LoadFootable();
                Select2();
            });
        }

        function LoadDatePicker() {
            $('#dpFechaInicio .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaFin .input-group.date').datepicker({
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
            $(".select2_formapago").select2(
            {
                placeholder: 'Seleccione una Forma de Pago',
                width: '100%'
            }).on('select2:select', function (e) {
                var loData = e.params.data;
                if (loData.id == 2) {
                    $(".select2_estado").prop("disabled", false);
                }
                else {
                    $(".select2_estado").val(5).trigger("change");
                    $(".select2_estado").prop("disabled", true);
                }
            });

            $(".select2_estado").select2(
            {
                placeholder: 'Seleccione un Estado',
                width: '100%'                    
            }).prop("disabled", true);
        }
    </script>
</asp:Content>
