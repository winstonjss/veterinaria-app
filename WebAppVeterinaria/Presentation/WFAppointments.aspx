<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAppointments.aspx.cs" Inherits="Presentation.WFAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Ingrese la Fecha de la cita"></asp:Label>
    <asp:Calendar ID="CALCita" runat="server"></asp:Calendar>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese la hora de inicio de la cita"></asp:Label>
    <asp:TextBox ID="TBHoraInicio" runat="server"></asp:TextBox>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Ingrese la hora final de la cita"></asp:Label>
    <asp:TextBox ID="TBHoraFin" runat="server"></asp:TextBox>
     <br />
     <asp:Label ID="Label5" runat="server" Text="Seleccione el animal"></asp:Label>
     <asp:DropDownList ID="DDLAnimals" runat="server"></asp:DropDownList>
     <br />
    <asp:Label ID="Label4" runat="server" Text="Seleccione el veterinario"></asp:Label>
    <asp:DropDownList ID="DDLVeterinario" runat="server"></asp:DropDownList>
    <br />

    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista De Citas--%>
    <div>
        <asp:GridView ID="GVCitas" runat="server"></asp:GridView>
    </div>
</asp:Content>
