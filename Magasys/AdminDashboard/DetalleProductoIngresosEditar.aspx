<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleProductoIngresosEditar.aspx.cs" Inherits="PL.AdminDashboard.DetalleProductoIngresosEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
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
        <form id="FormDetalleProductoIngresoEditar" runat="server">
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
                                                <th>Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                             
                                                <th data-hide="all">Eliminar Imagen</th>    
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
                                            <div id="dvFechaEdicion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>                                            
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                       
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
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
                                                <th>Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                          
                                                <th data-hide="all">Eliminar Imagen</th>                                       
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
                                            <div id="dvFechaEdicion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy")%>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvDescripcion" style="width: 100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                       
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
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
                                                <th>Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                        
                                                <th data-hide="all">Eliminar Imagen</th>                                               
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
                                            <div id="dvDescripcion" style="width: 100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                     
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
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
                                                <th>Tipo de Producto</th>
                                                <th>Autor</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                                
                                                <th data-hide="all">Eliminar Imagen</th>                                
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
                                            <asp:Label ID="lblAutor" runat="server" Text='<%#Eval("AUTOR").ToString().Length > 50 ? String.Format("{0}...", Eval("AUTOR").ToString().Remove(50).TrimEnd()):Eval("AUTOR")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEdicion" runat="server" Text='<%#Eval("EDICION").ToString().Length > 50 ? String.Format("{0}...", Eval("EDICION").ToString().Remove(50).TrimEnd()):Eval("EDICION")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div id="dvDescripcion" style="width: 100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                   
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
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
                                                <th>Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                                  
                                                <th data-hide="all">Eliminar Imagen</th>                                                
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
                                            <div id="dvFechaEdicion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_EDICION") != null) ? Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvDescripcion" style="width: 100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                      
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
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
                                                <th>Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="all">Descripci&oacute;n</th>
                                                <th data-hide="all">Precio</th>
                                                <th data-hide="all">Cantidad</th>
                                                <th data-hide="all">Fecha Devoluci&oacute;n</th>
                                                <th data-hide="all">Imagen</th>
                                                <th data-hide="all"></th>
                                                <th data-hide="all"></th>                                                  
                                                <th data-hide="all">Eliminar Imagen</th>                                             
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
                                            <div id="dvFechaEdicion" class="form-group" style="width:157px">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_EDICION") != null) ? Convert.ToDateTime(Eval("FECHA_EDICION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvDescripcion" style="width: 100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvPrecio" style="width: 100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("PRECIO").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvCantidad" style="width: 100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off" Text='<%#Eval("CANTIDAD_DISPONIBLE").ToString()%>'></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="dvFechaDevolucion" class="form-group" style="width:157px">
                                                <div class="input-group date">
                                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                    <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" Text='<%#(Eval("FECHA_DEVOLUCION") != null) ? Convert.ToDateTime(Eval("FECHA_DEVOLUCION")).ToString("dd/MM/yyyy"):null%>'></asp:TextBox>
                                                </div>
                                            </div>
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
                                        <td>
                                            <div style="width: 100%">
                                                <asp:FileUpload ID="fuploadImagen" accept=".jpg" runat="server" CssClass="form-control" />
                                            </div>
                                        </td>                                   
                                        <td>                                            
                                            <div style="width: 100%">                                               
                                                <asp:CheckBox ID="chkEliminar" runat="server" class="i-checks" />
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="hr-line-dashed"></div>
                            <div class="form-group" style="margin-bottom: 50px">
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
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="js/plugins/iCheck/icheck.min.js"></script>
    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {
                KeypressEnterDisabled();
                LoadFootable();
                LoadDatePicker();
            });
        }

        function KeypressEnterDisabled() {
            $('input').keypress(function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function LoadDatePicker() {
            $('#dvFechaEdicion .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dvFechaDevolucion .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });
        }

    </script>
            <script>
                $(document).ready(function () {
                    $('.i-checks').iCheck({
                        checkboxClass: 'icheckbox_square-green',
                        radioClass: 'iradio_square-green',
                    });
                });
        </script>
</asp:Content>
