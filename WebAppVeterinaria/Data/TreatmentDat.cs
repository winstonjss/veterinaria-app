using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class TreatmentDat
    {
        Persistence objPer = new Persistence();

        // Método para mostrar todos los tratamientos
        public DataSet showTratamientos(int _idTreatment)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTreatmentId"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_trat_id", MySqlDbType.Int32).Value = _idTreatment;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        // Método para guardar un tratamiento
        public bool saveTratamiento(string _nombre, string _descripcion, DateTime _fechaInicio, DateTime _fechaFin, int _diagnosticoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertTreatment"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_trat_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_trat_descripcion", MySqlDbType.Text).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_trat_fecha_inicio", MySqlDbType.Date).Value = _fechaInicio;
            objSelectCmd.Parameters.Add("p_trat_fecha_fin", MySqlDbType.Date).Value = _fechaFin;
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

        // Método para actualizar un tratamiento
        public bool updateTratamiento(int _id, string _nombre, string _descripcion, DateTime _fechaInicio, DateTime _fechaFin, int _diagnosticoId)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateTreatment"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_trat_id", MySqlDbType.Int32).Value = _id;
            objSelectCmd.Parameters.Add("p_trat_nombre", MySqlDbType.VarString).Value = _nombre;
            objSelectCmd.Parameters.Add("p_trat_descripcion", MySqlDbType.Text).Value = _descripcion;
            objSelectCmd.Parameters.Add("p_trat_fecha_inicio", MySqlDbType.Date).Value = _fechaInicio;
            objSelectCmd.Parameters.Add("p_trat_fecha_fin", MySqlDbType.Date).Value = _fechaFin;
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

        // Método para borrar un tratamiento
        public bool deleteTratamiento(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteTreatment"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_trat_id", MySqlDbType.Int32).Value = _id;

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
        public DataSet showTreatmentALL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectTreatment";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

    }
}
