<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="PL.AdminDashboard.Cliente" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormCliente" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Cliente</h2>
<%--                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Proveedores
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Alta de Proveedor"></asp:Label>
                        </strong>
                    </li>
                </ol>--%>
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
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                <label class="col-sm-2 control-label">Alias</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
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
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="select2_provincia form-control"></asp:DropDownList>
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
                                        <asp:HiddenField ID="hfdidLocalidad" runat="server"/>
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
                            <a href="javascript:history.go(-1)" class="btn btn-primary">Volver</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">      
        var FormCliente = '#<%=FormCliente.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {                
                ValidarForm();
                Select2();
            });

            ChangeProvincias();
            ChangeLocalidades();
        }        

        function ChangeProvincias(){
            $('#<%=ddlProvincia.ClientID%>').change(CargarLocalidesPorProvincia);
        }

        function ChangeLocalidades() {
            $('#<%=ddlLocalidad.ClientID%>').change(SeleccionarLocalidad);
        }

        function CargarLocalidesPorProvincia() {
            $.ajax({
                type: "POST",
                url: "Proveedor.aspx/CargarLocalidades",                
                data: JSON.stringify({ 'idProvincia': $("#<%=ddlProvincia.ClientID%>").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: CargarLocalidades,
                failure: function (response) {
                   console.log(response.d);
                }
            });
        }

        function SeleccionarLocalidad() {                       
            $("#<%=hfdidLocalidad.ClientID%>").val(this.value);
         }

        function CargarLocalidades(response) {
            CargarDatosAlControl(response.d, $("#<%=ddlLocalidad.ClientID %>"));
        }

        function CargarDatosAlControl(lista, combo) {
            combo.val(null).html(null).trigger('change');
            if (lista != null && lista.length > 0) {                
                $.each(lista, function () {                    
                    combo.append($("<option></option>").val(this['Value']).html(this['Text']));                    
                });
            }
        }

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormCliente).validate({
                rules: {
                    <%=txtNroDocumento.UniqueID%>: {
                        required: true,
                        number: true,
                        digits: true,
                        minlength: 8
                    },
                    <%=txtNombre.UniqueID%>: {
                        required: true,
                        normalizer: function (value) { return $.trim(value); }, // elimina espacios en blanco
                        lettersonly: true
                    },
                    <%=txtApellido.UniqueID%>: {
                        required: true,
                        normalizer: function (value) { return $.trim(value); },
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
                        email: true,
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
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
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=ddlLocalidad.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        normalizer: function (value) { return $.trim(value); }
                    },
                    <%=txtCodigoPostal.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        digits: true
                    }                    
				},
        messages: {                  
                    <%=txtNroDocumento.UniqueID%>: {
                       required: "Este campo es requerido.",
                       number: "Ingrese un número válido.",
                       digits: "Ingrese solo dígitos.",                        
                       minlength: "Este campo debe ser de 8 dígitos.",
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
                        email: "Formato de correo incorrecto."
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
