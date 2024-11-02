using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                showVeterinarian();
                showUsersDDL();
                showOfficeDDL();
            }
        }

        //Metodo para mostrar todos los propietarios
        private void showVeterinarian()
        {
            DataSet ds = new DataSet();
            ds = objVet.showVeterinarian();
            GVVeterinarian.DataSource = ds;
            GVVeterinarian.DataBind();
        }

        //Metodo para mostrar los usuarios en el DDL
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

        //Eventos que se ejecutan cuando se da clic en los botones (Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            _fkOffice = Convert.ToInt32(DDLOffice.SelectedValue);
            executed = objVet.saveVeterinarian(_name, _phone, _fkUsers, _fkOffice);

            if (executed)
            {
                LblMsg.Text = "El producto se guardo existosamente!";
                showVeterinarian();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        //Eventos que se ejecutan cuando se da clic en los botones (Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}