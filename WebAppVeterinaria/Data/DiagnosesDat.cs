using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class DiagnosesDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los diagnósticos
        public DataSet showDiagnosticos()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectDiagnosticos"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un diagnóstico
        public bool saveDiagnostico(string _clasificacion, string _codigo, int _anamnesisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertarDiagnostico"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_clasificacion", MySqlDbType.VarString).Value = _clasificacion;
            objSelectCmd.Parameters.Add("p_cod", MySqlDbType.VarString).Value = _codigo;
            objSelectCmd.Parameters.Add("p_anam_id", MySqlDbType.Int32).Value = _anamnesisId;

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

        // Método para actualizar un diagnóstico
        public bool updateDiagnostico(int _id, string _clasificacion, string _codigo, int _anamnesisId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateDiagnostico"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_clasificacion", MySqlDbType.VarString).Value = _clasificacion;
            objSelectCmd.Parameters.Add("p_cod", MySqlDbType.VarString).Value = _codigo;
            objSelectCmd.Parameters.Add("p_anam_id", MySqlDbType.Int32).Value = _anamnesisId;

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

        // Método para borrar un diagnóstico
        public bool deleteDiagnostico(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteDiagnostico"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_diag_id", MySqlDbType.Int32).Value = _id;

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
}