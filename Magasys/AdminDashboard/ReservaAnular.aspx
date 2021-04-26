<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaAnular.aspx.cs" Inherits="PL.AdminDashboard.ReservaAnular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Anular Reservas</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Reserva
                </li>
                <li class="active">
                    <strong>Anular Reservas</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormReservaAnular" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Reserva</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Reserva</label>   
                                            <div id="divTipoReserva">
                                                <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="select2_tiporeserva form-control"></asp:DropDownList>                                            
                                            </div>   
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre del Producto</label>
                                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Estado</label>   
                                            <div id="divEstado">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select2_estado form-control"></asp:DropDownList>                                            
                                            </div>   
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">      
                                <div class="ibox-title">
                                    <h5>Cliente</h5>
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
<%--                                        <button type="button" id="btnBuscar" runat="server" class="ladda-button btn btn-success" onserverclick="BtnBuscar_Click">
                                            <i class="fa fa-search"></i>&nbsp;&nbsp;<span>Buscar</span>
                                        </button>
                                        <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                            <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                                        </button>--%>
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
                            <asp:ListView ID="lsvReservas" runat="server">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>                                                
                                                <th class="text-left">Nombre del Cliente</th>
                                                <th class="text-left">Producto</th>
                                                <th data-hide="phone,tablet">Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Tipo de Reserva</th>                                               
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
                                            <asp:Label ID="lblNombreCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 23 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(23).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                        </td>     
                                        <td class="text-left">
                                            <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                        </td>                                           
                                        <td class="text-left">
                                            <asp:Label ID="lblTipoReserva" runat="server" Text='<%#Eval("TIPO_RESERVA").ToString()%>'></asp:Label>
                                        </td>                                             
                                        <td>
                                            <asp:Label ID="lblCodigoReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvReservas" runat="server" />                              
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
                LoadFootable();
                Select2();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function Select2() {
            $(".select2_tiporeserva").select2(
                {
                    placeholder: 'Seleccione un Tipo de Reserva',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_estado").select2(
                {
                    placeholder: 'Seleccione un Estado',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_tipodocumento").select2(
                {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
                });
        }
    </script>
</asp:Content>
