using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class MedicalHistory
    {
        Persistence objPer = new Persistence();

        public bool saveMedicalHistoryByDateId(int _dateId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertHistoriaClinicaByCita"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_cit_id", MySqlDbTy.Int32).Value = _dateId;
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

        public DataSet showMedicalHistoryByAnimal(int _idAnimal)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHistoriaClinicaByAnimal";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_anim_id", MySqlDbType.Int32).Value = _idAnimal;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool deleteMedicalHistory(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteHistoriaClinica"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_histo_cli_id", MySqlDbType.Int32).Value = _id;

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

        public bool updateMedicalHistory(int _medicalHistoryId, DateTime _medicalHistoryDate,
            int _dateId
            )
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateHistoriaClinica"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_histo_cli_id", MySqlDbType.Int32).Value = _medicalHistoryId;
            objSelectCmd.Parameters.Add("p_histo_cli_fecha", MySqlDbTy.Date).Value = _medicalHistoryDate;
            objSelectCmd.Parameters.Add("p_cit_id", MySqlDbType.Int32).Value = _dateId;            
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
        public DataSet showMedicalHistoryAll()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHistoriaClinicaAll";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }
    }
}