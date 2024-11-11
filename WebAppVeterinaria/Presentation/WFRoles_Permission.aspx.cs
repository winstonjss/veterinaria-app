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
        private int _rol_id, _permiso_id, _id_rol_permiso;
        private bool executed = false;
        private DateTime _fecha_asignacion;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                //aqui se invocan todos los métodos 
                TBper_rol_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
                    ID = row["rol_permiso"],
                    Rol_ID = row["tbl_rol_rol_id"],
                    NombreRol = row["rol_nombre"],
                    Per_ID = row["per_id"],
                    NombrePermiso = row["per_nombre"],
                    Date = Convert.ToDateTime(row["per_rol_fecha_asignacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.

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

        //Método para eliminar un Permiso
        public static bool deleteRolesPermision(int id)
        {
            // Crear una instancia de la clase de lógica de rol
            Roles_PermissionLog objRolPer = new Roles_PermissionLog();

            // Invocar al método para eliminar el Rol y devolver el resultado
            return objRolPer.deleteRolesPermision(id);
        }
        private void clear()
        {
            DDLRol.SelectedIndex = 0;
            DDLPermiso.SelectedIndex = 0;
            TBper_rol_fecha.Text = "";

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _permiso_id = Convert.ToInt32(DDLPermiso.SelectedValue);
            _fecha_asignacion = DateTime.Parse(TBper_rol_fecha.Text);

            executed = objRolPer.saveRolesPermisos(_rol_id, _permiso_id, _fecha_asignacion);

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
            if (string.IsNullOrEmpty(HFRol_Permiso.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Rol Permiso para actualizar.";
                return;
            }
            _id_rol_permiso = Convert.ToInt32(HFRol_Permiso.Value);
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _permiso_id = Convert.ToInt32(DDLPermiso.SelectedValue);
            _fecha_asignacion = DateTime.Parse(TBper_rol_fecha.Text);

            executed = objRolPer.updateRolesPermisos(_id_rol_permiso, _rol_id, _permiso_id, _fecha_asignacion);


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