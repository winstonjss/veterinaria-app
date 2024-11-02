<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnoses.aspx.cs" Inherits="Presentation.WFDiagnoses" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label2" runat="server" Text="Ingrese la clasificacion"></asp:Label>
    <asp:TextBox ID="TBClasification" runat="server"></asp:TextBox>
    <br />
      <asp:Label ID="Label1" runat="server" Text="Ingrese el codigo"></asp:Label>
  <asp:TextBox ID="TBCode" runat="server"></asp:TextBox>
  <br />
     <asp:Label ID="Label5" runat="server" Text="Seleccione Anamnesis"></asp:Label>
     <asp:DropDownList ID="DDLAnamnesis" runat="server"></asp:DropDownList>
     <br />
    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista De Diagnoses--%>
    <div>
        <asp:GridView ID="GVDiagnoses" runat="server"></asp:GridView>
    </div>
</asp:Content>
