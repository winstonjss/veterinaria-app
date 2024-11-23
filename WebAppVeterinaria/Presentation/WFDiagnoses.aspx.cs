using Logic;
using Model;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFDiagnoses : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        DiagnosesLog objDiag = new DiagnosesLog();
        private int _fkAnemnesis, _id;
        private string _clasification, _code;
        private bool executed = false;

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmDiagnoses.Visible = false;
                PanelAdmin.Visible = false;


                showAnamnesisDDL();

            }
            validatePermissionRol();
        }


        [WebMethod]
        public static object ListDiagnoses()
        {
            DiagnosesLog objDiag = new DiagnosesLog();

            // Se obtiene un DataSet que contiene la lista de Diaguctos desde la base de datos.
            var dataSet = objDiag.showDiagnosticos();

            // Se crea una lista para almacenar los Diaguctos que se van a devolver.
            var DiagnosesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Diagucto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                DiagnosesList.Add(new
                {
                    DiagnosesID = row["diag_id"],
                    Classification = row["diag_clasificacion"],
                    Code = row["diag_cod"],
                    FkAnamnesis = row["tbl_anamnesis_anam_id"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Diagnosticos.
            return new { data = DiagnosesList };
        }




        private void showAnamnesisDDL()
        {
            DDLAnamnesis.DataSource = objAnam.showAnamnesisDDl();
            DDLAnamnesis.DataValueField = "anam_id";
            DDLAnamnesis.DataTextField = "anam_descripcion";
            DDLAnamnesis.DataBind();
            DDLAnamnesis.Items.Insert(0, new ListItem("Seleccione", "0"));
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkAnemnesis = Convert.ToInt32(DDLAnamnesis.SelectedValue);

            _clasification = TBClassification.Text;
            _code = TBCode.Text;
            executed = objDiag.saveDiagnostico(_clasification, _code, _fkAnemnesis);
            if (executed)
            {
                lblMsg.Text = "se guardo diagnostico";
                showAnamnesisDDL();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(HFDiagnosesID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un anamnesis para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFDiagnosesID.Value);
            _fkAnemnesis = Convert.ToInt32(DDLAnamnesis.SelectedValue);

            _clasification = TBClassification.Text;
            _code = TBCode.Text;
            executed = objDiag.updateDiagnostico(_id, _clasification, _code, _fkAnemnesis);
            if (executed)
            {
                lblMsg.Text = "se guardo diagnostico";
                showAnamnesisDDL();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }
        [WebMethod]
        public static bool deleteDiagnostico(int id)
        {
            // Crear una instancia de la clase de lógica de diagnostico
            DiagnosesLog objDiag = new DiagnosesLog();

            // Invocar al método para eliminar el diagnostico y devolver el resultado
            return objDiag.deleteDiagnostico(id);
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
                            FrmDiagnoses.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnoses.Visible = true;
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
                            FrmDiagnoses.Visible = true;
                            BtnSave.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnoses.Visible = true;
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

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmDiagnoses.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnoses.Visible = false;
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
                            FrmDiagnoses.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmDiagnoses.Visible = false;
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
                // Si el rol no es reconocido, se deniega el acceso
                lblMsg.Text = "Diagnostico no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFDiagnosesID.Value = "";
            TBClassification.Text = "";
            TBCode.Text = "";
            DDLAnamnesis.SelectedIndex = 0;
        }

    }
}
