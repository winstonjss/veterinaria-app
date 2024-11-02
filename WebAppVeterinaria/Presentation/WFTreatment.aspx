<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatment.aspx.cs" Inherits="Presentation.WFTreatment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre"></asp:Label>
 <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
 <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripcion"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
      <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha inicio"></asp:Label>
  <asp:TextBox ID="TBStartDate" runat="server"></asp:TextBox>
  <br />
    <asp:Label ID="Label3" runat="server" Text="Ingrese fecha fin"></asp:Label>
    <asp:TextBox ID="TBEndDate" runat="server"></asp:TextBox>
    <br />
     <asp:Label ID="Label5" runat="server" Text="Seleccione Diadiagnostico"></asp:Label>
     <asp:DropDownList ID="DDLDiagonoses" runat="server"></asp:DropDownList>
     <br />
    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista De Treatment--%>
    <div>
        <asp:GridView ID="GVTreatment" runat="server"></asp:GridView>
    </div>
</asp:Content>

