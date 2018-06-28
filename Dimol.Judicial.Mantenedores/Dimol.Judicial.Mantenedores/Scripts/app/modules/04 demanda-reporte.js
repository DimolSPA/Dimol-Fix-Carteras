/*******************************************
04            DEMANDAS REPORTES
*******************************************/
function ConfiguracionColumnasReportePanelDemanda(reporte) {
    var widthFechaIngresoTribunal = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "FechaIngresoTribunal");
    $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

    var widthFechaEntrega = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "FechaEntrega");
    $("#gridPanelDemandaReporte").setGridWidth(widthFechaEntrega);

    var widthEncargado = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "Encargado");
    $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

    var widthDiasTranscurso = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "DiasTranscurso");
    $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);

    var widthCountCorrecciones = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "CountCorrecciones");
    $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);

    var widthFechaEnvio = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "FechaEnvio");
    $("#gridPanelDemandaReporte").setGridWidth(widthFechaEnvio);

    var widthCorrecciones = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "Correcciones");
    $("#gridPanelDemandaReporte").setGridWidth(widthCorrecciones);

    var widthFechaAsignacion = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "FechaAsignacion");
    $("#gridPanelDemandaReporte").setGridWidth(widthFechaAsignacion);

    var widthFechaAprobacionTraspaso = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "FechaAprobacionTraspaso");
    $("#gridPanelDemandaReporte").setGridWidth(widthFechaAprobacionTraspaso);

    var widthIngresoJudicial = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('showCol', "IngresoJudicial");
    $("#gridPanelDemandaReporte").setGridWidth(widthIngresoJudicial);

    var widthDiasTranscurso2 = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso2");
    $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso2);

    var widthTrackingDemanda = $("#gridPanelDemandaReporte").jqGrid('getGridParam', 'width');
    $("#gridPanelDemandaReporte").jqGrid('hideCol', "TrackingDemanda");
    $("#gridPanelDemandaReporte").setGridWidth(widthTrackingDemanda);

    switch (reporte) {
        case "1":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);

            break;
        case "2":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
        case "3":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
        case "4":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaEnvio");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaEnvio);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaIngresoTribunal");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaEntrega");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaEntrega);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Correcciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCorrecciones);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
        case "5":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "IngresoJudicial");
            $("#gridPanelDemandaReporte").setGridWidth(widthIngresoJudicial);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaIngresoTribunal");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaEntrega");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaEntrega);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Correcciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCorrecciones);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
        case "6":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAsignacion");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAsignacion);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAprobacionTraspaso");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAprobacionTraspaso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "IngresoJudicial");
            $("#gridPanelDemandaReporte").setGridWidth(widthIngresoJudicial);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaIngresoTribunal");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaEntrega");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaEntrega);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
        case "7":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAsignacion");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAsignacion);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAprobacionTraspaso");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAprobacionTraspaso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "IngresoJudicial");
            $("#gridPanelDemandaReporte").setGridWidth(widthIngresoJudicial);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaIngresoTribunal");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "DiasTranscurso");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso);
            break;
        case "8":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAsignacion");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAsignacion);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAprobacionTraspaso");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAprobacionTraspaso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Correcciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCorrecciones);

            $("#gridPanelDemandaReporte").jqGrid('showCol', "DiasTranscurso2");
            $("#gridPanelDemandaReporte").setGridWidth(widthDiasTranscurso2);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);

            $("#gridPanelDemandaReporte").jqGrid('showCol', "TrackingDemanda");
            $("#gridPanelDemandaReporte").setGridWidth(widthTrackingDemanda);
            break;
        case "9":
            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAsignacion");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAsignacion);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaAprobacionTraspaso");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaAprobacionTraspaso);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "IngresoJudicial");
            $("#gridPanelDemandaReporte").setGridWidth(widthIngresoJudicial);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "FechaIngresoTribunal");
            $("#gridPanelDemandaReporte").setGridWidth(widthFechaIngresoTribunal);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Encargado");
            $("#gridPanelDemandaReporte").setGridWidth(widthEncargado);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "Correcciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCorrecciones);

            $("#gridPanelDemandaReporte").jqGrid('hideCol', "CountCorrecciones");
            $("#gridPanelDemandaReporte").setGridWidth(widthCountCorrecciones);
            break;
    }
}

function fnPanelDemandaReporte(reporte, tituloReporte) {
    $("#TipoReportePanelDemanda").val(reporte);
    ConfiguracionColumnasReportePanelDemanda($("#TipoReportePanelDemanda").val());
    $("#gridPanelDemandaReporte").jqGrid('clearGridData');
    var r = $("#ppPanelDemandaReporte").dialog();
    $('#ppPanelDemandaReporte').dialog('option', 'title', tituloReporte);
    $('#ppPanelDemandaReporte').dialog('open');

    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarPanelDemandaReporteOrgChartItem/", // we are calling json method
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: true,
        data: JSON.stringify({ reporteId: reporte }),
        success: function (resultData) {
            if (resultData.length > 0) {
                for (var i = 0; i <= resultData.length; i++)
                    $("#gridPanelDemandaReporte").jqGrid('addRowData', i + 1, resultData[i]);
            }
            //jQuery("#gridPanelDemandaReporte").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert(JSON.stringify(jqXHR));
            //alert(textStatus);
            alert('Error al guardar Avance demanda.' + errorThrown);
        }
    });

}

function fnPanelDemandaMasivaReporte(reporte, tituloReporte) {
    $("#TipoReportePanelDemanda").val(reporte);
    ConfiguracionColumnasReportePanelDemanda($("#TipoReportePanelDemanda").val());
    $("#gridPanelDemandaReporte").jqGrid('clearGridData');
    var r = $("#ppPanelDemandaReporte").dialog();
    $('#ppPanelDemandaReporte').dialog('option', 'title', tituloReporte);
    $('#ppPanelDemandaReporte').dialog('open');

    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarPanelDemandaMasivaReporteOrgChartItem/", // we are calling json method
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: true,
        data: JSON.stringify({ reporteId: reporte }),
        success: function (resultData) {
            if (resultData.length > 0) {
                for (var i = 0; i <= resultData.length; i++)
                    $("#gridPanelDemandaReporte").jqGrid('addRowData', i + 1, resultData[i]);
            }
            //jQuery("#gridPanelDemandaReporte").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert(JSON.stringify(jqXHR));
            //alert(textStatus);
            alert('Error al guardar Avance demanda.' + errorThrown);
        }
    });

}