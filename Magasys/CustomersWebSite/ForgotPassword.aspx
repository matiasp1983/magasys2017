<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="PL.CustomersWebSite.ForgotPassword" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>MAGASYS | Se te olvid&oacute; tu contrase&ntilde;a</title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet" />
    <link href="css/animate.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <link href="css/plugins/sweetalert/sweetalert.css" rel="stylesheet" />
</head>

<body class="gray-bg">

    <div class="passwordBox animated fadeInDown">
        <div class="row">

            <div class="col-md-12">
                <div class="ibox-content">
                    <h2 class="font-bold">Se te olvid&oacute; tu contrase&ntilde;a</h2>
                    <p>
                        Ingres&aacute; tu direcci&oacute;n de correo electr&oacute;nico para recuperar tu contrase&ntilde;a.
                    </p>
                    <div class="row">
                        <div class="col-lg-12">
                            <form id="FormForgotPassword" runat="server" class="m-t" role="form">
                                <div class="form-group">
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" placeholder="Email"></asp:TextBox>
                                </div>

                                <asp:Button ID="btnEnviarEmail" runat="server" Text="Enviar email" CssClass="btn btn-primary block full-width m-b" OnClick="btnEnviarEmail_Click" />
                                <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-sm btn-white btn-block" OnClick="btnVolver_Click" formnovalidate="" />
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-12">
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
