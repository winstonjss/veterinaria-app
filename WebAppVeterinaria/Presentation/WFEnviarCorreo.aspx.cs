using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using Model;
using static Model.GenerateSecureToken;

namespace Presentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string _correo;

        TokenLog tokenLog = new TokenLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                // Los botones y otros elementos se inicializan en false, no visibles.                
                BtnUpdate.Visible = true;
                TBCorreo.Visible = true;

            }

        }

        public void metodoEnviarCorreo(string userEmail)
        {
            try
            {
                int validateEmail = tokenLog.validateEmail(userEmail);
                if (validateEmail == 1) {
                    GenerateSecureToken tokenGenerator = new GenerateSecureToken();

                    PasswordResetToken resetToken = tokenGenerator.GeneratePasswordResetToken();

                    tokenGenerator.SendPasswordResetEmail(userEmail, resetToken.Token);

                    DateTime tokenDateGeneration = DateTime.Now;

                    DateTime tokenDateExpiration = tokenDateGeneration.AddHours(1);

                    tokenLog.saveToken(resetToken.Token, _correo, tokenDateGeneration, tokenDateExpiration);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Enviado', 'Se ha enviado el correo para la recuperación de contraseña', 'success')", true);
                }
                else {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                        "swal('Enviado', 'Se ha enviado el correo para la recuperación de contraseña', 'success')", true);
                }
            }
            catch (Exception e)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error', 'Error al enviar el correo electronico', 'error')", true);                
            }

        }

        protected void BtnEnviarCorreo(object sender, EventArgs e)
        {
            _correo = TBCorreo.Text;
            metodoEnviarCorreo(_correo);
        }
    }
}