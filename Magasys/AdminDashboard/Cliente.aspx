<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="PL.AdminDashboard.Cliente" EnableEventValidation="false"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormCliente" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Cliente</h2>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">                    
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Alias</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Email</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divEmail">
                                        <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Móvil</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divTelefonoMovil">
                                        <span class="input-group-addon"><i class="fa fa-mobile-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoMovil" runat="server" CssClass="form-control" MaxLength="11" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03516243492.</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Fijo</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b" id="divTelefonoFijo">
                                        <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoFijo" runat="server" CssClass="form-control" MaxLength="11" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03514823455.</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Dirección</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Calle</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-md-2 control-label">Número</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Piso</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" MaxLength="2" autocomplete="off"></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Dpto</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" MaxLength="2" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Provincia</label>

                                <div class="col-sm-10">
                                    <div id="divProvincia">
                                        <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="select2_provincia form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Localidad</label>

                                <div class="col-sm-10">
                                    <div id="divLocalidad">
                                        <asp:DropDownList ID="ddlLocalidad" runat="server" CssClass="select2_localidad form-control"></asp:DropDownList>
                                        <asp:HiddenField ID="hfdidLocalidad" runat="server"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Barrio</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtBarrio" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código Postal</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">





<style>
      /* Always set the map height explicitly to define the size of the div
       * element that contains the map. */
      #map {
        height: 425px;
      }
      /* Optional: Makes the sample page fill the window. */
      html, body {
        height: 100%;
        margin: 0;
        padding: 0;
      }
      #description {
        font-family: Roboto;
        font-size: 15px;
        font-weight: 300;
      }

      #infowindow-content .title {
        font-weight: bold;
      }

      #infowindow-content {
        display: none;
      }

      #map #infowindow-content {
        display: inline;
      }

      .pac-card {
        margin: 10px 10px 0 0;
        border-radius: 2px 0 0 2px;
        box-sizing: border-box;
        -moz-box-sizing: border-box;
        outline: none;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        background-color: #fff;
        font-family: Roboto;
      }

      #pac-container {
        padding-bottom: 12px;
        margin-right: 12px;
      }

      .pac-controls {
        display: inline-block;
        padding: 5px 11px;
      }

      .pac-controls label {
        font-family: Roboto;
        font-size: 13px;
        font-weight: 300;
      }

      #pac-input {
        background-color: #fff;
        font-family: Roboto;
        font-size: 15px;
        font-weight: 300;
        margin-left: 12px;
        padding: 0 11px 0 13px;
        text-overflow: ellipsis;
        width: 400px;
      }

      #pac-input:focus {
        border-color: #4d90fe;
      }

      #title {
        color: #fff;
        background-color: #4d90fe;
        font-size: 25px;
        font-weight: 500;
        padding: 6px 12px;
      }
    </style>



                         <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Google Map Prueba</label>                               
                                <div class="col-sm-10">
                                     <button type="button" data-toggle="modal" data-target="#ModalMap" class="btn btn-default">
                                        <span class="glyphicon glyphicon-map-marker"></span> <span id="ubicacion">Seleccionar Ubicación</span>
                                    </button>
                                    <div class="modal fade" id="ModalMap" tabindex="-1" role="dialog" >
                                        <style>
                                            .pac-container {
                                                z-index: 99999;
                                            }
                                        </style>
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-body">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">Ubicación:</label>
                                                            <div class="col-sm-9">
                                                                <input type="text" class="form-control" id="pac-input" placeholder="Ingrese una localización"/>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                            </div>
                                                        </div>
                                                        <div id="map" style="width: 100%; height: 400px;"></div>
                                                        <div class="clearfix">&nbsp;</div>
                                                            <div id="infowindow-content">
                                                                <img src="" width="16" height="16" id="place-icon">
                                                                <span id="place-name"  class="title"></span><br>
                                                                <span id="place-address"></span>
                                                            </div>
                                                            <div class="m-t-small">
                                                                    <div class="col-sm-3">
                                                                        <button type="button" class="btn btn-primary btn-block" data-dismiss="modal">Aceptar</button>
                                                                    </div>
                                                            </div>
                                                        <div class="clearfix"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click"/>
                            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="BtnVolver_Click" formnovalidate="formnovalidate"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">    
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDvU6ejfj9r1VKtaLhTvrXqMOTTbeYypJo&libraries=places&callback=initMap" async defer></script>

    <script type="text/javascript">

        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -31.442216, lng: -64.171016 },
                zoom: 13
            });
            var card = document.getElementById('pac-card');
            var input = document.getElementById('pac-input');
            //var types = document.getElementById('type-selector');
            //var strictBounds = document.getElementById('strict-bounds-selector');

            map.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);

            var autocomplete = new google.maps.places.Autocomplete(input);

            // Bind the map's bounds (viewport) property to the autocomplete object,
            // so that the autocomplete requests use the current map bounds for the
            // bounds option in the request.
            //autocomplete.bindTo('bounds', map);

            // Set the data fields to return when the user selects a place.
            //autocomplete.setFields(['address_components', 'geometry', 'icon', 'name']);

            var infowindow = new google.maps.InfoWindow(); // ventanita blanca
            var infowindowContent = document.getElementById('infowindow-content'); // información que contiene la ventanita blanca, aqui se define en formato HTML se debe incluir lo que quiere que aprareza en la ventanita blanca. 
            infowindow.setContent(infowindowContent);

            // Marker: Inserta Marcador es el indicador rojo de la ubicación.
            var marker = new google.maps.Marker({
                map: map,
                anchorPoint: new google.maps.Point(0, -29)
            });

            autocomplete.addListener('place_changed', function () {
                infowindow.close();
                marker.setVisible(false);
                var place = autocomplete.getPlace();
                if (!place.geometry) {
                    // Mensaje para informar que la ubicación no se encontró.
                    window.alert("No hay detalles disponibles para la entrada: '" + place.name + "'");
                    return;
                }

                // If the place has a geometry, then present it on a map.
                if (place.geometry.viewport) {
                    map.fitBounds(place.geometry.viewport);
                } else {
                    map.setCenter(place.geometry.location); // centra el mapa en esta localización
                    map.setZoom(17);  // Why 17? Because it looks good.
                }

                // Agremos marcador
                marker.setPosition(place.geometry.location);
                marker.setVisible(true);

                var address = '';
                if (place.address_components) {
                    address = [
                        (place.address_components[0] && place.address_components[0].short_name || ''),
                        (place.address_components[1] && place.address_components[1].short_name || ''),
                        (place.address_components[2] && place.address_components[2].short_name || '')
                    ].join(' ');
                }

                infowindowContent.children['place-icon'].src = place.icon;
                infowindowContent.children['place-name'].textContent = place.name;
                infowindowContent.children['place-address'].textContent = address;
                infowindow.open(map, marker);
            });
        }

        $('#ModalMap').on('shown.bs.modal');
        

        var FormCliente = '#<%=FormCliente.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {                
                ValidarForm();
                Select2();        
            });

            ChangeProvincias();
            ChangeLocalidades();
        }        

        function ChangeProvincias(){
            $('#<%=ddlProvincia.ClientID%>').change(CargarLocalidesPorProvincia);
        }

        function ChangeLocalidades() {
            $('#<%=ddlLocalidad.ClientID%>').change(SeleccionarLocalidad);
        }

        function CargarLocalidesPorProvincia() {
            $.ajax({
                type: "POST",
                url: "Proveedor.aspx/CargarLocalidades",                
                data: JSON.stringify({ 'idProvincia': $("#<%=ddlProvincia.ClientID%>").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: CargarLocalidades,
                failure: function (response) {
                   console.log(response.d);
                }
            });
        }

        function SeleccionarLocalidad() {                       
            $("#<%=hfdidLocalidad.ClientID%>").val(this.value);
         }

        function CargarLocalidades(response) {
            CargarDatosAlControl(response.d, $("#<%=ddlLocalidad.ClientID %>"));
        }

        function CargarDatosAlControl(lista, combo) {
            combo.val(null).html(null).trigger('change');
            if (lista != null && lista.length > 0) {                
                $.each(lista, function () {                    
                    combo.append($("<option></option>").val(this['Value']).html(this['Text']));                    
                });
            }
        }

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormCliente).validate({
                rules: {
                    <%=txtNroDocumento.UniqueID%>: {
                        required: true,
                        number: true,
                        digits: true,
                        minlength: 8
                    },
                    <%=txtNombre.UniqueID%>: {
                        required: true,
                        normalizer: function (value) { return $.trim(value); }, // elimina espacios en blanco
                        lettersonly: true
                    },
                    <%=txtApellido.UniqueID%>: {
                        required: true,
                        normalizer: function (value) { return $.trim(value); },
                        lettersonly: true
                    },
                    <%=txtTelefonoMovil.UniqueID%>: {
                        required: true,
                        number: true,
                        digits: true
                    },
                    <%=txtTelefonoFijo.UniqueID%>: {
                        number: true,
                        digits: true
                    },
                    <%=txtEmail.UniqueID%>: {
                        email: true,
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        number: true,
                        min: 1,
                        digits: true
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtPiso.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=ddlProvincia.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=ddlLocalidad.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        normalizer: function (value) { return $.trim(value); }
                    },
                    <%=txtCodigoPostal.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        digits: true
                    }                    
				},
        messages: {                  
                    <%=txtNroDocumento.UniqueID%>: {
                       required: "Este campo es requerido.",
                       number: "Ingrese un número válido.",
                       digits: "Ingrese solo dígitos.",                        
                       minlength: "Este campo debe ser de 8 dígitos.",
                    },
                    <%=txtNombre.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtApellido.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtTelefonoMovil.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtTelefonoFijo.UniqueID%>: {
                        number: "Ingrese un número válido.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtEmail.UniqueID%>: {
                        email: "Formato de correo incorrecto."
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: "Este campo es requerido.",
                        number: "Ingrese un número válido.",
                        min: "Ingrese un valor mayor o igual a 1.",
                        digits: "Ingrese solo dígitos."
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=ddlProvincia.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=ddlLocalidad.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtCodigoPostal.UniqueID%>: {
                        required: "Este campo es requerido.",
                        digits: "Ingrese solo dígitos."
                    }
                }
            });
        }

        function Select2() {
            $(".select2_provincia").select2(
                {
                    placeholder: 'Seleccione una Provincia',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_localidad").select2(
                {
                    placeholder: 'Seleccione una Localidad',
                    width: '100%',
                    allowClear: true                    
                });
        }
        
    </script>    
</asp:Content>
