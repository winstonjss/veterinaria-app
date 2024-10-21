using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using System.Data;

namespace Logic
{
    public class MedicalHistoryLog
    {
        MedicalHistory objMedicalHistory = new MedicalHistory();

        public bool saveMedicalHistoryByDateId(int _dateId)
        {
            return objMedicalHistory.saveMedicalHistoryByDateId(_dateId);
        }

        public DataSet showMedicalHistoryByAnimal(int _idAnimal)
        {            
            return objMedicalHistory.showMedicalHistoryByAnimal(_idAnimal);
        }

        public bool deleteMedicalHistory(int _id)
        {
            return objMedicalHistory.deleteMedicalHistory(_id);
        }

        public bool updateMedicalHistory(int _medicalHistoryId, DateTime _medicalHistoryDate,
            int _dateId)
        {
            return objMedicalHistory.updateMedicalHistory(_medicalHistoryId,
                    _medicalHistoryDate,_dateId);
        }
        public DataSet showMedicalHistoryAll()
        {            
            return objMedicalHistory.showMedicalHistoryAll();
        }
    }
}