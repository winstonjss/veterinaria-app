using System;
using Logic;
using System.Data;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Collections.Generic;


namespace Presentation
{
    public partial class WFAppointments : System.Web.UI.Page
    {
        AppointmentsLog objApp = new AppointmentsLog();
        AnimalsLog objAni = new AnimalsLog();
        VeterinarianLog objVet = new VeterinarianLog();

        private TimeSpan _appoStartHour, _appoFinalHour;
        private DateTime _appoDate;
        private int _id, _fkVeterinarian, _fkAnimal;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Aqui se invocan todos los metodos
                TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                showAnimalsDDL();
                showVeterinariansDDL();
            }
        }

        [WebMethod]
        public static object ListAppoitments() {
            AppointmentsLog objApp = new AppointmentsLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objApp.showCitasAll();

            // Se crea una lista los datos que se van a devolver.
            var appoimentsList = new List<object>();

            // Se itera sobre cada fila del DataSet
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                appoimentsList.Add(new
                {
                    AppoitmentId = row["cit_id"],
                    AppoitmentDate = Convert.ToDateTime(row["cit_fecha"]).ToString("yyyy-MM-dd"),
                    AppoitmentHourStart = ((TimeSpan)row["cit_hora_inicio"]).ToString(@"hh\:mm"),
                    AppoitmentHourEnd = ((TimeSpan)row["cit_hora_fin"]).ToString(@"hh\:mm"),
                    FkAnimal = row["tbl_animales_anim_id"],
                    AnimalName = row["anim_nombre"],
                    FkVeterinary = row["tbl_veterinario_vet_id"],
                    VeterinaryName = row["vet_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = appoimentsList };
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
            _appoDate = DateTime.Parse(TBDate.Text);
            _appoStartHour = TimeSpan.Parse(TBHoraInicio.Text);
            _appoFinalHour = TimeSpan.Parse(TBHoraFin.Text);
            _fkAnimal = Convert.ToInt32(DDLAnimals.SelectedValue);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinario.SelectedValue);
            executed = objApp.saveDate(_fkAnimal,_fkVeterinarian,_appoDate,
                _appoStartHour, _appoFinalHour);
            if (executed)
            {
                lblMsg.Text = "se agendo exitosamente la cita";
                clear();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFAppoitmentID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFAppoitmentID.Value);
            _appoDate = DateTime.Parse(TBDate.Text);
            _appoStartHour = TimeSpan.Parse(TBHoraInicio.Text);
            _appoFinalHour = TimeSpan.Parse(TBHoraFin.Text);
            _fkAnimal = Convert.ToInt32(DDLAnimals.SelectedValue);
            _fkVeterinarian = Convert.ToInt32(DDLVeterinario.SelectedValue);
            executed = objApp.updateDate(_id, _fkAnimal, _fkVeterinarian, _appoDate,
                _appoStartHour, _appoFinalHour);
            if (executed)
            {
                lblMsg.Text = "se agendo exitosamente la cita";
                clear();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        [WebMethod]
        public static bool DeleteAppoitment(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            AppointmentsLog objApp = new AppointmentsLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objApp.deleteDate(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFAppoitmentID.Value = "";
            TBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBHoraInicio.Text = "";
            TBHoraFin.Text = "";
            DDLAnimals.SelectedIndex = 0;
            DDLVeterinario.SelectedIndex = 0;
        }

    }
}