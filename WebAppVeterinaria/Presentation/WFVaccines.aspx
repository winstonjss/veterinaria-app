<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVaccines.aspx.cs" Inherits="Presentation.WFVaccines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <%--formulario Vaccines--%>
  <form id="FrmVaccines" runat="server" class="container mt-4">
      <h2 class="text-center mb-4">Gestión de Vacunas</h2>

        <asp:HiddenField ID="HFVaccinesID" runat="server" />

        <div class="row mb-3">
            <!-- Nombre de la vacuna -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre de la vacuna:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBName"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Nombre de la vacuna"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFName"
                        runat="server"
                        ControlToValidate="TBName"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Tipo de vacuna -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label2" runat="server" Text="Ingrese el tipo de vacuna:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBGuy"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Tipo de vacuna"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFGuy"
                        runat="server"
                        ControlToValidate="TBGuy"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row mb-3">
            <!-- Cantidad de vacuna -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="Ingrese la cantidad de la vacuna:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:TextBox
                        ID="TBAmount"
                        runat="server"
                        CssClass="form-control"
                        Placeholder="Cantidad de vacunas"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFAmount"
                        runat="server"
                        ControlToValidate="TBAmount"
                        ForeColor="Red"
                        Display="Dynamic"
                        ErrorMessage="Este campo es obligatorio"></asp:RequiredFieldValidator>
                </div>
            </div>

            <!-- Diagnósticos -->
            <div class="col-md-6">
                <div class="form-group">
                    <asp:Label ID="Label5" runat="server" Text="Seleccione Diagnósticos:" CssClass="form-label fw-bold"></asp:Label>
                    <asp:DropDownList
                        ID="DDLDiagonoses"
                        runat="server"
                        CssClass="form-select">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFDiagnoses"
                        runat="server"
                        ControlToValidate="DDLDiagonoses"
                        InitialValue="0"
                        ErrorMessage="Debes seleccionar un diagnóstico."
                        ForeColor="Red"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>

        <div class="row">
      

            <!-- Botones de Guardar y Actualizar -->
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
        ID="lblMsg"
        runat="server"
        Text=""
        CssClass="form-text text-info"></asp:Label>
</div>



        </div>
    </form>


    <br />
<br />

    <%-- Lista De Vaccines--%>
   <asp:Panel ID="PanelAdmin" runat="server">
    <div class="card shadow-sm">
        <div class="card-header" style="background-color: #012749; color: white; text-align: center;">
            <h3 class="card-title m-0">Lista de Vacunas</h3>
        </div>
        <div class="card-body table-responsive">
            <table id="VaccinesTable" class="table table-striped table-bordered table-hover">
                <thead class="table-dark text-center">
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
                    <!-- Aquí irán los datos dinámicos de las vacunas -->
                </tbody>
            </table>
        </div>
    </div>
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
                            if (showEditButton || showDeleteButton) {
                                buttons += `<div class="d-flex justify-content-center gap-2">`;  // Centrar y espacio entre botones
                                if (showEditButton) {
                                    buttons += `<button class="edit-btn btn btn-warning me-2" data-id="${row.VaccinesId}">Editar</button>`;  // Amarillo, con margen derecho
                                }
                                if (showDeleteButton) {
                                    buttons += `<button class="delete-btn btn btn-danger" data-id="${row.VaccinesId}">Eliminar</button>`;  // Rojo
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
                //if (confirm("¿Estás seguro de que deseas eliminar este Vacuna?")) {
                //    deleteVaccines(id);// Invoca a la función para eliminar el vacuna
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
                            deleteVaccines(id);// Invoca a la función para eliminar 
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
                    /*alert("Vacuna eliminado exitosamente.");*/
                    swal('Exitoso', 'Vacuna eliminada exitosamente', 'success');
                },
                error: function () {
                    /*alert("Error al eliminar el Vacuna.");*/
                    swal('Error', 'Error al eliminar el Vacuna', 'error');
                }
            });
        }
    </script>
</asp:Content>

