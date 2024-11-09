using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;
using System.Web.Script.Serialization;

namespace Presentation
{
    public partial class WFAnamnensis : System.Web.UI.Page
    {
        AnamnesisLog objAnam = new AnamnesisLog();
        AppointmentsLog objAppo = new AppointmentsLog();

        private int _fkAppointment;
        private string _description;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                showAppointmentsDDL();
            }
        }

        [WebMethod]
        public static object ListAnamnesis()
        {
            AnamnesisLog objAnan = new AnamnesisLog();

            // Se obtiene un DataSet que contiene la lista de anamnesis desde la base de datos.
            var dataSet = objAnan.showAnamnesisAll();

            // Se crea una lista para almacenar los anamnesis que se van a devolver.
            var AnamnesisList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                AnamnesisList.Add(new
                {
                    AnamnesisID = row["anam_id"],
                    Description = row["anam_descripcion"],
                    FkAppointment = row["tbl_citas_cit_id"],

                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de anamnesis.
            return new { data = AnamnesisList };
        }

        private void showAppointmentsDDL()
        {
            DDLAppointments.DataSource = objAppo.showCitasDDl();
            DDLAppointments.DataValueField = "cit_id";
            DDLAppointments.DataTextField = "cit_fecha";
            DDLAppointments.DataBind();
            DDLAppointments.Items.Insert(0, new ListItem("Seleccione", ""));
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            
            
                _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
                _description = TBDescription.Text;
                executed = objAnam.saveAnamnesis(_description, _fkAppointment);
                if (executed)
                {
                    lblMsg.Text = "Se guardó la historia clínica correctamente.";
                    ClearForm();
                }
                else
                {
                    lblMsg.Text = "Error al guardar la historia clínica.";
                }
            
            
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFAnamnesisID.Value))
            {
                lblMsg.Text = "No se ha seleccionado un producto para actualizar.";
                return;
            }
            int anamnesisId = Convert.ToInt32(HFAnamnesisID.Value);
            _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
            _description = TBDescription.Text;
            executed = objAnam.updateAnamnesis(anamnesisId, _description, _fkAppointment);
            if (executed)
            {
                lblMsg.Text = "Se actualizó la historia clínica correctamente.";

                ClearForm();
            }
            else
            {
                lblMsg.Text = "Error al actualizar la historia clínica.";
            }
        }


        private void ClearForm()
        {
            HFAnamnesisID.Value = "";
            TBDescription.Text = "";
            DDLAppointments.SelectedIndex = 0;
        }

        [WebMethod]
        public static bool DeleteAnamnesis(int id)
        {
            AnamnesisLog objAnam = new AnamnesisLog();
            return objAnam.deleteAnamnesis(id);
        }


    }
}
