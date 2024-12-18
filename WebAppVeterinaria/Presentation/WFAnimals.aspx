<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAnimals.aspx.cs" Inherits="Presentation.WFAnimals" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.13.6/css/dataTables.bootstrap5.min.css">
    <link href="resources\css\styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="FrmAnimals" runat="server" class="container mt-4">
        <!-- Título del formulario -->
        <h2 class="text-center mb-4">Gestión de Animales</h2>

        <!-- ID Oculto -->
        <asp:HiddenField ID="HFAnimalID" runat="server" />

        <!-- Fila para las dos columnas -->
        <div class="row">
            <!-- Primera columna -->
            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="Label9"
                    runat="server"
                    Text="Ingrese el nombre del animal:"
                    CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox
                    ID="TBName"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Ingrese el nombre del animal aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVName"
                    runat="server"
                    ControlToValidate="TBName"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label2"
                    runat="server"
                    Text="Ingrese la especie:"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:TextBox
                    ID="TBSpecies"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Ingrese la especie aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVSpecies"
                    runat="server"
                    ControlToValidate="TBSpecies"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label3"
                    runat="server"
                    Text="Ingrese la raza:"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:TextBox
                    ID="TBRace"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Ingrese la raza aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVRace"
                    runat="server"
                    ControlToValidate="TBRace"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label4"
                    runat="server"
                    Text="Ingrese la fecha de nacimiento:"
                    AssociatedControlID="TBDate_birth"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:TextBox
                    ID="TBDate_birth"
                    runat="server"
                    TextMode="Date"
                    CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVDate_birth"
                    runat="server"
                    ControlToValidate="TBDate_birth"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>
            </div>

            <!-- Segunda columna -->
            <div class="col-md-6 mb-3">
                <asp:Label
                    ID="Label5"
                    runat="server"
                    Text="Sexo del animal:"
                    CssClass="form-label fw-bold"></asp:Label>
                <asp:TextBox
                    ID="TBSex"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Sexo del animal aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVSex"
                    runat="server"
                    ControlToValidate="TBSex"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label6"
                    runat="server"
                    Text="Peso del animal:"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:TextBox
                    ID="TBWeight"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Ingrese el peso del animal aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVWeight"
                    runat="server"
                    ControlToValidate="TBWeight"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label7"
                    runat="server"
                    Text="Color del animal:"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:TextBox
                    ID="TBColor"
                    runat="server"
                    CssClass="form-control"
                    Placeholder="Color del animal aquí"></asp:TextBox>
                <asp:RequiredFieldValidator
                    ID="RFVColor"
                    runat="server"
                    ControlToValidate="TBColor"
                    ForeColor="Red"
                    Display="Dynamic"
                    ErrorMessage="Este campo es obligatorio"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>

                <asp:Label
                    ID="Label8"
                    runat="server"
                    Text="Seleccione el propietario:"
                    CssClass="form-label fw-bold mt-3"></asp:Label>
                <asp:DropDownList
                    ID="DDLOwner"
                    runat="server"
                    CssClass="form-select">
                </asp:DropDownList>
                <asp:RequiredFieldValidator
                    ID="RFVOwner"
                    runat="server"
                    ControlToValidate="DDLOwner"
                    InitialValue=""
                    ErrorMessage="Debes seleccionar un propietario"
                    ForeColor="Red"
                    CssClass="form-text text-danger"></asp:RequiredFieldValidator>
            </div>
        </div>

        <!-- Botones de Guardar y Actualizar -->
        <div class="text-center mt-4">
            <asp:Button
                ID="BtnSave"
                runat="server"
                Text="Guardar"
                OnClick="BtnSave_Click"
                CssClass="btn btn-success me-2" />
            <asp:Button
                ID="BtnUpdate"
                runat="server"
                Text="Actualizar"
                OnClick="BtnUpdate_Click"
                CssClass="btn btn-primary me-2" />
            <asp:Label
                ID="LblMsg"
                runat="server"
                Text=""
                CssClass="form-text text-info"></asp:Label>
        </div>
    </form>

    <br />
    <br />
    <%--Panel para la gestion del Administrador--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <%-- Lista de veterinarios --%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Animales</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="animalsTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
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
                        <!-- Aquí irán los datos dinámicos de los veterinarios -->
                    </tbody>
                </table>
            </div>
        </div>
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
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.AnimalID}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.AnimalID}">Eliminar</button>`;  // Rojo
                                }
                                buttons += `</div>`;
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
                //if (confirm("¿Estás seguro de que deseas eliminar este animal?")) {
                //    deleteAnimal(id);// Invoca a la función para eliminar el animal
                //}
                swal({
                    title: "Esta seguro?",
                    text: "Precaución se eliminará permanentemente!",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
                            deleteAnimal(id);// Invoca a la función para eliminar el consultorio
                            swal("Poof! Eliminado exitosamente!", {
                                icon: "success",
                            });
                        } else {
                            swal("No se eliminó el registro!");
                        }
                    });
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
                    /* alert("Animal eliminado exitosamente.");*/
                    swal('Exitoso', 'Se eliminó el registro', 'success');
                },
                error: function () {
                    swal('Error', 'Error al eliminar', 'error');
                }
            });
        }
    </script>
</asp:Content>
