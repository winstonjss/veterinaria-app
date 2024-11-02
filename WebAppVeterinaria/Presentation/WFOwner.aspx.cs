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
                showOwner();
                showUsersDDL();
            }
        }
        //Metodo para mostrar todos los propietarios
        private void showOwner()
        {
            DataSet ds = new DataSet();
            ds = objOwn.showOwner();
            GVOwner.DataSource = ds;
            GVOwner.DataBind();
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

        //Eventos que se ejecutan cuando se da clic en los botones (Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _phone = TBPhone.Text;
            _fkUsers = Convert.ToInt32(DDLUsers.SelectedValue);
            executed = objOwn.saveOwner(_name, _phone, _fkUsers);

            if (executed)
            {
                LblMsg.Text = "El producto se guardo existosamente!";
                showOwner();
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