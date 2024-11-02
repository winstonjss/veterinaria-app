using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class AppointmentsDat
    {
        Persistence objPer = new Persistence();

        public bool saveDate(int _animalId, int _veterinarianId,
            DateTime _date, TimeSpan _startHour, TimeSpan _finalHour)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertCita";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_animal_id", MySqlDbType.Int32).Value = _animalId;
            objSelectCmd.Parameters.Add("p_veterinario_id", MySqlDbType.Int32).Value = _veterinarianId;
            objSelectCmd.Parameters.Add("p_cit_fecha", MySqlDbType.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_cit_hora_inicio", MySqlDbType.Time).Value = _startHour;
            objSelectCmd.Parameters.Add("p_cit_hora_fin", MySqlDbType.Time).Value = _finalHour;
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

        public bool updateDate(int _dateId, int _animalId, int _veterinarianId,
            DateTime _date, TimeSpan _startHour, TimeSpan _finalHour)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateCita"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cit_id", MySqlDbType.Int32).Value = _dateId;
            objSelectCmd.Parameters.Add("p_animal_id", MySqlDbType.Int32).Value = _animalId;
            objSelectCmd.Parameters.Add("p_veterinario_id", MySqlDbType.VarString).Value = _veterinarianId;
            objSelectCmd.Parameters.Add("p_cit_fecha", MySqlDbType.Date).Value = _date;
            objSelectCmd.Parameters.Add("p_cit_hora_inicio", MySqlDbType.Time).Value = _startHour;
            objSelectCmd.Parameters.Add("p_cit_hora_fin", MySqlDbType.Time).Value = _finalHour;
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

        public DataSet showDatesFilterbyAnimalAndRangeDate(int _idAnimal, DateTime _startDate,
            DateTime _finalDate)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitasAnimalRangoFechas";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_animal_id", MySqlDbType.Int32).Value = _idAnimal;
            objSelectCmd.Parameters.Add("p_fecha_inicio", MySqlDbType.Date).Value = _startDate;
            objSelectCmd.Parameters.Add("p_fecha_fin", MySqlDbType.Date).Value = _finalDate;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showCitasAll()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitasAll";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public DataSet showCitasDDl()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectCitasDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
        public bool deleteDate(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteCita"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cit_id", MySqlDbType.Int32).Value = _id;

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