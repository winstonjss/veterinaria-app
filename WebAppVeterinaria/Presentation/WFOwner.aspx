<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOwner.aspx.cs" Inherits="Presentation.WFOwner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmOwner" runat="server" class="container mt-4">
<h2 class="text-center mb-4">Gestión de Propietarios</h2>

        <%-- Id propietario --%>
        <asp:HiddenField ID="HFOwnerID" runat="server" />

        <div class="row">
            <!-- Primera columna: Nombre del propietario -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label1"
                        runat="server"
                        Text="Ingrese el nombre del propietario:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBName"
                        runat="server"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 18px;"
                        Placeholder="Ingrese el nombre del propietario aquí"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RFVName"
                        runat="server"
                        ControlToValidate="TBName"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Segunda columna: Teléfono del propietario -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Ingrese el teléfono:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBPhone"
                        runat="server"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 18px;"
                        Placeholder="Ingrese el teléfono aquí"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RFVPhone"
                        runat="server"
                        ControlToValidate="TBPhone"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Selección de usuario -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label3"
                        runat="server"
                        Text="Seleccione el documento del usuario:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLUsers"
                        runat="server"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 18px;">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="RFVUsers"
                        runat="server"
                        ControlToValidate="DDLUsers"
                        InitialValue=""
                        ErrorMessage="Debes seleccionar un Usuario."
                        ForeColor="Red"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Botones: Guardar y Actualizar -->
        <div class="text-center mt-4">
            <asp:Button
                ID="BtnSave"
                runat="server"
                Text="Guardar"
                OnClick="BtnSave_Click"
                CssClass="btn btn-success mx-2" />
            <asp:Button
                ID="BtnUpdate"
                runat="server"
                Text="Actualizar"
                OnClick="BtnUpdate_Click"
                CssClass="btn btn-primary mx-2" />
            <asp:Label
                ID="LblMsg"
                runat="server"
                Text=""
                CssClass="text-info"></asp:Label>
        </div>
    </form>


    <br />
<br />

    <%--Panel para la gestion del Administrador--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Propietarios</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="ownersTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>


                            <th>ID</th>
                            <th>Nombre</th>
                            <th>Telefono</th>
                            <th>FkUsuario</th>
                            <th>Usuario</th>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


    <%--Propietarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
                const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
                $('#ownersTable').DataTable({
                    "processing": true,
                    "serverSide": false,
                    "ajax": {
                        "url": "WFOwner.aspx/ListOwner",// Se invoca el WebMethod Listar Propietarios
                        "type": "POST",
                        "contentType": "application/json",
                        "data": function (d) {
                            return JSON.stringify(d);// Convierte los datos a JSON (d: es una variable, abreviacion de dataset)
                        },
                        "dataSrc": function (json) {
                            return json.d.data;// Obtiene la lista de propietarios del resultado
                        }
                    },
                    "columns": [
                        { "data": "OwnerID" },
                        { "data": "Name" },
                        { "data": "Phone" },
                        { "data": "FkUser", "visible": false },
                        { "data": "NameUser" }, //Verificar si es nombre de usuario o documento de usuario                  
                        {
                            "data": null,
                            "render": function (row) {
                                let buttons = '';
                                if (showEditButton || showDeleteButton) {
                                    buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                    if (showEditButton) {
                                        buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.OwnerID}">Editar</button>`;  // Amarillo, con margen derecho
                                    }
                                    if (showDeleteButton) {
                                        buttons += `<button class="delete-btn btn btn-danger" data-id="${row.OwnerID}">Eliminar</button>`;  // Rojo
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
                // Editar un propietario
                $('#ownersTable').on('click', '.edit-btn', function () {
                    //const id = $(this).data('id');
                    const rowData = $('#ownersTable').DataTable().row($(this).parents('tr')).data();
                    //alert(JSON.stringify(rowData, null, 2));
                    loadOwnerData(rowData);
                });

                // Eliminar un propietario
                $('#ownersTable').on('click', '.delete-btn', function () {
                    const id = $(this).data('id');// Obtener el ID del propietario
                    if (confirm("¿Estás seguro de que deseas eliminar este propietario?")) {
                        deleteOwner(id);// Invoca a la función para eliminar el propietario
                    }
                });
            });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadOwnerData(rowData) {
            $('#<%= HFOwnerID.ClientID %>').val(rowData.OwnerID);
                $('#<%= TBName.ClientID %>').val(rowData.Name);
                $('#<%= TBPhone.ClientID %>').val(rowData.Phone);
                $('#<%= DDLUsers.ClientID %>').val(rowData.FkUser);
        }

        // Función para eliminar un propietario
        function deleteOwner(id) {
            $.ajax({
                type: "POST",
                url: "WFOwner.aspx/DeleteOwner",// Se invoca el WebMethod Eliminar un Propietario
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#ownersTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Propietario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el propietario.");
                }
            });
        }
    </script>
</asp:Content>
