<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaFinalizar.aspx.cs" Inherits="PL.AdminDashboard.ReservaFinalizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormReservaFinalizar" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Finalizar Reservas</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li>Reserva
                    </li>
                    <li class="active">
                        <strong>Finalizar Reservas</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-5">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Cargar Código de reserva</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:Button ID="btnFinalizarReserva" runat="server" Text="Finalizar" CssClass="btn btn-primary" OnClick="BtnFinalizarReserva_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Procesar todas las reservas</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <asp:Button ID="btnEjecutar" runat="server" Text="Ejecutar" CssClass="btn btn-primary" OnClick="BtnEjecutar_Click"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">

        var FormReservaFinalizar = '#<%=FormReservaFinalizar.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
            });
        }

        function ValidarForm() {

            $(FormReservaFinalizar).validate({
                rules: {
                    <%=txtCodigo.UniqueID%>: {
                        number: true,
                        digits: true
                    }                
				},
                messages: {
                    <%=txtCodigo.UniqueID%>: {
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos."
                    }
                }
            });
        }
    </script>
</asp:Content>
