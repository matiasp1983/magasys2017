<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Venta.aspx.cs" Inherits="PL.AdminDashboard.Venta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormVenta" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Venta</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li class="active">
                        <strong>Venta</strong>
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
                                <label class="col-sm-2 control-label">Código de Venta</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigoVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Venta</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaVenta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Forma de Pago</label>
                                <div class="col-sm-10">
                                    <div id="divFormaPago">
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Clientes</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Tipo de Documento</label>
                                <div class="col-sm-2">
                                    <div id="divTipoDocumento">
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <label class="col-sm-4 control-label">Número de Documento</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <div class="col-sm-3">
                                    <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="BtnBuscarCliente_Click" formnovalidate="formnovalidate"/>
                                </div>
                                <div class="col-sm-5">
                                    <button type="button" id="btnNuevoCliente" runat="server" class="ladda-button btn btn-info" onserverclick="BtnNuevoCliente_Click">
                                        <i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Nuevo Cliente</span></button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
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
                                <br />

                                <asp:ListView ID="lsvDiarios" runat="server" Visible="false" OnItemDataBound="LsvDiarios_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
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
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lsvRevistas" runat="server" Visible="false" OnItemDataBound="LsvRevistas_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
                                                    <th data-hide="phone,tablet">Acci&oacute;n</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="11">
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
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lsvColecciones" runat="server" Visible="false" OnItemDataBound="LsvColecciones_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
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
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lsvLibros" runat="server" Visible="false" OnItemDataBound="LsvLibros_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th>Autor</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
                                                    <th data-hide="phone,tablet">Acci&oacute;n</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="11">
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
                                                <asp:Label ID="lblAutor" runat="server" Text='<%#Eval("AUTOR").ToString().Length > 50 ? String.Format("{0}...", Eval("AUTOR").ToString().Remove(50).TrimEnd()):Eval("AUTOR")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lsvSuplementos" runat="server" Visible="false" OnItemDataBound="LsvSuplementos_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
                                                    <th data-hide="phone,tablet">Acci&oacute;n</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="11">
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
                                                <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#(Eval("FECHA_EDICION") != null) ? Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <asp:ListView ID="lsvPeliculas" runat="server" Visible="false" OnItemDataBound="LsvPeliculas_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                    <th data-hide="phone,tablet">Precio</th>
                                                    <th data-hide="phone,tablet">Stock</th>
                                                    <th data-hide="phone,tablet">Cantidad</th>
                                                    <th data-hide="phone,tablet">Acci&oacute;n</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr id="itemPlaceholder" runat="server"></tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <td colspan="11">
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
                                                <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#(Eval("FECHA_EDICION") != null) ? Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCantidadDisponible" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <div id="dvCantidad" style="width: 100%">
                                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:TextBox>
                                                </div>
                                            </td>
                                            <td class="text-left">
                                                <div class="btn-group">
                                                    <button runat="server" id="btnAgregar" class="btn btn-outline btn-info  btn-xl" title="Añadir" onserverclick="BtnAgregarItem_Click"><i class="fa fa-plus"></i></button>
                                                </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <div id="dvMensajeLsvProductos" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divVentaTotales" class="ibox float-e-margins" runat="server">
                <div class="ibox">
                    <div class="ibox-title">
                        <h2>Items de la Venta</h2>
                    </div>
                    <div class="ibox-content">
                        <asp:ListView ID="lsvVenta" runat="server" OnItemDataBound="LsvVenta_ItemDataBound">
                            <LayoutTemplate>
                                <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                    <thead>
                                        <tr>
                                            <th class="text-left">C&oacute;digo</th>
                                            <th>Nombre</th>
                                            <th>Tipo de Producto</th>
                                            <th data-hide="phone,tablet">Edici&oacute;n</th>
                                            <th data-hide="phone,tablet">Precio unitario</th>
                                            <th data-hide="phone,tablet">Cantidad</th>
                                            <th data-hide="phone,tablet">Valor total</th>
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
                                        <asp:Label ID="lblPrecioUnitario" runat="server" Text='<%#Eval("PRECIO_UNITARIO").ToString()%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD").ToString()%>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("VALOR_TOTAL").ToString()%>'></asp:Label>
                                    </td>
                                    <td class="text-left">
                                        <div class="btn-group">
                                            <button runat="server" id="btnEliminar" class="btn btn-outline btn-danger btn-xl" title="Eliminar" onserverclick="BtnEliminar_Click"><i class="fa fa-trash-o"></i></button>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCodProductoEdicion" runat="server" Text='<%#Eval("COD_PRODUCTO_EDICION").ToString()%>' Visible="false"></asp:Label>
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
                                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardarVenta_Click" />
                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelarVenta_Click" formnovalidate="" />
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
        var FormVenta = '#<%=FormVenta.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    radioClass: 'iradio_square-green'
                });
                LoadFootable();
                ValidarForm();
                Select2();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function ValidarForm() {
            $(FormVenta).validate({
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
