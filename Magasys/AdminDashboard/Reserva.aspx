<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reserva.aspx.cs" Inherits="PL.AdminDashboard.Reserva" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormReserva" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Reserva</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li class="active">
                        <strong>Reserva</strong>
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
                                <label class="col-sm-2 control-label">Código de Reserva</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigoReserva" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Reserva</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaReserva" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group" id="dpFechaDesde">
                                <label class="col-sm-2 control-label">Fecha de inicio</label>
                                <div class="col-sm-10">
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group" id="dpFechaHasta">
                                <label class="col-sm-2 control-label">Fecha de fin</label>
                                <div class="col-sm-10">
                                    <div class="input-group date">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                        <asp:TextBox ID="txtFechaFin" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Forma de entrega</label>
                                <div class="col-sm-10">
                                    <div class="i-checks">
                                        <label>
                                            <input runat="server" type="radio" id="rdbRetiraEnLocal">
                                            <i></i>Retira en Local</label>
                                    </div>
                                    <div class="i-checks">
                                        <label>
                                            <input runat="server" type="radio" id="rdbEnvioDomicilio">
                                            <i></i>Envío a Domicilio</label>
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
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
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
                                    <asp:Button ID="btnBuscarCliente" runat="server" Text="Buscar" CssClass="btn btn-success" OnClick="BtnBuscarCliente_Click" formnovalidate="formnovalidate" />
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
            <div id="divProductoListado" class="ibox float-e-margins" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">
                                <br />
                                <asp:ListView ID="lsvProductos" runat="server" Visible="false">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                            <thead>
                                                <tr>
                                                    <td></td>
                                                    <th class="text-left">C&oacute;digo</th>
                                                    <th>Nombre</th>
                                                    <th>Tipo de Producto</th>
                                                    <th data-hide="phone,tablet">Descripci&oacute;n</th>
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
                                            <td>
                                                <input id="rdbCodigoProducto&<%#Eval("ID_PRODUCTO").ToString()%>" name="CodigoProducto" class="i-checks" type="radio"/>                                                
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCodigoProducto" runat="server" Text='<%#Eval("ID_PRODUCTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE").ToString().Remove(50).TrimEnd()):Eval("NOMBRE")%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("DESC_TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%#(Eval("DESCRIPCION") != null) ? Eval("DESCRIPCION").ToString().Length > 50 ? String.Format("{0}...", Eval("DESCRIPCION").ToString().Remove(50).TrimEnd()):Eval("DESCRIPCION"):null%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                                <asp:HiddenField ID="hfCodigoProducto" runat="server" />
                                <div id="dvMensajeLsvProductos" runat="server" />
                                <div class="form-group" style="margin-bottom: 10px">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardarReserva_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelarResera_Click" formnovalidate="" />
                                    </div>
                                </div>
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
        var FormReserva = '#<%=FormReserva.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    radioClass: 'iradio_square-green'
                }).on('ifChecked', function (e) {                    
                    var isChecked = e.currentTarget.checked;
                    var codigo = e.currentTarget.id.split('&')[1];                    
                    if (isChecked == true) {
                        $('#<%=hfCodigoProducto.ClientID%>').val(codigo); 
                    }
                });

                LoadFootable();
                LoadDatePicker();
                ValidarForm();
                Select2();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function LoadDatePicker() {
            $('#dpFechaDesde .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaHasta .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });
        }

        function ValidarForm() {
            $(FormReserva).validate({
                rules: {
                     <%=ddlProveedor.UniqueID%>: {
                    required: true
                     },
                     <%=ddlTipoProducto.UniqueID%>: {
                         required: true
                     },
                     <%=txtNroDocumento.UniqueID%>: {
                         required: true,
                         number: true,
                         digits: true,
                         minlength: 8
                     },
                     <%=txtFechaInicio.UniqueID%>: {
                          required: true
                     },
                },
        messages: {
                     <%=ddlProveedor.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                     <%=ddlTipoProducto.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                     <%=txtNroDocumento.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos.",
                        minlength: "Este campo debe ser de 8 dígitos."
                     },
                     <%=txtFechaInicio.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                 }
            });
        }

        function Select2() {
             $(".select2_tipodocumento").select2(
                {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
                 });

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
