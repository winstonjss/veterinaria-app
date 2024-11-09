using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFOffice : System.Web.UI.Page
    {
        OfficeLog objOfi = new OfficeLog();
        private int _id;
        private string _num_consultorio;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 


            }
        }

        //Metodo para mostrar todos tipos Consultorios
        [WebMethod]
        public static object ListOffice()
        {
            OfficeLog objofi = new OfficeLog();

            // Se obtiene un DataSet que contiene la lista de consultorios desde la base de datos.
            var dataSet = objofi.showOffice();

            // Se crea una lista para almacenar los consultorios que se van a devolver.
            var officeList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un consultorio).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                officeList.Add(new
                {
                    ID = row["con_id"],
                    Consultorio = row["con_num_consultorio"],

                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = officeList };
        }
        [WebMethod]

        public static bool deleteOffice(int id)
        {
            // Crear una instancia de la clase de lógica de consultorio 
            OfficeLog objOfi = new OfficeLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objOfi.deleteOffice(id);
        }

        private void clear()
        {
            HFOffice.Value = "";
            TBCon_num_consultorio.Text = "";

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _num_consultorio = TBCon_num_consultorio.Text;
            executed = objOfi.saveOffice(_num_consultorio);

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
            // Verifica si se ha seleccionado un consultorio  para actualizar
            if (string.IsNullOrEmpty(HFOffice.Value))
            {
                LblMsg.Text = "No se ha seleccionado un consultorio para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFOffice.Value);
            _num_consultorio = TBCon_num_consultorio.Text;
            executed = objOfi.updateOffice(_id, _num_consultorio);

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