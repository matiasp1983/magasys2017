<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaRevista.aspx.cs" Inherits="PL.CustomersWebSite.ReservaRevista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/switchery/switchery.css" rel="stylesheet">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormReservaColeccion" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <div class="widget style1 lazur-bg">
                                                <div class="row vertical-align">
                                                    <div class="col-xs-12 text-center">
                                                        <span class="font-bold" style="font-size: 18px">
                                                            <b class="font-bold" style="font-size: 18px">TOTAL A ABONAR $ 
                                                            <asp:Label ID="lblTotalAbonar" runat="server" Text=''></asp:Label></b>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSeleccionarTodo" runat="server" Text="SELECCIONAR TODO" CssClass="btn btn-block btn-outline btn-rounded btn-success" OnClick="BtnSeleccionarTodo_Click" />                                           
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnDeseleccionarTodo" runat="server" Text="DESELECCIONAR TODO" CssClass="btn btn-block btn-outline btn-rounded btn-success" OnClick="BtnDeseleccionarTodo_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="text-center">
                                        <br />
                                        <div class="form-group">
                                            <asp:Button ID="btnReservar" runat="server" Text="RESERVAR" CssClass="btn btn-primary" OnClick="BtnReservar_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <asp:ListView ID="lsvProductos" runat="server" OnItemDataBound="lsvProductos_ItemDataBound">
                            <LayoutTemplate>
                                <div class="row" id="itemPlaceholder" runat="server">
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <div class="col-md-3">
                                    <div class="ibox">
                                        <div class="ibox-content product-box">
                                            <div style="text-align: right; padding-right: 25px; padding-top: 10px; padding-bottom: 10px;">
                                                <input id="chkCodigoProductoEdicion" runat="server" class="i-checks" type="checkbox" title='<%#string.Format("{0};{1}", Eval("COD_PRODUCTO_EDICION").ToString(), Eval("PRECIO").ToString())%>' />
                                            </div>
                                            <div class="product-imitation" style="padding: 0px">
                                                <img id="imgProducto" runat="server" style="width: 200px" />
                                            </div>
                                            <div class="product-desc">
                                                <span class="product-price">
                                                    <asp:Label ID="lblPrecio" runat="server" Text='<%#(Eval("PRECIO") != null) ? string.Format("${0}",Eval("PRECIO").ToString()):string.Empty%>'></asp:Label>
                                                </span>
                                                <div style="font-size: 14px; font-weight: 600; color: #1ab394;">
                                                    <asp:Label ID="lblEdicion" runat="server" Text='<%#string.Format("Edición: {0}",Eval("EDICION").ToString())%>'></asp:Label>
                                                </div>
                                                <p class="text-success">
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString():null%>'></asp:Label>
                                                </p>
                                                <p class="text-muted">
                                                    <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#string.Format("Fecha Edición: {0}",Eval("FECHA_EDICION").ToString())%>'></asp:Label>
                                                </p>
                                                <p class="text-success">
                                                    <asp:Label ID="lblCodProducto" runat="server" Text='<%#Eval("COD_PRODUCTO").ToString()%>' Visible ="false"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <script type="text/javascript">
        var loTotal = parseFloat($('#<%=lblTotalAbonar.ClientID%>').text());

        if (window.jQuery) {
            $(document).ready(function () {
                CargarICheck();
            });
        }

        function CargarICheck() {
            $('.i-checks').iCheck({
                checkboxClass: 'icheckbox_square-green'
            }).on('ifChecked', function (e) {
                var isChecked = e.currentTarget.checked;
                var loPrecio = e.currentTarget.title.split(';')[1].replace(',', '.');

                if (isChecked == true) {
                    loTotal += parseFloat(loPrecio);
                    $('#<%=lblTotalAbonar.ClientID%>').text(loTotal.toFixed(2).replace('.', ','));
                }
                });

            $('.i-checks').on('ifUnchecked', function (e) {
                var isChecked = e.currentTarget.checked;
                var loPrecio = e.currentTarget.title.split(';')[1].replace(',', '.');

                if (isChecked == false) {
                    loTotal -= parseFloat(loPrecio);
                    $('#<%=lblTotalAbonar.ClientID%>').text(loTotal.toFixed(2).replace('.', ','));
                }
            });
        }
      
    </script>
</asp:Content>
