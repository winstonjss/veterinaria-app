using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class VeterinarianDat
    {
        // Se crea una instancia de la clase Persistence para manejar la conexión a la base de datos.
        Persistence objPer = new Persistence();


        // METODO PARA MOSTRAR LOS VETERINARIOS DESDE LA BASE DE DATOS.
        public DataSet showVeterinarian()
        {
            // Se crea un adaptador de datos para MySQL.
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();

            // Se crea un DataSet para almacenar los resultados de la consulta.
            DataSet objData = new DataSet();

            // Se crea un comando MySQL para seleccionar los veterinarios utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();

            // Se establece la conexión del comando utilizando el método openConnection() de Persistence.
            objSelectCmd.Connection = objPer.openConnection();

            // Se especifica el nombre del procedimiento almacenado a ejecutar.
            objSelectCmd.CommandText = "spSelectVeterinarian";

            // Se indica que se trata de un procedimiento almacenado.
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se establece el comando de selección del adaptador de datos.
            objAdapter.SelectCommand = objSelectCmd;

            // Se llena el DataSet con los resultados de la consulta.
            objAdapter.Fill(objData);

            // Se cierra la conexión después de obtener los datos.
            objPer.closeConnection();

            // Se devuelve el DataSet que contiene los veterinarios.
            return objData;
        }


        //METODO PARA GUARDAR UN NUEVO VETERINARIO
        public bool saveVeterinarian(string _name, string _phone, int _fkUsers, int _fkOffice)
        {
            // Se inicializa una variable para indicar si la operación se ejecutó correctamente.
            bool executed = false;
            int row;// Variable para almacenar el número de filas afectadas por la operación.

            // Se crea un comando MySQL para insertar un nuevo veterinario utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertVeterinarian"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del veterinario.
            objSelectCmd.Parameters.Add("p_name", MySqlDbType.VarString).Value = _name;
            objSelectCmd.Parameters.Add("p_phone", MySqlDbType.VarString).Value = _phone;
            objSelectCmd.Parameters.Add("p_fkusers", MySqlDbType.Int32).Value = _fkUsers;
            objSelectCmd.Parameters.Add("p_fkoffice", MySqlDbType.Int32).Value = _fkOffice;

            try
            {
                // Se ejecuta el comando y se obtiene el número de filas afectadas.
                row = objSelectCmd.ExecuteNonQuery();

                // Si se inserta una fila correctamente, se establece executed a true.
                if (row == 1)
                {
                    executed = true;
                }
            }
            catch (Exception e)
            {
                // Si ocurre un error durante la ejecución del comando, se muestra en la consola.
                Console.WriteLine("Error " + e.ToString());
            }
            objPer.closeConnection();
            // Se devuelve el valor de executed para indicar si la operación se ejecutó correctamente.
            return executed;
        }


        //METODO PARA ACTUALIZAR UN VETERINARIO
        public bool updateVeterinarian(int _vet_id, string _name, string _phone, int _fkUsers, int _fkOffice)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateVeterinarian"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del veterinario.
            objSelectCmd.Parameters.Add("p_vet_id", MySqlDbType.Int32).Value = _vet_id;
            objSelectCmd.Parameters.Add("p_name", MySqlDbType.VarString).Value = _name;
            objSelectCmd.Parameters.Add("p_phone", MySqlDbType.VarString).Value = _phone;
            objSelectCmd.Parameters.Add("p_fkusers", MySqlDbType.Int32).Value = _fkUsers;
            objSelectCmd.Parameters.Add("p_fkoffice", MySqlDbType.Int32).Value = _fkOffice;

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


        //METODO PARA BORRAR UN VETERINARIO
        public bool deleteVeterinarian(int _vet_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteVeterinarian"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_vet_id", MySqlDbType.Int32).Value = _vet_id;

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