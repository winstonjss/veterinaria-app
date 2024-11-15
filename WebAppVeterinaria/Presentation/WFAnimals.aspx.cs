using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAnimals : System.Web.UI.Page
    {
        //Crear los objetos 
        AnimalsLog objAni = new AnimalsLog();
        OwnerLog objOwn = new OwnerLog();

        //Definir atributos
        private int _anim_id, _fkOwner;
        private string _name, _species, _race, _sex, _color;
        private DateTime _date_birth;
        private float _weight;
        private bool executed = false; //Bandera (variable para establecer un estado de algo)

        public bool _showEditButton { get; set; } = false;
        public bool _showDeleteButton { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Se asigna la fecha actual al TextBox en formato "yyyy-MM-dd".
                TBDate_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
                //Aqui se invocan todos los metodos
                //Aqui se invocan todos los metodos
                BtnSave.Visible = false;
                BtnUpdate.Visible = false;
                FrmAnimals.Visible = false;
                PanelAdmin.Visible = false;
                //showAnimals();
                showOwnerDDL();
            }
            validatePermissionRol();
        }


        /*
         * Atributo [WebMethod] en ASP.NET, permite que el método sea expuesto como 
         * parte de un servicio web, lo que significa que puede ser invocado de manera
         * remota a través de HTTP.
         */

        //  ***  Metodo Listar un Animal
        [WebMethod]
        public static object ListAnimals()
        {
            AnimalsLog objAni = new AnimalsLog();

            // Se obtiene un DataSet que contiene la lista de animales desde la base de datos.
            var dataSet = objAni.showAnimals();

            // Se crea una lista para almacenar los animales que se van a devolver.
            var animalsList = new List<object>();

            // Se itera sobre cada fila del DataSet (que representa un animal).
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                animalsList.Add(new
                {
                    AnimalID = row["anim_id"],
                    Name = row["anim_nombre"],
                    Species = row["anim_especie"],
                    Race = row["anim_raza"],
                    Date_birth = Convert.ToDateTime(row["anim_fecha_nacimiento"]).ToString("yyyy-MM-dd"), // Formato de fecha específico.     
                    Sex = row["anim_sexo"],
                    Weight = row["anim_peso"],
                    Color = row["anim_color"],
                    FkOwner = row["tbl_propietario_pro_id"],
                    NameOwner = row["pro_nombre"]
                });
            }

            // Devuelve un objeto en formato JSON que contiene la lista de animales.
            return new { data = animalsList };
        }
        [WebMethod]


        //  ***  Metodo para eliminar un animal
        public static bool DeleteAnimal(int id)
        {
            // Crear una instancia de la clase de lógica de animales
            AnimalsLog objAni = new AnimalsLog();

            // Invocar al método para eliminar el animal y devolver el resultado
            return objAni.deleteAnimals(id);
        }


        //  ***  Metodo para mostrar los propietarios en el DDL
        private void showOwnerDDL()
        {
            DDLOwner.DataSource = objOwn.showOwnerDDL();
            DDLOwner.DataValueField = "pro_id"; //Nombre de la llave primaria
            DDLOwner.DataTextField = "pro_nombre"; //Nombre del propietario
            DDLOwner.DataBind();
            DDLOwner.Items.Insert(0, "Seleccione");
        }


        //  ***  Metodo para limpiar los TextBox y los DDL
        private void clear()
        {
            HFAnimalID.Value = "";
            TBName.Text = "";
            TBSpecies.Text = "";
            TBRace.Text = "";
            TBDate_birth.Text = "";
            TBSex.Text = "";
            TBWeight.Text = "";
            TBColor.Text = "";
            DDLOwner.SelectedIndex = 0;
        }


        ////  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Guardar)
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            _name = TBName.Text;  //Guardamos la variable ingresado por el usuario para poder mostrarla mas adelante
            _species = TBSpecies.Text;
            _race = TBRace.Text;
            _date_birth = DateTime.Parse(TBDate_birth.Text);

            _sex = TBSex.Text;
            _weight = Convert.ToSingle(TBWeight.Text); //NO es caja de texto, es un selector de propiedad (Value) y aplicar casteo (Convert.ToInt32)
            _color = TBColor.Text;
            _fkOwner = Convert.ToInt32(DDLOwner.SelectedValue);

            executed = objAni.saveAnimals(_name, _species, _race, _date_birth, _sex, _weight, _color, _fkOwner);

            if (executed)
            {
                LblMsg.Text = "El animal se guardo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
                //showAnimals();
            }
            else
            {
                LblMsg.Text = "Error al guardar";
            }
        }

        //ESTE METODO GENERO ERROR EN LA FECHA DE NACIMIENTO AL ACTUALIZAR Y NO HAY ESPACIO ENTRE PESO Y COLOR
        //  ***  Eventos que se ejecutan cuando se da clic en los botones (Metodo Actualizar)
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verifica si se ha seleccionado un animal para actualizar
            if (string.IsNullOrEmpty(HFAnimalID.Value))
            {
                LblMsg.Text = "No se ha seleccionado un animal para actualizar.";
                return;
            }
            _anim_id = Convert.ToInt32(HFAnimalID.Value);
            _name = TBName.Text;
            _species = TBSpecies.Text;
            _race = TBRace.Text;
            _date_birth = DateTime.Parse(TBDate_birth.Text);

            _sex = TBSex.Text;
            _weight = Convert.ToSingle(TBWeight.Text);
            _color = TBColor.Text;
            _fkOwner = Convert.ToInt32(DDLOwner.SelectedValue);

            executed = objAni.updateAnimals(_anim_id, _name, _species, _race, _date_birth, _sex, _weight, _color, _fkOwner);

            if (executed)
            {
                LblMsg.Text = "El animal se actualizo exitosamente!";
                clear(); //Se invoca el metodo para limpiar los campos 
            }
            else
            {
                LblMsg.Text = "Error al actualizar";
            }
        }

        private void validatePermissionRol()
        {
            // Se Obtiene el usuario actual desde la sesión
            var objUser = (User)Session["User"];

            // Variable para acceder a la MasterPage y modificar la visibilidad de los enlaces.
            var masterPage = (Main)Master;

            if (objUser == null)
            {
                // Redirige a la página de inicio de sesión si el usuario no está autenticado
                Response.Redirect("WFDefault.aspx");
                return;
            }
            // Obtener el rol del usuario
            var userRole = objUser.Rol.Nombre;

            if (userRole == "Admin")
            {
                //LblMsg.Text = "Bienvenido, Administrador!";

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnimals.Visible = true;// Se pone visible el formulario
                            BtnSave.Visible = true;// Se pone visible el boton guardar
                            break;
                        case "ACTUALIZAR":
                            FrmAnimals.Visible = true;
                            BtnUpdate.Visible = true;// Se pone visible el boton actualizar
                            PanelAdmin.Visible = true;// Se pone visible el panel
                            _showEditButton = true;// Se pone visible el boton editar dentro de la datatable
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;// Se pone visible el boton eliminar dentro de la datatable
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }
            }
            else if (userRole == "Veterinario")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnimals.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAnimals.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else if (userRole == "Propietario")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnimals.Visible = false;
                            BtnSave.Visible = false;
                            PanelAdmin.Visible = false;
                            break;
                        case "ACTUALIZAR":
                            FrmAnimals.Visible = false;
                            BtnUpdate.Visible = false;
                            PanelAdmin.Visible = false;
                            _showEditButton = false;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = false;
                            _showDeleteButton = false;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }           
            else if (userRole == "Secretaria")
            {
                //LblMsg.Text = "Bienvenido, Gerente!";

                masterPage.linkUsers.Visible = false;// Se oculta el enlace de Usuario
                masterPage.linkPermission.Visible = false; // Se oculta el enlace Permiso 
                masterPage.linkRolesPermission.Visible = false;// Se oculta el enlace de Permiso Rol

                foreach (var permiso in objUser.Permisos)
                {
                    switch (permiso.Nombre)
                    {
                        case "CREAR":
                            FrmAnimals.Visible = true;
                            BtnSave.Visible = true;
                            PanelAdmin.Visible = true;
                            break;
                        case "ACTUALIZAR":
                            FrmAnimals.Visible = true;
                            BtnUpdate.Visible = true;
                            PanelAdmin.Visible = true;
                            _showEditButton = true;
                            break;
                        case "MOSTRAR":
                            //LblMsg.Text += " Tienes permiso de Mostrar!";
                            PanelAdmin.Visible = true;
                            break;
                        case "ELIMINAR":
                            //LblMsg.Text += " Tienes permiso de Eliminar!";
                            PanelAdmin.Visible = true;
                            _showDeleteButton = true;
                            break;
                        default:
                            // Si el permiso no coincide con ninguno de los casos anteriores
                            LblMsg.Text += $" Permiso desconocido: {permiso.Nombre}";
                            break;
                    }
                }

            }
            else
            {
                // Si el rol no es reconocido, se deniega el acceso
                LblMsg.Text = "Rol no reconocido. No tienes permisos suficientes para acceder a esta página.";
                Response.Redirect("WFInicio.aspx");
            }
        }
    }
}