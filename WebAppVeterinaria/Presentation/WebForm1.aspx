<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Presentation.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
