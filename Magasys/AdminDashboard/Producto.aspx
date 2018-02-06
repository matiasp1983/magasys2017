<%@ Page Title="Datos del Producto" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="Producto.aspx.cs" Inherits="PL.AdminDashboard.Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormProducto" runat="server" class="form-horizontal">        
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
                    <h2>Informaci&oacute;n General</h2>
                </div>
                <div class="ibox-content">
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
                                                                <div class="input-group m-b" id="divPrecioLunesDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioLunesDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Martes </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioMartesDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioMartesDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Miercoles </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioMiercolesDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioMiercolesDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Jueves </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioJuevesDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioJuevesDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Viernes </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioViernesDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioViernesDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio S&aacute;bado </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioSabadoDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioSabadoDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-4 control-label">Precio Domingo </label>

                                                            <div class="col-sm-8">
                                                                <div class="input-group m-b" id="divPrecioDomingoDiario">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioDomingoDiario" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
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
                                                                <asp:DropDownList ID="ddlDiaDeEntregaRevista" runat="server" CssClass="select2_diadeentregarevista form-control"></asp:DropDownList>                                                                
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Periodicidad</label>

                                                            <div class="col-sm-9">
                                                                <div id="divPeriodicidadRevista">
                                                                    <asp:DropDownList ID="ddlPeriodicidadRevista" runat="server" CssClass="select2_periodicidadrevista form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <div class="input-group m-b" id="divPrecioRevista">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioRevista" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
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
                                                                <asp:DropDownList ID="ddlDiaDeEntregaColeccion" runat="server" CssClass="select2_diadeentregacoleccion form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Periodicidad</label>

                                                            <div class="col-sm-9">
                                                                <div id="divPeriodicidadColeccion">
                                                                    <asp:DropDownList ID="ddlPeriodicidadColeccion" runat="server" CssClass="select2_periodicidadcoleccion form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Cantidad de entregas</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtCantidadDeEntregaColeccion" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
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
                                                                <asp:TextBox ID="txtAutorLibro" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">A&ntilde;o de edici&oacute;n</label>

                                                            <div class="col-sm-9">                                                                
                                                                <div id="divAnioEdicionLibro">
                                                                    <asp:DropDownList ID="ddlAnioEdicionLibro" runat="server" CssClass="select2_anioeditoriallibro form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Editorial</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtEditorialLibro" runat="server" CssClass="form-control" MaxLength="50" autocomplete="off"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <div class="input-group m-b" id="divPrecioLibro">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioLibro" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
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
                                                                <asp:DropDownList ID="ddlDiaDeEntregaSuplemento" runat="server" CssClass="select2_diadeentregasuplemento form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Diario</label>

                                                            <div class="col-sm-9">
                                                                <div id="divDiarioSuplemento">
                                                                    <asp:DropDownList ID="ddlDiarioSuplemento" runat="server" CssClass="select2_diariosuplemento form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <div class="input-group m-b" id="divPrecioSuplemento">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioSuplemento" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Cantidad de entregas</label>

                                                            <div class="col-sm-9">
                                                                <asp:TextBox ID="txtCantidadDeEntregaSuplemento" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
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
                                                                <div id="divAnioDeEstrenoPelicula">
                                                                    <asp:DropDownList ID="ddlAnioDeEstrenoPelicula" runat="server" CssClass="select2_aniodeestrenopelicula form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-sm-3 control-label">Precio</label>

                                                            <div class="col-sm-9">
                                                                <div class="input-group m-b" id="divPrecioPelicula">
                                                                    <span class="input-group-addon"><i class="fa fa-usd"></i></span>
                                                                    <asp:TextBox ID="txtPrecioPelicula" runat="server" CssClass="form-control" MaxLength="6" autocomplete="off"></asp:TextBox>
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
                    </div>
                    <div class="hr-line-dashed"></div>
                    <div class="form-group">
                        <div class="col-xs-12 col-sm-6 col-md-8"></div>
                        <div class="col-xs-12 col-md-4" style="text-align: right">
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="BtnGuardar_Click" />
                            <a id="btnCancelar" runat="server" class="btn btn-primary" onserverclick="BtnCancelar_Click">Cancelar</a>
                            <a id="btnLimpiar" runat="server" class="btn btn-warning" onserverclick="BtnLimpiar_Click">Limpiar</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script type="text/javascript">
        var FormProducto = '#<%=FormProducto.ClientID%>';

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
                        required: true
                     },
                     <%=ddlProveedor.UniqueID%>: {
                         required: true
                     },
                     <%=ddlGenero.UniqueID%>: {
                         required: true
                     },
                     <%=txtPrecioLunesDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioMartesDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioMiercolesDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioJuevesDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioViernesDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioSabadoDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=txtPrecioDomingoDiario.UniqueID%>: {
                         number: true,
                         min: 0
                     },
                     <%=ddlPeriodicidadRevista.UniqueID%>: {
                         required: true
                     },
                     <%=txtPrecioRevista.UniqueID%>: {
                         required: true,
                         number: true,
                         min: 0
                     },
                     <%=ddlPeriodicidadColeccion.UniqueID%>: {
                         required: true
                     },
                     <%=txtCantidadDeEntregaColeccion.UniqueID%>: {
                         required: true,
                         number: true,
                         digits: true,
                         min: 0                         
                     },
                     <%=txtAutorLibro.UniqueID%>: {
                         required: true
                     },
                     <%=ddlAnioEdicionLibro.UniqueID%>: {
                         required: true
                     },
                     <%=txtEditorialLibro.UniqueID%>: {
                         required: true
                     },
                     <%=txtPrecioLibro.UniqueID%>: {
                         required: true,
                         number: true,
                         min: 0
                     },
                     <%=ddlDiarioSuplemento.UniqueID%>: {
                         required: true
                     },
                     <%=txtPrecioSuplemento.UniqueID%>: {
                         required: true,
                         number: true,
                         min: 0
                     },
                     <%=txtCantidadDeEntregaSuplemento.UniqueID%>: {
                         required: true,
                         number: true,
                         digits: true,
                         min: 0
                     },
                     <%=ddlAnioDeEstrenoPelicula.UniqueID%>: {
                         required: true
                     },
                     <%=txtPrecioPelicula.UniqueID%>: {
                         required: true,
                         number: true,
                         min: 0
                     }
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
                     <%=txtPrecioLunesDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioMartesDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioMiercolesDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioJuevesDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioViernesDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioSabadoDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtPrecioDomingoDiario.UniqueID%>: {
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=ddlPeriodicidadRevista.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtPrecioRevista.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=ddlPeriodicidadColeccion.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtCantidadDeEntregaColeccion.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         digits: "Ingrese solo dígitos.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtAutorLibro.UniqueID%>: {
                        required: "Este campo es requerido."
                     },
                     <%=ddlAnioEdicionLibro.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtEditorialLibro.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtPrecioLibro.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=ddlDiarioSuplemento.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtPrecioSuplemento.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=txtCantidadDeEntregaSuplemento.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         digits: "Ingrese solo dígitos.",
                         min: "Ingrese un valor mayor o igual a 0."
                     },
                     <%=ddlAnioDeEstrenoPelicula.UniqueID%>: {
                         required: "Este campo es requerido."
                     },
                     <%=txtPrecioPelicula.UniqueID%>: {
                         required: "Este campo es requerido.",
                         number: "Ingrese un número válido.",
                         min: "Ingrese un valor mayor o igual a 0."
                     }
                }
            });
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

            $(".select2_diadeentregarevista").select2(
            {
                    placeholder: 'Seleccione un Día de Entrega',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_periodicidadrevista").select2(
            {
                    placeholder: 'Seleccione una Periodicidad',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_diadeentregacoleccion").select2(
            {
                    placeholder: 'Seleccione un Día de Entrega',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_periodicidadcoleccion").select2(
            {
                    placeholder: 'Seleccione una Periodicidad',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_anioeditoriallibro").select2(
            {
                    placeholder: 'Seleccione un Año',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_diadeentregasuplemento").select2(
            {
                    placeholder: 'Seleccione un Día de Entrega',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_diariosuplemento").select2(
            {
                    placeholder: 'Seleccione un Diario',
                    width: '100%',
                    allowClear: true
            });

            $(".select2_aniodeestrenopelicula").select2(
            {
                    placeholder: 'Seleccione un Año',
                    width: '100%',
                    allowClear: true
            });
        }
    </script>
</asp:Content>
