<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrarUsuario.aspx.cs" Inherits="PL.CustomersWebSite.RegistrarUsuario" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>MAGASYS | Registrar Usuario</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet" />
</head>

<body class="gray-bg">
    <form id="FormRegistrarUsuario" runat="server" class="form-horizontal">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="col-md-7">
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
                                        <div id="divTipoDocumento">
                                            <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">N&uacute;mero de Documento</label>

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
                                    <label class="col-sm-2 control-label">Email</label>

                                    <div class="col-sm-10">
                                    <div class="input-group m-b" id="divEmail">
                                        <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Teléfono Móvil</label>

                                    <div class="col-sm-10">
                                    <div class="input-group m-b" id="divTelefonoMovil">
                                        <span class="input-group-addon"><i class="fa fa-mobile-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoMovil" runat="server" CssClass="form-control" MaxLength="11" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03516243492.</span>
                                </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">Nombre de Usuario</label>

                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label"></label>

                                    <div class="col-sm-10">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="pwd-container1">
                            <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Contrase&ntilde;a</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control example1" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            </div>
                            <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Confirmaci&oacute;n Contrase&ntilde;a</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtContraseniaConfirmacion" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                                </div>
                            <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-sm-12 control-label"></label>
                                <div class="col-sm-12">
                                    <div class="pwstrength_viewport_progress"></div>
                                </div>
                            </div>
                            </div>
                        </div>                        
                    </div>
                </div>
            </div>
            <div class="col-md-5">
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

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/pwstrength-bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/zxcvbn.js"></script>
    <script src="js/plugins/validate/jquery.validate.js"></script>
    <script src="js/plugins/select2/select2.full.min.js"></script>

    <script>
        var FormRestorePassword = '#<%=FormRegistrarUsuario.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Select2();
                Pwstrength();
            });
        }

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormRestorePassword).validate({
                rules: {
                    <%=ddlTipoDocumento.UniqueID%>: {
                    required: true
                    },
                    <%=txtNroDocumento.UniqueID%>: {
                        required: true,
                        number: true,
                        digits: true,
                        minlength: 8
                    },
                    <%=txtNombreUsuario.UniqueID%>: {
                required: true,
                remote: function () {
                    return {
                        type: "POST",
                        url: "RegistrarUsuario.aspx/ValidarNombreUsuario",
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
                    <%=txtEmail.UniqueID%>: {
                    required: true,
                    email: true
                },
                    <%=txtTelefonoMovil.UniqueID%>: {
                    required: true,
                    number: true,
                    digits: true
                },
                    <%=txtContrasenia.UniqueID%>: {
                    required: true,
                    minlength: 8
                },
                    <%=txtContraseniaConfirmacion.UniqueID%>: {
                    required: true,
                    equalTo: "#<%=txtContrasenia.ClientID %>",
                    minlength: 8
                }                    
                },
        messages: {
                    <%=txtNroDocumento.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos.",
                        minlength: "Este campo debe ser de 8 dígitos.",
                },
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
                    <%=txtEmail.UniqueID%>: {
                    required: "Este campo es requerido.",
                    email: "Formato de email incorrecto."
                },
                    <%=txtTelefonoMovil.UniqueID%>: {
                    required: "Este campo es requerido.",
                    number: "Ingrese un número válido.",
                    digits: "Ingrese solo dígitos."
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
                    <%=ddlTipoDocumento.UniqueID%>: {
                    required: "Este campo es requerido."
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
</body>
</html>

