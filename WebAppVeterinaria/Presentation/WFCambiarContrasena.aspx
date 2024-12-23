<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WFCambiarContrasena.aspx.cs" Inherits="Presentation.WebForm2" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Cambiar password</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <style>
        /* Fondo de la página */
        body {
            background: url('resources/images/inicio/FONDO2.png') no-repeat center center fixed;
            background-size: cover;
        }

        /* Estilos para el gif de carga */
        #loadingGif {
            display: none;
            text-align: center;
            margin-bottom: 20px;
        }

        #loadingGif img {
            width: 50px;
            height: 50x;
        }

        /* Ajustes del formulario */
        .card {
            background-color: rgba(255, 255, 255, 0.9); /* Fondo blanco con transparencia */
            border-radius: 20px;
        }

        /* Estilo del logo */
        .logo {
            display: block;
            margin: 0 auto 1px auto; /* Centrado horizontal y margen inferior */
            width: 550px; /* Tamaño mayor para el logo */
            height: auto;
        }

        .logo-container {
            text-align: center;
            margin-bottom: 2px; /* Espacio entre el logo y la tarjeta */
        }
    </style>
</head>
<body>
    
    <div class="container d-flex flex-column justify-content-center align-items-center min-vh-100">
        <!-- Tarjeta de inicio de sesión -->
        <div class="card shadow-lg p-4" style="max-width: 400px; width: 100%;">
            <h5 class="card-title text-center mb-4">Cambiar contraseña</h5>

            <form id="FrmCambiarContrasena" runat="server" method="post">
                <div class="mb-3">
                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Correo electrónico"></asp:Label>
                    <asp:TextBox ID="TBCorreo" CssClass="form-control" TextMode="Email" runat="server" required="true"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Nueva Contraseña"></asp:Label>
                    <asp:TextBox ID="TBContrasenaUno" CssClass="form-control" TextMode="Password" runat="server" required="true"></asp:TextBox>
                </div>

                <div class="mb-3">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Confirmar Contraseña"></asp:Label>
                        <asp:TextBox ID="TBContrasenaDos" CssClass="form-control" TextMode="Password" runat="server" required="true"></asp:TextBox>
                 </div>

                <div class="d-grid gap-2">
                    <asp:Button ID="BtnCambiarContrasena" CssClass="btn btn-primary" runat="server" Text="Cambiar contraseña" OnClick="BtnCambiarContrasena_Click" />
                </div>

                <asp:Label ID="LblMsg" CssClass="form-label text-danger mt-3" runat="server" Text=""></asp:Label>
            </form>
        </div>
    </div>
</body>
</html>
