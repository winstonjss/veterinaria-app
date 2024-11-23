<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVaccines.aspx.cs" Inherits="Presentation.WFVaccines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--formulario Vaccines--%>
    <form id="FrmVaccines" runat="server">
        <br />

        <asp:HiddenField ID="HFVaccinesID" runat="server" />
        <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre de la vacuna"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFName"
            runat="server"
            ControlToValidate="TBName"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="Label2" runat="server" Text="Ingrese el tipo de vacuna"></asp:Label>
        <asp:TextBox ID="TBGuy" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFGuy"
            runat="server"
            ControlToValidate="TBGuy"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>

        <br />
        <asp:Label ID="Label1" runat="server" Text="Ingrese la cantidad de la vacuna"></asp:Label>
        <asp:TextBox ID="TBAmount" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFAmount"
            runat="server"
            ControlToValidate="TBAmount"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <asp:Label ID="Label5" runat="server" Text="Seleccione Diagonosticos"></asp:Label>
        <asp:DropDownList ID="DDLDiagonoses" runat="server"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RFDiagnoses" runat="server"
            ControlToValidate="DDLDiagonoses"
            InitialValue="0"
            ErrorMessage="Debes seleccionar una Vacuna."
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
    <%-- Lista De Vaccines--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <h2>Lista de Vacunas</h2>
        <table id="VaccinesTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nombre</th>
                    <th>Tipo</th>
                    <th>Cantidad</th>
                    <th>Fkdiagnosticos</th>
                    <th>Diagnosticos</th>

                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </asp:Panel>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Vacunas--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#VaccinesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFVaccines.aspx/ListVaccines",// Se invoca el WebMethod Listar Vacunas
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de Vacunas del resultado
                    }
                },
                "columns": [
                    { "data": "VaccinesId" },
                    { "data": "VaccinesName" },
                    { "data": "VaccinesGuy" },
                    { "data": "VaccinesAmount" },
                    { "data": "FkDiagonoses", "visible": false },
                    { "data": "DiagonosesCode" },

                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="edit-btn" data-id="${row.VaccinesId}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="delete-btn" data-id="${row.VaccinesId}">Eliminar</button>`;
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

            // Editar un vacuna
            $('#VaccinesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#VaccinesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadVaccinesData(rowData);
            });

            // Eliminar un Vacuna
            $('#VaccinesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del vacuna
                if (confirm("¿Estás seguro de que deseas eliminar este Vacuna?")) {
                    deleteVaccines(id);// Invoca a la función para eliminar el vacuna
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadVaccinesData(rowData) {
            $('#<%= HFVaccinesID.ClientID %>').val(rowData.VaccinesId);
            $('#<%= TBName.ClientID %>').val(rowData.VaccinesName);
            $('#<%= TBGuy.ClientID %>').val(rowData.VaccinesGuy);
            $('#<%= TBAmount.ClientID %>').val(rowData.VaccinesAmount);
            $('#<%= DDLDiagonoses.ClientID %>').val(rowData.FkDiagonoses);
        }

        // Función para eliminar una Vacuna
        function deleteVaccines(id) {
            $.ajax({
                type: "POST",
                url: "WFVaccines.aspx/deleteVaccines",// Se invoca el WebMethod Eliminar una Vacuna
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#VaccinesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Vacuna eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Vacuna.");
                }
            });
        }
    </script>
</asp:Content>

