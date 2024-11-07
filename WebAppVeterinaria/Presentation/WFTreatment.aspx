<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFTreatment.aspx.cs" Inherits="Presentation.WFTreatment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <asp:HiddenField ID="HFTreatmentID" runat="server" />
     <asp:Label ID="Label4" runat="server" Text="Ingrese el nombre"></asp:Label>
 <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
 <br />
    
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripcion"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
      <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha inicio"></asp:Label>
  <asp:TextBox ID="TBStartDate" runat="server"></asp:TextBox>
  <br />
    <asp:Label ID="Label3" runat="server" Text="Ingrese fecha fin"></asp:Label>
    <asp:TextBox ID="TBEndDate" runat="server"></asp:TextBox>
    <br />
     <asp:Label ID="Label5" runat="server" Text="Seleccione Diadiagnostico"></asp:Label>
     <asp:DropDownList ID="DDLDiagonoses" runat="server"></asp:DropDownList>
     <br />
    <div>        
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" style="height: 26px" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista De Treatment--%>

    <h2>Lista de Tratamientos</h2>
    <table id="TreatmentsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Descripcion</th>
                <th>FechaInicio</th>
                <th>FechaFin</th>
                <th>FkDiagnosticos</th>
                <th>Diagnosticos</th>
                
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Productos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#TreatmentsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFTreatment.aspx/ListTreatment",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "TreatmentId" },
                    { "data": "TreatmentName" },
                    { "data": "TreatmentDescription" },
                    { "data": "TreatmentDateStart" },
                    { "data": "TreatmentDateEnd" },
                    { "data": "FkDiagonoses", "visible": false },
                    { "data": "DiagonosesCode" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.TreatmentId}">Editar</button>
                                 <button class="delete-btn" data-id="${row.TreatmentId}">Eliminar</button>`;
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

            // Editar un producto
            $('#TreatmentsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#TreatmentsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadTreatmentData(rowData);
            });

            // Eliminar un Tratamiento
            $('#TreatmentsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este tratamiento?")) {
                    deleteTreatment(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadTreatmentData(rowData) {
            $('#<%= HFTreatmentID.ClientID %>').val(rowData.TreatmentId);
            $('#<%= TBName.ClientID %>').val(rowData.TreatmentName);
            $('#<%= TBDescription.ClientID %>').val(rowData.TreatmentDescription);
            $('#<%= TBStartDate.ClientID %>').val(rowData.TreatmentDateStart);
            $('#<%= TBEndDate.ClientID %>').val(rowData.TreatmentDateEnd);
            $('#<%= DDLDiagonoses.ClientID %>').val(rowData.FkDiagonoses);
        }

        // Función para eliminar un Tratamiento
        function deleteTreatment(id) {
            $.ajax({
                type: "POST",
                url: "WFTreatment.aspx/DeleteTreatment",// Se invoca el WebMethod Eliminar un Tratamiento
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#TreatmentsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Tratamiento eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Tratamiento.");
                }
            });
        }
    </script>
</asp:Content>

