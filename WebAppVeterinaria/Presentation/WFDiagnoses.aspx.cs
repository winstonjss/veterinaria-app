using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFDiagnoses : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        DiagnosesLog objDiag = new DiagnosesLog();
        private int _fkAnemnesis, _id;
        private string _clasification, _code;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showAnamnesisDDL();

            }
        }


        [WebMethod]
        public static object ListDiagnoses()
        {
            DiagnosesLog objDiag = new DiagnosesLog();

            // Se obtiene un DataSet que contiene la lista de Diaguctos desde la base de datos.
            var dataSet = objDiag.showDiagnosticos();

            // Se crea una lista para almacenar los Diaguctos que se van a devolver.
            var DiagnosesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un Diagucto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                DiagnosesList.Add(new
                {
                    DiagnosesID = row["diag_id"],
                    Classification = row["diag_clasificacion"],
                    Code = row["diag_cod"],
                    FkAnamnesis = row["tbl_anamnesis_anam_id"],
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de Diagnosticos.
            return new { data = DiagnosesList };
        }




        private void showAnamnesisDDL()
        {
            DDLAnamnesis.DataSource = objAnam.showAnamnesisDDl();
            DDLAnamnesis.DataValueField = "anam_id";
            DDLAnamnesis.DataTextField = "anam_descripcion";
            DDLAnamnesis.DataBind();
            DDLAnamnesis.Items.Insert(0, "seleccione");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkAnemnesis = Convert.ToInt32(DDLAnamnesis.SelectedValue);

            _clasification = TBClassification.Text;
            _code = TBCode.Text;
            executed = objDiag.saveDiagnostico(_clasification,_code,_fkAnemnesis);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
                showAnamnesisDDL();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(HFDiagnosesID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFDiagnosesID.Value);
            _fkAnemnesis = Convert.ToInt32(DDLAnamnesis.SelectedValue);

            _clasification = TBClassification.Text;
            _code = TBCode.Text;
            executed = objDiag.updateDiagnostico(_id, _clasification, _code, _fkAnemnesis);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
                showAnamnesisDDL();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }           
        }
        [WebMethod]
        public static bool deleteDiagnostico(int id)
        {
            // Crear una instancia de la clase de lógica de productos
            DiagnosesLog objDiag = new DiagnosesLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objDiag.deleteDiagnostico(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFDiagnosesID.Value = "";
            TBClassification.Text = "";
            TBCode.Text = "";
            DDLAnamnesis.SelectedIndex = 0;
        }

    }
}
