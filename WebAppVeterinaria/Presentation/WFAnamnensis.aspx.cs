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
                showAnamnesisAll();
                showAppointmentsDDL();
            }
        }

        [WebMethod]
        public static object ListAnamnesis()
        {
            AnamnesisLog objAnam = new AnamnesisLog();
            var AnamnesisList = new List<object>();

            try
            {
                var dataSet = objAnam.showAnamnesisALL();

                if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        try
                        {
                            AnamnesisList.Add(new
                            {
                                AnamnesisID = Convert.ToInt32(row["AnamnesisID"]),
                                Code = row["Anam_codigo"].ToString(),
                                Description = row["Anam_descripcion"].ToString(),
                                FkAppointment = Convert.ToInt32(row["tbl_citas_cit_id"]),
                                NameAppointment = row["cit_fecha"].ToString(),
                            });
                        }
                        catch (Exception ex)
                        {
                            // Log the error for the specific row
                            System.Diagnostics.Debug.WriteLine($"Error processing row: {ex.Message}");

                            // Add an error object to the list instead of skipping the row
                            AnamnesisList.Add(new
                            {
                                AnamnesisID = -1,
                                Code = "Error",
                                Description = "Error processing this record",
                                FkAppointment = -1,
                                NameAppointment = "Error",
                                ErrorMessage = ex.Message
                            });
                        }
                    }
                }
                else
                {
                    // Log that no data was returned
                    System.Diagnostics.Debug.WriteLine("No data returned from showAnamnesis()");
                }
            }
            catch (Exception ex)
            {
                // Log the general error
                System.Diagnostics.Debug.WriteLine($"Error in ListAnamnesis: {ex.Message}");

                // Return an error object
                return new { error = true, message = "An error occurred while retrieving the anamnesis list." };
            }

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
            if (DDLAppointments.SelectedIndex > 0 && !string.IsNullOrEmpty(TBDescription.Text))
            {
                _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
                _description = TBDescription.Text;
                executed = objAnam.saveAnamnesis(_description, _fkAppointment);
                if (executed)
                {
                    lblMsg.Text = "Se guardó la historia clínica correctamente.";
                    showAnamnesisAll();
                    ClearForm();
                }
                else
                {
                    lblMsg.Text = "Error al guardar la historia clínica.";
                }
            }
            else
            {
                lblMsg.Text = "Por favor, complete todos los campos.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (DDLAppointments.SelectedIndex > 0 && !string.IsNullOrEmpty(TBDescription.Text) && !string.IsNullOrEmpty(HFAnamnesisID.Value))
            {
                int anamnesisId = Convert.ToInt32(HFAnamnesisID.Value);
                _fkAppointment = Convert.ToInt32(DDLAppointments.SelectedValue);
                _description = TBDescription.Text;
                executed = objAnam.updateAnamnesis(anamnesisId, _description, _fkAppointment);
                if (executed)
                {
                    lblMsg.Text = "Se actualizó la historia clínica correctamente.";
                    showAnamnesisAll();
                    ClearForm();
                }
                else
                {
                    lblMsg.Text = "Error al actualizar la historia clínica.";
                }
            }
            else
            {
                lblMsg.Text = "Por favor, complete todos los campos y seleccione un registro para actualizar.";
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

        private void showAnamnesisAll()
        {
            var anamnesisList = ListAnamnesis();
            var serializer = new JavaScriptSerializer();
            var serializedData = serializer.Serialize(anamnesisList);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "updateDataTable", $"updateDataTable({serializedData});", true);
        }
    }
}