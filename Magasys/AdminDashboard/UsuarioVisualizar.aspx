<%@ Page Title="Datos del UsuarioVisualizar" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="UsuarioVisualizar.aspx.cs" Inherits="PL.UsuarioVisualizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormUsuarioVisualizar" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Producto</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Usuarios
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Visualizar Usuario"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="col-md-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2>Informaci&oacute;n General</h2>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha Alta</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" disabled="" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" MaxLength="50" disabled="" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre de Usuario</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control" disabled="" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Contrase&ntilde;a</label>

                                <div class="col-sm-5">
                                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control" disabled="" MaxLength="30" autocomplete="off" TextMode="Password"></asp:TextBox>
                                </div>                                
                            </div>
                        </div>                       
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Rol de Usuario</label>

                                <div class="col-sm-10">
                                    <div id="divRol">
                                        <%--<asp:DropDownList ID="ddlRol" runat="server" CssClass="select2_rol form-control" disabled=""></asp:DropDownList>--%>
                                        <asp:TextBox ID="txtRol" runat="server" CssClass="form-control" disabled="" autocomplete="off"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h2>Avatar</h2>
                    </div>
                    <div class="ibox-content">
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-3 col-md-offset-3">
                                    <asp:Image ID="imgPreview" Width="200px" runat="server" ImageUrl="~/AdminDashboard/img/perfil_default.png" />
                                </div>
                            </div>
                        </div>                                                
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="BtnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" formnovalidate="" />
                        </div>
                    </div>
                </div>
            </div>
        </div>        
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">       

        if (window.jQuery) {
            $(document).ready(function () {                
                Select2();                
            });
        }

        function Select2() {
            $(".select2_rol").select2(
                {
                    placeholder: 'Seleccione un Rol',
                    width: '100%',
                    allowClear: true
                });
        }       
        
    </script>
</asp:Content>
