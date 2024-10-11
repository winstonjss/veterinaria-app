using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class AppointmentsDat
    {
        Persistence objPer = new Persistence();
        public DataSet showDatesFilterbyAnimal(int _idAnimal, DateTime _startDate,
            DateTime _finalDate)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHistoriaClinicaAnimal";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_anim_id", MySqlDbType.Int32).Value = _idAnimal;
            objSelectCmd.Parameters.Add("p_fecha_inicio", MySqlDbType.Date).Value = _startDate;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.Date).Value = _finalDate;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showDatesFilterbyVeterinarian(string _documentNumber, DateTime _startDate,
            DateTime _finalDate)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitasVeterinarioRangoFechas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_veterinario_documento", MySqlDbType.VarString).Value = _documentNumber;
            objSelectCmd.Parameters.Add("p_fecha_inicio", MySqlDbType.Date).Value = _startDate;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.Date).Value = _finalDate;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showDatesFilterbyDate(DateTime _startDate, DateTime _finalDate)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitasRangoFechas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_fecha_inicio", MySqlDbType.Date).Value = _startDate;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.Date).Value = _finalDate;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool saveDate(int _animalId, string _documentNumberVeterinarian,
            DateTime _date, DateTime _startHour, DateTime _finalHour)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertCita"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_animal_id", MySqlDbTy.Int32).Value = _animalId;
            objSelectCmd.Parameters.Add("p_veterinario_documento", MySqlDbType.VarString).Value = _documentNumberVeterinarian;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbTy.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_hora_inicio", MySqlDbTy.Time).Value = _startHour;
            objSelectCmd.Parameters.Add("p_hora_fin", MySqlDbTy.Time).Value = _finalHour;
            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }

        public bool updateDate(int _dateId, int _animalId, string _documentNumberVeterinarian,
            DateTime _date, DateTime _startHour, DateTime _finalHour)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCita"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cita_id", MySqlDbType.Int32).Value = _dateId;
            objSelectCmd.Parameters.Add("p_animal_id", MySqlDbTy.Int32).Value = _animalId;
            objSelectCmd.Parameters.Add("p_veterinario_documento", MySqlDbType.VarString).Value = _documentNumberVeterinarian;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbTy.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_hora_inicio", MySqlDbTy.Time).Value = _startHour;
            objSelectCmd.Parameters.Add("p_hora_fin", MySqlDbTy.Time).Value = _finalHour;
            try
            {
                row = objSelectCmd.ExecuteNonQuery();
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            return executed;
        }        
    }
}