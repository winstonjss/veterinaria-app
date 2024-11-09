using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFDocumentType : System.Web.UI.Page
    {
        //Crear los objetos 
        DocumentTypeLog objDocType = new DocumentTypeLog();
        private int _id;
        private string _descripcion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                //showDocumentType();

            }
        }

        //Metodo para mostrar todos tipos de documento
        [WebMethod]
        public static object ListDocumentType()
        {
            DocumentTypeLog objDoc = new DocumentTypeLog();

            // Se obtiene un DataSet que contiene la lista de productos desde la base de datos.
            var dataSet = objDoc.showDocumentType();

            // Se crea una lista para almacenar los productos que se van a devolver.
            var documentTypeList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un producto).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                documentTypeList.Add(new
                {
                    ID = row["tip_doc_id"],
                    DocumentType = row["tip_doc_descripcion"],

                });
            }
            // Devuelve un objeto en formato JSON que contiene la lista de productos.
            return new { data = documentTypeList };
        }
        [WebMethod]

        public static bool deleteDocumentType(int id)
        {
            // Crear una instancia de la clase de lógica de tipo de documento 
            DocumentTypeLog objDoc = new DocumentTypeLog();


            // Invocar al método para eliminar el producto y devolver el resultado
            return objDoc.deleteDocumentType(id);
        }

        private void clear()
        {
            HFDocumenTypeId.Value = "";
            TBTip_doc_descripcion.Text = "";

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _descripcion = TBTip_doc_descripcion.Text;
            executed = objDocType.saveDocumentType(_descripcion);


            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                //showDocumentType() ;
                clear();
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un Documento  para actualizar
            if (string.IsNullOrEmpty(HFDocumenTypeId.Value))
            {
                LblMsg.Text = "No se ha seleccionado un tipo de documento para actualizar.";
                return;
            }
            _id = Convert.ToInt32(HFDocumenTypeId.Value);
            _descripcion = TBTip_doc_descripcion.Text;
            executed = objDocType.updatedocumenttype(_id, _descripcion);


            if (executed)
            {
                LblMsg.Text = "Se ACTUALIZÓ exitosamente ";

                clear();
            }
            else
            {
                LblMsg.Text = "Error al ACTUALIZAR ";
            }
        }

    }
}