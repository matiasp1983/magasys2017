<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ReservaAnular.aspx.cs" Inherits="PL.AdminDashboard.ReservaAnular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    <link href="css/plugins/iCheck/custom.css" rel="stylesheet">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">  
    <form id="FormReservaAnular" runat="server"> 
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Anular Reservas</h2>
                <ol class="breadcrumb">
                    <li>
                        <a href="Index.aspx">Principal</a>
                    </li>
                    <li>Reserva
                    </li>
                    <li class="active">
                        <strong>Anular Reservas</strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">               
            <div class="row">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Buscar Reserva</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Tipo de Reserva</label>   
                                                <div id="divTipoReserva">
                                                    <asp:DropDownList ID="ddlTipoReserva" runat="server" CssClass="select2_tiporeserva form-control"></asp:DropDownList>                                            
                                                </div>   
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Nombre del Producto</label>
                                                <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Estado</label>   
                                                <div id="divEstado">
                                                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select2_estado form-control"></asp:DropDownList>                                            
                                                </div>   
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">      
                                    <div class="ibox-title">
                                        <h5>Cliente</h5>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Tipo de Documento</label>
                                                <div id="divTipoDocumento">
                                                    <asp:DropDownList ID="ddlTipoDocumento" runat="server" CssClass="select2_tipodocumento form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Número de Documento</label>
                                                <asp:TextBox ID="txtNroDocumento" runat="server" CssClass="form-control" MaxLength="8" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Nombre</label>
                                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Apellido</label>
                                                <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-sm-10 control-label">Alias</label>
                                                <asp:TextBox ID="txtAlias" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                            <div style="text-align: right;">
                                <a class="ladda-button btn btn-info" data-toggle="modal" data-target="#ModalAnularReserva" title="Eliminar" onclick="AnularReserva();">
                                    <i class="fas fa-clock"></i>&nbsp;&nbsp;<span>Ejecutar</span>
                                </a>
                            </div>
                            <br />
                            <asp:ListView ID="lsvReservas" runat="server" Visible="true">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>                                                    
                                                <td>                                                        
                                                    <button id="check-all" class="btn btn-sm btn-primary pull-left m-t-n-xs">Seleccionar todo</button>
                                                </td>
                                                <th class="text-left">Reserva</th>    
                                                <th data-hide="phone,tablet">Cliente</th>
                                                <th class="text-left">Producto</th>
                                                <th data-hide="phone,tablet">Tipo de Reserva</th>
                                                <th data-hide="phone,tablet">Estado</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="30">
                                                    <ul class="pagination pull-right"></ul>
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td>                                                
                                            <input id="chkCodigoReserva" runat="server" class="i-checks" type="checkbox" Visible="true"/>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblIdReserva" runat="server" Text='<%#Eval("ID_RESERVA").ToString()%>'></asp:Label>
                                        </td>
                                        <td class="text-left">
                                            <asp:Label ID="lblCliente" runat="server" Text='<%#Eval("NOMBRE_CLIENTE").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPorducto" runat="server" Text='<%#Eval("NOMBRE_PRODUCTO").ToString().Length > 50 ? String.Format("{0}...", Eval("NOMBRE_PRODUCTO").ToString().Remove(50).TrimEnd()):Eval("NOMBRE_PRODUCTO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoReserva" runat="server" Text='<%#Eval("TIPO_RESERVA").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("ESTADO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblCodCliente" runat="server" Text='<%#Eval("COD_CLIENTE").ToString()%>' Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvReservas" runat="server" />
                            <div id="ModalAnularReserva" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content animated bounce">
                                        <div class="modal-body">
                                            <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #86caf8;">
                                                <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #86caf8;"></span>
                                                <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #86caf8;"></span>
                                            </div>
                                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que desea anular la Reserva?</h2>
                                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                            </p>
                                        </div>
                                        <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                            <asp:Button ID="btnCancelarAccion" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                data-dismiss="modal" />
                                            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #558bdd"
                                                OnClick="BtnGuardar_Click" />
                                        </div>
                                    </div>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>        
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <!-- iCheck -->
    <script src="js/plugins/iCheck/icheck.min.js"></script>

    <script type="text/javascript">

        if (window.jQuery) {
            $(document).ready(function () {
                $('.i-checks').iCheck({
                    checkboxClass: 'icheckbox_square-green'
                });

                var checked = false;

                $('#check-all').on('click', function () {
                    if (checked == false) {
                        $('.i-checks').prop('checked', true).iCheck('update');
                        checked = true;
                    } else {
                        $('.i-checks').prop('checked', false).iCheck('update');
                        checked = false;
                    }
                });
                LoadFootable();
                Select2();
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

            $(".select2_estado").select2(
                {
                    placeholder: 'Seleccione un Estado',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_tipodocumento").select2(
                {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
                });
        }
    </script>
</asp:Content>
