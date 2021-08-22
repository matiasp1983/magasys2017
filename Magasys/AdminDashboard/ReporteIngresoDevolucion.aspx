<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReporteIngresoDevolucion.aspx.cs" Inherits="PL.AdminDashboard.ReporteIngresoDevolucion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/plugins/chosen/bootstrap-chosen.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Reporte</h2>
<%--            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Reportes
                </li>
                <li class="active">
                    <strong>Ratio entre el ingreso y la devolución de productos</strong>
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
                            <h5>Ratio entre el ingreso y la devolución de productos</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Producto</label>
                                            <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control" OnSelectedIndexChanged="TipoProductoSeleccionado" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Selección de productos</label>
                                            <div>
                                                 <asp:DropDownList ID="ddlProducto" runat="server"  data-placeholder="Seleccionar un Producto..." CssClass="chosen-select" multiple="" OnSelectedIndexChanged="TipoProductoSeleccionado"></asp:DropDownList>
                                            </div>                                           
                                        </div>
                                    </div>
                                </div>                            
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-12">
                                        <div class="form-group space-15" id="mode">
                                            <button id="btnHoy" class="btn btn-outline btn-success m-l-xs active" type="button" onclick="grafico('Hoy')">Hoy</button>
                                            <button id="btn7dias" class="btn btn-outline btn-success m-l-xs" type="button" onclick="grafico('7dias')">Últimos 7 días</button>
                                            <button id="btnEsteMes" class="btn btn-outline btn-success m-l-xs" type="button" onclick="grafico('EsteMes')">Este mes</button>
                                            <button id="btn30dias" class="btn btn-outline btn-success m-l-xs" type="button" onclick="grafico('30dias')">Últimos 30 días</button>
                                            <button id="btnEsteAnio" class="btn btn-outline btn-success m-l-xs" type="button" onclick="grafico('EsteAnio')">Este año</button>
                                            <button id="btnFiltro" class="btn btn-outline btn-success m-l-xs" type="button" onclick="botonSeleccionado('Filtro')" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false" aria-controls="collapseExample">Aplicar Filtro</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="collapse" id="collapseExample">
                                        <div class="card card-body">
                                            <div class="row">
                                                <div class="col-sm-8 m-t-xs">
                                                    <div class="col-md-6">
                                                        <div class="form-group" id="dpFecha">
                                                            <label class="control-label">Selección de rango</label>
                                                            <div style="display:flex; align-items:center">
                                                                <div class="input-daterange input-group" id="datepicker">
                                                                    <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                                                    <span class="input-group-addon">a</span>
                                                                    <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                                <button id="btnFiltrar" class="btn btn-white m-l-xs" type="button" onclick="FiltrarPorRangoDeFecha()">Filtrar</button>
                                                            </div>
                                                        </div>                                                                                                              
                                                    </div>                                                     
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">    
                                <div class="col-sm-12">
                                    <div class="hr-line-dashed"></div>
                                    <div id="columnchart" style="width: 1000px; height: 400px"></div>
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
                LoadDatePicker();
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

        function LoadDatePicker() {
            $('#dpFecha .input-daterange').datepicker({
                startView: 2,
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false,
                endDate: "today"
            });
        }

        function grafico(pOperacion) {
            $('#collapseExample').removeClass('collapse in');
            $('#collapseExample').addClass('collapse');
            var loTipoProducto = $(".select2_tipoproducto").val();
            var loProducto = $('.chosen-select').val(); 

            botonSeleccionado(pOperacion);

            $.ajax({
                type: "POST",
                url: "ReporteIngresoDevolucion.aspx/ObtenerRatioIngresosDevoluciones",
                data: JSON.stringify({'pProductos':loProducto,'pTipoProducto':loTipoProducto, 'pOperacion':pOperacion}),
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

        }

        function FiltrarPorRangoDeFecha() {
            var loProducto = $('.chosen-select').val();
            var loTipoProducto = $(".select2_tipoproducto").val();
            var txtFechaDesde = $(<%=txtFechaDesde.ClientID%>).val();
            var txtFechaHasta = $(<%=txtFechaHasta.ClientID%>).val();

            $.ajax({
                type: "POST",
                url: "ReporteIngresoDevolucion.aspx/ObtenerRatioIngresosDevolucionesPorFiltro",
                data: JSON.stringify({'pProductos':loProducto,'pTipoProducto':loTipoProducto, 'pFechaDesde':txtFechaDesde, 'pFechaHasta':txtFechaHasta}),
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
        }

        function botonSeleccionado(pOperacion) {
            if (pOperacion == undefined) {                
                return;
            }

            switch (pOperacion) {
                case "Hoy":
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs active');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs');        
                    break;

                case "7dias":
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs active');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs');
                    break;

                case "EsteMes":
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs active');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs');
                    break;

                case "30dias":
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs active');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs');
                    break;

                case "EsteAnio":
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs active');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs');
                    break;

                default:
                    $('#btnHoy').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn7dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteMes').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btn30dias').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnEsteAnio').attr('class', 'btn btn-outline btn-success m-l-xs');
                    $('#btnFiltro').attr('class', 'btn btn-outline btn-success m-l-xs active');
            }
        }
    </script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['bar'] });

        function drawChart(pData) {

            var loData = pData;
            var data = new google.visualization.arrayToDataTable(loData);

            var options = {
                vAxis: { // Valores verticales
                    title:"Escala del 0 al 1"
                },
                width: 600,
                height: 400,
                legend: { position: 'top', maxLines: 3 },
                bar: { groupWidth: '75%' },
                isStacked: true,
            };

            var chart = new google.charts.Bar(document.getElementById('columnchart'));
            chart.draw(data, google.charts.Bar.convertOptions(options));
        }
    </script>
   
</asp:Content>
