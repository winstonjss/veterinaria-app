<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOffice.aspx.cs" Inherits="Presentation.WFOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <form id="FrmOffice" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Consultorios</h2>

        <!-- Campo oculto para el ID del consultorio -->
        <asp:HiddenField ID="HFOffice" runat="server" />

        <div class="row">
            <!-- Número de consultorio -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label1"
                        runat="server"
                        Text="Ingrese Número de consultorio:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBCon_num_consultorio"
                        runat="server"
                        CssClass="form-control custom-textbox"
                        Style="width: 100%;"
                        Placeholder="Ingrese el número de consultorio aquí">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RFVCon_num_consultorio"
                        runat="server"
                        ControlToValidate="TBCon_num_consultorio"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Botones Guardar y Actualizar -->
            <div class="col-md-12 text-center mt-3">
                <asp:Button
                    ID="BtnSave"
                    runat="server"
                    Text="Guardar"
                    OnClick="BtnSave_Click"
                    CssClass="btn btn-success mx-2 custom-button" />
                <asp:Button
                    ID="BtnUpdate"
                    runat="server"
                    Text="Actualizar"
                    OnClick="BtnUpdate_Click"
                    CssClass="btn btn-primary mx-2 custom-button" />
                <asp:Label
                    ID="LblMsg"
                    runat="server"
                    Text=""
                    CssClass="text-info custom-message">
                </asp:Label>
            </div>
        </div>
    </form>


    <br />
    <br />

    <%--CSS consultorio--%>
    <%-- Lista De consultorio--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Consultorios</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="officeTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>




                            <th>ID</th>
                            <th>Consultorio</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Consultorios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#officeTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFOffice.aspx/ListOffice",// Se invoca el WebMethod Listar Consultorios 
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
                    { "data": "Consultorio" },

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

            // Editar un Consultorio 
            $('#officeTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#officeTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadOfficeData(rowData);
            });

            // Eliminar un consultorio
            $('#officeTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo de documento 
                if (confirm("¿Estás seguro de que deseas eliminar este consultorio ?")) {
                    deleteoffice(id);// Invoca a la función para eliminar el consultorio
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadOfficeData(rowData) {
            $('#<%= HFOffice.ClientID %>').val(rowData.ID);
            $('#<%= TBCon_num_consultorio.ClientID %>').val(rowData.Consultorio);

        }

        // Función para eliminar un tipo de documento 
        function deleteoffice(id) {
            $.ajax({
                type: "POST",
                url: "WFOffice.aspx/deleteOffice",// Se invoca el WebMethod Eliminar un Consultorio
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#officeTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Consultorio eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el consultorio .");
                }
            });
        }
    </script>

</asp:Content>
