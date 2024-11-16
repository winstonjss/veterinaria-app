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
    public partial class WFAnamnensis : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        AppointmentsLog objAppo = new AppointmentsLog();

        private int _fkAppointment;
        private string _description;
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
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAnamnesis.Visible = false;
                PanelAdmin.Visible = false;
                showAppointmentsDDL();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListAnamnesis()
        {
            AnamnesisLog objAnan = new AnamnesisLog();

            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objAnan.showAnamnesisAll();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var AnamnesisList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa anamnesis).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                AnamnesisList.Add(new
                {
                    AnamnesisID = row["anam_id"],
                    Description = row["anam_descripcion"],
                    FkAppointment = row["tbl_citas_cit_id"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = AnamnesisList };
        }

        private void showAppointmentsDDL()
        {
            DDLAppointments.DataSource = objAppo.showCitasDDl();
            DDLAppointments.DataValueField = "cit_id";
            DDLAppointments.DataTextField = "detalle_cita";
            DDLAppointments.DataBind();
            DDLAppointments.Items.Insert(0, new ListItem("Seleccione", ""));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            
            
                _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
                _description = TBDescription.Text;
                executed = objAnam.saveAnamnesis(_description, _fkAppointment);
                if (executed)
                {
                    lblMsg.Text = "Se guardó anamnesis correctamente.";
                    ClearForm();
                }
                else
                {
                    lblMsg.Text = "Error al guardar anamnesis.";
                }
            
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFAnamnesisID.Value))
            {
                lblMsg.Text = "No se ha seleccionado anamnesis para actualizar.";
                return;
            }
            int anamnesisId = Convert.ToInt32(HFAnamnesisID.Value);
            _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
            _description = TBDescription.Text;
            executed = objAnam.updateAnamnesis(anamnesisId, _description, _fkAppointment);
            if (executed)
            {
                lblMsg.Text = "Se actualizó anamnesis correctamente.";

                ClearForm();
            }
            else
            {
                lblMsg.Text = "Error al actualizar anamnesis.";
            }
        }


        private void ClearForm()
        {
            HFAnamnesisID.Value = "";
            TBDescription.Text = "";
            DDLAppointments.SelectedIndex = 0;
        }

        [WebMethod]
        public static bool DeleteAnamnesis(int id)
        {
            AnamnesisLog objAnam = new AnamnesisLog();
            return objAnam.deleteAnamnesis(id);
        }

        // Metodo validar permisos roles
        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Admin")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnamnesis.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAnamnesis.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
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
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnamnesis.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAnamnesis.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
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

    }
}
