<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAnimals.aspx.cs" Inherits="Presentation.WFAnimals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Aqui va todo lo del formulario de animales--%>
    <br />
    <br />
    <asp:TextBox ID="TBAnim_id" runat="server"></asp:TextBox>
    <br />
    <%--Nombre del animal--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del animal"></asp:Label>
    <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
    <br />
    <%--Especie del animal--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la especie del animal"></asp:Label>
    <asp:TextBox ID="TBSpecies" runat="server"></asp:TextBox>
    <br />
    <%--Raza del animal--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la raza del animal"></asp:Label>
    <asp:TextBox ID="TBRace" runat="server"></asp:TextBox>
    <br />
    <%--Fecha de nacimiento del animal--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese la fecha de nacimiento del animal"></asp:Label>
    <asp:TextBox ID="TBDate_birth" runat="server"></asp:TextBox>
    <br />
    <%--Sexo del animal--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese el sexo del animal"></asp:Label>
    <asp:TextBox ID="TBSex" runat="server"></asp:TextBox>
    <br />
    <%--Peso del animal--%>
    <asp:Label ID="Label6" runat="server" Text="Ingrese el peso del animal"></asp:Label>
    <asp:TextBox ID="TBWeight" runat="server"></asp:TextBox>
    <br />
    <%--Color del animal--%>
    <asp:Label ID="Label7" runat="server" Text="Ingrese el color del animal"></asp:Label>
    <asp:TextBox ID="TBColor" runat="server"></asp:TextBox>
    <br />
    <%--DDL del Propietario--%>
    <asp:Label ID="Label8" runat="server" Text="Seleccione el propietario"></asp:Label>
    <asp:DropDownList ID="DDLOwner" runat="server"></asp:DropDownList>
    <br />


    <%--Botones de Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />

        <%--Este Label si se modifica porque lo vamos a utilizar como salida--%>
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de animales--%>
    <div>
        <asp:GridView ID="GVAnimals" runat="server"></asp:GridView>
    </div>
</asp:Content>