using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

namespace Presentation
{
    public partial class WFVeterinarian : System.Web.UI.Page
    {
        //Crear los objetos 
        VeterinarianLog objVet = new VeterinarianLog();
        UsersLog objUse = new UsersLog();
        OfficeLog objOfi = new OfficeLog();

        //Definir atributos
        private int _vet_id, _fkUsers, _fkOffice;
        private string _name, _phone;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                //showVeterinarian();
                showUsersDDL();
                showOfficeDDL();
            }
        }
        /*
        * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
        * parte de un servicio web, lo que significa que puede ser invocado de manera
        * remota a través de HTTP.
        */

        //  ***  Metodo Listar un Veterinario
        [WebMethod]
        public static object ListVeterinarian()
        {
            VeterinarianLog objVet = new VeterinarianLog();

            // Se obtiene un DataSet que contiene la lista de veterinarios desde la base de datos.
            var dataSet = objVet.showVeterinarian();

            // Se crea una lista para almacenar los veterinarios que se van a devolver.
            var veterinarianList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un veterinario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                veterinarianList.Add(new
                {
                    VeterinarianID = row["vet_id"],
                    Name = row["vet_nombre"],
                    Phone = row["vet_telefono"],
                    FkUser = row["tbl_usuarios_usu_id"],
                    NameUser = row["usu_documento"],
                    FkOffice = row["tbl_consultorio_con_id"],
                    NameOffice = row["con_num_consultorio"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de veterinarios.
            return new { data = veterinarianList };
        }
        [WebMethod]


        //  ***  Metodo para eliminar veterinarios
        public static bool DeleteVeterinarian(int id)
        {
            // Crear una instancia de la clase de lógica de veterinarios
            VeterinarianLog objVet = new VeterinarianLog();

            // Invocar al método para eliminar el veterinario y devolver el resultado
            return objVet.deleteVeterinarian(id);
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


        //Metodo para mostrar los consultorios en el DDL
        private void showOfficeDDL()
        {
            DDLOffice.DataSource = objOfi.showOffice();
            DDLOffice.DataValueField = "con_id"; //Nombre de la llave primaria
            DDLOffice.DataTextField = "con_num_consultorio"; //NUmero del consultorio
            DDLOffice.DataBind();
            DDLOffice.Items.Insert(0, "Seleccione");
        }


        //  ***  Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVeterinarianID.Value = "";
            TBName.Text = "";
            TBPhone.Text = "";
            DDLUsers.SelectedIndex = 0;
            DDLOffice.SelectedIndex = 0;
        }


        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            _fkOffice = Convert.ToInt32(DDLOffice.SelectedValue);

            executed = objVet.saveVeterinarian(_name, _phone, _fkUsers, _fkOffice);

            if (executed)
            {
                LblMsg.Text = "El veterinario se guardo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
                //showVeterinarian();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }


        //Eventos que se ejecutan cuando se da clic en los botones (Metodo Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un veterinario para actualizar
            if (string.IsNullOrEmpty(HFVeterinarianID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un veterinario para actualizar.";
                return;
            }
            _vet_id = Convert.ToInt32(HFVeterinarianID.Value);
            _name = TBName.Text;
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            _fkOffice = Convert.ToInt32(DDLOffice.SelectedValue);

            executed = objVet.updateVeterinarian(_vet_id, _name, _phone, _fkUsers, _fkOffice);

            if (executed)
            {
                LblMsg.Text = "El veterinario se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}