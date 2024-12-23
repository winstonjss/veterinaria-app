<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAnamnensis.aspx.cs" Inherits="Presentation.WFAnamnensis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/gh/habibmhamadi/multi-select-tag@3.1.0/dist/css/multi-select-tag.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <form id="FrmAnamnesis" runat="server" class="container mt-4">


           

        <!-- Título del formulario -->
        <h2 class="text-center mb-4">Gestión de Anamnesis</h2>

        <!-- ID Oculto -->
        <asp:HiddenField ID="HFAnamnesisID" runat="server" />

        <div class="row">
            <!-- Primera columna: Descripción de anamnesis -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Descripción de anamnesis:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBDescription"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Ingrese una descripción"
                        Style="width: 100%;"></asp:TextBox>

                    <asp:RequiredFieldValidator
                        ID="RFDescription"
                        runat="server"
                        ControlToValidate="TBDescription"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Segunda columna: Selección de anamnesis -->
            <div class="col-md-6 mb-3">
                <div class="form-group">
                    <asp:Label
                        ID="Label5"
                        runat="server"
                        Text="Seleccione la anamnesis:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLAppointments"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="RFDDLAppointments"
                        runat="server"
                        ControlToValidate="DDLAppointments"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar una anamnesis."
                        ForeColor="Red"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <br />
            <select name="countries" id="countries" multiple>
        <option value="1">1</option>
        <option value="2">2</option>
        <option value="3">3</option>
        <option value="4">4</option>
        <option value="5">5</option>
</select>
                    <asp:HiddenField ID="hdnSelectedCountries" runat="server" ClientIDMode="Static" />

       <!-- Botones Guardar y Actualizar -->
<div class="text-center mt-4">
    <asp:Button
        ID="BtnSave"
        runat="server"
        Text="Guardar"
        OnClick="BtnSave_Click"
        CssClass="btn btn-success me-2" />
    <asp:Button
        ID="BtnUpdate"
        runat="server"
        Text="Actualizar"
        OnClick="BtnUpdate_Click"
        CssClass="btn btn-primary me-2" />
    <asp:Label
    ID="Label1"
    runat="server"
    Text=""
    CssClass="form-text text-success"></asp:Label>
</div>


        <!-- Mensaje de confirmación o error -->
        <div class="text-center mt-3">
            <asp:Label
                ID="lblMsg"
                runat="server"
                Text=""
                CssClass="form-text text-success"></asp:Label>
        </div>
    </form>





    <%--Lista de anamnesis--%>


    <asp:Panel ID="PanelAdmin" runat="server">
        <%-- Lista de veterinarios --%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Anamnesis</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="AnamnesisTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>

                            <th>ID</th>
                            <th>Descripción</th>
                            <th>ID Cita</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de los veterinarios -->


                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>

    <script src="https://cdn.jsdelivr.net/gh/habibmhamadi/multi-select-tag@3.1.0/dist/js/multi-select-tag.js"></script>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Anamnesis--%>
    <script type="text/javascript">
        $(document).ready(function () {
            //var multiSelectTag = new MultiSelectTag('countries');
             // Aquí colocas tu valor dinámico


            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
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
                    { "data": "Description" },
                    { "data": "FkAppointment" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.AnamnesisID}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.AnamnesisID}">Eliminar</button>`;  // Rojo
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
                    alert("Error al eliminar el Anamnesis.");   }
            });
        }
    </script>

    <script>
        new MultiSelectTag('countries', {
            rounded: true,    // default true
            shadow: true,      // default false
            placeholder: 'Search',  // default Search...
            tagColor: {
                textColor: '#327b2c',
                borderColor: '#92e681',
                bgColor: '#eaffe6',
            },
            onChange: function (values) {
                var selectedCountries = Array.from(
                    document.getElementById('countries').selectedOptions
                ).map(option => option.value).join(',');

                document.getElementById('<%=hdnSelectedCountries.ClientID%>').value = selectedCountries;
            }
        })

        // Añade el evento de cambio
        document.getElementById('countries').addEventListener('change', updateSelectedCountries);
</script>
</asp:Content>
