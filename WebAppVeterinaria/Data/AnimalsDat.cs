using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data
{
    public class AnimalsDat
    {
        // Se crea una instancia de la clase Persistence para manejar la conexión a la base de datos.
        Persistence objPer = new Persistence();


        // METODO PARA MOSTRAR LOS ANIMALES DESDE LA BASE DE DATOS.
        public DataSet showAnimals()
        {
            // Se crea un adaptador de datos para MySQL.
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();

            // Se crea un DataSet para almacenar los resultados de la consulta.
            DataSet objData = new DataSet();

            // Se crea un comando MySQL para seleccionar los animales utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();

            // Se establece la conexión del comando utilizando el método openConnection() de Persistence.
            objSelectCmd.Connection = objPer.openConnection();

            // Se especifica el nombre del procedimiento almacenado a ejecutar.
            objSelectCmd.CommandText = "spSelectAnimals";

            // Se indica que se trata de un procedimiento almacenado.
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se establece el comando de selección del adaptador de datos.
            objAdapter.SelectCommand = objSelectCmd;

            // Se llena el DataSet con los resultados de la consulta.
            objAdapter.Fill(objData);

            // Se cierra la conexión después de obtener los datos.
            objPer.closeConnection();

            // Se devuelve el DataSet que contiene los animales.
            return objData;
        }


        //METODO PARA MOSTRAR UNICAMENTE EL ID Y LA DESCRIPCION  -  (ES NECESARIO EL DDL PORQUE LA TABLA TIENE LLAVE FORANEA)
        public DataSet showAnimalsDDL()
        {
            MySqlDataAdapter objAdapter = new MySqlDataAdapter();
            DataSet objData = new DataSet();

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spSelectAnimalsDDL";
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand = objSelectCmd;
            objAdapter.Fill(objData);
            objPer.closeConnection();
            return objData;
        }


        //METODO PARA GUARDAR UN NUEVO ANIMAL
        public bool saveAnimals(string _name, string _species, string _race, DateTime _date_birth, string _sex, float _weight, string _color, int _fkOwner)
        {
            // Se inicializa una variable para indicar si la operación se ejecutó correctamente.
            bool executed = false;
            int row;// Variable para almacenar el número de filas afectadas por la operación.

            // Se crea un comando MySQL para insertar un nuevo animal utilizando un procedimiento almacenado.
            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spInsertAnimals"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del animal.
            objSelectCmd.Parameters.Add("p_name", MySqlDbType.VarString).Value = _name;
            objSelectCmd.Parameters.Add("p_species", MySqlDbType.VarString).Value = _species;
            objSelectCmd.Parameters.Add("p_race", MySqlDbType.VarString).Value = _race;
            objSelectCmd.Parameters.Add("p_date_birth", MySqlDbType.Date).Value = _date_birth;
            objSelectCmd.Parameters.Add("p_sex", MySqlDbType.VarString).Value = _sex;
            objSelectCmd.Parameters.Add("p_weight", MySqlDbType.Float).Value = _weight;
            objSelectCmd.Parameters.Add("p_color", MySqlDbType.VarString).Value = _color;
            objSelectCmd.Parameters.Add("p_fkowner", MySqlDbType.Int32).Value = _fkOwner;

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


        //METODO PARA ACTUALIZAR UN ANIMAL
        public bool updateAnimals(int _anim_id, string _name, string _species, string _race, DateTime _date_birth, string _sex, float _weight, string _color, int _fkOwner)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spUpdateAnimals"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;

            // Se agregan parámetros al comando para pasar los valores del animal.
            objSelectCmd.Parameters.Add("p_anim_id", MySqlDbType.Int32).Value = _anim_id;
            objSelectCmd.Parameters.Add("p_name", MySqlDbType.VarString).Value = _name;
            objSelectCmd.Parameters.Add("p_species", MySqlDbType.VarString).Value = _species;
            objSelectCmd.Parameters.Add("p_race", MySqlDbType.VarString).Value = _race;
            objSelectCmd.Parameters.Add("p_date_birth", MySqlDbType.Date).Value = _date_birth;
            objSelectCmd.Parameters.Add("p_sex", MySqlDbType.VarString).Value = _sex;
            objSelectCmd.Parameters.Add("p_weight", MySqlDbType.Float).Value = _weight;
            objSelectCmd.Parameters.Add("p_color", MySqlDbType.VarString).Value = _color;
            objSelectCmd.Parameters.Add("p_fkowner", MySqlDbType.Int32).Value = _fkOwner;

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


        //METODO PARA BORRAR UN ANIMAL
        public bool deleteAnimals(int _anim_id)
        {
            bool executed = false;
            int row;

            MySqlCommand objSelectCmd = new MySqlCommand();
            objSelectCmd.Connection = objPer.openConnection();
            objSelectCmd.CommandText = "spDeleteAnimals"; //nombre del procedimiento almacenado
            objSelectCmd.CommandType = CommandType.StoredProcedure;
            objSelectCmd.Parameters.Add("p_anim_id", MySqlDbType.Int32).Value = _anim_id;

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