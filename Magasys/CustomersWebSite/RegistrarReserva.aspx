<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarReserva.aspx.cs" Inherits="PL.CustomersWebSite.RegistrarReserva" %>
<%@ MasterType VirtualPath="~/CustomersWebSite/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/touchspin/jquery.bootstrap-touchspin.min.css" rel="stylesheet">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormRegistrarReserva" runat="server" class="form-horizontal">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-md-9">
                    <div class="ibox">
                        <div class="ibox-title">
                            <span class="pull-right">(<strong><asp:Label ID="lblCantidadItems" runat="server" Text="0"></asp:Label></strong>) items</span>
                            <h5>Productos de tu reserva</h5>
                        </div>
                        <div id="divIboxContent" class="ibox-content">
                            <asp:ListView ID="lsvProductos" runat="server" OnItemDataBound="lsvProductos_ItemDataBound">
                                <LayoutTemplate>
                                    <div class="row" id="itemPlaceholder" runat="server">
                                    </div>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div id="divTabla_<%#Eval("PRODUCTO_EDICION").ToString()%>" class="table-responsive">
                                        <table class="table shoping-cart-table">
                                            <tbody>
                                                <tr>
                                                    <td style="width: 90px">
                                                        <div class="cart-product-imitation" style="padding: 0px">
                                                            <img src="#" id="imgProducto" runat="server" style="width: 90px" />
                                                        </div>
                                                    </td>
                                                    <td class="desc">
                                                        <h3>
                                                            <a href="#" class="text-navy">
                                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE").ToString()%>'></asp:Label>
                                                                <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#(Eval("COD_PRODUCTO") != null) ? Eval("COD_PRODUCTO").ToString():null%>' style="display:none"></asp:Label>
                                                                <asp:Label ID="lblCodigoProductoEdicion" runat="server" Text='<%#(Eval("COD_PRODUCTO_EDICION") != null) ? Eval("COD_PRODUCTO_EDICION").ToString():null%>' style="display:none"></asp:Label>
                                                                <asp:Label ID="lblCodigoTipoProducto" runat="server" Text='<%#Eval("COD_TIPO_PRODUCTO").ToString()%>' style="display:none"></asp:Label>
                                                            </a>
                                                        </h3>
                                                        <p class="small">
                                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString():null%>'></asp:Label>
                                                        </p>
                                                        <p class="small">
                                                            <asp:Label ID="lblFechaEdicion" runat="server" Text='<%#(Eval("FECHA_EDICION") != null) ? Eval("FECHA_EDICION").ToString():null%>'></asp:Label>
                                                        </p>
                                                        <dl class="small m-b-none">
                                                            <div id="divPrecio" runat="server">
                                                                <dt>Precio unitario</dt>
                                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO").ToString()%>'></asp:Label>
                                                            </div>
                                                        </dl>
                                                        <dl class="small m-b-none">
                                                            <dt>Forma de entrega</dt>
                                                            <div class="i-checks">
                                                                <input runat="server" type="radio" id="rdbRetiraEnLocal" checked>
                                                                Retira en Local&nbsp;
                                                                    <input runat="server" type="radio" id="rdbEnvioDomicilio">
                                                                Envío a Domicilio
                                                            </div>                                                            
                                                        </dl>
                                                        <dl class="small m-b-none">
                                                            <div id="divFechas" runat="server" visible="false">
                                                                <div class="col-sm-5" style="margin-right: 20px">
                                                                    <div class="form-group" id="dpFechaInicio">
                                                                        <label class="control-label">Fecha Inicio de Reserva</label>
                                                                        <div class="input-group date">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            <asp:TextBox ID="txtFechaIniReserva" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-5" style="margin-right: 20px">
                                                                    <div class="form-group" id="dpFechaFin">
                                                                        <label class="control-label">Fecha Fin de Reserva</label>
                                                                        <div class="input-group date">
                                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            <asp:TextBox ID="txtFechaFinReserva" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </dl>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <div id="divCantidad" runat="server" data-id="divCantidad">
                                                            <asp:TextBox ID="txtCantidad" runat="server" Text='<%#Eval("CANTIDAD").ToString()%>' autocomplete="off" title='<%#string.Format("{0};{1}", Eval("PRECIO").ToString(), Eval("PRODUCTO_EDICION").ToString())%>' Enabled="True" MaxLength="0" ReadOnly="True" BackColor="White"></asp:TextBox>
                                                        </div>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <div id="divSubtotal" runat="server">
                                                            <h4>
                                                                <dt>Subtotal</dt>
                                                                <div id="divSubTotal_<%#Eval("PRODUCTO_EDICION").ToString()%>">
                                                                    <asp:Label ID="lblSubTotal" runat="server" Text='<%#Eval("SUBTOTAL").ToString()%>'></asp:Label>
                                                                </div>
                                                            </h4>
                                                        </div>
                                                    </td>
                                                    <td style="width: 150px">
                                                        <a class="btn btn-outline btn-danger" title="Eliminar" onclick="EliminarProducto(this);">Eliminar&nbsp;<i class="fa fa-trash"></i>
                                                            <asp:HiddenField ID="hdCodProducto" runat="server" Value='<%#Eval("PRODUCTO_EDICION").ToString()%>' />
                                                        </a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                            <asp:HiddenField ID="hfIdUsuarioLogueado" runat="server" />
                        </div>
                        <div class="ibox-content">                            
                            <a class="btn btn btn-warning pull-right" onclick="Cancelar();">Cancelar</a>
                            <a class="btn btn-primary pull-right" onclick="ConfirmarReserva();" style="margin-right: 3px;">Confirmar Reserva&nbsp;<i class="fa fa fa-shopping-cart"></i></a>
                            <a class="btn btn-white" href="Reserva.aspx"><i class="fa fa-arrow-left"></i>&nbsp;Seguir reservando
                            </a>
                        </div>
                    </div>
                </div>
                <div class="col-md-3">
<%--                    <div class="ibox">
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
                    </div>--%>

                    <div class="ibox">
                        <div class="ibox-title">
                            <h5>Soporte</h5>
                        </div>
                        <div class="ibox-content text-center">
                            <h3><i class="fa fa-phone"></i>+54 9 351 611-3752</h3>
                            <span class="small">Póngase en contacto con nosotros si tiene alguna pregunta. Estamos disponibles las 24h.
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
    <!-- Sweet alert -->
    <script src="js/plugins/sweetalert/sweetalert.min.js"></script>

    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {
                LoadDatePicker();                
            });
        }

        function LoadDatePicker() {
            $('#dpFechaInicio .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaFin .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });
        }        

        $('.i-checks').iCheck({
            radioClass: 'iradio_square-green'
        });               

        $('[data-id="divCantidad"] > input').TouchSpin({
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
                DescontarCantidadDeItems();
                var loCodigo = control.lastElementChild.defaultValue;
                $('#divTabla_' + loCodigo).remove();
            }
        }

        function ConfirmarReserva() {

            var loReservas = [];

            var loIdUsuarioLogueado = $('#<%=hfIdUsuarioLogueado.ClientID%>').val();

            loReservas.push("IdUsuario:" + loIdUsuarioLogueado + ";");

            $('#divIboxContent > div > table > tbody > tr').each(function () {
                var loFormaDeEntrega = "D";
                if (this.cells[1].children[4].children[1].children[0].className == "iradio_square-green checked") {
                    loFormaDeEntrega = "L"
                }

                var loCodProducto = "";

                if (this.cells[1].children[0].children[0].children[1].textContent.trim() != "") {
                    loCodProducto = this.cells[1].children[0].children[0].children[1].textContent.trim();
                }

                var loCodProductoEdicion = "";

                if (this.cells[1].children[0].children[0].children[2].textContent.trim() != "") {
                    loCodProductoEdicion = this.cells[1].children[0].children[0].children[2].textContent.trim();
                }

                var loCodTipoProducto = "";

                if (this.cells[1].children[0].children[0].children[3].textContent.trim() != "") {
                    loCodTipoProducto = this.cells[1].children[0].children[0].children[3].textContent.trim();
                }

                var loCantidad = "";

                if (this.cells[2].childElementCount > 0) {
                    loCantidad = this.cells[2].children[0].children[0].children[1].value.trim();
                }

                var loFechaInicio = "";
                var loFechaFin = "";

                if (this.cells[1].children[5].childElementCount > 0) {

                    loFechaInicio = this.cells[1].children[5].children[0].children[0].children[0].children[1].children[1].value.trim();
                    loFechaFin = this.cells[1].children[5].children[0].children[1].children[0].children[1].children[1].value.trim();
                }

                loReservas.push(loCodProducto + ";" + loCodProductoEdicion + ";" + loFormaDeEntrega + ";" + loCantidad + ";" + loFechaInicio + ";" + loFechaFin + ";" + loCodTipoProducto);
            });           
            if (loReservas.length > 1) {
                ValidarReserva(loReservas);
            }
            else {
                swal({
                    title: "Confirmación Reserva",
                    text: "No se disponen de reservas para confirmar.",
                    type: "warning",
                    confirmButtonText: 'Aceptar'
                });
            }
        }

        function ValidarReserva(loReservas) {
            $.ajax({
                type: "POST",
                url: "RegistrarReserva.aspx/ValidarReserva",
                data: JSON.stringify({ 'pReservas': loReservas }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var msg = JSON.parse(data.d);
                    if (msg == 1) {
                        GuardarReserva(loReservas);
                    }
                    else if (msg == 2) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "La forma de entrega “Envío a Domicilio” requiere que el cliente complete los datos de la dirección.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                    else if (msg == 3) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "La Fecha de fin debe ser mayor que la Fecha de inicio.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                    else if (msg == 4) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "Se debe registrar como cliente para tomar la reserva.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                    else if (msg == 5) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "Se debe ingresar la Fecha Inicio de Reserva.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                    else if (msg == 6) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "La Fecha de inicio debe ser mayor o igual que la fecha actual.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                },
                failure: function (data) {
                    swal({
                        title: "Confirmación Reserva",
                        text: "La reserva no se pudo guardar.",
                        type: "warning",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }

        function GuardarReserva(loReservas) {
            $.ajax({
                type: "POST",
                url: "RegistrarReserva.aspx/GuardarReserva",
                data: JSON.stringify({ 'pReservas': loReservas }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var msg = JSON.parse(data.d);
                    if (msg) {
                        swal({
                            title: "Confirmación Reserva",
                            text: "La reserva se guardó correctamente.",
                            type: "success",
                            confirmButtonText: 'Aceptar',
                        }, function () {
                            var loLocation = window.location;
                            var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);
                            var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));
                            window.location.href = url + 'Reserva.aspx';
                        });
                    }
                    else {
                        swal({
                            title: "Confirmación Reserva",
                            text: "La reserva no se pudo guardar.",
                            type: "warning",
                            confirmButtonText: 'Aceptar'
                        });
                    }
                },
                failure: function (data) {
                    swal({
                        title: "Confirmación Reserva",
                        text: "La reserva no se pudo guardar.",
                        type: "warning",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }

        function Cancelar() {
            $.ajax({
                type: "POST",
                url: "RegistrarReserva.aspx/Cancelar",                
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var msg = JSON.parse(data.d);
                    if (msg) {
                        var loLocation = window.location;
                        var loPathName = loLocation.pathname.substring(0, loLocation.pathname.lastIndexOf('/') + 1);
                        var url = loLocation.href.substring(0, loLocation.href.length - ((loLocation.pathname + loLocation.search + loLocation.hash).length - loPathName.length));
                        window.location.href = url + 'Reserva.aspx';
                    }                    
                }
            });
        }

        function DescontarCantidadDeItems() {
            var lblCantidad = $('#<%=lblCantidadItems.ClientID%>');
            var loCantidad = lblCantidad.text();
            var loOperacion = parseInt(loCantidad, 10) - 1;
            lblCantidad.text(loOperacion);
        }       

    </script>
</asp:Content>
