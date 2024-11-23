using Logic;
using Model;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFVaccines : System.Web.UI.Page
    {
        VaccinesLog objVacc = new VaccinesLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkDiagnoses, _id;
        private string _name, _type;
        private string _description;
        private decimal _quantity;


        private bool executed = false;


        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                showDiagnosesDDL();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListVaccines()
        {
            VaccinesLog objVacc = new VaccinesLog();
            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objVacc.showVaccinessALL();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var VaccinesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                VaccinesList.Add(new
                {
                    VaccinesId = row["vac_id"],
                    VaccinesName = row["vac_nombre"],
                    VaccinesGuy = row["vac_tipo"],
                    VaccinesAmount = row["vac_cantidad"],
                    FkDiagonoses = row["tbl_diagnosticos_diag_id"],
                    DiagonosesCode = row["diag_cod"]


                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = VaccinesList };
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
            _type = TBGuy.Text;
            _quantity = Convert.ToDecimal(TBAmount.Text);
            executed = objVacc.saveVacuna(_name, _type, _quantity, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo la vacuna";
                clear();


            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFVaccinesID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un Tratamiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFVaccinesID.Value);

            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _type = TBGuy.Text;
            _quantity = Convert.ToDecimal(TBAmount.Text);
            executed = objVacc.updateVacuna(_id, _name, _type, _quantity, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo la vacuna";
                clear();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }

        }
        [WebMethod]
        public static bool deleteVaccines(int id)
        {
            // Crear una instancia de la clase de lógica de anamnesis
            VaccinesLog objVacc = new VaccinesLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objVacc.deleteVacuna(id);
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
                            FrmVaccines.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = true;
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
                            FrmVaccines.Visible = true;
                            BtnSave.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = true;
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
                            FrmVaccines.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = false;
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
                            FrmVaccines.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = false;
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
                lblMsg.Text = "Vacuna no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVaccinesID.Value = "";
            TBName.Text = "";
            TBGuy.Text = "";
            TBAmount.Text = "";
            DDLDiagonoses.SelectedIndex = 0;


        }
    }
}

using Logic;
using Model;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFVaccines : System.Web.UI.Page
    {
        VaccinesLog objVacc = new VaccinesLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkDiagnoses, _id;
        private string _name, _type;
        private string _description;
        private decimal _quantity;


        private bool executed = false;


        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                showDiagnosesDDL();
            }
            validatePermissionRol();
        }

        [WebMethod]
        public static object ListVaccines()
        {
            VaccinesLog objVacc = new VaccinesLog();
            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objVacc.showVaccinessALL();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var VaccinesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                VaccinesList.Add(new
                {
                    VaccinesId = row["vac_id"],
                    VaccinesName = row["vac_nombre"],
                    VaccinesGuy = row["vac_tipo"],
                    VaccinesAmount = row["vac_cantidad"],
                    FkDiagonoses = row["tbl_diagnosticos_diag_id"],
                    DiagonosesCode = row["diag_cod"]


                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = VaccinesList };
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
            _type = TBGuy.Text;
            _quantity = Convert.ToDecimal(TBAmount.Text);
            executed = objVacc.saveVacuna(_name, _type, _quantity, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo la vacuna";
                clear();


            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFVaccinesID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un Tratamiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFVaccinesID.Value);

            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _type = TBGuy.Text;
            _quantity = Convert.ToDecimal(TBAmount.Text);
            executed = objVacc.updateVacuna(_id, _name, _type, _quantity, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo la vacuna";
                clear();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }

        }
        [WebMethod]
        public static bool deleteVaccines(int id)
        {
            // Crear una instancia de la clase de lógica de anamnesis
            VaccinesLog objVacc = new VaccinesLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objVacc.deleteVacuna(id);
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
                            FrmVaccines.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = true;
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
                            FrmVaccines.Visible = true;
                            BtnSave.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = true;
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
                            FrmVaccines.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = false;
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
                            FrmVaccines.Visible = false;
                            BtnSave.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmVaccines.Visible = false;
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
                lblMsg.Text = "Vacuna no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }

        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVaccinesID.Value = "";
            TBName.Text = "";
            TBGuy.Text = "";
            TBAmount.Text = "";
            DDLDiagonoses.SelectedIndex = 0;


        }
    }
}

