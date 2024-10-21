using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Logic
{
    public class VaccinesLog
    {
        VaccinesDat objVaccines = new VaccinesDat();

        public DataSet showVacunas()
        {
            return objVaccines.showVacunas();
        }

        public bool saveVacuna(string _nombre, string _tipo, decimal _cantidad, int _diagnosticoId)
        {
            return objVaccines.saveVacuna(_nombre, _tipo, _cantidad, _diagnosticoId);
        }

        public bool updateVacuna(int _id, string _nombre, string _tipo, decimal _cantidad, int _diagnosticoId)
        {
            return objVaccines.updateVacuna(_id, _nombre, _tipo, _cantidad, _diagnosticoId);
        }

        public bool deleteVacuna(int _id)
        {
            return objVaccines.deleteVacuna(_id);
        }

        public DataSet showVaccinesById(int _idVacciness)
        {
            return objVaccines.showVaccinesById(_idVacciness);
        }

        public DataSet showVaccinessALL()
        {
            return objVaccines.showVaccinessALL();
        }
    }
}