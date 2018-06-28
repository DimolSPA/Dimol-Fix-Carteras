/*******************************************
02                TRASPASOS
*******************************************/
function fnCargarTraspasos() {
    jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
}

function fnGuardarTraspasos() {
    var traspasar = $("#gridPorTraspasar").jqGrid('getGridParam', 'selarrrow');

    if (traspasar == "") {
        alert("Debe seleccionar uno o mas deudores para hacer el traspaso.");
    } else {
        var postData = {
            ids: JSON.stringify(traspasar)
        };
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarTraspasos/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                if (data != -1) {
                    $("body").removeClass("loading");
                    jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    fnCargarTraspasosPendientes();
                } else {
                    $("body").removeClass("loading");
                    alert('Error al guardar traspasos.');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar traspasos.' + ex);
            }

        });
    }
}

function fnCargarTraspasosPendientes() {
    var desde = $('#FechaDesde').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    var hasta = $('#FechaHasta').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    var newUrl = "/Judicial/GetTraspasoJudicialHecho/?"
    newUrl += "fechaDesde=" + desde + "&fechaHasta=" + hasta;
    jQuery("#gridTraspasados").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnExcelTraspasosPendientes() {
    var url = "/Judicial/ExcelTraspasosHechos/?fechaDesde=" + $('#FechaDesde').datepicker({ dateFormat: 'dd-mm-yyyy' }).val() + "&fechaHasta=" + $('#FechaHasta').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    window.location.href = url;
}

function fnRevertirTraspasos() {
    var revertir;
    revertir = $("#gridPorRevertir").jqGrid('getGridParam', 'selarrrow');
    //alert(traspasar);
    if (revertir == "" || $("#Estado").val() == "") {
        alert("Debe seleccionar uno o mas documentos para hacer la reversa el traspaso y debe seleccionar un nuevo estado.");
    } else {
        var postData = {
            ids: JSON.stringify(revertir),
            estid: $("#Estado").val(),
            comentario: $("#Comentario").val()
        };
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarReversaTraspasos/", // we are calling json method
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                if (data != -1) {
                    $("body").removeClass("loading");
                    jQuery("#gridTraspasados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    jQuery("#gridPorRevertir").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                } else {
                    $("body").removeClass("loading");
                    alert('Error al guardar traspasos.');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar traspasos.' + ex);
            }
        });
    }
}

function MostrarDocumentosReversa(id, s) {
    var ids = id.split('|');
    var newUrl = "/Judicial/GetDocumentosReversaTraspasoJudicial/?"
    newUrl += "pclid=" + ids[0] + "&ctcid=" + ids[1];
    jQuery("#gridPorRevertir").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}
