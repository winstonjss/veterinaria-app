using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFTreatment : System.Web.UI.Page
    {
        TreatmentLog objTrea = new TreatmentLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkDiagnoses, _id ;
        private string _description, _name;
        private DateTime _startDate, _endDate;
        private bool executed = false;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                showDiagnosesDDL();



            }
        }

        [WebMethod]
        public static object ListTreatment() {
            TreatmentLog objTrea = new TreatmentLog();
            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objTrea.showTreatmentALL();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var treatmentList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                treatmentList.Add(new
                {
                    TreatmentId = row["trat_id"],
                    TreatmentName = row["trat_nombre"],
                    TreatmentDescription = row["trat_descripcion"],
                    TreatmentDateStart = Convert.ToDateTime(row["trat_fecha_inicio"]).ToString("yyyy-MM-dd"),
                    TreatmentDateEnd = Convert.ToDateTime(row["trat_fecha_fin"]).ToString("yyyy-MM-dd"),
                    FkDiagonoses = row["tbl_diagnosticos_diag_id"],
                    DiagonosesCode = row["diag_cod"]


                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = treatmentList };
        }


        private void showDiagnosesDDL()
        {
            DDLDiagonoses.DataSource = objDiag.showDiagnosesDLL();
            DDLDiagonoses.DataValueField = "diag_id";
            DDLDiagonoses.DataTextField = "diag_clasificacion";
            DDLDiagonoses.DataBind();
            DDLDiagonoses.Items.Insert(0, "seleccione");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _description = TBDescription.Text;
            _startDate = Convert.ToDateTime(TBStartDate.Text);
            _endDate = Convert.ToDateTime(TBEndDate.Text);
            executed = objTrea.saveTratamiento(_name, _description, _startDate, _endDate, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo el tratamiento";
                
            }
            else
            {
                lblMsg.Text = "error al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFTreatmentID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un Tratamiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFTreatmentID.Value);
            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _description = TBDescription.Text;
            _startDate = Convert.ToDateTime(TBStartDate.Text);
            _endDate = Convert.ToDateTime(TBEndDate.Text);
            executed = objTrea.updateTratamiento(_id, _name, _description, _startDate, _endDate, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo el tratamiento";

            }
            else
            {
                lblMsg.Text = "error al guardar";
            }
        }
        [WebMethod]
        public static bool DeleteTreatment(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            TreatmentLog objTrea = new TreatmentLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objTrea.deleteTratamiento(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFTreatmentID.Value = "";
            TBName.Text = "";
            TBDescription.Text = "";
            TBStartDate.Text = "";
            TBEndDate.Text = "";
            DDLDiagonoses.SelectedIndex = 0;
        
        
        }
    }
}
