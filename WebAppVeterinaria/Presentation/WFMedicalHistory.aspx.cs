using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
namespace Presentation
{
    public partial class WFMedicalHistory : System.Web.UI.Page
    {
        MedicalHistoryLog objMed = new MedicalHistoryLog();
        AppointmentsLog objApp = new AppointmentsLog();
        private int _id, _fkAppoitment;
        private bool executed = false;
        private DateTime _medicalHistoryDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showAppoitmentsDDL();
            }
        }

        [WebMethod]
        public static object ListMedicalHistory()
        {
            MedicalHistoryLog objMed = new MedicalHistoryLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objMed.showMedicalHistoryAll();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var medicalsHistoryList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                medicalsHistoryList.Add(new
                {
                    MedicalHistoryID = row["histo_cli_id"],
                    Date = Convert.ToDateTime(row["histo_cli_fecha"]).ToString("yyyy-MM-dd"),                    
                    Description = row["histo_cli_descripcion"],
                    FKAppoitment = row["tbl_citas_cit_id"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = medicalsHistoryList };
        }

        private void showAppoitmentsDDL()
        {
            DDLAppoitment.DataSource = objApp.showCitasDDl();
            DDLAppoitment.DataValueField = "cit_id";
            DDLAppoitment.DataTextField = "cit_fecha";
            DDLAppoitment.DataBind();
            DDLAppoitment.Items.Insert(0, "seleccione");

        }

        //eventos que se ejecutan cuando se da click en los botones
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkAppoitment = Convert.ToInt32(DDLAppoitment.SelectedValue);
            _medicalHistoryDate = CALMedicalHistory.SelectedDate;
            executed = objMed.saveMedicalHistoryByDateId(_fkAppoitment, _medicalHistoryDate);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";                
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFMedicalHistoryID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }

            _id = Convert.ToInt32(HFMedicalHistoryID.Value);
            _fkAppoitment = Convert.ToInt32(DDLAppoitment.SelectedValue);
            _medicalHistoryDate = CALMedicalHistory.SelectedDate;

            executed = objMed.updateMedicalHistory(_fkAppoitment, _medicalHistoryDate,
                _fkAppoitment);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        [WebMethod]
        public static bool DeleteMedicalHistory(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            MedicalHistoryLog objMed = new MedicalHistoryLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objMed.deleteMedicalHistory(id);
        }

        private void clear()
        {
            HFMedicalHistoryID.Value = "";            
            DDLAppoitment.SelectedIndex = 0;
        }
    }
}