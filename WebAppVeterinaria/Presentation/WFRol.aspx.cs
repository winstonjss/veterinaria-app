using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFRol : System.Web.UI.Page
    {
        RolesLog objRol = new RolesLog();
        private int _id;
        private string _nombre, _descripcion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        //Metodo para mostrar todos los Roles
        [WebMethod]
        public static object ListRoles()
        {
            RolesLog objRol = new RolesLog();

            // Se obtiene un DataSet que contiene la lista de Roles desde la base de datos.
            var dataSet = objRol.showRoles();

            // Se crea una lista para almacenar los Roles que se van a devolver.
            var rolesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un consultorio).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                rolesList.Add(new
                {
                    ID = row["rol_id"],
                    Nombre = row["rol_nombre"],
                    Descripcion = row["rol_descripcion"],
                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = rolesList };
        }
        [WebMethod]

        public static bool deleteRol(int id)
        {
            // Crear una instancia de la clase de lógica de rol
            RolesLog objrol = new RolesLog();

            // Invocar al método para eliminar el Rol y devolver el resultado
            return objrol.deleteRol(id);
        }

        private void clear()
        {
            HFRol.Value = "";
            TBRol_nombre.Text = "";
            TBRol_descripcion.Text = "";

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _nombre = TBRol_nombre.Text;
            _descripcion = TBRol_descripcion.Text;
            executed = objRol.saveRoles(_nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un Rol  para actualizar
            if (string.IsNullOrEmpty(HFRol.Value))
            {
                LblMsg.Text = "No se ha seleccionado un Rol para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFRol.Value);
            _nombre = TBRol_nombre.Text;
            _descripcion = TBRol_descripcion.Text;
            executed = objRol.updateRol(_id, _nombre, _descripcion);

            if (executed)
            {
                LblMsg.Text = "Se ACTUALIZÓ exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al ACTUALIZAR ";
            }
        }


    }
}