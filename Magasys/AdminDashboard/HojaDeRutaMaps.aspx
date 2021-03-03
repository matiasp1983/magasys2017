<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="HojaDeRutaMaps.aspx.cs" Inherits="PL.AdminDashboard.HojaDeRutaMaps" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.googlemap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Planificación de repartos</h2>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormHojaDeRutaMaps" runat="server">            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Modo de viaje</h5>
                                     <div style="text-align: right;">
                                                    <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-success" OnClick="BtnVolver_Click" formnovalidate="formnovalidate" />
                                                </div>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3">
                                                <div class="form-group space-15" id="mode">
                                                    <button id="btnDriving" class="btn btn-outline btn-success dim active" type="button" title="En auto" onclick="initMap('DRIVING')"><i class="fas fa-car"></i></button>
                                                    <button id="btnWalking" class="btn btn-outline btn-success dim" type="button" title="A pie" onclick="initMap('WALKING')"><i class="fas fa-walking"></i></button>
                                                    <button id="btnBiking" class="btn btn-outline btn-success dim" type="button" title="En bicicleta" onclick="initMap('BICYCLING')"><i class="fas fa-biking"></i></button>
                                                </div>
                                            </div>
                                            <div class="col-md-9">
                                                <div class="form-group">
                                                    <label class="control-label">Punto de partida</label>
                                                    <asp:TextBox ID="txtPutoDePartida" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label class="control-label">Distancia total</label>
                                                    <asp:TextBox ID="txtDistanciaTotal" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Enabled="false"></asp:TextBox>
                                                </div>
                                                <div style="text-align: right;">                                                    
                                                    <a class="btn btn-info" data-toggle="modal" data-target="#ModalConfirmarRuta"><i class="fa fa-map-o"></i>&nbsp;&nbsp;Confirmar Ruta</a>
                                                    <button id="btnExportarExcel" runat="server" class="btn btn-info" type="button" onserverclick="BtnExportarExcel_Click"><i class="fas fa-file-download"></i>&nbsp;&nbsp;Exportar Ruta</button>                                                    
                                                </div>                                                                
                                               
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            <div class="sk-spinner sk-spinner-three-bounce">
                                                <div class="sk-bounce1"></div>
                                                <div class="sk-bounce2"></div>
                                                <div class="sk-bounce3"></div>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox">
                                <div class="ibox-content">
                                    <div id="map"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnExportarExcel" />
                </Triggers>
            </asp:UpdatePanel>
            <div id="ModalConfirmarRuta" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content animated bounce">
                        <div class="modal-body">
                            <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #86caf8;">
                                <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #86caf8;"></span>
                                <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #86caf8;"></span>
                            </div>
                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Desea confirmar la hoja de ruta?</h2>
                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                            </p>
                        </div>
                        <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                            <asp:Button ID="btnConfirmarReparto" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #558bdd"
                                OnClick="BtnConfirmarRuta_Click" />
                            <asp:Button ID="btnCancelarAccion" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                data-dismiss="modal" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB59e_fh71w0-GgkFuO_cD5ILETaLOkX3w&libraries=places&callback=initMap" async defer></script>

    <script type="text/javascript">

        function initMap(pModoTransporte) {

            BotonSeleccionado(pModoTransporte);

            if (pModoTransporte == undefined)
                pModoTransporte = "DRIVING";

            const directionsRenderer = new google.maps.DirectionsRenderer();
            const directionsService = new google.maps.DirectionsService();
            const map = new google.maps.Map(document.getElementById("map"), {
                zoom: 6,
                center: {
                    lat: -31.442216,
                    lng: -64.171016
                }
            });
            directionsRenderer.setMap(map);

            $.ajax({
                type: "POST",
                url: "HojaDeRutaMaps.aspx/EstrategiaFuerzaBruta",
                data: JSON.stringify({ 'pModoTransporte': pModoTransporte }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        calculateAndDisplayRoute(directionsService, directionsRenderer, data.d);
                    }
                },
                failure: function (data) {
                    swal({
                        title: "Generación de Hoja de Ruta",
                        text: "La hoja de ruta no se puedo generar.",
                        type: "warning",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }

        function calculateAndDisplayRoute(directionsService, directionsRenderer, waypointLatLng) {
            const waypts = [];
            var selectedMode = "";
            var loLatDestino;
            var loLongDestino;

            for (let i = 1; i < waypointLatLng.length; i++) {
                if (waypointLatLng[i][0] != "ModoTransporte" && waypointLatLng[i][0] != "DistanciaTotal" && waypointLatLng[i + 1][0] != "ModoTransporte" && waypointLatLng[i + 1][0] != "DistanciaTotal") {
                    waypts.push({
                        location: new google.maps.LatLng(waypointLatLng[i][0], waypointLatLng[i][1]),
                        stopover: true
                    });
                }
                else {
                    if (waypointLatLng[i][0] == "ModoTransporte") {
                        selectedMode = waypointLatLng[i][1];
                        loLatDestino = waypointLatLng[i - 1][0];
                        loLongDestino = waypointLatLng[i - 1][1];
                    }
                    if (waypointLatLng[i][0] == "DistanciaTotal") {
                        $("#<%=txtDistanciaTotal.ClientID%>").val(waypointLatLng[i][1]);
                    }
                }
            }
            
            directionsService.route(
                {
                    origin: {                   
                        lat: waypointLatLng[0][0],
                        lng: waypointLatLng[0][1]
                    },
                    destination: {             
                        lat: loLatDestino,
                        lng: loLongDestino
                    },
                    waypoints: waypts,
                    //waypoints: [{ location: first, stopover: true },  // marga       --> stopover True para que marque en el mapa el globito rojo
                    //{ location: second, stopover: true }]            // marga
                    // Note that Javascript allows us to access the constant
                    // using square brackets and a string value as its
                    // "property."
                    travelMode: google.maps.TravelMode[selectedMode]
                },
                (response, status) => {
                    if (status == "OK") {
                        directionsRenderer.setDirections(response);
                    } else {
                        window.alert("Directions request failed due to " + status);
                    }
                }
            );
        }

        function BotonSeleccionado(pModoTransporte) {
            if (pModoTransporte == undefined) {                
                return;
            }
            if (pModoTransporte == "DRIVING") {
                $('#btnDriving').attr('class', 'btn btn-outline btn-success dim active');
                $('#btnWalking').attr('class', 'btn btn-outline btn-success dim');
                $('#btnBiking').attr('class', 'btn btn-outline btn-success dim');
            } else {
                if (pModoTransporte == "WALKING") {
                    $('#btnDriving').attr('class', 'btn btn-outline btn-success dim');
                    $('#btnWalking').attr('class', 'btn btn-outline btn-success dim active');
                    $('#btnBiking').attr('class', 'btn btn-outline btn-success dim');
                }
                else {                    
                    $('#btnDriving').attr('class', 'btn btn-outline btn-success dim');
                    $('#btnWalking').attr('class', 'btn btn-outline btn-success dim');
                    $('#btnBiking').attr('class', 'btn btn-outline btn-success dim active');
                }
            }
        }
    </script>
</asp:Content>
