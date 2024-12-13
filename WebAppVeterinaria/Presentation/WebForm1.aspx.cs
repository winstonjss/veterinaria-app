using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace Presentation
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            GenerateSecureToken generateSecureToken = new GenerateSecureToken();
            Program program = new Program();
            program.metodoEnviarCorreo();
        }
    }
}