<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOwner.aspx.cs" Inherits="Presentation.WFOwner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmOwner" runat="server">

        <%--Id propietario--%>
        <asp:HiddenField ID="HFOwnerID" runat="server" />
        <br />

        <%--Nombre del propietario--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del propietario"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFVName"
            runat="server"
            ControlToValidate="TBName"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <%--Telefono del propietario--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el telefono"></asp:Label>
        <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFVPhone"
            runat="server"
            ControlToValidate="TBPhone"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <%--DDL del Usuarios--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione el documento del usuario"></asp:Label>
        <asp:DropDownList ID="DDLUsers" runat="server"></asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFVUsers" 
            runat="server"
            ControlToValidate="DDLUsers"
            InitialValue=""
            ErrorMessage="Debes seleccionar un Usuario."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />


        <%--Botones de Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />

            <%--Este Label si se modifica porque lo vamos a utilizar como salida--%>
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>

    <%--Panel para la gestion del Administrador--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <%--Lista de propietarios--%>
        <h2>Lista de Propietarios</h2>
        <table id="ownersTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Telefono</th>
                    <th>FkUsuario</th>
                    <th>Usuario</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


        <%--Propietarios--%>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#ownersTable').DataTable({
                    "processing": true,
                    "serverSide": false,
                    "ajax": {
                        "url": "WFOwner.aspx/ListOwner",// Se invoca el WebMethod Listar Propietarios
                        "type": "POST",
                        "contentType": "application/json",
                        "data": function (d) {
                            return JSON.stringify(d);// Convierte los datos a JSON (d: es una variable, abreviacion de dataset)
                        },
                        "dataSrc": function (json) {
                            return json.d.data;// Obtiene la lista de propietarios del resultado
                        }
                    },
                    "columns": [
                        { "data": "OwnerID" },
                        { "data": "Name" },
                        { "data": "Phone" },
                        { "data": "FkUser", "visible": false },
                        { "data": "NameUser" }, //Verificar si es nombre de usuario o documento de usuario                  
                        {
                            "data": null,
                            "render": function (data, type, row) {
                                return `<button class="edit-btn" data-id="${row.OwnerID}">Editar</button>
                             <button class="delete-btn" data-id="${row.OwnerID}">Eliminar</button>`;
                            }
                        }
                    ],

                    //Establece el legunaje, (igual para los demas)
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ registros por página",
                        "zeroRecords": "No se encontraron resultados",
                        "info": "Mostrando página _PAGE_ de _PAGES_",
                        "infoEmpty": "No hay registros disponibles",
                        "infoFiltered": "(filtrado de _MAX_ registros totales)",
                        "search": "Buscar:",
                        "paginate": {
                            "first": "Primero",
                            "last": "Último",
                            "next": "Siguiente",
                            "previous": "Anterior"
                        }
                    }

                });

                //Funciones para editar, eliminar
                // Editar un propietario
                $('#ownersTable').on('click', '.edit-btn', function () {
                    //const id = $(this).data('id');
                    const rowData = $('#ownersTable').DataTable().row($(this).parents('tr')).data();
                    //alert(JSON.stringify(rowData, null, 2));
                    loadOwnerData(rowData);
                });

                // Eliminar un propietario
                $('#ownersTable').on('click', '.delete-btn', function () {
                    const id = $(this).data('id');// Obtener el ID del propietario
                    if (confirm("¿Estás seguro de que deseas eliminar este propietario?")) {
                        deleteOwner(id);// Invoca a la función para eliminar el propietario
                    }
                });
            });

            // Cargar los datos en los TextBox y DDL para actualizar
            function loadOwnerData(rowData) {
                $('#<%= HFOwnerID.ClientID %>').val(rowData.OwnerID);
                $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBPhone.ClientID %>').val(rowData.Phone);
            $('#<%= DDLUsers.ClientID %>').val(rowData.FkUser);
            }

            // Función para eliminar un propietario
            function deleteOwner(id) {
                $.ajax({
                    type: "POST",
                    url: "WFOwner.aspx/DeleteOwner",// Se invoca el WebMethod Eliminar un Propietario
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ id: id }),
                    success: function (response) {
                        $('#ownersTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                        alert("Propietario eliminado exitosamente.");
                    },
                    error: function () {
                        alert("Error al eliminar el propietario.");
                    }
                });
            }
        </script>
</asp:Content>
