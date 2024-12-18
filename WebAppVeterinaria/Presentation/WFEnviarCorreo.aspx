<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFEnviarCorreo.aspx.cs" Inherits="Presentation.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label2" runat="server" Text="Recuperar contraseña"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Ingrese su correo electronico"></asp:Label>
        <br />
        <asp:TextBox ID="TBCorreo" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator
            ID="RFCorreo"
            runat="server"
            ControlToValidate="TBCorreo"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio"
            CssClass="form-text text-danger">
        </asp:RequiredFieldValidator>
        <div>
            <asp:Button
            ID="BtnUpdate"
            runat="server"
            Text="Actualizar"
            OnClick="BtnUpdate_Click"
            CssClass="btn btn-primary me-2" />
        </div>
    </form>

    
</body>
</html>
