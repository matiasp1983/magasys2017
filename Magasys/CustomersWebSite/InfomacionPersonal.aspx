<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="InfomacionPersonal.aspx.cs" Inherits="PL.CustomersWebSite.InformacionPersonal" %>
<%@ MasterType VirtualPath="~/CustomersWebSite/MasterPage.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.googlemap.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormInformacionPersonal" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-content">
                                <h2>Información personal</h2>
                            </div>
                            <div class="ibox-title">
                                <h3>Perfil</h3>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Cliente</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Fecha Alta</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
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
                                <div class="hr-line-dashed"></div>
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
                                            <label class="col-sm-2 control-label">Documento</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off" Enabled="false"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Contrase&ntilde;a</label>
                                            <div class="col-sm-7">
                                                <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" TextMode="Password" Enabled="False"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-3">
                                                <a data-toggle="modal" class="btn btn-white" href="#modal-cambiarcontarsenia">Cambiar</a>
                                            </div>
                                            <div id="modal-cambiarcontarsenia" class="modal fade" aria-hidden="true">
                                                <div class="modal-dialog">
                                                    <div class="modal-content">
                                                        <div class="modal-body">
                                                            <div class="row" id="pwd-container1">
                                                                <div class="col-sm-12">
                                                                    <div class="form-group">
                                                                        <label>Contrase&ntilde;a Nueva</label>
                                                                        <asp:TextBox ID="txtContraseniaNueva" runat="server" CssClass="form-control example1" MaxLength="30" autocomplete="off" TextMode="Password" placeholder="Contraseña nueva"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <label>Confirmar la Contrase&ntilde;a Nueva</label>
                                                                        <asp:TextBox ID="txtContraseniaNuevaConfirmar" runat="server" CssClass="form-control" MaxLength="30" autocomplete="off" TextMode="Password" placeholder="Confirmar la contraseña nueva"></asp:TextBox>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="col-sm-12">
                                                                            <div class="pwstrength_viewport_progress"></div>
                                                                        </div>
                                                                    </div>
                                                                    <div>
                                                                        <asp:Button ID="btnCambiarContrasenia" CssClass="btn btn-success" runat="server" Text="Cambiar la Contraseña" OnClick="btnCambiarContrasenia_Click" />
                                                                        <asp:Button ID="btnCancelarCambiarContrasenia" CssClass="btn btn-primary" runat="server" Text="Cancelar" formnovalidate="" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Avatar</label>
                                            <div class="col-md-3 col-md-offset-3">
                                                <asp:Image ID="imgPreview" Width="80px" runat="server" ImageUrl="~/CustomersWebSite/img/perfil_default.png" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Archivo</label>
                                            <div class="col-sm-10">
                                                <asp:FileUpload ID="fuploadImagen" runat="server" CssClass="form-control" onchange="ShowImagePreview(this);" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <div class="col-sm-7 col-md-offset-2">                                                
                                                <input id="btnLimpiarImagen" type="button" class="btn btn-warning" value="Limpiar Imagen" />
                                                <asp:HiddenField ID="hdfLimpiariarImagen" runat="server" Value="false" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ibox-title">
                                <h3>Información de contacto</h3>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
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
                                <div class="hr-line-dashed"></div>
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
                            <div class="ibox-title">
                                <h3>Dirección</h3>
                            </div>
                            <div class="ibox-content">
                                <div class="form-group">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">

                                        <button type="button" data-toggle="modal" data-target="#ModalMap" class="btn btn-info">
                                            <span class="glyphicon glyphicon-map-marker"></span>&nbsp;<span id="ubicacion">Seleccionar Ubicación</span>
                                        </button>
                                        <asp:Button ID="btnBorrarDireccion" runat="server" Text="Borrar Dirección" CssClass="btn btn-warning" OnClick="BtnBorrarDireccion_Click" />
                                        <div class="modal fade" id="ModalMap" tabindex="-1" role="dialog">
                                            <div class="modal-dialog" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-body">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <label class="col-sm-2 control-label">Ubicación:</label>
                                                                <div class="col-sm-9">
                                                                    <input type="text" class="form-control" id="pacInput" placeholder="Ingrese una localización" />
                                                                </div>
                                                                <div class="col-sm-1">
                                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                                </div>
                                                            </div>
                                                            <div id="map" style="width: 100%; height: 400px;"></div>
                                                            <div class="clearfix">&nbsp;</div>
                                                            <div id="infowindow-content">
                                                                <img src="#" width="16" height="16" id="place-icon">
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
                                <asp:HiddenField ID="hdIdDireccionMaps" runat="server" />
                                <br />
                                <div class="hr-line-dashed"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Calle</label>

                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true" Text=""></asp:TextBox>
                                                <asp:HiddenField ID="hdCalle" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Número</label>

                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hdNumero" runat="server" />
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
                                <div class="hr-line-dashed"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Localidad</label>

                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hdLocalidad" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Provincia</label>

                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hdProvincia" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Barrio</label>

                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtBarrio" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                <asp:HiddenField ID="hdBarrio" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-2 control-label">Código Postal</label>

                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" MaxLength="10" autocomplete="off" ReadOnly="true"></asp:TextBox>
                                                <asp:HiddenField ID="hdCodigoPostal" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                    <div class="col-xs-12 col-md-4" style="text-align: right">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
                                    </div>
                                    <br />
                                    <br />
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
    <script src="js/plugins/pwstrength/pwstrength-bootstrap.min.js"></script>
    <script src="js/plugins/pwstrength/zxcvbn.js"></script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB59e_fh71w0-GgkFuO_cD5ILETaLOkX3w&libraries=places&callback=initMap" async defer></script>
    
    <script type="text/javascript">

        var FormInformacionPersonal = '#<%=FormInformacionPersonal.ClientID%>';
        var autocomplete;

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Pwstrength();
                LimpiarFileUploadImagen();
            });
        }       

        function ValidarForm() {

            jQuery.validator.addMethod("lettersonly", function (value, element) { return this.optional(element) || /^[a-zñÑáéíóúÁÉÍÓÚ\s]+$/i.test(value); }, "Este campo solo permite letras.");

            $(FormInformacionPersonal).validate({
                rules: {
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
                    <%=txtCalle.UniqueID%>: {
                        required: true
                    },
                    <%=txtNumero.UniqueID%>: {
                        required: true
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
                    },
                    <%=txtContraseniaNueva.UniqueID%>: {
                        required: true,
                        minlength: 8
                    },
                    <%=txtContraseniaNuevaConfirmar.UniqueID%>: {
                        required: true,
                        equalTo: "#<%=txtContraseniaNueva.ClientID %>",
                        minlength: 8
                    }
				},
            messages: {
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
                    <%=txtNumero.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtDepartamento.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtBarrio.UniqueID%>: {
                        required: "Este campo es requerido."
                    },
                    <%=txtContraseniaNueva.UniqueID%>: {
                        required: "Este campo es requerido.",
                        minlength: "Usa 8 caracteres o más para tu contraseña."
                    },
                    <%=txtContraseniaNuevaConfirmar.UniqueID%>: {
                        required: "Este campo es requerido.",
                        equalTo: "Las contraseñas no coinciden. Vuelva a intentarlo.",
                        minlength: "Usa 8 caracteres o más para tu contraseña."
                   }                    
                }
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

            // Limpiar caja de texto id: pacInput
            document.getElementById("pacInput").value = "";   
        }

        function Pwstrength() {
            var options1 = {};
            options1.ui = {
                container: "#pwd-container1",
                showVerdictsInsideProgressBar: true,
                viewports: {
                    progress: ".pwstrength_viewport_progress"
                }
            };
            options1.common = {
                debug: false
            };
            $('.example1').pwstrength(options1);
        }

        function ShowImagePreview(input) {  
            if (input.files && input.files[0]) {  
                var reader = new FileReader();  
                reader.onload = function (e) {  
                    $('#<%=imgPreview.ClientID%>').prop('src', e.target.result);  
                };  
                reader.readAsDataURL(input.files[0]);  
            }  
        }

        function LimpiarFileUploadImagen() {
            $('#btnLimpiarImagen').on('click', function () {                                   
                $('#<%=imgPreview.ClientID%>').prop('src', 'img/perfil_default.png');
                $('#<%=hdfLimpiariarImagen.ClientID%>').val('true');
                $('#<%=fuploadImagen.ClientID%>').val('');
            });
        }

    </script>
</asp:Content>
