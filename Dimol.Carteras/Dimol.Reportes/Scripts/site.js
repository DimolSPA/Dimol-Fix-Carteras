var totalMontoAsignado = 0;
var totalSaldoAsignado = 0;
var totalMontoPorAsignar = 0;
var totalSaldoPorAsignar = 0;
var totalMontoRol = 0;
var totalSaldoRol = 0;
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

function formatThousands(n, dp) {
    var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
    while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
    return s.substr(0, i + 3) + r +
        (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
};

function OnlyNumber(event) {
    var keycode = (event.which) ? event.which : event.keyCode;
    var charcode = event.charCode;
    if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || (keycode >= 48 && keycode <= 57)) && charcode != 46)) {
        event.preventDefault();
    }
}


/* Copyright (c) 2009 José Joaquín Núñez (josejnv@gmail.com) http://joaquinnunez.cl/blog/
 * Licensed under GPL (http://www.opensource.org/licenses/gpl-2.0.php)
 * Use only for non-commercial usage.
 *
 * Version : 0.5
 *
 * Requires: jQuery 1.2+
 */

(function ($) {
    jQuery.fn.Rut = function (options) {
        var defaults = {
            digito_verificador: null,
            on_error: function () { },
            on_success: function () { },
            validation: true,
            format: true,
            format_on: 'change'
        };

        var opts = $.extend(defaults, options);

        return this.each(function () {

            if (defaults.format) {
                jQuery(this).bind(defaults.format_on, function () {
                    jQuery(this).val(jQuery.Rut.formatear(jQuery(this).val(), defaults.digito_verificador == null));
                });
            }
            if (defaults.validation) {
                if (defaults.digito_verificador == null) {
                    jQuery(this).bind('blur', function () {
                        var rut = jQuery(this).val();
                        if (jQuery(this).val() != "" && !jQuery.Rut.validar(rut)) {
                            defaults.on_error();
                        }
                        else if (jQuery(this).val() != "") {
                            defaults.on_success();
                        }
                    });
                }
                else {
                    var id = jQuery(this).attr("id");
                    jQuery(defaults.digito_verificador).bind('blur', function () {
                        var rut = jQuery("#" + id).val() + "-" + jQuery(this).val();
                        if (jQuery(this).val() != "" && !jQuery.Rut.validar(rut)) {
                            defaults.on_error();
                        }
                        else if (jQuery(this).val() != "") {
                            defaults.on_success();
                        }
                    });
                }
            }
        });
    }
})(jQuery);

/**
  Funciones
*/


jQuery.Rut = {

    formatear: function (Rut, digitoVerificador) {
        var sRut = new String(Rut);
        var sRutFormateado = '';
        sRut = jQuery.Rut.quitarFormato(sRut);
        if (digitoVerificador) {
            var sDV = sRut.charAt(sRut.length - 1);
            sRut = sRut.substring(0, sRut.length - 1);
        }
        while (sRut.length > 3) {
            sRutFormateado = "." + sRut.substr(sRut.length - 3) + sRutFormateado;
            sRut = sRut.substring(0, sRut.length - 3);
        }
        sRutFormateado = sRut + sRutFormateado;
        if (sRutFormateado != "" && digitoVerificador) {
            sRutFormateado += "-" + sDV;
        }
        else if (digitoVerificador) {
            sRutFormateado += sDV;
        }

        return sRutFormateado;
    },

    quitarFormato: function (rut) {
        var strRut = new String(rut);
        while (strRut.indexOf(".") != -1) {
            strRut = strRut.replace(".", "");
        }
        while (strRut.indexOf("-") != -1) {
            strRut = strRut.replace("-", "");
        }

        return strRut;
    },

    digitoValido: function (dv) {
        if (dv != '0' && dv != '1' && dv != '2' && dv != '3' && dv != '4'
            && dv != '5' && dv != '6' && dv != '7' && dv != '8' && dv != '9'
            && dv != 'k' && dv != 'K') {
            return false;
        }
        return true;
    },

    digitoCorrecto: function (crut) {
        largo = crut.length;
        if (largo < 2) {
            return false;
        }
        if (largo > 2) {
            rut = crut.substring(0, largo - 1);
        }
        else {
            rut = crut.charAt(0);
        }
        dv = crut.charAt(largo - 1);
        jQuery.Rut.digitoValido(dv);

        if (rut == null || dv == null) {
            return 0;
        }

        dvr = jQuery.Rut.getDigito(rut);

        if (dvr != dv.toLowerCase()) {
            return false;
        }
        return true;
    },

    getDigito: function (rut) {
        var dvr = '0';
        suma = 0;
        mul = 2;
        for (i = rut.length - 1; i >= 0; i--) {
            suma = suma + rut.charAt(i) * mul;
            if (mul == 7) {
                mul = 2;
            }
            else {
                mul++;
            }
        }
        res = suma % 11;
        if (res == 1) {
            return 'k';
        }
        else if (res == 0) {
            return '0';
        }
        else {
            return 11 - res;
        }
    },

    validar: function (texto) {
        texto = jQuery.Rut.quitarFormato(texto);
        largo = texto.length;

        // rut muy corto
        if (largo < 2) {
            return false;
        }

        // verifica que los numeros correspondan a los de rut
        for (i = 0; i < largo; i++) {
            // numero o letra que no corresponda a los del rut
            if (!jQuery.Rut.digitoValido(texto.charAt(i))) {
                return false;
            }
        }

        var invertido = "";
        for (i = (largo - 1), j = 0; i >= 0; i-- , j++) {
            invertido = invertido + texto.charAt(i);
        }
        var dtexto = "";
        dtexto = dtexto + invertido.charAt(0);
        dtexto = dtexto + '-';
        cnt = 0;

        for (i = 1, j = 2; i < largo; i++ , j++) {
            if (cnt == 3) {
                dtexto = dtexto + '.';
                j++;
                dtexto = dtexto + invertido.charAt(i);
                cnt = 1;
            }
            else {
                dtexto = dtexto + invertido.charAt(i);
                cnt++;
            }
        }

        invertido = "";
        for (i = (dtexto.length - 1), j = 0; i >= 0; i-- , j++) {
            invertido = invertido + dtexto.charAt(i);
        }

        if (jQuery.Rut.digitoCorrecto(texto)) {
            return true;
        }
        return false;
    }
};

function fnOcultarMostrarBanner() {
    if ($("#divBanner").is(":visible")) {
        $("#divBanner").hide();
        $("#btnBanner").html('<img src="/Images/botones/show.png" width="25px" height="25px" />')
    } else {
        $("#divBanner").show();
        $("#btnBanner").html('<img src="/Images/botones/hide.png" width="25px" height="25px" />')
    }


}

$("#capa").hover(function () {
    $("#mensaje").show();
}, function () {
    $("#mensaje").hide();
});

function CargarDeudor() {
    $.ajax({
        type: 'POST',
        url: "/Cartera/Buscar/?Ctcid=" + $("#Ctcid").val(),
        async: false,
        beforeSend: function () { $("body").addClass("loading"); },
        success: function (data) {
            //alert(JSON.stringify(data));

            $("#Pais").val(data.IdPais);
            PaisSel();
            $("#Region").val(data.IdRegion);
            RegionSel();
            if (data.Quiebra == "S") {
                $("#lblQuiebra").text("DEUDOR EN QUIEBRA");
                $("#lblPreQuiebra").text("");
            } else {
                $("#lblQuiebra").text("");
                if (data.PreQuiebra == "S") {
                    $("#lblPreQuiebra").text("DEUDOR EN PROCESO DE QUIEBRA");
                } else {
                    $("#lblPreQuiebra").text("");
                }
            }
            $("#Ciudad").val(data.IdCiudad);
            CiudadSel();
            $("#Direccion").val(data.Direccion);
            $("#EstadoDireccion").val(data.EstadoDireccion);
            $("#Categoria").val(data.Categoria);
            $("#Comuna").val(data.IdComuna);
            ListarGrupoCpbt();
            fnBuscarCpbtDeudor();
            fnBuscarTelefonosDeudor();
            fnBuscarEmailDeudor();
            fnBuscarHistorialDeudor();
            fnBuscarDocClienteDeudor();
            fnBuscarRolDeudor();
            CargarImagenesCpbt();
            CargarAgregarHistorial();
            //fnListarDocumentosHistorial();
            ActualizarGestiones();
            fnCargarAsociados();
            //if ($("#Pclid").val() == 522) {
            //    $("#divDirecciones").show();
            //    fnBuscarDireccionDeudor();
            //} else {
            //    $("#divDirecciones").hide();
            //}
            fnBuscarDireccionDeudor();
            fnBuscarSMSPreDeudor();
            fnBuscarEmailPreDeudor();
            fnBuscarObservacionDeudor();
            fnBuscarCausaDeudor();
            $("#TodosSeleccionados").val("0");
        }
    });
    $("body").removeClass("loading");
    //alert($("#ctcid").val());
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

function fnBuscarCpbtDeudor() {
    if ($("#Ctcid").val() == "") {
        $("#Ctcid").val($("#CtcidDialog").val());
    }
    //ListarGrupoCpbt();
    var newUrl = "/Cartera/GetCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&SituacionCartera=" + $("#EstadoCpbt").val()

    jQuery("#Documentos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnBuscarHistorialDeudor() {
    if ($("#Pclid").val() != '' && $("#Ctcid").val() != '') {
        var newUrl = "/Cartera/GetHistorial/?"
        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&tipoHistorial=" + $("#TipoHistorial").val()

        jQuery("#gridHistorial").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnBuscarObservacionDeudor() {
    if ($("#Pclid").val() != '' && $("#Ctcid").val() != '') {
        var newUrl = "/Cartera/GetObservacion/?"
        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&tipoHistorial=A";

        jQuery("#gridObservacion").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnBuscarCausaDeudor() {
    if ($("#Pclid").val() != '' && $("#Ctcid").val() != '') {
        var newUrl = "/Cartera/GetCausaRut/?"
        newUrl += "rut=" + $("#Rut").val();

        jQuery("#gridCausas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}


function fnNotifObservacionDeudor() {
    var count = jQuery("#gridObservacion").getGridParam("records");
    if (count > 0) {
        $("#notObservacion").text(count);
        $("#notObservacion").addClass("nav-counter nav-counter-blue");
    } else {
        $("#notObservacion").text("");
        $("#notObservacion").removeClass("nav-counter nav-counter-blue");
    }
}

function fnNotifCausasDeudor() {
    var count = jQuery("#gridCausas").getGridParam("records");
    if (count > 0) {
        $("#notCausas").text(count);
        $('#tabDetallesDeudor').tabs('enable', 5);
        $("#notCausas").addClass("nav-counter");
    } else {
        $("#notCausas").text("");
        $('#tabDetallesDeudor').tabs('disable', 5);
        $("#notCausas").removeClass("nav-counter");
    }
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

function RolSeleccionado(id) {
    var ids = id.split('|');

    $.ajax({
        type: 'POST',
        url: "/Cartera/GetTotalDocRol", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Rolid: ids[1] },
        success: function (total) {
            totalMontoRol = total.monto;
            totalSaldoRol = total.saldo;
            $("#hddTotalMontoRol").val(total.monto);
            $("#hddTotalSaldoRol").val(total.saldo);
        }

    });

    var newUrl = "/Cartera/GetDocRol/?"
    newUrl += "Rolid=" + ids[1];
    jQuery("#gridDocRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrl = "/Judicial/GetEstados/?"
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
        url: "/Cartera/ListarRegion", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pais: $(controlOrigen).val() == "" ? 0 : $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione Region --</option>');
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
        url: "/Cartera/ListarCiudad", // we are calling json method
        dataType: 'json',
        async: false,
        data: { region: $(controlOrigen).val() == "" ? 0 : $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione Ciudad --</option>');
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
    RegionSeleccionada($("#Region"), "Ciudad");
    //$("#Ciudad").empty();
    //$.ajax({
    //    type: 'POST',
    //    url: "/Cartera/ListarCiudad", // we are calling json method
    //    dataType: 'json',
    //    async: false,
    //    data: { region: $("#Region").val() == "" ? 0 : $("#Region").val() },
    //    // here we are get value of selected country and passing same value as input to json method GetStates.
    //    success: function (states) {
    //        // states contains the JSON formatted list
    //        // of states passed from the controller
    //        $("#" + controlDestino).append('<option value="">-- Seleccione Region --</option>');
    //        $.each(states, function (i, state) {
    //            $("#Ciudad").append('<option value="' + state.Value + '">' +
    //                    state.Text + '</option>');
    //            // here we are adding option for States
    //        });
    //    },
    //    error: function (ex) {
    //        alert('Error al recuperar la ciudad.' + ex);
    //    }

    //});
}

function CiudadSeleccionada(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarComuna", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ciudad: $(controlOrigen).val() == "" ? 0 : $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione Comuna --</option>');
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
    CiudadSeleccionada($("#Ciudad"), "Comuna");
    //$("#Comuna").empty();
    //$.ajax({
    //    type: 'POST',
    //    url: "/Cartera/ListarComuna", // we are calling json method
    //    dataType: 'json',
    //    async: false,
    //    data: { ciudad: $("#Ciudad").val() == "" ? 0 : $("#Ciudad").val() },
    //    // here we are get value of selected country and passing same value as input to json method GetStates.
    //    success: function (states) {
    //        // states contains the JSON formatted list
    //        // of states passed from the controller
    //        $("#" + controlDestino).append('<option value="">-- Seleccione Comuna --</option>');
    //        $.each(states, function (i, state) {
    //            $("#Comuna").append('<option value="' + state.Value + '">' +
    //                    state.Text + '</option>');
    //            // here we are adding option for States
    //        });
    //    },
    //    error: function (ex) {
    //        alert('Error al recuperar la comuna.' + ex);
    //    }

    //});
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
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_Intereses]').show();
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "Intereses");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_Intereses]').hide();
            }
            break;
        case "chkHonorarios":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "Honorarios");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_Honorarios]').show();
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "Honorarios");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_Honorarios]').hide();
            }
            break;
        case "chkGasPrej":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "GastoOtros");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_GastoOtros]').show();
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "GastoOtros");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_GastoOtros]').hide();
            }
            break;
        case "chkGasJud":
            if ($(control).prop('checked')) {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "GastoJudicial");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_GastoJudicial]').show();
            }
            else {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('hideCol', "GastoJudicial");
                $("#Documentos").setGridWidth(width);
                $('#gbox_Documentos .ui-jqgrid-sdiv .ui-jqgrid-hbox .ui-jqgrid-ftable .myfootrow').find('>td[aria-describedby=Documentos_GastoJudicial]').hide();
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
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("#Ctcid").val(data);
                if (data != "0") {
                    $('#tabDeudor').tabs('enable', 1);
                    $('#tabDeudor').tabs('enable', 2);
                    $('#tabDeudor').tabs('enable', 3);
                    fnBuscarTelefonosABMDeudor();
                    fnBuscarEmailABMDeudor();
                    fnBuscarContactosABMDeudor();
                }
                $("body").removeClass("loading");
                $("#btnGuardarDeudor").attr("disabled", "disabled");

            },
            error: function (ex) {
                $("body").removeClass("loading");
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
                if (data != -1) {
                    //alert(data);
                    $("#Ccbid").val(data);
                    alert("Documento guardado.");
                } else {
                    alert("No es posible grabar el documento.");
                }

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
                if (data == -1) {
                    alert("No es posible eliminar el documento.");
                } else {
                    alert("Documento eliminado.");
                }
            },
            error: function (ex) {
                alert('Error al eliminar el documento.' + ex);
            }

        });
    }
}

function fnCargarArchivo() {
    var newUrl = "/Cartera/ProcesoCargaMasiva/"
    var postData = $("#frmCargaMasiva").serializeArray();
    if ($('#frmCargaMasiva').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("body").removeClass("loading");
                if (data.length > 0) {
                    for (var i = 0; i <= data.length; i++)
                        $("#grdCargaMasiva").jqGrid('addRowData', i + 1, data[i]);
                    alert('Archivo cargado con errores');
                } else {
                    alert('Archivo cargado con exito');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
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
    // get IDs of all the rows odf jqGrid 
    var rowIds = $('#grdCargaMasiva').jqGrid('getDataIDs');
    // iterate through the rows and delete each of them
    for (var i = 0, len = rowIds.length; i < len; i++) {
        var currRow = rowIds[i];
        $('#grdCargaMasiva').jqGrid('delRowData', currRow);
    }
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
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Ingrese el cliente.');
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
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Ingrese el cliente y seleccione el codigo de carga.');
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
                jQuery("#gridAprobarCarga").trigger('reloadGrid');
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
    newUrl += "&Direccion=" + $("#Direccion").val() + "&TipoDocumento=" + $("#TipoDocumento").val() + "&Numero=" + $("#Numero").val() + "&NumeroInterno=" + $("#NumeroInterno").val()
    newUrl += "&Tipo=" + $("#Tipo").val() + "&Cartera=" + $("#Cartera").val()
    jQuery("#gridComprobantes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function ComprobanteSeleccionado(id) {
    var ids = id.split('-');
    var url = "/Cartera/Comprobante/?tipo=" + $("#Tipo").val() + "&pag=203&pj=" + $("#Cartera").val() + "&idd=" + ids[0] + "&id=" + ids[1];
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
            async: false,
            data: postData,
            success: function (data) {
                if (data != "" && data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al aceptar el(los) comprobantes.' + ex);
            }

        });
        fnCargarAceptarComprobantes();
    }
}
// codigo multiselect con paginacion
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


//Mail Cocha
/*
var saldoOfSelectedRows = [];
var idsOfDolaresRows = [];
var saldoOfDolaresRows = [];

function UpdateSaldoOfSelectedRows(id, isSelected) {
   
    if ($('#gridDeudorCpbt').jqGrid('getCell', id, 'Moneda') == "PESOS"){
        var index = $.inArray(id, idsOfSelectedRows);
        
        if (!isSelected && index >= 0) {

           
        
            idsOfSelectedRows.splice(index, 1);
            saldoOfSelectedRows.splice(index, 1); // remove id from the list

        } else if (index < 0) {
            idsOfSelectedRows.push(id);
            saldoOfSelectedRows.push(parseFloat($('#gridDeudorCpbt').jqGrid('getCell', id, 'Saldo')));   

          

        }

        $('#Saldo').val(sum(saldoOfSelectedRows));
    }
    else if ($('#gridDeudorCpbt').jqGrid('getCell', id, 'Moneda') == "DOLAR") {

        var index = $.inArray(id, idsOfDolaresRows);
        
        if (!isSelected && index >= 0) {

            idsOfDolaresRows.splice(index, 1);
            saldoOfDolaresRows.splice(index, 1); // remove id from the list

        } else if (index < 0) {

            idsOfDolaresRows.push(id);
            saldoOfDolaresRows.push(parseFloat($('#gridDeudorCpbt').jqGrid('getCell', id, 'Saldo')));
            
        }

        $('#SaldoDolar').val(sum(saldoOfDolaresRows));
    }
}

function fnOnSelectAllCocha(aRowids, status) {
    var i, count, id;
    
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        
        UpdateSaldoOfSelectedRows(id, status);
    }
    $('#Saldo').val(sum(saldoOfSelectedRows));
}

function fnOnLoadCompleteDolares(grilla) {
    var i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
    }

    for (i = 0, count = idsOfDolaresRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfDolaresRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfDolaresRows[i]).prop("checked", true);
    }
}

function sum(rows) {
    var suma = 0;

    for (i = 0; i < rows.length; i++) {
        suma += rows[i];
    }
    
    return suma;
}

function fnBuscarDeudoresCpbtCocha() {

    idsOfSelectedRows = [];    
    saldoOfSelectedRows = [];
    $('#Saldo').val('');
    
    if ($("#Pclid").val() != '') {
        
        var newUrl = "/Email/GetDeudoresCpbt/?"
        newUrl += "RutCliente=" + $("#RutCliente").val() + "&NombreCliente=" + $("#NombreClienteBuscar").val() + "&Pclid=" + $("#Pclid").val()
        newUrl += "&Rut=" + $("#RutDeudorBuscar").val() + "&Ctcid=" + $("#Ctcid").val() + "&Email=" + $("#Email").val()
        
        jQuery("#gridDeudorCpbt").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnEnviarEmailCocha() {
        
    var mails = $("#Email").val().split(","), estadoMail = false;
        
    estadoMail = validarEmail(mails);
    
    if (estadoMail && $("#Pclid").val() != '') {

        if ($("#Monto").val() == $("#Saldo").val() && $("#MontoDolar").val() == $("#SaldoDolar").val()) {

            if ($("#TipoReporte").val() != '') {

                var newUrl = "/Email/EnviarEmailCocha/?"

                var documentosRows = JSON.stringify(jQuery('#gridDeudorCpbt').jqGrid('getGridParam', 'selarrrow'));
                                
                newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Email=" + $("#Email").val() + "&Cuenta=" + $("#Cuenta").val() + "&Banco=" + $("#Banco").val() + "&Saldo=" + $("#Saldo").val() + "&Documentos=" + documentosRows  
                newUrl += "&TipoReporte=" + $("#TipoReporte").val() + "&Email=" + $("#Email").val() + "&Monto=" + $("#Monto").val() + "&SaldoDolar=" + $("#SaldoDolar").val() + "&MontoDolar=" + $("#MontoDolar").val()
                
                $.ajax({
                    type: 'POST',
                    url: newUrl, // we are calling json method
                    dataType: 'json',
                    async: true,
                    beforeSend: function () { $("body").addClass("loading"); },
                    success: function (data) {
                        $("body").removeClass("loading");
                        if (data === false) {
                            alert("Correos enviados con éxito.");
                        } else {
                            alert("Correos enviados con éxito.");
                        }
                    },
                    error: function (ex) {
                        alert('Error al enviar correos.' + ex);
                    }
            
                });
            }
            else {
                alert("Debe seleccionar un tipo de reporte");
            }
        }
        else {
            alert("El monto y el saldo deben ser iguales");
        }
    }
    else {
        alert("Debe ingresar el cliente y/o email");
    }
}

function validarEmail(email) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    for (var i = 0; i < email.length; i++) {
        if (!expr.test(email[i].trim())){
            alert("Error: La dirección de correo " + email[i] + " es incorrecta.");
            return false;
        }      
    }

    return true;
}

function fnCochaOnCreate() {
    $("#Pclid").val(22);
    $('#TipoReporte > option[value="1"]').attr('selected', 'selected');
    fnBuscarDeudoresCpbtCocha();
}
*/
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
    var r = confirm("Desea eliminar la subcartera?\n Para editar, seleccione Cancelar.");
    if (r == true) {
        var url = "/Cartera/EliminarSubCartera/?id=" + id;
        $.ajax({
            type: 'POST',
            url: url, // we are calling json method
            dataType: 'json',
            async: true,
            //data: postData,
            success: function (data) {
                if (data === 1) {
                    alert("Sub-Cartera eliminada con éxito.");
                } else {
                    alert("Sub-Cartera no fue eliminada, revise los datos.");
                }
            },
            error: function (ex) {
                alert('Error al eliminar sub cartera.' + ex);
            }

        });
        fnBuscarSubcarteras();
    } else {
        var url = "/Cartera/SubCartera/?idd=" + id;
        window.location.href = url;
    }

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
        for (i = 0; i < crut.length; i++)
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
        for (i = rut.length - 1; i >= 0; i--) {
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
    $(".ui-dialog-content").html('<object data="' + rowid + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + rowid + '">.</a></p></object>');
    $(".ui-dialog-content").dialog("open");
}

//Judicial

function fnRefreshGrid() {
    var newUrl = "/Judicial/GetEnteAsignado/?"
    newUrl += "Id=" + $("#TipoEnte").val()
    jQuery("#EnteAsignado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
    jQuery("#EnteReasignar").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnGrabarEnte() {
    var s;
    var r;
    s = $("#EnteAsignado").jqGrid('getGridParam', 'selarrrow') + '|' + $("#EnteReasignar").jqGrid('getGridParam', 'selarrrow');
    //r = $("#EnteReasignar").jqGrid('getGridParam', 'selarrrow');
    var postData = { ids: s };
    //var postDatos = { idr: r };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Judicial/GrabarEnte", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData,
            //data2:postDatos,

            success: function (data) {
                if (data != "" || data != null) {
                    alert(data, postDatos);
                }
            },
            error: function (ex) {
                alert('Error al grabar ente.' + ex);
            }
        });
    }
}

//nuevos cambios 20150806

function fnRolSeleccionado(id) {
    var url = "/Judicial/Rol/?idd=" + id;
    window.location.href = url;//'@Url.Action("Deudores", "CarteraController", new{id='+id+'}';
}

jQuery.fn.extend({
    propAttr: $.fn.prop || $.fn.attr
});

function fnGuardarGestionesJud() {
    var newUrl = "/Judicial/GuardarEstadoRol/?"
    var datos = {
        Rolid: $("#Rolid").val(),
        EstadoHistorial: $("#EstadoHistorial").val(),
        MateriaHistorial: $("#MateriaHistorial").val(),
        FechaHistorial: $("#FechaHistorial").val(),
        ComentarioHistorial: $("#ComentarioHistorial").val(),
    };

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        data: datos,
        success: function (data) {
            if (data > 0) {
                fnRefreshGridEstados();
                $('#ppHistorial').dialog('close');
            } else {
                alert(data);
            }
        },
        error: function (ex) {
            alert('Error al guardar gestion.' + ex);
        }

    });
}

function fnActualizarEstadosRol() {
    $("#EstadoHistorial").empty();
    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarEstadosHistorial", // we are calling json method
        dataType: 'json',
        async: false,
        data: { esjid: $("#MateriaHistorial").val() == "" ? 0 : $("#MateriaHistorial").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            //$("#EstadoHistorial").append('<option value="">Seleccione Estado</option>');
            $.each(states, function (i, state) {
                $("#EstadoHistorial").append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los estados rol.' + ex);
        }

    });
}

function fnRefreshGridEstados() {
    var newUrl = "/Judicial/GetEstados/?"
    newUrl += "Rolid=" + $("#Rolid").val()
    jQuery("#gridEstados").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBotonesArchivo(cellvalue, options, rowobject) {
    //alert(rowobject);
    if (cellvalue != '') {
        return "<div align='center'><button style=\"vertical-align:middle\" onclick=\" fnMuestraDocumentoPJ('" + cellvalue + "');\"  class=\"search\"><img width=\"20px\" height=\"20px\" alt=\"Documento\" title=\"Agregar\" src=\"/Images/botones/doc20.png\"></button></div>";
    } else {
        return "";
    }
}

function ActualizarGestiones() {
    fnLimpiarFormularioGestion();
}

function fnLimpiarFormularioGestion() {
    $("#EstadoHistorial").val("-1");
    $("#MateriaHistorial").val("-1");
    $("#FechaHistorial").val("");
    $("#ComentarioHistorial").val("");
}

function fnMuestraDocumentoPJ(url) {
    if (url != "") {
        $('#ppDocto').html("");
        var extension = url.split('.');
        var ext = extension[extension.length - 1];
        switch (ext) {
            case "jpg":
            case "JPG":
                $('#ppDocto').html('<img src="' + url + '" 	style="max-width: 870px;"/>');
                break;
            case "pdf":
            case "PDF":
                $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
                break;
            case "doc":
            case "docx":
                $('#ppDocto').html('<object data="' + url + '" type="application/word" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
                break;
            default:
                if (ext.indexOf("do?COD_Opcion") > -1) {
                    window.open(url);
                    // $('#ppDocto').html('<object data="' + url + '" type="application/word" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
                } else if (ext.indexOf("do?TIP_Documento") > -1) {
                    $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
                } else {
                    $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
                }
        }
        $('#ppDocto').dialog('open');
    }
}

function fnGuardarEnte() {
    var s = $("#gridEnte").jqGrid('getGridParam', 'selarrrow');

    $.ajax({
        type: 'POST',
        url: "/Judicial/ActualizarEntesRol", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), NuevoEnte: $("#EnteJudicial").val(), ListaEntes: s + '' },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (data) {
            if (data > 0) {
                jQuery("#gridEnte").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                $("#EnteJudicial").val("");
            } else {
                alert('Error al actualizar los entes.' + ex);
            }
        },
        error: function (ex) {
            alert('Error al actualizar los entes.' + ex);
        }

    });
}

function fnGuardarDocumentos(agregar, eliminar) {
    //var agregar = $("#gridDocSinAsignar").jqGrid('getGridParam', 'selarrrow');
    //var eliminar = $("#gridDocAsignado").jqGrid('getGridParam', 'selarrrow');

    $.ajax({
        type: 'POST',
        url: "/Judicial/ActualizarDocumentosRol", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val(), DocumentosAsignar: agregar, DocumentosEliminar: eliminar },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (data) {
            if (data > 0) {
                jQuery("#gridDocSinAsignar").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                jQuery("#gridDocAsignado").jqGrid().trigger('reloadGrid', [{ page: 1 }])
            } else {
                //alert('Error al grabar los documentos.');
            }
        },
        error: function (ex) {
            alert('Error al grabar los documentos.' + ex);
        }

    });
}

function fnGuardarDemandado() {
    var s = $("#gridDemandados").jqGrid('getGridParam', 'selarrrow');
    if ($.Rut.validar($("#DemandadoRut").val()) || $("#DemandadoRut").val() == "") {
        $.ajax({
            type: 'POST',
            url: "/Judicial/ActualizarDemandadosRol", // we are calling json method
            dataType: 'json',
            async: false,
            data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), DemandadoRut: $("#DemandadoRut").val(), DemandadoNombre: $("#DemandadoNombre").val(), DemandadoRepresentanteLegal: $("#DemandadoRepresentanteLegal").prop('checked'), ListaDemandados: s + '' },
            // here we are get value of selected country and passing same value as input to json method GetStates.
            success: function (data) {
                if (data > 0) {
                    jQuery("#gridDemandados").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                    $("#DemandadoRut").val("");
                    $("#DemandadoNombre").val("");
                    $("#DemandadoRepresentanteLegal").prop("checked", false);
                } else {
                    alert('Error al actualizar los demandados.' + ex);
                }
            },
            error: function (ex) {
                alert('Error al actualizar los demandados.');
            }

        });
    }

}

function fnGuardarRol() {
    if ($('#Ctcid').val() == "" || $('#Pclid').val() == "" || $('#Rol').val() == "" || $("#Tribunal").val() == "" || $("#TipoCausa").val() == "") {
        alert("Debe ingresar todos los datos mandatorios.");
    }
    else {
        var edita = $("#Rolid").val();
        var agregar = $("#gridDocSinAsignar").jqGrid('getGridParam', 'selarrrow');
        var eliminar = $("#gridDocAsignado").jqGrid('getGridParam', 'selarrrow');
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarRol/", // we are calling json method
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: {
                TipoRol: $("#TipoRol").val(),
                Rol: $("#Rol").val(),
                Tribunal: $("#Tribunal").val(),
                TipoCausa: $("#TipoCausa").val(),
                FechaIngreso: $("#FechaIngreso").val(),
                FechaRol: $("#FechaRol").val(),
                FechaDemanda: $("#FechaDemanda").val(),
                MateriaJudicial: $("#MateriaJudicial").val(),
                Estado: $("#Estado").val(),
                Comentario: $("#Comentario").val(),
                BloquearRol: $("#BloquearRol").prop("checked"),
                ProcesoQuiebra: $("#ProcesoQuiebra").prop("checked"),
                Rolid: $("#Rolid").val(),
                Pclid: $("#Pclid").val(),
                Ctcid: $("#Ctcid").val(),
                ComboQuiebra: $("#ComboQuiebra").val(),

                FechaAvenimiento: $("#FechaAvenimiento").val(),
                MontoAvenimiento: $("#MontoAvenimiento").val(),
                CuotasAvenimiento: $("#CuotasAvenimiento").val(),
                MontoCuotaAvenimiento: $("#MontoCuotaAvenimiento").val(),
                MontoUltimaCuotaAvenimiento: $("#MontoUltimaCuotaAvenimiento").val(),
                FechaPrimeraCuotaAvenimiento: $("#FechaPrimeraCuotaAvenimiento").val(),
                FechaUltimaCuotaAvenimiento: $("#FechaUltimaCuotaAvenimiento").val(),
                InteresAvenimiento: $("#InteresAvenimiento").val(),
                FechaDemandaAve: $("#FechaDemandaAve").val(),
                MontoDemanda: $("#MontoDemanda").val(),
                CuotasDemanda: $("#CuotasDemanda").val(),
                MontoCuotaDemanda: $("#MontoCuotaDemanda").val(),
                MontoUltimaCuotaDemanda: $("#MontoUltimaCuotaDemanda").val(),
                FechaPrimeraCuotaDemanda: $("#FechaPrimeraCuotaDemanda").val(),
                FechaUltimaCuotaDemanda: $("#FechaUltimaCuotaDemanda").val(),
                InteresDemanda: $("#InteresDemanda").val()
            },
            success: function (data) {
                if (data != -1) {
                    $("body").removeClass("loading");

                    $("#Rolid").val(data);
                    if ($("#Rolid").val() != "0" && $("#Rolid").val() != '') {
                        var newUrl = "/Judicial/GetDocumentosPorAsignar/?"
                        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
                        jQuery("#gridDocSinAsignar").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                        newUrl = "/Judicial/GetDocumentosAsignados/?"
                        newUrl += "Rolid=" + data;
                        jQuery("#gridDocAsignado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                        fnRefreshGridEstados();
                        newUrl = "/Judicial/GetRolEntes/?"
                        newUrl += "Rolid=" + data;
                        jQuery("#gridEnte").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                        newUrl = "/Judicial/GetDemandados/?"
                        newUrl += "Rolid=" + data;
                        fnCargarAsociados();
                        $("#tabRol").show();
                        if (edita > 0) {
                            fnGuardarDocumentos(JSON.stringify(agregar), JSON.stringify(eliminar));
                            fnCargarAsociados();
                        }
                        fnCargarTotales();
                    } else {
                        $("#tabRol").hide();
                    }
                } else {
                    $("body").removeClass("loading");
                    alert('Error al guardar Rol.');
                }
            }
        });
    }
}

function FormatCurrency(ctrl, event) {
    //Check if arrow keys are pressed - we want to allow navigation around textbox using arrow keys
    if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40) {
        return;
    }
    var val = ctrl.value;
    val = val.replace(/\./g, "")
    ctrl.value = "";
    val += '';
    x = val.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + '.' + '$2');
    }
    ctrl.value = x1 + x2;
}
function CheckNumeric(event) {
    return (event.charCode >= 48 && event.charCode <= 57) || event.keyCode == 8 || event.keyCode == 46 || event.charCode == 44 || event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40;
}


function fnCargarAsociados() {
    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarAsociados", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ctcid: $("#Ctcid").val() == "" ? 0 : $("#Ctcid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (chartsdata) {
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Nombre');
            data.addColumn('string', 'Padre');
            data.addColumn('string', 'ToolTip');
            for (var i = 0; i < chartsdata.length; i++) {
                var contenido;
                if (chartsdata[i].Monto == "" && chartsdata[i].RepLegal == "") {
                    contenido = '<div onclick=\"window.location.href =\'/Cartera/Index/?tipo=J&pag=355&r=' + chartsdata[i].Rut.replace(/\./g, '').replace('-', '') + '\';\" style=\"cursor:pointer\"><b>' + chartsdata[i].Nombre + '</b><div style="color:red; font-style:italic">' + chartsdata[i].Rut + '</div></div>';
                    data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Padre, chartsdata[i].Tooltip]);
                } else if (chartsdata[i].Nombre == "" && chartsdata[i].RepLegal == "") {
                    contenido = '<div onclick=\"window.location.href = \'/Judicial/Rol/?idd=' + chartsdata[i].Id + '\';\"  style=\"cursor:pointer\"><b>' + chartsdata[i].Caratulado + '</b><div style="color:red; font-style:italic">' + chartsdata[i].Rol + '</div>' +
                        '<div style="color:blue; font-style:italic">' + chartsdata[i].Tribunal + '</div>' +
                        '<div style="color:red; font-style:italic">$ ' + chartsdata[i].Monto + '</div></div>';
                    data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Padre, chartsdata[i].Tooltip]);
                } else if (chartsdata[i].RepLegal != "") {
                    contenido = '<div><b>' + chartsdata[i].Nombre + '</b></div><div style="color:red; font-style:italic">' + chartsdata[i].Rut + '</div>';
                    if (chartsdata[i].RepLegal == "S") { contenido += '<div style="color:red; font-style:italic">Rep. Legal</div>'; }
                    contenido += '<div style="color:red; font-style:italic">Demandado</div>';
                    data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Padre, chartsdata[i].Tooltip]);
                }
            }
            var chart = new google.visualization.OrgChart(document.getElementById('chart_div'));
            chart.draw(data, { allowHtml: true });

        },
        error: function (ex) {
            alert('Error al cargar los asociados.');
        }

    });
}
//Judicial

//empresa

function BuscarEmpleadoSeleccionado(id) {
    var url = "/Empresa/Empleado/?idd=" + id;
    window.location.href = url;//'@Url.Action("Deudores", "CarteraController", new{id='+id+'}';

}
function imageFormat(cellValue, rowId, rowData, options) {
    if (cellValue == '')
        return '';
    else
        //return '<img src= "' + cellValue + '"/>';
        return '<img  src= "' + cellValue + '" style="width:80;height:80px;" />';
}


function fnGuardarEmpresa_2() {
    var newUrl = "/Empresa/EditarEmpresa/?"
    //var newUrl = "/Empresa/GetEmpleado/?"
    newUrl += "Rut=" + $("#Rut").val() + "&Nombre=" + $("#Nombre").val()
    newUrl += "&RutRepresentanteLegal=" + $("#RutRepresentanteLegal").val()
    newUrl += "&NombreRepresentanteLegal=" + $("#NombreRepresentanteLegal").val()
    newUrl += "&Giro=" + $("#Giro").val()
    //newUrl += "&Direccion=" + $("#DireccionBuscar").val() + "&Rol=" + $("#Rol").val() + "&SituacionCartera=" + $("#SituacionCartera").val() + "&NumeroCPBT=" + $("#NumeroCPBT").val()
    //newUrl += "&Gestor=" + $("#gesid").val()
    //alert('paso');
    jQuery("#Empresa").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}


function fnGuardarEmpresa() {
    var newUrl = "/Empresa/EditarEmpresa/?"
    //var newUrl = "/Empresa/GetEmpleado/?"
    newUrl += "Rut=" + $("#Rut").val() + "&Nombre=" + $("#Nombre").val()
    newUrl += "&RutRepresentanteLegal=" + $("#RutRepresentanteLegal").val()
    newUrl += "&NombreRepresentanteLegal=" + $("#NombreRepresentanteLegal").val()
    newUrl += "&Giro=" + $("#Giro").val()

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        //data: postData,
        success: function (data) {

        },
        error: function (ex) {
            alert('Error al guardar empresa.' + ex);
        }

    });

}

//empresa

//proveedor cliente

function fnGuardarProveedorCliente() {
    var newUrl = "/ProvCli/GrabarProveedorCliente/?"
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
    var newUrl = "/ProvCli/GrabarInsumos/?"
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
    var newUrl = "/ProvCli/GrabarProductos/?"
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
        + "&Cubicaje=" + $("#Cubicaje").val() + "&TipoPeso=" + $("#TipoPeso").val()
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
//proveedor cliente

//Tesoreria

function TipoSeleccionado() {
    $("#TipoDocumento").empty();
    $.ajax({
        type: 'POST',
        url: "/Tesoreria/ListarTipoDocumento", // we are calling json method
        dataType: 'json',
        async: false,
        data: { tipo: $("#Tipo").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#TipoDocumento").append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar tipo documento.' + ex);
        }
    });
}
function fnBuscarDocumentosCaja() {
    var newUrl = "/Tesoreria/GetDocumentosCaja/?"
    newUrl += "Tipo=" + $("#Tipo").val() + "&TipoDocumento=" + $("#TipoDocumento").val() + "&Pclid=" + $("#Pclid").val()
    newUrl += "&Rut=" + $("#Rut").val() + "&Ctcid=" + $("#Ctcid").val();
    newUrl += "&Numero=" + $("#Numero").val() + "&MontoDesde=" + $("#MontoDesde").val() + "&MontoHasta=" + $("#MontoHasta").val()
    jQuery("#gridDocumentoCaja").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function DocumentoCajaSeleccionado(id) {
    var url = "/Tesoreria/Caja/?idd=" + id;
    window.location.href = url;
}

function fnBuscarAnularPagos() {
    var newUrl = "/Tesoreria/GetAnularPagos/?"
    newUrl += "Tipo=" + $("#Tipo").val() + "&Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
    jQuery("#gridAnularPagos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function CalculaTotalesAnularPagos(grilla) {
    var CapitalSum = jQuery(grilla).jqGrid('getCol', 'Capital', false, 'sum');
    var InteresSum = jQuery(grilla).jqGrid('getCol', 'Interes', false, 'sum');
    var HonorarioSum = jQuery(grilla).jqGrid('getCol', 'Honorario', false, 'sum');
    var GastoPrejudicialSum = jQuery(grilla).jqGrid('getCol', 'GastoPrejudicial', false, 'sum');
    var GastoJudicialSum = jQuery(grilla).jqGrid('getCol', 'GastoJudicial', false, 'sum');
    var TotalSum = jQuery(grilla).jqGrid('getCol', 'Total', false, 'sum');

    jQuery(grilla).jqGrid('footerData', 'set',
        {
            Fecha: 'Total:',
            Capital: CapitalSum,
            Interes: InteresSum,
            Honorario: HonorarioSum,
            GastoPrejudicial: GastoPrejudicialSum,
            GastoJudicial: GastoJudicialSum,
            Total: TotalSum
        });
}

function fnBuscarModificarPagos() {
    var newUrl = "/Tesoreria/GetModificarPagos/?"
    newUrl += "Tipo=" + $("#Tipo").val() + "&Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
    jQuery("#gridModificarPagos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function OnSelectClienteNegociacionCaja() {
    $("#Negociacion").empty();
    $.ajax({
        type: 'POST',
        url: "/Tesoreria/ListarNegociacion", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ctcid: $("#Ctcid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#Negociacion").append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar negociaciones.' + ex);
        }
    });
}

//Tesoreria

//finanzas

function fnGuardarComisiones() {
    var newUrl = "/Finanzas/GuardarComisiones/?"
    newUrl += "desde=" + $("#Desde").val() + "&hasta=" + $("#Hasta").val()

    if ($('#Desde').val() == "" || $("#Hasta").val() == "") {

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

//finanzas

//ultimos cambios

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
    if ($("#SituacionCartera").val() != "") {
        $("#EstadoCpbt").val($("#SituacionCartera").val());
    }
    CargarDeudor();
    $(".ui-dialog-content").dialog().dialog("close");
    $("#frmBuscarDeudor").reset();
    jQuery("#Deudor").jqGrid().setGridParam({ url: "/Cartera/GetDeudores/?NumeroCPBT=-1119999" }).trigger('reloadGrid', [{ page: 1 }])
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
                    //$("#EstadoCpbt").val($("#SituacionCartera").val());
                    //CargarDeudor();
                    //CargarImagenesCpbt();
                } else {
                    $("#RutDeudorBuscar").val($("#Rut").val());
                    fnBuscarDeudores();
                    $('#ppBuscar').dialog('open');
                    //$(".ui-dialog-content").dialog().dialog("open");
                }

            }
        });
    }
    if ($("#Ctcid").val() != "") {
        CargarDeudor();
    }

}

function fnMuestraDocumentoCliente(id) {
    var url = $('#gridDocCliente').getRowData(id).UrlArchivo;
    var extension = url.split('.');
    var ext = extension[extension.length - 1];
    switch (ext) {
        case "jpg":
        case "JPG":
            $('#ppDocto').html('<img src="' + url + '" 	style="max-width: 870px;"/>');
            break;
        case "pdf":
        case "PDF":
            $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
            break;
        default:
            $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
    }
    $('#ppDocto').dialog('open');
}
function fnMuestraDocumentoDeudor(id) {
    var url = $('#gridDocDeudor').getRowData(id).UrlArchivo;
    var extension = url.split('.');
    var ext = extension[extension.length - 1];
    switch (ext) {
        case "jpg":
        case "JPG":
            $('#ppDocto').html('<img src="' + url + '" 	style="max-width: 870px;"/>');
            break;
        case "pdf":
        case "PDF":
            $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
            break;
        default:
            $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
    }
    $('#ppDocto').dialog('open');
}

function CargarCpbtTotal() {
    var newUrl = "/Cartera/GetCpbtTotal/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&SituacionCartera=" + $("#EstadoCpbt").val()
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {
            $("#hddTotales").val(JSON.stringify(data));
        },
        error: function (ex) {
            alert('Error al recuperar totales.' + ex);
        }

    });

}

function CargarCpbtTotalMoneda() {
    var newUrl = "/Cartera/GetCpbtTotalMoneda/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&SituacionCartera=" + $("#EstadoCpbt").val()
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {
            $("#hddTotalesMoneda").val(JSON.stringify(data));
        },
        error: function (ex) {
            alert('Error al recuperar totales moneda.' + ex);
        }

    });

}

function CargarImagenesCpbt() {
    var newUrl = "/Cartera/CargarImagenesCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#tabDetallesDeudor').tabs('select', 0);
            if (data != '') {
                $("#tabDetallesDeudor").tabs({ disabled: [6] });
                $(".es-carousel").html('<ul style="padding:0px;margin:0px">' + data + '</ul>');
                $(".rg-view-full").click();
                $(".rg-view-thumbs").click();
            } else {
                $("#tabDetallesDeudor").tabs({ disabled: [2, 6] });
            }

        },
        error: function (ex) {
            alert('Error al recuperar totales moneda.' + ex);
        }

    });

}

function fnBuscarTelefonosABMDeudor() {
    var newUrl = "/Cartera/GetTelefonoDeudor/?"
    newUrl += "Ctcid=" + $("#Ctcid").val()
    jQuery("#gridTelefonos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarEmailABMDeudor() {
    var newUrl = "/Cartera/GetEmailDeudor/?"
    newUrl += "Ctcid=" + $("#Ctcid").val()
    jQuery("#gridEmail").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarContactosABMDeudor() {
    var newUrl = "/Cartera/GetContactosDeudor/?"
    newUrl += "Ctcid=" + $("#Ctcid").val()
    jQuery("#gridContactos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnLimpiarDeudor() {
    //alert("limpiar");
    $("#frmDeudor").reset();
}


function fnMostrarCambioEstados() {
    var d = $("#ppHistorial").dialog();
    if ($("#CambiaEstado").prop('checked')) {
        $("#divCambioEstado").show();
        d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
    } else {
        $("#divCambioEstado").hide();
        d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
    }

}

function CargarAgregarHistorial() {
    fnActualizarContactos("Contacto");
    fnActualizarEstadosHistorial("TipoEstado");
    fnActualizarTelefonosContactos("TelefonoHistorial");
}

function fnActualizarContactos(controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarContactos", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ctcid: $("#Ctcid").val() == "" ? 0 : $("#Ctcid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione --</option>');
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarEstadosHistorial(controlDestino) {
    $("#" + controlDestino).empty();
    $("#" + controlDestino).attr("onchange", "fnActualizarEstados()");
    var d = $("#ppHistorial").dialog();
    var estados = '';
    if ($("#EstadosXDocumentos").prop('checked')) {
        estados = 'V';
        $("#divGrillaDocsHistorial").show();

    } else {
        $("#divGrillaDocsHistorial").hide();
    }
    d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarEstadosHistorial", // we are calling json method
        dataType: 'json',
        async: false,
        data: { grupo: $("#Agrupa").val(), tipo: $("#Tipo").val(), estadoXDoc: estados },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione --</option>');
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarTelefonosContactos(controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarTelefonosContactos", // we are calling json method
        dataType: 'json',
        async: false,
        data: { ctcid: $("#Ctcid").val() == "" ? 0 : $("#Ctcid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione --</option>');
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarTipoGestion() {
    var newUrl = "/Cartera/BuscarAccionesAgrupa/?"
    newUrl += "&id=" + $("#TipoGestion").val();
    var d = $("#ppHistorial").dialog();
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        success: function (data) {
            if (data == 1) {
                $("#divTelefonoHistorial").show();
                $("#MostrarTelefono").val(true);
                d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
            } else {
                $("#divTelefonoHistorial").hide();
                $("#MostrarTelefono").val(false);
                d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
            }
        },
        error: function (ex) {
            alert('Error al revisar accion.' + ex);
        }

    });
}

function fnActualizarEstados() {
    var newUrl = "/Cartera/BuscarDestalleEstado/?"
    newUrl += "&id=" + $("#TipoEstado").val();
    var d = $("#ppHistorial").dialog();
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        success: function (data) {
            if (data.SolicitaFecha == "S") {
                $("#divFechaHistorial").show();
                $("#MostrarFecha").val(true);
                d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
            } else {
                $("#divFechaHistorial").hide();
                $("#MostrarFecha").val(false);
                d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
            }
            if (data.Compromiso == "S" && data.Utiliza == "D") {
                var width = $("#grdDocumentosCambioEstado").jqGrid('getGridParam', 'width');
                $("#grdDocumentosCambioEstado").jqGrid('showCol', "Compromiso");
                $("#grdDocumentosCambioEstado").setGridWidth(width);
            } else {
                var width = $("#grdDocumentosCambioEstado").jqGrid('getGridParam', 'width');
                $("#grdDocumentosCambioEstado").jqGrid('hideCol', "Compromiso");
                $("#grdDocumentosCambioEstado").setGridWidth(width);
            }
        },
        error: function (ex) {
            alert('Error al revisar estado.' + ex);
        }

    });
}

function fnListarDocumentosHistorial() {
    var newUrl = "/Cartera/GetDocumentosHistorial/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Tipo=" + $("#Tipo").val()
    jQuery("#grdDocumentosCambioEstado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnGuardarGestiones() {
    var s = $("#grdDocumentosCambioEstado").jqGrid('getGridParam', 'selarrrow');
    var datosGrilla = $('#grdDocumentosCambioEstado').jqGrid('getRowData');
    var ids = s;
    jsonObj = [];
    $.each(datosGrilla, function (i, item) {
        item2 = {}
        if (!item.Compromiso) { item.Compromiso = "0"; }
        item2["id"] = item.Ccbid;
        item2["m"] = item.Compromiso;
        if ($.inArray(item.Ccbid, ids) > -1) {
            jsonObj.push(item2);
        }

    });
    //alert(JSON.stringify(jsonObj));
    var newUrl = "/Cartera/GuardarGestiones/?"

    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Tipo=" + $("#Tipo").val() + "&Ids=" + s + "&FechaHistorial=" + $("#FechaHistorial").val()
    newUrl += "&TipoGestion=" + $("#TipoGestion").val() + "&CambiaEstado=" + $("#CambiaEstado").prop('checked') + "&Contacto=" + $("#Contacto").val() + "&Comentario=" + $("#Comentario").val() + "&Agrupa=" + $("#Agrupa").val()
    newUrl += "&EstadosXDocumentos=" + $("#EstadosXDocumentos").prop('checked') + "&TipoEstado=" + $("#TipoEstado").val() + "&TelefonoHistorial=" + $("#TelefonoHistorial").val() + "&ResultadoLlamado=" + $("#ResultadoLlamado").val()
    newUrl += "&MostrarTelefono=" + $("#MostrarTelefono").val() + "&MostrarFecha=" + $("#MostrarFecha").val() + "&EstadoCpbt=" + $("#EstadoCpbt").val() + "&Documentos=" + JSON.stringify(jsonObj)

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        success: function (data) {
            if (data > 0) {
                fnBuscarHistorialDeudor();
                fnBuscarCpbtDeudor();
                fnBuscarObservacionDeudor();
                //alert('Gestion guardada exitosamente.');
                $('#ppHistorial').dialog('close');
            } else {
                //alert(data);
            }
        },
        error: function (ex) {
            alert('Error al guardar gestion.' + ex);
        }

    });

}

function fnGuardarGestionesPost() {
    var s = $("#grdDocumentosCambioEstado").jqGrid('getGridParam', 'selarrrow');
    var datosGrilla = $('#grdDocumentosCambioEstado').jqGrid('getRowData');
    var ids = s;
    jsonObj = [];
    $.each(datosGrilla, function (i, item) {
        item2 = {}
        if (!item.Compromiso) { item.Compromiso = "0"; }
        item2["Ccbid"] = item.Ccbid;
        item2["Compromiso"] = item.Compromiso;
        if ($.inArray(item.Ccbid, ids) > -1) {
            jsonObj.push(item2);
        }

    });
    //alert(JSON.stringify(jsonObj));
    var newUrl = "/Cartera/GuardarGestiones/?"
    var datos = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Tipo: $("#Tipo").val(),
        Ids: JSON.stringify(s),
        FechaHistorial: $("#FechaHistorial").val(),
        TipoGestion: $("#TipoGestion").val(),
        CambiaEstado: $("#CambiaEstado").prop('checked'),
        Contacto: $("#Contacto").val(),
        Comentario: $("#Comentario").val(),
        Agrupa: $("#Agrupa").val(),
        EstadosXDocumentos: $("#EstadosXDocumentos").prop('checked'),
        TipoEstado: $("#TipoEstado").val(),
        TelefonoHistorial: $("#TelefonoHistorial").val(),
        ResultadoLlamado: $("#ResultadoLlamado").val(),
        MostrarTelefono: $("#MostrarTelefono").val(),
        MostrarFecha: $("#MostrarFecha").val(),
        EstadoCpbt: $("#EstadoCpbt").val(),
        Documentos: JSON.stringify(jsonObj),
        TodosSeleccionados: $("#TodosSeleccionados").val()
    };

    //newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Tipo=" + $("#Tipo").val() + "&Ids=" + s + "&FechaHistorial=" + $("#FechaHistorial").val()
    //newUrl += "&TipoGestion=" + $("#TipoGestion").val() + "&CambiaEstado=" + $("#CambiaEstado").prop('checked') + "&Contacto=" + $("#Contacto").val() + "&Comentario=" + $("#Comentario").val() + "&Agrupa=" + $("#Agrupa").val()
    //newUrl += "&EstadosXDocumentos=" + $("#EstadosXDocumentos").prop('checked') + "&TipoEstado=" + $("#TipoEstado").val() + "&TelefonoHistorial=" + $("#TelefonoHistorial").val() + "&ResultadoLlamado=" + $("#ResultadoLlamado").val()
    //newUrl += "&MostrarTelefono=" + $("#MostrarTelefono").val() + "&MostrarFecha=" + $("#MostrarFecha").val() + "&EstadoCpbt=" + $("#EstadoCpbt").val() + "&Documentos=" + JSON.stringify(jsonObj)

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        data: datos,
        success: function (data) {
            if (data > 0) {
                fnBuscarHistorialDeudor();
                fnBuscarCpbtDeudor();
                fnBuscarObservacionDeudor();
                //alert('Gestion guardada exitosamente.');
                $('#ppHistorial').dialog('close');
            } else {
                alert(data);
            }
        },
        error: function (ex) {
            alert('Error al guardar gestion.' + ex);
        }

    });

}

function ActualizarGestiones() {
    fnLimpiarFormularioGestion();
    fnActualizarContactos("Contacto");
    fnActualizarEstadosHistorial("TipoEstado");
    fnActualizarTelefonosContactos("TelefonoHistorial");
}
// Reportes

function fnDescargarReporte() {
    if ($("#Pclid").val() != '' && $("#Ctcid").val() != '' && $("#Tipo").val() != '' && $("#Reporte").val() != '') {
        if ($("#Reporte").val() == 3) {
            $('#ppTipoReporte').dialog('open');
        } else {
            fnGeneraPDF();
        }
    } else {
        alert("Faltan datos para generar el reporte.")
    }
}

function fnGeneraPDF() {
    $('#ppTipoReporte').dialog('close');
    var newUrl = "GeneraReporte/?";
    newUrl += "pclid=" + $("#Pclid").val() + "&ctcid=" + $("#Ctcid").val() + "&tipo=" + $("#EstadoCpbt").val() + "&rep=" + $("#Reporte").val() + "&pag=" + $("#Pag").val();
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        beforeSend: function () { $("body").addClass("loading"); },
        success: function (data) {
            $("body").removeClass("loading");
            fnMuestraReporteCliente(data);
        },
        error: function (ex) {
            $("body").removeClass("loading");
            alert('Error al generar el reporte.');
        }
    });
}

function fnGeneraXLS() {
    $('#ppTipoReporte').dialog('close');
    var newUrl = "GeneraReporteXLS/?";
    newUrl += "pclid=" + $("#Pclid").val() + "&ctcid=" + $("#Ctcid").val() + "&tipo=" + $("#EstadoCpbt").val() + "&rep=" + $("#Reporte").val() + "&pag=" + $("#Pag").val();
    window.location = newUrl;

}

// Reportes

//email
function fnActualizarGestores() {
    var newUrl = "/Email/GetGestor/?"
    newUrl += "TipoCartera=" + $("#TipoCartera").val() + "&grupo=" + $("#GrupoCobranza").val()


    jQuery("#gridGestor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnActualizarGestoresMasivo() {
    var newUrl = "/Email/GetGestorEmailMasivo/?";
    newUrl += "TipoCartera=" + $("#TipoCartera").val();
    jQuery("#gridGestor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnEnviarEmail() {

    var newUrl = "/Email/EnviarEmail/?"
    var gestorRows = JSON.stringify(jQuery('#gridGestor').jqGrid('getGridParam', 'selarrrow'));
    var estadosRow = JSON.stringify(jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow'));
    newUrl += "Reporte=" + $("#Reporte option:selected").val() + "&Pclid=" + $("#Pclid").val() + "&Email=" + $("#Email").val() + "&TipoCartera=" + $("#TipoCartera").val() + "&GrupoCobranza=" + $("#GrupoCobranza").val() + "&Gestores=" + gestorRows + "&Estados=" + estadosRow

    newUrl += "&EmailTodos=" + $("#EmailTodos").prop('checked') + "&EmailContacto=" + $("#EmailContacto").prop('checked');
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        beforeSend: function () { $("body").addClass("loading"); },
        success: function (data) {
            $("body").removeClass("loading");
            if (data === false) {
                alert("Correos enviados con éxito.");
            } else {
                alert("Correos enviados con éxito, revise el reporte a continuacion.");
            }
        },
        error: function (ex) {
            alert('Error al enviar correos.' + ex);
        }

    });
}
//email

function fnLimpiarFormularioGestion() {
    var d = $("#ppHistorial").dialog()
    $("#frmGuardarGestion").reset();
    var width = $("#grdDocumentosCambioEstado").jqGrid('getGridParam', 'width');
    $("#grdDocumentosCambioEstado").jqGrid('hideCol', "Compromiso");
    $("#grdDocumentosCambioEstado").setGridWidth(width);
    //jQuery("#grdDocumentosCambioEstado").jqGrid().setGridParam({ url: "/Cartera/GetDummy" }).trigger('reloadGrid', [{ page: 1 }])
    fnListarDocumentosHistorial();
    $("#divTelefonoHistorial").hide();
    $("#MostrarTelefono").val(false);
    $("#divFechaHistorial").hide();
    $("#MostrarFecha").val(false);
    $("#divCambioEstado").hide();
    idsCambioEstadoHistorial = [];
    d.dialog("option", "height", $("#divTabla").outerHeight() + 50);
}

function fnMuestraReporteCliente(url) {
    var extension = url.split('.');
    var ext = extension[extension.length - 1];
    switch (ext) {
        case "jpg":
        case "JPG":
            $('#ppDocto').html('<img src="' + url + '" 	style="max-width: 870px;"/>');
            break;
        case "pdf":
        case "PDF":
            $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
            break;
        default:
            $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
    }
    $('#ppDocto').dialog('open');
}

function fnFormularioContactos(tipo) {
    var d = $("#ppContactoTelefono").dialog();
    $("#frmContactoTelefono").reset();
    $("#Ddcid").val("");
    fnActualizarTipoDireccionSitrel();
    switch (tipo) {
        case "T":
            $("#TipoForm").val(tipo);
            $("#divContactoEmail").hide();
            if ($("#Pclid").val() == 522) {
                $("#divAnexo").show();
                $("#divTipoDireccion").show();
            } else {
                $("#divAnexo").hide();
                $("#divTipoDireccion").hide();
            }
            $("#divContactoTelefono").show();
            $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Teléfono Contacto');
            break;
        case "E":
            $("#TipoForm").val(tipo);
            $("#divContactoTelefono").hide();
            $("#divContactoEmail").show();
            if ($("#Pclid").val() == 522) {
                $("#divTipoDireccion").show();
            } else {
                $("#divTipoDireccion").hide();
            }
            $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Email Contacto');
            break;
        case "D":
            $("#TipoForm").val(tipo);
            $("#divContactoTelefono").hide();
            $("#divContactoEmail").hide();
            $("#divTipoDireccion").hide();
            //if ($("#Pclid").val() == 522)
            {
                $("#divTipoDireccion").show();
                //} else {
                //$("#divTipoDireccion").hide();
            }
            $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Dirección Contacto');
            break;
    }
    $('#IdPais').val(56);
    PaisSeleccionado($('#IdPais'), "IdRegion");
    $("#IdRegion").val(6);
    RegionSeleccionada($('#IdRegion'), "IdCiudad");
    $("#IdCiudad").val(21);
    CiudadSeleccionada($('#IdCiudad'), "IdComuna");
    $('#IdComuna').val(112);

    $('#ppContactoTelefono').dialog('open');
    d.dialog("option", "height", $("#divTablaContacto").outerHeight() + 50);
}

function ActualizarContactoTelefono() {
    //fnLimpiarFormularioGestion();
    //fnActualizarContactos("Contacto");
    //fnActualizarEstadosHistorial("TipoEstado");
    //fnActualizarTelefonosContactos("TelefonoHistorial");
}

function fnGuardarContacto() {
    var newUrl = "/Cartera/GuardarContacto/?"
    var postData = $("#frmContactoTelefono").serialize();

    var extraData = {
        'Pclid': $("#Pclid").val(), 'Ctcid': $("#Ctcid").val(), 'TipoForm': $("#TipoForm").val()
    };

    var datos = postData + '&' + $.param(extraData);
    //postData += "&Pclid=" + $("#Pclid").val() + "&Ctcid =" + $("#Ctcid").val();
    if ($("#frmContactoTelefono input[id=Telefono]").val() == '' && $("#TipoForm").val() == 'T') {
        alert("Ingrese el teléfono.");
        return false;
    }
    if ($("#frmContactoTelefono input[id=Email]").val() == '' && $("#TipoForm").val() == 'E') {
        alert("Ingrese el email.");
        return false;
    }


    if ($("#IdPais").val() != '' && $("#IdRegion").val() != '' && $("#IdCiudad").val() != '' && $("#IdComuna").val() != '' && $("#frmContactoTelefono input[id=Direccion]").val() != '') {// && $("#NombreContacto").val() != ''
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: datos,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                //alert(data);
                fnBuscarEmailDeudor();
                fnBuscarTelefonosDeudor();
                fnBuscarDireccionDeudor();
                $("body").removeClass("loading");
                $('#ppContactoTelefono').dialog('close');
                //$("#btnGuardarDeudor").attr("disabled", "disabled");

            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar el deudor.' + ex);
                $('#ppContactoTelefono').dialog('close');
            }

        });
    } else {
        alert("Favor ingresar todos los campos");
    }
}

//Predefinidos

function fnGenerarReportePredefinido() {
    if ($("#Pclid").val() != '' || $("#TipoCartera").val() != '' || $("#Reporte").val() != '' || $("#pag").val() != '') {
        var newUrl = "/Reportes/GeneraReporte/?";
        var formato = 0;

        newUrl += "pclid=" + $("#Pclid").val() + "&rep=" + $("#Reporte").val() + "&tipoCartera=" + $("#TipoCartera").val() + "&pag=" + $("#pag").val();

        if ($("#Reporte").val() == '16' && $("#pag").val() == 357) {
            if (!$("#CodigoCarga").val()) {
                return false;
            }
            newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
        }

        if ($("#Reporte").val() == '6' && $("#pag").val() == 357) {
            newUrl += "&tipo=" + $("#EstadoDocumento").val()
        }

        if ($("#Reporte").val() == '22' && $("#pag").val() == 357) {

            if ($("#CodigoCarga").val()) {
                newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
            }

            newUrl += "&tipo=" + $("#EstadoDocumento").val()
        }

        if ($("#Reporte").val() == '1' && $("#pag").val() == 357) {
            newUrl += "&tipo=" + $("#EstadoDocumento").val() + "&codigoCarga=" + $("#CodigoCarga").val() + "&gestor=" + $("#Gestor").val()
        }

        if ($("#Reporte").val() == '7' && $("#pag").val() == 357) {
            newUrl += "&tipo=" + $("#SituacionCartera").val() + "&desde=" + $("#FechaDesde").val() + "&hasta=" + $("#FechaHasta").val()
        }

        if ($("#Reporte").val() == '21' && $("#pag").val() == 357) {
            if ($("#Pclid").val() == '') {
                newUrl = "/Reportes/GeneraReporte/?pclid=0&rep=" + $("#Reporte").val() + "&tipoCartera=" + $("#TipoCartera").val() + "&pag=" + $("#pag").val() + "&gestor=" + $("#Gestor").val() + "&vencidos=" + $("#Vencidos").is(":checked")
            }
            else {
                newUrl += "&gestor=" + $("#Gestor").val() + "&vencidos=" + $("#Vencidos").is(":checked")
            }

        }

        if (($("#Reporte").val() == '3' || $("#Reporte").val() == '4') && $("#pag").val() == 358) {
            if (!$("#CodigoCarga").val()) {
                return false;
            }
            newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
        }

        if ($("#Reporte").val() == '16' && $("#pag").val() == 358) {
            if ($("#CodigoCarga").val()) {
                newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
            }
        }

        if ($("#Reporte").val() == '9' && $("#pag").val() == 358) {
            newUrl += "&ctcid=" + $("#Ctcid").val() + "&rol=" + $("#Rol").val()
        }

        if ($("#Reporte").val() == '14' && $("#pag").val() == 358) {
            formato = 1;
            $('#ppTipReporte').dialog('open');
        }

        if ($("#Reporte").val() == '15' && $("#pag").val() == 358) {
            formato = 1;
            var newUrl = "/Reportes/GenerarReporteXLS/?pclid=" + $("#Pclid").val() + "&ctcid=" + $("#Ctcid").val() + "&rep=" + $("#Reporte").val() + "&pag=" + $("#Pag").val() + "&diasPre=" + $("#DiasPrescripcion").val();
            window.location = newUrl;
        }

        if ($("#Reporte").val() == '23' && $("#pag").val() == 357) {
            formato = 1;
            var newUrl = "/Reportes/GenerarReporteXLS/?pclid=" + $("#Pclid").val() + "&rep=" + $("#Reporte").val() + "&pag=" + $("#Pag").val();
            window.location = newUrl;
        }

        if (formato != 1) {
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                beforeSend: function () { $("body").addClass("loading"); },
                success: function (data) {
                    $("body").removeClass("loading");
                    // //fnMultiFormato(data, formato);
                    fnMuestraReportePredefinido(data);
                }

            });
        }

    } else {
        alert("Faltan datos para generar el reporte.")
    }
}
//Predefinidos

function fnBotonesContactoTelefono(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarTelefonoContacto(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarTelefonoContacto(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnBotonesContactoEmail(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarEmailContacto(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarEmailContacto(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnEditarTelefonoContacto(id) {
    //alert(id);
    fnFormularioContactos('T');
    var datos = id.split(',');
    $('#frmContactoTelefono input[id=Telefono]').val(datos[1]);
    $('#frmContactoTelefono select[id=TipoTelefono]').val(datos[6]);
    $('#frmContactoTelefono select[id=EstadoTelefono]').val(datos[8]);
    $("#frmContactoTelefono select[id=TipoContacto]").val(datos[9]);
    $("#Ddcid").val(datos[10]);
    $('#frmContactoTelefono input[id=NombreContacto]').val(datos[4]);
    $('#frmContactoTelefono input[id=EstadoContacto]').val(datos[1]);
    $('#frmContactoTelefono input[id=Direccion]').val(datos[12]);
    $('#frmContactoTelefono select[id=EstadoDireccion]').val(datos[11]);
    $('#IdPais').val(datos[15]);
    PaisSeleccionado($('#IdPais'), "IdRegion");
    $("#IdRegion").val(datos[14]);
    RegionSeleccionada($('#IdRegion'), "IdCiudad");
    $("#IdCiudad").val(datos[13]);
    CiudadSeleccionada($('#IdCiudad'), "IdComuna");
    $('#IdComuna').val(datos[7]);

}

function fnEliminarTelefonoContacto(id) {
    var r = confirm("Desea eliminar el telefono de contacto?");
    if (r == true) {
        var datos = id.split(',');
        var url = "/Cartera/EliminarContactoTelefono/?id=" + datos[5] + '|' + datos[1];
        $.ajax({
            type: 'POST',
            url: url, // we are calling json method
            dataType: 'json',
            async: true,
            //data: postData,
            success: function (data) {
                //if (data === 1) {
                //    alert("Sub-Cartera eliminada con éxito.");
                //} else {
                //    alert("Sub-Cartera no fue eliminada, revise los datos.");
                //}
            },
            error: function (ex) {
                alert('Error al eliminar telefono.' + ex);
            }

        });
        fnBuscarTelefonosDeudor();
    }

}

function fnEditarEmailContacto(id) {
    //alert(id);
    fnFormularioContactos('E');
    var datos = id.split(',');
    $('#frmContactoTelefono input[id=Email]').val(datos[0]);
    $("#frmContactoTelefono select[id=TipoEmail]").val(datos[1].substring(0, 1));// + ")").attr('selected', 'selected');
    //$('#frmContactoTelefono select[id=TipoEmail]').val(datos[6]);
    if (datos[14] == 'S') {
        $('#frmContactoTelefono input[id=EmailMasivo]').prop("checked", true);
    } else {
        $('#frmContactoTelefono input[id=EmailMasivo]').prop("checked", false);
    }
    //$('#frmContactoTelefono input[id=EmailMasivo]').val(datos[14]);
    $('#frmContactoTelefono select[id=EstadoTelefono]').val(datos[8]);
    $("#frmContactoTelefono select[id=TipoContacto]").val(datos[6]);
    $("#Ddcid").val(datos[5]);
    $('#frmContactoTelefono input[id=NombreContacto]').val(datos[3]);
    $('#frmContactoTelefono input[id=EstadoContacto]').val(datos[7]);

    $('#frmContactoTelefono input[id=Direccion]').val(datos[8]);
    $('#frmContactoTelefono select[id=EstadoDireccion]').val(datos[9]);
    $('#IdPais').val(datos[12]);
    PaisSeleccionado($('#IdPais'), "IdRegion");
    $("#IdRegion").val(datos[11]);
    RegionSeleccionada($('#IdRegion'), "IdCiudad");
    $("#IdCiudad").val(datos[10]);
    CiudadSeleccionada($('#IdCiudad'), "IdComuna");
    $('#IdComuna').val(datos[13]);
}

function fnEliminarEmailContacto(id) {
    var r = confirm("Desea eliminar el email de contacto?");
    if (r == true) {
        var datos = id.split(',');
        var url = "/Cartera/EliminarContactoEmail/?id=" + datos[4] + '|' + datos[0];
        $.ajax({
            type: 'POST',
            url: url, // we are calling json method
            dataType: 'json',
            async: true,
            //data: postData,
            success: function (data) {
                //if (data === 1) {
                //    alert("Sub-Cartera eliminada con éxito.");
                //} else {
                //    alert("Sub-Cartera no fue eliminada, revise los datos.");
                //}
            },
            error: function (ex) {
                alert('Error al eliminar email.' + ex);
            }

        });
        fnBuscarEmailDeudor();
    }
}

function PaisContactoSel() {
    PaisSeleccionado($('#IdPais'), "IdRegion");
}
function RegionContactoSel() {
    RegionSeleccionada($('#IdRegion'), "IdCiudad");
}
function CiudadContactoSel() {
    CiudadSeleccionada($('#IdCiudad'), "IdComuna");
}

var idsEstadoHistorial = [];

//function fnOnSelectAllHistorial(aRowids, status) {
//    //alert(aRowids);
//    $("#TodosSeleccionados").val(status === false ? 0 : aRowids.length);
//    //alert( $("#TodosSeleccionados").val());
//}

//Judicial 20150930

function fnBtnBorradores() {
    $('#ppBorradores').dialog('open');
    var editor = CKEDITOR.instances['editor1'];
    if (editor) {
        editor.destroy(true);
    }
    CKEDITOR.replace('editor1');
    return false;
}

function fnBtnAsociados() {
    $('#ppAsociados').dialog('open');
    return false;
}


//Carga Itau
function fnCargarArchivosItau() {
    var newUrl = "/Cartera/ProcesoCargaItau/"
    var postData = $("#frmCargaMasiva").serializeArray();
    if ($('#frmCargaMasiva').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("#btnProcesar").removeAttr("disabled");
                $("body").removeClass("loading");
                if (data.length > 0) {
                    for (var i = 0; i <= data.length; i++)
                        $("#grdCargaMasivaItau").jqGrid('addRowData', i + 1, data[i]);
                    alert('Archivos cargados con errores');
                } else {
                    alert('Archivos cargados con exito');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al cargar los archivos.' + ex);
            }

        });
    }
}

function fnFormularioGestion() {
    if ($("#Pclid").val() == 522) {
        $('#ppGestionSitrel').dialog('open');
    } else {
        $('#ppHistorial').dialog('open');
    }

}

function ActualizarGestionesSitrel() {
    var d = $("#ppGestionSitrel").dialog()
    fnLimpiarFormularioGestionSitrel();
    fnActualizarAccionesSitrel("AccionSitrel");
    fnActualizarContactosSitrel("ContactoSitrel");
    fnActualizarRespuestasSitrel("RespuestaSitrel");
    fnListarDocumentosHistorialSitrel();
    d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 150);
    //fnActualizarTelefonosContactos("TelefonoHistorial");
}

function fnLimpiarFormularioGestionSitrel() {
    var d = $("#ppGestionSitrel").dialog()
    $("#frmGuardarGestionSitrel").reset();
    $("#divTelefonoContactoSitrel").hide();
    $("#divCompromiso").hide();
    $("#lblComentarioSitrel").text(250);
    d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
}

function fnActualizarAccionesSitrel(controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarAccionesST", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarContactosSitrel(controlDestino) {
    $("#" + controlDestino).empty();
    if (!$("#AccionSitrel").val().indexOf("BUSQANT") != -1) {
        var d = $("#ppGestionSitrel").dialog()
        $("#divTelefonoContactoSitrel").show();
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    } else {
        var d = $("#ppGestionSitrel").dialog()
        $("#divTelefonoContactoSitrel").hide();
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    }
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarContactosST", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val(), accion: $("#AccionSitrel").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarRespuestasSitrel(controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarRespuestasST", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val(), accion: $("#AccionSitrel").val(), contacto: $("#ContactoSitrel").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnActualizarCompromisol() {
    if ($("#RespuestaSitrel").val().indexOf("COMP") != -1) {
        var d = $("#ppGestionSitrel").dialog()
        $("#divCompromiso").show();
        var width = $("#grdDocsEstadoSitrel").jqGrid('getGridParam', 'width');
        $("#grdDocsEstadoSitrel").jqGrid('showCol', "Compromiso");
        $("#grdDocsEstadoSitrel").setGridWidth(width);
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    } else {
        var d = $("#ppGestionSitrel").dialog()
        $("#divCompromiso").hide();
        var width = $("#grdDocsEstadoSitrel").jqGrid('getGridParam', 'width');
        $("#grdDocsEstadoSitrel").jqGrid('hideCol', "Compromiso");
        $("#grdDocsEstadoSitrel").setGridWidth(width);
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    }

    if ($("#RespuestaSitrel").val().indexOf("SOLICITARELL") != -1) {
        var d = $("#ppGestionSitrel").dialog()
        $("#divProgramacion").show();
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    } else {
        var d = $("#ppGestionSitrel").dialog()
        $("#divProgramacion").hide();
        d.dialog("option", "height", $("#divTablaSitrel").outerHeight() + 50);
    }
}

function fnActualizarTipoDireccionSitrel() {
    $("#TipoDireccion").empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarTipoDireccionST", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pclid: 0 },//$("#Pclid").val() == "" ? 0 : $("#Pclid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#TipoDireccion").append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los contactos.' + ex);
        }

    });
}

function fnGuardarGestionesSitrelPost() {
    if ($("#RespuestaSitrel").val().indexOf("COMPAG") > -1) {
        if ($("#FechaCompromisoSitrel").val() == "") {
            alert("Favor ingrese fecha de compromiso.");
            return false;
        }
    }

    if ($("#RespuestaSitrel").val().indexOf("SOLICITARELL") > -1) {
        if ($("#FechaProgramadaSitrel").val() == "") {
            alert("Favor ingrese fecha de programacion para la llamada.");
            return false;
        }
        if ($("#HoraProgramadaSitrel").val() == "" && $("#MinutoProgramadoSitrel").val() == "") {
            alert("Favor ingrese hora de programacion para la llamada.");
            return false;
        }
    }

    if (!$("#AccionSitrel").val().indexOf("BUSQANT") > -1) {
        if ($("#TelefonoContactoSitrel").val() == "") {
            alert("Favor ingrese telefono de contacto.");
            return false;
        }
    }

    var s = $("#grdDocsEstadoSitrel").jqGrid('getGridParam', 'selarrrow');
    //alert(JSON.stringify(s));
    var datosGrilla = $('#grdDocsEstadoSitrel').jqGrid('getRowData');
    var ids = s;
    jsonObj = [];
    $.each(datosGrilla, function (i, item) {
        item2 = {}
        if (!item.Compromiso) { item.Compromiso = "0"; }
        item2["id"] = item.Ccbid;
        item2["m"] = item.Compromiso;
        if ($.inArray(item.Ccbid, ids) > -1) {
            jsonObj.push(item2);
        }

    });
    //alert(JSON.stringify(jsonObj));

    var newUrl = "/Cartera/GuardarGestionesSitrel/?"
    var datos = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Tipo: $("#Tipo").val(),
        AccionSitrel: $("#AccionSitrel").val(),
        ContactoSitrel: $("#ContactoSitrel").val(),
        NombreContactoSitrel: $("#NombreContactoSitrel").val(),
        TelefonoContactoSitrel: $("#TelefonoContactoSitrel").val(),
        RespuestaSitrel: $("#RespuestaSitrel").val(),
        MontoGestionSitrel: $("#MontoGestionSitrel").val(),
        ComentarioSitrel: $("#ComentarioSitrel").val(),
        FechaCompromisoSitrel: $("#FechaCompromisoSitrel").val(),
        MontoCompromisoSitrel: $("#MontoCompromisoSitrel").val(),
        FechaProgramadaSitrel: $("#FechaProgramadaSitrel").val(),
        HoraProgramadaSitrel: $("#HoraProgramadaSitrel").val(),
        MinutoProgramadoSitrel: $("#MinutoProgramadoSitrel").val(),
        CodigoEmpresa: $("#CodigoEmpresa").val(),
        EstadoCpbt: $("#EstadoCpbt").val(),
        Ids: JSON.stringify(ids),
        Documentos: JSON.stringify(jsonObj)
    };
    //alert(JSON.stringify(datos));
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        data: datos,
        success: function (data) {
            if (data > 0) {
                fnBuscarHistorialDeudor();
                fnBuscarCpbtDeudor();
                fnBuscarObservacionDeudor();
                //alert('Gestion guardada exitosamente.');
                $('#ppGestionSitrel').dialog('close');
            } else {
                alert(data);
            }
        },
        error: function (ex) {
            alert('Error al guardar gestion.' + ex);
        }

    });

}


function fnLimpiarCargaItau() {
    $('#frmCargaMasiva').reset();
    $("#imgSubirArchivo").removeClass("ok").removeClass("error");
    $("#btnCargar").attr("disabled", "disabled");
    $("#btnProcesar").attr("disabled", "disabled");
    $("#btnSubmit").removeAttr("disabled");
    // get IDs of all the rows odf jqGrid 
    var rowIds = $('#grdCargaMasivaItau').jqGrid('getDataIDs');
    // iterate through the rows and delete each of them
    for (var i = 0, len = rowIds.length; i < len; i++) {
        var currRow = rowIds[i];
        $('#grdCargaMasivaItau').jqGrid('delRowData', currRow);
    }
    $("#imgSubirArchivoDeudor").removeClass("ok");
    $("#imgSubirArchivoDireccion").removeClass("ok");
    $("#imgSubirArchivoTelefono").removeClass("ok");
    $("#imgSubirArchivoEmail").removeClass("ok");
    $("#imgSubirArchivoOperacion").removeClass("ok");
    $("#imgSubirArchivoCuota").removeClass("ok");
    $("#imgSubirArchivoPago").removeClass("ok");
}

function fnBuscarDireccionDeudor() {
    var newUrl = "/Cartera/GetDireccion/?"
    newUrl += "Ctcid=" + $("#Ctcid").val()
    jQuery("#gridDireccion").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function kpComentarioItau(e) {
    if (e.keyCode != 13 && e.charCode != 59) {
        $("#lblComentarioSitrel").text(250 - $("#ComentarioSitrel").val().length - 1);
    } else {
        e.preventDefault();
        $("#lblComentarioSitrel").text(250 - $("#ComentarioSitrel").val().length);
    }

}

function fnProcesarCargaItau() {
    var newUrl = "/Cartera/SPCargaItau/"
    var postData = $("#frmCargaMasiva").serializeArray();
    if ($('#frmCargaMasiva').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("body").removeClass("loading");
                if (data.length > 0) {
                    for (var i = 0; i <= data.length; i++)
                        $("#grdCargaMasivaItau").jqGrid('addRowData', i + 1, data[i]);
                    alert('Archivos procesados con errores');
                } else {
                    alert('Archivos procesados con exito');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al cargar los archivos.' + ex);
            }

        });
    }
}

function fnArchivoSalidaItau(tipo) {
    var newUrl = "ArchivosSalidaItau/?";
    newUrl += "Pclid=" + $("#Pclid").val() + "&FechaDesde=" + $("#FechaDesde").val() + "&FechaHasta=" + $("#FechaHasta").val() + "&TipoArchivo=" + tipo;
    window.location = newUrl;
}


function fnListarDocumentosHistorialSitrel() {
    var newUrl = "/Cartera/GetDocumentosHistorial/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Tipo=" + $("#Tipo").val()
    jQuery("#grdDocsEstadoSitrel").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnOnSelectHistorial(aRowids) {
    alert(aRowids);
    $("#TodosSeleccionados").val(0);
    alert($("#TodosSeleccionados").val());
}
//Fin Carga Itau

// Judicial 20151207

function handleCKEditorPost() {
    var htmlData = CKEDITOR.instances.editor1.getData();
    //alert(htmlData);
    $.ajax({
        type: 'POST',
        url: "/Judicial/GuardarBorrador", // we are calling json method
        dataType: 'json',
        async: false,
        data: {
            Rolid: $("#Rolid").val(),
            Borradores: $("#Borradores").val(),
            HTMLBorrador: htmlData
        },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (chartsdata) {
            CKEDITOR.instances.editor1.setData(chartsdata);
            alert("Borrador guardado exitosamente.");
        },
        error: function (ex) {
            alert('Error al cargar el borrador.');
        }

    });
}

function fnBorradorSeleccionado() {
    if ($("#Borradores").val() != '' && $("#Borradores").val() != null) {
        $.ajax({
            type: 'POST',
            url: "/Judicial/GetBorrador", // we are calling json method
            dataType: 'json',
            async: false,
            data: {
                Rolid: $("#Rolid").val(),
                Borradores: $("#Borradores").val()
            },
            // here we are get value of selected country and passing same value as input to json method GetStates.
            success: function (chartsdata) {
                fnTraeHistorialBorrador();
                CKEDITOR.instances.editor1.setData(chartsdata);

            },
            error: function (ex) {
                alert('Error al cargar el borrador.');
            }

        });
    }
}


function MensajeCerrarBorradores() {
    if (window.confirm("Está seguro que desea salir sin guardar los últimos cambios?")) {
        return true;
    } else {
        return false;
    }
}

function fnEliminarGestionRol(rowId) {


    var newUrl = "/Judicial/OperRol/"
    var postData = {
        oper: 'del',
        id: rowId
    };
    if (window.confirm("Está seguro que desea eliminar la gestión?")) {

        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                if (data != "OK") {
                    alert(data);
                } else {
                    alert("Gestión eliminada.");
                    //var newUrl = "~/Judicial/GetEstados/?Rolid=" +$("#Rolid").val()
                    jQuery("#gridEstados").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                }
            },
            error: function (ex) {
                alert('Error al eliminar la gestión.' + ex);
            }

        });

        return true;
    } else {
        return false;
    }


}

function fnTraeHistorialBorrador() {
    var newUrl = "/Judicial/GetHistoriaBorrador/";
    var postData = {
        Rolid: $("#Rolid").val(),
        Borradores: $("#Borradores").val()
    };
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        data: postData,
        success: function (data) {
            if (data !== null) {

                $("#PrimerBorrador").text(data.creacion);
                $("#UltimoBorrador").text(data.ultimo);

            } else {
                $("#PrimerBorrador").text("");
                $("#UltimoBorrador").text("");
            }


        }
    });
}

function fnBotonEliminarGestionRol(cellvalue, options, rowobject) {
    if (cellvalue != '0') {
        var ids = cellvalue.split('|');
        if (ids[1] != '0' && ids[2] != '0') {
            return "<div align='center'><button type='button' class='ui-icon ui-icon-trash' style='height:20px;width:20px'  onclick='fnEliminarGestionRol(\"" + cellvalue + "\")'>Eliminar</button></div>";
            // <div align='center'><button style=\"vertical-align:middle\" onclick=\" fnMuestraDocumentoPJ('" + cellvalue + "');\"  class=\"search\"><img width=\"20px\" height=\"20px\" alt=\"Documento\" title=\"Agregar\" src=\"/Images/botones/doc20.png\"></button></div>";
        } else {
            return "";
        }
    } else {
        return "";
    }
}

//2016

function fnCargarTraspasos() {
    jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
}

function fnGuardarTraspasos() {
    var traspasar;
    traspasar = $("#gridPorTraspasar").jqGrid('getGridParam', 'selarrrow');
    //alert(traspasar);
    if (traspasar == "") {
        alert("Debe seleccionar uno o mas deudores para hacer el traspaso.");
    } else {
        var postData = {
            ids: JSON.stringify(traspasar)
        };
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarTraspasos/", // we are calling json method
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
//Comprobantes
function ActualizaSucursalCliente() {
    $("#Sucursal").empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarSucursal", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $.each(states, function (i, state) {
                $("#Sucursal").append('<option value="' + state.Value + '">' +
                    state.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar la sucursal.' + ex);
        }

    });
}

function ActualizaTipoComprobante() {
    $.ajax({
        type: 'POST',
        url: "/Cartera/TraeClasificacionComprobante", // we are calling json method
        dataType: 'json',
        async: false,
        data: { tpcid: $("#TipoComprobante").val() == "" ? 0 : $("#TipoComprobante").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (clas) {
            //alert(JSON.stringify(clas));
            $("#Tipcpbtdoc").val(clas.Tipcpbtdoc);
            $("#Tipprod").val(clas.Tipprod);
            $("#Costos").val(clas.Costos);
            $("#Selcpbt").val(clas.Selcpbt);
            $("#Cartcli").val(clas.Cartcli);
            $("#Contable").val(clas.Contable);
            $("#Selapl").val(clas.Selapl);
            $("#Aplica").val(clas.Aplica);
            $("#Cptoctbl").val(clas.Cptoctbl);
            $("#Findeuda").val(clas.Findeuda);
            $("#Cancela").val(clas.Cancela);
            $("#Libcompra").val(clas.Libcompra);
            $("#Cambiodoc").val(clas.Cambiodoc);
            $("#Remesa").val(clas.Remesa);
            $("#Forpag").val(clas.Forpag);
            $("#Tipdig").val(clas.Tipdig);
            $("#Ordcomp").val(clas.Ordcomp);
            $("#Clbid").val(clas.Clbid);
            $("#Sinimp").val(clas.Sinimp);

            if (clas.Contable == "S") {
                $("#divLblCont").show();
                $("#divDtpCont").show();
            } else {
                $("#divLblCont").hide();
                $("#divDtpCont").hide();
            }

            if (clas.Ordcomp == "S") {
                $("#divOC").show();
            } else {
                $("#divOC").hide();
            }
        },
        error: function (ex) {
            alert('Error al recuperar la clasificacion.' + ex);
        }

    });
}

function fnGuardarComprobante() {
    if ($("#Estado").val() == 6) {
        return false;
    }
    if ($("#Rolid").val() == "" || $("#Rolid").val() == 0) {
        return false;
    }
    if ($("#Sucursal").val() == "") {
        alert('Seleccione sucursal.');
        return false;
    }
    if ($("#TipoComprobante").val() == "") {
        alert('Seleccione tipo de comprobante.');
        return false;
    }
    if ($("#Numero").is(":visible") && $("#Numero").val() == "") {
        alert('Ingrese numero del comprobante.');
        return false;
    }
    if ($("#FormaPago").val() == "") {
        alert('Seleccione forma de pago.');
        return false;
    }
    if ($("#FechaDocumento").val() == "") {
        alert('Ingrese la fecha del documento.');
        return false;
    }
    if ($("#FechaVencimiento").val() == "") {
        alert('Ingrese la fecha de vencimiento.');
        return false;
    }
    if ($("#FechaDocumento").val() == "") {
        alert('Ingrese la fecha del documento.');
        return false;
    }
    if ($("#Contable").val() == "S" && $("#FechaContabilizacion").val() == "") {
        alert('Ingrese la fecha contable.');
        return false;
    }
    if ($("#TipoCambio").val() == "" || $("#TipoCambio").val() == "0") {
        alert('Ingrese el tipo de cambio.');
        return false;
    }


    var postData = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Ccbid: $("#Ccbid").val(),
        CabeceraId: $("#CabeceraId").val(),
        RutCliente: $("#RutCliente").val(),
        NombreCliente: $("#NombreCliente").val(),
        TipoComprobante: $("#TipoComprobante").val(),
        TipoComprobanteDesc: $("#TipoComprobante option:selected").text(),
        Sucursal: $("#Sucursal").val(),
        Numero: $("#Numero").val(),
        NumeroOC: $("#NumeroOC").val(),
        FechaIngreso: $("#FechaIngreso").val(),
        FechaDocumento: $("#FechaDocumento").val(),
        FechaVencimiento: $("#FechaVencimiento").val(),
        FechaOrdenCompra: $("#FechaOrdenCompra").val(),
        FechaEntrega: $("#FechaEntrega").val(),
        FechaContabilizacion: $("#FechaContabilizacion").val(),
        Moneda: $("#Moneda").val(),
        TipoCambio: $("#TipoCambio").val(),
        FormaPago: $("#FormaPago").val(),
        TipoGasto: $("#TipoGasto").val(),
        MotivoCobranza: $("#MotivoCobranza").val(),
        Estado: $("#Estado").val(),
        Glosa: $("#Glosa").val(),

        Tipcpbtdoc: $("#Tipcpbtdoc").val(),
        Tipprod: $("#Tipprod").val(),
        Costos: $("#Costos").val(),
        Selcpbt: $("#Selcpbt").val(),
        Cartcli: $("#Cartcli").val(),
        Contable: $("#Contable").val(),
        Selapl: $("#Selapl").val(),
        Aplica: $("#Aplica").val(),
        Cptoctbl: $("#Cptoctbl").val(),
        Findeuda: $("#Findeuda").val(),
        Cancela: $("#Cancela").val(),
        Libcompra: $("#Libcompra").val(),
        Cambiodoc: $("#Cambiodoc").val(),
        Remesa: $("#Remesa").val(),
        Forpag: $("#Forpag").val(),
        Tipdig: $("#Tipdig").val(),
        Ordcomp: $("#Ordcomp").val(),
        Clbid: $("#Clbid").val(),
        Sinimp: $("#Sinimp").val(),

        Tipo: $("#Tipo").val(),
        PJ: $("#PJ").val(),
        Pag: $("#Pag").val(),
        Rolid: $("#Rolid").val(),
        Nombre: $("#Codigo").val()
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/GrabarComprobante/", // we are calling json method
        dataType: 'json',
        async: false,
        beforeSend: function () { $("body").addClass("loading"); },
        data: postData,
        success: function (data) {
            if (data != -1) {
                $("body").removeClass("loading");
                if (data.mensaje == "") {
                    $("#CabeceraId").val(data.id);
                    fnActualizarDatosComprobante();
                    return data.id;
                } else {
                    if (data.repetido < 1) alert(data.mensaje);
                }

                //    jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                //    fnCargarTraspasosPendientes();
            } else {
                $("body").removeClass("loading");
                //    alert('Error al guardar traspasos.');
            }
        },
        error: function (ex) {
            $("body").removeClass("loading");
            alert('Error al guardar traspasos.' + ex);
        }

    });

}

function fnAgregarCabeceraDetalle() {
    if ($("#Estado").val() != 6) {
        $("#Insid").val("");
        $("#Codigo").val("");
        $("#Monto").val("");
        $("#Cantidad").val("");
        $("#Item").val("");
        $("#Modo").val("add");
        $('#ppCabeceraDetalleCompra').dialog('open');
    }
}

function fnAutocompleteItems(request, response) {
    var postData = {
        pclid: $("#Pclid").val(),
        gasto: $("#TipoGasto").val(),
        tipprod: $("#Tipprod").val(),
        term: request.term
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/BuscarItemComprobante/", // we are calling json method
        dataType: 'json',
        async: true,
        data: postData,
        success: function (data) {
            $("body").removeClass("ui-autocomplete-loading");
            response(data);
        }

    });
}

function fnActualizarFormularioComprobante() {
    if ($("#Tribunal").val() == "") {
        //alert('Seleccione tribunal.');
        return false;
    }
    if ($("#TipoRol").val() == "") {
        //alert('Seleccione tipo de rol.');
        return false;
    }
    if ($("#Rol").val() == "") {
        //alert('Ingrese numero del rol.');
        return false;
    }
    if ($("#Anio").val() == "") {
        //alert('Ingrese año del rol.');
        return false;
    }

    var postData = {
        Tribunal: $("#Tribunal").val(),
        TipoRol: $("#TipoRol").val(),
        Rol: $("#Rol").val(),
        Anio: $("#Anio").val()
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/TraeRolComprobante/", // we are calling json method
        dataType: 'json',
        async: true,
        data: postData,
        beforeSend: function () { $("body").addClass("loading"); },
        success: function (data) {
            if (data.Rolid == 0) {
                $("#imgRol").removeClass("ok").addClass("error");
                //alert("Revise los datos, el rol no existe en el sistema.")
            } else {
                $("#imgRol").removeClass("error").addClass("ok");
                $("#Rolid").val(data.Rolid);
            }
            $("#RutNombreCliente").val(data.NombreCliente);
            $("#RutNombreDeudor").val(data.NombreDeudor);
            $("#Asegurados").val(data.Asegurados);
            //ActualizaSucursalCliente();
            $("body").removeClass("loading");
        }

    });

}

function fnGuardarComprobanteDetalle() {
    if ($("#Rolid").val() == "" || $("#Rolid").val() == 0) {
        return false;
    }
    if ($("#Sucursal").val() == "") {
        alert('Seleccione sucursal.');
        return false;
    }
    if ($("#TipoComprobante").val() == "") {
        alert('Seleccione tipo de comprobante.');
        return false;
    }
    if ($("#Numero").is(":visible") && $("#Numero").val() == "") {
        alert('Ingrese numero del comprobante.');
        return false;
    }
    if ($("#FormaPago").val() == "") {
        alert('Seleccione forma de pago.');
        return false;
    }
    if ($("#FechaDocumento").val() == "") {
        alert('Ingrese la fecha del documento.');
        return false;
    }
    if ($("#FechaVencimiento").val() == "") {
        alert('Ingrese la fecha de vencimiento.');
        return false;
    }
    if ($("#FechaDocumento").val() == "") {
        alert('Ingrese la fecha del documento.');
        return false;
    }
    if ($("#Contable").val() == "S" && $("#FechaContabilizacion").val() == "") {
        alert('Ingrese la fecha contable.');
        return false;
    }
    if ($("#TipoCambio").val() == "" || $("#TipoCambio").val() == "0") {
        alert('Ingrese el tipo de cambio.');
        return false;
    }

    fnGuardarComprobante();

    var postData = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Ccbid: $("#Ccbid").val(),
        CabeceraId: $("#CabeceraId").val(),
        RutCliente: $("#RutCliente").val(),
        NombreCliente: $("#NombreCliente").val(),
        TipoComprobante: $("#TipoComprobante").val(),
        TipoComprobanteDesc: $("#TipoComprobante option:selected").text(),
        Sucursal: $("#Sucursal").val(),
        Numero: $("#Numero").val(),
        NumeroOC: $("#NumeroOC").val(),
        FechaIngreso: $("#FechaIngreso").val(),
        FechaDocumento: $("#FechaDocumento").val(),
        FechaVencimiento: $("#FechaVencimiento").val(),
        FechaOrdenCompra: $("#FechaOrdenCompra").val(),
        FechaEntrega: $("#FechaEntrega").val(),
        FechaContabilizacion: $("#FechaContabilizacion").val(),
        Moneda: $("#Moneda").val(),
        TipoCambio: $("#TipoCambio").val(),
        FormaPago: $("#FormaPago").val(),
        TipoGasto: $("#TipoGasto").val(),
        MotivoCobranza: $("#MotivoCobranza").val(),
        Estado: $("#Estado").val(),
        Glosa: $("#Glosa").val(),

        Tipcpbtdoc: $("#Tipcpbtdoc").val(),
        Tipprod: $("#Tipprod").val(),
        Costos: $("#Costos").val(),
        Selcpbt: $("#Selcpbt").val(),
        Cartcli: $("#Cartcli").val(),
        Contable: $("#Contable").val(),
        Selapl: $("#Selapl").val(),
        Aplica: $("#Aplica").val(),
        Cptoctbl: $("#Cptoctbl").val(),
        Findeuda: $("#Findeuda").val(),
        Cancela: $("#Cancela").val(),
        Libcompra: $("#Libcompra").val(),
        Cambiodoc: $("#Cambiodoc").val(),
        Remesa: $("#Remesa").val(),
        Forpag: $("#Forpag").val(),
        Tipdig: $("#Tipdig").val(),
        Ordcomp: $("#Ordcomp").val(),
        Clbid: $("#Clbid").val(),
        Sinimp: $("#Sinimp").val(),

        Tipo: $("#Tipo").val(),
        PJ: $("#PJ").val(),
        Pag: $("#Pag").val(),

        Insid: $("#Insid").val(),
        Codigo: $("#Codigo").val(),
        Monto: $("#Monto").val(),
        Cantidad: $("#Cantidad").val(),
        Rolid: $("#Rolid").val(),
        Modo: $("#Modo").val(),
        Item: $("#Item").val(),
        ImpuestoRetenido: $("#ImpuestoRetenido").is(":checked")
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/GrabarDetalleComprobante/", // we are calling json method
        dataType: 'json',
        async: true,
        beforeSend: function () { $("body").addClass("loading"); },
        data: postData,
        success: function (data) {
            if (data != -1) {
                $("body").removeClass("loading");
                $('#ppCabeceraDetalleCompra').dialog('close');
                CargarDetalleCompra();
                $("#Insid").val("");
                $("#Codigo").val("");
                $("#Monto").val("");
                $("#Cantidad").val("");
                $("#Modo").val("");
                $("#Item").val("");
                fnActualizarDatosComprobante();
                $("#btnGrabar").show();

            } else {
                $("#btnGrabar").hide();
                $("body").removeClass("loading");
                alert('Error al guardar detalle.');
            }
        },
        error: function (ex) {
            $("body").removeClass("loading");
            alert('Error al guardar detalle.' + ex);
        }

    });

}

function CargarDetalleCompra() {
    var newUrl = "/Cartera/GetDetalleCompra/?"
    newUrl += "tcp=" + $("#TipoComprobante").val() + "&numero=" + $("#CabeceraId").val();
    jQuery("#gridItemC").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function DetalleCompraSeleccionado(rowid, s) {
    if ($("#Estado").val() != 6) {
        var row = $("#gridItemC").getRowData(rowid);
        var ids = rowid.split('|');
        //alert(row.Precio.split('.')[0]);
        $("#Item").val(ids[2]);
        $("#Insid").val(ids[3]);
        $("#Codigo").val(row.Codigo + " - " + row.Nombre);
        $("#Monto").val(row.Precio.split('.')[0]);
        $("#Cantidad").val(row.Cantidad.split('.')[0]);
        $("#Modo").val("edit");

        if (row.Retenido == "SI") {
            $('#ImpuestoRetenido').prop('checked', true);
        }
        else {
            $('#ImpuestoRetenido').prop('checked', false);
        }

        $('#ppCabeceraDetalleCompra').dialog('open');
    }
}

function fnAnularComprobante() {
    if ($("#Estado").val() != 6) {

        if ($("#Sucursal").val() == "") {
            alert('Seleccione sucursal.');
            return false;
        }
        if ($("#TipoComprobante").val() == "") {
            alert('Seleccione tipo de comprobante.');
            return false;
        }
        if ($("#Numero").is(":visible") && $("#Numero").val() == "") {
            alert('Ingrese numero del comprobante.');
            return false;
        }

        if ($("#FormaPago").val() == "") {
            alert('Seleccione forma de pago.');
            return false;
        }
        if ($("#FechaDocumento").val() == "") {
            alert('Ingrese la fecha del documento.');
            return false;
        }
        if ($("#FechaVencimiento").val() == "") {
            alert('Ingrese la fecha de vencimiento.');
            return false;
        }
        if ($("#FechaDocumento").val() == "") {
            alert('Ingrese la fecha del documento.');
            return false;
        }
        if ($("#Contable").val() == "S" && $("#FechaContabilizacion").val() == "") {
            alert('Ingrese la fecha contable.');
            return false;
        }
        if ($("#TipoCambio").val() == "" || $("#TipoCambio").val() == "0") {
            alert('Ingrese el tipo de cambio.');
            return false;
        }


        var postData = {
            CabeceraId: $("#CabeceraId").val(),
            TipoComprobante: $("#TipoComprobante").val(),
            TipoCambio: $("#TipoCambio").val(),
            Tipo: $("#Tipo").val(),
            PJ: $("#PJ").val(),
            Pag: $("#Pag").val()
            //ids: JSON.stringify(traspasar)
        };
        $.ajax({
            type: 'POST',
            url: "/Cartera/AnularComprobante/", // we are calling json method
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                if (data == "") {
                    $("body").removeClass("loading");
                    CargarDetalleCompra();
                    fnActualizarDatosComprobante();
                } else {
                    $("body").removeClass("loading");
                    alert(data.mensaje);
                }
                fnNuevoComprobante();
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar traspasos.' + ex);
            }

        });

    }
}

function fnNuevoComprobante() {
    var url = "/Cartera/Comprobante/?tipo=" + $("#Tipo").val() + "&pag=203&pj=" + $("#PJ").val();
    window.location.href = url;
}

function fnActualizarDatosComprobante() {
    if ($("#Estado").val() != 6) {


        if ($("#TipoComprobante").val() == "") {
            alert('Seleccione tipo de comprobante.');
            return false;
        }
        if ($("#CabeceraId").val() == "0") {
            alert('Seleccione forma de pago.');
            return false;
        }


        var postData = {
            CabeceraId: $("#CabeceraId").val(),
            TipoComprobante: $("#TipoComprobante").val()
            //TipoCambio: $("#TipoCambio").val(),
            //Tipo: $("#Tipo").val(),
            //PJ: $("#PJ").val(),
            //Pag: $("#Pag").val()
            //ids: JSON.stringify(traspasar)
        };
        $.ajax({
            type: 'POST',
            url: "/Cartera/ActualizarDatosComprobante/", // we are calling json method
            dataType: 'json',
            async: true,
            //beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                $("#Estado").val(data.Estado);
                $("#Glosa").val(data.Glosa);

                $("#Subtotal").val(data.Subtotal);
                $("#Descuento").val(data.Descuento);
                $("#Neto").val(data.Neto);
                $("#Impuestos").val(data.Impuestos);
                $("#Retenido").val(data.Retenido);
                $("#Total").val(data.Total);
            },
            error: function (ex) {
                //$("body").removeClass("loading");
                alert('Error al guardar traspasos.' + ex);
            }

        });

    }
}

//




//Recordatorio
//SMS


function fnFormularioRecordatorio(tipo) {
    $("#frmGuardarRecordatorio").reset();
    switch (tipo) {
        case "SMS":
            $("#TipoForm").val(tipo);
            $("#divRecordatorioEmail").hide();
            $("#divRecordatorioTelefono").show();
            $('#ppRecordatorio').dialog('option', 'title', 'Agregar SMS');
            break;
        case "E":
            $("#TipoForm").val(tipo);
            $("#divRecordatorioTelefono").hide();
            $("#divRecordatorioEmail").show();
            $('#ppRecordatorio').dialog('option', 'title', 'Agregar Email');
            break;
    }
    $('#ppRecordatorio').dialog('open');
}

function fnGuardarRecordatorio() {
    if ($("#TipoForm").val() == "SMS" && $("#TelefonoRecordatorio").val() == "") {
        alert("Ingrese telefono.")
        return false;
    }
    if ($("#TipoForm").val() == "E" && $("#EmailRecordatorio").val() == "") {
        alert("Ingrese email.")
        return false;
    }
    if ($("#FechaEnvioRecordatorio").val() == "") {
        alert("Ingrese fecha de envio.")
        return false;
    }
    if ($('#frmGuardarRecordatorio').valid()) {
        var postData = {
            TipoForm: $("#TipoForm").val(),
            EmailRecordatorio: $("#EmailRecordatorio").val(),
            TelefonoRecordatorio: $("#TelefonoRecordatorio").val(),
            FechaEnvioRecordatorio: $("#FechaEnvioRecordatorio").val(),
            Ctcid: $("#Ctcid").val()
        };
        $.ajax({
            type: 'POST',
            url: "/Cartera/GuardarRecordatorio/", // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                if (data != "") {
                    alert(data);
                } else {
                    $('#ppRecordatorio').dialog('close');
                }

            }

        });
    }
    fnBuscarSMSPreDeudor();
    fnBuscarEmailPreDeudor();
    $('#TelefonoRecordatorio').attr('readonly', false);
    $('#EmailRecordatorio').attr('readonly', false);
}

function fnBuscarSMSPreDeudor() {
    var newUrl = "/Cartera/GetSMSPreDeudor/?"
    newUrl += "ctcid=" + $("#Ctcid").val()
    jQuery("#gridSMS").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarEmailPreDeudor() {
    var newUrl = "/Cartera/GetEmailPreDeudor/?"
    newUrl += "ctcid=" + $("#Ctcid").val()
    jQuery("#gridEmailPre").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBotonesSMSPre(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarSMSPre(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarSMSPre(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnBotonesEmailPre(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarEmailPre(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarEmailPre(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnEditarSMSPre(id) {
    //alert(id);
    fnFormularioRecordatorio('SMS');
    var datos = id.split(',');
    $("#TelefonoRecordatorio").val(datos[0]);
    $('#TelefonoRecordatorio').attr('readonly', true);
    var date = new Date(parseInt(datos[1].substr(6)));
    $("#FechaEnvioRecordatorio").val(date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear());
}

function fnEliminarSMSPre(id) {
    var r = confirm("Desea eliminar el SMS de contacto?");
    if (r == true) {
        var datos = id.split(',');
        var url = "/Cartera/EliminarSMSPre/?id=" + $("#Ctcid").val() + '|' + datos[0];
        $.ajax({
            type: 'POST',
            url: url, // we are calling json method
            dataType: 'json',
            async: true,
            //data: postData,
            success: function (data) {
                //if (data === 1) {
                //    alert("Sub-Cartera eliminada con éxito.");
                //} else {
                //    alert("Sub-Cartera no fue eliminada, revise los datos.");
                //}
            },
            error: function (ex) {
                alert('Error al eliminar telefono.' + ex);
            }

        });
        fnBuscarSMSPreDeudor();
    }

}

function fnEditarEmailPre(id) {
    //alert(id);
    fnFormularioRecordatorio('E');
    var datos = id.split(',');
    $("#EmailRecordatorio").val(datos[0]);
    $('#EmailRecordatorio').attr('readonly', true);
    var date = new Date(parseInt(datos[1].substr(6)));
    $("#FechaEnvioRecordatorio").val(date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear());
}

function fnEliminarEmailPre(id) {
    var r = confirm("Desea eliminar el email de contacto?");
    if (r == true) {
        var datos = id.split(',');
        var url = "/Cartera/EliminarEmailPre/?id=" + $("#Ctcid").val() + '|' + datos[0];
        $.ajax({
            type: 'POST',
            url: url, // we are calling json method
            dataType: 'json',
            async: true,
            //data: postData,
            success: function (data) {
                //if (data === 1) {
                //    alert("Sub-Cartera eliminada con éxito.");
                //} else {
                //    alert("Sub-Cartera no fue eliminada, revise los datos.");
                //}
            },
            error: function (ex) {
                alert('Error al eliminar email.' + ex);
            }

        });
        fnBuscarEmailPreDeudor();
    }
}

//Recordatorio

//cartera 20160411
function SelectDemandaPendiente(rowid, e) {
    var checked = $(e.target).is(":checked");

    var row = $("#Documentos").getRowData(rowid);
    var postData = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Ccbid: row.Ccbid,
        DemandaPendiente: checked
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/DemandaPendienteCpbt/", // we are calling json method
        dataType: 'json',
        async: true,
        data: postData,
        success: function (data) {
            if (data != "0") {
                //alert(data);
                return true;
            } else {
                return false;
            }

        }

    });
}

function fnLinkPJ(cellvalue, options, rowdata) {
    //alert(rowdata);
    return "<a href=\'#\' onclick=\"window.open(\'" + rowdata[5] + "\')\" >" + cellvalue + "</a>";
}

//cartera 20160411

// grabar categoria 20160422

function fnGrabarCategoria() {

    var postData = {
        ctcid: $("#Ctcid").val(),
        categoria: $("#Categoria").val()
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/GrabarCategoria/", // we are calling json method
        dataType: 'json',
        async: true,
        data: postData,
        success: function (data) {
        }

    });

}
// grabar categoria 20160422


function kpBuscarDeudorEvento(e) {
    if (e.keyCode == 13) {
        fnBuscarDeudores();
    }
}

/// 20160505 judicial


function fnCargarTotales() {
    $.ajax({
        type: 'POST',
        url: "/Judicial/GetTotalDocsAsignados", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Rolid: $("#Rolid").val() },
        success: function (total) {
            totalMontoAsignado = total.monto;
            totalSaldoAsignado = total.saldo;
            //$("#divTotalMontoAsig").html(total.monto);
            //$("#divTotalSaldoAsig").html(total.saldo);
        }

    });

    $.ajax({
        type: 'POST',
        url: "/Judicial/GetTotalDocsPorAsignar", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val() },
        success: function (total) {
            totalMontoPorAsignar = total.monto;
            totalSaldoPorAsignar = total.saldo;
            //$("#divTotalMontoSinAsig").html(total.monto);
            //$("#divTotalSaldoSinAsig").html(total.saldo);
        }

    });
}

//20160526
function fnActualizarMoverCartera() {
    var newUrl = "/Cartera/GetMoverCartera/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Estado=" + $("#Estado").val() + "&Comentario=" + $("#Comentario").val();


    jQuery("#gridMoverCartera").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnMoverDocumentos() {
    var postData = {
        Pclid: $("#Pclid").val(),
        Ctcid: $("#Ctcid").val(),
        Comentario: $("#Comentario").val(),
        Estado: $("#Estado").val(),
        EstadoDocumento: $("#EstadoDocumento").val(),
        Ids: JSON.stringify(idsOfSelectedRows)
    };

    //alert(JSON.stringify(idsOfSelectedRows));

    $.ajax({
        type: 'POST',
        url: "/Cartera/GrabarMoverDocumentos/", // we are calling json method
        dataType: 'json',
        async: false,
        data: postData,
        success: function (data) {
            if (data != "") {
                alert(data);
            }
        }

    });
    fnActualizarMoverCartera();

}

//Predefinidos
function fnMultiFormato(extension) {
    //function fnMultiFormato(url, formato) {
    $('#ppTipReporte').dialog('close');
    //var archivo = url.substring(0, url.length - 4);         

    switch (extension) {
        case 1: //PDF
            var newUrl = "/Reportes/GeneraReporteArbolJudicial/?&rep=" + $("#Reporte").val() + "&pag=" + $("#pag").val() + "&abogados=" + JSON.stringify(jQuery('#gridAbogado').jqGrid('getGridParam', 'selarrrow'))
            //$('#ppDocto').html('<table><tr><td>Descargar Excel</td><td><a href="' + archivo + '.xls"><img src="\\Images\\xls.png"></a></td></tr><tr><td>Descargar Pdf</td><td><a href="' + archivo + '.pdf"><img src="\\Images\\pdf.png"></a></td></table>');
            //$('#ppDocto').html('<table><tr><td>Descargar Excel</td><td><a href="' + archivo + '.xls"><img src="\\Images\\xls.png"></a></td></tr><tr><td>Descargar Pdf</td><td><button class="search" id="btnRepPDF" onclick="' + fnMuestraReportePredefinido(archivo + '.pdf') + '"><img src="\\Images\\pdf.png" width="40px" height="40px" title="PDF" alt="PDF" /> </button></td></table>');
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                beforeSend: function () { $("body").addClass("loading"); },
                success: function (data) {
                    $("body").removeClass("loading");
                    fnMuestraReportePredefinido(data);
                }

            });

            break;
        case 2: //XLS
            var newUrl = "/Reportes/GenerarReporteXLS/?pclid=" + $("#Pclid").val() + "&ctcid=" + $("#Ctcid").val() + "&rep=" + $("#Reporte").val() + "&pag=" + $("#Pag").val() + "&abogados=" + JSON.stringify(jQuery('#gridAbogado').jqGrid('getGridParam', 'selarrrow'));
            window.location = newUrl;
            break;
        default:
            $('#ppDocto').html('No hay archivos para visualizar');
    }


}
//Repetida en site.js ********************
function fnMuestraReportePredefinido(url) {

    var extension = url.split('.');
    var ext = extension[extension.length - 1];
    switch (ext) {
        case "jpg":
        case "JPG":
            $('#ppDocto').html('<img src="' + url + '" 	style="max-width: 870px;"/>');
            break;
        case "pdf":
        case "PDF":
            $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
            break;
        default:
            $('#ppDocto').html('El archivo de extension ' + ext + ' no puede ser visualizado');
    }
    $('#ppDocto').dialog('open');
}

////Predefinidos cartera
function SeleccionaCliente() {
    ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
}

function ActualizaComboCodigoCarga(controlOrigen, controlDestino) {
    if ($("#" + controlOrigen).val() != '') {
        $("#" + controlDestino).empty();
        $.ajax({
            type: 'POST',
            url: "/Reportes/ListarCodigoCarga", // we are calling json method
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
                alert('Ingrese el cliente.');
            }

        });
    }
}

//Predefinidos cartera

//20161110
function fnReporteSeleccionado() {
    //$('div[id ^= xdiv]').hide();
    $("#xdivSituacionCartera").hide();

    if ($("#pag").val() == 357) {
        $("#xdivTipoCartera").show();
        switch ($("#Reporte").val()) {
            case '1':
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").show();
                $("#xdivSituacionCartera").hide();
                $("#xdivCodigoCarga").show();
                $("#xdivVencidos").hide();
                $("#xdivEstadoDocumento").show();
                break;
            case '7':
                $("#xdivRangoFecha").show();
                $("#xdivGestor").hide();
                $("#xdivSituacionCartera").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivVencidos").hide();
                $("#xdivEstadoDocumento").hide();
                break;
            case '6':
                $("#xdivCodigoCarga").hide();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                $("#xdivEstadoDocumento").show();
                break;
            case '19':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                $("#xdivEstadoDocumento").hide();
                break;
            case '22':
                $("#xdivCodigoCarga").show();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                $("#xdivEstadoDocumento").show();
                break;
            case '16':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").show();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                $("#xdivEstadoDocumento").hide();
                break;
            case '21':
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").show();
                $("#xdivSituacionCartera").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivVencidos").show();
                $("#xdivEstadoDocumento").hide();
                break;
            case '23':
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivSituacionCartera").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivVencidos").hide();
                $("#xdivTipoCartera").hide();
                $("#xdivEstadoDocumento").hide();
                break;
        }
    } else if ($("#pag").val() == 358) {
        $("#xdivTipoCartera").hide();
        switch ($("#Reporte").val()) {
            case '1':
            case '2':
            case '12':
            case '13':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").show();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").hide();
                $("#xdivRol").hide();
                break;
            case '16':
            case '3':
            case '4':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").show();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").show();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").hide();
                $("#xdivRol").hide();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                break;
            case '9':
                $("#xdivNombreRutDeudor").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").show();
                $("#xdivSituacionCartera").hide();
                $('#SituacionCartera > option[value="J"]').attr('selected', 'selected');
                $("#xdivDiasPrescr").hide();
                $("#xdivRol").show();
                break;
            case '14':
                $("#xdivGridAbogado").show();
                $("#xdivNombreRutCliente").hide();
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").hide();
                $("#xdivRol").hide();
                break;
            case '15':
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").hide();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").show();
                $("#xdivRol").hide();
                break;
        }
    }
}

function SeleccionaCliente() {
    ActualizaComboCodigoCargaReportes("Pclid", "CodigoCarga");
}

function ActualizaComboCodigoCargaReportes(controlOrigen, controlDestino) {
    if ($("#" + controlOrigen).val() != '') {
        $("#" + controlDestino).empty();
        $.ajax({
            type: 'POST',
            url: "/Reportes/ListarCodigoCarga", // we are calling json method
            dataType: 'json',
            async: false,
            data: { codemp: 1, pclid: $("#" + controlOrigen).val() },
            // here we are get value of selected country and passing same value as input to json method GetStates.
            success: function (states) {
                $.each(states, function (i, state) {
                    $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                    // here we are adding option for States
                });
            },
            error: function (ex) {
                alert('Ingrese el cliente.');
            }

        });
    }
}

//predefinidos
// BH Contabilidad y facturacion   20160915
function fnContabilizarComprobantes() {
    var s;
    s = idsOfSelectedRows;//$("#gridAceptarComprobante").jqGrid('getGridParam', 'selarrrow');
    var postData = { ids: s };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/ContabilizaBH", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: false,
            data: postData,
            success: function (data) {
                if (data != "" && data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al aceptar el(los) comprobantes.' + ex);
            }

        });
        jQuery("#gridContaComprobante").jqGrid().trigger('reloadGrid', [{ page: 1 }])
    }
}

function fnFacturarComprobantes() {
    var s;
    s = idsOfSelectedRows;//$("#gridAceptarComprobante").jqGrid('getGridParam', 'selarrrow');
    var postData = { ids: s };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/FacturaBH", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: false,
            data: postData,
            success: function (data) {
                if (data != "" && data != null) {
                    alert(data);
                }
            },
            error: function (ex) {
                alert('Error al aceptar el(los) comprobantes.' + ex);
            }

        });
        jQuery("#gridFacturaComprobante").jqGrid().trigger('reloadGrid', [{ page: 1 }])
    }
}

function fnExtraerBH() {
    var newUrl = "GeneraSalidaBH/?";
    newUrl += "desde=" + $("#FechaEmisionDesde").val() + "&hasta=" + $("#FechaEmisionHasta").val() + "&tipo=" + $("#Estado").val();
    window.location = newUrl;
}

function fnEliminarBH() {
    var newUrl = "GeneraSalidaBH/?";
    newUrl += "desde=" + $("#FechaEmisionDesde").val() + "&hasta=" + $("#FechaEmisionHasta").val() + "&tipo=" + $("#Estado").val();
    window.location = newUrl;
}

function fnCargarAceptarComprobantes() {
    jQuery("#gridAceptarComprobante").jqGrid().trigger('reloadGrid', [{ page: 1 }])
}

// BH Contabilidad y facturacion 

// Castigo y Devolucion

function fnBuscarDocsCastDev() {
    var newUrl = "/Cartera/GetCastigoDevolucion/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Estado=" + $("#Estado").val() + "&Cartera=" + $("#Cartera").val() + "&Tipo=" + $("#Tipo").val();


    jQuery("#gridDocumentos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnGrabarCastDev() {
    if ($("#Pclid").val() != '' && $("#Ctcid").val() != '' && $("#Tipo").val() != '' && $("#TipoComprobante").val() != '' && $("#Estado").val() != '' && $("#Cartera").val() != '' && idsOfSelectedRows.length > 0) {
        var postData = {
            Pclid: $("#Pclid").val(),
            Ctcid: $("#Ctcid").val(),
            Tipo: $("#Tipo").val(),
            TipoComprobante: $("#TipoComprobante").val(),
            Estado: $("#Estado").val(),
            Cartera: $("#Cartera").val(),
            Glosa: $("#Glosa").val(),
            Ids: JSON.stringify(idsOfSelectedRows)
        };

        //alert(JSON.stringify(idsOfSelectedRows));

        $.ajax({
            type: 'POST',
            url: "/Cartera/GrabarCastigoDevolucion/", // we are calling json method
            dataType: 'json',
            async: false,
            data: postData,
            success: function (data) {
                if (data != "") {
                    alert(data);
                }
            }

        });
        fnActualizarMoverCartera();
    } else {
        alert("Ingrese todos los datos.");
    }

}

function fnOnSelectAllCastigo(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateIdsOfSelectedRows(id, status);
    }
}

function fnOnLoadCompleteCastigo(grilla, data) {
    var cartera = "V";
    //alert($("#Cartera").val());
    if ($("#Cartera").val() != "P") {
        cartera = $("#Cartera").val();
    }
    //alert(cartera);
    if (data.rows.length > 0) {
        for (var i = 0; i < data.rows.length; i++) {
            if (data.rows[i].cell[7] != cartera) {
                //alert(data.rows[i].cell[7]);
                $("[id='jqg_" + grilla.id + "_" + data.rows[i].id + "']").attr("disabled", "disabled");
            }
        }
    }
    var i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
    }
}

//

// control proceso 20161212

function fnInterruptorProceso(cellvalue, options, rowobject) {
    //alert(cellvalue);
    var button = '';
    if (cellvalue == true) {
        button = '<button type="button" class="proceso-on"  onclick="fnAccionarInterruptorProceso(\'' + rowobject + '\')" ></button>';
    } else {
        button = '<button type="button" class="proceso-off" onclick="fnAccionarInterruptorProceso(\'' + rowobject + '\')" ></button>';
    }
    return button;
}

function fnAccionarInterruptorProceso(value) {

    var postData = {
        Action: value
    };

    //alert(JSON.stringify(idsOfSelectedRows));

    $.ajax({
        type: 'POST',
        url: "/Cartera/InterruptorProceso/", // we are calling json method
        dataType: 'json',
        async: false,
        data: postData,
        success: function (data) {
            if (data != "") {
                //alert(data);
            }
        }

    });
    jQuery("#gridProceso").jqGrid().trigger('reloadGrid', [{ page: 1 }])
}



// control proceso 20161212

// castigos masivos
function fnCargarCastigoMasivo() {
    var newUrl = "/Cartera/ProcesoCastigoMasivo/"
    var postData = $("#frmCastigoMasivo").serializeArray();
    if ($('#Pclid').val() != '' && $('#Archivo').val() != '') {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("body").removeClass("loading");
                if (data.length > 0) {
                    for (var i = 0; i <= data.length; i++)
                        $("#grdCargaMasiva").jqGrid('addRowData', i + 1, data[i]);
                    alert('Archivo cargado con errores');
                } else {
                    alert('Archivo cargado con exito');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al cargar el archivo.' + ex);
            }

        });
    }
}



// castigos masivos

// Envío masivo de emails
function fnBuscarClientesTemplates() {
    var pclid = $('#Pclid').val();
    if (pclid) {
        var url = "/Email/ListarEmailCliente/" + pclid;
        $.ajax({
            type: 'GET',
            url: url, // we are calling json method
            dataType: 'json',
            beforeSend: function () {
                $("body").addClass("loading");
                $('#Template').html('');
                $('.template-control').css('display', 'none');
            },
            success: function (data) {
                $("body").removeClass("loading");
                $('#Template').append($('<option>', {
                    value: '',
                    text: 'General',
                    selected: true
                }));
                $.each(data, function (index, item) {
                    $('#Template').append($('<option>', {
                        value: item.Filename,
                        text: item.TemplateId + ' - ' + item.Alias
                    }));
                });
                if (data.length)
                    $('.template-control').css('display', 'inline-block');
            },
            error: function (ex) {
                $("body").removeClass("loading");
            }
        });
    }

    buscarEstadosClientes(pclid);
}

function buscarEstadosClientes(pclid) {
    var newUrl = "/Email/GetEstadosCliente?";
    newUrl += "Pclid=" + pclid;
    jQuery("#gridEstado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid');
}

var preview;
$('document').ready(function () {
    preview = $("#templatePreview").dialog({
        autoOpen: false,
        height: 400,
        width: 700,
        modal: true,
        buttons: {
            "Cerrar preview": function () {
                preview.dialog("close");
            }
        },
        close: function () {
            //Logic on close dialog
        }
    });
});

function showEmailTemplatePreview() {
    getTemplatePreviewContent();
}

function getTemplatePreviewContent() {
    var pclid = $('#Pclid').val();
    var filename = $('#Template').val();
    if (!filename || filename === '') {
        pclid = "0";
        filename = "GENERAL";
    }
    if (pclid && filename) {
        var url = "/Email/Previewtemplate?pclid=" + pclid + "&filename=" + filename;
        $.ajax({
            type: 'GET',
            url: url, // we are calling json method
            dataType: 'json',
            beforeSend: function () {
                $("body").addClass("loading");
            },
            success: function (data) {
                $("body").removeClass("loading");
                $('#templatePreview').html(data).text();
                preview.dialog("open");
            },
            error: function (ex) {
                $("body").removeClass("loading");
            }
        });
    }
}

function generateFiltersUrl(Path) { //Generar filtros de url email masivos
    var pclid = $('#Pclid').val();
    if (!pclid)
        return '';

    var BasePath = Path + "?";
    BasePath += "Pclid=" + pclid;

    var filename = $('#Template').val();
    if (filename) BasePath += " &Template=" + filename;

    var estadosRow = JSON.stringify(jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow'));
    if (estadosRow) BasePath += "&Estados=" + estadosRow;

    var gestoresRow = JSON.stringify(jQuery('#gridGestor').jqGrid('getGridParam', 'selarrrow'));
    if (gestoresRow) BasePath += "&Gestores=" + gestoresRow;

    var liquidacion = $('#Liquidacion').val();
    if (liquidacion) BasePath += "&Liquidacion=" + liquidacion;

    var liquidacionTipo = $('#TipoLiquidacion').val();
    if (liquidacionTipo) BasePath += "&TipoLiquidacion=" + liquidacionTipo;

    var carteraTipo = $('#TipoCartera').val();
    if (carteraTipo) BasePath += "&TipoCartera=" + carteraTipo;

    var montoDesde = $('#MontoDesde').val();
    if (montoDesde) BasePath += "&MontoDesde=" + montoDesde;

    var montoHasta = $('#MontoHasta').val();
    if (montoHasta) BasePath += "&MontoHasta=" + montoHasta;

    var fechaVencimiento = $('#FechaVencimiento').datepicker('getDate');
    if (fechaVencimiento)
        var formatedDate = fechaVencimiento.getFullYear() + '/' + ("0" + (fechaVencimiento.getMonth() + 1)).slice(-2) + "/" + ("0" + (fechaVencimiento.getDate())).slice(-2);

    if (formatedDate) BasePath += "&FechaVencimiento=" + formatedDate;

    var fechaOperador = $('#FechaOperador').val();
    if (formatedDate) BasePath += "&FechaOperador=" + fechaOperador;

    //Toda la url
    return BasePath;
}

function createTargetsList() {
    var estados = jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow').length;
    if (estados > 0) {
        var url = generateFiltersUrl("/Email/ApplyEmailFilters");
        $("#gridTargets").jqGrid().setGridParam({ url: url }).trigger('reloadGrid');
    } else {
        alert("Debe seleccionar al menos un estado");
    }
}

function sendTestEmails() {
    var estados = jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow').length;
    if (estados > 0) {
        var url = generateFiltersUrl("/Email/SendMassiveEmail");
        url += "&Test=true"
        $.ajax({
            type: 'GET',
            url: url, // we are calling json method
            dataType: 'json',
            beforeSend: function () {
                $("body").addClass("loading");
            },
            success: function (data) {
                $("body").removeClass("loading");
                alert("Proceso de envío completado, " + data);
            },
            error: function (ex) {
                $("body").removeClass("loading");
            }
        });
    } else {
        alert("Debe seleccionar al menos un estado");
    }  
}

function sendMassiveEmails() {
    var res = confirm("¿Está seguro de enviar los emails con los filtros indicados?, si tiene dudas presione el botón 'Realizar envío de prueba'");
    if (res) {
        var estados = jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow').length;
        if (estados > 0) {
            var url = generateFiltersUrl("/Email/SendMassiveEmail");
            url += "&Test=false"
            $.ajax({
                type: 'GET',
                url: url, // we are calling json method
                dataType: 'json',
                beforeSend: function () {
                    $("body").addClass("loading");
                },
                success: function (data) {
                    $("body").removeClass("loading");
                    alert("Proceso de envío completado, " + data);
                },
                error: function (ex) {
                    $("body").removeClass("loading");
                }
            });
        } else {
            alert("Debe seleccionar al menos un estado");
        }
    } 
}

function sendTestEmailFromTemplate() {
    var pclid = $('#Pclid').val();
    var filename = $('#Template').val();
    var estadosRow = JSON.stringify(jQuery('#gridEstado').jqGrid('getGridParam', 'selarrrow'));
    var data = {
        Estados: estadosRow,
        Template: filename,
        Pclid: pclid
    };
    if (pclid && filename) {
        var url = "/Email/SendEmailTemplate";
        $.ajax({
            type: 'GET',
            url: url, // we are calling json method
            dataType: 'json',
            data: data,
            beforeSend: function () {
                $("body").addClass("loading");
            },
            success: function (data) {
                $("body").removeClass("loading");
                if (data === true) {
                    alert("Email enviado");
                } else {
                    alert("Ha ocurrido un error al enviar este mail de prueba");
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
            }
        });
    }
}

function fnBuscarLiquidaciones(e) {
    if (e.checked) {
        var url = "/Email/ListarLiquidacionesEnvioMasivo";
        $.ajax({
            type: 'GET',
            url: url, // we are calling json method
            dataType: 'json',
            beforeSend: function () {
                $("body").addClass("loading");
            },
            success: function (data) {
                $("body").removeClass("loading");
                $("#TipoLiquidacion").html('');
                $.each(data, function (i, liquidacion) {
                    $("#TipoLiquidacion").append($('<option>', {
                        value: liquidacion.Id,
                        text: liquidacion.Nombre
                    }));
                });
                $('.liquidacion-tipo-ctnr').css('display', 'inherit');
            },
            error: function (ex) {
                $("body").removeClass("loading");
            }
        });
    } else {
        $('.liquidacion-tipo-ctnr').css('display', 'none');
    }

}
//End Envío masivo de emails