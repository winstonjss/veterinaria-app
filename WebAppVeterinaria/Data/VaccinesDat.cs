using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class VaccinesDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todas las vacunas
        public DataSet showVacunas()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectVacunas"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar una vacuna
        public bool saveVacuna(string _nombre, string _tipo, decimal _cantidad, int _diagnosticoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertVacuna"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_vac_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_vac_tipo", MySqlDbType.VarString).Value = _tipo;
            objSelectCmd.Parameters.Add("p_vac_cantidad", MySqlDbType.Decimal).Value = _cantidad;
            objSelectCmd.Parameters.Add("p_tbl_diagnosticos_diag_id", MySqlDbType.Int32).Value = _diagnosticoId;

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

        // Método para actualizar una vacuna
        public bool updateVacuna(int _id, string _nombre, string _tipo, decimal _cantidad, int _diagnosticoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateVacuna"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_vac_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_vac_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_vac_tipo", MySqlDbType.VarString).Value = _tipo;
            objSelectCmd.Parameters.Add("p_vac_cantidad", MySqlDbType.Decimal).Value = _cantidad;
            objSelectCmd.Parameters.Add("p_tbl_diagnosticos_diag_id", MySqlDbType.Int32).Value = _diagnosticoId;

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

        // Método para borrar una vacuna
        public bool deleteVacuna(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteVacuna"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_vac_id", MySqlDbType.Int32).Value = _id;

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