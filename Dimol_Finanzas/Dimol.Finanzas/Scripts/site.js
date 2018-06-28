// Traducción al español
$(function ($) {
    $.datepicker.regional['es'] = {
        closeText: 'Cerrar',
        prevText: '<Ant',
        nextText: 'Sig>',
        currentText: 'Hoy',
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
        dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
        weekHeader: 'Sm',
        dateFormat: 'dd/mm/yy',
        firstDay: 1,
        isRTL: false,
        showMonthAfterYear: false,
        yearSuffix: ''
    };
    $.datepicker.setDefaults($.datepicker.regional['es']);
});

jQuery.fn.reset = function () {
    $(this).each(function () { this.reset(); });
}

jQuery.fn.extend({
    propAttr: $.fn.prop || $.fn.attr
});

$.validator.methods.number = function (value, element) {
    value = floatValue(value);
    return this.optional(element) || !isNaN(value);
}
$.validator.methods.range = function (value, element, param) {
    value = floatValue(value);
    return this.optional(element) ||
           (value >= param[0] && value <= param[1]);
}

function floatValue(value) {
    return parseFloat(value.replace(",", "."));
}

$body = $("body");

//$(document).on({
//    ajaxStart: function () { $body.addClass("loading"); },
//    ajaxStop: function () { $body.removeClass("loading"); }
//});

function CargarDeudor() {
    $.ajax({
        type: 'POST',
        url: "/Cartera/Buscar/?Ctcid=" + $("#Ctcid").val(),
        async: false,
        success: function (data) {
            //alert(JSON.stringify(data));

            $("#Pais").val(data.IdPais);
            PaisSel();
            $("#Region").val(data.IdRegion);
            RegionSel();
            if (data.Quiebra == "S") {
                $("#lblQuiebra").text("DEUDOR EN QUIEBRA");
            }
            $("#Ciudad").val(data.IdCiudad);
            CiudadSel();
            $("#Direccion").val(data.Direccion);
            $("#EstadoDireccion").val(data.EstadoDireccion);
            $("#Comuna").val(data.IdComuna);
            fnBuscarDocumentosDeudor();
            fnBuscarTelefonosDeudor();
            fnBuscarEmailDeudor();
            fnBuscarHistorialDeudor();
            fnBuscarDocClienteDeudor();
            fnBuscarRolDeudor();
        }
    });
    //alert($("#ctcid").val());
}

function CargarDeudorRut() {
    if ($("#Rut").val() != "") {
        $.ajax({
            type: 'POST',
            url: "/Cartera/GetDeudor/?Rut=" + $("#Rut").val(),
            async: false,
            success: function (data) {
                if (data.rows.length == 1) {
                    row = data.rows[0];
                    $("#Ctcid").val(row.Ctcid);
                    $("#CtcidDialog").val(row.Ctcid);
                    $("#Pclid").val(row.Pclid);
                    $("#Rut").val(row.Rut);
                    $("#Nombre").val(row.NombreFantasia);
                    $("#Cliente").val(row.NombreCliente);
                    $("#Gestor").val(row.Gestor);
                    $("#Gesid").val(row.Gesid);
                    $("#EstadoCpbt").val($("#SituacionCartera").val());
                    CargarDeudor();
                } else {
                    $("#RutDeudorBuscar").val($("#Rut").val());
                    fnBuscarDeudores();
                    $(".ui-dialog-content").dialog().dialog("open");
                }

            }
        });
    }
}

function CargarDeudorRutEvento(e) {
    if (e.keyCode == 13) {
        CargarDeudorRut();
    }
}

function fnPostData() {
    return "v:" + $("#CtcidDialog").val();
}

function fnBuscarDeudores() {
    var newUrl = "/Cartera/GetDeudores/?"
    newUrl += "RutCliente=" + $("#RutCliente").val() + "&NombreCliente=" + $("#NombreClienteBuscar").val() + "&Pclid=" + $("#Pclid").val()
    newUrl += "&Rut=" + $("#RutDeudorBuscar").val() + "&Ctcid=" + $("#Ctcid").val() + "&Nombre=" + $("#NombreBuscar").val() + "&ApellidoPaterno=" + $("#ApellidoPaterno").val()
    newUrl += "&NombreFantasia=" + $("#NombreFantasia").val() + "&Telefono=" + $("#Telefono").val() + "&Email=" + $("#Email").val()
    newUrl += "&Direccion=" + $("#DireccionBuscar").val() + "&Rol=" + $("#Rol").val() + "&SituacionCartera=" + $("#SituacionCartera").val() + "&NumeroCPBT=" + $("#NumeroCPBT").val()
    //newUrl += "&Gestor=" + $("#gesid").val()

    jQuery("#Deudor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarTelefonosDeudor() {
    var newUrl = "/Cartera/GetTelefonos/?"
    newUrl += "Ctcid=" + $("#Ctcid").val() + "&telefono="
    jQuery("#gridTelefonos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarEmailDeudor() {
    var newUrl = "/Cartera/GetEmail/?"
    newUrl += "Ctcid=" + $("#Ctcid").val() + "&email="
    jQuery("#gridEmail").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarDocumentosDeudor() {
    ListarGrupoCpbt();
    var newUrl = "/Cartera/GetCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&SituacionCartera=" + $("#EstadoCpbt").val()

    jQuery("#Documentos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnBuscarHistorialDeudor() {
    var newUrl = "/Cartera/GetHistorial/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val()

    jQuery("#gridHistorial").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnBuscarDocClienteDeudor() {
    var newUrl = "/Cartera/GetDocCliente/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val()

    jQuery("#gridDocCliente").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrl = "/Cartera/GetDocDeudor/?"
    newUrl += "&Ctcid=" + $("#Ctcid").val()

    jQuery("#gridDocDeudor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnBuscarRolDeudor() {
    var newUrl = "/Cartera/GetRol/?"
    newUrl += "&Ctcid=" + $("#Ctcid").val()

    jQuery("#gridRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function DeudorSeleccionado(id) {
    var row = $("#Deudor").getRowData(id);
    //alert(JSON.stringify(row));
    $("#Ctcid").val(row.Ctcid);
    $("#Pclid").val(row.Pclid);
    $("#Rut").val(row.Rut);
    $("#Nombre").val(row.NombreFantasia);
    $("#Cliente").val(row.NombreCliente);
    $("#Gestor").val(row.Gestor);
    $("#Gesid").val(row.Gesid);
    $("#EstadoCpbt").val($("#SituacionCartera").val());
    CargarDeudor();
    $(".ui-dialog-content").dialog().dialog("close");
    $("#frmBuscarDeudor").reset();
    jQuery("#Deudor").jqGrid().setGridParam({ url: "/Cartera/GetDeudores/?NumeroCPBT=-1119999" }).trigger('reloadGrid', [{ page: 1 }])
}

function RolSeleccionado(id) {
    var ids = id.split('|');

    var newUrl = "/Cartera/GetDocRol/?"
    newUrl += "Rolid=" + ids[1];

    jQuery("#gridDocRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    newUrl = "/Cartera/GetEstadoRol/?"
    newUrl += "Rolid=" + ids[1];

    jQuery("#gridEstadosRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    newUrl = "/Cartera/GetArchivosRol/?"
    newUrl += "Ctcid=" + ids[0] + "&Rolid=" + ids[1];

    jQuery("#gridArchivosRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function EstadoSeleccionado() {

    alert($("#SituacionCartera").val()); alert($("#EstadoCpbt").val());
    //$("#EstadoCpbt").val($("#SituacionCartera").val());
}

function PaisSeleccionado(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Finanzas/ListarRegion", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pais: $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la región.' + ex);
        }

    });
}

function PaisSel() {
    PaisSeleccionado($("#Pais"), "Region");
}

function RegionSeleccionada(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Finanzas/ListarCiudad", // we are calling json method
        dataType: 'json',
        async: false,
        data: { region: $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la ciudad.' + ex);
        }

    });
}

function RegionSel() {
    $("#Ciudad").empty();
    $.ajax({
        type: 'POST',
        url: "/Finanzas/ListarCiudad", // we are calling json method
        dataType: 'json',
        async: false,
        data: { region: $("#Region").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#Ciudad").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la ciudad.' + ex);
        }

    });
}

function CiudadSeleccionada(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Finanzas/ListarComuna", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ciudad: $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la comuna.' + ex);
        }

    });
}

function CiudadSel() {
    $("#Comuna").empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarComuna", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ciudad: $("#Ciudad").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#Comuna").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la comuna.' + ex);
        }

    });
}

function ListarGrupoCpbt() {
    TipoCarteraMasiva();
    var newUrl = "/Cartera/ListarGrupoCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&EstadoCpbt=" + $("#EstadoCpbt").val()
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#GrupoCpbt").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar el grupo de los comprobantes.' + ex);
        }

    });
}

function CambioConfiguracionGrillaCpbt(control) {
    switch ($(control).attr("id")) {
        case "chkNegocio":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "NumeroEspecial");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "NumeroEspecial");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkCarga":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "CodigoCargaNombre");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "CodigoCargaNombre");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkFecIng":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaIngreso");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaIngreso");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkFecDoc":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaDocumento");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaDocumento");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkFecVenc":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaVencimiento");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaVencimiento");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkDiasVenc":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "DiasVencido");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "DiasVencido");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkFecPla":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaPlazo");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaPlazo");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkCongInt":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaCalculoInteres");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaCalculoInteres");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkFecUltGest":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "FechaUltimaGestion");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "FechaUltimaGestion");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkIntereses":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "Intereses");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "Intereses");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkHonorarios":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "Honorarios");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "Honorarios");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkGasPrej":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "GastoOtros");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "GastoOtros");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkGasJud":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "GastoJudicial");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "GastoJudicial");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkComentario":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "Comentario");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "Comentario");
                $("#Documentos").setGridWidth(width);
            }
            break;
        case "chkAsegurado":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "SubcarteraNombre");
                $("#Documentos").setGridWidth(width);
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "SubcarteraNombre");
                $("#Documentos").setGridWidth(width);
            }
            break;
    }
}

function TipoCarteraMasiva() {
    var newUrl = "/Cartera/TipoCarteraGrilla/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&EstadoCpbt=" + $("#EstadoCpbt").val()
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        success: function (data) {
            //alert(JSON.stringify(data));
            if (data.tipoCartera == '1') {
                $("#chkHonorarios").attr('checked', false);
                CambioConfiguracionGrillaCpbt($("#chkHonorarios"));
                $("#chkGasPrej").attr('checked', false);
                CambioConfiguracionGrillaCpbt($("#chkGasPrej"));
                $("#chkGasJud").attr('checked', false);
                CambioConfiguracionGrillaCpbt($("#chkGasJud"));
            }
        },
        error: function (ex) {
            alert('Error al recuperar el tipo de carga.' + ex);
        }

    });
}

function returnHyperLink(cellValue, options, rowdata, action) {
    return "<a href='" + cellValue + "' >" + rowdata[3] + "</a>";

}


//DEUDOR
function fnGridBuscarDeudores() {
    var newUrl = "/Cartera/GetBuscarDeudores/?"
    newUrl += "RutCliente=" + $("#RutCliente").val() + "&Direccion=" + $("#DireccionBuscar").val()
    newUrl += "&Rut=" + $("#RutDeudorBuscar").val() + "&Nombre=" + $("#NombreBuscar").val() + "&ApellidoPaterno=" + $("#ApellidoPaterno").val()
    newUrl += "&NombreFantasia=" + $("#NombreFantasia").val() + "&Telefono=" + $("#Telefono").val() + "&Email=" + $("#Email").val()

    jQuery("#gridBuscarDeudores").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}


function BuscarDeudorSeleccionado(id) {
    var url = "/Cartera/Deudores/?idd=" + id;
    window.location.href = url;//'@Url.Action("Deudores", "CarteraController", new{id='+id+'}';

}

function fnGuardarDeudor() {
    var newUrl = "/Cartera/Deudores/"
    var postData = $("#frmDeudor").serializeArray();
    if ($('#frmDeudor').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                $("#Ctcid").val(data);
                if (data != "0") {
                    $('#tabDeudor').tabs('enable', 1);
                    $('#tabDeudor').tabs('enable', 2);
                    $('#tabDeudor').tabs('enable', 3);
                    fnBuscarTelefonosDeudor();
                    fnBuscarEmailDeudor();
                }
            },
            error: function (ex) {
                alert('Error al guardar el deudor.' + ex);
            }

        });
    }
}

//DEUDOR CPBT

function fnBuscarDeudoresCpbt() {
    var newUrl = "/Cartera/GetDeudoresCpbt/?"
    newUrl += "RutCliente=" + $("#RutCliente").val() + "&NombreCliente=" + $("#NombreClienteBuscar").val() + "&Pclid=" + $("#Pclid").val()
    newUrl += "&Rut=" + $("#RutDeudorBuscar").val() + "&Ctcid=" + $("#Ctcid").val() + "&Nombre=" + $("#NombreBuscar").val() + "&ApellidoPaterno=" + $("#ApellidoPaterno").val()
    newUrl += "&NombreFantasia=" + $("#NombreFantasia").val() + "&Telefono=" + $("#Telefono").val() + "&Email=" + $("#Email").val()
    newUrl += "&Direccion=" + $("#DireccionBuscar").val() + "&Rol=" + $("#Rol").val() + "&TipoDocumento=" + $("#TipoDocumento").val() + "&NumeroCPBT=" + $("#NumeroCPBT").val()
    //newUrl += "&Gestor=" + $("#gesid").val()

    jQuery("#gridDeudorCpbt").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function DeudorCpbtSeleccionado(id) {
    var url = "/Cartera/DeudorCpbt/?idd=" + id;
    window.location.href = url;//'@Url.Action("Deudores", "CarteraController", new{id='+id+'}';

}

function CargarClienteEvento(e) {
    if (e.keyCode == 13) {
        //CargarDeudorRut();
    }
}

function CargarDeudorEvento(e) {
    if (e.keyCode == 13) {
        //CargarDeudorRut();
    }
}

function ActualizarTipoCartera(ddl) {
    alert($(ddl).val());
}

function fnLimpiarCpbt() {
    //alert("limpiar");
    $("#frmDeudorCpbt").reset();
}

function fnGuardarCpbt() {
    var newUrl = "/Cartera/GuardarCpbt/"
    var postData = $("#frmDeudorCpbt").serializeArray();
    if ($('#frmDeudorCpbt').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                alert(data);
            },
            error: function (ex) {
                alert('Error al guardar el documento.' + ex);
            }

        });
    }
}

function fnEliminarCpbt() {
    var newUrl = "/Cartera/EliminarCpbt/"
    var postData = $("#frmDeudorCpbt").serializeArray();
    if ($('#frmDeudorCpbt').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                alert(data);
            },
            error: function (ex) {
                alert('Error al eliminar el documento.' + ex);
            }

        });
    }
}

function fnCargarArchivo() {
    $body.addClass("loading");
    var newUrl = "/Cartera/ProcesoCargaMasiva/"
    var postData = $("#frmCargaMasiva").serializeArray();
    if ($('#frmCargaMasiva').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                $body.removeClass("loading");
                for (var i = 0; i <= data.length; i++)
                    $("#grdCargaMasiva").jqGrid('addRowData', i + 1, data[i]);
            },
            error: function (ex) {
                $body.removeClass("loading");
                alert('Error al cargar el archivo.' + ex);
            }

        });
    }
}

function fnLimpiarCarga() {
    $('#frmCargaMasiva').reset();
    $("#imgSubirArchivo").removeClass("ok").removeClass("error");
    $("#btnCargar").attr("disabled", "disabled");
    $("#btnSubmit").removeAttr("disabled");
}

function OnSelectClienteCargaMasiva() {
    ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    ActualizaComboContrato("Pclid", "TipoCartera", "Contrato");
}

function OnSelectTipoCartera() {
    ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    ActualizaComboContrato("Pclid", "TipoCartera", "Contrato");
}

function ActualizaComboCodigoCarga(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarCodigoCarga", // we are calling json method
        dataType: 'json',
        async: false,
        data: { codemp: 1, pclid: $("#" + controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar codigo carga.' + ex);
        }

    });
}

function ActualizaComboContrato(controlOrigen, controlCartera, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarContrato", // we are calling json method
        dataType: 'json',
        async: false,
        data: { codemp: 1, pclid: $("#" + controlOrigen).val(), tipoCartera: $("#" + controlCartera).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar contrato.' + ex);
        }

    });
}

function fnAprobarCarga() {
    var s;
    s = $("#gridAprobarCarga").jqGrid('getGridParam', 'selarrrow');
    var postData = { ids: s };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/AprobarCarga", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData,
            success: function (data) {
                if (data != "" || data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al aprobar la carga.' + ex);
            }

        });
    }
}

function fnCargarArchivoPago() {
    $body.addClass("loading");
    var newUrl = "/Cartera/ProcesoCargaPago/"
    var postData = $("#frmCargaMasiva").serializeArray();
    if ($('#Pclid').val() != "" && $('#Archivo').val() != "") {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                $body.removeClass("loading");
                for (var i = 0; i <= data.length; i++)
                    $("#grdCargaMasiva").jqGrid('addRowData', i + 1, data[i]);
            },
            error: function (ex) {
                $body.removeClass("loading");
                alert('Error al cargar el archivo.' + ex);
            }

        });
        $('#btnCargar').attr('disabled', 'disabled');
    } else {
        alert("Debe seleccionar un cliente y subir el archivo a cargar.");
    }
}

/*   Comprobante       */

function OnSelectClienteComprobante() {
    var cliente = $("#RutNombreCliente").val().split('-');
    $("#RutCliente").val(cliente[0].trim());
    $("#NombreCliente").val(cliente[1].trim());
    //ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    //ActualizaComboContrato("Pclid", "TipoCartera", "Contrato");
}

function fnBuscarComprobantes() {
    var newUrl = "/Cartera/GetComprobantes/?"
    newUrl += "RutCliente=" + $("#RutCliente").val() + "&NombreCliente=" + $("#NombreCliente").val() + "&Pclid=" + $("#Pclid").val()
    newUrl += "&Rut=" + $("#Rut").val() + "&Ctcid=" + $("#Ctcid").val();//+ "&Nombre=" + $("#NombreBuscar").val() + "&ApellidoPaterno=" + $("#ApellidoPaterno").val()
    newUrl += "&NombreFantasia=" + $("#NombreFantasia").val() + "&Telefono=" + $("#Telefono").val() + "&Email=" + $("#Email").val()
    newUrl += "&Direccion=" + $("#Direccion").val() + "&TipoDocumento=" + $("#TipoDocumento").val() + "&Numero=" + $("#Numero").val()
    newUrl += "&Tipo=" + $("#Tipo").val() + "&Cartera=" + $("#Cartera").val()
    jQuery("#gridComprobantes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function ComprobanteSeleccionado(id) {
    var url = "/Cartera/Comprobante/?idd=" + id;
    window.location.href = url;
}

function fnGuardarAceptarComprobantes() {
    var s;
    s = idsOfSelectedRows;//$("#gridAceptarComprobante").jqGrid('getGridParam', 'selarrrow');
    var postData = { ids: s };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/GrabarAceptarComprobantes", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData,
            success: function (data) {
                if (data != "" || data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al aceptar el(los) comprobantes.' + ex);
            }

        });
    }
}

var idsOfSelectedRows = [];

function UpdateIdsOfSelectedRows(id, isSelected) {
    var index = $.inArray(id, idsOfSelectedRows);
    if (!isSelected && index >= 0) {
        idsOfSelectedRows.splice(index, 1); // remove id from the list
    } else if (index < 0) {
        idsOfSelectedRows.push(id);
    }
}

function fnOnSelectAll(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateIdsOfSelectedRows(id, status);
    }
}

function fnOnLoadComplete(grilla) {
    var i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
    }
}


/*   Comprobante       */

//Sub Carteras

function fnBuscarSubcarteras() {
    var newUrl = "/Cartera/GetSubCartera/?"
    newUrl += "rut=" + $("#Rut").val() + "&nombre=" + $("#Nombre").val()

    jQuery("#gridSubCartera").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnGrabarSubCartera() {
    $body.addClass("loading");
    var newUrl = "/Cartera/GuardarSubCartera/"
    var postData = $("#frmSubCartera").serializeArray();
    fnValidateDropdownList($('#Comuna'));

    if ($('#frmSubCartera').valid() && validarut($('#Rut'))) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                if (data === 1) {
                    alert("Sub-Cartera guardada con éxito.");
                    $("#frmSubCartera").reset();
                } else {
                    alert("Sub-Cartera no fue guardada, revise los datos.");
                }
            },
            error: function (ex) {
                alert('Error al guardar sub cartera.' + ex);
            }

        });
    }
}

function SubcarteraSeleccionada(id) {
    var url = "/Cartera/SubCartera/?idd=" + id;
    window.location.href = url;
}

function fnValidateDropdownList(ddlControl) {
    if (ddlControl.val() == '' || ddlControl.val() === null) {
        ddlControl.removeClass("valid");
        ddlControl.addClass("input-validation-error");
        return false;
    } else {
        ddlControl.removeClass("input-validation-error");
        ddlControl.addClass("valid");
        return true;
    }

}

function validarut(Objeto) {
    var tmpstr = "";
    var intlargo = Objeto.val()
    if (intlargo.length > 0) {
        crut = Objeto.val();
        largo = crut.length;
        if (largo < 2) {
            //alert('rut inválido')
            Objeto.focus()
            return false;
        }
        for (i = 0; i < crut.length ; i++)
            if (crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-') {
                tmpstr = tmpstr + crut.charAt(i);
            }
        rut = tmpstr; crut = tmpstr; largo = crut.length;
        if (largo > 2) rut = crut.substring(0, largo - 1);
        else rut = crut.charAt(0); dv = crut.charAt(largo - 1);
        if (rut == null || dv == null)
            return 0;
        var dvr = '0';
        suma = 0;
        mul = 2;
        for (i = rut.length - 1 ; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul;
            if (mul == 7) mul = 2; else mul++;
        }
        res = suma % 11;
        if (res == 1) dvr = 'k';
        else if (res == 0) dvr = '0';
        else {
            dvi = 11 - res;
            dvr = dvi + "";
        }
        if (dvr != dv.toLowerCase()) {
            // alert('El Rut Ingreso es Invalido')
            Objeto.removeClass("valid");
            Objeto.addClass("input-validation-error");
            Objeto.focus()
            return false;
        }
        //alert('El Rut Ingresado es Correcto!')
        Objeto.removeClass("input-validation-error");
        Objeto.addClass("valid");
        Objeto.focus()
        return true;
    }
}

function fnGrabarDocumentoDeudor() {
    $body.addClass("loading");
    var newUrl = "/Cartera/GuardarDocumentoDeudor/"
    var postData = $("#frmDocumentosDeudor").serializeArray();

    if ($('#TipoDocumento').val() != "") {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                if (data === 1) {
                    alert("Documento guardado con éxito.");
                    $("#frmSubCartera").reset();
                    fnBuscarDocumentosDeudor();
                } else {
                    alert("Documento no fue guardado, revise los datos.");
                }
            },
            error: function (ex) {
                alert('Error al guardar Documento.' + ex);
            }

        });
    } else {
        alert("Debe seleccionar tipo documento y archivo a lo menos.");
    }

}

function fnBuscarDocumentosDeudor() {
    if ($("#Ctcid").val() != "") {
        var newUrl = "/Cartera/GetDocumentosDeudor/?"
        newUrl += "Ctcid=" + $("#Ctcid").val() + "&Pclid=" + $("#Pclid").val()

        jQuery("#gridDocumentosDeudor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    } else {
        alert("Debe ingresar un deudor.");
    }
}

function MostrarDocumento(rowid) {
    //$("#dialogContent").html(rowid);
    //$(".ui-dialog").attr("id", "dialog");
    $(".ui-dialog-content").html('<object data="' + rowid + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargarel archivo desde este link <a href="' + rowid + '">.</a></p></object>');
    $(".ui-dialog-content").dialog("open");
}

//RODRIGO GARRIDO//
/*
function fnGuardarProveedorCliente() {
    $body.addClass("loading");
    //var newUrl = "/Finanzas/ProveedorCliente/"
    var postData = $("#frmProveedorCliente").serializeArray();

    if ($('#Tipo').val() != "") {
        $.ajax({
            type: 'POST',
            url: "~/Finanzas/GrabarProveedorCliente/", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData,
            success: function (data) {
                if (data != "" || data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al guardar Cliente.' + ex);
            }

        });
    } else {
        alert("Debe seleccionar tipo cliente y nacionalidad.");
    }

}

*/
function fnGuardarProveedorCliente() {
    var newUrl = "/Finanzas/GrabarProveedorCliente/?"
    //FORMULARIO CLIENTE
    newUrl += "tipo=" + $("#Tipo").val() + "&Nacionalidad=" + $("#Nacionalidad").val() + "&Rut=" + $("#Rut").val()
    + "&Nombre=" + $("#Nombre").val() + "&ApellidoPaterno=" + $("#ApellidoPaterno").val() + "&ApellidoMaterno=" + $("#ApellidoMaterno").val()
    + "&NombreFantasia=" + $("#NombreFantasia").val() + "&Giro=" + $("#Giro").val() + "&FechaIngreso=" + $("#FechaIngreso").val()
    + "&Estado=" + $("#Estado").val() + "&FechaFin=" + $("#FechaFin").val() + "&Transportista=" + $("#Transportista").val()
    + "&Naviera=" + $("#Naviera").val() + "&Comentario=" + $("#Comentario").val() + "&RutRepLegal=" + $("#RutRepLegal").val()
    + "&NombreRepLegal=" + $("#NombreRepLegal").val() + "&Mostrar=" + $("#Mostrar").val() + "&TipoCartera=" + $("#TipoCartera").val()
    + "&CodigoSAP=" + $("#CodigoSAP").val() + "&Usuario=" + $("#Usuario").val()
    //FORMULARIO SUCURSAL
    + "&NombreSucursal=" + $("#NombreSucursal").val() + "&Comuna=" + $("#Comuna").val()
    + "&Direccion=" + $("#Direccion").val() + "&Telefono=" + $("#Telefono").val()
    + "&Fax=" + $("#Fax").val() + "&Correo=" + $("#Correo").val()
    + "&CasaMatriz=" + $("#CasaMatriz").val() + "&Banco=" + $("#Banco").val()
    + "&TipoCuenta=" + $("#TipoCuenta").val() + "&Numero=" + $("#Numero").val() + "&CodigoSucursal=" + $("#CodigoSucursal").val()
    //FORMULARIO IMPUESTO
    + "&Impuesto=" + $("#Impuesto").val()
    //FORMULARIO CONTACTO
    + "&Sucursal=" + $("#Sucursal").val() + "&TipoContacto=" + $("#TipoContacto").val() + "&NombreContacto=" + $("#NombreContacto").val()
    + "&TelefonoContacto=" + $("#TelefonoContacto").val() + "&AnexoContacto=" + $("#AnexoContacto").val()
    + "&FaxContacto=" + $("#FaxContacto").val() + "&CelularContacto=" + $("#CelularContacto").val()
    + "&CorreoContacto=" + $("#CorreoContacto").val()
    //FORMULARIO CUENTA CORRIENTE
    + "&Tipo=" + $("#Tipo").val() + "&FormaDePago=" + $("#FormaDePago").val() + "&UtilizaCredito=" + $("#UtilizaCredito").val()
    + "&LimiteCredito=" + $("#LimiteCredito").val() + "&CreditoConsumido=" + $("#CreditoConsumido").val()
    + "&EstadoCredito=" + $("#EstadoCredito").val() + "&ComentarioCuentaCorriente=" + $("#ComentarioCuentaCorriente").val()
    //FORMULARIO CONTRATO
    + "&ContratoCartera=" + $("#ContratoCartera").val() + "&FechaInicioContrato=" + $("#FechaInicioContrato").val()
    + "&FechaFinContrato=" + $("#FechaFinContrato").val() + "&Indefinido=" + $("#Indefinido").val()
    + "&RutContrato=" + $("#RutContrato").val() + "&NombreContrato=" + $("#NombreContrato").val()
    + "&InteresClientes=" + $("#InteresClientes").val() + "&HonorariosClientes=" + $("#HonorariosClientes").val()

    if ($('#Tipo').val() == "" || $("#Nacionalidad").val() == "" || $("#Rut").val() == "" || $("#Nombre").val() == ""
        || $("#ApellidoPaterno").val() == "" || $("#ApellidoMaterno").val() == "" || $("#NombreSucursal").val() == ""
        || $("#Comuna").val() == "" || $("#Direccion").val() == "" || $("#Telefono").val() == ""
        || $("#Fax").val() == "" || $("#Correo").val() == "" || $("#Impuesto").val() == "" || $("#Sucursal").val() == ""
        || $("#TipoContacto").val() == "" || $("#NombreContacto").val() == "" || $("#TelefonoContacto").val() == ""
        || $("#FormaDePago").val() == "") {

        alert("Debe ingresar todos los datos mandatorios.");
    }
    else {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            success: function (data) {
                if (data != -1) {
                    alert('Cliente guardado exitosamente. ID Cliente : ' + data);
                }
            },
            error: function (ex) {
                alert('Error al guardar Cliente.' + ex);
            }

        });
    }
}

function fnGuardarInsumos() {
    var newUrl = "/Finanzas/GrabarInsumos/?"
    //FORMULARIO DATOS BASICOS
    newUrl += "TiposInsumo=" + $("#TiposInsumo").val() + "&Tipo=" + $("#Tipo").val() + "&Codigo=" + $("#Codigo").val()
    + "&Nombre=" + $("#Nombre").val() + "&EstadoInsumo=" + $("#EstadoInsumo").val() + "&FechaIngreso=" + $("#FechaIngreso").val()
    + "&FechaFin=" + $("#FechaFin").val() + "&Cuenta=" + $("#Cuenta").val() + "&Impuesto=" + $("#Impuesto").val()
    + "&ImputarDeudor=" + $("#ImputarDeudor").val() + "&ImputarCliente=" + $("#ImputarCliente").val()
    + "&GastoJudicial=" + $("#GastoJudicial").val()
    + "&Arancel=" + $("#Arancel").val() + "&PorcentajeArancel=" + $("#PorcentajeArancel").val()
    + "&SuperCategoria=" + $("#SuperCategoria").val() + "&Categoria=" + $("#Categoria").val()
    //FORMULARIO DATOS STOCK
    + "&TipoIngreso=" + $("#TipoIngreso").val() + "&Cubicaje=" + $("#Cubicaje").val()
    + "&Medidas=" + $("#Medidas").val() + "&Alto=" + $("#Alto").val()
    + "&Ancho=" + $("#Ancho").val() + "&Largo=" + $("#Largo").val()
    + "&Perecible=" + $("#Perecible").val() + "&Costo=" + $("#Costo").val()
    + "&CostoPromedio=" + $("#CostoPromedio").val() + "&ProductoFinal=" + $("#ProductoFinal").val()
    + "&MedidasEntrada=" + $("#MedidasEntrada").val() + "&ValorEntrada=" + $("#ValorEntrada").val()
    + "&MedidasSalida=" + $("#MedidasSalida").val() + "&ValorSalida=" + $("#ValorSalida").val()
    + "&Factor=" + $("#Factor").val()

    //FORMULARIO STOCK
    + "&CierreAno=" + $("#CierreAno").val() + "&Total=" + $("#Total").val() + "&Reservado=" + $("#Reservado").val()
    + "&Transito=" + $("#Transito").val() + "&Merma=" + $("#Merma").val()
    + "&Minimo=" + $("#Minimo").val() + "&Maximo=" + $("#Maximo").val()
    + "&Pack=" + $("#Pack").val() + "&PackInterno=" + $("#PackInterno").val()

    //FORMULARIO ESPECIFICACIONES
    + "&TipoProductoEspecificaciones=" + $("#TipoProductoEspecificaciones").val()
    + "&Especificaciones=" + $("#Especificaciones").val()

    if ($('#TiposInsumo').val() == "" || $("#Tipo").val() == "" || $("#Codigo").val() == "" || $("#Nombre").val() == ""
        || $("#SuperCategoria").val() == "" || $("#Categoria").val() == "") {

        alert("Debe ingresar todos los datos mandatorios.");
    }
    else {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            success: function (data) {
                if (data != -1) {
                    alert('Insumo guardado exitosamente.');
                }
            },
            error: function (ex) {
                alert('Error al guardar Insumo.' + ex);
            }

        });
    }
}

    function fnGuardarProductos() {
        var newUrl = "/Finanzas/GrabarProductos/?"
        //FORMULARIO DATOS BASICOS
        newUrl += "Tipo=" + $("#Tipo").val() + "&Codigo=" + $("#Codigo").val()
        + "&Nombre=" + $("#Nombre").val() + "&Estado=" + $("#Estado").val() + "&FechaIngreso=" + $("#FechaIngreso").val()
        + "&FechaFin=" + $("#FechaFin").val() + "&Cuenta=" + $("#Cuenta").val() 
        + "&ImputarDeudor=" + $("#ImputarDeudor").val() + "&ImputarCliente=" + $("#ImputarCliente").val()
        + "&GastoJudicial=" + $("#GastoJudicial").val() + "&Impuesto=" + $("#Impuesto").val()
        + "&HabilitadoVenta=" + $("#HabilitadoVenta").val() + "&ImpuestoEspecifico=" + $("#ImpuestoEspecifico").val()
        + "&SuperCategoria=" + $("#SuperCategoria").val() + "&Categoria=" + $("#Categoria").val()
        + "&CodigoBarra=" + $("#CodigoBarra").val()
        //FORMULARIO MONEDA
        + "&Moneda=" + $("#Moneda").val() + "&Valor=" + $("#Valor").val()
        //FORMULARIO IMPUESTO
        + "&SelImpuesto=" + $("#SelImpuesto").val()

        //FORMULARIO DATOS STOCK
        + "&Medidas=" + $("#Medidas").val() + "&Alto=" + $("#Alto").val()
        + "&Ancho=" + $("#Ancho").val() + "&Largo=" + $("#Largo").val()
        + "&Cubicaje=" + $("#Cubicaje").val()  + "&TipoPeso=" + $("#TipoPeso").val()
        + "&Peso=" + $("#Peso").val()
        + "&Perecible=" + $("#Perecible").val() + "&Costo=" + $("#Costo").val()
        + "&CostoPromedio=" + $("#CostoPromedio").val()
        + "&MedidasEntrada=" + $("#MedidasEntrada").val() + "&ValorEntrada=" + $("#ValorEntrada").val()
        + "&MedidasSalida=" + $("#MedidasSalida").val() + "&ValorSalida=" + $("#ValorSalida").val()
        + "&ProductoArmado=" + $("#ProductoArmado").val() + "&TipoArmado=" + $("#TipoArmado").val()

        //FORMULARIO STOCK
        + "&CierreAno=" + $("#CierreAno").val() + "&Total=" + $("#Total").val() + "&Reservado=" + $("#Reservado").val()
        + "&Transito=" + $("#Transito").val() + "&Merma=" + $("#Merma").val()
        + "&Minimo=" + $("#Minimo").val() + "&Maximo=" + $("#Maximo").val()
        + "&Pack=" + $("#Pack").val() + "&PackInterno=" + $("#PackInterno").val()

       if ($('#TiposInsumo').val() == "" || $("#Tipo").val() == "" || $("#Codigo").val() == "" || $("#Nombre").val() == ""
            || $("#SuperCategoria").val() == "" || $("#Categoria").val() == "") {

            alert("Debe ingresar todos los datos mandatorios.");
        }
        else {
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                success: function (data) {
                    if (data != -1) {
                        alert('Producto guardado exitosamente.');
                    }
                },
                error: function (ex) {
                    alert('Error al guardar Producto.' + ex);
                }

            });
        }
    }

    function fnGuardarComisiones() {
        var newUrl = "/Finanzas/GuardarComisiones/?"
        newUrl += "desde=" + $("#Desde").val() + "&hasta=" + $("#Hasta").val()
        
        if ($('#Desde').val() == "" || $("#Hasta").val() == "" ) {

            alert("Debe ingresar todos los datos mandatorios.");
        }
        else {
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                success: function (data) {
                    if (data != -1) {
                        alert('Comisiones guardadas exitosamente.');
                    }
                },
                error: function (ex) {
                    alert('Error al guardar Comision.' + ex);
                }

            });
        }
    }

    function fnGuardarClausulasContrato() {
        if (document.getElementById('Clonar').checked == 1) {
            $("#Clonar").val(true);
            
        }
        else {

            $("#Clonar").val(false);
        }
        if (document.getElementById('ValorFijo').checked == 1) {
            $("#ValorFijo").val(true);

        }
        else {

            $("#ValorFijo").val(false);
        }
        if (document.getElementById('Capital').checked == 1) {
            $("#Capital").val(true);

        }
        else {

            $("#Capital").val(false);
        }
        if (document.getElementById('Interes').checked == 1) {
            $("#Interes").val(true);

        }
        else {

            $("#Interes").val(false);
        }
        if (document.getElementById('Honorario').checked == 1) {
            $("#Honorario").val(true);

        }
        else {

            $("#Honorario").val(false);
        }
        if (document.getElementById('GastoPrejudicial').checked == 1) {
            $("#GastoPrejudicial").val(true);

        }
        else {

            $("#GastoPrejudicial").val(false);
        }
        if (document.getElementById('GastoJudicial').checked == 1) {
            $("#GastoJudicial").val(true);

        }
        else {

            $("#GastoJudicial").val(false);
        }
        if (document.getElementById('AnulaMaximaConvencional').checked == 1) {
            $("#AnulaMaximaConvencional").val(true);

        }
        else {

            $("#AnulaMaximaConvencional").val(false);
        }
        if (document.getElementById('Rango').checked == 1) {
            $("#Rango").val(true);

        }
        else {

            $("#Rango").val(false);
        }
        
        var newUrl = "/Finanzas/GuardarClausulasContrato/?"
        
        newUrl += "tipo=" + $("#Tipo").val() + "&nombre=" + $("#Nombre").val() + "&area=" + $("#Area").val()
        + "&tipoAplicacion=" + $("#TipoAplicacion").val() + "&valor=" + $("#Valor").val()
        + "&rango=" + $("#Rango").val() + "&valorFijo=" + $("#ValorFijo").val()
        + "&capital=" + $("#Capital").val() + "&interes=" + $("#Interes").val()
        + "&honorario=" + $("#Honorario").val() + "&gastoPrejudicial=" + $("#GastoPrejudicial").val()
        + "&gastoJudicial=" + $("#GastoJudicial").val() + "&anulaMaxima=" + $("#AnulaMaximaConvencional").val()
        + "&tipoRango=" + $("#TipoRango").val() + "&nombreClonar=" + $("#NombreClonar").val()
        + "&clonar=" + $("#Clonar").val() + "&id=" + $("#id").val()
        
        if ($('#Tipo').val() == "" || $("#Nombre").val() == "" || $("#Area").val() == ""
            || $("#TipoAplicacion").val() == "" || $("#TipoRango").val() == "") {

            alert("Debe ingresar todos los datos mandatorios.");
        }
        else {
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                success: function (data) {
                    if (data != -1) {
                        alert('Clausulas guardadas exitosamente.');
                        jQuery("#BuscarClausulasContratoCartera").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
                    }
                },
                error: function (ex) {
                    alert('Error al guardar.' + ex);
                }

            });
        }
    }

    function fnCargarClausulas(id) {

        var newUrl = "/Finanzas/GetClausulas/?idcct=" + id

        //newUrl += "&id=" + id

        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])

        var row = $("#ContratosClienteCartera").getRowData(id);
        //alert(row.tipo)
        $("#Nombre").val(row.cct_nombre);
        $("#idCCT").val(id);
        
        /*
        var tipo = document.getElementById("Tipo");
        setSelectedValue(tipo, row.tipo);
        */
       
    }

    function fnEliminarClausula() {
        var selr = jQuery('#Clausulas').jqGrid('getGridParam', 'selrow')
               
        var newUrl = "/Finanzas/GetClausulas/?id=" + selr + "&idcct=" + $("#idCCT").val() + "&oper=" + 'eliminar'
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
    
       
    }

    function fnAgregarClausula() {
        /*
        var selr = jQuery('#Clausulas').jqGrid('getGridParam', 'selrow')

        var newUrl = "/Finanzas/GetClausulas/?id=" + selr + "&idcct=" + $("#idCCT").val() + "&oper=" + 'eliminar'
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
        */
        //alert($("#idCCT").val())
        $("#idCCT2").val($("#idCCT").val());
        
    }

    function fnAgregarClausula2() {
        /*
        var selr = jQuery('#Clausulas').jqGrid('getGridParam', 'selrow')

        var newUrl = "/Finanzas/GetClausulas/?id=" + selr + "&idcct=" + $("#idCCT").val() + "&oper=" + 'eliminar'
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
        */
        var newUrl = "/Finanzas/GetClausulas/?id=" + $("#ClausulasTodas2").val() + "&idcct=" + $("#idCCT2").val() + "&oper=" + 'agregar'
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
        //$(".ui-dialog-content").dialog().dialog().dialog("close");
        
       // alert($("#ClausulasTodas2").val())
        

    }

    function fnEliminarContratoCartera() {
        var selr = jQuery('#ContratosClienteCartera').jqGrid('getGridParam', 'selrow')
        //alert(selr)
        if (selr != null) {
            var newUrl = "/Finanzas/GetContratosCartera2?id=" + selr + "&oper=" + 'eliminar'
            jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
            location.reload();
        }
        else {
            alert("Debe seleccionar una fila")
        }

       

    }

    function fnGuardarTodoClausulas() {
        /*
        var selr = jQuery('#Clausulas').jqGrid('getGridParam', 'selrow')

        var newUrl = "/Finanzas/GetClausulas/?id=" + selr + "&idcct=" + $("#idCCT").val() + "&oper=" + 'eliminar'
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
        */
        var newUrl = "/Finanzas/GuardarTodoClausulas/?nom=" + $("#Nombre").val() + "&tipo=" + $("#Tipo").val()
        jQuery("#Clausulas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
        //$(".ui-dialog-content").dialog().dialog().dialog("close");

        // alert($("#ClausulasTodas2").val())


    }