<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDiagnoses.aspx.cs" Inherits="Presentation.WFDiagnoses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--Id --%>
    <form id="FrmDiagnoses" runat="server">
    <asp:HiddenField ID="HFDiagnosesID" runat="server" />

    <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese la clasificacion"></asp:Label>
    <asp:TextBox ID="TBClassification" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFClassification"
    runat="server"
    ControlToValidate="TBClassification"
    ForeColor="Red"
    Display="Dynamic"
    ErrorMessage="Este campo es obligatorio">
</asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="Label1" runat="server" Text="Ingrese el codigo"></asp:Label>
    <asp:TextBox ID="TBCode" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFCode"
    runat="server"
    ControlToValidate="TBCode"
    ForeColor="Red"
    Display="Dynamic"
    ErrorMessage="Este campo es obligatorio">
</asp:RequiredFieldValidator>
    <br />

    <asp:Label ID="Label5" runat="server" Text="Seleccione Anamnesis"></asp:Label>
    <asp:DropDownList ID="DDLAnamnesis" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RFAnamnesis" runat="server"
            ControlToValidate="DDLAnamnesis"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Diagnostico."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
    <br />

    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" Style="height: 26px" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
        </form>
    <%--Lista de Diagnosticos--%>
    <asp:Panel ID="PanelAdmin" runat="server">
    <h2>Lista de Diagnosticos</h2>
    <table id="DiagnosesTable" class="display" style="width: 100%">
        <thead>
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
                            if (showEditButton) {
                                buttons += `<button class="edit-btn" data-id="${row.DiagnosesID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="delete-btn" data-id="${row.DiagnosesID}">Eliminar</button>`;
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
