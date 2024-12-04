<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRol.aspx.cs" Inherits="Presentation.WFRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <form id="FrmRol" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Roles</h2>

        <!-- ID (Hidden) -->
        <asp:HiddenField ID="HFRol" runat="server" />

        <!-- Nombre del Rol -->
        <div class="mb-3">
            <asp:Label
                ID="Label3"
                runat="server"
                Text="Seleccione el Rol:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:DropDownList
                ID="DDLNombreRol"
                runat="server"
                CssClass="form-select">
                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                <asp:ListItem Value="Administrador">Administrador</asp:ListItem>
                <asp:ListItem Value="Veterinario">Veterinario</asp:ListItem>
                <asp:ListItem Value="Secretaria">Secretaria</asp:ListItem>
                <asp:ListItem Value="Propietario">Propietario</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="RFVNombreRol"
                runat="server"
                ControlToValidate="DDLNombreRol"
                InitialValue="0"
                ErrorMessage="Debes seleccionar un Rol."
                ForeColor="Red"
                CssClass="form-text text-danger"></asp:RequiredFieldValidator>
        </div>

        <!-- Descripción del Rol -->
        <div class="mb-3">
            <asp:Label
                ID="Label2"
                runat="server"
                Text="Descripción del Rol:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox
                ID="TBRol_descripcion"
                runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RFVDescripcion"
                runat="server"
                ControlToValidate="TBRol_descripcion"
                ForeColor="Red"
                Display="Dynamic"
                ErrorMessage="Este campo es obligatorio"
                CssClass="form-text text-danger"></asp:RequiredFieldValidator>
        </div>

        <!-- Botones Guardar y Actualizar -->
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
                CssClass="form-text text-success"></asp:Label>
        </div>

    </form>

    <br />
<br />

    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Roles</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="rolTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th>ID</th>
                            <th>Nombre Rol</th>
                            <th>Descripción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de los roles -->
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#rolTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRol.aspx/ListRoles",// Se invoca el WebMethod Listar Roles 
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de consultorios del resultado
                    }
                },
                "columns": [
                    { "data": "ID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },

                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning" data-id="${row.ID}">Editar</button>`;  // Amarillo
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.ID}">Eliminar</button>`;  // Rojo
                                }
                                buttons += `</div>`;
                            }
                            return buttons;
                        }


                    }
                ],
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

            // Editar un Rol 
            $('#rolTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#rolTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadRolData(rowData);
            });

            // Eliminar un Rol
            $('#rolTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Rol
                if (confirm("¿Estás seguro de que deseas eliminar este Rol ?")) {
                    deleterol(id);// Invoca a la función para eliminar el rol
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadRolData(rowData) {
            $('#<%= HFRol.ClientID %>').val(rowData.ID);
            $('#<%= DDLNombreRol.ClientID %>').val(rowData.Nombre);
            $('#<%= TBRol_descripcion.ClientID %>').val(rowData.Descripcion);

        }

        // Función para eliminar un Rol
        function deleterol(id) {
            $.ajax({
                type: "POST",
                url: "WFRol.aspx/deleteRol",// Se invoca el WebMethod Eliminar un Rol
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#rolTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Rol eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Rol .");
                }
            });
        }
    </script>
</asp:Content>
