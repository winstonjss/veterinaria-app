<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFVeterinaryHours.aspx.cs" Inherits="Presentation.WFVeterinaryHours" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

 <%--Estilos--%>
 <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">

        <%--Aqui va todo lo del formulario de horario de veterinario--%>

        <%--Id Horario Veterinario--%>
        <asp:HiddenField ID="HFVeterinaryHoursID" runat="server" />
        <br />
        <%--Fecha de incio del horario de veterinario--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha de incio"></asp:Label>
        <asp:TextBox ID="TBStart_date" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--Fecha final del horario de veterinario--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha final"></asp:Label>
        <asp:TextBox ID="TBEnd_date" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <%--Hora de incio del horario de veterinario--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la hora de incio"></asp:Label>
        <asp:TextBox ID="TBStart_time" runat="server"  TextMode="Time"></asp:TextBox>
        <br />
        <%--Hora final del horario de veterinario--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la hora final"></asp:Label>
        <asp:TextBox ID="TBFinal_time" runat="server"  TextMode="Time"></asp:TextBox>
        <br />

        <%--DDL del veterinario--%>
        <asp:Label ID="Label5" runat="server" Text="Seleccione el veterinario"></asp:Label>
        <asp:DropDownList ID="DDLVeterinarian" runat="server"></asp:DropDownList>
        <br />


        <%--Botones de Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />

            <%--Este Label si se modifica porque lo vamos a utilizar como salida--%>
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>


    <%--Lista de horarios de veterinario--%>
    <h2>Lista de Horarios de Veterinarios</h2>
    <table id="veterinaryhoursTable" class="display" style="width: 100%">
        <thead>
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
        </tbody>
    </table>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


    <%--Horarios de Veterinarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
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
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.VeterinaryHoursID}">Editar</button>
                             <button class="delete-btn" data-id="${row.VeterinaryHoursID}">Eliminar</button>`;
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
                if (confirm("¿Estás seguro de que deseas eliminar este horario de veterinario?")) {
                    deleteVeterinaryHours(id);// Invoca a la función para eliminar el horarios de veterinario
                }
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
                    alert("Horario de veterinario eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el horario de veterinario.");
                }
            });
        }
    </script>
</asp:Content>

