<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="HojaDeRuta.aspx.cs" Inherits="PL.AdminDashboard.HojaDeRuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Planificación de repartos</h2>
        </div>
    </div>

    <button id="btnCar" class="btn btn-outline btn-success dim  active" type="button"><i class="fas fa-car"></i></button>
    <button id="btnWalking" class="btn btn-outline btn-success dim" type="button"><i class="fas fa-walking"></i></button>
    <button id="btnBiking" class="btn btn-outline btn-success dim" type="button"><i class="fas fa-biking"></i></button>
    <form id="formHojaDeRuta" runat="server">
    <asp:HiddenField ID="hdfModoTransporte" runat="server" />
        </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        $('#btnCar').click(function (e) {
            $('#btnCar').addClass('active');
            $('#btnWalking').removeClass('active');
            $('#btnBiking').removeClass('active');
            $('#<%=hdfModoTransporte.ClientID%>').val('Driving');
        });

        $('#btnWalking').click(function (e) {
            $('#btnCar').removeClass('active');
            $('#btnWalking').addClass('active');
            $('#btnBiking').removeClass('active');
            $('#<%=hdfModoTransporte.ClientID%>').val('Walking');
        });

        $('#btnBiking').click(function (e) {
            $('#btnCar').removeClass('active');
            $('#btnWalking').removeClass('active');
            $('#btnBiking').addClass('active');
            $('#<%=hdfModoTransporte.ClientID%>').val('Bicycling');
        });
    </script>
</asp:Content>
