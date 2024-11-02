<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles_Permission.aspx.cs" Inherits="Presentation.WFRoles_Permission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
     <%-- Seleccionar Rol --%>
<asp:Label ID="Label6" runat="server" Text="Seleccione el Rol "></asp:Label>
<asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
<br />

     <%-- Seleccionar Permiso --%>
<asp:Label ID="Label1" runat="server" Text="Seleccione el permiso"></asp:Label>
<asp:DropDownList ID="DDLPermiso" runat="server"></asp:DropDownList>
<br />
     <%-- Botones Guardar y actualizar --%>
  <div>
      <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
      <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
      <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
  </div>
  <br />
     <%--Lista de Usuarios --%>
 <div>   
     <asp:GridView ID="GVRoles_Permisos" runat="server"></asp:GridView>
 </div>
    <br />
</asp:Content>
