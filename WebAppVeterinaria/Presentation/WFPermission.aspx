<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermission.aspx.cs" Inherits="Presentation.WFPermission" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <br />

    <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
       <br />
   <%-- Nombre Permiso--%>
   <asp:Label ID="Label1" runat="server" Text="Ingrese Nombre de Permiso"></asp:Label>
   <asp:TextBox ID="TBPer_nombre" runat="server"></asp:TextBox>
   <br />

      <%-- Descripción Permiso--%>
  <asp:Label ID="Label2" runat="server" Text="Ingrese la descripcion"></asp:Label>
  <asp:TextBox ID="TBPer_descripcion" runat="server"></asp:TextBox>
  <br />

   <%-- Botones Guardar y actualizar --%>
<div>
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
</div>
   <br />
    <%--Lista de Permisos --%>
<div>   
    <asp:GridView ID="GVPermisos" runat="server"></asp:GridView>
</div>
</asp:Content>
