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
    public class UsersLog
    {
        UsersDat objUse = new UsersDat();

        //Metodo para mostrar todos los Usuarios
        public DataSet showUsers()
        {
            return objUse.showUsers();
        }

        //Metodo para mostrar unicamente el ID y el documento del usuario
        public DataSet showUsersDDL()
        {
           return objUse.showUsersDDL();
        }

        //Metodo para guardar un Usuario
        public bool saveUser(string _documento, string _correo, string _contrasena, string _salt,
            string _estado, DateTime _fecha_creación, int _rol_id, int _tipo_documento_id)
        {
            return objUse.saveUser(_documento, _correo, _contrasena, _salt,
            _estado, _fecha_creación, _rol_id, _tipo_documento_id);
        }

        //Metodo para Actualizar un usuario 
        public bool UpdateUser(int _usu_id, string _documento, string _correo, string _contrasena, string _salt,
            string _estado, DateTime _fecha_creación, int _rol_id, int _tipo_documento_id)
        {
            return objUse.UpdateUser(_usu_id, _documento, _correo, _contrasena, _salt,
              _estado, _fecha_creación, _rol_id, _tipo_documento_id);
        }

        //Metodo para eliminar un Usuario
        public bool deleteUser(int _id)
        {
            return objUse.deleteUser(_id);
        }
    }
}