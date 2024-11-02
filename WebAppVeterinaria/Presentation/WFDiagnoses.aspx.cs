using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFDiagnoses : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        DiagnosesLog objDiag = new DiagnosesLog();

        private int _fkAnemnesis;
        private string _clasification, _code;
        private bool executed = false;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showDiagnosesAll();
                showAnamnesisDDL();

            }
        }

        private void showDiagnosesAll()
        {
            DataSet dataSet = new DataSet();
            dataSet = objDiag.showDiagnosticos();
            GVDiagnoses.DataSource = dataSet;
            GVDiagnoses.DataBind();
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

            _clasification = TBClasification.Text;
            _code = TBCode.Text;
            executed = objDiag.saveDiagnostico(_clasification,_code,_fkAnemnesis);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
                showDiagnosesAll();
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
