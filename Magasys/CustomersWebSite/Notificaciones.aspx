<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="Notificaciones.aspx.cs" Inherits="PL.CustomersWebSite.Notificaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormNotificaciones" runat="server">
            <div class="row">
                <div class="col-lg-6">
                    <div class="ibox" id="ibox1">
                        <div class="ibox-title">
                            <h5>Notificaciones</h5>
                        </div>
                        <div class="ibox-content">
                            <a class="pull-right btn btn-xs btn-white" data-toggle="modal" data-target="#ModalEliminarNotificaciones" runat="server" visible="false" id="btnEliminarNotificaciones">Eliminar Todos</a>
                            <p>
                                Te informamos las novedades sobre el estado de tus reservas.
                            </p>                            
                            <div class="sk-spinner sk-spinner-double-bounce">
                                <div class="sk-double-bounce1"></div>
                                <div class="sk-double-bounce2"></div>
                            </div>
                            <ul class="sortable-list connectList agile-list ui-sortable" id="todo">
                                <asp:ListView ID="lsvNotificaciones" runat="server">
                                    <ItemTemplate>                                        
                                        <li class="<%#Eval("TIPO_MENSAJE").ToString()%>" id="task1">                                                   
                                            <asp:Label ID="lblDescripcion" runat="server" Text='<%#Eval("DESCRIPCION").ToString()%>'></asp:Label>
                                            <div class="agile-detail">
                                                <a class="pull-right btn btn-xs btn-white" onclick="MarcarNotificacionComoEliminada(<%#Eval("ID_MENSAJE") %>);">Eliminar notificación</a>
                                                <i class="fa fa-clock-o"></i> <asp:Label ID="lblFechaRegistroMensaje" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA_REGISTRO_MENSAJE")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                            </div>
                                        </li>                                        
                                    </ItemTemplate>
                                </asp:ListView>
                                <div id="dvMensajeLsvNotificaciones" runat="server" />
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="ModalEliminarNotificaciones" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content animated bounce">
                        <div class="modal-body">                           
                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Desea eliminar todas las Notificaciones?</h2>
                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                            </p>
                        </div>
                        <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                data-dismiss="modal" />
                            <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; 
                            color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #558bdd"
                                OnClick="btnAceptar_Click" />
                        </div>
                    </div>                    
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        function MarcarNotificacionComoEliminada(idMensaje) {
            $.ajax({
                type: "POST",
                url: "Notificaciones.aspx/MarcarNotificacionComoEliminada",
                data: JSON.stringify({ 'pIdMensaje': idMensaje }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {                   
                    if (data.d != "") {
                        if (data.d == true) {
                            location.reload();
                        }
                    }
                },
                failure: function (data) {
                    swal({
                        title: "Notificación",
                        text: "Ocurrio un error, no se puedo marcar el mensaje como visto.",
                        type: "warning",
                        confirmButtonText: 'Aceptar'
                    });
                }
            });
        }
    </script>
</asp:Content>
