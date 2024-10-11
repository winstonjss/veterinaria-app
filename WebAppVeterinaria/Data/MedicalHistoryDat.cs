using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class MedicalHistory
    {
        Persistence objPer = new Persistence();        
        public DataSet showMedicalHistoryByAnimal(int _idAnimal)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectHistoriaClinicaAnimal";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_anim_id", MySqlDbType.Int32).Value = _idAnimal;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public bool saveMedicalHistoryByDateId(int _dateId)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertHistoriaClinica"; //nombre del proce dimiento almacenado 
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

        public bool saveMedicalHistoryByDate(DateTime _date)
        {
            bool executed = false;
            int row;
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertHistoriaClinicaPorFecha"; //nombre del proce dimiento almacenado 
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_fecha", MySqlDbTy.Date).Value = _date;
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