using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class User
    {
        private string _correo;
        private string _contrasena;
        private string _salt;
        private string _state;
        //privat string _nameRol;
        //privat int _idPer;

        private Rol _rol;
        private List<Permission> _permisos;

        public string Correo { get => _correo; set => _correo = value; }
        public string Contrasena { get => _contrasena; set => _contrasena = value; }
        public string Salt { get => _salt; set => _salt = value; }
        public string State { get => _state; set => _state = value; }
        public Rol Rol { get => _rol; set => _rol = value; }
        public List<Permission> Permisos { get => _permisos; set => _permisos = value; }


        public User(string correo, string contrasena, string salt, string state, Rol rol, List<Permission> permisos)
        {
            _correo = correo;
            _contrasena = contrasena;
            _salt = salt;
            _state = state;
            _rol = rol;
            _permisos = permisos;
        }

        public User()
        {
            _permisos = new List<Permission>(); // Inicializa lista vacía
        }
    }
}