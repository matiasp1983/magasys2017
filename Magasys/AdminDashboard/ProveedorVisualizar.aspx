<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProveedorVisualizar.aspx.cs" Inherits="PL.AdminDashboard.ProveedorVisualizar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <div class="row wrapper border-bottom white-bg page-heading">
        <div class="col-lg-10">
            <h2>Datos del Proveedor</h2>
            <ol class="breadcrumb">
                <li>
                    <a href="Index.aspx">Principal</a>
                </li>
                <li>Proveedores
                </li>
                <li class="active">
                    <strong>Visualizar Proveedor</strong>
                </li>
            </ol>
        </div>
    </div>
    <div class="wrapper wrapper-content animated fadeInRight">
        <form id="FormProveedorVisualizar" runat="server" class="form-horizontal">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                    </div>
                    <h2>Información General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha Alta</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">CUIT</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCuit" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Razón Social</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Movil</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b">
                                        <span class="input-group-addon"><i class="fa fa-mobile-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoMovil" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03516243492.</span>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Teléfono Fijo</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b">
                                        <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                        <asp:TextBox ID="txtTelefonoFijo" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                    </div>
                                    <span class="help-block m-b-none">Código de área + N°. Ej: 03514823455.</span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Email</label>

                                <div class="col-sm-10">
                                    <div class="input-group m-b">
                                        <span class="input-group-addon"><i class="fa fa-envelope-o"></i></span>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Dirección</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Calle</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-md-2 control-label">Número</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Piso</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>

                                <label class="col-md-1 control-label">Dpto</label>

                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Provincia</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Localidad</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Barrio</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtBarrio" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código Postal</label>

                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" disabled=""></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CssClass="btn btn-success" OnClick="BtnModificar_Click" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-primary" OnClick="BtnCancelar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server"/>
