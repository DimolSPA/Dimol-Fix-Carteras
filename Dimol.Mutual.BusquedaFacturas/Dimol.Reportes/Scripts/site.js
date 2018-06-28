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
        url: "/Cartera/Buscar/?Ctcid="+ $("#Ctcid").val(),
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
            url: "/Cartera/GetDeudor/?Rut=" + $("#Rut").val() ,
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

function PaisSeleccionado(controlOrigen, controlDestino){
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarRegion", // we are calling json method
        dataType: 'json',
        async: false,
        data: { pais: $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            // states contains the JSON formatted list
            // of states passed from the controller
            $.each(states, function (i, state) {
                $("#"+ controlDestino).append('<option value="' + state.Value + '">' +  
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
        url: "/Cartera/ListarCiudad", // we are calling json method
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
        url: "/Cartera/ListarComuna", // we are calling json method
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
    switch($(control).attr("id")){
        case "chkNegocio":
            if($(control).prop('checked'))
            {
                var width = $("#Documentos").jqGrid('getGridParam', 'width');
                $("#Documentos").jqGrid('showCol', "NumeroEspecial");
                $("#Documentos").setGridWidth(width);
            } 
            else
            {
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

function TipoCarteraMasiva()
{
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
    return "<a href='" + cellValue + "' >"+rowdata[3]+"</a>";

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
    var url = "/Cartera/Deudores/?idd="+id;
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
    ActualizaComboContrato("Pclid","TipoCartera", "Contrato");
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
    var postData ={ ids:  s};
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/AprobarCarga", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData ,
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
    var  i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i] ).prop("checked", true);
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
    if (intlargo.length> 0) 
    { 
        crut = Objeto.val();
        largo = crut.length; 
        if ( largo <2 ) 
        { 
            //alert('rut inválido')
            Objeto.focus() 
            return false; 
        } 
        for ( i=0; i <crut.length ; i++ ) 
            if ( crut.charAt(i) != ' ' && crut.charAt(i) != '.' && crut.charAt(i) != '-' ) 
            { 
            tmpstr = tmpstr + crut.charAt(i); 
            } 
        rut = tmpstr; crut=tmpstr; largo = crut.length; 
        if ( largo> 2 ) rut = crut.substring(0, largo - 1); 
        else rut = crut.charAt(0); dv = crut.charAt(largo-1); 
        if ( rut == null || dv == null ) 
            return 0; 
        var dvr = '0'; 
        suma = 0; 
        mul = 2; 
        for (i= rut.length-1 ; i>= 0; i--) 
        { 
            suma = suma + rut.charAt(i) * mul;
            if (mul == 7) mul = 2; else mul++; 
        } 
        res = suma % 11; 
        if (res==1) dvr = 'k'; 
        else if (res==0) dvr = '0'; 
        else 
        { dvi = 11-res; 
        dvr = dvi + ""; 
        } 
        if ( dvr != dv.toLowerCase() ) 
        { 
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
        alert("Debe ingresar un deudor." );
    }
}

function MostrarDocumento(rowid) {
    //$("#dialogContent").html(rowid);
    //$(".ui-dialog").attr("id", "dialog");
    $(".ui-dialog-content").html('<object data="'+rowid+'" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargarel archivo desde este link <a href="'+rowid+'">.</a></p></object>');
    $(".ui-dialog-content").dialog("open");
}

////////  Inicio Reportes






////////  Fin Reportes

//email
function fnActualizarGestores() {
    var newUrl = "/Email/GetGestor/?"
    newUrl += "TipoCartera=" + $("#TipoCartera").val() + "&grupo=" + $("#GrupoCobranza").val()

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
                alert("Correos enviados con éxito, revise el reporte a continuacion..");
            }
        },
        error: function (ex) {
            alert('Error al enviar correos.' + ex);
        }

    });
}


//email

//Predefinidos

function fnGenerarReportePredefinido() {
    if ($("#Pclid").val() != '' || $("#TipoCartera").val() != '' || $("#Reporte").val() != '' || $("#pag").val() != '') {
        var newUrl = "/Reportes/GeneraReporte/?";
        newUrl += "pclid=" + $("#Pclid").val() + "&rep=" + $("#Reporte").val() + "&tipoCartera=" + $("#TipoCartera").val() + "&pag=" + $("#pag").val();
        if ($("#Reporte").val() == '16' && $("#pag").val() == 357) {
            if (!$("#CodigoCarga").val()) {
                return false;
            }
            newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
        }
        if (($("#Reporte").val() == '3' || $("#Reporte").val() == '4') && $("#pag").val() == 358) {
            if (!$("#CodigoCarga").val()) {
                return false;
            }
            newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
        }
        
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            success: function (data) {
                fnMuestraReportePredefinido(data);
            }

        });
    } else {
        alert("Faltan datos para generar el reporte.")
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
            $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
    }
    $('#ppDocto').dialog('open');
}

////Predefinidos cartera
function fnReporteSeleccionado() {
    //$('div[id ^= xdiv]').hide();
    if ($("#pag").val() == 357) {
        $("#xdivTipoCartera").show();
        switch ($("#Reporte").val()) {
            case '6':
            case '19':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").hide();
                break;
            case '16':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").show();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
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
                $("#xdivCodigoCarga").hide();
                break;
            case '3':
            case '4':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").show();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                break;
        }
    }
}

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

