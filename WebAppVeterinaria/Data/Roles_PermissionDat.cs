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

        //Metodo para mostrar Todos los roles y permisos
        public DataSet showRolesPermisos()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectRoles_Permission"; // Nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;


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


              //Metodo para guardar rol y permiso
        public bool saveRolesPermisos(int _rol_id, int _permiso_id, DateTime _p_date)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertRole_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_id", MySqlDbType.Int32).Value = _rol_id;
            objSelectCmd.Parameters.Add("p_permiso_id", MySqlDbType.Int32).Value = _permiso_id;
            objSelectCmd.Parameters.Add("p_date", MySqlDbType.DateTime).Value = _p_date;


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
        public bool updateRolesPermisos(int _p_rol_permiso, int _p_fkrol, int _p_fkpermiso, DateTime  _p_date )
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateRoles_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_rol_permiso", MySqlDbType.Int32).Value = _p_rol_permiso;
            objSelectCmd.Parameters.Add("p_fkrol", MySqlDbType.Int32).Value = _p_fkrol;
            objSelectCmd.Parameters.Add("p_fkpermiso", MySqlDbType.Int32).Value = _p_fkpermiso;
            objSelectCmd.Parameters.Add("p_date", MySqlDbType.DateTime).Value = _p_date;

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
        public bool deleteRolesPermision(int _id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteRole_Permission"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_id", MySqlDbType.Int32).Value = _id;
            

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