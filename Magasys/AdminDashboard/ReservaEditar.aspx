<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaEditar.aspx.cs" Inherits="PL.AdminDashboard.ReservaEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Datos de la Reserva</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Reserva
                </li>
                <li class="active">
                    <strong>Editar Reserva</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormReservaEditar" runat="server" class="form-horizontal">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha Alta</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Estado</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Reserva</label>
                                <div class="col-sm-10">
                                    <div id="divTipoReserva">
                                        <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
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
                    <h2>Cliente</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
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
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Producto</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Descripción</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcionProducto" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
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
        var FormReservaEditar = '#<%=FormReservaEditar.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    radioClass: 'iradio_square-green'
                });
                LoadFootable();
                LoadDatePicker();
                ValidarForm();
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
            $(FormReservaEditar).validate({
                rules: {
                     <%=txtFechaInicio.UniqueID%>: {
                          required: true
                     },
                },
                messages: {
                     <%=txtFechaInicio.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                 }
            });
        }

    </script>
</asp:Content>
