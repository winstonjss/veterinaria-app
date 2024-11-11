using Logic;
using SimpleCrypto;
using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                TBUsu_fecha_creación.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showDocumentTypeDDL();
                showRolesDDL();
            }

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
        }

        //Metodo para mostrar los tipo de documento en el DDL

        private void showDocumentTypeDDL()
        {
            DDLTipo_documento.DataSource = objDoc.showDocumentTypeDDL();
            DDLTipo_documento.DataValueField = "tip_doc_id";
            DDLTipo_documento.DataTextField = "tip_doc_descripcion";
            DDLTipo_documento.DataBind();
            DDLTipo_documento.Items.Insert(0, "Seleccione");

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