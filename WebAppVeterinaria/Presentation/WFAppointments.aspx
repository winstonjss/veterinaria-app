<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAppointments.aspx.cs" Inherits="Presentation.WFAppointments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="FrmAppointments" runat="server">
    <%--Id --%>
    <asp:HiddenField ID="HFAppoitmentID" runat="server" />

    <asp:Label ID="Label1" runat="server" Text="Ingrese la Fecha de la cita"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFDate"
            runat="server"
            ControlToValidate="TBDate"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
    </asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Ingrese la hora de inicio de la cita"></asp:Label>
    <asp:TextBox ID="TBHoraInicio" TextMode="Time" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFHoraInicio"
            runat="server"
            ControlToValidate="TBHoraInicio"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
    </asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="Label3" runat="server" Text="Ingrese la hora final de la cita"></asp:Label>
    <asp:TextBox ID="TBHoraFin" TextMode="Time" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RFHoraFin"
            runat="server"
            ControlToValidate="TBHoraFin"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
    </asp:RequiredFieldValidator>
     <br />
     <asp:Label ID="Label5" runat="server" Text="Seleccione el animal"></asp:Label>
     <asp:DropDownList ID="DDLAnimals" runat="server"></asp:DropDownList>
     <asp:RequiredFieldValidator ID="RFAnimals" runat="server"
            ControlToValidate="DDLAnimals"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un animal."
            ForeColor="Red">
    </asp:RequiredFieldValidator>
     <br />
    <asp:Label ID="Label4" runat="server" Text="Seleccione el veterinario"></asp:Label>
    <asp:DropDownList ID="DDLVeterinario" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RFVeterinario" runat="server"
            ControlToValidate="DDLVeterinario"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un veterinario."
            ForeColor="Red">
    </asp:RequiredFieldValidator>
    <br />

    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    </form>

    <%-- Lista De Citas--%>
    <asp:Panel ID="PanelAdmin" runat="server">
    <h2>Lista de Citas</h2>
    <table id="appoitmentsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>CitaFecha</th>
                <th>CitaHoraInicio</th>
                <th>CitaHoraFinal</th>            
                <th>FKAnimales</th>
                <th>Animal</th>
                <th>FKVeterinarios</th>
                <th>Veterinario</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Citas--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#appoitmentsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFAppointments.aspx/ListAppoitments",// Se invoca el WebMethod Listar Citas
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de citas del resultado
                    }
                },
                "columns": [
                    { "data": "AppoitmentId" },
                    { "data": "AppoitmentDate" },
                    { "data": "AppoitmentHourStart" },
                    { "data": "AppoitmentHourEnd" },                    
                    { "data": "FkAnimal", "visible": false },
                    { "data": "AnimalName" },
                    { "data": "FkVeterinary", "visible": false },
                    { "data": "VeterinaryName" },
                    {
                        "data": null,
                         "render": function (row) {
                             let buttons = '';
                             if (showEditButton) {
                                 buttons += `<button class="edit-btn" data-id="${row.AppoitmentId}">Editar</button>`;
                             }
                             if (showDeleteButton) {
                                 buttons += `<button class="delete-btn" data-id="${row.AppoitmentId}">Eliminar</button>`;
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

            // Editar una cita
            $('#appoitmentsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#appoitmentsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadAppoitmentData(rowData);
            });

            // Eliminar una cita
            $('#appoitmentsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del cita
                if (confirm("¿Estás seguro de que deseas eliminar esta cita?")) {
                    deleteAppoitment(id);// Invoca a la función para eliminar la cita
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadAppoitmentData(rowData) {
            $('#<%= HFAppoitmentID.ClientID %>').val(rowData.AppoitmentId);
            $('#<%= TBDate.ClientID %>').val(rowData.AppoitmentDate);
            $('#<%= TBHoraInicio.ClientID %>').val(rowData.AppoitmentHourStart);
            $('#<%= TBHoraFin.ClientID %>').val(rowData.AppoitmentHourEnd);
            $('#<%= DDLAnimals.ClientID %>').val(rowData.FkAnimal);
            $('#<%= DDLVeterinario.ClientID %>').val(rowData.FkVeterinary);
        }

        // Función para eliminar una cita
        function deleteAppoitment(id) {
            $.ajax({
                type: "POST",
                url: "WFAppointments.aspx/DeleteAppoitment",// Se invoca el WebMethod Eliminar una cita
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#appoitmentsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Cita eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar cita.");
                }
            });
        }
    </script>
</asp:Content>
