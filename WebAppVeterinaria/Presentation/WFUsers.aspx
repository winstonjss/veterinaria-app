<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsers.aspx.cs" Inherits="Presentation.WFUsers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <form id="FrmUsers" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HDUserID" runat="server" />
        <%-- Documento--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Documento"></asp:Label>
        <asp:TextBox ID="TBUsu_documento" runat="server"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFUsu_documento"
            runat="server"
            ControlToValidate="TBUsu_documento"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <%-- Correo--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el correo"></asp:Label>
        <asp:TextBox ID="TBUsu_correo" runat="server"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFUsu_correo"
            runat="server"
            ControlToValidate="TBUsu_correo"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />
        <%-- Contraseña --%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la Contraseña"></asp:Label>
        <asp:TextBox ID="TBUsu_contrasena" runat="server" TextMode="Password"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFUsu_contrasena"
            runat="server"
            ControlToValidate="TBUsu_contrasena"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

      <%-- Salt --%>
        <asp:Label ID="Label9" runat="server" Text="Ingrese el salt"></asp:Label>
        <asp:TextBox ID="TBUsu_salt" runat="server"></asp:TextBox>
        <br />


     <%--Estados--%>
        <asp:Label ID="Label4" runat="server" Text="Estado"></asp:Label>
        <asp:DropDownList ID="DDLState" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Activo">Activo</asp:ListItem>
            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
        </asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFVState" runat="server"
            ControlToValidate="DDLState"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Estado."
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />

        <%-- Fecha de creación --%>
        <asp:Label ID="Label8" runat="server" Text="Fecha de creación "></asp:Label>
        <asp:TextBox ID="TBUsu_fecha_creación" runat="server" TextMode="Date"></asp:TextBox>
        <%--Valida que el TextBox este lleno--%>
        <asp:RequiredFieldValidator ID="RFUsu_fecha_creación"
            runat="server"
            ControlToValidate="TBUsu_fecha_creación"
            ForeColor="Red"
            Display="Dynamic"
            ErrorMessage="Este campo es obligatorio">
        </asp:RequiredFieldValidator>
        <br />

        <%-- Rol --%>
        <asp:Label ID="Label5" runat="server" Text="Seleccione el Rol"></asp:Label>
        <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFRol" runat="server"
            ControlToValidate="DDLRol"
            InitialValue="0"
            ErrorMessage="Debes seleccionar un Rol"
            ForeColor="Red">
        </asp:RequiredFieldValidator>

        <br />

        <%-- Tipo documento --%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione tipo documento"></asp:Label>
        <asp:DropDownList ID="DDLTipo_documento" runat="server"></asp:DropDownList>
        <%--Valida que el DropDownList este seleccionado con algun valor--%>
        <asp:RequiredFieldValidator ID="RFTipo_documento" runat="server"
            ControlToValidate="DDLTipo_documento"
            InitialValue="0"
            ErrorMessage="Debes seleccionar el Tipo de documento"
            ForeColor="Red">
        </asp:RequiredFieldValidator>
        <br />
        <%-- Botones Guardar y actualizar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />

    </form>

    <%--Lista de Usuarios --%>
    <asp:Panel ID="PanelAdmin" runat="server">
   <h2>Lista de Usuarios</h2>
   <table id="usersTable" class="display" style="width: 100%">
       <thead>
           <tr>
               <th>ID</th>
               <th>Documento</th>
               <th>Correo</th>
               <th>Contraseña</th>
               <th>Salt</th>     
               <th>Estado</th>
               <th>Fecha_creación</th>
               <th>Rol</th>
               <th>Tipo_documento</th>
           </tr>
       </thead>
       <tbody>

       </tbody>
   </table>
        </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

  <%--  Usuarios--%>
     <script type="text/javascript">
         $(document).ready(function () {
             const showEditButton = '<%= _showEditButton %>' === 'True';
             const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
             $('#usersTable').DataTable({
                 "processing": true,
                 "serverSide": false,
                 "ajax": {
                     "url": "WFUsers.aspx/ListUsers",// Se invoca el WebMethod Listar Usuarios 
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
                     { "data": "UserID" },
                     { "data": "Document" },
                     { "data": "Email" },
                     { "data": "Password" },
                     { "data": "Salt" },
                     { "data": "State" },
                     { "data": "Date" },
                     { "data": "FkRol" },
                     { "data": "FKDocumentType" },
                     {
                         "data": null,
                         "render": function (row) {
                             let buttons = '';
                             if (showEditButton) {
                                 buttons += `<button class="edit-btn" data-id="${row.UserID}">Editar</button>`;
                             }
                             if (showDeleteButton) {
                                 buttons += `<button class="delete-btn" data-id="${row.UserID}">Eliminar</button>`;
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

             // Editar un usuario
             $('#usersTable').on('click', '.edit-btn', function () {
                 //const id = $(this).data('id');
                 const rowData = $('#usersTable').DataTable().row($(this).parents('tr')).data();
                 //alert(JSON.stringify(rowData, null, 2));
                 loadProductData(rowData);
             });

             // Eliminar un Usuario
             $('#usersTable').on('click', '.delete-btn', function () {
                 const id = $(this).data('id');// Obtener el ID del usuario
                 if (confirm("¿Estás seguro de que deseas eliminar este usuario?")) {
                     deleteUser(id);// Invoca a la función para eliminar el usuario
                 }
             });
         });

         // Cargar los datos en los TextBox y DDL para actualizar

         function loadProductData(rowData) {
             $('#<%= HDUserID.ClientID %>').val(rowData.UserID);
             $('#<%= TBUsu_documento.ClientID %>').val(rowData.Document);
             $('#<%= TBUsu_correo.ClientID %>').val(rowData.Email);
            <%-- $('#<%= TBUsu_contrasena.ClientID %>').val(rowData.Password);
             $('#<%= TBUsu_salt.ClientID %>').val(rowData.Salt);--%>
             $('#<%= DDLState.ClientID %>').val(rowData.State);
             $('#<%= TBUsu_fecha_creación.ClientID %>').val(rowData.Date);
             $('#<%= DDLRol.ClientID %>').val(rowData.FkRol);
             $('#<%= DDLTipo_documento.ClientID %>').val(rowData.FKDocumentType);
         }


         // Función para eliminar un producto
         function deleteUser(id) {
             $.ajax({
                 type: "POST",
                 url: "WFUsers.aspx/DeleteUser",// Se invoca el WebMethod Eliminar un Producto
                 contentType: "application/json; charset=utf-8",
                 data: JSON.stringify({ id: id }),
                 success: function (response) {
                     $('#usersTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                     alert("Usuario eliminado exitosamente.");
                 },
                 error: function () {
                     alert("Error al eliminar el Usuario.");
                 }
             });
         }
     </script>
</asp:Content>
