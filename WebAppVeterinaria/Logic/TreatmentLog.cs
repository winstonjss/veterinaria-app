using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Logic
{
    public class TreatmentLog
    {
        TreatmentDat objTreatment = new TreatmentDat();

        public DataSet showTratamientos(int _idTreatment)
        {
            return objTreatment.showTratamientos(_idTreatment);
        }

        public bool saveTratamiento(string _nombre, string _descripcion, DateTime _fechaInicio, DateTime _fechaFin, int _diagnosticoId)
        {
            return objTreatment.saveTratamiento(_nombre, _descripcion, _fechaInicio, _fechaFin, _diagnosticoId);
        }

        public bool updateTratamiento(int _id, string _nombre, string _descripcion, DateTime _fechaInicio, DateTime _fechaFin, int _diagnosticoId)
        {
            return objTreatment.updateTratamiento(_id, _nombre, _descripcion, _fechaInicio, _fechaFin, _diagnosticoId);
        }

        public bool deleteTratamiento(int _id)
        {
            return objTreatment.deleteTratamiento(_id);
        }

        public DataSet showTreatmentALL()
        {
            return objTreatment.showTreatmentALL();
        }
    }
}