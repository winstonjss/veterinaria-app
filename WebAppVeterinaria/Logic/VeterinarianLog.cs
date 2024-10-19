using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;

namespace Logic
{
    public class VeterinarianLog
    {
        VeterinarianDat objVet = new VeterinarianDat();

        //Metodo para mostrar todos los Veterinarios
        public DataSet showVeterinarian()
        {
            return objVet.showVeterinarian();
        }


        //Metodo para mostrar unicamente el id y la descripcion
        public DataSet showVeterinarianDDL()
        {
            return objVet.showVeterinarianDDL();
        }


        //Metodo para guardar un nuevo Veterinario
        public bool saveVeterinarian(string _name, string _phone, int _fkUsers, int _fkOffice)
        {
            return objVet.saveVeterinarian(_name, _phone, _fkUsers, _fkOffice);
        }


        //Metodo para actualizar un Veterinario
        public bool updateVeterinarian(int _vet_id, string _name, string _phone, int _fkUsers, int _fkOffice)
        {
            return objVet.updateVeterinarian(_vet_id, _name, _phone, _fkUsers, _fkOffice);
        }


        //Metodo para borrar un Veterinario
        public bool deleteVeterinarian(int _vet_id)
        {
            return objVet.deleteVeterinarian(_vet_id);
        }
    }
}