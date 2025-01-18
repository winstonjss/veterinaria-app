using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAppointments : System.Web.UI.Page
    {
        AppointmentsLog objApp = new AppointmentsLog();
        AnimalsLog objAni = new AnimalsLog();
        VeterinarianLog objVet = new VeterinarianLog();

        private TimeSpan _appoStartHour, _appoFinalHour;
        private DateTime _appoDate;
        private int _id, _fkVeterinarian, _fkAnimal;
        private bool executed = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        public bool _showMostrarHorariosButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TBHoraInicio.Text = DateTime.Now.ToString("HH:mm");
                TBHoraFin.Text = DateTime.Now.ToString("HH:mm");
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                BtnMostrarHorarios.Visible = false;
                FrmAppointments.Visible = false;
                PanelAdmin.Visible = false;
                showAnimalsDDL();
                showVeterinariansDDL();
            }
           validatePermissionRol();
        }

        [WebMethod]
        public static object ListAppoitments()
        {
            AppointmentsLog objApp = new AppointmentsLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objApp.showCitasAll();

            // Se crea una lista los datos que se van a devolver.
            var appoimentsList = new List<object>();

            // Se itera sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                appoimentsList.Add(new
                {
                    AppoitmentId = row["cit_id"],
                    AppoitmentDate = Convert.ToDateTime(row["cit_fecha"]).ToString("yyyy-MM-dd"),
                    AppoitmentHourStart = ((TimeSpan)row["cit_hora_inicio"]).ToString(@"hh\:mm"),
                    AppoitmentHourEnd = ((TimeSpan)row["cit_hora_fin"]).ToString(@"hh\:mm"),
                    FkAnimal = row["tbl_animales_anim_id"],
                    AnimalName = row["anim_nombre"],
                    FkVeterinary = row["tbl_veterinario_vet_id"],
                    VeterinaryName = row["vet_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = appoimentsList };
        }


        private void showAnimalsDDL()
        {
            DDLAnimals.DataSource = objAni.showAnimalsDDL();
            DDLAnimals.DataValueField = "anim_id";
            DDLAnimals.DataTextField = "anim_nombre";
            DDLAnimals.DataBind();
            DDLAnimals.Items.Insert(0, new ListItem("seleccione", "0"));
        }

        private void showVeterinariansDDL()
        {
            DDLVeterinario.DataSource = objVet.showVeterinarianDDL();
            DDLVeterinario.DataValueField = "vet_id";
            DDLVeterinario.DataTextField = "vet_nombre";
            DDLVeterinario.DataBind();
            DDLVeterinario.Items.Insert(0, new ListItem("seleccione", "0"));
        }

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
                lblMsg.Text = "El usuario no tiene permisos asignados.";
                return;
            }
            if (userRole == "Administrador")
            {
                lblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAppointments.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAppointments.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //lblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            _showMostrarHorariosButton = true;
                            BtnMostrarHorarios.Visible = true;
                            break;
                        case "ELIMINAR":
                            //lblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            lblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Veterinario")
            {
                lblMsg.Text = "Bienvenido, Veterinario!";

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
                            FrmAppointments.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAppointments.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //lblMsg.Text += " Tienes permiso de Mostrar!";
                            _showMostrarHorariosButton = true;
                            BtnMostrarHorarios.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //lblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            lblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Secretaria")
            {
                lblMsg.Text = "Bienvenido, Secretaria!";
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
                            FrmAppointments.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAppointments.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            _showMostrarHorariosButton = true;
                            PanelAdmin.Visible = true;
                            BtnMostrarHorarios.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            lblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }

            else if (userRole == "Propietario")
            {
                lblMsg.Text = "Bienvenido, Propietario!";

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
                            FrmAppointments.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmAppointments.Visible = false;
                            BtnUpdate.Visible = false;
                            //PanelAdmin.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            _showMostrarHorariosButton = true;
                            PanelAdmin.Visible = true;
                            BtnMostrarHorarios.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = false;
                            _showDeleteButton = false;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            lblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                lblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _appoDate = DateTime.Parse(TBDate.Text);
            _appoStartHour = TimeSpan.Parse(TBHoraInicio.Text);
            _appoFinalHour = TimeSpan.Parse(TBHoraFin.Text);
            _fkAnimal = Convert.ToInt32(DDLAnimals.SelectedValue);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinario.SelectedValue);
            executed = objApp.saveDate(_fkAnimal, _fkVeterinarian, _appoDate,
                _appoStartHour, _appoFinalHour);
            if (executed)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Exitoso', 'Se registró exitosamente la cita', 'success')", true);

                clear();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error', 'Error al guardar la cita', 'error')", true);
            }
        }


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFAppoitmentID.Value))
            {
                lblMsg.Text = "No se ha seleccionado una cita para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFAppoitmentID.Value);
            _appoDate = DateTime.Parse(TBDate.Text);
            _appoStartHour = TimeSpan.Parse(TBHoraInicio.Text);
            _appoFinalHour = TimeSpan.Parse(TBHoraFin.Text);
            _fkAnimal = Convert.ToInt32(DDLAnimals.SelectedValue);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinario.SelectedValue);
            executed = objApp.updateDate(_id, _fkAnimal, _fkVeterinarian, _appoDate,
                _appoStartHour, _appoFinalHour);
            if (executed)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                   "swal('Exitoso', 'Se actualizó exitosamente la cita', 'success')", true);
                clear();
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert",
                    "swal('Error', 'Error al actualizar la cita ', 'error')", true);
            }
        
    }

        [WebMethod]
        public static bool DeleteAppoitment(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            AppointmentsLog objApp = new AppointmentsLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objApp.deleteDate(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFAppoitmentID.Value = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBHoraInicio.Text = "";
            TBHoraFin.Text = "";
            DDLAnimals.SelectedIndex = 0;
            DDLVeterinario.SelectedIndex = 0;
        }

    }
}