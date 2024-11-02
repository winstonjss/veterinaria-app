using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;

namespace Presentation
{
    public partial class WFAnamnensis : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        AppointmentsLog objAppo = new AppointmentsLog();

        private int _fkAppoimen;
        private string _description;
        private bool executed= false;




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showAnamnesisAll();
                showAppoitmentsDDL();

            }
        }

        private void showAnamnesisAll() 
        { 
            DataSet dataSet = new DataSet();
            dataSet = objAnam.showAnamnesisAll();
            GVAnamnesis.DataSource=dataSet;
            GVAnamnesis.DataBind();
        }
        private void showAppoitmentsDDL()
        {
            DDLAppoiments.DataSource = objAppo.showCitasDDl();
            DDLAppoiments.DataValueField = "cit_id";
            DDLAppoiments.DataTextField = "cit_fecha";
            DDLAppoiments.DataBind();
            DDLAppoiments.Items.Insert(0, "seleccione");
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _fkAppoimen = Convert.ToInt32(DDLAppoiments.SelectedValue);

            _description = TBDescription.Text;
            executed = objAnam.saveAnamnesis(_description,_fkAppoimen);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
                showAnamnesisAll();
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