<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOwner.aspx.cs" Inherits="Presentation.WFOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Aqui va todo lo del formulario de propietario--%>
    <br />
    <br />
    <asp:TextBox ID="TBPro_id" runat="server"></asp:TextBox>
    <br />
    <%--Nombre del propietario--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del propietario"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Telefono del propietario--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el telefono del propietario"></asp:Label>
    <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
    <br />
    <%--DDL del Usuarios--%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el usuario"></asp:Label>
    <asp:DropDownList ID="DDLUsers" runat="server"></asp:DropDownList>
    <br />


    <%--Botones de Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />

        <%--Este Label si se modifica porque lo vamos a utilizar como salida--%>
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de propietarios--%>
    <div>
        <asp:GridView ID="GVOwner" runat="server"></asp:GridView>
    </div>
</asp:Content>