<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="PL.AdminDashboard.Cliente" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.googlemap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormCliente" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Cliente</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Clientes
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Alta de Cliente"></asp:Label>
                        </strong>
                    </li>
                </ol>
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
                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
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
                    <div class="form-group">
                                <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                <div class="col-xs-12 col-md-4" style="text-align: right">
                                    <button type="button" data-toggle="modal" data-target="#ModalMap" class="btn btn-warning">
                                        <span class="glyphicon glyphicon-map-marker"></span><span id="ubicacion">Seleccionar Ubicación</span>
                                    </button>
                                    <div class="modal fade" id="ModalMap" tabindex="-1" role="dialog">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-body">
                                                    <div class="form-horizontal">
                                                        <div class="form-group">
                                                            <label class="col-sm-2 control-label">Ubicación:</label>
                                                            <div class="col-sm-9">
                                                                <input type="text" class="form-control" id="pacInput" placeholder="Ingrese una localización"/>
                                                            </div>
                                                            <div class="col-sm-1">
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                            </div>
                                                        </div>
                                                        <div id="map" style="width: 100%; height: 400px;"></div>
                                                        <div class="clearfix">&nbsp;</div>
                                                        <div id="infowindow-content">
                                                            <img src="" width="16" height="16" id="place-icon">
                                                            <span id="place-name" class="title"></span>
                                                            <br>
                                                            <span id="place-address"></span>
                                                        </div>
                                                        <div class="m-t-small">
                                                            <div class="col-sm-3">
                                                                <button type="button" class="btn btn-primary btn-block" data-dismiss="modal" onclick="CargarDireccion();">Aceptar</button>
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
                    <asp:HiddenField ID="hdIdDireccionMaps" runat="server"/>
                    <asp:HiddenField ID="hdLatitud" runat="server"/>
                    <asp:HiddenField ID="hdLongitud" runat="server"/>
                    <div class="hr-line-dashed"></div>                     
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Calle</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true" Text=""></asp:TextBox>
                                    <asp:HiddenField ID="hdCalle" runat="server"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-md-2 control-label">Número</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off" ReadOnly="true"></asp:TextBox>                                    
                                    <asp:HiddenField ID="hdNumero" runat="server"/>
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
                                <label class="col-sm-2 control-label">Localidad</label>

                                <div class="col-sm-10">                                   
                                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdLocalidad" runat="server"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Provincia</label>

                                <div class="col-sm-10">                                    
                                    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="hdProvincia" runat="server"/>
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
                                     <asp:HiddenField ID="hdBarrio" runat="server"/>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código Postal</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" ReadOnly="true"></asp:TextBox>         
                                    <asp:HiddenField ID="hdCodigoPostal" runat="server"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                            <asp:Button ID="btnVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="BtnVolver_Click" formnovalidate="formnovalidate" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB59e_fh71w0-GgkFuO_cD5ILETaLOkX3w&libraries=places&callback=initMap" async defer></script>
    
    <script type="text/javascript">

        var FormCliente = '#<%=FormCliente.ClientID%>';
        var autocomplete;

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Select2();
            });
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
                        normalizer: function (value) { return $.trim(value); },
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
                        required: true,
                        email: true
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtPiso.ClientID%>').val().length > 0 }
                        }
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: {
                            depends: function (element) { return $('#<%=txtCalle.ClientID%>').val().length > 0 }
                        },
                        normalizer: function (value) { return $.trim(value); }
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
                        required: "Este campo es requerido.",
                        email: "Formato de email incorrecto."
                    },
                    <%=txtCalle.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: "Este campo es requerido."
                    }                    
                }
            });
        }

        function Select2() {
            $(".select2_tipodocumento").select2(
                {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
                });
        }

        function initMap() {
            var map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: -31.442216, lng: -64.171016 },
                zoom: 13
            });
            var card = document.getElementById('pac-card');
            var input = document.getElementById('pacInput');

            map.controls[google.maps.ControlPosition.TOP_RIGHT].push(card);

            autocomplete = new google.maps.places.Autocomplete(input);

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
                    console.log("No hay detalles disponibles para la entrada: '" + place.name + "'");
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

        function CargarDireccion() {

            // Limpiar campos
            $("#<%=txtCodigoPostal.ClientID %>").val("");
            $("#<%=hdCodigoPostal.ClientID%>").val("");
            $("#<%=txtBarrio.ClientID %>").val("");
            $("#<%=hdBarrio.ClientID%>").val("");

            // Get the place details from the autocomplete object.
            var place = autocomplete.getPlace();

            for (var i = 0; i < place.address_components.length; i++) {
                var loAddressType = place.address_components[i].types[0];
                var loValor = place.address_components[i]["long_name"];

                switch (loAddressType) {
                    case 'street_number':
                        $("#<%=txtNumero.ClientID %>").val(loValor);
                        $("#<%=hdNumero.ClientID%>").val(loValor);
                        break;
                    case 'route':
                        $("#<%=txtCalle.ClientID %>").val(loValor);
                        $("#<%=hdCalle.ClientID%>").val(loValor);
                        break;
                    case 'neighborhood':
                        $("#<%=txtBarrio.ClientID %>").val(loValor);
                        $("#<%=hdBarrio.ClientID%>").val(loValor);
                        break;
                    case 'locality':
                        $("#<%=txtLocalidad.ClientID %>").val(loValor);
                        $("#<%=hdLocalidad.ClientID%>").val(loValor);
                        break;
                    case 'administrative_area_level_1':
                        $("#<%=txtProvincia.ClientID %>").val(loValor);
                        $("#<%=hdProvincia.ClientID%>").val(loValor);
                        break;
                    case 'postal_code':
                        $("#<%=txtCodigoPostal.ClientID %>").val(loValor);
                        $("#<%=hdCodigoPostal.ClientID%>").val(loValor);
                        break;
                    default:
                        break;
                }
            }

            //Guardamos la dirección Maps en el control hidden que se envía al server en el postback
            var loDireccionMaps = document.getElementById("pacInput").value;
            document.getElementById("<%=hdIdDireccionMaps.ClientID%>").value = loDireccionMaps;
            document.getElementById("<%=hdLatitud.ClientID%>").value = place.geometry.location.lat();
            document.getElementById("<%=hdLongitud.ClientID%>").value = place.geometry.location.lng();

            // Limpiar caja de texto id: pacInput
            document.getElementById("pacInput").value = "";   
        }

    </script>
</asp:Content>
