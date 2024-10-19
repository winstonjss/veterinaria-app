using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Logic
{
    public class AnamnesisLog
    {
        AnamnesisDat objAnamnesis = new AnamnesisDat();

        public DataSet showAnamnesis(int _idAnamnesis)
        {
            return objAnamnesis.showAnamnesis(_idAnamnesis);
        }

        public bool saveAnamnesis(string _descripcion, int _citasId)
        {
            return objAnamnesis.saveAnamnesis(_descripcion, _citasId);
        }

        public bool updateAnamnesis(int _idAnamnesis, string _descripcion, int _citasId)
        {
            return objAnamnesis.updateAnamnesis(_idAnamnesis, _descripcion, _citasId);
        }

        public bool deleteAnamnesis(int _idAnamnesis)
        {
            return objAnamnesis.deleteAnamnesis(_idAnamnesis);
        }

        public DataSet showAnamnesisById(int _idAnamnesis)
        {
            return objAnamnesis.showAnamnesisById(_idAnamnesis);
        }

        public DataSet showAnamnesisAll()
        {
            return objAnamnesis.showAnamnesisAll();
        }

        public DataSet showAnamnesisDDl()
        {
            return objAnamnesis.showAnamnesisDDl();
        }
    }
}