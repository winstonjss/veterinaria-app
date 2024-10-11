using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Data
{
    public class Roles_PermissionDat
    {
        Persistence objPer = new Persistence();


        //Metodo para mostrar Permisos por Rol
        public DataSet showPermissionByRol(int _rol_id)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectPermisionByRol"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar el parámetro del permiso al comando
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;

            // Asignar el comando al adaptador
            objAdapter.SelectCommand = objSelectCmd;

            try
            {
                // Rellenar el DataSet con los resultados de la consulta
                objAdapter.Fill(objData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                objPer.closeConnection();
            }

            return objData;
        }

        //Metodo para mostrar Roles por permiso
        public DataSet showRolByPermission(int _permiso_id)
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectRolesByPermiso"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Agregar el parámetro del permiso al comando
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;

            // Asignar el comando al adaptador
            objAdapter.SelectCommand = objSelectCmd;

            try
            {
                // Rellenar el DataSet con los resultados de la consulta
                objAdapter.Fill(objData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
            }
            finally
            {
                // Asegurarse de cerrar la conexión
                objPer.closeConnection();
            }

            return objData;
        }


        //public DataSet showPermissionById()
        //{
        //    MySqlDataAdapter objAdapter = new MySqlDataAdapter();
        //    DataSet objData = new DataSet();

        //    MySqlCommand objSelectCmd = new MySqlCommand();
        //    objSelectCmd.Connection = objPer.openConnection();
        //    objSelectCmd.CommandText = "spSelectPermissionById";
        //    objSelectCmd.CommandType = CommandType.StoredProcedure;
        //    objAdapter.SelectCommand = objSelectCmd;
        //    objAdapter.Fill(objData);
        //    objPer.closeConnection();
        //    return objData;
        //}

        //Metodo para guardar rol y permiso
        public bool saveRolesPermisos(int _rol_id, int _permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertRole_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;


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

        //Metodo para actualizar rol y permiso
        public bool updateRolesPermisos(int _old_rol_id, int _old_permiso_id, int _new_rol_id,
            int _new_permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateRoles_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("old_rol_id", MySqlDbType.Int32).Value = _old_rol_id;
            objSelectCmd.Parameters.Add("old_permiso_id", MySqlDbType.Int32).Value = _old_permiso_id;
            objSelectCmd.Parameters.Add("new_rol_id", MySqlDbType.Int32).Value = _new_rol_id;
            objSelectCmd.Parameters.Add("new_permiso_id", MySqlDbType.Int32).Value = _new_permiso_id;

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

        //Metodo para borrar un Rol-Permiso
        public bool deleteRolesPermision(int _rol_id, int _permiso_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteRole_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;

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