using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
namespace Presentation
{
    public partial class WFMedicalHistory : System.Web.UI.Page
    {
        MedicalHistoryLog objMed = new MedicalHistoryLog();
        AppointmentsLog objApp = new AppointmentsLog();
        private int _fkAppoitment;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showAppoitmentsDDL();
                showMedicalHistoryAll();
            }
        }

        private void showMedicalHistoryAll()
        {
            DataSet ds = new DataSet();
            ds = objMed.showMedicalHistoryAll();
            GVHistoryMedical.DataSource = ds;
            GVHistoryMedical.DataBind();
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

            executed = objMed.saveMedicalHistoryByDateId(_fkAppoitment);
            if (executed)
            {
                lblMsg.Text = "se guardo la historia clinica";
                showMedicalHistoryAll();
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