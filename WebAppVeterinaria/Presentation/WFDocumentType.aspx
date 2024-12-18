<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDocumentType.aspx.cs" Inherits="Presentation.WFDocumentType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <form id="FrmDocumentType" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Tipo de Documento</h2>

        <%-- ID --%>
        <asp:HiddenField ID="HFDocumenTypeId" runat="server" />

        <div class="row">
            <!-- Primera columna: Tipo de documento -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Tipo de documento"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLTip_doc_descripcion"
                        runat="server"
                        CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="Cédula de ciudadania">Cédula Ciudadania</asp:ListItem>
                        <asp:ListItem Value="Cédula extranjeria">Cédula extranjeria</asp:ListItem>
                        <asp:ListItem Value="Tarjeta de identidad">Tarjeta de identidad</asp:ListItem>
                        <asp:ListItem Value="Pasaporte">Pasaporte</asp:ListItem>
                        <asp:ListItem Value="Libreta militar">Libreta militar</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="RFVTip_doc_descripcion"
                        runat="server"
                        ControlToValidate="DDLTip_doc_descripcion"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un Tipo de documento."
                        ForeColor="Red"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Botones Guardar y Actualizar -->
            <div class="col-md-6 mb-3">
                <div class="form-group text-center">
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
                </div>
            </div>
        </div>

        <div class="text-center mt-3">
            <asp:Label
                ID="LblMsg"
                runat="server"
                Text=""
                CssClass="text-info"></asp:Label>
        </div>

    </form>

    <br />
    <br />

    <asp:Panel ID="PanelAdmin" runat="server">
        <%-- Lista de veterinarios --%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Tipo de Documento</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="documentTypeTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>

                            <th>ID</th>
                            <th>Tipo Documento</th>
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
    <%--Tipo Documentos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#documentTypeTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFDocumentType.aspx/ListDocumentType",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "ID" },
                    { "data": "DocumentType" },

                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.ID}">Editar</button>`;  // Amarillo, con margen derecho
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

            // Editar un Tipo de documento 
            $('#documentTypeTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#documentTypeTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadProductData(rowData);
            });

            // Eliminar un producto
            $('#documentTypeTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo de documento
                //if (confirm("¿Estás seguro de que deseas eliminar este tipo de documento ?")) {
                //    deleteDocumentType(id);// Invoca a la función para eliminar el producto
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
                            deleteDocumentType(id);// Invoca a la función para eliminar el consultorio
                            swal("Poof! Eliminado exitosamente!", {
                                icon: "success",
                            });
                        } else {
                            swal("No se eliminó el registro!");
                        }
                    });

            });
        });

        // Cargar los datos en los TextBox 
        function loadProductData(rowData) {
            $('#<%= HFDocumenTypeId.ClientID %>').val(rowData.ID);
            $('#<%= DDLTip_doc_descripcion.ClientID %>').val(rowData.DocumentType);


        }

        // Función para eliminar un tipo de documento 
        function deleteDocumentType(id) {
            $.ajax({
                type: "POST",
                url: "WFDocumentType.aspx/deleteDocumentType",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#documentTypeTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    /*alert("Tipo de documento eliminado exitosamente.");*/
                    swal('Exitoso', 'Tipo de documento eliminado exitosamente', 'success');
                },
                error: function () {
                   /* alert("Error al eliminar el tipo de documento .");*/
                    swal('Error', 'Error al eliminar el tipo de documento', 'error');
                }
            });
        }
    </script>

</asp:Content>
