<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarReserva.aspx.cs" Inherits="PL.CustomersWebSite.RegistrarReserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/touchspin/jquery.bootstrap-touchspin.min.css" rel="stylesheet">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormRegistrarReserva" runat="server" class="form-horizontal">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-md-9">
                    <div class="ibox">
                        <div class="ibox-title">
                            <span class="pull-right">(<strong>5</strong>) items</span>
                            <h5>Productos de tu reserva</h5>
                        </div>
                        <div id="divIboxContent" class="ibox-content">
                            <asp:ListView ID="lsvProductos" runat="server" OnItemDataBound="lsvProductos_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="row" id="itemPlaceholder" runat="server">
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div id="divTabla_<%#Eval("COD_PRODUCTO").ToString()%>" class="table-responsive">
                                        <table class="table shoping-cart-table">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 90px">
                                                        <div class="cart-product-imitation" style="padding: 0px">
                                                            <img id="imgProducto" runat="server" style="width: 90px" />
                                                        </div>
                                                    </td>
                                                    <td class="desc">
                                                        <h3>
                                                            <a href="#" class="text-navy">
                                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#(Eval("COD_PRODUCTO") != null) ? Eval("COD_PRODUCTO").ToString():null%>' Visible="False"></asp:Label>
                                                                <asp:Label ID="lblCodigoProductoEdicion" runat="server" Text='<%#(Eval("COD_PRODUCTO_EDICION") != null) ? Eval("COD_PRODUCTO_EDICION").ToString():null%>' Visible="False"></asp:Label>
                                                            </a>
                                                        </h3>
                                                        <p class="small">
                                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("DESCRIPCION").ToString()%>'></asp:Label>
                                                        </p>
                                                        <p class="small">
                                                            <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#(Eval("FECHA_EDICION") != null) ? Eval("FECHA_EDICION").ToString():null%>'></asp:Label>
                                                        </p>
                                                        <dl class="small m-b-none">
                                                            <dt>Precio unitario</dt>
                                                            <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                                        </dl>
                                                        <dl class="small m-b-none">
                                                            <dt>Forma de entrega</dt>
                                                            <div class="i-checks">
                                                                <input runat="server" type="radio" id="rdbRetiraEnLocal" checked>
                                                                Retira en Local&nbsp;
                                                                    <input runat="server" type="radio" id="rdbEnvioDomicilio">
                                                                Envío a Domicilio
                                                            </div>
                                                            <div id="divFormaDeEntrega_<%#Eval("COD_PRODUCTO").ToString()%>">
                                                                 <asp:HiddenField ID="hdFormaDeEntrega" runat="server"/>
                                                            </div>
                                                        </dl>
                                                        <div class="m-t-sm">
                                                            <a class="btn btn-outline btn-default btn-xl" title="Eliminar" onclick="EliminarProducto(this);">Eliminar&nbsp;<i class="fa fa-trash"></i>
                                                                <asp:HiddenField ID="hdCodProducto" runat="server" Value='<%#Eval("COD_PRODUCTO").ToString()%>' />
                                                            </a>
                                                        </div>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <div id="divCantidad">
                                                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%#Eval("CANTIDAD").ToString()%>' autocomplete="off" title='<%#string.Format("{0};{1}", Eval("PRECIO").ToString(), Eval("COD_PRODUCTO").ToString())%>' Enabled="True" MaxLength="0" ReadOnly="True"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <h4>
                                                            <dt>Subtotal</dt>
                                                            <div id="divSubTotal_<%#Eval("COD_PRODUCTO").ToString()%>">
                                                                <asp:Label ID="lblSubTotal" runat="server" Text='<%#Eval("SUBTOTAL").ToString()%>'></asp:Label>
                                                            </div>
                                                        </h4>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="ibox-content">
                            <a class="btn btn-primary pull-right" onclick="ConfirmarReserva();">Confirmar Reservar&nbsp;<i class="fa fa fa-shopping-cart"></i>
                            </a>
                            <button class="btn btn-white"><i class="fa fa-arrow-left"></i>Continue shopping</button>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="ibox">
                        <div class="ibox-title">
                            <h5>Cart Summary</h5>
                        </div>
                        <div class="ibox-content">
                            <span>Total
                            </span>
                            <h2 class="font-bold">$390,00
                            </h2>

                            <hr />
                            <span class="text-muted small">*For United States, France and Germany applicable sales tax will be applied
                            </span>
                            <div class="m-t-sm">
                                <div class="btn-group">
                                    <a href="#" class="btn btn-primary btn-sm"><i class="fa fa-shopping-cart"></i>Checkout</a>
                                    <a href="#" class="btn btn-white btn-sm">Cancel</a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox">
                        <div class="ibox-title">
                            <h5>Support</h5>
                        </div>
                        <div class="ibox-content text-center">
                            <h3><i class="fa fa-phone"></i>+43 100 783 001</h3>
                            <span class="small">Please contact with us if you have any questions. We are avalible 24h.
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <!-- TouchSpin -->
    <script src="js/plugins/touchspin/jquery.bootstrap-touchspin.min.js"></script>
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <script type="text/javascript">

        $('.i-checks').iCheck({
            radioClass: 'iradio_square-green'
        });

        $('#divCantidad > input').TouchSpin({
            verticalbuttons: true,
            min: 1,
            max: 99
        }).on('touchspin.on.startspin', function (e) {

            var loCantidad = parseInt(e.currentTarget.value);
            var loPrecio = parseFloat(e.currentTarget.title.split(';')[0].replace('$', '').replace(',', '.'));
            var loCodigoProducto = e.currentTarget.title.split(';')[1];
            var loSubtotal = parseFloat(loPrecio * loCantidad);

            $('#divSubTotal_' + loCodigoProducto).html('$' + loSubtotal.toFixed(2).replace('.', ','));
        });

        function EliminarProducto(control) {
            if (window.jQuery) {
                var loCodigo = control.lastElementChild.defaultValue;
                $('#divTabla_' + loCodigo).remove();
            }
        }

        function ConfirmarReserva() {

            var loReservas = [];

            $('#divIboxContent > div > table > tbody > tr').each(function () {
                var loFormaDeEntrega = "D";
                if (this.cells[1].children[4].children[1].children[0].className == "iradio_square-green checked") {
                    loFormaDeEntrega = "L"
                }
                var loCantidad = this.cells[2].children[0].children[0].children[1].value.trim();
                var loSubTotal = this.cells[3].children[0].children[1].textContent.replace("$",'').trim();

                loReservas.push(loFormaDeEntrega + ";" + loCantidad + ";" + loSubTotal);
            });

            console.log(loReservas);

            /*var lommm;
            $.ajax({
                type: "POST",
                url: "RegistrarReserva.aspx/GuargarReserva",
                data: JSON.stringify({ 'mmm': lommm }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response.d);
                },
                failure: function (response) {
                   console.log(response.d);
                }
            });*/
        }

    </script>
</asp:Content>
