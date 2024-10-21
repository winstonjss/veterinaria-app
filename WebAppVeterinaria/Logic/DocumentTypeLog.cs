using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using Data;

namespace Logic
{
    public class DocumentTypeLog
    {
        DocumentTypeDat objDoc =new DocumentTypeDat();

        //Metodo para mostrar todos los tipos de documento
        public DataSet showDocumentType()
        {
           return objDoc.showDocumentType();
        }

        //Metodo para mostrar solo el tipo de documento
        public DataSet showDocumentTypeDDL()
        {
            return objDoc.showDocumentTypeDDL();
        }

        //Metodo para guardar un tipo de documento
        public bool saveDocumentType(string _descripcion)
        {
            return objDoc.saveDocumentType(_descripcion);
        }

        //Metodo para actualizar un tipo de documento
        public bool updatedocumenttype(int _id, string _descripcion)
        {
            return objDoc.updatedocumenttype(_id, _descripcion);
        }

        //Metodo para borrar un Tipo de documento
        public bool deleteCategory(int _id)
        {
         return objDoc.deleteCategory(_id);
        }
    }
}