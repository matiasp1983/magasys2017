<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoDevolucion.aspx.cs" Inherits="PL.AdminDashboard.ProductoDevolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormDevolucion" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Devoluci&oacute;n de Productos</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li class="active">
                        <strong>Devoluci&oacute;n</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código de Devolución</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigoDevolucion" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Devolución</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaDevoluion" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Selecci&oacute;n de Productos</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Proveedor</label>
                                <div class="col-sm-10">
                                    <div id="divProveedor">
                                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="select2_proveedor form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Producto</label>
                                <div class="col-sm-10">
                                    <div id="divTipoProducto">
                                        <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre Producto</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Descripción Producto</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcionProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Edición</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Descripción Edición</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcionEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div style="text-align: right; padding-right: 15px;">
                            <a id="btnBuscarSuccess" class="ladda-button btn btn-success" style="background-color: #1c84c6; color: #FFFFFF; border-color: #1c84c6; border-radius: 3px; height: 33px; width: 100px; padding-left: 10px; padding-top: 0px;">
                                <i class="fa fa-search"></i>
                                <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar" CssClass="ladda-button btn btn-success" OnClick="BtnBuscarProducto_Click" Style="padding: 4px 5px" />
                            </a>
                            <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <div style="text-align: right;">
                                    <button type="button" id="btnDevolucionDiarias" runat="server" class="ladda-button btn btn-info" onserverclick="BtnDevolucionesDiarias_Click">
                                        <i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Devoluciones Diarias</span>
                                    </button>
                                </div>
                                <br />

                                <asp:ListView ID="lsvDevolucion" runat="server" Visible="false" OnItemDataBound="LsvDevolucion_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Stock</th>
                                                    <th data-hide="phone,tablet">Reservas Confirmadas</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
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
                                                <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("COD_PRODUCTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE").ToString().Remove(50).TrimEnd()):Eval("NOMBRE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvFechaDevolucion" style="width: 100%">
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy")%>'></asp:TextBox>
                                                </div
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadReservas" runat="server" Text='<%#Eval("CANTIDAD_RESERVAS").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn-success btn btn-xs" title="Devolver Producto" onserverclick="BtnAgregarItem_Click">Devolver</button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <div id="dvMensajeLsvDevolucion" runat="server" />
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
        var FormDevolucion = '#<%=FormDevolucion.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                LoadFootable();
                ValidarForm();
                Select2();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function ValidarForm() {
            $(FormDevolucion).validate({
                rules: {
                     <%=ddlProveedor.UniqueID%>: {
                    required: true
                },
                     <%=ddlTipoProducto.UniqueID%>: {
                         required: true
                     }
                },
        messages: {
                     <%=ddlProveedor.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                     <%=ddlTipoProducto.UniqueID%>: {
                         required: "Este campo es requerido."
                     }
        }
            });
        }

        function Select2() {
            $(".select2_proveedor").select2(
                {
                    placeholder: 'Seleccione un Proveedor',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_tipoproducto").select2(
                {
                    placeholder: 'Seleccione un Tipo de Producto',
                    width: '100%',
                    allowClear: true
                });
        }
    </script>
</asp:Content>