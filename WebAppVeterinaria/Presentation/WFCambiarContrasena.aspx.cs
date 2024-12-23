using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using SimpleCrypto;

namespace Presentation
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri currentUrl = Request.Url;

            string _urlToken = HttpUtility.UrlDecode(Request.QueryString["token"]);                    
            if (!IsPostBack)
            {
                FrmCambiarContrasena.Visible = false;
                BtnCambiarContrasena.Visible = false;

            }
            validateToken(_urlToken);
        }

        protected void BtnCambiarContrasena_Click(object sender, EventArgs e)
        {            
            TokenLog tokenLog = new TokenLog();
            ICryptoService cryptoService = new PBKDF2();
            string email = TBCorreo.Text;
            string _password = TBContrasenaUno.Text;
            string _password2 = TBContrasenaDos.Text;
            string salt = cryptoService.GenerateSalt();
            

            if (_password == _password2)
            {
                string _encryptedPassword = cryptoService.Compute(_password);
                executed = tokenLog.updatePassword(email, salt, _encryptedPassword);
                if (executed)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Acualizado', 'Su contraseña se actualizo correctamente', 'success')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Error', 'No se pudo actualizar la contraseña intente de nuevo', 'error')", true);
                }
            }
            else {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Error', 'Las contraseñas son diferentes', 'error')", true);
            }
        }

        private void validateToken(string _urlToken)
        {
            if (!string.IsNullOrEmpty(_urlToken))
            {
                TokenLog tokenLog = new TokenLog();

                var dataSet = tokenLog.showTokenByHash(_urlToken);

                // Se obtiene la fecha actual del sistema
                DateTime currentDate = DateTime.Now;

                // Variable para almacenar el correo si hay un token válido
                string validEmail = null;

                // Se itera sobre cada fila del DataSet
                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    DateTime tokenDateExpiration = DateTime.Parse(row["token_fecha_vencimiento"].ToString());

                    // Verificar si el token aún no ha expirado
                    if (currentDate <= tokenDateExpiration)
                    {
                        validEmail = row["token_correo"].ToString();
                        break; // Salir del bucle después de encontrar un token válido
                    }
                }

                // Verificar si se encontró un correo válido
                if (!string.IsNullOrEmpty(validEmail))
                {
                    // Llenar la caja de texto con el correo y bloquearla
                    TBCorreo.Text = validEmail;
                    TBCorreo.Enabled = false;
                    FrmCambiarContrasena.Visible = true;
                    BtnCambiarContrasena.Visible = true;
                }
                else
                {
                    // Si no hay tokens válidos, ocultar o deshabilitar elementos
                    Console.WriteLine("No hay tokens válidos.");
                    FrmCambiarContrasena.Visible = false;
                    BtnCambiarContrasena.Visible = false;
                }
            }
            else
            {
                FrmCambiarContrasena.Visible = false;
                BtnCambiarContrasena.Visible = false;
            }
        }

        
    }
}