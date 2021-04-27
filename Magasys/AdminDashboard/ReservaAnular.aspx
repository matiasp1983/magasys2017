<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaAnular.aspx.cs" Inherits="PL.AdminDashboard.ReservaAnular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Anular Reservas</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Reserva
                </li>
                <li class="active">
                    <strong>Anular Reservas</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormReservaAnular" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Reserva</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Reserva</label>   
                                            <div id="divTipoReserva">
                                                <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="select2_tiporeserva form-control"></asp:DropDownList>                                            
                                            </div>   
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre del Producto</label>
                                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Estado</label>   
                                            <div id="divEstado">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select2_estado form-control"></asp:DropDownList>                                            
                                            </div>   
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">      
                                <div class="ibox-title">
                                    <h5>Cliente</h5>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Documento</label>
                                            <div id="divTipoDocumento">
                                                <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Número de Documento</label>
                                            <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre</label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Apellido</label>
                                            <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Alias</label>
                                            <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div style="text-align: right; padding-right: 15px;">
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
                            <asp:ListView ID="lsvReservas" runat="server" Visible="true">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>                                                    
                                                <td>                                                        
                                                    <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                </td>
                                                <th class="text-left">Reserva</th>    
                                                <th data-hide="phone,tablet">Cliente</th>
                                                <th class="text-left">Producto</th>
                                                <th data-hide="phone,tablet">Tipo de Reserva</th>
                                                <th data-hide="phone,tablet">Estado</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="30">
                                                    <ul class="pagination pull-right"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>                                                
                                            <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" Visible="true"/>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoReserva" runat="server" Text='<%#Eval("TIPO_RESERVA").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("ESTADO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvReservas" runat="server" />
                            <asp:HiddenField ID="hfCodigoReserva" runat="server"/>
                            <div class="form-group" style="margin-bottom: 10px">
                                <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                <div class="col-xs-12 col-md-4" style="text-align: right">
                                    <asp:Button ID="btnGuardar" runat="server" Text="Confirmar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
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
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <script type="text/javascript">

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
                Select2();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function Select2() {
            $(".select2_tiporeserva").select2(
                {
                    placeholder: 'Seleccione un Tipo de Reserva',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_estado").select2(
                {
                    placeholder: 'Seleccione un Estado',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_tipodocumento").select2(
                {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
                });
        }
    </script>
</asp:Content>
