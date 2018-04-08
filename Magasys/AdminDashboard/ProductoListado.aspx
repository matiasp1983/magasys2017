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
            <asp:Button ID="Button1" runat="server" Text="Diario 64 Editar" OnClick="Button1_Click" />
            <asp:Button ID="Button11" runat="server" Text="Diario 64 Visualizar" OnClick="Button11_Click" />
            <asp:Button ID="Button2" runat="server" Text="Revista 22 Editar" OnClick="Button2_Click" />
            <asp:Button ID="Button22" runat="server" Text="Revista 22 Visualizar" OnClick="Button22_Click" />
            <asp:Button ID="Button3" runat="server" Text="Colección 27 Editar" OnClick="Button3_Click" />
            <asp:Button ID="Button33" runat="server" Text="Colección 27 Visualizar" OnClick="Button33_Click" />
            <asp:Button ID="Button4" runat="server" Text="Libro 73 Editar" OnClick="Button4_Click" />
            <asp:Button ID="Button44" runat="server" Text="Libro 73 Visualizar" OnClick="Button44_Click" />
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
