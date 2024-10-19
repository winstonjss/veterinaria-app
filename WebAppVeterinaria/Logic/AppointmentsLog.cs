using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using System.Data;


namespace Logic
{
    public class AppointmentsLog
    {
        AppointmentsDat objAppointments = new AppointmentsDat();

        public bool saveDate(int _animalId, string _documentNumberVeterinarian,
            DateTime _date, DateTime _startHour, DateTime _finalHour)
        {
            return objAppointments.saveDate(_animalId, _documentNumberVeterinarian,
                _date, _startHour, _finalHour);
        }

        public bool updateDate(int _dateId, int _animalId, string _documentNumberVeterinarian,
            DateTime _date, DateTime _startHour, DateTime _finalHour)
        {
            return objAppointments.updateDate(_animalId, _documentNumberVeterinarian,
                _date, _startHour, _finalHour);
        }

        public DataSet showDatesFilterbyDate(DateTime _startDate, DateTime _finalDate)
        {
            return objAppointments.showDatesFilterbyDate(_startDate, _finalDate);
        }

        public DataSet showDatesFilterbyVeterinarian(string _documentNumber, DateTime _startDate,
            DateTime _finalDate)
        {            
            return objData.showDatesFilterbyVeterinarian(_documentNumber, _startDate, _finalDate);
        }

        public DataSet showDatesFilterbyAnimalAndRangeDate(int _idAnimal, DateTime _startDate,
            DateTime _finalDate)
        {
            return objAppointments.showDatesFilterbyAnimalAndRangeDate(_idAnimal,
                _startDate, _finalDate);
        }

        public DataSet showCitasAll()
        {
            return objAppointments.showCitasAll();
        }

        public bool deleteDate(int _id)
        {
            return objAppointments.deleteDate(_id);
        }

        public DataSet showCitasDDl()
        {
            return objAppointments.showCitasDDl();
        }
    }
}