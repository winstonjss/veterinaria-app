using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class OfficeLog
    {
        OfficeDat objOfi = new OfficeDat();

        //Metodo para mostrar todos los Consultorios
        public DataSet showOffice()
        {
           return objOfi.showOffice();
        }

        //Metodo para mostrar solo los Consultorios
        public DataSet showOfficeDDL()
        {
            return objOfi.showOfficeDDL();
        }

        //Metodo para guardar un consultorio
        public bool saveOffice(string _num_consultorio)
        {
            return objOfi.saveOffice(_num_consultorio);
        }

        //Metodo para actualizar un consultorio
        public bool updateOffice(int _id, string _num_consultorio)
        {
            return objOfi.updateOffice(_id, _num_consultorio);
        }

        //Metodo para borrar un consultorio
        public bool deleteOffice(int _id)
        {
           return objOfi.deleteOffice(_id);
        }
    }
}