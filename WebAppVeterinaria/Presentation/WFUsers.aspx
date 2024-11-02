<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsers.aspx.cs" Inherits="Presentation.WFUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
     <asp:TextBox ID="TBId" runat="server"></asp:TextBox><br />
 <%-- Documento--%>
 <asp:Label ID="Label1" runat="server" Text="Ingrese el Documento"></asp:Label>
 <asp:TextBox ID="TBUsu_documento" runat="server"></asp:TextBox>
 <br />
 <%-- Correo--%>
 <asp:Label ID="Label2" runat="server" Text="Ingrese el correo"></asp:Label>
 <asp:TextBox ID="TBUsu_correo" runat="server"></asp:TextBox>
 <br />
 <%-- Contraseña --%>
 <asp:Label ID="Label3" runat="server" Text="Ingrese la Contraseña"></asp:Label>
 <asp:TextBox ID="TBUsu_contrasena" runat="server"></asp:TextBox>
 <br />

  <%-- Salt --%>
 <asp:Label ID="Label4" runat="server" Text="Ingrese el salt"></asp:Label>
 <asp:TextBox ID="TBUsu_salt" runat="server"></asp:TextBox>
 <br />

    
  <%-- Estado --%>
 <asp:Label ID="Label7" runat="server" Text="Ingrese Estado"></asp:Label>
 <asp:TextBox ID="TBUsu_estado" runat="server"></asp:TextBox>
 <br />

    
  <%-- Fecha de creación --%>
 <asp:Label ID="Label8" runat="server" Text="Fecha de creación "></asp:Label>
 <asp:TextBox ID="TBUsu_fecha_creación" runat="server"></asp:TextBox>
 <br />

  <%-- Rol --%>
 <asp:Label ID="Label5" runat="server" Text="Seleccione el Rol"></asp:Label>
 <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
 <br />

  <%-- Tipo documento --%>
 <asp:Label ID="Label6" runat="server" Text="Seleccione tipo documento"></asp:Label>
 <asp:DropDownList ID="DDLTipo_documento" runat="server"></asp:DropDownList>
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
     <asp:GridView ID="GVUsers" runat="server"></asp:GridView>
 </div>

</asp:Content>
