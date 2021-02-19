<%@ Page Title="Listado de Productos" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoListado.aspx.cs" Inherits="PL.AdminDashboard.ProductoListado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Listado de Productos</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Productos
                </li>
                <li class="active">
                    <strong>Lista de Productos</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormProductoListado" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Buscar Producto</h5>
                        </div>
                        <div class="ibox-content">
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Código</label>
                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" MaxLength="12" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Nombre</label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Tipo de Producto</label>
                                            <asp:DropDownList ID="ddlTipoProducto" runat="server" CssClass="select2_tipoproducto form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Estado</label>
                                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="select2_estado form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-10 control-label">Proveedor</label>
                                            <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="select2_proveedor form-control" ></asp:DropDownList>
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
                                <button type="button" id="btnNuevo" runat="server" class="ladda-button btn btn-info" onserverclick="BtnNuevo_Click">
                                    <i class="fa fa-plus"></i>&nbsp;&nbsp;<span>Nuevo Producto</span>
                                </button>
                            </div>
                            <br />
                            <asp:ListView ID="lsvProductos" runat="server" OnItemDataBound="LsvProductos_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th class="text-left">Código</th>
                                                <th data-hide="phone,tablet">Tipo de Producto</th>
                                                <th data-hide="phone,tablet">Nombre</th>
                                                <th data-hide="phone,tablet">Estado</th>
                                                <th data-hide="phone,tablet">G&eacute;nero</th>
                                                <th data-hide="phone,tablet">Proveedor</th>
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
                                            <asp:Label ID="lblCodigo" runat="server" Text='<%#Eval("ID_PRODUCTO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTipoProducto" runat="server" Text='<%#Eval("DESC_TIPO_PRODUCTO").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNombre" runat="server" Text='<%#Eval("NOMBRE").ToString().Length > 23 ? String.Format("{0}...", Eval("NOMBRE").ToString().Remove(23).TrimEnd()):Eval("NOMBRE")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEstado" runat="server" Text='<%#Eval("DESC_ESTADO").ToString().Length > 13 ? String.Format("{0}...", Eval("DESC_ESTADO").ToString().Remove(13).TrimEnd()):Eval("DESC_ESTADO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblGenero" runat="server" Text='<%#Eval("DESC_GENERO").ToString().Length > 13 ? String.Format("{0}...", Eval("DESC_GENERO").ToString().Remove(13).TrimEnd()):Eval("DESC_GENERO")%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblProveedor" runat="server" Text='<%#Eval("DESC_PROVEEDOR").ToString().Length > 13 ? String.Format("{0}...", Eval("DESC_PROVEEDOR").ToString().Remove(13).TrimEnd()):Eval("DESC_PROVEEDOR")%>'></asp:Label>
                                        </td>
                                        <td class="text-right">
                                            <div class="btn-group">
                                                <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>
                                                <button runat="server" id="btnModificar" class="btn btn-outline btn-info  btn-xl" title="Modificar" onserverclick="BtnModificar_Click"><i class="fa fa-pencil"></i></button>
                                                <a class="btn btn-outline btn-danger btn-xl" data-toggle="modal" data-target="#ModalProductoBaja" title="Eliminar" onclick="CargarIdModalProductoBaja(this);"><i class="fa fa-trash-o"></i>
                                                    <asp:HiddenField ID="hdProductoBaja" runat="server" />
                                                </a>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                            <div id="dvMensajeLsvProductos" runat="server" />
                            <div id="ModalProductoBaja" class="modal" tabindex="-1" role="dialog" aria-hidden="true" data-keyboard="false" data-backdrop="static">
                                <div class="modal-dialog">
                                    <div class="modal-content animated bounce">
                                        <div class="modal-body">
                                            <div style="display: block; width: 80px; height: 80px; border: 4px solid gray; border-radius: 50%; margin: 20px auto; padding: 0; position: relative; box-sizing: content-box; border-color: #F8BB86;">
                                                <span style="position: absolute; width: 5px; height: 47px; left: 50%; top: 10px; -webkit-border-radius: 2px; border-radius: 2px; margin-left: -2px; background-color: #F8BB86;"></span>
                                                <span style="position: absolute; width: 7px; height: 7px; -webkit-border-radius: 50%; border-radius: 50%; margin-left: -3px; left: 50%; bottom: 10px; background-color: #F8BB86;"></span>
                                            </div>
                                            <h2 style="color: #575757; font-size: 30px; text-align: center; font-weight: 600; text-transform: none; position: relative; margin: 25px 0; padding: 0; line-height: 40px; display: block;">¿Está seguro que quiere dar de baja el Producto?</h2>
                                            <p style="color: #797979; font-size: 16px; font-weight: 300; position: relative; text-align: center; float: none; margin: 0; padding: 0; line-height: normal;">
                                                Se dará de baja el producto con Código: <b>
                                                    <asp:Label ID="lblProductoBaja" runat="server"></asp:Label></b>.
                                           
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
                                    <asp:HiddenField ID="hdIdProductoBaja" runat="server" Value="" />
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
        var hdIdProductoBaja = '#<%=hdIdProductoBaja.ClientID%>';
        var lblProductoBaja = '#<%=lblProductoBaja.ClientID%>';
    </script>
    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {                
                KeypressEnterDisabled();
                LoadFootable();
                Select2();
            });
        }

        function KeypressEnterDisabled()
        {
            $('input').keypress(function (e) {
                if (e.which == 13) {
                    return false;
                }
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }

        function Select2() {
            $(".select2_tipoproducto").select2(
                {
                    placeholder: 'Seleccione un Tipo de Producto',
                    width: '100%',
                    allowClear: true
                });

            $(".select2_estado").select2(
                {
                    placeholder: 'Seleccione un Estado',
                    width: '100%',
                    allowClear: true                    
                });

             $(".select2_proveedor").select2(
                {
                    placeholder: 'Seleccione un Proveedor',
                    width: '100%',
                    allowClear: true                    
                });
        }

        function CargarIdModalProductoBaja(control) {
            if (window.jQuery) {
                var loId = control.lastElementChild.defaultValue;
                $(hdIdProductoBaja).val(loId);
                $(lblProductoBaja).text(loId);
            }
        }
    </script>
</asp:Content>
