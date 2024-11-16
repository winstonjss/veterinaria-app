<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAnimals.aspx.cs" Inherits="Presentation.WFAnimals" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <form id="FrmAnimals" runat="server">

        <%--Id animal--%>
        <asp:HiddenField ID="HFAnimalID" runat="server" />
        <br />

        <%--Nombre del animal--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del animal"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <br />
        <%--Especie del animal--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la especie"></asp:Label>
        <asp:TextBox ID="TBSpecies" runat="server"></asp:TextBox>
        <br />
        <%--Raza del animal--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la raza"></asp:Label>
        <asp:TextBox ID="TBRace" runat="server"></asp:TextBox>
        <br />
        <%--Fecha de nacimiento del animal--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la fecha de nacimiento"></asp:Label>
        <asp:TextBox ID="TBDate_birth" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--Sexo del animal--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese el sexo"></asp:Label>
        <asp:TextBox ID="TBSex" runat="server"></asp:TextBox>
        <br />
        <%--Peso del animal--%>
        <asp:Label ID="Label6" runat="server" Text="Ingrese el peso"></asp:Label>
        <asp:TextBox ID="TBWeight" runat="server"></asp:TextBox>
        <br />
        <%--Color del animal--%>
        <asp:Label ID="Label7" runat="server" Text="Ingrese el color"></asp:Label>
        <asp:TextBox ID="TBColor" runat="server"></asp:TextBox>
        <br />
        <%--DDL del Propietario--%>
        <asp:Label ID="Label8" runat="server" Text="Seleccione el propietario"></asp:Label>
        <asp:DropDownList ID="DDLOwner" runat="server"></asp:DropDownList>
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


    <%--Lista de animales--%>
    <h2>Lista de Animales</h2>
    <asp:Panel ID="PanelAdmin" runat="server">
    <table id="animalsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Especie</th>
                <th>Raza</th>
                <th>Fecha_nacimiento</th>
                <th>Sexo</th>
                <th>Peso</th>
                <th>Color</th>
                <th>FkPropietario</th>
                <th>Propietario</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>


    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


    <%--Animales--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#animalsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAnimals.aspx/ListAnimals",// Se invoca el WebMethod Listar Animales
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON (d: es una variable, abreviacion de dataset)
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de animales del resultado
                    }
                },
                "columns": [
                    { "data": "AnimalID" },
                    { "data": "Name" },
                    { "data": "Species" },
                    { "data": "Race" },
                    { "data": "Date_birth" },
                    { "data": "Sex" },
                    { "data": "Weight" },
                    { "data": "Color" },
                    { "data": "FkOwner", "visible": false },
                    { "data": "NameOwner" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="edit-btn" data-id="${row.ProductID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="delete-btn" data-id="${row.ProductID}">Eliminar</button>`;
                            }
                            return buttons;
                        }
                    }
                ],
                //Establece el legunaje
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
            //(Funciones para editar, eliminar)

            // Editar un animal
            $('#animalsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#animalsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadAnimalData(rowData);
            });

            // Eliminar un animal
            $('#animalsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del animal
                if (confirm("¿Estás seguro de que deseas eliminar este animal?")) {
                    deleteAnimal(id);// Invoca a la función para eliminar el animal
                }
            });
        });


        // Cargar los datos en los TextBox y DDL para actualizar
        function loadAnimalData(rowData) {
            $('#<%= HFAnimalID.ClientID %>').val(rowData.AnimalID);
            $('#<%= TBName.ClientID %>').val(rowData.Name);
            $('#<%= TBSpecies.ClientID %>').val(rowData.Species);
            $('#<%= TBRace.ClientID %>').val(rowData.Race);
            $('#<%= TBDate_birth.ClientID %>').val(rowData.Date_birth);
            $('#<%= TBSex.ClientID %>').val(rowData.Sex);
            $('#<%= TBWeight.ClientID %>').val(rowData.Weight);
            $('#<%= TBColor.ClientID %>').val(rowData.Color);
            $('#<%= DDLOwner.ClientID %>').val(rowData.FkOwner);
        }


        // Función para eliminar un animal
        function deleteAnimal(id) {
            $.ajax({
                type: "POST",
                url: "WFAnimals.aspx/DeleteAnimal",// Se invoca el WebMethod Eliminar un Animal
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#animalsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Animal eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el animal.");
                }
            });
        }
    </script>
</asp:Content>
