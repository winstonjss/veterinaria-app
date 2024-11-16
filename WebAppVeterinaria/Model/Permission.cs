using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class Permission
    {
        private int _id;
        private string _nombre;
        private string _descripcion;

        public int Id { get => _id; set => _id = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Descripcion { get => _descripcion; set => _descripcion = value; }

        public Permission(int id, string nombre, string descripcion)
        {
            _id = id;
            _nombre = nombre;
            _descripcion = descripcion;
        }
    }
}