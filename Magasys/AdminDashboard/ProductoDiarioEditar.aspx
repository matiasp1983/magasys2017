﻿<%@ Page Title="Datos del Producto" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="ProductoDiarioEditar.aspx.cs" Inherits="PL.AdminDashboard.ProductoDiarioEditar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormProductoDiarioEditar" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Producto</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Productos
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Editar Producto Diario"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
                    <div class="row" id="divRowCodigoFechaAlta" runat="server">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Código</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha Alta</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaAlta" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Descripci&oacute;n</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Proveedor</label>

                                <div class="col-sm-10">
                                    <div id="divProveedor">
                                        <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="select2_proveedor form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">G&eacute;nero</label>

                                <div class="col-sm-10">
                                    <div id="divGenero">
                                        <asp:DropDownList ID="ddlGenero" runat="server" CssClass="select2_genero form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div id="divDiario" runat="server" class="panel-body" visible="true">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Dia de la Semana </label>

                                            <div class="col-sm-8">
                                                <div class="input-group m-b" id="divPrecioLunesDiario">
                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                    <asp:TextBox ID="txtDiaDeLaSemanaDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off" Enabled="False"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">Precio </label>

                                            <div class="col-sm-8">
                                                <div class="input-group m-b" id="divPrecioMartesDiario">
                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                    <asp:TextBox ID="txtPrecioDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                             </div>                            
                        </div>
                    </div>
                    <div class="hr-line-dashed"></div>                    
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                         <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                            <a id="btnCancelar" runat="server" class="btn btn-primary" onserverclick="BtnCancelar_Click">Cancelar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        var FormProducto = '#<%=FormProductoDiarioEditar.ClientID%>';

        if (window.jQuery) {
            $(document).ready(function () {
                ValidarForm();
                Select2();
            });
        }

        function ValidarForm() {
            $(FormProducto).validate({
                rules: {
                     <%=txtNombre.UniqueID%>: {
                        required: true,
                        normalizer: function (value) {                            
                            return $.trim(value);
                        }
                     },
                     <%=ddlProveedor.UniqueID%>: {
                         required: true
                     },
                     <%=ddlGenero.UniqueID%>: {
                         required: true
                     },
                     <%=txtDiaDeLaSemanaDiario.UniqueID%>: {
                         required: true,
                     },
                     <%=txtPrecioDiario.UniqueID%>: {
                         number: true,
                         min: 1
                     },
                     
                
        messages: {
                     <%=txtNombre.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                     <%=ddlProveedor.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=ddlGenero.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtDiaDeLaSemanaDiario.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtPrecioDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 1."
                     },        
                
            );
        }

        function Select2() {
            $(".select2_proveedor").select2(
            {
                    placeholder: 'Seleccione un Proveedor',
                    width: '100%',
                    allowClear: true                    
            });

            $(".select2_genero").select2(
            {
                    placeholder: 'Seleccione un Género',
                    width: '100%',
                    allowClear: true
            });
        }
    </script>
</asp:Content>
