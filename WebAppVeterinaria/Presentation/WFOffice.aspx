<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOffice.aspx.cs" Inherits="Presentation.WFOffice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
               <br />

   <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
      <br />
   <%-- Número de consultorio--%>
   <asp:Label ID="Label1" runat="server" Text="Ingrese Número de consultorio "></asp:Label>
   <asp:TextBox ID="TBCon_num_consultorio" runat="server"></asp:TextBox>
   <br />

   <%-- Botones Guardar y actualizar --%>
<div>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
</div>
   <br />
    <%--Lista de Tipos de documento --%>
<div>   
    <asp:GridView ID="GVOffice" runat="server"></asp:GridView>
</div>

</asp:Content>
