<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProveedorListado.aspx.cs" Inherits="PL.AdminDashboard.ProveedorListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server" />
<asp:Content ID="Content3" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Listado de Proveedores</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Proveedores
                </li>
                <li class="active">
                    <strong>Lista de Proveedores</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormProveedorListado" runat="server">
            <asp:ScriptManager ID="smgProveedorListado" runat="server"></asp:ScriptManager>
            <div class="row">
                <div class="col-lg-8">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Proveedor</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Código</label>
                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="12"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">CUIT</label>
                                            <asp:TextBox ID="txtCuitBusqueda" runat="server" CssClass="form-control" placeholder="99-99999999-9" data-mask="99-99999999-9" MaxLength="13"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <label class="col-sm-10 control-label">Fecha Alta</label>
                                        <div id="datePickerRange" class="form-group">
                                            <div class="input-daterange input-group">
                                                <asp:TextBox ID="txtFechaAltaDesde" runat="server" CssClass="input-sm form-control"></asp:TextBox>
                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                <asp:TextBox ID="txtFechaAltaHasta" runat="server" CssClass="input-sm form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Razón Social</label>
                                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="hr-line-dashed"></div>
                                <div class="form-group">
                                    <div class="col-sm-4 col-sm-offset-8">
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
                <div class="col-lg-4">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Alta Proveedor <small>Ingrese el CUIT</small></h5>
                        </div>
                        <div class="ibox-content">
                            <asp:UpdatePanel ID="uplAltaProveedor" runat="server">
                                <ContentTemplate>
                                    <div class="text-center">
                                        <div class="form-group">
                                            <asp:TextBox ID="txtCuitAlta" runat="server" placeholder="99-99999999-9" CssClass="form-control" data-mask="99-99999999-9" MaxLength="13"></asp:TextBox>
                                        </div>
                                        <button type="button" id="btnCrearProveedor" runat="server" class="btn btn-info" onserverclick="BtnCrearProveedor_Click">
                                            <i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Crear</span>
                                        </button>
                                    </div>
                                    <div id="dvMensajeCuit" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-content">
                            <asp:ListView ID="lsvProveedores" runat="server" OnItemDataBound="LsvProveedores_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="5">
                                        <thead>
                                            <tr>
                                                <th>CUIT</th>
                                                <th data-hide="phone,tablet">Raz&oacute;n Social</th>
                                                <th data-hide="phone,tablet">Fecha Alta</th>
                                                <th data-hide="phone,tablet">Nombre</th>
                                                <th data-hide="phone,tablet">Apellido</th>
                                                <th data-hide="phone,tablet">Email</th>
                                                <th data-hide="phone,tablet">Tel&eacute;fono Movil</th>
                                                <th class="text-center" data-sort-ignore="true">Acti&oacute;n</th>
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
                                        <td>
                                            <asp:Label ID="lblCuit" runat="server" Text='<%#Eval("CUIT")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblRazonSocial" runat="server" Text='<%#Eval("RAZON_SOCIAL")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaAlta" runat="server" Text='<%#Eval("FECHA_ALTA","{0:dd/MM/yyyy}")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApellido" runat="server" Text='<%#Eval("APELLIDO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("EMAIL")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTelefonoMovil" runat="server" Text='<%#Eval("TELEFONO_MOVIL")%>'></asp:Label>
                                        </td>
                                        <td class="text-center">
                                            <div class="btn-group">
                                                <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>
                                                <button runat="server" id="btnModificar" class="btn btn-outline btn-info  btn-xl" title="Modificar" onserverclick="BtnModificar_Click"><i class="fa fa-pencil"></i></button>
                                                <a class="btn btn-outline btn-danger btn-xl" data-toggle="modal" data-target="#ModalProveedorBaja" title="Eliminar" onclick="CargarIdCuitModalProveedorBaja(this);"><i class="fa fa-trash-o"></i>
                                                    <asp:HiddenField ID="hdIdCuitProveedorBaja" runat="server" />
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvProveedores" runat="server" />
                            <div id="ModalProveedorBaja" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content animated bounce">
                                        <div class="modal-body">
                                            <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #F8BB86;">
                                                <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #F8BB86;"></span>
                                                <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #F8BB86;"></span>
                                            </div>
                                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que quiere eliminar el Proveedor?</h2>
                                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                                Se eliminará el proveedor con CUIT: <b>
                                                    <asp:Label ID="lblCuitProveedorBaja" runat="server"></asp:Label></b>.
                                            </p>
                                        </div>
                                        <div class="modal-footer" style="text-align: center; padding-top: 0px;">
                                            <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-default" Text="Cancelar"
                                                Style="background-color: #D0D0D0; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer;"
                                                data-dismiss="modal" />
                                            <asp:Button ID="btnBaja" runat="server" CssClass="btn btn-danger" Text="Aceptar" Style="display: inline-block; box-shadow: rgba(221, 107, 85, 0.8) 0px 0px 2px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px inset; color: white; border: none; box-shadow: none; font-size: 17px; font-weight: 500; -webkit-border-radius: 4px; border-radius: 5px; padding: 10px 32px; margin: 26px 5px 0 5px; cursor: pointer; background-color: #DD6B55"
                                                OnClick="BtnBaja_Click" />
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdIdProveedorBaja" runat="server" Value="" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        var hdIdProveedorBaja = '#<%=hdIdProveedorBaja.ClientID%>';
        var lblCuitProveedorBaja = '#<%=lblCuitProveedorBaja.ClientID%>';
    </script>
    <script src="js/magasysProveedorListado.js"></script>
</asp:Content>
