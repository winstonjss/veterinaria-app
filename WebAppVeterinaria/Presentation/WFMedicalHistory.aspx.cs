using Logic;
using Model;
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
    public partial class WFMedicalHistory : System.Web.UI.Page
    {
       
            MedicalHistoryLog objMed = new MedicalHistoryLog();
            AppointmentsLog objApp = new AppointmentsLog();
        private int _id, _fkAppoitment;
        private bool executed = false;
        private DateTime _medicalHistoryDate;
        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showAppoitmentsDDL();
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmMedicalHistory.Visible = false;
                PanelAdmin.Visible = false;
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListMedicalHistory()
        {
            MedicalHistoryLog objMed = new MedicalHistoryLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objMed.showMedicalHistoryAll();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var medicalsHistoryList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                medicalsHistoryList.Add(new
                {
                    MedicalHistoryID = row["histo_cli_id"],
                    Date = Convert.ToDateTime(row["histo_cli_fecha"]).ToString("yyyy-MM-dd"),
                    Description = row["histo_cli_descripcion"],
                    FKAppoitment = row["tbl_citas_cit_id"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = medicalsHistoryList };
        }

        private void showAppoitmentsDDL()
        {
            DDLAppoitment.DataSource = objApp.showCitasDDl();
            DDLAppoitment.DataValueField = "cit_id";
            DDLAppoitment.DataTextField = "detalle_cita";
            DDLAppoitment.DataBind();
            DDLAppoitment.Items.Insert(0, new ListItem("seleccione", "0"));

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
                            FrmMedicalHistory.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmMedicalHistory.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //lblMsg.Text += " Tienes permiso de Mostrar!";
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
                            FrmMedicalHistory.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmMedicalHistory.Visible = true;
                            BtnUpdate.Visible = true;
                            //PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //lblMsg.Text += " Tienes permiso de Mostrar!";
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
                            FrmMedicalHistory.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmMedicalHistory.Visible = true;
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
                            FrmMedicalHistory.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmMedicalHistory.Visible = false;
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

        //eventos que se ejecutan cuando se da click en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkAppoitment = Convert.ToInt32(DDLAppoitment.SelectedValue);
            _medicalHistoryDate = DateTime.Parse(TBDate.Text);
            executed = objMed.saveMedicalHistoryByDateId(_fkAppoitment, _medicalHistoryDate);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFMedicalHistoryID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFMedicalHistoryID.Value);
            _fkAppoitment = Convert.ToInt32(DDLAppoitment.SelectedValue);
            _medicalHistoryDate = DateTime.Parse(TBDate.Text);

            executed = objMed.updateMedicalHistory(_fkAppoitment, _medicalHistoryDate,
                _fkAppoitment);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        [WebMethod]
        public static bool DeleteMedicalHistory(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            MedicalHistoryLog objMed = new MedicalHistoryLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objMed.deleteMedicalHistory(id);
        }

        private void clear()
        {
            HFMedicalHistoryID.Value = "";
            DDLAppoitment.SelectedIndex = 0;
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

        }
    }
}