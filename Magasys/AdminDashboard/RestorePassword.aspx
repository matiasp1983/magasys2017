<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestorePassword.aspx.cs" Inherits="PL.AdminDashboard.RestorePassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>MAGASYS | Restore Password</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
</head>

<body class="gray-bg">
    <div class="loginColumns animated fadeInDown">
        <div class="row">
            <div class="col-md-12">
                <div class="ibox-content" id="pwd-container1">
                    <form id="FormRestorePassword" runat="server" class="m-t" role="form">
                        <div class="form-group">
                            <label>Contrase&ntilde;a Nueva</label>
                            <asp:TextBox ID="txtContraseniaNueva" runat="server" CssClass="form-control example1" MaxLength="30" autocomplete="off" TextMode="Password" placeholder="Contraseña nueva"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <label>Confirmar la Contrase&ntilde;a Nueva</label>
                            <asp:TextBox ID="txtContraseniaNuevaConfirmar" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" TextMode="Password" placeholder="Confirmar la contraseña nueva"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <div class="pwstrength_viewport_progress"></div>
                            </div>
                        </div>
                        <div>
                            <asp:Button ID="btnCambiarContrasenia" CssClass="btn btn-success" runat="server" Text="Cambiar la Contraseña" OnClick="btnCambiarContrasenia_Click" />                            
                        </div>
                    </form>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-12">
                ©2020 Magasys - Todos los derechos reservados.
            </div>
        </div>
    </div>

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/pwstrength-bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/zxcvbn.js"></script>        
    
    <script>
        var FormRestorePassword = '#<%=FormRestorePassword.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {                    
                Pwstrength();
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
    </script>
</body>
</html>