<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles_Permission.aspx.cs" Inherits="Presentation.WFRoles_Permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <form runat="server">
        <asp:Label ID="Label6" runat="server" Text="Seleccione el Rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
        <br />
        
        <asp:Label ID="Label1" runat="server" Text="Seleccione el permiso"></asp:Label>
        <asp:DropDownList ID="DDLPermiso" runat="server"></asp:DropDownList>
        <br />

         <!-- Campos ocultos para almacenar valores antiguos -->
        <asp:HiddenField ID="oldRolId" runat="server" />
        <asp:HiddenField ID="oldPermisoId" runat="server" />
        
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <br />
    
    <h2>Lista de Roles y Permisos</h2>
    <table id="RolPermisoTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>rol ID</th>
                <th>Nombre Rol</th>
                <th>permiso ID</th>
                <th>Nombre Permiso</th>
            </tr>
        </thead>
        <tbody></tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#RolPermisoTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRoles_Permission.aspx/ListRolesPermisos",
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "ID_rol" },
                    { "data": "Nombre_Rol" },
                    { "data": "ID_Permiso" },
                    { "data": "Nombre_Permiso" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-rol-id="${row.ID_rol}" 
                            data-permiso-id="${row.ID_Permiso}">Editar</button> 
                            <button class="delete-btn" data-rol-id="${row.ID_rol}" 
                            data-permiso-id="${row.ID_Permiso}">Eliminar</button>`;
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

            $('#RolPermisoTable').on('click', '.edit-btn', function () {
                const rowData = $('#RolPermisoTable').DataTable().row($(this).parents('tr')).data();
                loadRolesPermisosData(rowData);
            });

            $('#RolPermisoTable').on('click', '.delete-btn', function () {
                const rol_id = $(this).data('rol-id');
                const permiso_id = $(this).data('permiso-id');
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso?")) {
                    deleteRolesPermision(rol_id, permiso_id);
                }
            });
        });


        // Función para cargar los datos del permiso en el formulario y pasar los valores antiguos
        function loadRolesPermisosData(rowData) {
            // Carga los valores seleccionados en los DropDownList
            $('#<%= DDLRol.ClientID %>').val(rowData.ID_rol);
            $('#<%= DDLPermiso.ClientID %>').val(rowData.ID_Permiso);

          // Guarda los valores anteriores en los campos ocultos
             $('#<%= oldRolId.ClientID %>').val(rowData.ID_rol);
             $('#<%= oldPermisoId.ClientID %>').val(rowData.ID_Permiso);
        }
      
        function updateRolesPermisos(_old_rol_id, old_permiso_id, new_rol_id, new_permiso_id) {
            $.ajax({
                type: "POST",
                url: "WFRoles_Permission.aspx/updateRolesPermisos",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({
                    oldRolId: _old_rol_id,
                    oldPermisoId: old_permiso_id,
                    newRolId: new_rol_id,
                    newPermisoId: new_permiso_id
                }),
                success: function (response) {
                    $('#RolPermisoTable').DataTable().ajax.reload();
                    alert("Permiso Actualizado exitosamente.");
                },
                error: function (xhr, status, error) {
                    alert("Error al Actualizar el Permiso: " + error);
                }
            });
        }

        // Función para eliminar un Rol y Permiso
        function deleteRolesPermision(rol_id,permiso_id) {
            $.ajax({
                type: "POST",
                url: "WFRoles_Permission.aspx/deleteRolesPermision",// Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ _rol_id:rol_id, _permiso_id: permiso_id }),
                success: function (response) {
                    $('#RolPermisoTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Rol y Permiso eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Permiso .");
                }
            });
        }

</script>
</asp:Content>