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
                                        <li class="active"><a data-toggle="tab" href="#tab-1">Producto A</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-2">Producto B</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-3">Producto C</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-4">Producto D</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-5">Producto E</a></li>
                                        <li class=""><a data-toggle="tab" href="#tab-6">Producto F</a></li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="tab-1" class="tab-pane active">
                                            <div class="panel-body">
                                                <strong>Lorem ipsum dolor sit amet, consectetuer adipiscing</strong>

                                                <p>
                                                    A wonderful serenity has taken possession of my entire soul, like these sweet mornings of spring which I enjoy with my whole heart. I am alone, and feel the charm of
                                        existence in this spot, which was created for the bliss of souls like mine.
                                                </p>

                                                <p>
                                                    I am so happy, my dear friend, so absorbed in the exquisite sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at
                                        the present moment; and yet I feel that I never was a greater artist than now. When.
                                                </p>
                                            </div>
                                        </div>
                                        <div id="tab-2" class="tab-pane">
                                            <div class="panel-body">
                                                <strong>Donec quam felis</strong>

                                                <p>
                                                    Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects
                                            and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath
                                                </p>

                                                <p>
                                                    I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite
                                            sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.
                                                </p>
                                            </div>
                                        </div>
                                        <div id="tab-3" class="tab-pane">
                                            <div class="panel-body">
                                                <strong>Donec quam felis</strong>

                                                <p>
                                                    Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects
                                            and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath
                                                </p>

                                                <p>
                                                    I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite
                                            sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.
                                                </p>
                                            </div>
                                        </div>
                                        <div id="tab-4" class="tab-pane">
                                            <div class="panel-body">
                                                <strong>Donec quam felis</strong>

                                                <p>
                                                    Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects
                                            and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath
                                                </p>

                                                <p>
                                                    I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite
                                            sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.
                                                </p>
                                            </div>
                                        </div>
                                        <div id="tab-5" class="tab-pane">
                                            <div class="panel-body">
                                                <strong>Donec quam felis</strong>

                                                <p>
                                                    Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects
                                            and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath
                                                </p>

                                                <p>
                                                    I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite
                                            sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.
                                                </p>
                                            </div>
                                        </div>
                                        <div id="tab-6" class="tab-pane">
                                            <div class="panel-body">
                                                <strong>Donec quam felis</strong>

                                                <p>
                                                    Thousand unknown plants are noticed by me: when I hear the buzz of the little world among the stalks, and grow familiar with the countless indescribable forms of the insects
                                            and flies, then I feel the presence of the Almighty, who formed us in his own image, and the breath
                                                </p>

                                                <p>
                                                    I am alone, and feel the charm of existence in this spot, which was created for the bliss of souls like mine. I am so happy, my dear friend, so absorbed in the exquisite
                                            sense of mere tranquil existence, that I neglect my talents. I should be incapable of drawing a single stroke at the present moment; and yet.
                                                </p>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
