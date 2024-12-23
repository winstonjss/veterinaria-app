using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using MySql.Data.MySqlClient;

namespace Data
{
    public class TokenDat
    {
        Persistence objPer = new Persistence();
     
        // Método para guardar un token
        public bool saveToken(string hash, string correo, DateTime _fechaInicio, DateTime _fechaFin)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertToken"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_token_hash", MySqlDbType.VarString).Value = hash;
            objSelectCmd.Parameters.Add("p_token_correo", MySqlDbType.Text).Value = correo;
            objSelectCmd.Parameters.Add("p_token_fecha_generacion", MySqlDbType.Timestamp).Value = _fechaInicio;
            objSelectCmd.Parameters.Add("p_token_fecha_vencimiento", MySqlDbType.Timestamp).Value = _fechaFin;            

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

        public DataSet showTokenByHash(string hash)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spShowTokenByHash"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objSelectCmd.Parameters.Add("p_token_hash", MySqlDbType.Text).Value = hash;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        public int validateTokenExpiration(string correo, DateTime fechaEntrada)
        {
            int totalUsers;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spValidateTokenExpiration";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            //Parametros de entrada
            objSelectCmd.Parameters.Add("p_token_hash", MySqlDbType.String).Value = correo;
            objSelectCmd.Parameters.Add("p_fecha_comparacion", MySqlDbType.Timestamp).Value = fechaEntrada;
            // Agregar el parámetro de salida
            objSelectCmd.Parameters.Add(new MySqlParameter("@p_is_expired", MySqlDbType.Int32));
            objSelectCmd.Parameters["@p_is_expired"].Direction = ParameterDirection.Output;

            // Ejecutar el comando
            objSelectCmd.ExecuteNonQuery();

            // Obtener el valor del parámetro de salida
            totalUsers = Convert.ToInt32(objSelectCmd.Parameters["@p_is_expired"].Value);
            objPer.closeConnection();
            return totalUsers;
        }

        public int validateEmail(string correo)
        {
            int totalUsers;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spVerificarCorreoExistente";
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            //Parametros de entrada
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.String).Value = correo;
            // Agregar el parámetro de salida
            objSelectCmd.Parameters.Add(new MySqlParameter("@p_existe", MySqlDbType.Int32));
            objSelectCmd.Parameters["@p_existe"].Direction = ParameterDirection.Output;

            // Ejecutar el comando
            objSelectCmd.ExecuteNonQuery();

            // Obtener el valor del parámetro de salida
            totalUsers = Convert.ToInt32(objSelectCmd.Parameters["@p_existe"].Value);
            objPer.closeConnection();
            return totalUsers;
        }

        public bool updatePassword(string correo, string salt, string password)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spActualizarContrasena"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del propietario.
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = correo;
            objSelectCmd.Parameters.Add("p_nueva_contrasena", MySqlDbType.Text).Value = password;
            objSelectCmd.Parameters.Add("p_nuevo_salt", MySqlDbType.Text).Value = salt;

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