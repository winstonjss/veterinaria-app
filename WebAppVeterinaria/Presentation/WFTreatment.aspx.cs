using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFTreatment : System.Web.UI.Page
    {
        TreatmentLog objTrea = new TreatmentLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkDiagnoses;
        private string _description, _name;
        private DateTime _startDate, _endDate;
        private bool executed = false;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showTreamentALL();
                showDiagnosesDDL();



            }
        }

        private void showTreamentALL()
        {
            DataSet dataSet = new DataSet();
            dataSet = objTrea.showTreatmentALL();
            GVTreatment.DataSource = dataSet;
            GVTreatment.DataBind();
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
                showTreamentALL();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
