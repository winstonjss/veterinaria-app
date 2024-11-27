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
    public partial class WFinicio : System.Web.UI.Page
    {
        //Crear los objetos
        UsersLog objUsu = new UsersLog();
        AnimalsLog objAni = new AnimalsLog();
        OwnerLog objOwner = new OwnerLog();
        VeterinarianLog objVet = new VeterinarianLog();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCountUsers();
                showCountAnimals();
                showCountOwner();
                showCountVeterinarian();
            }
            validatePermissionRol();
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
                            FrmInicio.Visible = true;// Se pone visible el formulario
                            //BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
                            //BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            //_showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            //PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            //PanelAdmin.Visible = true;
                            //_showDeleteButton = true;
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
                            FrmInicio.Visible = true;
                            //BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
                            //BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            //_showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            //PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            //PanelAdmin.Visible = true;
                            //_showDeleteButton = true;
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
                            FrmInicio.Visible = true;
                            //BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = true;
                            //BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            //_showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = true;
                            //_showDeleteButton = true;
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
                            FrmInicio.Visible = false;
                            //BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmInicio.Visible = false;
                            //BtnUpdate.Visible = false;
                            //PanelAdmin.Visible = false;
                            //_showEditButton = false;
                            break;
                        case "MOSTRAR":
                            //PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //PanelAdmin.Visible = false;
                            //_showDeleteButton = false;
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

        [WebMethod]

        public static object list()
        {
            AppointmentsLog objApp = new AppointmentsLog();
            DateTime fechaActual = DateTime.Now;


            // Calcular el primer día del mes
            DateTime primerDia = new DateTime(fechaActual.Year, fechaActual.Month, 1);

            // Calcular el último día del mes
            DateTime ultimoDia = primerDia.AddMonths(1).AddDays(-1);

            // Las fechas en formato DateTime
            DateTime fechaFormato = fechaActual.Date;
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objApp.spCitasResumenMesActual2(primerDia, ultimoDia, fechaFormato);


            // Se crea una lista los datos que se van a devolver.
            var appoimentsList = new List<object>();

            // Se itera sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                appoimentsList.Add(new
                {
                    MesActual = row["MesActual"],
                    CitasUno = row["TotalCitasAtendidas"],
                    CitasDos = row["TotalCitasPendientes"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = appoimentsList };
        }

        //Médoto para el data set de la gráfica Diagnosticos 
        [WebMethod]
        public static object spGraficoLineasRecursosPorDiagnostico()
        {
            DiagnosesLog objDiag= new DiagnosesLog();
          

           // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objDiag.spGraficoLineasRecursosPorDiagnostico();
            // Se crea una lista los datos que se van a devolver.
            var diagnosesList = new List<object>();

            // Se itera sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                diagnosesList.Add(new
                {
                    Diagnostico = row["Diagnostico"],
                    TotalRecursos = row["TotalRecursos"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = diagnosesList};
        }

        //Médoto para el data set de la gráfica Diagnosticos 
        [WebMethod]
        public static object selectVaccines()
        {
            VaccinesLog objVac = new VaccinesLog();
           
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objVac.SelectVaccinesbyDiagnoses();
            // Se crea una lista los datos que se van a devolver.
            var vacinesList = new List<object>();

            // Se itera sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                vacinesList.Add(new
                {
                    Vacuna = row["nombre_vacuna"],
                    Diagnostico = row["diagnostico"],
                    Cantidad = row["cantidad"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = vacinesList };
        }

        private void showCountUsers()
        {
            int count = objUsu.showCountUsers();
            LblCantUsu.Text = count.ToString();
        }

        private void showCountAnimals()
        {
            int count = objAni.showCountAnimals();
            LblCantAnim.Text = count.ToString();
        }

        private void showCountOwner()
        {
            int count = objOwner.showCountOwners();
            LblCantProp.Text = count.ToString();
        }

        private void showCountVeterinarian()
        {
            int count = objVet.showCountVeterinarian();
            LblCantVet.Text = count.ToString();
        }
    }
   
}