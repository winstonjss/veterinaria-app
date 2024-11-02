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
    public partial class WFVeterinaryHours : System.Web.UI.Page
    {
        //Crear los objetos 
        VeterinaryHoursLog objVeh = new VeterinaryHoursLog();
        VeterinarianLog objVet = new VeterinarianLog();

        //Definir atributos
        private int _hor_vet_id, _fkVeterinarian;
        private DateTime _start_date, _end_date;
        private TimeSpan _start_time, _final_time;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                showVeterinaryHours();
                showVeterinarianDDL();
            }
        }
        //Metodo para mostrar todos los horarios del veterinario
        private void showVeterinaryHours()
        {
            DataSet ds = new DataSet();
            ds = objVeh.showVeterinaryHours();
            GVVeterinaryHours.DataSource = ds;
            GVVeterinaryHours.DataBind();
        }

        //Metodo para mostrar los veterinario en el DDL
        private void showVeterinarianDDL()
        {
            DDLVeterinarian.DataSource = objVet.showVeterinarianDDL();
            DDLVeterinarian.DataValueField = "vet_id"; //Nombre de la llave primaria
            DDLVeterinarian.DataTextField = "vet_nombre"; //Nombre del veterinario
            DDLVeterinarian.DataBind();
            DDLVeterinarian.Items.Insert(0, "Seleccione");
        }

        //Eventos que se ejecutan cuando se da clic en los botones (Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _start_date = Convert.ToDateTime(TBStart_date.Text);  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _end_date = Convert.ToDateTime(TBEnd_date.Text);
            _start_time = TimeSpan.Parse(TBStart_time.Text);
            _final_time = TimeSpan.Parse(TBFinal_time.Text);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinarian.SelectedValue);
            executed = objVeh.saveVeterinaryHours(_start_date, _end_date, _start_time, _final_time, _fkVeterinarian);

            if (executed)
            {
                LblMsg.Text = "El producto se guardo existosamente!";
                showVeterinaryHours();
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