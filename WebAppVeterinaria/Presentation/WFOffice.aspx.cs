using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
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
                showOffice();

            }
        }

        public void showOffice()
        {
            DataSet ds = new DataSet();
            ds = objOfi.showOffice();
            GVOffice.DataSource = ds;
            GVOffice.DataBind();

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _num_consultorio = TBCon_num_consultorio.Text;
            executed = objOfi.saveOffice(_num_consultorio);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                showOffice();
                TBCon_num_consultorio.Text = "";
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}