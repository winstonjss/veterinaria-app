using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFVaccines : System.Web.UI.Page
    {
        VaccinesLog objVacc = new VaccinesLog();
        DiagnosesLog objDiag = new DiagnosesLog();

    private int _fkDiagnoses, _id;
        private string _name, _type;
        private string _description;
        private decimal _quantity;


    private bool executed = false;




    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            showDiagnosesDDL();
        }
    }

        [WebMethod]
        public static object ListVaccines()
        {
            VaccinesLog objVacc = new VaccinesLog();
            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objVacc.showVaccinessALL();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var VaccinesList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                VaccinesList.Add(new
                {
                    VaccinesId = row["vac_id"],
                    VaccinesName = row["vac_nombre"],
                    VaccinesGuy = row["vac_tipo"],
                    VaccinesAmount = row["vac_cantidad"],
                    FkDiagonoses = row["tbl_diagnosticos_diag_id"],
                    DiagonosesCode = row["diag_cod"]


                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = VaccinesList };
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
        _type = TBGuy.Text;
        _quantity = Convert.ToDecimal(TBAmount.Text);
        executed = objVacc.saveVacuna(_name, _type, _quantity, _fkDiagnoses);
        if (executed)
        {
            lblMsg.Text = "se guardo la vacuna";
                clear(); 


        }
        else
        {
            lblMsg.Text = "erorr al guardar";
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
            if (string.IsNullOrEmpty(HFVaccinesID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un Tratamiento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFVaccinesID.Value);

            _fkDiagnoses = Convert.ToInt32(DDLDiagonoses.SelectedValue);

            _name = TBName.Text;
            _type = TBGuy.Text;
            _quantity = Convert.ToDecimal(TBAmount.Text);
            executed = objVacc.updateVacuna(_id, _name, _type, _quantity, _fkDiagnoses);
            if (executed)
            {
                lblMsg.Text = "se guardo la vacuna";
                clear();
            }
            else
            {
                lblMsg.Text = "erorr al guardar";
            }

        }
        [WebMethod]
        public static bool deleteVaccines(int id)
        {
            // Crear una instancia de la clase de lógica de anamnesis
            VaccinesLog objVacc = new VaccinesLog();

            // Invocar al método para eliminar el producto y devolver el resultado
            return objVacc.deleteVacuna(id);
        }

        //Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFVaccinesID.Value = "";
            TBName.Text = "";
            TBGuy.Text = "";
            TBAmount.Text = "";
            DDLDiagonoses.SelectedIndex = 0;


        }
    }
}

