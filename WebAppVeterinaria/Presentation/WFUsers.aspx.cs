using Logic;
using Model;
using SimpleCrypto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Presentation
{

    public partial class WFUsers : System.Web.UI.Page
    {
        //Crear los objetos 
        UsersLog objUse = new UsersLog();
        DocumentTypeLog objDoc = new DocumentTypeLog();
        RolesLog objRol = new RolesLog();

        private int _usu_id, _rol_id, _tipo_documento_id;
        private string _documento, _correo, _contrasena, _salt, _estado, _encryptedPassword;
        private DateTime _fecha_creacion;
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
                //aqui se invocan todos los métodos 
                TBUsu_fecha_creación.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showDocumentTypeDDL();
                showRolesDDL();

                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmUsers.Visible = false;
                PanelAdmin.Visible = false;
            }
            validatePermissionRol();
        }

        //Metodo para mostrar todos los usuarios
        /*
        * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
        * parte de un servicio web, lo que significa que puede ser invocado de manera
        * remota a través de HTTP.
        */
        [WebMethod]
        public static object ListUsers()
        {
            UsersLog objUser = new UsersLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objUser.showUsers();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var usersList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                usersList.Add(new
                {
                    UserID = row["usu_id"],
                    Document = row["usu_documento"],
                    Email = row["usu_correo"],
                    Password = row["usu_contrasena"],
                    Salt = row["usu_salt"],
                    // No incluir Password ni Salt
                    State = row["usu_estado"],
                    Date = Convert.ToDateTime(row["usu_fecha_creacion"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    FkRol = row["rol_nombre"],
                    FKDocumentType = row["tip_doc_descripcion"]

                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = usersList };
        }
        [WebMethod]

        public static bool DeleteUser(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            UsersLog objUser = new UsersLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objUser.deleteUser(id);
        }
        //Metodo para mostrar los roles DDL
        private void showRolesDDL()
        {
            DDLRol.DataSource = objRol.showRolesDDL();
            DDLRol.DataValueField = "rol_id";
            DDLRol.DataTextField = "rol_nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione");
            // Añade manualmente la opción inicial al DropDownList
            DDLRol.Items.Insert(0, new ListItem("Seleccione", "0")); 
        }

        //Metodo para mostrar los tipo de documento en el DDL

        private void showDocumentTypeDDL()
        {
            DDLTipo_documento.DataSource = objDoc.showDocumentTypeDDL();
            DDLTipo_documento.DataValueField = "tip_doc_id";
            DDLTipo_documento.DataTextField = "tip_doc_descripcion";
            DDLTipo_documento.DataBind();
            DDLTipo_documento.Items.Insert(0, "Seleccione");
            // Añade manualmente la opción inicial al DropDownList
            DDLTipo_documento.Items.Insert(0, new ListItem("Seleccione", "0")); 

        }

        private void clear()
        {
            HDUserID.Value = "";
            TBUsu_documento.Text = "";
            TBUsu_correo.Text = "";
            TBUsu_contrasena.Text = "";
            DDLState.Text = "";
            TBUsu_fecha_creación.Text = "";
            DDLRol.SelectedIndex = 0;
            DDLTipo_documento.SelectedIndex = 0;

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
                            FrmUsers.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmUsers.Visible = true;
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
                            FrmUsers.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmUsers.Visible = true;
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
                            FrmUsers.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmUsers.Visible = true;
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
                            FrmUsers.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmUsers.Visible = false;
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
            /*
             * PBKDF2: Password-Based Key Derivation Function 2, es un algoritmo para proteger contraseñas,
             * ya que es seguro contra ataques de fuerza bruta, genera un hash mediante múltiples iteraciones
             */
            ICryptoService cryptoService = new PBKDF2();
            _documento = TBUsu_documento.Text;
            _correo = TBUsu_correo.Text;
            _contrasena = TBUsu_contrasena.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_contrasena);// Se generar un hash de la contraseña.
            _estado = DDLState.Text;
            _fecha_creacion = DateTime.Parse(TBUsu_fecha_creación.Text);
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _tipo_documento_id = Convert.ToInt32(DDLTipo_documento.SelectedValue);

            executed = objUse.saveUser(_documento, _correo, _encryptedPassword, _salt, _estado,
                _fecha_creacion, _rol_id, _tipo_documento_id);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente el usuario ";
                //showUsers();
                clear();//Se invoca el metodo para limpiar los campos 

            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un producto para actualizar
            if (string.IsNullOrEmpty(HDUserID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un usuario para actualizar.";
                return;
            }
            ICryptoService cryptoService = new PBKDF2();
            _usu_id = Convert.ToInt32(HDUserID.Value);
            _documento = TBUsu_documento.Text;
            _correo = TBUsu_correo.Text;
            _contrasena = TBUsu_contrasena.Text;
            _salt = cryptoService.GenerateSalt();// Se generar un salt único para esa contraseña.
            _encryptedPassword = cryptoService.Compute(_contrasena);// Se generar un hash de la contraseña.
            _estado = DDLState.Text;
            _fecha_creacion = DateTime.Parse(TBUsu_fecha_creación.Text);
            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _tipo_documento_id = Convert.ToInt32(DDLTipo_documento.SelectedValue);

            executed = objUse.UpdateUser(_usu_id, _documento, _correo, _encryptedPassword, _salt, _estado,
                _fecha_creacion, _rol_id, _tipo_documento_id);


            if (executed)
            {
                LblMsg.Text = "Se Actualizó exitosamente el usuario ";
                //showUsers();
                clear();

            }
            else
            {
                LblMsg.Text = "Error al Actualizar ";
            }
        }
    }
}