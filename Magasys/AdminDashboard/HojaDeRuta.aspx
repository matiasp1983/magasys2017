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
                                                    <button id="btnDriving" runat="server" class="btn btn-outline btn-success dim  active" type="button" onserverclick="btnDriving_ServerClick"><i class="fas fa-car"></i></button>
                                                    <button id="btnWalking" runat="server" class="btn btn-outline btn-success dim" type="button" onserverclick="btnWalking_ServerClick"><i class="fas fa-walking"></i></button>
                                                    <button id="btnBiking" runat="server" class="btn btn-outline btn-success dim" type="button" onserverclick="btnBiking_ServerClick"><i class="fas fa-biking"></i></button>
                                                </div>
                                            </div>
                                            <div class="col-md-9">
                                                <div class="form-group">
                                                    <label class="control-label">Punto de partida</label>
                                                    <asp:TextBox ID="txtPutoDePartida" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off" Enabled="false"></asp:TextBox>
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
            </asp:UpdatePanel>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
