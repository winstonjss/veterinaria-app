using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPermission : System.Web.UI.Page
    {
        PermissionLog objPer = new PermissionLog();
        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                showPermission();
            }
        }
        public void showPermission()
        {
            DataSet ds = new DataSet();
            ds = objPer.showPermission();
            GVPermisos.DataSource = ds;
            GVPermisos.DataBind();

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBPer_nombre.Text;
            _descripcion = TBPer_descripcion.Text;

            executed = objPer.savePermission(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                showPermission();
                TBPer_nombre.Text = "";
                TBPer_descripcion.Text = "";
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }


    }
}