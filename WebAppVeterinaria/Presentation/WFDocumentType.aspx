<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFDocumentType.aspx.cs" Inherits="Presentation.WFDocumentType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <form id="FrmDocumentType" runat="server">

        <%--ID--%>
        <asp:HiddenField ID="HFDocumenTypeId" runat="server" />
        <br />
        <%--Documento--%>
        <asp:Label ID="Label2" runat="server" Text="">Tipo de documento</asp:Label>
        <asp:DropDownList ID="DDLTip_doc_descripcion" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Cédula de ciudadania">Cédula Ciudadania</asp:ListItem>
            <asp:ListItem Value="Cédula extranjeria">Cédula extranjeria</asp:ListItem>
            <asp:ListItem Value="Tarjeta de identidad">Tarjeta de identidad</asp:ListItem>
            <asp:ListItem Value="Pasaporte">Pasaporte</asp:ListItem>
            <asp:ListItem Value="Libreta militar">Libreta militar</asp:ListItem>
        </asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algu
     valor--%>
        <asp:RequiredFieldValidator ID="RFVTip_doc_descripcion" runat="server"
            ControlToValidate="DDLTip_doc_descripcion"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Tipo de documento."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />


        <%-- Documento
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Tipo de Documento"></asp:Label>
        <asp:TextBox ID="TBTip_doc_descripcion" runat="server"></asp:TextBox>

         <%--Valida que el TextBox este lleno
        <asp:RequiredFieldValidator ID="RFVDocumentType"
            runat="server"
            ControlToValidate="TBTip_doc_descripcion"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>--%>
        <br />
        <%-- Botones Guardar y actualizar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>

    </form>
    <br />
    <asp:Panel ID="PanelAdmin" runat="server">
    <%--Lista de Tipos de documento --%>
  
    <h2>Lista de Tipo de Documento </h2>
    <table id="documentTypeTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Tipo Documento</th>
               
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
         </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Tipo Documentos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#documentTypeTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFDocumentType.aspx/ListDocumentType",// Se invoca el WebMethod Listar Productos
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
                    { "data": "ID" },
                    { "data": "DocumentType" },
                    
                    {
                        "data": null,
                        "render": function (row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="edit-btn" data-id="${row.ID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="delete-btn" data-id="${row.ID}">Eliminar</button>`;
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

            // Editar un Tipo de documento 
            $('#documentTypeTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#documentTypeTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadProductData(rowData);
            });

            // Eliminar un producto
            $('#documentTypeTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del tipo de documento 
                if (confirm("¿Estás seguro de que deseas eliminar este tipo de documento ?")) {
                    deleteDocumentType(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox 
        function loadProductData(rowData) {
            $('#<%= HFDocumenTypeId.ClientID %>').val(rowData.ID);
            $('#<%= DDLTip_doc_descripcion.ClientID %>').val(rowData.DocumentType);


        }

        // Función para eliminar un tipo de documento 
        function deleteDocumentType(id) {
            $.ajax({
                type: "POST",
                url: "WFDocumentType.aspx/deleteDocumentType",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#documentTypeTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Tipo de documento eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el tipo de documento .");
                }
            });
        }
    </script>

</asp:Content>
