<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PL.AdminDashboard.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/select2/select2.min.css" rel="stylesheet" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormIndex" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Filtros de Gráficos</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Seleccione los Meses del filtro</label>
                                            <asp:DropDownList ID="ddlMes" runat="server" class="select2_mes form-control" multiple="multiple"/>                                                                                        
                                        </div>
                                    </div>                                    
                                </div>
                            </div>
                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div style="text-align: right; padding-right: 15px;">
                                        <button type="button" id="btnBuscar" runat="server" class="btn btn-success">
                                            <i class="fa fa-filter"></i>&nbsp;&nbsp;<span>Filtrar</span>
                                        </button>
                                        <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning">
                                            <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
        <div class="row">
            <div class="col-lg-12">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Título del Gráfico</h5>
                    </div>
                    <div class="ibox-content">
                        <div>
                            <canvas id="barChart" height="140"></canvas>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="js/plugins/select2/select2.full.min.js"></script>    
    <script src="js/plugins/chartJs/Chart.min.js"></script>
    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {                                
                Select2();                
                DibujarGrafico();
            });
        }        

        function Select2() {
            $(".select2_mes").select2(
            {                
                width: '100%',
                allowClear: true
            });

            $('#<%=ddlMes.ClientID%> > option').prop("selected", "selected");
            $('#<%=ddlMes.ClientID%>').trigger("change");
        }               

        function DibujarGrafico() {
            var barData = {
                labels:  $(".select2_mes").val(),
                datasets: [
                    {
                        label: "Dato 1",
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        pointBorderColor: "#fff",
                        data: [65, 59, 80, 81, 56, 55, 40]
                    },
                    {
                        label: "Dato 2",
                        backgroundColor: 'rgba(54, 162, 235, 1)',
                        borderColor: "rgba(26,179,148,0.7)",
                        pointBackgroundColor: "rgba(26,179,148,1)",
                        pointBorderColor: "#fff",
                        data: [28, 48, 40, 19, 86, 27, 90]
                    }
                ]
            };

            var barOptions = {
                responsive: true
            };


            var ctx2 = document.getElementById("barChart").getContext("2d");
            new Chart(ctx2, { type: 'bar', data: barData, options: barOptions });
        }

    </script>
</asp:Content>
