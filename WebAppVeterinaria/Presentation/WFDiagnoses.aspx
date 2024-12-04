<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnoses.aspx.cs" Inherits="Presentation.WFDiagnoses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Id --%>
    <form id="FrmDiagnoses" runat="server" class="container mt-4">
        <!-- Título del formulario -->
        <h2 class="text-center mb-4">Gestión de Diagnósticos</h2>

        <asp:HiddenField ID="HFDiagnosesID" runat="server" />

        <div class="row">
            <!-- Primera columna: Clasificación -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Ingrese la clasificación:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBClassification"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Ingrese la clasificación del diagnóstico aquí"
                        Style="width: 100%; font-size: 14px;"></asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="RFClassification"
                        runat="server"
                        ControlToValidate="TBClassification"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Segunda columna: Código -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label1"
                        runat="server"
                        Text="Ingrese el código:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBCode"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Ingrese el código del diagnóstico aquí"
                        Style="width: 100%; font-size: 14px;"></asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="RFCode"
                        runat="server"
                        ControlToValidate="TBCode"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <!-- Primera columna: Selección de Anamnesis -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label5"
                        runat="server"
                        Text="Seleccione Anamnesis:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLAnamnesis"
                        runat="server"
                        CssClass="form-control"
                        Style="width: 100%; font-size: 14px;">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator
                        ID="RFAnamnesis"
                        runat="server"
                        ControlToValidate="DDLAnamnesis"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un Diagnóstico."
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



    <asp:Panel ID="PanelAdmin" runat="server">
        <%--Lista de Diagnosticos--%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Diagnosticos</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="DiagnosesTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>



                            <th>ID</th>
                            <th>Clasificacion</th>
                            <th>Codigo</th>
                            <th>FkAnamnesis</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Diagnosticos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#DiagnosesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFDiagnoses.aspx/ListDiagnoses",// Se invoca el WebMethod Listar Diagnosticos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de Diagnosticos del resultado
                    }
                },
                "columns": [
                    { "data": "DiagnosesID" },
                    { "data": "Classification" },
                    { "data": "Code" },
                    { "data": "FkAnamnesis", "visible": false },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.DiagnosesID}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.DiagnosesID}">Eliminar</button>`;  // Rojo
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

            // Editar un Diagnostico
            $('#DiagnosesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#DiagnosesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadDiagnosesData(rowData);
            });

            // Eliminar un Diagnostico
            $('#DiagnosesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Diagnostico
                if (confirm("¿Estás seguro de que deseas eliminar este Diagnostico?")) {
                    deleteDiagnostico(id);// Invoca a la función para eliminar el Diagnostico
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadDiagnosesData(rowData) {
            $('#<%= HFDiagnosesID.ClientID %>').val(rowData.DiagnosesID);
            $('#<%= TBClassification.ClientID %>').val(rowData.Classification);
            $('#<%= TBCode.ClientID %>').val(rowData.Code);
            $('#<%= DDLAnamnesis.ClientID %>').val(rowData.FkAnamnesis);
        }

        // Función para eliminar un DiagnosticoContentPlaceHolderID
        function deleteDiagnostico(id) {
            $.ajax({
                type: "POST",
                url: "WFDiagnoses.aspx/deleteDiagnostico",// Se invoca el WebMethod Eliminar un Diagnostico
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#DiagnosesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Diagnostico eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Diagnostico.");
                }
            });
        }
    </script>
</asp:Content>
