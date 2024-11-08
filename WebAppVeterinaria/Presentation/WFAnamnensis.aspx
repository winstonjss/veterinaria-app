<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAnamnensis.aspx.cs" Inherits="Presentation.WFAnamnensis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Anamnesis</h2>
    <asp:HiddenField ID="HFAnamnesisID" runat="server" />
    <div class="form-group">
        <asp:Label ID="Label2" runat="server" Text="Descripción de anamnesis:"></asp:Label>
        <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:Label ID="Label5" runat="server" Text="Seleccione la cita:"></asp:Label>
        <asp:DropDownList ID="DDLAppointments" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
    <div class="form-group">        
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" CssClass="btn btn-primary" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-warning" />
        <asp:Label ID="lblMsg" runat="server" Text="" CssClass="text-info"></asp:Label>
    </div>
    <br />
    
    <h2>Lista de Anamnesis</h2>
    <table id="AnamnesisTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Código</th>
                <th>Descripción</th>
                <th>ID Cita</th>            
                <th>Fecha Cita</th>
                <th>Acciones</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var table = $('#AnamnesisTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAnamnensis.aspx/ListAnamnesis",
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
                    { "data": "AnamnesisID" },
                    { "data": "Code" },
                    { "data": "Description" },
                    { "data": "FkAppointment" },
                    { "data": "NameAppointment" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return '<button class="btn btn-sm btn-info edit-btn" data-id="' + row.AnamnesisID + '">Editar</button> ' +
                                '<button class="btn btn-sm btn-danger delete-btn" data-id="' + row.AnamnesisID + '">Eliminar</button>';
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

            $('#AnamnesisTable').on('click', '.edit-btn', function () {
                var rowData = table.row($(this).closest('tr')).data();
                loadAnamnesisData(rowData);
            });

            $('#AnamnesisTable').on('click', '.delete-btn', function () {
                var id = $(this).data('id');
                if (confirm("¿Estás seguro de que deseas eliminar este Anamnesis?")) {
                    deleteAnamnesis(id);
                }
            });
        });

        function loadAnamnesisData(rowData) {
            $('#<%= HFAnamnesisID.ClientID %>').val(rowData.AnamnesisID);
            $('#<%= TBDescription.ClientID %>').val(rowData.Description);
            $('#<%= DDLAppointments.ClientID %>').val(rowData.FkAppointment);
        }

        function deleteAnamnesis(id) {
            $.ajax({
                type: "POST",
                url: "WFAnamnensis.aspx/DeleteAnamnesis",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    if (response.d) {
                        $('#AnamnesisTable').DataTable().ajax.reload();
                        alert("Anamnesis eliminado exitosamente.");
                    } else {
                        alert("Error al eliminar el Anamnesis.");
                    }
                },
                error: function () {
                    alert("Error al eliminar el Anamnesis.");
                }
            });
        }
    </script>
</asp:Content>