<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title>Inicio de Sesión</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <!-- Inicio de Sesión... -->
        <div class="d-flex justify-content-center align-items-center vh-100">
            <div class="card" style="width: 24rem;">
                <div class="card-body">

                    <!-- Imagen de cargando... -->
                    <div id="loadingGif" style="display: none;">
                        <img src="resources/images/loading-7528_128.gif" /" alt="Cargando..." />
                    </div>

                    <h5 class="card-title text-center mb-4">Inicio de sesión</h5>

                    <div class="mb-3">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="">Correo</asp:Label>
                        <asp:TextBox ID="TBCorreo" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox><br />
                    </div>

                    <div class="mb-3">
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="">Contraseña</asp:Label>
                        <asp:TextBox ID="TBContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox><br />
                    </div>

                    <div class="mb-3 form-check">
                        <input type="checkbox" class="form-check-input" id="exampleCheck1">
                        <label class="form-check-label" for="exampleCheck1">Olvido la contraseña</label>
                    </div>

                    <asp:Button ID="BtnIniciar" CssClass="btn btn-success" runat="server" Text="Iniciar" OnClick="BtnIniciar_Click" OnClientClick="showLoading();" /><br />
                    <asp:Label ID="LblMsg" CssClass="form-label" runat="server" Text=""></asp:Label>

                </div>
            </div>
        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>

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