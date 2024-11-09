using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

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
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBStart_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TBEnd_date.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // Se asigna la hora actual al TextBox en formato "HH:mm".
                TBStart_time.Text = DateTime.Now.ToString("HH:mm");
                TBFinal_time.Text = DateTime.Now.ToString("HH:mm");

                //showVeterinaryHours();
                showVeterinarianDDL();
            }
        }


        /*
        * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
        * parte de un servicio web, lo que significa que puede ser invocado de manera
        * remota a través de HTTP.
        */

        //  ***  Metodo Listar un Horario de Veterinarios
        [WebMethod]
        public static object ListVeterinaryHours()
        {
            VeterinaryHoursLog objVeh = new VeterinaryHoursLog();

            // Se obtiene un DataSet que contiene la lista de horarios de veterinarios desde la base de datos.
            var dataSet = objVeh.showVeterinaryHours();

            // Se crea una lista para almacenar los horarios de veterinarios que se van a devolver.
            var veterinaryhoursList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un horario de veterinario).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                veterinaryhoursList.Add(new
                {
                    VeterinaryHoursID = row["hor_vet_id"],
                    Start_date = Convert.ToDateTime(row["hor_vet_fecha_inicio"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    End_date = Convert.ToDateTime(row["hor_vet_fecha_final"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.
                    Start_time = TimeSpan.Parse(row["hor_vet_hora_inicio"].ToString()).ToString(@"hh\:mm"), // Formato de hora específico.
                    Final_time = TimeSpan.Parse(row["hor_vet_hora_final"].ToString()).ToString(@"hh\:mm"), // Formato de hora específico.

                    FkVeterinarian = row["tbl_veterinario_vet_id"],
                    NameVeterinarian = row["vet_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de horarios de veterinarios.
            return new { data = veterinaryhoursList };
        }
        [WebMethod]


        //  ***  Metodo para eliminar un horario de veterinario
        public static bool DeleteVeterinaryHours(int id)
        {
            // Crear una instancia de la clase de lógica de horarios de veterinarios
            VeterinaryHoursLog objVeh = new VeterinaryHoursLog();

            // Invocar al método para eliminar el horario de veterinario y devolver el resultado
            return objVeh.deleteVeterinaryHours(id);
        }


        //Metodo para mostrar los horarios de veterinarios en el DDL
        private void showVeterinarianDDL()
        {
            DDLVeterinarian.DataSource = objVet.showVeterinarianDDL();
            DDLVeterinarian.DataValueField = "vet_id"; //Nombre de la llave primaria
            DDLVeterinarian.DataTextField = "vet_nombre"; //Nombre del veterinario
            DDLVeterinarian.DataBind();
            DDLVeterinarian.Items.Insert(0, "Seleccione");
        }


        //  ***  Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVeterinaryHoursID.Value = "";
            TBStart_date.Text = DateTime.Now.ToString("yyyy-MM-dd"); // Asignar fecha actual al limpiar
            TBEnd_date.Text = DateTime.Now.ToString("yyyy-MM-dd"); // Asignar fecha actual al limpiar

            TBStart_time.Text = DateTime.Now.ToString("HH:mm"); // Asignar hora actual al limpiar
            TBFinal_time.Text = DateTime.Now.ToString("HH:mm"); // Asignar hora actual al limpiar

            TBStart_time.Text = "";
            TBFinal_time.Text = "";
            DDLVeterinarian.SelectedIndex = 0;
        }


        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _start_date = DateTime.Parse(TBStart_date.Text); // Guardar la fecha de inicio
            _end_date = DateTime.Parse(TBEnd_date.Text); // Guardar la fecha de final

            _start_time = TimeSpan.Parse(TBStart_time.Text); // Guardar la hora de inicio
            _final_time = TimeSpan.Parse(TBFinal_time.Text); // Guardar la hora final
            _fkVeterinarian = Convert.ToInt32(DDLVeterinarian.SelectedValue);

            executed = objVeh.saveVeterinaryHours(_start_date, _end_date, _start_time, _final_time, _fkVeterinarian);

            if (executed)
            {
                LblMsg.Text = "El horario del veterinario se guardo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
                //showVeterinaryHours();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un horario de veterinario para actualizar
            if (string.IsNullOrEmpty(HFVeterinaryHoursID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un horario de veterinario para actualizar.";
                return;
            }
            _hor_vet_id = Convert.ToInt32(HFVeterinaryHoursID.Value);
            _start_date = DateTime.Parse(TBStart_date.Text);
            _end_date = DateTime.Parse(TBEnd_date.Text);

            _start_time = TimeSpan.Parse(TBStart_time.Text); // Actualizar la hora de inicio
            _final_time = TimeSpan.Parse(TBFinal_time.Text); // Actualizar la hora final
            _fkVeterinarian = Convert.ToInt32(DDLVeterinarian.SelectedValue);

            executed = objVeh.updateVeterinaryHours(_hor_vet_id, _start_date, _end_date, _start_time, _final_time, _fkVeterinarian);

            if (executed)
            {
                LblMsg.Text = "El horario del veterinario se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }
    }
}