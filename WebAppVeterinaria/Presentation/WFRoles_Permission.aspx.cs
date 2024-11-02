using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFRoles_Permission : System.Web.UI.Page
    {
        Roles_PermissionLog objRolPer = new Roles_PermissionLog();
        RolesLog objRol = new RolesLog();
        PermissionLog objPer = new PermissionLog();
        private int _rol_id, _permiso_id;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                showRolesPermisos();
                showRolesDDL();
                showPermissionDDl();
            }
        }
        private void showRolesPermisos()
        {
            DataSet ds = new DataSet();

            ds = objRolPer.showRolesPermisos();
            GVRoles_Permisos.DataSource = ds;
            GVRoles_Permisos.DataBind();
        }

        //Metodo para mostrar los roles DDL
        private void showRolesDDL()
        {
            DDLRol.DataSource = objRol.showRolesDDL();
            DDLRol.DataValueField = "rol_id";
            DDLRol.DataTextField = "rol_nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione");
        }

        private void showPermissionDDl()
        {
            DDLPermiso.DataSource = objPer.showPermissionDDl();
            DDLPermiso.DataValueField = "per_id";
            DDLPermiso.DataTextField = "per_nombre";
            DDLPermiso.DataBind();
            DDLPermiso.Items.Insert(0, "Seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _permiso_id = Convert.ToInt32(DDLPermiso.SelectedValue);

            executed = objRolPer.saveRolesPermisos(_rol_id, _permiso_id);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                showRolesPermisos();
                DDLRol.SelectedIndex = 0;
                DDLPermiso.SelectedIndex = 0;
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