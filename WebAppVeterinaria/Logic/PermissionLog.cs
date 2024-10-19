using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using System.Data.SqlClient;

namespace Logic
{
    public class PermissionLog
    {
        PermissionDat objPer = new PermissionDat();

        //Metodo para mostrar todos los Permisos
        public DataSet showPermission()
        {
            return objPer.showPermission();
        }

        //Metodo para guardar un Permiso
        public bool savePermission(string _nombre, string _descripcion)
        {
            return objPer.savePermission(_nombre, _descripcion);

        }

        //Metodo para actualizar un Permiso
        public bool updatePermision(int _id, string _nombre, string _descripcion)
        {
            return objPer.updatePermision(_id, _nombre, _descripcion);

        }

        //Metodo para borrar un Permiso
        public bool deletePermision(int _id)
        {
            return objPer.deletePermision(_id);

        }
    }
}