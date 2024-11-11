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
        public DataSet showRolesPermisos()
        {
            return objRolPer.showRolesPermisos();
        }

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

        //Metodo para guardar los roles y permisos 
        public bool saveRolesPermisos(int _rol_id, int _permiso_id, DateTime _p_date)
        {
            
            return objRolPer.saveRolesPermisos(_rol_id,_permiso_id,_p_date);
        }


        //Metodo para actualizar rol y permiso
        public bool updateRolesPermisos(int _p_rol_permiso, int _p_fkrol, int _p_fkpermiso, DateTime _p_date)
        {
            return objRolPer.updateRolesPermisos(_p_rol_permiso,_p_fkrol,_p_fkpermiso,_p_date) ;
        }

        //Metodo para borrar un Rol-Permiso
        public bool deleteRolesPermision(int _id)
        {
            return objRolPer.deleteRolesPermision(_id);
        }
    }
}