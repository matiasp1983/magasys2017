<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="HojaDeRuta.aspx.cs" Inherits="PL.AdminDashboard.HojaDeRuta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/style.timeline.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Planificación de repartos</h2>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormHojaDeRuta" runat="server">            
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Modo de viaje</h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="col-md-3">
                                                <div class="form-group space-15">
                                                    <button id="btnDriving" runat="server" class="btn btn-outline btn-success dim  active" type="button" title="En auto" onserverclick="btnDriving_ServerClick"><i class="fas fa-car"></i></button>
                                                    <button id="btnWalking" runat="server" class="btn btn-outline btn-success dim" type="button" title="A pie" onserverclick="btnWalking_ServerClick"><i class="fas fa-walking"></i></button>
                                                    <button id="btnBiking" runat="server" class="btn btn-outline btn-success dim" type="button" title="En bicicleta" onserverclick="btnBiking_ServerClick"><i class="fas fa-biking"></i></button>
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
                                    <asp:ListView ID="lsvReparto" runat="server">
                                        <LayoutTemplate>
                                            <div class="container">
                                                <div class="row">
                                                    <div class="col-md-9 col-md-offset-1">
                                                        <div class="main-timeline">
                                                            <div id="itemPlaceholder" runat="server"></div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="timeline">
                                                <div class="timeline-icon"><%#Eval("ICONO")%></div>
                                                <div class="timeline-content">
                                                    <h3 class="title"><%#Eval("CLIENTE")%></h3>
                                                    <h3 class="font-bold">Dirección Origen</h3>
                                                    <p><%#Eval("DIRECCION_ORIGEN")%></p>
                                                    <h3 class="font-bold">Dirección Destino</h3>
                                                    <p><%#Eval("DIRECCION_DESTINO")%></p>
                                                    <p><b><%#Eval("TIPO_DOCUMENTO")%></b> <%#Eval("NRO_DOCUMENTO")%></p>
                                                    <p><b>TELÉFONO MÓVIL</b> <%#Eval("TELEFONO_MOVIL")%></p>
                                                </div>
                                                <div class="border"></div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:ListView>
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
</asp:Content>
