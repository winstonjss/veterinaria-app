using Logic;
using Model;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFDocumentType : System.Web.UI.Page
    {
        //Crear los objetos 
        DocumentTypeLog objDocType = new DocumentTypeLog();
        private int _id;
        private string _descripcion;
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
                // Los botones y otros elementos se inicializan en false, no visibles.
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmDocumentType.Visible = false;
                PanelAdmin.Visible = false;

            }
            validatePermissionRol();

        }

        //Metodo para mostrar todos tipos de documento
        [WebMethod]
        public static object ListDocumentType()
        {
            DocumentTypeLog objDoc = new DocumentTypeLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objDoc.showDocumentType();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var documentTypeList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                documentTypeList.Add(new
                {
                    ID = row["tip_doc_id"],
                    DocumentType = row["tip_doc_descripcion"],

                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = documentTypeList };
        }
        [WebMethod]

        public static bool deleteDocumentType(int id)
        {
            // Crear una instancia de la clase de lógica de tipo de documento 
            DocumentTypeLog objDoc = new DocumentTypeLog();


            // Invocar al método para eliminar el producto y devolver el resultado
            return objDoc.deleteDocumentType(id);
        }

        private void clear()
        {
            HFDocumenTypeId.Value = "";
            DDLTip_doc_descripcion.SelectedIndex =0;
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
                            FrmDocumentType.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmDocumentType.Visible = true;
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
                            FrmDocumentType.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDocumentType.Visible = true;
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
                            FrmDocumentType.Visible = true;
                            BtnSave.Visible = true;
                            //PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmDocumentType.Visible = true;
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
                            FrmDocumentType.Visible = false;
                            BtnSave.Visible = false;
                            //PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmDocumentType.Visible = false;
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
            // Verificar que todos validadores de la pagina esten ok
            if (Page.IsValid)
            {
                _descripcion = DDLTip_doc_descripcion.SelectedValue.ToUpper();
                executed = objDocType.saveDocumentType(_descripcion);


                if (executed)
                {
                    LblMsg.Text = "Se guardó exitosamente ";
                    //showDocumentType() ;
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
            // Verifica si se ha seleccionado un Documento  para actualizar
            if (string.IsNullOrEmpty(HFDocumenTypeId.Value))
            {
                LblMsg.Text = "No se ha seleccionado un tipo de documento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFDocumenTypeId.Value);
            _descripcion = DDLTip_doc_descripcion.SelectedValue.ToUpper();
            executed = objDocType.updatedocumenttype(_id, _descripcion);


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