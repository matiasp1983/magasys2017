<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cobro.aspx.cs" Inherits="PL.AdminDashboard.Cobro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormCobro" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Cobro</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Clientes
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Registrar Cobro"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Clientes</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <div id="divTipoDocumento">
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Alias</label>
                                <div class="col-sm-10">
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
            <div id="divClienteListado" class="ibox float-e-margins" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <br />
                                <asp:ListView ID="lsvClientes" runat="server">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <td></td>
                                                    <th>Nombre del Cliente</th>
                                                    <th>Tipo de Documento</th>
                                                    <th>Nro. Documento</th>
                                                    <th>Alias</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="10">
                                                        <ul class="pagination pull-right"></ul>
                                                    </td>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <input id="<%#Eval("ID_CLIENTE").ToString()%>" name="CodigoProducto" class="i-checks" type="radio"/>                                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombreCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_CLIENTE").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_CLIENTE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoDocumento" runat="server" Text='<%#Eval("TIPO_DOCUMENTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNroDocumento" runat="server" Text='<%#Eval("NRO_DOCUMENTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAlias" runat="server" Text='<%#(Eval("ALIAS") != null) ? Eval("ALIAS").ToString().Length > 50 ? String.Format("{0}...", Eval("ALIAS").ToString().Remove(50).TrimEnd()):Eval("ALIAS"):null%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <asp:HiddenField ID="hfCodigoCliente" runat="server"/>
                                <div id="dvMensajeLsvClientes" runat="server"/>
                                <div class="form-group" style="margin-bottom: 10px">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="btn btn-success" OnClick="BtnContinuar_Click"/>
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
                    radioClass: 'iradio_square-green'
                }).on('ifChecked', function (e) {                    
                    var isChecked = e.currentTarget.checked;                   
                    if (isChecked == true) {
                        $('#<%=hfCodigoCliente.ClientID%>').val(e.currentTarget.id); 
                    }
                });

                LoadFootable();
                Select2();
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
