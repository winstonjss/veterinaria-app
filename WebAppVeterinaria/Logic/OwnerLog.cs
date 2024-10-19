using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class OwnerLog
    {
        OwnerDat objOwn = new OwnerDat();

        //Metodo para mostrar todos los Propietarios
        public DataSet showOwner()
        {
            return objOwn.showOwner();
        }


        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showOwnerDDL()
        {
            return objOwn.showOwnerDDL();
        }


        //Metodo para guardar un nuevo Propietario
        public bool saveOwner(string _name, string _phone, int _fkUsers)
        {
            return objOwn.saveOwner(_name, _phone, _fkUsers);
        }


        //Metodo para actualizar un Propietario
        public bool updateOwner(int _pro_id, string _name, string _phone, int _fkUsers)
        {
            return objOwn.updateOwner(_pro_id, _name, _phone, _fkUsers);
        }


        //Metodo para borrar un Propietario
        public bool deleteOwner(int _pro_id)
        {
            return objOwn.deleteOwner(_pro_id);
        }
    }
}