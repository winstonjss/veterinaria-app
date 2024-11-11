<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles_Permission.aspx.cs" Inherits="Presentation.WFRoles_Permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <form runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFRol_Permiso" runat="server" />
        <br />
    
        
        <br />
<asp:Label ID="Label3" runat="server" Text="Seleccione el Rol"></asp:Label>
<asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
<br />
        <asp:Label ID="Label1" runat="server" Text="Seleccione el permiso"></asp:Label>
        <asp:DropDownList ID="DDLPermiso" runat="server"></asp:DropDownList>
        <br />

         <%-- Fecha de asignación --%>
        <asp:Label ID="Label2" runat="server" Text="Fecha de asignación "></asp:Label>
        <asp:TextBox ID="TBper_rol_fecha" runat="server"  TextMode="Date"></asp:TextBox>
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
                <th>ID Rol_Permiso</th>
                <th>Rol_ID</th>
                <th>Nombre Rol </th>
                <th>Permiso ID </th>
                <th>Nombre Permiso</th>
                <th>Fecha asignación</th>
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
                    { "data": "ID" },
                    { "data": "Rol_ID" },
                    { "data": "NombreRol" },
                    { "data": "Per_ID" },
                    { "data": "NombrePermiso" },
                    { "data": "Date" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.ID}">Editar</button>
                              <button class="delete-btn" data-id="${row.ID}">Eliminar</button>`;
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

            // Editar un Rol y Permiso
            $('#RolPermisoTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#RolPermisoTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadRolesPermisosData(rowData);
            });

            $('#RolPermisoTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Rol Permiso
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso?")) {
                    deleteRolesPermision(id);// Invoca a la función para eliminar el Rol Permiso
                }
            });
        });

        // Función para cargar los datos del permiso en el formulario 
        function loadRolesPermisosData(rowData) {
            // Cargar ID del rol_permiso en el campo oculto
            $('#<%= HFRol_Permiso.ClientID %>').val(rowData.ID);
            $('#<%= DDLRol.ClientID %>').val(rowData.Rol_ID);
            $('#<%= DDLPermiso.ClientID %>').val(rowData.Per_ID);
            $('#<%= TBper_rol_fecha.ClientID %>').val(rowData.Date);
        }


        // Función para eliminar un Rol y Permiso
        function deleteRolesPermision(id) {
            $.ajax({
                type: "POST",
                url: "WFRoles_Permission.aspx/deleteRolesPermision",// Se invoca el WebMethod Eliminar un Permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
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
