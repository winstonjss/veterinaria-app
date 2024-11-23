using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Web;
using System.Web.Services;
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
                FrmRol.Visible = false;
                PanelAdmin.Visible = false;
            }
            validatePermissionRol();
        }

        //Metodo para mostrar todos los Roles
        [WebMethod]
        public static object ListRoles()
        {
            RolesLog objRol = new RolesLog();

            // Se obtiene un DataSet que contiene la lista de Roles desde la base de datos.
            var dataSet = objRol.showRoles();

            // Se crea una lista para almacenar los Roles que se van a devolver.
            var rolesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un consultorio).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolesList.Add(new
                {
                    ID = row["rol_id"],
                    Nombre = row["rol_nombre"],
                    Descripcion = row["rol_descripcion"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = rolesList };
        }
        [WebMethod]

        public static bool deleteRol(int id)
        {
            // Crear una instancia de la clase de lógica de rol
            RolesLog objrol = new RolesLog();

            // Invocar al método para eliminar el Rol y devolver el resultado
            return objrol.deleteRol(id);
        }

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
                            FrmRol.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = true;
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
                            FrmRol.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = true;
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
                masterPage.linkAnamnesis.Visible = false;
                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmRol.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = true;
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
                            FrmRol.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmRol.Visible = false;
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
        private void clear()
        {
            HFRol.Value = "";
            DDLNombreRol.SelectedIndex = 0;
            TBRol_descripcion.Text = "";

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Verificar que todos validadores de la pagina esten ok
            if (Page.IsValid)
            {
                _nombre = DDLNombreRol.SelectedValue.ToUpper();
                _descripcion = TBRol_descripcion.Text;

                executed = objRol.saveRoles(_nombre, _descripcion);

                if (executed)
                {
                    LblMsg.Text = "El Rol se guardó exitosamente ";
                    clear();
                }
                else
                {
                    LblMsg.Text = "Error al guardar ";
                }
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un Rol  para actualizar
            if (string.IsNullOrEmpty(HFRol.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Rol para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFRol.Value);
            _nombre = DDLNombreRol.SelectedValue.ToUpper();
            _descripcion = TBRol_descripcion.Text;
            executed = objRol.updateRol(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se ACTUALIZÓ exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al ACTUALIZAR ";
            }
        }


    }
}