using System;
using Logic;
using System.Data;
using System.CodeDom.Compiler;


namespace Presentation
{
    public partial class WFAppointments : System.Web.UI.Page
    {
        AppointmentsLog objApp = new AppointmentsLog();
        AnimalsLog objAni = new AnimalsLog();
        VeterinarianLog objVet = new VeterinarianLog();

        private TimeSpan _appoStartHour, _appoFinalHour;
        private DateTime _appoDate;
        private int _fkVeterinarian, _fkAnimal;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                showAppoitments();
                showAnimalsDDL();
                showVeterinariansDDL();                
            }
        }

        private void showAppoitments() {
            DataSet ds = new DataSet();    
            ds = objApp.showCitasAll();
            GVCitas.DataSource = ds;
            GVCitas.DataBind();
        }

        private void showAnimalsDDL()
        {
            DDLAnimals.DataSource = objAni.showAnimalsDDL();
            DDLAnimals.DataValueField = "anim_id";
            DDLAnimals.DataTextField = "anim_nombre";
            DDLAnimals.DataBind();
            DDLAnimals.Items.Insert(0, "seleccione");
        }

        private void showVeterinariansDDL()
        {
            DDLVeterinario.DataSource = objVet.showVeterinarianDDL();
            DDLVeterinario.DataValueField = "vet_id";
            DDLVeterinario.DataTextField = "vet_nombre";
            DDLVeterinario.DataBind();
            DDLVeterinario.Items.Insert(0, "seleccione");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {            
            _appoDate = CALCita.SelectedDate;
            _appoStartHour = TimeSpan.Parse(TBHoraInicio.Text);
            _appoFinalHour = TimeSpan.Parse(TBHoraFin.Text);
            _fkAnimal = Convert.ToInt32(DDLAnimals.SelectedValue);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinario.SelectedValue);
            executed = objApp.saveDate(_fkAnimal,_fkVeterinarian,_appoDate,
                _appoStartHour, _appoFinalHour);
            if (executed)
            {
                lblMsg.Text = "se agendo exitosamente la cita";
                showAppoitments();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }

    }
}