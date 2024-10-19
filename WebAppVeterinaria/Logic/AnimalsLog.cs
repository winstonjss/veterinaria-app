using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class AnimalsLog
    {
        AnimalsDat objAni = new AnimalsDat();

        //Metodo para mostrar todos los Animales
        public DataSet showAnimals()
        {
            return objAni.showAnimals();
        }


        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showAnimalsDDL()
        {
            return objAni.showAnimalsDDL();
        }


        //Metodo para guardar un nuevo Animal
        public bool saveAnimals(string _name, string _species, string _race, DateTime _date_birth, string _sex, float _weight, string _color, int _fkOwner)
        {
            return objAni.saveAnimals(_name, _species, _race, _date_birth, _sex, _weight, _color, _fkOwner);
        }


        //Metodo para actualizar un Animal
        public bool updateAnimals(int _anim_id, string _name, string _species, string _race, DateTime _date_birth, string _sex, float _weight, string _color, int _fkOwner)
        {
            return objAni.updateAnimals(_anim_id, _name, _species, _race, _date_birth, _sex, _weight, _color, _fkOwner);
        }


        //Metodo para borrar un Animal
        public bool deleteAnimals(int _anim_id)
        {
            return objAni.deleteAnimals(_anim_id);
        }
    }
}
