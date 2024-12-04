<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMedicalHistory.aspx.cs" Inherits="Presentation.WFMedicalHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<%--CSS INGRESE LA FECHA DE LA CITA--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmMedicalHistory" runat="server" class="container mt-4">
        <!-- Título del formulario -->
        <h2 class="text-center mb-4">Gestión de Historias Clínicas</h2>

        <asp:HiddenField ID="HFMedicalHistoryID" runat="server" />

        <div class="row">
            <!-- Primera columna: Fecha de la cita -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Ingrese la Fecha de la cita:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBDate"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 14px;"></asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="RFDate"
                        runat="server"
                        ControlToValidate="TBDate"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Segunda columna: Selección de la cita -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label1"
                        runat="server"
                        Text="Seleccione la cita:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLAppoitment"
                        runat="server"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 14px;">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator
                        ID="RFAppoitment"
                        runat="server"
                        ControlToValidate="DDLAppoitment"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar una cita."
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
                ID="lblMsg"
                runat="server"
                Text=""
                CssClass="text-info"></asp:Label>
        </div>
    </form>



    <br />
<br />

    <%--CSS Lista de Historias clinicas--%>
    <%--Lista de Historias clinicas--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Historias clinicas</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="medicalHistoryTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>



                            <th>ID</th>
                            <th>Fecha</th>
                            <th>Descripcion</th>
                            <th>Cita</th>

                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Productos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#medicalHistoryTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFMedicalHistory.aspx/ListMedicalHistory",
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;
                    }
                },
                "columns": [
                    { "data": "MedicalHistoryID" },
                    { "data": "Date" },
                    { "data": "Description" },
                    { "data": "FKAppoitment" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.MedicalHistoryID}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.MedicalHistoryID}">Eliminar</button>`;  // Rojo
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

            $('#medicalHistoryTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#medicalHistoryTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadMedicalHistoryData(rowData);
            });

            // Eliminar un producto
            $('#medicalHistoryTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar historia clinica?")) {
                    deleteMedicalHistory(id);
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadMedicalHistoryData(rowData) {
            $('#<%= HFMedicalHistoryID.ClientID %>').val(rowData.MedicalHistoryID);
            $('#<%= DDLAppoitment.ClientID %>').val(rowData.FKAppoitment);
        }

        // Función para eliminar un producto
        function deleteMedicalHistory(id) {
            $.ajax({
                type: "POST",
                url: "WFMedicalHistory.aspx/DeleteMedicalHistory",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#medicalHistoryTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Historia Clinica eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar historia clinica.");
                }
            });
        }
    </script>
</asp:Content>
