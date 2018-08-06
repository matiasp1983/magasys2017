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
                                                <th class="text-left">C&oacute;digo</th>
                                                <th>Nombre</th>
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
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="hr-line-dashed"></div>
                            <div class="form-group" style="margin-bottom: 50px">
                                <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                <div class="col-xs-12 col-md-4" style="text-align: right">
                                    <a href="javascript:history.go(-1)" class="btn btn-primary">Volver</a>
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
