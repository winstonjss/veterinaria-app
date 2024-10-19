using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class VeterinaryHoursLog
    {
        VeterinaryHoursDat objVeh = new VeterinaryHoursDat();

        //Metodo para mostrar todos los horarios del veterinario
        public DataSet showVeterinaryHours()
        {
            return objVeh.showVeterinaryHours();
        }


        //Metodo para guardar un nuevo horarios del veterinario
        public bool saveVeterinaryHours(DateTime _start_date, DateTime _end_date, TimeSpan _start_time, TimeSpan _final_time, int _fkVeterinarian)
        {
            return objVeh.saveVeterinaryHours(_start_date, _end_date, _start_time, _final_time, _fkVeterinarian);
        }


        //Metodo para actualizar un horarios del veterinario
        public bool updateVeterinaryHours(int _hor_vet_id, DateTime _start_date, DateTime _end_date, TimeSpan _start_time, TimeSpan _final_time, int _fkVeterinarian)
        {
            return objVeh.updateVeterinaryHours(_hor_vet_id, _start_date, _end_date, _start_time, _final_time, _fkVeterinarian);
        }


        //Metodo para borrar un horarios del veterinario
        public bool deleteVeterinaryHours(int _hor_vet_id)
        {
            return objVeh.deleteVeterinaryHours(_hor_vet_id);
        }
    }
}