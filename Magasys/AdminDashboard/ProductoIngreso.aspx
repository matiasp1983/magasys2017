<%@ Page Title="Ingreso de Productos" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoIngreso.aspx.cs" Inherits="PL.AdminDashboard.ProductoIngreso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Ingreso de Producto</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Depósito
                </li>
                <li class="active">
                    <strong>Ingreso de Producto</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormProductoIngreso" runat="server">
            <asp:ScriptManager ID="smgProductoIngreso" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Producto</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Proveedor</label>                                            
                                                <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="select2_proveedor form-control"></asp:DropDownList>                                            
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Producto</label>
                                            <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>    
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre</label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>                              
                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div style="text-align: right; padding-right: 15px;">
                                        <%--Verificar porque no funciona el validadores de jquery--%>
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
                                <button type="button" id="btnNuevo" runat="server" class="ladda-button btn btn-info" onserverclick="BtnNuevo_Click">
                                    <i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Nuevo Producto</span>
                                </button>
                            </div>
                            <br />
                            
                            <asp:ListView ID="lsvDiarios" runat="server" Visible="false">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>                                                                                                
                                                <th data-hide="phone,tablet">Día de semana</th>
                                                 <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                 <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>    
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <asp:Label ID="lblDiaSemana" runat="server" Text='<%#Eval("DIA_SEMANA").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaEdicion" style="width:100%">
                                                <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>    
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>       
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>                                                                                                
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaEdicion" style="width:100%">
                                                <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>    
                                        <td>
                                            <div id="dvDescripcion" style="width:100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>                                                                                                
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>      
                                        <td>
                                            <div id="dvDescripcion" style="width:100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>    
                                                <th>Autor</th>
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>     
                                        <td>
                                            <div id="dvDescripcion" style="width:100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>                                                                                                
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaEdicion" style="width:100%">
                                                <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>    
                                        <td>
                                            <div id="dvDescripcion" style="width:100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>                                                                                                
                                                <th data-hide="phone,tablet">Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Fecha Edici&oacute;n</th>
                                                <th data-hide="phone,tablet">Descripci&oacute;n</th>
                                                <th data-hide="phone,tablet">Precio</th>
                                                <th data-hide="phone,tablet">Cantidad</th>
                                                <th data-hide="phone,tablet">Fecha Devoluci&oacute;n</th>
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
                                            <div id="dvEdicion" style="width:100%">
                                                <asp:TextBox ID="txtEdicion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaEdicion" style="width:100%">
                                                <asp:TextBox ID="txtFechaEdicion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>    
                                        <td>
                                            <div id="dvDescripcion" style="width:100%">
                                                <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvPrecio" style="width:100%">
                                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                        <td>
                                            <div id="dvCantidad" style="width:100%">
                                                <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" MaxLength="5" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>   
                                        <td>
                                            <div id="dvFechaDevolucion" style="width:100%">
                                                <asp:TextBox ID="txtFechaDevolucion" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </td>                                         
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div id="dvMensajeLsvProductos" runat="server" />
                            <div class="hr-line-dashed"></div>
                            <div class="form-group" style="margin-bottom:50px">
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
    <script type="text/javascript">
        var FormProductoIngreso = '#<%=FormProductoIngreso.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {                
                //KeypressEnterDisabled();
                //LoadFootable();  
                ValidarForm();
                Select2();
            });
        }

        function ChangeTipoProducto() {
            $('#<%=ddlTipoProducto.ClientID%>').change(MostrarGrillaPorTipoProducto);
        }

        function MostrarGrillaPorTipoProducto() {
            $.ajax({
                type: "POST",
                url: "ProductoIngreso.aspx/MostrarGrillaPorTipoProducto",
                data: JSON.stringify({ 'idTipoProducto': $("#<%=ddlTipoProducto.ClientID%>").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: console.log("ok"),
                failure: function (response) {
                    console.log(response.d);
                }
             });
         }

        function KeypressEnterDisabled()
        {
            $('input').keypress(function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function ValidarForm() {
            $(FormProductoIngreso).validate({
                rules: {
                     <%=ddlProveedor.UniqueID%>: {
                         required: true
                     }
                },
                messages: {
                     <%=ddlProveedor.UniqueID%>: {
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
