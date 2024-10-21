using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Data;
using System.Data.SqlClient;
using System.Runtime.Remoting;

namespace Logic
{
    public class Roles_PermissionLog
    {
        Roles_PermissionDat objRolPer = new Roles_PermissionDat();

        //Metodo para mostrar Permisos por Rol
        public DataSet showPermissionByRol(int _rol_id)
        {
            return objRolPer.showPermissionByRol(_rol_id);
        }

        //Metodo para mostrar Roles por permiso
        public DataSet showRolByPermission(int _permiso_id)
        {
            return objRolPer.showRolByPermission(_permiso_id);
        }

        public bool saveRolesPermisos(int _rol_id, int _permiso_id)
        {
            return objRolPer.saveRolesPermisos(_rol_id, _permiso_id);
        }

        //Metodo para actualizar rol y permiso
        public bool updateRolesPermisos(int _old_rol_id, int _old_permiso_id, int _new_rol_id,
            int _new_permiso_id)
        {
            return objRolPer.updateRolesPermisos(_old_rol_id, _old_permiso_id, _new_rol_id,
             _new_permiso_id);
        }

        //Metodo para borrar un Rol-Permiso
        public bool deleteRolesPermision(int _rol_id, int _permiso_id)
        {
           return objRolPer.deleteRolesPermision(_rol_id,_permiso_id);
        }
    }
}