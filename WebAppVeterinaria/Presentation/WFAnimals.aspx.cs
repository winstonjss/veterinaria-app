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
    public partial class WFAnimals : System.Web.UI.Page
    {
        //Crear los objetos 
        AnimalsLog objAni = new AnimalsLog();
        OwnerLog objOwn = new OwnerLog();

        //Definir atributos
        private int _anim_id, _fkOwner;
        private string _name, _species, _race, _sex, _color;
        private DateTime _date_birth;
        private float _weight;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                showAnimals();
                showOwnerDDL();
            }
        }
        //Metodo para mostrar todos los animales
        private void showAnimals()
        {
            DataSet ds = new DataSet();
            ds = objAni.showAnimals();
            GVAnimals.DataSource = ds;
            GVAnimals.DataBind();
        }

        //Metodo para mostrar los propietarios en el DDL
        private void showOwnerDDL()
        {
            DDLOwner.DataSource = objOwn.showOwnerDDL();
            DDLOwner.DataValueField = "pro_id"; //Nombre de la llave primaria
            DDLOwner.DataTextField = "pro_nombre"; //Nombre del propietario
            DDLOwner.DataBind();
            DDLOwner.Items.Insert(0, "Seleccione");
        }

        //Eventos que se ejecutan cuando se da clic en los botones (Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _species = TBSpecies.Text;
            _race = TBRace.Text;
            _date_birth = Convert.ToDateTime(TBDate_birth.Text); //Aqui se hace un proceso de parseo de forma manual (Convert.ToInt32)
            _sex = TBSex.Text;
            _weight = Convert.ToSingle(TBWeight.Text); //NO es caja de texto, es un selector de propiedad (Value) y aplicar casteo (Convert.ToInt32)
            _color = TBColor.Text;
            _fkOwner = Convert.ToInt32(DDLOwner.SelectedValue);

            executed = objAni.saveAnimals(_name, _species, _race, _date_birth, _sex, _weight, _color, _fkOwner);

            if (executed)
            {
                LblMsg.Text = "El producto se guardo existosamente!";
                showAnimals();
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