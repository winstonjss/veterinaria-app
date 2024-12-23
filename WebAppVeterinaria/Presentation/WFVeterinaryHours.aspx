<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVeterinaryHours.aspx.cs" Inherits="Presentation.WFVeterinaryHours" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmVeterinaryHours" runat="server" class="container mt-4">
        <h2 class="text-center mb-4">Gestión de Horarios de Veterinario</h2>

        <asp:HiddenField ID="HFVeterinaryHoursID" runat="server" />

        <!-- Fecha de inicio y fecha final -->
        <div class="row mb-3">
            <!-- Fecha de inicio -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de inicio:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBStart_date"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVStart_date"
                        runat="server"
                        ControlToValidate="TBStart_date"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Fecha final -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha final:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBEnd_date"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVEnd_date"
                        runat="server"
                        ControlToValidate="TBEnd_date"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Hora de inicio y hora final -->
        <div class="row mb-3">
            <!-- Hora de inicio -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label3" runat="server" Text="Ingrese la hora de inicio:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBStart_time"
                        runat="server"
                        TextMode="Time"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVStart_time"
                        runat="server"
                        ControlToValidate="TBStart_time"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Hora final -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Ingrese la hora final:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBFinal_time"
                        runat="server"
                        TextMode="Time"
                        CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVFinal_time"
                        runat="server"
                        ControlToValidate="TBFinal_time"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Selección del veterinario -->
        <div class="row mb-3">
            <div class="col-md-12">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Seleccione el veterinario:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLVeterinarian"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVVeterinarian"
                        runat="server"
                        ControlToValidate="DDLVeterinarian"
                        InitialValue=""
                        ErrorMessage="Debes seleccionar un Veterinario."
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-12 text-center">
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
                    ID="LblMsg"
                    runat="server"
                    Text=""
                    CssClass="form-text text-success"></asp:Label>
            </div>

        </div>
    </form>


    <br />
<br />

    <%--Panel para la gestion del Administrador--%>
    <asp:Panel ID="PanelAdmin" runat="server">
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Horarios de Veterinarios</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="veterinaryhoursTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
                        <tr>
                            <th>ID</th>
                            <th>Fecha inicio</th>
                            <th>Fecha final</th>
                            <th>Hora inicio</th>
                            <th>Hora final</th>
                            <th>FkVeterinario</th>
                            <th>Veterinario</th>
                        </tr>
                    </thead>
                    <tbody>
                        <!-- Aquí irán los datos dinámicos de los horarios de veterinarios -->
                    </tbody>
                </table>
            </div>
        </div>
    </asp:Panel>


    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


    <%--Horarios de Veterinarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#veterinaryhoursTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFVeterinaryHours.aspx/ListVeterinaryHours",// Se invoca el WebMethod Listar Horarios de Veterinarios
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON (d: es una variable, abreviacion de dataset)
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de horarios de veterinarios del resultado
                    }
                },
                "columns": [
                    { "data": "VeterinaryHoursID" },
                    { "data": "Start_date" },
                    { "data": "End_date" },
                    { "data": "Start_time" },
                    { "data": "Final_time" },
                    { "data": "FkVeterinarian", "visible": false },
                    { "data": "NameVeterinarian" },
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="edit-btn btn btn-warning me-3" data-id="${row.VeterinaryHoursID}">Editar</button>`;

                            }
                            if (showDeleteButton) {
                                buttons += `<button class="delete-btn btn btn-danger " data-id="${row.VeterinaryHoursID}">Eliminar</button>`;

                            }
                            return buttons;
                        }
                    }
                ],

                //Establece el legunaje, (igual para los demas)
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


            //Funciones para editar, eliminar
            // Editar un horarios de veterinario
            $('#veterinaryhoursTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#veterinaryhoursTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadveterinaryhoursData(rowData);
            });


            // Eliminar un horarios de veterinario
            $('#veterinaryhoursTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del horarios de veterinario
                //if (confirm("¿Estás seguro de que deseas eliminar este horario de veterinario?")) {
                //    deleteVeterinaryHours(id);// Invoca a la función para eliminar el horarios de veterinario
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
                            deleteVeterinaryHours(id);// Invoca a la función para eliminar 
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
        function loadveterinaryhoursData(rowData) {
            $('#<%= HFVeterinaryHoursID.ClientID %>').val(rowData.VeterinaryHoursID);
            $('#<%= TBStart_date.ClientID %>').val(rowData.Start_date);
            $('#<%= TBEnd_date.ClientID %>').val(rowData.End_date);
            $('#<%= TBStart_time.ClientID %>').val(rowData.Start_time);
            $('#<%= TBFinal_time.ClientID %>').val(rowData.Final_time);
            $('#<%= DDLVeterinarian.ClientID %>').val(rowData.FkVeterinarian);
        }

        // Función para eliminar un horarios de veterinario
        function deleteVeterinaryHours(id) {
            $.ajax({
                type: "POST",
                url: "WFVeterinaryHours.aspx/DeleteVeterinaryHours",// Se invoca el WebMethod Eliminar un horarios de veterinario
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#veterinaryhoursTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    /*alert("Horario de veterinario eliminado exitosamente.");*/
                    swal('Exitoso', 'Horario de veterinario eliminado exitosamente', 'success');
                },
                error: function () {
                    /*alert("Error al eliminar el horario de veterinario.");*/
                    swal('Error', 'Error al eliminar el horario de veterinario', 'error');
                }
            });
        }
    </script>
</asp:Content>

