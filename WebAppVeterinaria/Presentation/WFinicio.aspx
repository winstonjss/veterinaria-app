<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFinicio.aspx.cs" Inherits="Presentation.WFinicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://cdn.lineicons.com/5.0/lineicons.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" rel="stylesheet">

    <form id="FrmInicio" runat="server">
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        <div class="container-fluid">
            <div class="row">
                <!-- PRIMERA TARJETA -->
                <div class="col-12 col-sm-6 col-md-3 mb-3">
                    <div class="card border-success mb-3" style="max-width: 18rem;">
                        <div class="card-body text-dark d-flex justify-content-between align-items-center">
                            <!-- Título en el lado izquierdo (color negro) -->
                            <h5 class="card-title mb-0 text-dark">Total Usuarios</h5>
                            <!-- Ícono en el lado derecho (color negro) -->
                            <i class="fas fa-user fs-3 text-dark"></i>
                        </div>
                        <div class="card-body text-dark">
                            <!-- Label de total usuarios (número en color negro) -->
                            <asp:Label ID="LblCantUsu" runat="server" Text="" CssClass="fs-4 fw-bold d-block mt-2 text-dark"></asp:Label>
                        </div>
                        <div class="card-footer bg-transparent border-success text-center">
                            <a class="small-box-footer" href="WFUsers.aspx">Más info
                    <i class="fas fa-chevron-right"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- SEGUNDA TARJETA -->
                <div class="col-12 col-sm-6 col-md-3 mb-3">
                    <div class="card border-success mb-3" style="max-width: 18rem;">
                        <div class="card-body text-dark d-flex justify-content-between align-items-center">
                            <!-- Título en el lado izquierdo (color negro) -->
                            <h5 class="card-title mb-0 text-dark">Total Animales</h5>
                            <!-- Ícono de huella de perro en el lado derecho (color negro) -->
                            <i class="fas fa-paw fs-3 text-dark"></i>
                        </div>
                        <div class="card-body text-dark">
                            <!-- Label de total usuarios (número en color negro) -->
                            <asp:Label ID="LblCantAnim" runat="server" Text="" CssClass="fs-4 fw-bold d-block mt-2 text-dark"></asp:Label>
                        </div>
                        <div class="card-footer bg-transparent border-success text-center">
                            <a class="small-box-footer" href="WFUsers.aspx">Más info
                    <i class="fas fa-chevron-right"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- TERCERA TARJETA (Propietarios) -->
                <div class="col-12 col-sm-6 col-md-3 mb-3">
                    <div class="card border-success mb-3" style="max-width: 18rem;">
                        <div class="card-body text-dark d-flex justify-content-between align-items-center">
                            <!-- Título en el lado izquierdo (color negro) -->
                            <h5 class="card-title mb-0 text-dark">Total Propietarios</h5>
                            <!-- Ícono de propietarios en el lado derecho (color negro) -->
                            <i class="fas fa-users fs-3 text-dark"></i>
                            <!-- Icono de usuarios -->
                        </div>
                        <div class="card-body text-dark">
                            <!-- Label de total propietarios (número en color negro) -->
                            <asp:Label ID="LblCantProp" runat="server" Text="" CssClass="fs-4 fw-bold d-block mt-2 text-dark"></asp:Label>
                        </div>
                        <div class="card-footer bg-transparent border-success text-center">
                            <a class="small-box-footer" href="WFOwner.aspx">Más info
                    <i class="fas fa-chevron-right"></i>
                            </a>
                        </div>
                    </div>
                </div>

                <!-- CUARTA TARJETA (Veterinarios) -->
                <div class="col-12 col-sm-6 col-md-3 mb-3">
                    <div class="card border-success mb-3" style="max-width: 18rem;">
                        <div class="card-body text-dark d-flex justify-content-between align-items-center">
                            <!-- Título en el lado izquierdo (color negro) -->
                            <h5 class="card-title mb-0 text-dark">Total Veterinarios</h5>
                            <!-- Ícono de veterinarios en el lado derecho (color negro) -->
                            <i class="fas fa-user-md fs-3 text-dark"></i>
                            <!-- Icono de veterinario -->
                        </div>
                        <div class="card-body text-dark">
                            <!-- Label de total veterinarios (número en color negro) -->
                            <asp:Label ID="LblCantVet" runat="server" Text="" CssClass="fs-4 fw-bold d-block mt-2 text-dark"></asp:Label>
                        </div>
                        <div class="card-footer bg-transparent border-success text-center">
                            <a class="small-box-footer" href="WFVeterinarian.aspx">Más info
                    <i class="fas fa-chevron-right"></i>
                            </a>
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="row">
            <!-- Gráfica 1 -->
            <div class="col-md-6">
                <div class="card shadow-sm mb-3">
                    <div class="card-header bg-primary text-white d-flex align-items-center">
                        <i class="fas fa-chart-bar me-2"></i>
                        <!-- Ícono de barras -->
                        <span>Resumen de Citas</span>
                    </div>
                    <div class="card-body p-3">
                        <div id="piechart" style="width: 100%; height: 300px;"></div>
                    </div>
                </div>
            </div>

            <!-- Gráfica 2 -->
            <div class="col-md-6">
                <div class="card shadow-sm mb-3">
                    <div class="card-header bg-primary text-white d-flex align-items-center">
                        <i class="fas fa-chart-pie me-2"></i>
                        <%-- <i class="fas fa-chart-line me-2"></i>--%>
                        <span>Total Diagnósticos</span>
                    </div>
                    <div class="card-body p-3">
                        <div id="linechart" style="width: 100%; height: 300px;"></div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Fila adicional para otra gráfica -->
        <div class="row">
            <!-- Gráfica 3 -->
            <div class="col-12">
                <div class="card shadow-sm mb-3">
                    <div class="card-header bg-primary text-white d-flex align-items-center">
                        <i class="fas fa-chart-line me-2"></i>
                        <%--<i class="fas fa-syringe me-2"></i>--%>
                        <span>Vacunas según Diagnóstico</span>
                    </div>
                    <div class="card-body p-3">
                        <div id="linechart1" style="width: 100%; height: 300px;"></div>
                    </div>
                </div>
            </div>
        </div>


    </form>


    <%--JQuery--%>
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" crossorigin="anonymous"></script>
    <!--Load the AJAX API-->
    <script src="https://www.gstatic.com/charts/loader.js"></script>

   
  
    <script type="text/javascript">
        // Función para obtener el nombre del mes
        function getMonthName(monthIndex) {
            const monthNames = [
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            ];
            return monthNames[monthIndex];
        }

        // Carga la API de Google Charts
        google.charts.load('current', { 'packages': ['corechart'] });

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChart);

        // Función para obtener datos desde el WebMethod
        function fetchDataAndDrawChart() {
            $.ajax({
                url: 'WFInicio.aspx/list', // Ajustar con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'MesActual');
                    data.addColumn('number', 'Citas Atendidas');
                    data.addColumn('number', 'Citas Pendientes');

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        // Suponiendo que item.MesActual es de tipo 'YYYY-MM-DD'
                        let dateParts = item.MesActual.split('-'); // Separar por '-'
                        let monthIndex = parseInt(dateParts[1], 10) - 1; // Convertir a índice de mes (0-11)
                        let monthName = getMonthName(monthIndex); // Obtener el nombre del mes

                        data.addRow([monthName, item.CitasUno, item.CitasDos]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Resumen de Citas',
                        chartArea: { width: '70%', height: '75%' }, // Ajuste del área del gráfico
                        colors: ['#28a745', '#dc3545'], // Verde para atendidas, rojo para pendientes
                        hAxis: {
                            title: 'Mes',
                            textStyle: { fontSize: 12 },
                            titleTextStyle: { bold: true }
                        },
                        vAxis: {
                            title: 'Total de Citas',
                            minValue: 0,
                            textStyle: { fontSize: 12 },
                            titleTextStyle: { bold: true }
                        },
                        legend: { position: 'bottom' }, // Leyenda en la parte inferior
                        bar: { groupWidth: '75%' } // Ajusta el ancho de las barras
                    };

                    // Dibuja la gráfica (ColumnChart para barras verticales)
                    var chart = new google.visualization.ColumnChart(document.getElementById('piechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawChart);
</script>

    <%-- Gráfica 2 --%>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(fetchDataAndDrawChart);

        function fetchDataAndDrawChart() {
            $.ajax({
                url: 'WFInicio.aspx/spGraficoLineasRecursosPorDiagnostico',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;

                    // Verifica si hay datos disponibles
                    if (!rawData || rawData.length === 0) {
                        console.error("No hay datos disponibles para graficar.");
                        alert("No hay datos disponibles para graficar.");
                        return;
                    }

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Diagnostico');
                    data.addColumn('number', 'TotalRecursos');

                    // Procesar y agregar datos
                    rawData.forEach(function (item) {
                        data.addRow([item.Diagnostico, parseInt(item.TotalRecursos)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Cantidad de Diagnósticos',
                        pieHole: 0.4, // Hace que sea un Donut Chart
                        is3D: false, // Desactiva el efecto 3D (más parecido a AdminLTE3)
                        pieSliceText: 'percentage', // Mostrar porcentajes en las secciones
                        tooltip: { isHtml: true }, // Tooltip mejorado
                        legend: { position: 'bottom' }, // Leyenda debajo del gráfico
                        colors: ['#36A2EB', '#FF6384', '#FFCE56', '#4BC0C0', '#9966FF'], // Colores personalizados
                        chartArea: { width: '90%', height: '75%' }, // Ajuste del área del gráfico
                    };

                    // Dibujar el gráfico
                    var chart = new google.visualization.PieChart(document.getElementById('linechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawChart);
</script>

    <%-- Gráfica 3 --%>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(fetchDataAndDrawChart);
        function fetchDataAndDrawChart() {
            $.ajax({
                url: 'WFInicio.aspx/selectVaccines',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;
                    //alert(JSON.stringify(rawData, null, 2));
                    if (!rawData || rawData.length === 0) {
                        console.error("No hay datos disponibles para graficar.");
                        //alert("No hay datos disponibles para graficar.");
                        return;
                    }

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Vacuna');
                    data.addColumn('number', 'Cantidad');


                    // Procesar y agregar datos
                    rawData.forEach(function (item) {
                        data.addRow([item.Vacuna + '-' + item.Diagnostico, parseInt(item.Cantidad)]);
                    });

                    var options = {
                        title: 'Cantidad de Vacunas por Diagnóstico',
                        curveType: 'function',
                        legend: { position: 'bottom' },

                        hAxis: {
                            title: 'Vacunas',
                            slantedText: true,
                            slantedTextAngle: 45
                        },
                        vAxis: {
                            title: 'Cantidad de Aplicaciones'
                        },
                        colors: ['#4285F4'],
                        pointSize: 5,
                        lineWidth: 3,
                        animation: {
                            duration: 1000,
                            easing: 'out',
                            startup: true
                        },
                        chartArea: {
                            width: '50%',
                            height: '50%'
                        }
                    };
                    // Dibujar el gráfico
                    var chart = new google.visualization.LineChart(document.getElementById('linechart1'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawChart);

    </script>

</asp:Content>
