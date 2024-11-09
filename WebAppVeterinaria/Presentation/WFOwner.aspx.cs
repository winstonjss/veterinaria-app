using Logic;
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
    public partial class WFOwner : System.Web.UI.Page
    {
        //Crear los objetos 
        OwnerLog objOwn = new OwnerLog();
        UsersLog objUse = new UsersLog();

        //Definir atributos
        private int _pro_id, _fkUsers;
        private string _name, _phone;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showOwner();
                showUsersDDL();
            }
        }


        /*
        * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
        * parte de un servicio web, lo que significa que puede ser invocado de manera
        * remota a través de HTTP.
        */

        //  ***  Metodo Listar un Propietario
        [WebMethod]
        public static object ListOwner()
        {
            OwnerLog objOwn = new OwnerLog();

            // Se obtiene un DataSet que contiene la lista de propietarios desde la base de datos.
            var dataSet = objOwn.showOwner();

            // Se crea una lista para almacenar los propietarios que se van a devolver.
            var ownerList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un propietario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                ownerList.Add(new
                {
                    OwnerID = row["pro_id"],
                    Name = row["pro_nombre"],
                    Phone = row["pro_telefono"],
                    FkUser = row["tbl_usuarios_usu_id"],
                    NameUser = row["usu_documento"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de propietarios.
            return new { data = ownerList };
        }
        [WebMethod]


        //  ***  Metodo para eliminar propietarios
        public static bool DeleteOwner(int id)
        {
            // Crear una instancia de la clase de lógica de propietarios
            OwnerLog objOwn = new OwnerLog();

            // Invocar al método para eliminar el propietario y devolver el resultado
            return objOwn.deleteOwner(id);
        }


        //  ***  Metodo para mostrar los usuarios en el DDL
        private void showUsersDDL()
        {
            DDLUsers.DataSource = objUse.showUsersDDL();
            DDLUsers.DataValueField = "usu_id"; //Nombre de la llave primaria
            DDLUsers.DataTextField = "usu_documento"; //Documento del usuario
            DDLUsers.DataBind();
            DDLUsers.Items.Insert(0, "Seleccione");
        }


        //  ***  Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFOwnerID.Value = "";
            TBName.Text = "";
            TBPhone.Text = "";
            DDLUsers.SelectedIndex = 0;
        }


        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);

            executed = objOwn.saveOwner(_name, _phone, _fkUsers);

            if (executed)
            {
                LblMsg.Text = "El propietario se guardo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
                //showOwner();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }


        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un propietario para actualizar
            if (string.IsNullOrEmpty(HFOwnerID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un propietario para actualizar.";
                return;
            }
            _pro_id = Convert.ToInt32(HFOwnerID.Value);
            _name = TBName.Text;
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);

            executed = objOwn.updateOwner(_pro_id, _name, _phone, _fkUsers);

            if (executed)
            {
                LblMsg.Text = "El propietario se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}