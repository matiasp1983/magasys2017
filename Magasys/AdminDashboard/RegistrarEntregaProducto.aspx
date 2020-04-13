<%@ Page Title="Registrar Entrega de Producto" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="RegistrarEntregaProducto.aspx.cs" Inherits="PL.AdminDashboard.RegistrarEntregaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormRegistrarEntregaProducto" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Registrar Entrega de Producto</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li class="active">
                        <strong>Registrar Entrega de Producto</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Buscar Cliente</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <div id="divTipoDocumento">
                                        <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
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
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div style="text-align: right; padding-right: 15px;">
                            <a id="btnBuscarSuccess" class="ladda-button btn btn-success" style="background-color: #1c84c6; color: #FFFFFF; border-color: #1c84c6; border-radius: 3px; height: 33px; width: 100px; padding-left: 10px; padding-top: 0px;">
                                <i class="fa fa-search"></i>
                                <asp:Button ID="btnBuscarProducto" runat="server" Text="Buscar" CssClass="ladda-button btn btn-success" OnClick="BtnBuscar_Click" Style="padding: 4px 5px" />
                            </a>
                            <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="divMensajeEntregaProducto" class="ibox float-e-margins" runat="server">
                <div id="dvMensajeLsvEntregaProducto" runat="server" />  
            </div>
            <div id="divEntregaProducto" class="ibox float-e-margins" runat="server">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                            <div class="ibox-content">                                
                                <asp:ListView ID="lsvReservas" runat="server" OnItemDataBound="LsvReservas_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="5">
                                            <thead>
                                                <tr>
                                                    <td>                                                        
                                                        <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                    </td>
                                                    <th class="text-left">Código de Reserva</th>
                                                    <th class="text-left">Producto</th>
                                                    <th data-hide="phone,tablet">Edición</th>
                                                    <th data-hide="phone,tablet">Precio</th>
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
                                            <td>                                                
                                                <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" title='<%#string.Format("{0}", Eval("PRECIO_EDICION").ToString())%>' />
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblCodigoReserva" runat="server" Text='<%#Eval("ID_RESERVA_EDICION").ToString()%>'></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNombreProducto" runat="server" Text='<%#(Eval("NOMBRE_PRODUCTO") != null) ? Eval("NOMBRE_PRODUCTO"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblDescripcionEdicion" runat="server" Text='<%#(Eval("DESC_EDICION") != null) ? Eval("DESC_EDICION"):null%>'></asp:Label>
                                            </td>
                                            <td class="text-left">
                                                <asp:Label ID="lblPrecio" runat="server" Text='<%#Eval("PRECIO_EDICION").ToString()%>'></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>    
                                <asp:HiddenField ID="hfCodigoReserva" runat="server"/>                                
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <h2 class="col-sm-4 control-label font-bold">Total $</h2>
                                            <div class="col-sm-1 control-label font-bold">
                                                <h2>
                                                    <asp:Label ID="lblTotal" runat="server" autocomplete="off"></asp:Label></h2>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <hr />
                                <div class="form-group" style="margin-bottom: 10px">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardarEntrega_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelarEntrega_Click" formnovalidate="" />
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
        var FormRegistrarEntregaProducto = '#<%=FormRegistrarEntregaProducto.ClientID%>';
        var loTotal = parseFloat($('#<%=lblTotal.ClientID%>').text());

        if (window.jQuery) {
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green'
                }).on('ifChecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    var codigo = e.currentTarget.id.split('&')[1] + ";";

                    if (isChecked == true) {
                        if ($('#<%=hfCodigoReserva.ClientID%>').val() == "") {
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigo);
                        } else {
                            var codigoAnterior = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(codigoAnterior + codigo);
                        }

                        var loPrecio = e.currentTarget.title;
                        loTotal += parseFloat(loPrecio);
                        $('#<%=lblTotal.ClientID%>').text(loTotal);
                    }
                    });

                $('.i-checks').on('ifUnchecked', function (e) {
                    var isChecked = e.currentTarget.checked;
                    var codigo = e.currentTarget.id.split('&')[1] + ";";

                    if (isChecked == false) {
                        if ($('#<%=hfCodigoReserva.ClientID%>').val() != "") {
                            var mm = $('#<%=hfCodigoReserva.ClientID%>').val();
                            $('#<%=hfCodigoReserva.ClientID%>').val(mm.replace(codigo, "").trim());
                        }

                        var loPrecio = e.currentTarget.title;
                        loTotal -= parseFloat(loPrecio);
                        $('#<%=lblTotal.ClientID%>').text(loTotal);
                    }
                });

                var checked = false;

                $('#check-all').on('click', function () {
                    if (checked == false) {
                        $('.i-checks').prop('checked', true).iCheck('update');
                        $('#<%=hfCodigoReserva.ClientID%>').val('');

                        $('.i-checks').each(function () {
                            $('#<%=hfCodigoReserva.ClientID%>').val($('#<%=hfCodigoReserva.ClientID%>').val() + (this.id.split('&')[1] + ";"));
                        });

                        checked = true;
                    } else {
                        $('.i-checks').prop('checked', false).iCheck('update');
                        $('#<%=hfCodigoReserva.ClientID%>').val('');
                        checked = false;
                    }

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

            $(FormRegistrarEntregaProducto).validate({
                rules: {
                    <%=txtNroDocumento.UniqueID%>: {
                    required: true,
                    number: true,
                    digits: true,
                    minlength: 8
                    }                  
				},
        messages: {
                    <%=txtNroDocumento.UniqueID%>: {
                    required: "Este campo es requerido.",
                    number: "Ingrese un número válido.",
                    digits: "Ingrese solo dígitos.",
                    minlength: "Este campo debe ser de 8 dígitos."
                    }
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
        }

    </script>
</asp:Content>
