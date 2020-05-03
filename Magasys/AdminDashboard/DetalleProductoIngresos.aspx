<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleProductoIngresos.aspx.cs" Inherits="PL.AdminDashboard.DetalleProductoIngresos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Detalle Ingreso de Producto</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Depósito
                </li>
                <li class="active">
                    <strong>Detalle de Ingreso de Producto</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormDetalleProductoIngreso" runat="server">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Proveedor</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtProveedor" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Ingreso</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaIngresoProducto" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
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
                            <br />

                            <asp:ListView ID="lsvDiarios" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>                                    
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
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <asp:ListView ID="lsvRevistas" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>                                           
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
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <asp:ListView ID="lsvColecciones" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
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
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <asp:ListView ID="lsvLibros" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th>Autor</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>                                              
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
                                            <asp:Label ID="lblAutor" runat="server" Text='<%#Eval("AUTOR").ToString().Length > 50 ? String.Format("{0}...", Eval("AUTOR").ToString().Remove(50).TrimEnd()):Eval("AUTOR")%>'></asp:Label>
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
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <asp:ListView ID="lsvSuplementos" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>                                               
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
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <asp:ListView ID="lsvPeliculas" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left" data-toggle="true">C&oacute;digo</th>
                                                <th>Nombre</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>                                             
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
                                            <asp:Label ID="lblCantidad" runat="server" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaDevolucion" runat="server" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <img src="<%#Eval("IMAGEN.ImageUrl")%>" style="width:200px"/>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%">
                                                <asp:Label ID="lblTitulo" runat="server" Text='<%#(Eval("TITULO") != null) ? Eval("TITULO").ToString().Length > 50 ? String.Format("{0}...", Eval("TITULO").ToString().Remove(50).TrimEnd()):Eval("TITULO"):null%>'></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="BtnModificar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" />
                                    </div>
                                </div>
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
                LoadFootable();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }
    </script>
</asp:Content>
