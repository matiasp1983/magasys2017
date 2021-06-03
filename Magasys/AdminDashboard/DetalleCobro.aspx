﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminDashboard/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleCobro.aspx.cs" Inherits="PL.AdminDashboard.DetalleCobro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentMaster" runat="server">
    <form id="FormDetalleCobro" runat="server" class="form-horizontal">
        <div class="row wrapper border-bottom white-bg page-heading">
           <div class="col-lg-10">
               <h2>Detalle Cobro</h2>
               <ol class="breadcrumb">
                   <li>
                   <a href="Index.aspx">Principal</a>
                   </li>
                   <li>Clientes
                   </li>
                   <li class="active">
                       <strong>Cobro</strong>
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
                                <label class="col-sm-2 control-label">Código de Cobro</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtCodigoCobro" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fecha de Cobro</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaCobro" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Estado</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtEstado" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h2>Cliente</h2>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Tipo de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTipoDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Número de Documento</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNumeroDocumento" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Nombre</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Apellido</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" Enabled="false" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox">
                        <div class="ibox-title">
                            <h2>Items de la Cobro</h2>
                        </div>
                        <div class="ibox-content">
                            <br />

                            <asp:ListView ID="lsvDetalleCobro" runat="server" OnItemDataBound="LsvDetalleCobro_ItemDataBound">
                                <LayoutTemplate>
                                    <table class="footable table table-stripped toggle-arrow-tiny" data-page-size="15">
                                        <thead>
                                            <tr>
                                                <th>Cód. venta</th>
                                                <th class="text-left">Fecha venta</th>
                                                <th data-hide="phone,tablet">Forma de pago</th>
                                                <th data-hide="phone,tablet">Subtotal</th>
                                                <th>Detalle</th>
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
                                            <asp:Label ID="lblCodigoVenta" runat="server" Text='<%#Eval("ID_VENTA").ToString()%>'></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFechaVenta" runat="server" Text='<%#Convert.ToDateTime(Eval("FECHA")).ToString("dd/MM/yyyy")%>'></asp:Label>
                                        </td>        
                                        <td class="text-left">
                                            <asp:Label ID="lblFormaPago" runat="server" Text='<%#Eval("FORMA_PAGO").ToString()%>'></asp:Label>
                                        </td>     
                                        <td class="text-left">
                                            <asp:Label ID="lblTotal" runat="server" Text='<%#Eval("TOTAL").ToString()%>'></asp:Label>
                                        </td>  
                                        <td class="text-left">
                                            <div class="btn-group">
                                                <button runat="server" id="btnVisualizar" class="btn btn-outline btn-success btn-xl" title="Visualizar" onserverclick="BtnVisualizar_Click"><i class="fa fa-search"></i></button>
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>

                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h2 class="col-sm-4 control-label font-bold">Total $</h2>
                                        <div class="col-sm-1 control-label font-bold">
                                            <h2>
                                                <asp:Label ID="lblTotal" runat="server" MaxLength="5" autocomplete="off"></asp:Label></h2>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="hr-line-dashed"></div>
                            <div class="form-group" style="margin-bottom: 50px">
                                <div class="col-xs-12 col-sm-6 col-md-8"></div>
                                <div class="col-xs-12 col-md-4" style="text-align: right">
                                    <a href="javascript:history.go(-1)" class="btn btn-primary">Volver</a>
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
    <script type="text/javascript">
        if (window.jQuery) {
            $(document).ready(function () {
                LoadFootable();
            });
        }

        function LoadFootable() {
            $('.footable').footable();
        }
    </script>
</asp:Content>