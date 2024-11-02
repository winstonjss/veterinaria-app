using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Presentation
{

    public partial class WFUsers : System.Web.UI.Page
    {
        //Crear los objetos 
        UsersLog objUse = new UsersLog();
        DocumentTypeLog objDoc = new DocumentTypeLog();
        RolesLog objRol = new RolesLog();

        private int _usu_id, _rol_id, _tipo_documento_id;
        private string _documento, _correo, _contrasena, _salt, _estado;
        private DateTime _fecha_creacion;
        private bool executed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //aqui se invocan todos los métodos 
                showUsers();
                showDocumentTypeDDL();
                showRolesDDL();
            }

        }

        //Metodo para mostrar todos los usuarios
        private void showUsers()
        {
            DataSet ds = new DataSet();
            ds = objUse.showUsers();
            GVUsers.DataSource = ds;
            GVUsers.DataBind();
        }


        //Metodo para mostrar los roles DDL
        private void showRolesDDL()
        {
            DDLRol.DataSource = objRol.showRolesDDL();
            DDLRol.DataValueField = "rol_id";
            DDLRol.DataTextField = "rol_nombre";
            DDLRol.DataBind();
            DDLRol.Items.Insert(0, "Seleccione");
        }

        //Metodo para mostrar los tipo de documento en el DDL

        private void showDocumentTypeDDL()
        {
            DDLTipo_documento.DataSource = objDoc.showDocumentTypeDDL();
            DDLTipo_documento.DataValueField = "tip_doc_id";
            DDLTipo_documento.DataTextField = "tip_doc_descripcion";
            DDLTipo_documento.DataBind();
            DDLTipo_documento.Items.Insert(0, "Seleccione");

        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _documento = TBUsu_documento.Text;
            _correo = TBUsu_correo.Text;
            _contrasena = TBUsu_contrasena.Text;
            _salt = TBUsu_salt.Text;
            _estado = TBUsu_estado.Text;
            _fecha_creacion = Convert.ToDateTime(TBUsu_fecha_creación.Text);

            _rol_id = Convert.ToInt32(DDLRol.SelectedValue);
            _tipo_documento_id = Convert.ToInt32(DDLTipo_documento.SelectedValue);

            executed = objUse.saveUser(_documento, _correo, _contrasena, _salt, _estado,
                _fecha_creacion, _rol_id, _tipo_documento_id);

            if (executed)
            {
                LblMsg.Text = "Se guardó exitosamente el usuario ";
                showUsers();
                TBUsu_documento.Text = "";
                TBUsu_correo.Text = "";
                TBUsu_contrasena.Text = "";
                TBUsu_salt.Text = "";
                TBUsu_estado.Text = "";
                TBUsu_fecha_creación.Text = "";
                DDLRol.SelectedIndex = 0;
                DDLTipo_documento.SelectedIndex = 0;
            }
            else
            {
                LblMsg.Text = "Error al guardar ";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}