<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles_Permission.aspx.cs" Inherits="Presentation.WFRoles_Permisos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <form id="FrmRoles_Permission" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFRol_Permiso" runat="server" />
        <br />

        <%--Roles--%>
        <asp:Label ID="Label3" runat="server" Text="Seleccione el Rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
        
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFVNombreRol" runat="server"
            ControlToValidate="DDLRol"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Rol."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />
        <%--Permisos--%>
        <asp:Label ID="Label1" runat="server" Text="Seleccione el permiso"></asp:Label>
        <asp:DropDownList ID="DDLPermiso" runat="server"></asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFVNombrePermiso" runat="server"
            ControlToValidate="DDLPermiso"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Permiso."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />

         <%-- Fecha de asignación --%>
        <asp:Label ID="Label2" runat="server" Text="Fecha de asignación "></asp:Label>
        <asp:TextBox ID="TBper_rol_fecha" runat="server"  TextMode="Date"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFTBper_rol_fecha"
            runat="server"
            ControlToValidate="TBper_rol_fecha"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <%--<!-- Campos ocultos para almacenar valores antiguos -->
        <asp:HiddenField ID="oldRolId" runat="server" />
        <asp:HiddenField ID="oldPermisoId" runat="server" />--%>
        
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <br />
    
    <asp:Panel ID="PanelAdmin" runat="server">
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
</asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                const showEditButton = '<%= _showEditButton %>' === 'True';
                const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
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
                            "render": function (row) {
                                let buttons = '';
                                if (showEditButton) {
                                    buttons += `<button class="btn btn-info edit-btn" data-id="${row.ID}">Editar</button>`;
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ID}">Eliminar</button>`;
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
