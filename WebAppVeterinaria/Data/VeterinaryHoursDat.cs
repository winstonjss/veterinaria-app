using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class VeterinaryHoursDat
    {
        // Se crea una instancia de la clase Persistence para manejar la conexión a la base de datos.
        Persistence objPer = new Persistence();


        // METODO PARA MOSTRAR LOS HORARIOS DEL VETERINARIO DESDE LA BASE DE DATOS.
        public DataSet showVeterinaryHours()
        {
            // Se crea un adaptador de datos para MySQL.
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();

            // Se crea un DataSet para almacenar los resultados de la consulta.
            DataSet objData = new DataSet();

            // Se crea un comando MySQL para seleccionar los horarios del veterinario utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();

            // Se establece la conexión del comando utilizando el método openConnection() de Persistence.
            objSelectCmd.Connection = objPer.openConnection();

            // Se especifica el nombre del procedimiento almacenado a ejecutar.
            objSelectCmd.CommandText = "spSelectVeterinaryHours";

            // Se indica que se trata de un procedimiento almacenado.
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se establece el comando de selección del adaptador de datos.
            objAdapter.SelectCommand = objSelectCmd;

            // Se llena el DataSet con los resultados de la consulta.
            objAdapter.Fill(objData);

            // Se cierra la conexión después de obtener los datos.
            objPer.closeConnection();

            // Se devuelve el DataSet que contiene los horarios del veterinario.
            return objData;
        }


        //METODO PARA GUARDAR UN NUEVO HORARIO DEL VETERINARIO
        public bool saveVeterinaryHours(DateTime _start_date, DateTime _end_date, TimeSpan _start_time, TimeSpan _final_time, int _fkVeterinarian)
        {
            // Se inicializa una variable para indicar si la operación se ejecutó correctamente.
            bool executed = false;
            int row;// Variable para almacenar el número de filas afectadas por la operación.

            // Se crea un comando MySQL para insertar un nuevo horario del veterinario utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertVeterinaryHours"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de los horarios del veterinario.
            objSelectCmd.Parameters.Add("p_start_date", MySqlDbType.Date).Value = _start_date;
            objSelectCmd.Parameters.Add("p_end_date", MySqlDbType.Date).Value = _end_date;
            objSelectCmd.Parameters.Add("p_start_time", MySqlDbType.Time).Value = _start_time;
            objSelectCmd.Parameters.Add("p_final_time", MySqlDbType.Time).Value = _final_time;
            objSelectCmd.Parameters.Add("p_fkveterinarian", MySqlDbType.Int32).Value = _fkVeterinarian;

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


        //METODO PARA ACTUALIZAR UN HORARIO DEL VETERINARIO
        public bool updateVeterinaryHours(int _hor_vet_id, DateTime _start_date, DateTime _end_date, TimeSpan _start_time, TimeSpan _final_time, int _fkVeterinarian)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateVeterinaryHours"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores de los horarios del veterinario.
            objSelectCmd.Parameters.Add("p_hor_vet_id", MySqlDbType.Int32).Value = _hor_vet_id;
            objSelectCmd.Parameters.Add("p_start_date", MySqlDbType.Date).Value = _start_date;
            objSelectCmd.Parameters.Add("p_end_date", MySqlDbType.Date).Value = _end_date;
            objSelectCmd.Parameters.Add("p_start_time", MySqlDbType.Time).Value = _start_time;
            objSelectCmd.Parameters.Add("p_final_time", MySqlDbType.Time).Value = _final_time;
            objSelectCmd.Parameters.Add("p_fkveterinarian", MySqlDbType.Int32).Value = _fkVeterinarian;

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
        public bool deleteVeterinaryHours(int _hor_vet_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteVeterinaryHours"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_hor_vet_id", MySqlDbType.Int32).Value = _hor_vet_id;

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