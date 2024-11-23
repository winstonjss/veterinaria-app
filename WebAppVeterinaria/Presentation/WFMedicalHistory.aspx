<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMedicalHistory.aspx.cs" Inherits="Presentation.WFMedicalHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmMedicalHistory" runat="server">
    <%--Id --%>
    <asp:HiddenField ID="HFMedicalHistoryID" runat="server" />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese la Fecha de la cita"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFDate"
            runat="server"
            ControlToValidate="TBDate"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
    </asp:RequiredFieldValidator>
    <br />
    <%--Citas--%>
    <asp:Label ID="Label1" runat="server" Text="Selecciones la cita"></asp:Label>
    <asp:DropDownList ID="DDLAppoitment" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RFAppoitment" runat="server"
        ControlToValidate="DDLAppoitment"
        InitialValue="0"
        ErrorMessage="Debes seleccionar una cita."
        ForeColor="Red">
    </asp:RequiredFieldValidator>
    <br />
    <%--Botones--%>
    <div>        
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    </form>
     <%--Lista de Historias clinicas--%>
<h2>Lista de Historias clinicas</h2>
<asp:Panel ID="PanelAdmin" runat="server">
<table id="medicalHistoryTable" class="display" style="width: 100%">
    <thead>
        <tr>
            <th>ID</th>
            <th>Fecha</th>
            <th>Descripcion</th>
            <th>Cita</th>
        </tr>
    </thead>
    <tbody>
    </tbody>
</table>
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
                             if (showEditButton) {
                                 buttons += `<button class="edit-btn" data-id="${row.MedicalHistoryID}">Editar</button>`;
                             }
                             if (showDeleteButton) {
                                 buttons += `<button class="delete-btn" data-id="${row.MedicalHistoryID}">Eliminar</button>`;
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
