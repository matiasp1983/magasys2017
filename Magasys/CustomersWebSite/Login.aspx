<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PL.CustomersWebSite.Login" %>

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
    <div class="loginColumns animated fadeInDown">
        <div class="row">

            <div class="col-md-6">
                <h2 class="font-bold">Bienvenido a M+</h2>

                <p>
                    Ahora ya podés obtener los mejores productos de Magasys sin moverte de tu casa.
                </p>

                <p>
                    Hacete cliente en nuestra plataforma web y ahorrá tiempo desde la comodidad de tu casa. Aprovechá para reservar todos los productos que quieras que te los llevamos a domicilio!
                </p>

<%--                <p>
                    When an unknown printer took a galley of type and scrambled it to make a type specimen book.
                </p>--%>

<%--                <p>
                    <small>It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.</small>
                </p>--%>

            </div>
            <div class="col-md-6">
                <div class="ibox-content">
                    <form id="form1" runat="server" class="m-t" role="form">
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

                        <p class="text-muted text-center">
                            <small>¿No tiene una cuenta?</small>
                        </p>
                        <a class="btn btn-sm btn-white btn-block" href="RegistrarUsuario.aspx">Crear cuenta</a>
                    </form>                    
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-6">                
                ©2020 Magasys - Todos los derechos reservados.
            </div>
<%--            <div class="col-md-6 text-right">
                <small>©2020</small>
            </div>--%>
        </div>
    </div>

    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</body>
</html>
