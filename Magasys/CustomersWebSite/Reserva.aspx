<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="PL.CustomersWebSite.Reserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/switchery/switchery.css" rel="stylesheet">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormReserva" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Selección de Productos</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label">Tipo de Producto</label>
                                            <div id="divTipoProducto">
                                                <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Nombre Producto</label>
                                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Descripción</label>
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label">Género</label>
                                            <div id="divProveedor">
                                                <asp:DropDownList ID="ddlGenero" runat="server" CssClass="select2_genero form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="hr-line-dashed"></div>
                                    <div class="form-group">
                                        <div style="text-align: right; padding-right: 15px;">
                                            <a id="btnBuscarSuccess" class="ladda-button btn btn-success" style="background-color: #1c84c6; color: #FFFFFF; border-color: #1c84c6; border-radius: 3px; height: 33px; width: 100px; padding-left: 10px; padding-top: 0px;">
                                                <i class="fa fa-search"></i>
                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="ladda-button btn btn-success" OnClick="BtnBuscar_Click" Style="padding: 4px 5px" />
                                            </a>
                                            <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                                <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divReserva" runat="server">
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
                                                <input id="chkCodigoProducto" runat="server" class="i-checks" type="checkbox" title='<%#string.Format("{0};{1}", Eval("COD_PRODUCTO").ToString(), Eval("PRECIO").ToString())%>' />
                                            </div>
                                            <div class="product-imitation" style="padding: 0px">
                                                <img id="imgProducto" runat="server" style="width: 200px" />
                                            </div>
                                            <div class="product-desc">
                                                <div id="divPrecio" runat="server">
                                                    <span class="product-price">
                                                        <asp:Label ID="lblPrecio" runat="server" Text='<%#(Eval("PRECIO") != null) ? string.Format("${0}",Eval("PRECIO").ToString()):string.Empty%>'></asp:Label>
                                                    </span>
                                                </div>
                                                <div style="font-size: 14px; font-weight: 600; color: #1ab394;">
                                                    <asp:Label ID="lblNombreProducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString()%>'></asp:Label>
                                                </div>
                                                <p class="text-success">
                                                    <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString():null%>'></asp:Label>
                                                </p>
                                                <p class="text-muted">
                                                    <asp:Label ID="lblNombreDiario" runat="server" Text='<%#(Eval("NOMBRE_DIARIO") != null) ? Eval("NOMBRE_DIARIO").ToString():null%>'></asp:Label>
                                                </p>
                                                <p class="text-muted">
                                                    <asp:Label ID="lblEditorial" runat="server" Text='<%#(Eval("EDITORIAL") != null) ? Eval("EDITORIAL").ToString():null%>'></asp:Label>
                                                </p>
                                                <p class="text-muted">
                                                    <asp:Label ID="lblAutor" runat="server" Text='<%#(Eval("AUTOR") != null) ? Eval("AUTOR").ToString():null%>'></asp:Label>
                                                </p>
                                                <p class="text-muted">
                                                    <asp:Label ID="lblAnio" runat="server" Text='<%#(Eval("ANIO") != null) ? Eval("ANIO").ToString():null%>'></asp:Label>
                                                </p>
                                                <div class="m-t text-righ">
                                                    <button type="reset" id="btnEdiciones" runat="server" onserverclick="BtnMostrarEdiciones_Click" class="btn btn-xs btn-outline btn-primary">
                                                        <a href="#">VER EDICIONES <i class="fa fa-long-arrow-right"></i></a>
                                                    </button>
                                                </div>
                                                <div class="m-t text-righ">
<%--                                                    <button type="reset" id="btnReservarEdic" class="btn btn-xs btn-outline btn-primary" data-toggle="modal" data-target="#ModalProducto">RESERVAR EDICIONES
                                                        <i class="fa fa-long-arrow-right"></i>
                                                    </button>--%>
                                                    <a id="btnReservarEdic" href="#" class="btn btn-xs btn-outline btn-primary" data-toggle="modal" data-target="#ModalProducto">RESERVAR EDICIONES <i class="fa fa-long-arrow-right"></i> </a>
                                                </div>
                                                <div id="ModalProducto" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content animated bounce">
                                                            <div class="modal-body">
                                                                <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #F8BB86;">
                                                                    <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #F8BB86;"></span>
                                                                    <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #F8BB86;"></span>
                                                                </div>
                                                                <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Quiere reservar todas las Ediciones?</h2>
<%--                                                                <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                                                    Se dará de baja el proveedor con CUIT: 
                                           
                                                                </p>--%>
                                                            </div>
                                                            <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                                                <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                                    Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                                    data-dismiss="modal" />
                                                                <asp:Button ID="btnReservarEdiciones" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #DD6B55"
                                                                    OnClick="BtnReservarEdiciones_Click" />
                                                            </div>
                                                        </div>
                                                        <asp:HiddenField ID="hdIdProducto" runat="server" Value="" />
                                                    </div>
                                                </div>
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
        var FormReserva = '#<%=FormReserva.ClientID%>';
        var loTotal = parseFloat($('#<%=lblTotalAbonar.ClientID%>').text());

        if (window.jQuery) {
            $(document).ready(function () {
                CargarICheck();
                ValidarForm();
                Select2();
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

        function ValidarForm() {
            $(FormReserva).validate({
                rules: {
                     <%=ddlTipoProducto.UniqueID%>: {
                    required: true
                },
                },
        messages: {
                     <%=ddlTipoProducto.UniqueID%>: {
                required: "Este campo es requerido."
            },
        }
           });
        }

        function Select2() {
            $(".select2_genero").select2(
                {
                    placeholder: 'Seleccione un Género',
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
