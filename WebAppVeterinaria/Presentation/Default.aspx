<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
            <!-- Imagen de cargando... -->
            <div id="loadingGif" style="display: none;">
                <img src="resources/images/loading-7528_128.gif" /" alt="Cargando..." />
            </div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="">Iniciar Sesion</asp:Label><br />
                <asp:Label ID="Label1" runat="server" Text="">Correo</asp:Label>
                <asp:TextBox ID="TBCorreo" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label3" runat="server" Text="">Contraseña</asp:Label>
                <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox><br />
                <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar" OnClick="BtnIniciar_Click" OnClientClick="showLoading();" /><br />
                <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
            </div>
      
    </form>

    <%--Script para mostrar y ocultar la animacion de cargando...--%>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loadingGif").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loadingGif").style.display = "none";
        }
    </script>
</body>
</html>
