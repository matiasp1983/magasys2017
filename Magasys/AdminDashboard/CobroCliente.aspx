<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="CobroCliente.aspx.cs" Inherits="PL.AdminDashboard.CobroCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormCobroCliente" runat="server" class="form-horizontal">
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
                    <h2>Cliente</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                <label class="col-sm-2 control-label">Apellido</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Venta a Cuenta</h2>
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <div style="text-align: right;">
                                    <button type="button" id="btnCobrarTodo" runat="server" class="ladda-button btn btn-info" onserverclick="BtnCobrarTodo_Click">
                                        <span>Cobrar Todo</span>
                                    </button> 
                                    <button type="button" id="btnVolver" runat="server" class="btn btn-primary" onserverclick="BtnVolver_Click">
                                        <span>Volver</span>
                                    </button>                                      
                                </div>
                                <br />
                                <asp:ListView ID="lsvVentas" runat="server" OnItemDataBound="LsvVentas_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo de Venta</th>
                                                    <th data-hide="phone,tablet">Fecha de Venta</th>
                                                    <th data-hide="phone,tablet">Total</th>
                                                    <th data-hide="phone,tablet">Acci&oacute;n</th>
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
                                            <td class="text-left">
                                                <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                    <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="dvMensajeLsvVentas" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divCobro" class="ibox float-e-margins" runat="server">
                <div class="ibox">
                    <div class="ibox-title">
                        <h2>Items del Cobro</h2>
                    </div>
                    <div class="ibox-content">
                        <asp:ListView ID="lsvCobro" runat="server" OnItemDataBound="LsvCobro_ItemDataBound">
                            <LayoutTemplate>
                                <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                    <thead>
                                        <tr>
                                            <th class="text-left">C&oacute;digo de Venta</th>
                                            <th data-hide="phone,tablet">Total</th>
                                            <th data-hide="phone,tablet">Acci&oacute;n</th>
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
                                        <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                    </td>
                                    <td class="text-left">
                                        <div class="btn-group">
                                            <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger btn-xl" title="Eliminar" onserverclick="BtnEliminar_Click"><i class="fa fa-trash-o"></i></button>
                                        </div>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <h2 class="col-sm-4 control-label font-bold">Total $</h2>
                                    <div class="col-sm-1 control-label font-bold">
                                        <h2>
                                            <asp:Label ID="lblTotal" runat="server" MaxLength="5" autocomplete="off"></asp:Label></h2>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <hr />
                        <div class="form-group" style="margin-bottom: 10px">
                            <div class="col-xs-12 col-sm-6 col-md-8"></div>
                            <div class="col-xs-12 col-md-4" style="text-align: right">
                                <asp:Button ID="btnGuardar" runat="server" Text="Registrar Cobro" CssClass="btn btn-success" OnClick="BtnGuardarCobro_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelarCobro_Click" formnovalidate="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
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
