using System;
using Data;
using System.Data;
using System.Runtime.Remoting;


namespace Logic
{
    public class AppointmentsLog
    {
        AppointmentsDat objAppointments = new AppointmentsDat();

        public bool saveDate(int _animalId, int _veterinarianId,
            DateTime _date, TimeSpan _startHour, TimeSpan _finalHour)
        {
            return objAppointments.saveDate(_animalId, _veterinarianId,
                _date, _startHour, _finalHour);
        }

        public bool updateDate(int _dateId, int _animalId, int _veterinarianId,
            DateTime _date, TimeSpan _startHour, TimeSpan _finalHour)
        {
            return objAppointments.updateDate(_dateId, _animalId, _veterinarianId,
                _date, _startHour, _finalHour);
        }

        public DataSet showDatesFilterbyDate(DateTime _startDate, DateTime _finalDate)
        {
            return objAppointments.showDatesFilterbyDate(_startDate, _finalDate);
        }

        public DataSet showDatesFilterbyVeterinarian(string _documentNumber, DateTime _startDate,
            DateTime _finalDate)
        {            
            return objAppointments.showDatesFilterbyVeterinarian(_documentNumber, _startDate, _finalDate);
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

        public DataSet spCitasResumenMesActual2(DateTime fechaInicio, DateTime fechaFinal, DateTime fechaActual)
        {
            return objAppointments.spCitasResumenMesActual2(fechaInicio, fechaFinal, fechaActual);
        }
    }
}