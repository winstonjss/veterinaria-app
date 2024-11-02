using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFVaccines : System.Web.UI.Page
    {
        VaccinesLog objVacc = new VaccinesLog();
        DiagnosesLog objDiag = new DiagnosesLog();

    private int _fkDiagnoses, _quantity;
        private string _name, _type;
      
    private bool executed = false;




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            showVaccinesALL();
            showDiagnosesDDL();
        }
    }

    private void showVaccinesALL()
    {
        DataSet dataSet = new DataSet();
        dataSet = objVacc.showVaccinessALL();
        GVVaccines.DataSource = dataSet;
            GVVaccines.DataBind();
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
        _type = TBType.Text;
        _quantity = Convert.ToInt32(TBQuantity.Text);
        executed = objVacc.saveVacuna(_name, _type, _quantity, _fkDiagnoses);
        if (executed)
        {
            lblMsg.Text = "se guardo la vacuna";
                showVaccinesALL(); 
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