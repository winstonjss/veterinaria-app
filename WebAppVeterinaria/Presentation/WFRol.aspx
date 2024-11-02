<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRol.aspx.cs" Inherits="Presentation.WFRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
           <br />

   <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
   <br />
   <%-- Nombre Rol--%>
   <asp:Label ID="Label1" runat="server" Text="Ingrese Nombre de Rol"></asp:Label>
   <asp:TextBox ID="TBRol_nombre" runat="server"></asp:TextBox>
   <br />

      <%-- Descripción Rol--%>
  <asp:Label ID="Label2" runat="server" Text="Ingrese la descripcion"></asp:Label>
  <asp:TextBox ID="TBRol_descripcion" runat="server"></asp:TextBox>
  <br />

   <%-- Botones Guardar y actualizar --%>
<div>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
</div>
   <br />
    <%--Lista de Roles --%>
<div>   
    <asp:GridView ID="GVRoles" runat="server"></asp:GridView>
</div>

</asp:Content>
