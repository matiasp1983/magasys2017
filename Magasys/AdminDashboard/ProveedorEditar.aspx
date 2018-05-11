﻿<%@ Page Title="Datos del ProveedorEditar" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProveedorEditar.aspx.cs" Inherits="PL.AdminDashboard.ProveedorEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormProveedorEditar" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Proveedor</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Proveedores
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Editar Proveedor"></asp:Label>
                        </strong>
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
                                <label class="col-sm-2 control-label">CUIT</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" Enabled="false" MaxLength="11" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Razón Social</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Movil</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divTelefonoMovil">
                                        <span class="input-group-addon"><i class="fa fa-mobile-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoMovil" runat="server" CssClass="form-control" MaxLength="11" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03516243492.</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Fijo</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divTelefonoFijo">
                                        <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoFijo" runat="server" CssClass="form-control" MaxLength="11" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03514823455.</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Email</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divEmail">
                                        <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Dirección</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Calle</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-md-2 control-label">Número</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Piso</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" MaxLength="2" autocomplete="off"></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Dpto</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" MaxLength="2" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Provincia</label>

                                <div class="col-sm-10">
                                    <div id="divProvincia">
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="select2_provincia form-control" OnSelectedIndexChanged="DdlProvincia_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Localidad</label>

                                <div class="col-sm-10">
                                    <div id="divLocalidad">
                                        <asp:DropDownList ID="ddlLocalidad" runat="server" CssClass="select2_localidad form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Barrio</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtBarrio" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código Postal</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
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
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">      
        var FormProveedorEditar = '#<%=FormProveedorEditar.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Select2();
            });
        }

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormProveedorEditar).validate({
                rules: {                     
                    <%=txtRazonSocial.UniqueID%>: {
                         required: true,
                         normalizer: function (value) {
                             return $.trim(value);
                         },
                         lettersonly: true
                     },
                    <%=txtNombre.UniqueID%>: {
                        required: true,
                        normalizer: function (value) {
                            return $.trim(value);
                        },
                        lettersonly: true
                    },
                    <%=txtApellido.UniqueID%>: {
                        required: true,
                        normalizer: function (value) {
                            return $.trim(value);
                        },
                        lettersonly: true
                    },
                    <%=txtTelefonoMovil.UniqueID%>: {
                        required: true,
                        number: true,
                        digits: true
                    },
                    <%=txtTelefonoFijo.UniqueID%>: {
                        number: true,
                        digits: true
                    },
                    <%=txtEmail.UniqueID%>: {
                        required: true,
                        email: true,
                        normalizer: function (value) {
                            return $.trim(value);
                        }
                    },
                    <%=txtCalle.UniqueID%>: {
                        required: true,
                        normalizer: function (value) {
                            return $.trim(value);
                        }
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: true,
                        number: true,
                        min: 1,
                        digits: true
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtPiso.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=ddlProvincia.UniqueID%>: {
                        required: true
                    },
                    <%=ddlLocalidad.UniqueID%>: {
                        required: true
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: true,
                        normalizer: function (value) {
                            return $.trim(value);
                        }
                    },
                    <%=txtCodigoPostal.UniqueID%>: {
                        required: true,
                        digits: true
                    }                    
				},
        messages: {                    
                    <%=txtRazonSocial.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtNombre.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtApellido.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtTelefonoMovil.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtTelefonoFijo.UniqueID%>: {
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtEmail.UniqueID%>: {
                        required: "Este campo es requerido.",
                        email: "Formato de correo incorrecto."
                    },
                    <%=txtCalle.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        min: "Ingrese un valor mayor o igual a 1.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=ddlProvincia.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=ddlLocalidad.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtCodigoPostal.UniqueID%>: {
                        required: "Este campo es requerido.",
                        digits: "Ingrese solo dígitos."
                    }
                }
            });
        }

        function Select2() {
            $(".select2_provincia").select2(
                {
                    placeholder: 'Seleccione una Provincia',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_localidad").select2(
                {
                    placeholder: 'Seleccione una Localidad',
                    width: '100%',
                    allowClear: true
                });
        }
    </script>
</asp:Content>
