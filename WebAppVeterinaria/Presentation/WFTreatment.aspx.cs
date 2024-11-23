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
    public partial class WFTreatment : System.Web.UI.Page
    {
        TreatmentLog objTrea = new TreatmentLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkDiagnoses, _id ;
        private string _description, _name;
        private DateTime _startDate, _endDate;
        private bool executed = false;


        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                showDiagnosesDDL();
                TBStartDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TBEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");


            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListTreatment() {
            TreatmentLog objTrea = new TreatmentLog();
            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objTrea.showTreatmentALL();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var treatmentList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un anamnesis).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                treatmentList.Add(new
                {
                    TreatmentId = row["trat_id"],
                    TreatmentName = row["trat_nombre"],
                    TreatmentDescription = row["trat_descripcion"],
                    TreatmentDateStart = Convert.ToDateTime(row["trat_fecha_inicio"]).ToString("yyyy-MM-dd"),
                    TreatmentDateEnd = Convert.ToDateTime(row["trat_fecha_fin"]).ToString("yyyy-MM-dd"),
                    FkDiagonoses = row["tbl_diagnosticos_diag_id"],
                    DiagonosesCode = row["diag_cod"]


                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = treatmentList };
        }


        private void showDiagnosesDDL()
        {
            DDLDiagonoses.DataSource = objDiag.showDiagnosesDLL();
            DDLDiagonoses.DataValueField = "diag_id";
            DDLDiagonoses.DataTextField = "diag_clasificacion";
            DDLDiagonoses.DataBind();
            DDLDiagonoses.Items.Insert(0, new ListItem("Seleccione", "0"));
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _description = TBDescription.Text;
            _startDate = DateTime.Parse(TBStartDate.Text);
            _endDate = DateTime.Parse(TBEndDate.Text);
            executed = objTrea.saveTratamiento(_name, _description, _startDate, _endDate, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo el tratamiento";
                
            }
            else
            {
                lblMsg.Text = "error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFTreatmentID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un Tratamiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFTreatmentID.Value);
            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _description = TBDescription.Text;
            _startDate = DateTime.Parse(TBStartDate.Text);
            _endDate = DateTime.Parse(TBEndDate.Text);
            executed = objTrea.updateTratamiento(_id, _name, _description, _startDate, _endDate, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo el tratamiento";

            }
            else
            {
                lblMsg.Text = "error al guardar";
            }
        }
        [WebMethod]
        public static bool DeleteTreatment(int id)
        {
            // Crear una instancia de la clase de lógica de anamnesis
            TreatmentLog objTrea = new TreatmentLog();

            // Invocar al método para eliminar el anamnesis y devolver el resultado
            return objTrea.deleteTratamiento(id);
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
                //Response.Redirect("Default.aspx");
                return;
            }
            // Obtener el tratamiento del usuario
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
                            FrmTreatment.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmTreatment.Visible = true;
                            BtnUpdate.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
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
                            FrmTreatment.Visible = true;
                            BtnSave.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatment.Visible = true;
                            BtnUpdate.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
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
                            FrmTreatment.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatment.Visible = false;
                            BtnUpdate.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            _showDeleteButton = false;
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
                            FrmTreatment.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmTreatment.Visible = false;
                            BtnUpdate.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
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
                // Si el Tratamiento no es reconocido, se deniega el acceso
                lblMsg.Text = "Tratamiento no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

        }



        //Metodo para limpiar los TextBox y los DDL

        private void clear()
        {
            HFTreatmentID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
            TBStartDate.Text = "";
            TBEndDate.Text = "";
            DDLDiagonoses.SelectedIndex = 0;
        
        
        }
    }
}
