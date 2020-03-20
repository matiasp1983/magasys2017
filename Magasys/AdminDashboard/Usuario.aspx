﻿<%@ Page Title="Datos del Usuario" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="PL.Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormUsuario" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Usuario</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Usuarios
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Alta de Usuario"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="col-md-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2>Informaci&oacute;n General</h2>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre de Usuario</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="pwd-container1">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Contrase&ntilde;a</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control example1" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Confirmaci&oacute;n Contrase&ntilde;a</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtContraseniaConfirmacion" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label"></label>
                                <div class="col-sm-10">
                                    <div class="pwstrength_viewport_progress"></div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Rol de Usuario</label>

                                <div class="col-sm-10">
                                    <div id="divRol">
                                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="select2_rol form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2>Avatar</h2>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Image ID="imgPreview" Width="200px" runat="server" ImageUrl="~/AdminDashboard/img/perfil_default.png" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Archivo</label>

                                <div class="col-sm-8">
                                    <asp:FileUpload ID="fuploadImagen" runat="server" CssClass="form-control" onchange="ShowImagePreview(this);"/>
                                </div>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-content">
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
    <script src="js/plugins/pwstrength/pwstrength-bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/zxcvbn.js"></script>
    <script type="text/javascript">
        var FormUsuario = '#<%=FormUsuario.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Select2();
                Pwstrength();
            });
        }

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormUsuario).validate({
                rules: {
                    <%=txtNombreUsuario.UniqueID%>: {
                required: true,
                remote: function () {
                    return {
                        type: "POST",
                        url: "Usuario.aspx/ValidarNombreUsuario",
                        data: JSON.stringify({ 'pNombreUsuario': $("#<%=txtNombreUsuario.ClientID%>").val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        dataFilter: function (data) {
                            var msg = JSON.parse(data);
                            if (msg.hasOwnProperty('d'))
                                return msg.d;
                            else
                                return msg;
                        }
                    }
                }
            },
                    <%=txtNombre.UniqueID%>: {
                    required: true,
                    lettersonly: true
                },
                    <%=txtApellido.UniqueID%>: {
                    required: true,
                    lettersonly: true
                },
                    <%=txtContrasenia.UniqueID%>: {
                    required: true,
                    minlength: 8
                },
                    <%=txtContraseniaConfirmacion.UniqueID%>: {
                    required: true,
                    equalTo: "#<%=txtContrasenia.ClientID %>",
                    minlength: 8
                },
                    <%=ddlRol.UniqueID%>: {
                    required: true
                }
                },
        messages: {
                    <%=txtNombreUsuario.UniqueID%>: {
                required: "Este campo es requerido.",
                    remote: "Este nombre de usuario ya está en uso. Elige otro."
            },
                    <%=txtNombre.UniqueID%>: {
                required: "Este campo es requerido."
            },
                    <%=txtApellido.UniqueID%>: {
                required: "Este campo es requerido."
            },
                    <%=txtContrasenia.UniqueID%>: {
                required: "Este campo es requerido.",
                    minlength: "Usa 8 caracteres o más para tu contraseña."
            },
                    <%=txtContraseniaConfirmacion.UniqueID%>: {
                required: "Este campo es requerido.",
                    equalTo: "Las contraseñas no coinciden. Vuelva a intentarlo.",
                        minlength: "Usa 8 caracteres o más para tu contraseña."
            },
                    <%=ddlRol.UniqueID%>: {
                required: "Este campo es requerido."
            }
        }
            });
        }

        function Select2() {
            $(".select2_rol").select2(
                {
                    placeholder: 'Seleccione un Rol',
                    width: '100%',
                    allowClear: true
                });
        }

        function Pwstrength() {
            var options1 = {};
            options1.ui = {
                container: "#pwd-container1",
                showVerdictsInsideProgressBar: true,
                viewports: {
                    progress: ".pwstrength_viewport_progress"
                }
            };
            options1.common = {
                debug: false
            };
            $('.example1').pwstrength(options1);
        }

        function ShowImagePreview(input) {  
            if (input.files && input.files[0]) {  
                var reader = new FileReader();  
                reader.onload = function (e) {  
                    $('#<%=imgPreview.ClientID%>').prop('src', e.target.result);  
                };  
                reader.readAsDataURL(input.files[0]);  
            }  
        }

    </script>
</asp:Content>
