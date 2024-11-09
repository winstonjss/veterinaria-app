<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVeterinarian.aspx.cs" Inherits="Presentation.WFVeterinarian" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   <%--Estilos--%>
   <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <%--Aqui va todo lo del formulario de veterinario--%>

        <%--Id veterinario--%>
        <asp:HiddenField ID="HFVeterinarianID" runat="server" />
        <br />
        <%--Nombre del veterinario--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del veterinario"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <br />
        <%--Telefono del veterinario--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el telefono"></asp:Label>
        <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
        <br />
        <%--DDL del Usuarios--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione el documento del usuario"></asp:Label>
        <asp:DropDownList ID="DDLUsers" runat="server"></asp:DropDownList>
        <br />
        <%--DDL del Consultorio--%>
        <asp:Label ID="Label4" runat="server" Text="Seleccione el consultorio"></asp:Label>
        <asp:DropDownList ID="DDLOffice" runat="server"></asp:DropDownList>
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


    <%--Lista de veterinarios--%>
    <h2>Lista de Veterinarios</h2>
    <table id="veterinariansTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Telefono</th>
                <th>FkUsuario</th>
                <th>Usuario</th>
                <th>FkConsultorio</th>
                <th>Consultorio</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Veterinarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#veterinariansTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFVeterinarian.aspx/ListVeterinarian",// Se invoca el WebMethod Listar Veterinarios
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON (d: es una variable, abreviacion de dataset)
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de veterinarios del resultado
                    }
                },
                "columns": [
                    { "data": "VeterinarianID" },
                    { "data": "Name" },
                    { "data": "Phone" },
                    { "data": "FkUser", "visible": false },
                    { "data": "NameUser" }, //Verificar si es nombre de usuario o documento de usuario
                    { "data": "FkOffice", "visible": false },
                    { "data": "NameOffice" }, 
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.VeterinarianID}">Editar</button>
                             <button class="delete-btn" data-id="${row.VeterinarianID}">Eliminar</button>`;
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
            // Editar un veterinario
            $('#veterinariansTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#veterinariansTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadVeterinarianData(rowData);
            });


            // Eliminar un veterinario
            $('#veterinariansTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del veterinario
                if (confirm("¿Estás seguro de que deseas eliminar este veterinario?")) {
                    deleteVeterinarian(id);// Invoca a la función para eliminar el veterinario
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadVeterinarianData(rowData) {
            $('#<%= HFVeterinarianID.ClientID %>').val(rowData.VeterinarianID);
            $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBPhone.ClientID %>').val(rowData.Phone);
            $('#<%= DDLUsers.ClientID %>').val(rowData.FkUser);
            $('#<%= DDLOffice.ClientID %>').val(rowData.FkOffice);
        }

        // Función para eliminar un veterinario
        function deleteVeterinarian(id) {
            $.ajax({
                type: "POST",
                url: "WFVeterinarian.aspx/DeleteVeterinarian",// Se invoca el WebMethod Eliminar un Veterinario
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#veterinariansTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Veterinario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el veterinario.");
                }
            });
        }
    </script>
</asp:Content>
