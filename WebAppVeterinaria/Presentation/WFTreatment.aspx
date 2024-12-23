<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatment.aspx.cs" Inherits="Presentation.WFTreatment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <!-- Campo de descripción de un Tratamiento -->

    <form id="FrmTreatment" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Tratamientos</h2>

        <asp:HiddenField ID="HFTreatmentID" runat="server" />

        <div class="row mb-3">
            <!-- Nombre del tratamiento -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre del tratamiento:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBName"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Ingrese un nombre de un tratamiento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFName"
                        runat="server"
                        ControlToValidate="TBName"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Descripción -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBDescription"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Ingrese la descripción del tratamiento"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFDescription"
                        runat="server"
                        ControlToValidate="TBDescription"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <!-- Fecha inicio -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha inicio:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBStartDate"
                        TextMode="Date"
                        runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFStartDate"
                        runat="server"
                        ControlToValidate="TBStartDate"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Fecha fin -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Ingrese fecha fin:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBEndDate"
                        TextMode="Date"
                        runat="server"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFEndDate"
                        runat="server"
                        ControlToValidate="TBEndDate"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row mb-4">
            <!-- Selección de Diagnóstico -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Seleccione Diagnóstico:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLDiagonoses"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFDiagonoses"
                        runat="server"
                        ControlToValidate="DDLDiagonoses"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un diagnóstico."
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Botones -->
            <div class="col-md-12 text-center">
                <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" CssClass="btn btn-success me-2" />
                <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click"  CssClass="btn btn-primary me-2" />


                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="form-text text-success"></asp:Label>
            </div>
        </div>
    </form>

    <br />
<br />

    <%-- Lista De tratamientos--%>

    <asp:Panel ID="PanelAdmin" runat="server">
        <h2 class="mb-3">Lista de Tratamientos</h2>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Tratamientos Registrados</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="TreatmentsTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th>ID</th>
                            <th>Nombre</th>
                            <th>Descripción</th>
                            <th>Fecha Inicio</th>
                            <th>Fecha Fin</th>
                            <th>FkDiagnosticos</th>
                            <th>Diagnósticos</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de los tratamientos -->
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Tratamientos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#TreatmentsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFTreatment.aspx/ListTreatment",// Se invoca el WebMethod Listar Tratamientos
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
                    { "data": "TreatmentId" },
                    { "data": "TreatmentName" },
                    { "data": "TreatmentDescription" },
                    { "data": "TreatmentDateStart" },
                    { "data": "TreatmentDateEnd" },
                    { "data": "FkDiagonoses", "visible": false },
                    { "data": "DiagonosesCode" },

                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-3" data-id="${row.TreatmentId}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.TreatmentId}">Eliminar</button>`;  // Rojo
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

            // Editar un producto
            $('#TreatmentsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#TreatmentsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadTreatmentData(rowData);
            });

            // Eliminar un Tratamiento
            $('#TreatmentsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                //if (confirm("¿Estás seguro de que deseas eliminar este tratamiento?")) {
                //    deleteTreatment(id);// Invoca a la función para eliminar el producto
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
                            deleteTreatment(id);// Invoca a la función para eliminar 
                            swal("Poof! Eliminado exitosamente!", {
                                icon: "success",
                            });
                        } else {
                            swal("No se eliminó el registro!");
                        }
                    });

            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadTreatmentData(rowData) {
            $('#<%= HFTreatmentID.ClientID %>').val(rowData.TreatmentId);
            $('#<%= TBName.ClientID %>').val(rowData.TreatmentName);
            $('#<%= TBDescription.ClientID %>').val(rowData.TreatmentDescription);
            $('#<%= TBStartDate.ClientID %>').val(rowData.TreatmentDateStart);
            $('#<%= TBEndDate.ClientID %>').val(rowData.TreatmentDateEnd);
            $('#<%= DDLDiagonoses.ClientID %>').val(rowData.FkDiagonoses);
        }

        // Función para eliminar un Tratamiento
        function deleteTreatment(id) {
            $.ajax({
                type: "POST",
                url: "WFTreatment.aspx/DeleteTreatment",// Se invoca el WebMethod Eliminar un Tratamiento
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#TreatmentsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    /*alert("Tratamiento eliminado exitosamente.");*/
                    swal('Exitoso', 'Tratamiento eliminado exitosamente', 'success');
                },
                error: function () {
                    /*alert("Error al eliminar el Tratamiento.");*/
                    swal('Error', 'Error al eliminar el Tratamiento', 'error');
                }
            });
        }
    </script>
</asp:Content>

