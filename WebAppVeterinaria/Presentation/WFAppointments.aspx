<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAppointments.aspx.cs" Inherits="Presentation.WFAppointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <%--Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <form id="FrmAppointments" runat="server" class="container mt-4">
        <!-- Título del formulario -->
        <h2 class="text-center mb-4">Gestión de Citas</h2>


        <!-- Contenedor de formulario -->
        <div class="row">
            <!-- Primera columna -->
            <div class="col-md-6 mb-3">
                <!-- ID de la cita (oculto) -->
                <asp:HiddenField ID="HFAppoitmentID" runat="server" />

                <!-- Fecha de la cita -->
                <div class="form-group">
                    <asp:Label
                        ID="Label1"
                        runat="server"
                        Text="Seleccione la Fecha de la cita"
                        AssociatedControlID="TBDate"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBDate"
                        runat="server"
                        TextMode="Date"
                        CssClass="form-control custom-calendar"
                        Style="width: 200px;"></asp:TextBox>
                    <asp:RequiredFieldValidator
                        ID="RFDate"
                        runat="server"
                        ControlToValidate="TBDate"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>

                <!-- Hora de inicio -->
                <div class="form-group">
                    <asp:Label
                        ID="Label2"
                        runat="server"
                        Text="Ingrese la hora de inicio de la cita"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBHoraInicio"
                        TextMode="Time"
                        runat="server"
                        CssClass="form-control custom-time"
                        Style="width: 200px;"></asp:TextBox>
                </div>

                <!-- Seleccionar el animal -->
                <div class="form-group">
                    <asp:Label
                        ID="Label5"
                        runat="server"
                        Text="Seleccione el animal"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLAnimals"
                        runat="server"
                        CssClass="form-control custom-dropdown"
                        Style="width: 250px;">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="RFAnimals"
                        runat="server"
                        ControlToValidate="DDLAnimals"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un animal."
                        ForeColor="Red"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Segunda columna -->
            <div class="col-md-6 mb-3">
                <!-- Hora de fin -->
                <div class="form-group">
                    <asp:Label
                        ID="Label3"
                        runat="server"
                        Text="Ingrese la hora final de la cita"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBHoraFin"
                        TextMode="Time"
                        runat="server"
                        CssClass="form-control custom-time"
                        Style="width: 200px;"></asp:TextBox>
                </div>

                <!-- Seleccionar veterinario -->
                <div class="form-group">
                    <asp:Label
                        ID="Label4"
                        runat="server"
                        Text="Seleccione el veterinario:"
                        CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLVeterinario"
                        runat="server"
                        CssClass="form-control custom-dropdown"
                        Style="width: 250px;">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator
                        ID="RFVeterinario"
                        runat="server"
                        ControlToValidate="DDLVeterinario"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un veterinario."
                        ForeColor="Red"
                        CssClass="form-text text-danger"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <!-- Botones de Guardar y Actualizar -->
        <div class="text-center mt-3">
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
            <asp:Button 
                ID="BtnMostrarHorarios" 
                runat="server" 
                Text="Horarios veterinarios" 
                CssClass="btn btn-secondary mx-2"
                OnClientClick="abrirVentanaEmergente(); 
                return false;" />
            <asp:Label
                ID="lblMsg"
                runat="server"
                Text=""
                CssClass="form-text text-info"></asp:Label>
            
        </div>

    </form>

    <br />
    <br />

    <%--CSS DE LISTA DE CITAS--%>
    <%-- Lista De Citas--%>



    <asp:Panel ID="PanelAdmin" runat="server">
        <%-- Lista de veterinarios --%>
        <div class="card shadow-sm">
            <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
                <h3 class="card-title m-0">Lista de Citas</h3>
            </div>
            <div class="card-body table-responsive">
                <table id="appoitmentsTable" class="table table-striped table-bordered table-hover">
                    <thead class="table-dark text-center">
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
            </div>
        </div>
    </asp:Panel>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Citas--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            const showMostrarHorariosButton = '<%= _showMostrarHorariosButton %>' === 'True';
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
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.AppoitmentId}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.AppoitmentId}">Eliminar</button>`;  // Rojo
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
                //if (confirm("¿Estás seguro de que deseas eliminar esta cita?")) {
                //    deleteAppoitment(id);// Invoca a la función para eliminar la cita
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
                            deleteAppoitment(id);// Invoca a la función para eliminar el consultorio
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
                    /* alert("Cita eliminada exitosamente.");*/
                    swal('Exitoso', 'Cita eliminada exitosamente', 'success');
                },
                error: function () {
                    swal('Error', 'Error al eliminar la cita', 'error');
                }
            });
        }
    </script>

    <script type="text/javascript">
        function abrirVentanaEmergente() {
            window.open('WFVeterinaryHours.aspx', 'Formulario',
                'width=600,height=400,resizable=yes,scrollbars=yes');
        }
</script>
</asp:Content>
