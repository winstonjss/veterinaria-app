<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFOffice.aspx.cs" Inherits="Presentation.WFOffice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />

    <form runat="server">

        <%--ID--%>
        <asp:HiddenField ID="HFOffice" runat="server" />
        <br />
        <%-- Número de consultorio--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese Número de consultorio "></asp:Label>
        <asp:TextBox ID="TBCon_num_consultorio" runat="server"></asp:TextBox>
        <br />

        <%-- Botones Guardar y actualizar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>

    </form>
    <br />


    <%--Lista de consultorios --%>

    <h2>Lista de Consultorios </h2>
    <table id="officeTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Consultorio</th>

            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Consultorios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#officeTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFOffice.aspx/ListOffice",// Se invoca el WebMethod Listar Consultorios 
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de consultorios del resultado
                    }
                },
                "columns": [
                    { "data": "ID" },
                    { "data": "Consultorio" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.ID}">Editar</button>
                             <button class="delete-btn" data-id="${row.ID}">Eliminar</button>`;
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

            // Editar un Consultorio 
            $('#officeTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#officeTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadOfficeData(rowData);
            });

            // Eliminar un consultorio
            $('#officeTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo de documento 
                if (confirm("¿Estás seguro de que deseas eliminar este consultorio ?")) {
                    deleteoffice(id);// Invoca a la función para eliminar el consultorio
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadOfficeData(rowData) {
            $('#<%= HFOffice.ClientID %>').val(rowData.ID);
           $('#<%= TBCon_num_consultorio.ClientID %>').val(rowData.Consultorio);

        }

        // Función para eliminar un tipo de documento 
        function deleteoffice(id) {
            $.ajax({
                type: "POST",
                url: "WFOffice.aspx/deleteOffice",// Se invoca el WebMethod Eliminar un Consultorio
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#officeTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Consultorio eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el consultorio .");
                }
            });
        }
</script>

</asp:Content>
