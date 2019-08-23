<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="CobroListado.aspx.cs" Inherits="PL.AdminDashboard.CobroListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Listado de Cobros</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Clientes
                </li>
                <li class="active">
                    <strong>Lista de Cobros</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormCobroListado" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Cobro</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group" id="dpFechaDesde">
                                            <label class="control-label">Fecha Cobro Desde</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaCobroDesde" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group" id="dpFechaHasta">
                                            <label class="control-label">Fecha Cobro Hasta</label>
                                            <div class="input-group date">
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaCobroHasta" runat="server" CssClass="input-sm form-control" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Código de Cobro</label>
                                            <asp:TextBox ID="txtCodigoCobro" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Estado</label>   
                                            <div id="divEstado">
                                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control"></asp:DropDownList>                                            
                                            </div>  
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">      
                                <div class="ibox-title">
                                    <h5>Clientes</h5>
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
                            <asp:ListView ID="lsvCobros" runat="server" OnItemDataBound="LsvCobros_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="5">
                                        <thead>
                                            <tr>
                                                <th class="text-left">Código</th>
                                                <th class="text-left">Fecha Cobro</th>
                                                <th data-hide="phone,tablet">Nombre del Cliente</th>
                                                 <th data-hide="phone,tablet">Total</th>
                                                <th class="text-right" data-sort-ignore="true">Acci&oacute;n</th>
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
                                            <asp:Label ID="lblCodigoCobro" runat="server" Text='<%#Eval("ID_COBRO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaCobro" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                        </td>    
                                        <td class="text-left">
                                            <asp:Label ID="lblNombreCliente" runat="server" Text='<%#(Eval("NOMBRE_CLIENTE") != null) ? Eval("NOMBRE_CLIENTE").ToString():null%>'></asp:Label>
                                        </td>       
                                        <td class="text-left">
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                        </td>                                          
                                        <td class="text-right">
                                            <div class="btn-group">
                                                <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>
                                                <a class="btn btn-outline btn-danger btn-xl" data-toggle="modal" data-target="#ModalCobroAnular" title="Anular" onclick="CargarIdCobroAnular(this);"><i class="fa fa-trash-o"></i>
                                                    <asp:HiddenField ID="hdIdCobro" runat="server" />
                                                </a>                                                
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvCobros" runat="server" />   
                            <div id="ModalCobroAnular" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content animated bounce">
                                        <div class="modal-body">
                                            <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #F8BB86;">
                                                <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #F8BB86;"></span>
                                                <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #F8BB86;"></span>
                                            </div>
                                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que quiere anular el Cobro?</h2>
                                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                           
                                            </p>
                                        </div>
                                        <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                data-dismiss="modal" />
                                            <asp:Button ID="btnAnular" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #DD6B55"
                                                OnClick="BtnAnular_Click" />
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdIdCobroAnular" runat="server" Value="" />
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
        var hdIdCobroAnular = '#<%=hdIdCobroAnular.ClientID%>';
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
            $('#dpFechaDesde .input-group.date').datepicker({
                todayBtn: "linked",
                clearBtn: true,
                forceParse: true,
                autoclose: true,
                language: "es",
                format: "dd/mm/yyyy",
                keyboardNavigation: false
            });

            $('#dpFechaHasta .input-group.date').datepicker({
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
            $(".select2_tipodocumento").select2(
            {
                    placeholder: 'Seleccione un Tipo de Documento',
                    width: '100%',
                    allowClear: true
            });
        }

        function CargarIdCobroAnular(control) {
            if (window.jQuery) {
                var loIdCobro = control.lastElementChild.defaultValue;
                $(hdIdCobroAnular).val(loIdCobro);
            }
        }
    </script>
</asp:Content>
