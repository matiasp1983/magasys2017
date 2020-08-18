<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PL.AdminDashboard.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>MAGASYS | Login</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
</head>

<body class="gray-bg">
    <div class="middle-box text-center loginscreen animated fadeInDown">
        <div>
            <div>
                <h1 class="logo-name">M+</h1>
            </div>
            <h3>Inicie Sesi&oacute;n</h3>
            <form id="form1" runat="server">
                <div class="form-group">
                    <asp:TextBox ID="txtUsuario" runat="server" class="form-control" placeholder="Usuario"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtContrasenia" runat="server" class="form-control" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                </div>
                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary block full-width m-b" OnClick="btnLogin_Click" />
                <a href="ForgotPassword.aspx">
                    <small>¿Se te olvid&oacute; tu contrase&ntilde;a?</small>
                </a>
            </form>
            <p class="m-t"><small>©2020 Magasys - Todos los derechos reservados.</small> </p>
        </div>
    </div>

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
