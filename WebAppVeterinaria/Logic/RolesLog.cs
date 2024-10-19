using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class RolesLog
    {
        RolesDat objRol = new RolesDat();

        //Metodo para mostrar todos los Roles
        public DataSet showRoles()
        {
             return objRol.showRoles();
        }

        //Metodo para mostrar Unicamente el ID y el nombre del rol 
        public DataSet showRolesDDL()
        {
            return objRol.showRolesDDL();
        }

        //Metodo para guardar un Rol
        public bool saveRoles(string _nombre, string _descripcion)
        {
            return objRol.saveRoles(_nombre, _descripcion);

        }

        //Metodo para actualizar un Rol
        public bool updateRol(int _id, string _nombre, string _descripcion)
        {
            return objRol.updateRol(_id, _nombre, _descripcion)
        }

        //Metodo para eliminar un Rol
        public bool deletePermision(int _id)
        {
            return objRol.deletePermision(_id);
        }
    }