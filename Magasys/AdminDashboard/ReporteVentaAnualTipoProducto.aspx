<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReporteVentaAnualTipoProducto.aspx.cs" Inherits="PL.AdminDashboard.ReporteVentaAnualTipoProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Reporte</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Reportes
                </li>
                <li class="active">
                    <strong>Venta anual por tipo de producto</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormReporteVentaAnualTipoProducto" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Datos de selección</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Producto</label>
                                            <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Año</label>
                                            <div>
                                                 <asp:DropDownList ID="ddlAnio" runat="server"  CssClass="select2_anio form-control"></asp:DropDownList>
                                            </div>                                           
                                        </div>
                                    </div>
                                </div>                            
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-sm-10">
                                        <input id="btnFiltrar" type="button" value="Filtrar" class="btn btn-success" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">    
                                <div class="col-sm-12">
                                    <div class="hr-line-dashed"></div>
                                    <div id="linechart" style="width: 1000px; height: 500px"></div>
                                </div>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {                
                Select2();
            });
        }      

        function Select2() {
            $(".select2_tipoproducto").select2(
            {
                placeholder: 'Seleccione un Tipo de Producto',
                width: '100%',
                allowClear: true
            });     

            $(".select2_anio").select2(
            {
                    placeholder: 'Seleccione un Año',
                    width: '100%',
                    allowClear: true
            });
        }

        $('#btnFiltrar').click(function () {

            var loTipoProducto = $(".select2_tipoproducto").val();
            var loAnio = $(".select2_anio").val();

            $.ajax({
                type: "POST",
                url: "ReporteVentaAnualTipoProducto.aspx/ObtenerVentaAnualPorTipoProducto",
                data: JSON.stringify({ 'pAnio': loAnio, 'pTipoProducto':loTipoProducto}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {                   
                    if (data.d != "") {
                        drawChart(data.d);
                    }
                },
                failure: function (data) {
                    swal({
                        title: "Generación de Gráfico",
                        text: "El gráfico no se puedo generar.",
                        type: "warning",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        });      

    </script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['line'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart(pData) {

            var loData = pData;
            var data = new google.visualization.arrayToDataTable(loData);

            var options = {
                title: "Venta anual por tipo de producto",
                vAxis: { // Valores verticales
                    title:"Cantidad"
                }
            };

            var chart = new google.charts.Line(document.getElementById('linechart'));
            chart.draw(data, google.charts.Line.convertOptions(options));
        };
    </script>
   
</asp:Content>
