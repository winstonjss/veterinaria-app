using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Logic
{
    public class DiagnosesLog
    {
        DiagnosesDat objDiagnoses = new DiagnosesDat();

        public DataSet showDiagnosticos()
        {
            return objDiagnoses.showDiagnosticos();
        }

        public bool saveDiagnostico(string _clasificacion, string _codigo, int _anamnesisId)
        {
            return objDiagnoses.saveDiagnostico(_clasificacion, _codigo, _anamnesisId);
        }

        public bool updateDiagnostico(int _id, string _clasificacion, string _codigo, int _anamnesisId)
        {
            return objDiagnoses.updateDiagnostico(_id, _clasificacion, _codigo, _anamnesisId);
        }

        public bool deleteDiagnostico(int _id)
        {
            return objDiagnoses.deleteDiagnostico(_id);
        }

        public DataSet showDatesFilterbyAnimalAndRangeDate(int _idDiagnoses)
        {
            return objDiagnoses.showDatesFilterbyAnimalAndRangeDate(_idDiagnoses);
        }

        public DataSet showDiagnosesDLL()
        {
            return objDiagnoses.showDiagnosesDLL();
        }
    }
}