using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFRol : System.Web.UI.Page
    {
        RolesLog objRol = new RolesLog();
        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showRoles();
            }
        }
        public void showRoles()
        {
            DataSet ds = new DataSet();
            ds = objRol.showRoles();
            GVRoles.DataSource = ds;
            GVRoles.DataBind();

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBRol_nombre.Text;
            _descripcion = TBRol_descripcion.Text;
            executed = objRol.saveRoles(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                showRoles();
                TBRol_nombre.Text = "";
                TBRol_descripcion.Text = "";
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