<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermission.aspx.cs" Inherits="Presentation.WFPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <form id="FrmPermission" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Permisos</h2>

        <!-- ID (Hidden) -->
        <asp:HiddenField ID="HFPermiso" runat="server" />

        <!-- Nombre del Permiso -->
        <div class="mb-3">
            <asp:Label
                ID="Label1"
                runat="server"
                Text="Seleccione el Permiso:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:DropDownList
                ID="DDLNombrePer"
                runat="server"
                CssClass="form-select">
                <asp:ListItem Value="0">Seleccione</asp:ListItem>
                <asp:ListItem Value="CREAR">Crear</asp:ListItem>
                <asp:ListItem Value="ACTUALIZAR">Actualizar</asp:ListItem>
                <asp:ListItem Value="MOSTRAR">Mostrar</asp:ListItem>
                <asp:ListItem Value="ELIMINAR">Eliminar</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="RFVNombrePer"
                runat="server"
                ControlToValidate="DDLNombrePer"
                InitialValue="0"
                ErrorMessage="Debes seleccionar un Permiso."
                ForeColor="Red"
                CssClass="form-text text-danger"></asp:RequiredFieldValidator>
        </div>

        <!-- Descripción del Permiso -->
        <div class="mb-3">
            <asp:Label
                ID="Label2"
                runat="server"
                Text="Descripción del Permiso:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox
                ID="TBPer_descripcion"
                runat="server"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RFVDescripcion"
                runat="server"
                ControlToValidate="TBPer_descripcion"
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
                <h3 class="card-title m-0">Lista de Permisos</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="permisoTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th>ID</th>
                            <th>Nombre Permiso</th>
                            <th>Descripción</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de los permisos -->
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Permisos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#permisoTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermission.aspx/ListPermission",// Se invoca el WebMethod Listar Permisos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de Permisos del resultado
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

            // Editar un Permiso 
            $('#permisoTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permisoTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPermisoData(rowData);
            });

            // Eliminar un Permiso
            $('#permisoTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Permiso
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso ?")) {
                    deletepermission(id);// Invoca a la función para eliminar el permiso
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadPermisoData(rowData) {
            $('#<%= HFPermiso.ClientID %>').val(rowData.ID);
            $('#<%= DDLNombrePer.ClientID %>').val(rowData.Nombre);
            $('#<%= TBPer_descripcion.ClientID %>').val(rowData.Descripcion);

        }

        // Función para eliminar un Permiso
        function deletepermission(id) {
            $.ajax({
                type: "POST",
                url: "WFPermission.aspx/deletePermision",// Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#permisoTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Permiso .");
                }
            });
        }
    </script>
</asp:Content>
