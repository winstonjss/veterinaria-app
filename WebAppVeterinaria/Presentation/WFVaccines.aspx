<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVaccines.aspx.cs" Inherits="Presentation.WFVaccines" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre de la vacuna"></asp:Label>
 <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
 <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese el tipo de vacuna"></asp:Label>
    <asp:TextBox ID="TBType" runat="server"></asp:TextBox>
    <br />
      <asp:Label ID="Label1" runat="server" Text="Ingrese la cantidad de la vacuna"></asp:Label>
  <asp:TextBox ID="TBQuantity" runat="server"></asp:TextBox>
  <br />
    
     <asp:Label ID="Label5" runat="server" Text="Seleccione Diagonosticos"></asp:Label>
     <asp:DropDownList ID="DDLDiagonoses" runat="server"></asp:DropDownList>
     <br />
    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista De Vaccines--%>
    <div>
        <asp:GridView ID="GVVaccines" runat="server"></asp:GridView>
    </div>
</asp:Content>

