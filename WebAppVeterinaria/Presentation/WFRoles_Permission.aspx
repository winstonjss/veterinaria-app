<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRoles_Permission.aspx.cs" Inherits="Presentation.WFRoles_Permisos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <form id="FrmRoles_Permission" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Roles y Permisos</h2>

        <!-- ID (Hidden) -->
        <asp:HiddenField ID="HFRol_Permiso" runat="server" />

        <!-- Selección de Roles -->
        <div class="mb-3">
            <asp:Label
                ID="Label3"
                runat="server"
                Text="Seleccione el Rol:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:DropDownList
                ID="DDLRol"
                runat="server"
                CssClass="form-select">
            </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="RFVNombreRol"
                runat="server"
                ControlToValidate="DDLRol"
                InitialValue="0"
                ErrorMessage="Debes seleccionar un Rol."
                ForeColor="Red"
                CssClass="form-text text-danger"></asp:RequiredFieldValidator>
        </div>

        <!-- Selección de Permisos -->
        <div class="mb-3">
            <asp:Label
                ID="Label1"
                runat="server"
                Text="Seleccione el Permiso:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:DropDownList
                ID="DDLPermiso"
                runat="server"
                CssClass="form-select">
            </asp:DropDownList>
            <asp:RequiredFieldValidator
                ID="RFVNombrePermiso"
                runat="server"
                ControlToValidate="DDLPermiso"
                InitialValue="0"
                ErrorMessage="Debes seleccionar un Permiso."
                ForeColor="Red"
                CssClass="form-text text-danger"></asp:RequiredFieldValidator>
        </div>

        <!-- Fecha de Asignación -->
        <div class="mb-3">
            <asp:Label
                ID="Label2"
                runat="server"
                Text="Fecha de asignación:"
                CssClass="form-label fw-bold"></asp:Label>
            <asp:TextBox
                ID="TBper_rol_fecha"
                runat="server"
                TextMode="Date"
                CssClass="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator
                ID="RFTBper_rol_fecha"
                runat="server"
                ControlToValidate="TBper_rol_fecha"
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
                <h3 class="card-title m-0">Lista de Roles y Permisos</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="RolPermisoTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th>ID Rol_Permiso</th>
                            <th>Rol_ID</th>
                            <th>Nombre Rol</th>
                            <th>Permiso ID</th>
                            <th>Nombre Permiso</th>
                            <th>Fecha asignación</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de Roles y Permisos -->
                    </tbody>
                </table>
            </div>
        </div>
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
                                if (showEditButton || showDeleteButton) {
                                    buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                    if (showEditButton) {
                                        buttons += `<button class="btn btn-warning edit-btn" data-id="${row.ID}">Editar</button>`;  // Amarillo
                                    }
                                    if (showDeleteButton) {
                                        buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ID}">Eliminar</button>`;  // Rojo
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
                // Editar un Rol y Permiso
                $('#RolPermisoTable').on('click', '.edit-btn', function () {
                    //const id = $(this).data('id');
                    const rowData = $('#RolPermisoTable').DataTable().row($(this).parents('tr')).data();
                    //alert(JSON.stringify(rowData, null, 2));
                    loadRolesPermisosData(rowData);
                });

                $('#RolPermisoTable').on('click', '.delete-btn', function () {
                    const id = $(this).data('id');// Obtener el ID del Rol Permiso
                    //if (confirm("¿Estás seguro de que deseas eliminar este Permiso?")) {
                    //    deleteRolesPermision(id);// Invoca a la función para eliminar el Rol Permiso
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
                                deleteRolesPermision(id);// Invoca a la función para eliminar 
                                swal("Poof! Eliminado exitosamente!", {
                                    icon: "success",
                                });
                            } else {
                                swal("No se eliminó el registro!");
                            }
                        });
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
                    /* alert("Rol y Permiso eliminado exitosamente.");*/
                    swal('Exitoso', 'Rol - Permiso eliminado exitosamente', 'success');
                },
                error: function () {
                    /* alert("Error al eliminar el Permiso .");*/
                    swal('Error', 'Error al eliminar el Rol - Permiso', 'error');
                }
            });
        }

    </script>
</asp:Content>
