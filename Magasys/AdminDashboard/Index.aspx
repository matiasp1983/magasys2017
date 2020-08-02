<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PL.AdminDashboard.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Reportes</h2>
<%--            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Productos
                </li>
                <li class="active">
                    <strong>Lista de Productos</strong>
                </li>
            </ol>--%>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormIndex" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Reporte</h5>
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
                                            <label class="col-sm-10 control-label">Selección múltiple</label>
                                            <div>
                                                 <asp:DropDownList ID="ddlAnio" runat="server"  data-placeholder="Seleccionar un Año..." CssClass="chosen-select" multiple=""></asp:DropDownList>
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
                                    <div id="columnchart" style="width: 1000px; height: 500px"></div>
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
     <!-- Chosen -->
    <script src="js/plugins/chosen/chosen.jquery.js"></script>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {                
                Select2();
                Chosen();
            });
        }      

        function Select2() {
            $(".select2_tipoproducto").select2(
            {
                placeholder: 'Seleccione un Tipo de Producto',
                width: '100%',
                allowClear: true
            });            
        }

        function Chosen() {
            $('.chosen-select').chosen({ width: "100%" });
        }

        $('#btnFiltrar').click(function () {

            var loTipoProducto = $(".select2_tipoproducto").val();
            var loAnio = $('.chosen-select').val();

            $.ajax({
                type: "POST",
                url: "Index.aspx/ObtenerVentasPorProducto",
                data: JSON.stringify({ 'pAnios': loAnio, 'pTipoProducto':loTipoProducto}),
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
        google.charts.load('current', { 'packages': ['bar'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart(pData) {

            var loData = pData;
            var data = new google.visualization.arrayToDataTable(loData);

            var options = {
                title: "Ventas por año y tipo de producto",
                vAxis: { // Valores verticales
                    title:"Cantidad"
                }
            };

            var chart = new google.charts.Bar(document.getElementById('columnchart'));
            chart.draw(data, google.charts.Bar.convertOptions(options));
        };
    </script>
   
</asp:Content>
