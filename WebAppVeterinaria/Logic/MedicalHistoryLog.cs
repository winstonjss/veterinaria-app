using System;
using Data;
using System.Data;

namespace Logic
{
    public class MedicalHistoryLog
    {
        MedicalHistory objMedicalHistory = new MedicalHistory();

        public bool saveMedicalHistoryByDateId(int _dateId, DateTime _medicalHistoryDate)
        {
            return objMedicalHistory.saveMedicalHistoryByDateId(_dateId, _medicalHistoryDate);
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