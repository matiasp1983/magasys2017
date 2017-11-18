<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="PL.AdminDashboard.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="menu" runat="server" />
<asp:Content ID="Content3" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormProducto" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="smgProducto" runat="server"></asp:ScriptManager>
        <div class="row wrapper border-bottom white-bg page-heading">
            <div class="col-lg-10">
                <h2>Datos del Producto</h2>
                <ol class="breadcrumb">
                    <li>Principal
                    </li>
                    <li>Producto
                    </li>
                    <li class="active">
                        <strong>
                            <asp:Label ID="lblBreadcrumbActive" runat="server" Text="Alta de Producto"></asp:Label>
                        </strong>
                    </li>
                </ol>
            </div>
        </div>
        <div class="wrapper wrapper-content animated fadeInRight">
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
                                <label class="col-sm-2 control-label">Nombre</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Descripci&oacute;n</label>

                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Proveedor</label>

                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlProveedor" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">G&eacute;nero</label>

                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlGenero" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="tabs-container">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a data-toggle="tab" href="#tab-1">Diario</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-2">Revista</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-3">Colecci&oacute;n</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-4">Libro</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-5">Suplemento</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-6">Pel&iacute;cula</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Lunes </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioLunesDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Martes </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioMartesDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Miercoles </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioMiercolesDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Jueves </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioJuevesDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Viernes </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioViernesDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio S&aacute;bado </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioSabadoDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Domingo </label>

                                                            <div class="col-sm-8">
                                                                <asp:TextBox ID="txtPrecioDomingoDiario" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">D&iacute;a de entrega</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlDiaDeEntregaRevista" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Periodicidad</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlPeriodicidadRevista" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtPrecioRevista" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-3" class="tab-pane">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">D&iacute;a de entrega</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlDiaDeEntregaColeccion" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Periodicidad</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlPeriodicidadColeccion" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Cantidad de entregas</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtCantidadDeEntregaColeccion" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-4" class="tab-pane">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Autor</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAutorLibro" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">A&ntilde;o de edici&oacute;n</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAnioEdicionLibro" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Editorial</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtEditorialLibro" runat="server" CssClass="form-control" MaxLength="50"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtPrecioLibro" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-5" class="tab-pane">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">D&iacute;a de entrega</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlDiaDeEntregaSuplemento" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Diario</label>

                                                            <div class="col-sm-9">
                                                                <asp:DropDownList ID="ddlDiarioSuplemento" runat="server" CssClass="form-control m-b" AutoPostBack="True"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtPrecioSuplemento" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Cantidad de entregas</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtCantidadDeEntregaSuplemento" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="tab-6" class="tab-pane">
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">A&ntilde;o de estreno</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtAnioDeEstrenoPelicula" runat="server" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtPrecioPelicula" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
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
                        <div class="col-xs-8 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" />
                            <a id="btnCancelar" runat="server" class="btn btn-primary">Cancelar</a>
                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </div>
            </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
