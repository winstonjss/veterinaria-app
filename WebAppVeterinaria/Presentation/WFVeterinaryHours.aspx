<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVeterinaryHours.aspx.cs" Inherits="Presentation.WFVeterinaryHours" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Aqui va todo lo del formulario de horario de veterinario--%>
    <br />
    <br />
    <asp:TextBox ID="TBHor_vet_id" runat="server"></asp:TextBox>
    <br />
    <%--Fecha de incio del horario de veterinario--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de incio"></asp:Label>
    <asp:TextBox ID="TBStart_date" runat="server"></asp:TextBox>
    <br />
    <%--Fecha final del horario de veterinario--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha final"></asp:Label>
    <asp:TextBox ID="TBEnd_date" runat="server"></asp:TextBox>
    <br />
    <%--Hora de incio del horario de veterinario--%>
    <asp:Label ID="Label3" runat="server" Text="Ingrese la hora de incio"></asp:Label>
    <asp:TextBox ID="TBStart_time" runat="server"></asp:TextBox>
    <br />
    <%--Hora final del horario de veterinario--%>
    <asp:Label ID="Label4" runat="server" Text="Ingrese la hora final"></asp:Label>
    <asp:TextBox ID="TBFinal_time" runat="server"></asp:TextBox>
    <br />

    <%--DDL del veterinario--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el veterinario"></asp:Label>
    <asp:DropDownList ID="DDLVeterinarian" runat="server"></asp:DropDownList>
    <br />


    <%--Botones de Guardar y Actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />

        <%--Este Label si se modifica porque lo vamos a utilizar como salida--%>
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%--Lista de horarios de veterinario--%>
    <div>
        <asp:GridView ID="GVVeterinaryHours" runat="server"></asp:GridView>
    </div>
</asp:Content>
