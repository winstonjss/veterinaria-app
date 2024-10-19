using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class UsersDat
    {
        Persistence objPer = new Persistence();
        //Metodo para mostrar todos los Usuarios
        public DataSet showUsers()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUser";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para mostrar unicamente el ID y el documento del usuario
        public DataSet showUsersDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectUserDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }

        //Metodo para guardar un Usuario
        public bool saveUser(string _documento, string _correo, string _contrasena, string _salt,
            string _estado, DateTime _fecha_creación, int _rol_id, int _tipo_documento_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_documento", MySqlDbType.VarString).Value = _documento;
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = _correo;
            objSelectCmd.Parameters.Add("p_contrasena", MySqlDbType.VarString).Value = _contrasena;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarString).Value = _estado;
            objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.DateTime).Value = _fecha_creación;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_tipo_documento_id", MySqlDbType.Int32).Value = _tipo_documento_id;


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

        //Metodo para Actualizar un usuario 
        public bool UpdateUser(int _usu_id, string _documento, string _correo, string _contrasena, string _salt,
            string _estado, DateTime _fecha_creación, int _rol_id, int _tipo_documento_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_usu_id", MySqlDbType.Int32).Value = _usu_id;
            objSelectCmd.Parameters.Add("p_documento", MySqlDbType.VarString).Value = _documento;
            objSelectCmd.Parameters.Add("p_correo", MySqlDbType.VarString).Value = _correo;
            objSelectCmd.Parameters.Add("p_contrasena", MySqlDbType.VarString).Value = _contrasena;
            objSelectCmd.Parameters.Add("p_salt", MySqlDbType.VarString).Value = _salt;
            objSelectCmd.Parameters.Add("p_estado", MySqlDbType.VarString).Value = _estado;
            objSelectCmd.Parameters.Add("p_fecha_creacion", MySqlDbType.DateTime).Value = _fecha_creación;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_tipo_documento_id", MySqlDbType.Int32).Value = _tipo_documento_id;


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

        //Metodo para eliminar un Usuario
        public bool deleteUser(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteUser"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_usu_id", MySqlDbType.Int32).Value = _id;

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