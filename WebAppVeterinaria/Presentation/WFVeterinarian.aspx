<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVeterinarian.aspx.cs" Inherits="Presentation.WFVeterinarian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmVeterinarian" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Veterinarios</h2>

        <asp:HiddenField ID="HFVeterinarianID" runat="server" />

        <div class="row mb-3">
            <!-- Nombre del veterinario -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre del veterinario:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBName"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Nombre del veterinario"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVName"
                        runat="server"
                        ControlToValidate="TBName"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Teléfono del veterinario -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Ingrese el teléfono:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBPhone"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Teléfono del veterinario"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVPhone"
                        runat="server"
                        ControlToValidate="TBPhone"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <!-- Documento del usuario -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Seleccione el documento del usuario:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLUsers"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVUsers"
                        runat="server"
                        ControlToValidate="DDLUsers"
                        InitialValue=""
                        ErrorMessage="Debes seleccionar un Usuario."
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Consultorio -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Seleccione el consultorio:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLOffice"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVOffice"
                        runat="server"
                        ControlToValidate="DDLOffice"
                        InitialValue=""
                        ErrorMessage="Debes seleccionar un Consultorio."
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Botones -->
            <div class="col-md-12 text-center">
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
                    CssClass="form-text text-success"></asp:Label>
            </div>

        </div>
    </form>


    <br />
<br />

    <%--Steven desde aqui--%>
    <%--Panel para la gestion del Administrador--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <%-- Lista de veterinarios --%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Veterinarios Registrados</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="veterinariansTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th style="text-align: center;">ID</th>
                            <th style="text-align: center;">Nombre</th>
                            <th style="text-align: center;">Teléfono</th>
                            <th style="text-align: center;">FkUsuario</th>
                            <th style="text-align: center;">Usuario</th>
                            <th style="text-align: center;">FkConsultorio</th>
                            <th style="text-align: center;">Consultorio</th>
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
    <%--Steven Hasta aqui--%>


    <%--Veterinarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
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
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.VeterinarianID}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.VeterinarianID}">Eliminar</button>`;  // Rojo
                                }
                                buttons += `</div>`;
                            }
                            return buttons;
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
                //if (confirm("¿Estás seguro de que deseas eliminar este veterinario?")) {
                //    deleteVeterinarian(id);// Invoca a la función para eliminar el veterinario
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
                            deleteVeterinarian(id);// Invoca a la función para eliminar 
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
                    /* alert("Veterinario eliminado exitosamente.");*/
                    swal('Exitoso', 'Veterinario eliminado exitosamente', 'success');
                },
                error: function () {
                    /* alert("Error al eliminar el veterinario.");*/
                    swal('Error', 'Error al eliminar el veterinario', 'error');
                }
            });
        }
    </script>
</asp:Content>
