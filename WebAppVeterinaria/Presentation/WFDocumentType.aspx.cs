using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
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
                showDocumentType();

            }
        }

        //Metodo para mostrar todos tipos de documento
        private void showDocumentType()
        {
            DataSet ds = new DataSet();
            ds = objDocType.showDocumentType();
            GVDocumentType.DataSource = ds;
            GVDocumentType.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _descripcion = TBTip_doc_descripcion.Text;
            executed = objDocType.saveDocumentType(_descripcion);


            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente ";
                showDocumentType();
                TBTip_doc_descripcion.Text = "";
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            //_id = Convert.ToInt32(_id);
            //_descripcion = TBTip_doc_descripcion.Text;
            //executed = objDocType.updatedocumenttype(_id, _descripcion);


            //if (executed)
            //{
            //    LblMsg.Text = "Se Actualizó exitosamente ";
            //    showDocumentType();
            //}
            //else
            //{
            //    LblMsg.Text = "Error al actualizar ";
            //}
        }
    }

}