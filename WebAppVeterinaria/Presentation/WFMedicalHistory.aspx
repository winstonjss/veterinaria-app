<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMedicalHistory.aspx.cs" Inherits="Presentation.WFMedicalHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--Categorias--%>
    <asp:Label ID="Label1" runat="server" Text="Selecciones la cita"></asp:Label>
    <asp:DropDownList ID="DDLAppoitment" runat="server"></asp:DropDownList>
    <br />
    <%--Botones--%>
    <div>        
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />

    <%-- Lista De productos--%>
    <div>
        <asp:GridView ID="GVHistoryMedical" runat="server"></asp:GridView>
    </div>
</asp:Content>
