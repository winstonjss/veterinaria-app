using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFRoles_Permisos : System.Web.UI.Page
    {
        Roles_PermissionLog objRolPer = new Roles_PermissionLog();
        RolesLog objRol = new RolesLog();
        PermissionLog objPer = new PermissionLog();
        private int _rol_id, _permiso_id, _old_rol_id, _old_permiso_id, _new_rol_id, _new_permiso_id;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 

                showRolesDDL();
                showPermissionDDl();
            }
        }

        //Metodo para mostrar todos los Roles y Permisos
        [WebMethod]
        public static object ListRolesPermisos()
        {
            Roles_PermissionLog objRolPer = new Roles_PermissionLog();

            // Se obtiene un DataSet que contiene la lista de Permisos desde la base de datos.
            var dataSet = objRolPer.showRolesPermisos();

            // Se crea una lista para almacenar los Roles que se van a devolver.
            var rolespermisosList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un consultorio).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolespermisosList.Add(new
                {
                    ID_rol = row["rol_id"],
                    Nombre_Rol = row["rol_nombre"],
                    ID_Permiso = row["per_id"],
                    Nombre_Permiso = row["per_nombre"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = rolespermisosList };
        }
        [WebMethod]



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

        [WebMethod]
        public static bool deleteRolesPermision(int _rol_id, int _permiso_id)
        {
            try
            {
                Roles_PermissionLog objRolPer = new Roles_PermissionLog();
                return objRolPer.deleteRolesPermision(_rol_id, _permiso_id);
            }
            catch (Exception)
            {
                // Aquí puedes loguear el error si es necesario
                return false;
            }
        }
        ////Método para eliminar un Permiso
        //public static bool deleteRolesPermision(int _rol_id, int _permiso_id)
        //{
        //    // Crear una instancia de la clase de lógica de rol
        //    Roles_PermissionLog objRolPer = new Roles_PermissionLog();

        //    // Invocar al método para eliminar el Rol y devolver el resultado
        //    return objRolPer.deleteRolesPermision(_rol_id, _permiso_id);
        //}
        private void clear()
        {
            DDLRol.SelectedIndex = 0;
            DDLPermiso.SelectedIndex = 0;

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _permiso_id = Convert.ToInt32(DDLPermiso.SelectedValue);

            executed = objRolPer.saveRolesPermisos(_rol_id, _permiso_id);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        [WebMethod]
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un Rol  para actualizar
            if (string.IsNullOrEmpty(DDLRol.SelectedValue))
            {
                LblMsg.Text = "No se ha seleccionado un Rol para actualizar.";
                return;
            }
            // Verifica si se ha seleccionado un Permiso  para actualizar
            if (string.IsNullOrEmpty(DDLPermiso.SelectedValue))
            {
                LblMsg.Text = "No se ha seleccionado un Permiso para actualizar.";
                return;
            }

            int _old_rol_id = Convert.ToInt32(oldRolId.Value); // Valor antiguo de rol
            int _old_permiso_id = Convert.ToInt32(oldPermisoId.Value); // Valor antiguo de permiso
            int _new_rol_id = Convert.ToInt32(DDLRol.SelectedValue); // Nuevo valor de rol
            int _new_permiso_id = Convert.ToInt32(DDLPermiso.SelectedValue); // Nuevo valor de permiso

            executed = objRolPer.updateRolesPermisos(_old_rol_id, _old_permiso_id, _new_rol_id, _new_permiso_id);

            if (executed)
            {
                LblMsg.Text = "Se actualizó correctamente";
                clear();
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

    }
}