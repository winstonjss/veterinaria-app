using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFVeterinarian : System.Web.UI.Page
    {
        //Crear los objetos 
        VeterinarianLog objVet = new VeterinarianLog();
        UsersLog objUse = new UsersLog();
        OfficeLog objOfi = new OfficeLog();

        //Definir atributos
        private int _vet_id, _fkUsers, _fkOffice;
        private string _name, _phone;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

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
                FrmVeterinarian.Visible = false;
                PanelAdmin.Visible = false;
                //Aqui se invocan todos los metodos
                //showVeterinarian();
                showUsersDDL();
                showOfficeDDL();
            }
            // Se invoca el metodo validar permisos roles.
            validatePermissionRol();
        }


        //  ***  Metodo para mostrar todos los veterinario
        /*
        * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
        * parte de un servicio web, lo que significa que puede ser invocado de manera
        * remota a través de HTTP.
        */
        [WebMethod]
        public static object ListVeterinarian()
        {
            VeterinarianLog objVet = new VeterinarianLog();

            // Se obtiene un DataSet que contiene la lista de veterinarios desde la base de datos.
            var dataSet = objVet.showVeterinarian();

            // Se crea una lista para almacenar los veterinarios que se van a devolver.
            var veterinarianList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un veterinario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                veterinarianList.Add(new
                {
                    VeterinarianID = row["vet_id"],
                    Name = row["vet_nombre"],
                    Phone = row["vet_telefono"],
                    FkUser = row["tbl_usuarios_usu_id"],
                    NameUser = row["usu_documento"],
                    FkOffice = row["tbl_consultorio_con_id"],
                    NameOffice = row["con_num_consultorio"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de veterinarios.
            return new { data = veterinarianList };
        }
        [WebMethod]


        //  ***  Metodo para eliminar veterinarios
        public static bool DeleteVeterinarian(int id)
        {
            // Crear una instancia de la clase de lógica de veterinarios
            VeterinarianLog objVet = new VeterinarianLog();

            // Invocar al método para eliminar el veterinario y devolver el resultado
            return objVet.deleteVeterinarian(id);
        }


        //  ***  Metodo para mostrar los usuarios en el DDL
        private void showUsersDDL()
        {
            DDLUsers.DataSource = objUse.showUsersDDL();
            DDLUsers.DataValueField = "usu_id"; //Nombre de la llave primaria
            DDLUsers.DataTextField = "usu_documento"; //Documento del usuario
            DDLUsers.DataBind();
            DDLUsers.Items.Insert(0, new ListItem("Seleccione", "0"));
        }


        //Metodo para mostrar los consultorios en el DDL
        private void showOfficeDDL()
        {
            DDLOffice.DataSource = objOfi.showOffice();
            DDLOffice.DataValueField = "con_id"; //Nombre de la llave primaria
            DDLOffice.DataTextField = "con_num_consultorio"; //NUmero del consultorio
            DDLOffice.DataBind();
            DDLOffice.Items.Insert(0, new ListItem("Seleccione", "0"));
        }


        //  ***  Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVeterinarianID.Value = "";
            TBName.Text = "";
            TBPhone.Text = "";
            DDLUsers.SelectedIndex = 0;
            DDLOffice.SelectedIndex = 0;
        }


        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            _fkOffice = Convert.ToInt32(DDLOffice.SelectedValue);

            executed = objVet.saveVeterinarian(_name, _phone, _fkUsers, _fkOffice);

            if (executed)
            {
                //LblMsg.Text = "El veterinario se guardo exitosamente!";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Exitoso', 'El veterinario se guardo exitosamente', 'success')", true);

                clear();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error', 'Error al guardar', 'error')", true);
            }
        }


        //Eventos que se ejecutan cuando se da clic en los botones (Metodo Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un veterinario para actualizar
            if (string.IsNullOrEmpty(HFVeterinarianID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un veterinario para actualizar.";
                return;
            }
            _vet_id = Convert.ToInt32(HFVeterinarianID.Value);
            _name = TBName.Text;
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            _fkOffice = Convert.ToInt32(DDLOffice.SelectedValue);

            executed = objVet.updateVeterinarian(_vet_id, _name, _phone, _fkUsers, _fkOffice);

            if (executed)
            {
                //LblMsg.Text = "El veterinario se actualizo exitosamente!";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                     "swal('Exitoso', 'El veterinario se actualizo exitosamente!', 'success')", true);
                clear();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error', 'Error al actualizar', 'error')", true);
            }
        }


        //  ***  Metodo validar permisos roles
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
                            FrmVeterinarian.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmVeterinarian.Visible = true;
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
                            FrmVeterinarian.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmVeterinarian.Visible = true;
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
                            FrmVeterinarian.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmVeterinarian.Visible = true;
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
                            FrmVeterinarian.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmVeterinarian.Visible = false;
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
    }
}