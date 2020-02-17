<%@ Page Title="" Language="C#" MasterPageFile="~/CustomersWebSite/MasterPage.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PL.CustomersWebSite.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="wrapper wrapper-content">
        <form id="FormIndex" runat="server">
            <div class="container">
                <div class="row">
                    <div class="col-md-3">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <span class="label label-success pull-right">Hoy</span>
                                <h5>Total a Pagar</h5>
                            </div>
                            <div class="ibox-content">
                                <h1 class="no-margins"><asp:Label ID="lblTotalAPagar" runat="server"></asp:Label></h1>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <span class="label label-info pull-right">Mensuales</span>
                                <h5>Reservas Confirmadas</h5>
                            </div>
                            <div class="ibox-content">
                                <h1 class="no-margins"><asp:Label ID="lblReservasConfirmadas" runat="server"></asp:Label></h1>
                            </div>
                        </div>
                    </div>                 
                </div>      

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Consultar Reservas</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label">Estado</label>
                                            <div id="divEstado">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>                                            
                                            </div>  
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label">Tipo de Reserva</label>
                                            <div id="divTipoReserva">
                                                <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="select2_tiporeserva form-control"></asp:DropDownList>                                            
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label" for="customer">Nombre del Producto</label>
                                            <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4">
                                        <div class="form-group" id="dpFechaInicio">
                                            <label class="control-label">Fecha Inicio de Reserva</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaIniReserva" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group" id="dpFechaFin">
                                            <label class="control-label">Fecha Fin de Reserva</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaFinReserva" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label class="control-label">Forma de Entrega</label>
                                            <div id="divFormaPago">
                                                <asp:DropDownList ID="ddlFormaEntrega" runat="server" CssClass="select2_formaentrega form-control"></asp:DropDownList>                                            
                                            </div>  
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="hr-line-dashed"></div>
                                    <div class="form-group">
                                        <div style="text-align: right; padding-right: 15px;">
                                            <button type="button" id="btnBuscar" runat="server" class="ladda-button btn btn-success" onserverclick="BtnBuscar_Click">
                                                <i class="fa fa-search"></i>&nbsp;&nbsp;<span>Buscar</span>
                                            </button>
                                            <button type="reset" id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">
                                                <i class="fa fa-trash-o"></i>&nbsp;&nbsp;<span>Limpiar</span>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox">
                                <div class="ibox-content">
                                    <asp:ListView ID="lsvReservas" runat="server" OnItemDataBound="LsvReservas_ItemDataBound">
                                        <LayoutTemplate>
                                            <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                                <thead>
                                                    <tr>
                                                        <th>C&oacute;digo</th>
                                                        <th>Fecha de solicitud</th>
                                                        <th data-hide="phone,tablet" >Producto</th>
                                                        <th>Tipo de Reserva</th>
                                                        <th data-hide="phone">Forma de Entrega</th>
                                                        <th data-hide="phone,tablet" >Inicio de Reserva</th>
                                                        <th data-hide="phone,tablet" >Fin de Reserva</th>
                                                        <th class="text-right">Acci&oacute;n</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr id="itemPlaceholder" runat="server"></tr>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td colspan="8">
                                                            <ul class="pagination pull-right"></ul>
                                                        </td>
                                                    </tr>
                                                </tfoot>
                                            </table>                                    
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="text-left">
                                                    <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>'></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="lblFechaAlta" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 23 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(23).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                                </td>  
                                                <td class="text-left">
                                                    <asp:Label ID="lblTipoReserva" runat="server" Text='<%#Eval("TIPO_RESERVA").ToString()%>'></asp:Label>
                                                </td>  
                                                <td class="text-left">
                                                    <asp:Label ID="lblFormaEntrega" runat="server" Text='<%#Eval("FORMA_ENTREGA").ToString()%>'></asp:Label>
                                                </td>  
                                                <td class="text-left">
                                                   <asp:Label ID="lblFechaInicio" runat="server" Text='<%#(Eval("FECHA_INICIO") != null) ? Convert.ToDateTime(Eval("FECHA_INICIO")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                                </td>
                                                <td class="text-left">
                                                    <asp:Label ID="lblFechaFin" runat="server" Text='<%#(Eval("FECHA_FIN") != null) ? Convert.ToDateTime(Eval("FECHA_FIN")).ToString("dd/MM/yyyy"):null%>'></asp:Label>
                                                </td>
                                                <td class="text-right">
                                                    <div class="btn-group">
                                                        <%--<button runat="server" id="btnModificar" class="btn btn-outline btn-info  btn-xl" title="Modificar" onserverclick="BtnModificar_Click"><i class="fa fa-pencil"></i></button>--%>
                                                        <a class="btn btn-outline btn-danger btn-xl" data-toggle="modal" data-target="#ModalReservaAnular" title="Anular" onclick="CargarIdReservaAnular(this);"><i class="fa fa-trash-o"></i>
                                                            <asp:HiddenField ID="hdIdReserva" runat="server" />
                                                        </a>  
                                                    </div>
                                                </td>
                                            </tr>
                                        </ItemTemplate>                       
                                    </asp:ListView>
                                    <div id="dvMensajeLsvReservas" runat="server" />
                                    <div id="ModalReservaAnular" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                        <div class="modal-dialog">
                                            <div class="modal-content animated bounce">
                                                <div class="modal-body">
                                                    <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #F8BB86;">
                                                        <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #F8BB86;"></span>
                                                        <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #F8BB86;"></span>
                                                    </div>
                                                    <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que quiere anular la Reserva?</h2>
                                                    <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                           
                                                    </p>
                                                </div>
                                                <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                                    <asp:Button ID="btnCancelarAnular" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                        Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                        data-dismiss="modal" />
                                                    <asp:Button ID="btnAnular" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #DD6B55"
                                                        OnClick="BtnAnular_Click" />
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdIdReservaAnular" runat="server" Value="" />
                                        </div>
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
    <script type="text/javascript">
        var hdIdReservaAnular = '#<%=hdIdReservaAnular.ClientID%>';
    </script>
    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {                
                LoadDatePicker();
                LoadFootable();
                Select2();
            });
        }

        function LoadDatePicker() {
            $('#dpFechaInicio .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaFin .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function Select2() {
            $(".select2_tiporeserva").select2(
                {
                    placeholder: 'Seleccione un Tipo de Reserva',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_formaentrega").select2(
                {
                    placeholder: 'Seleccione una Forma de Entrega',
                    width: '100%',
                    allowClear: true
                });
        }

        function CargarIdReservaAnular(control) {
            if (window.jQuery) {
                var loIdReserva = control.lastElementChild.defaultValue;
                $(hdIdReservaAnular).val(loIdReserva);
            }
        }
    </script>
</asp:Content>
