using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
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

        /*
        *  Variables de tipo pública que indiquen si el usuario tiene
        *  permiso para ver los botones editar y eliminar.
        */
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmRoles_Permission.Visible = false;
                PanelAdmin.Visible = false;
                //aqui se invocan todos los métodos 
                //aqui se invocan todos los métodos 
                TBper_rol_fecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showRolesDDL();
                showPermissionDDl();
            }
            validatePermissionRol();
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

            // Añade manualmente la opción inicial al DropDownList
            DDLRol.Items.Insert(0, new ListItem("Seleccione", "0")); // Text: "Seleccione", Value: "0"
                                                                    
        }

        private void showPermissionDDl()
        {

            DDLPermiso.DataSource = objPer.showPermissionDDl();
            DDLPermiso.DataValueField = "per_id";
            DDLPermiso.DataTextField = "per_nombre";
            DDLPermiso.DataBind();
            DDLPermiso.Items.Insert(0, "Seleccione");

            DDLPermiso.Items.Insert(0, new ListItem("Seleccione", "0")); // Text: "Seleccione", Value: "0"
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

        // Metodo para validar permisos roles
        // Metodo para validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("Default.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;
            if (objUser.Permisos == null || !objUser.Permisos.Any())
            {
                LblMsg.Text = "El usuario no tiene permisos asignados.";
                return;
            }
            if (userRole == "Administrador")
            {
                LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRoles_Permission.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRoles_Permission.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Veterinario")
            {
                LblMsg.Text = "Bienvenido, Veterinario!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkRol.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkDocumentType.Visible = false;
                masterPage.linkSecurity.Visible = false;

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRoles_Permission.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRoles_Permission.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Secretaria")
            {
                LblMsg.Text = "Bienvenido, Secretaria!";
                masterPage.linkRol.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol
                masterPage.linkDocumentType.Visible = false;
                masterPage.linkSecurity.Visible = false;
                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRoles_Permission.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRoles_Permission.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }

            else if (userRole == "Propietario")
            {
                LblMsg.Text = "Bienvenido, Propietario!";

                masterPage.linkRol.Visible = false;
                masterPage.linkPermission.Visible = false;
                masterPage.linkRolesPermission.Visible = false;
                masterPage.linkDocumentType.Visible = false;
                masterPage.linkUsers.Visible = false;
                masterPage.linkAnamnesis.Visible = false;
                masterPage.linkDiagnoses.Visible = false;
                masterPage.linkTreatment.Visible = false;
                masterPage.linkVaccines.Visible = false;
                masterPage.linkSecurity.Visible = false;


                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRoles_Permission.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmRoles_Permission.Visible = false;
                            BtnUpdate.Visible = false;
                            //PanelAdmin.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = false;
                            _showDeleteButton = false;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

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