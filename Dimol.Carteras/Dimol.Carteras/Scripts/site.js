var totalMontoAsignado = 0;
var totalSaldoAsignado = 0;
var totalMontoPorAsignar = 0;
var totalSaldoPorAsignar = 0;
var totalMontoRol = 0;
var totalSaldoRol = 0;

//Traducción al español
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
        for (i = 0; i < largo ; i++) {
            // numero o letra que no corresponda a los del rut
            if (!jQuery.Rut.digitoValido(texto.charAt(i))) {
                return false;
            }
        }

        var invertido = "";
        for (i = (largo - 1), j = 0; i >= 0; i--, j++) {
            invertido = invertido + texto.charAt(i);
        }
        var dtexto = "";
        dtexto = dtexto + invertido.charAt(0);
        dtexto = dtexto + '-';
        cnt = 0;

        for (i = 1, j = 2; i < largo; i++, j++) {
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
        for (i = (dtexto.length - 1), j = 0; i >= 0; i--, j++) {
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
        url: "/Cartera/Buscar/?Ctcid="+ $("#Ctcid").val(),
        async: false,
        beforeSend: function () { $("body").addClass("loading"); },
        success: function (data) {
            //alert(JSON.stringify(data));

            $("#Pais").val(data.IdPais);
            PaisSel();
            $("#Region").val(data.IdRegion);
            RegionSel();
            if (data.Quiebra == "S") {
                $("#lblQuiebra").text("DEUDOR EN LIQUIDACIÓN");
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
            ListarTiposImagenesCpbt();
            fnBuscarCpbtDeudor();
            fnBuscarTelefonosDeudor();
            fnBuscarEmailDeudor();
            fnBuscarEmailDeudorProv();
            fnBuscarHistorialDeudor();
            fnBuscarDocClienteDeudor();
            fnBuscarRolDeudor();
            $("#gridDocRol").jqGrid('clearGridData');
            $("#gridEstadosRol").jqGrid('clearGridData');
            CargarImagenesCpbt();
            ListarVisitasTerreno();
            CargarAgregarHistorial();
            //fnListarDocumentosHistorial();
            ActualizarGestiones();
            fnCargarAsociados();
            ActivarBotonMail();
            fnCargarTimbrajeSII();
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
            fnBuscarTercerosDocumentos();
            ListarBienes();
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

function fnBuscarEmailDeudorProv() {
    var newUrl = "/Cartera/GetEmailProv/?"
    newUrl += "Ctcid=" + $("#Ctcid").val() + "&email="
    jQuery("#gridEmailProv").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnBuscarCpbtDeudor() {
    if ($("#Ctcid").val() == "") {
        $("#Ctcid").val($("#CtcidDialog").val());
    }
    //ListarGrupoCpbt();
    var newUrl = "/Cartera/GetCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&SituacionCartera=" + $("#EstadoCpbt").val() 

    jQuery("#Documentos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    ListarCpbt();
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

//Zip
var Promise = window.Promise;
if (!Promise) {
    Promise = JSZip.external.Promise;
}
/**
* Fetch the content and return the associated promise.
* @param {String} url the url of the content to fetch.
* @return {Promise} the promise containing the data.
*/
function urlToPromise(url) {
    return new Promise(function (resolve, reject) {
        JSZipUtils.getBinaryContent(url, function (err, data) {
            if (err) {
                reject(err);
            } else {
                resolve(data);
            }
        });
    });
}

function comprimir() {
   var zip = new JSZip();
   $('#carouselDocumentos').find('li').each(function () {
        var imgUrl = $(this).find('a').find("img").attr("src");
       // find every checked item
        var url = imgUrl //.substr(imgUrl.indexOf('/', 7) + 1);
        var filename = url.replace(/.*\//g, "");

        zip.file(filename, urlToPromise(url), { binary: true });
   });
    // when everything has been downloaded, we can trigger the dl
   zip.generateAsync({ type: "blob" })
   .then(function callback(blob) {
       // see FileSaver.js
       saveAs(blob, $("#Pclid").val() + "-" + $("#Rut").val() + ".zip");
   });
}
// en zip
// visita terreno
function ListarVisitasTerreno() {
    if ($("#Ctcid").val() != "") {
        var newUrl = "/Cartera/ListarVisitasTerreno/?"
        newUrl += "ctcid=" + $("#Ctcid").val();

        jQuery("#gridVisitaTerreno").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        fnNotifTerrenoColor();
        var newUrl = "/Cartera/ListarVisitasTerrenoGPS/?"
        newUrl += "idVisita=" + 0 + "&idVisitaDetalle=" + 0;
        jQuery("#gridGPS").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        ListarVisitasTerrenoFotos(0, 0);
        ListarVisitaTerrenoTelefonos(0, 0);
    }
}
function fnNotifTerrenoColor() {
    if ($("#Ctcid").val() != "") {
        $("#ColorTerreno").text("");
        $("#ColorTerreno").removeClass("nav-counter");
        $("#ColorTerreno").removeClass("nav-counter nav-counter-green");
        $("#ColorTerreno").removeClass("nav-counter nav-counter-orange");
        var newUrl = "/Cartera/NotificarTerrenoColor/?"
        newUrl += "ctcid=" + $("#Ctcid").val();
        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            async: false,
            success: function (data) {
                //alert(data);
                if (data.length > 0) {
                    var datos = data.split(',');
                    $("#ColorTerreno").text(datos[1]);
                    if (datos[0] =='VERDE') {
                        $("#ColorTerreno").addClass("nav-counter nav-counter-green");
                       
                    }
                    else {
                        if (datos[0] == 'ROJO') {
                            $("#ColorTerreno").addClass("nav-counter");

                        } else {
                            if (datos[0] == 'NARANJA') {
                                $("#ColorTerreno").addClass("nav-counter nav-counter-orange");

                            }
                            
                        }
                    }
                } else {
                    $("#ColorTerreno").text("");
                    $("#ColorTerreno").removeClass("nav-counter");
                    $("#ColorTerreno").removeClass("nav-counter nav-counter-green");
                    $("#ColorTerreno").removeClass("nav-counter nav-counter-orange");
                }
            },
            error: function (ex) {
                alert('Error al recuperar color para pestana Terreno' + ex);
            }

        });
    }
}
function ListarVisitasTerrenoGPS(id) {
    var ids = id.split('|');

    var newUrl = "/Cartera/ListarVisitasTerrenoGPS/?"
    newUrl += "idVisita=" + ids[0] + "&idVisitaDetalle=" + ids[1];
    jQuery("#gridGPS").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    ListarVisitasTerrenoFotos(ids[0], ids[1]);
    ListarVisitaTerrenoTelefonos(ids[0], ids[1]);
}

function ListarVisitasTerrenoFotos(idVisita, idVisitaDetalle) {
    $('#rg-gallery-visita .rg-image-wrapper').hide('slow', function () { $(this).remove(); })
       if ($("#Rut").val() != "") {
        var newUrl = "/Cartera/ListarVisitasTerrenoFotos/?"
        newUrl += "idVisita=" + idVisita + "&idVisitaDetalle=" + idVisitaDetalle;
        $.ajax({
            type: 'POST',
            url: newUrl, 
            dataType: 'json',
            async: false,
            success: function (data) {
                
                if (data != '') {
                    $("#carouselVisitaTerreno").html('<ul style="padding:0px;margin:0px">' + data + '</ul>');
                   
                    $(".rg-view-full").click();
                    $(".rg-view-thumbs").click();
                }
                else {
                    $("#carouselVisitaTerreno").html('<ul style="padding:0px;margin:0px"> <li><a href="#"><img src="" data-large="1" data-description="Sin Imagen" /></a></li></ul>');
                }
                SelectGalleryFotosVisita();
                $('#rg-gallery-visita .rg-view').first().hide('slow', function () { $(this).remove(); })
               
            },
            error: function (ex) {
                alert('Error al recuperar imagenes' + ex);
            }
        });
    }
}

function ListarVisitaTerrenoTelefonos(idVisita, idVisitaDetalle) {
    var newUrl = "/Cartera/ListarVisitaTerrenoTelefonos/?"
    newUrl += "idVisita=" + idVisita + "&idVisitaDetalle=" + idVisitaDetalle;

    jQuery("#gridVisitaDetalleTelefonos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function VerMapaVisitasTerrenoGPS(cellvalue, options, rowdata) {
    if (cellvalue != '') {
        return "<div align='center'><button style=\"vertical-align:middle\" onclick=\" fnMapaVisitasTerrenoGPS('" + rowdata[0] + "','" + rowdata[1] + "','" + rowdata[4] + "');\"  class=\"search\"><img width=\"20px\" height=\"20px\" alt=\"Documento\" title=\"Ver\" src=\"/Images/botones/Search.png\"></button></div>";
    } else {
        return "";
    }
}
var map;
var marker;
function fnMapaVisitasTerrenoGPS(lat, lng, comuna) {
   
    if ((lat != "") && (lng != "")) {
       //showMarker({
        //    gestor: "Marker",
        //    direccion: "New York, NY",
        //    municipio: "",
        //    capital: "",
        //    ciudad: "New York",
        //    provincia: "NY",
        //});
        var mapOptions = {
            center: new google.maps.LatLng(lat, lng),
            zoom: 18
        };
        //build new google maps marker with google maps api
        map = new google.maps.Map(document.getElementById('ppGoogleMaps'), mapOptions);

        var myLatlng = new google.maps.LatLng(lat, lng);
        marker = new google.maps.Marker({
            position: myLatlng,
            map: map,
            title: comuna
        });

        $('#ppGoogleMaps').dialog('open');
        google.maps.event.trigger(map, 'resize');
        map.setCenter(myLatlng);
        marker.setPosition(myLatlng);
        map.setZoom(18);
    }
}
function fnBotonesSolicitudVisita(cellvalue, options, rowdata) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" title="Solicitud de Visita" class="ui-icon ui-icon-home" style="height:20px;width:20px" onclick="fnSolicitarVisita(\'' + rowdata + '\')" >Solicitar</button></div></div></div>';
}
function fnSolicitarVisita(data) {
    if (data.length > 0) {
       var datos = data.split(',');
       var newUrl = "/Cartera/InsertarVisitaTerrenoSolicitud/?"
       newUrl += "ctcid=" + datos[0] + "&direccion=" + datos[2] + "&idRegion=" + datos[5] + "&idCiudad=" + datos[6] + "&idComuna=" + datos[7] + "&comuna=" + datos[3]
       $.ajax({
           type: 'POST',
           url: newUrl, 
           dataType: 'json',
           async: true,
           success: function (data) {

               if (data > 0) {
                   alert('La solicitud ha sido creada con exito');
                   InsertarVisitaTerrenoSolicitudCoordenadas(datos[2], datos[3], data);
               }
               if (data == -3) {
                   alert('Ya existe una solicitud de visita terreno a ésta dirección');
               }
               if (data == -1) {
                   alert('Ha ocurrido un error al Insertar la visita');
               }
               if (data == -2) {
                   alert('Verifique los documentos del deudor');
               }
           },
           error: function (ex) {
               alert('Error al crear la solicitud.' + ex);
           }
       });
      
        ListarVisitasTerreno();
        fnBuscarHistorialDeudor();
    }
}

function InsertarVisitaTerrenoSolicitudCoordenadas(address, comuna, solicitudId) {
    var geocoder = new google.maps.Geocoder();
    var latitud; var longitud;
    var newAddress;
          
    if (geocoder) {
       
       geocoder.geocode({
           'address': address + ", " + comuna + ", " + "Chile"
       }, function (results, status) {
           console.log("Direccion con calle: " + status);
            if (status == google.maps.GeocoderStatus.OK) {
                latitud = results[0].geometry.location.lat();
                longitud = results[0].geometry.location.lng();
                newAddress = results[0].formatted_address;
                
                var newUrl = "/Cartera/InsertarVisitaTerrenoSolicitudCoordenadas/?"
                newUrl += "coordenadas=" + latitud + "|" + longitud + "&respuesta=" + "true" + "&SolicitudId=" + solicitudId
                $.ajax({
                    type: 'POST',
                    url: newUrl, // we are calling json method
                    dataType: 'json',
                    async: true,
                    success: function (data) {
                        
                    },
                    error: function (ex) {
                        alert('Error al crear las coordenadas.' + ex);
                    }
                });
            }
            else {
                geocoder.geocode({
                    'address': comuna //address.split(', ')[1]
                }, function (locResult, status2) {
                    console.log("Direccion con comuna: " + status2);
                    if (status2 == google.maps.GeocoderStatus.OK) {
                        var lat1 = locResult[0].geometry.location.lat();
                        var lng1 = locResult[0].geometry.location.lng();
                        var newUrl = "/Cartera/InsertarVisitaTerrenoSolicitudCoordenadas/?"
                        newUrl += "coordenadas=" + lat1 + "|" + lng1 + "&respuesta=" + "true" + "&SolicitudId=" + solicitudId
                        $.ajax({
                            type: 'POST',
                            url: newUrl, // we are calling json method
                            dataType: 'json',
                            async: true,
                            success: function (data) {

                            },
                            error: function (ex) {
                                alert('Error al crear las coordenadas.' + ex);
                            }
                        });
                    }
                    else {
                        var newUrl = "/Cartera/InsertarVisitaTerrenoSolicitudCoordenadas/?"
                        newUrl += "coordenadas=" + trans.GeocodingError + status2 + "&respuesta=" + "false" + "&SolicitudId=" + solicitudId
                        $.ajax({
                            type: 'POST',
                            url: newUrl, // we are calling json method
                            dataType: 'json',
                            async: true,
                            success: function (data) {

                            },
                            error: function (ex) {
                                alert('Error al crear la solicitud.' + ex);
                            }
                        });
                    }
                    
                });
                
            }
        });
    }
}

var montoVisitaOfSelectedRows = [];
function fnRefrescar() {
    montoVisitaOfSelectedRows = [];
    idsOfSelectedRows = [];
    $('#MontoVisita').val('');
}
function fnBuscarDeudoresVisitaTerrenoAprobar() {
    var newUrl = "/Cartera/ListarVisitaTerrenoSolicitudAprobar/?"
    newUrl += "Pclid=" + $("#Pclid").val()
    newUrl += "&Pais=" + $("#Pais").val() + "&Region=" + $("#Region").val() + "&Ciudad=" + $("#Ciudad").val() + "&Comuna=" + $("#Comuna").val()
    newUrl += "&Monto=" + $("#Monto").val() + "&Quiebra=" + $("#enQuiebra").prop('checked') + "&PreQuiebra=" + $("#enPreQuiebra").prop('checked') + "&Solicitud=" + $("#enSolicitud").prop('checked')
         
    jQuery("#gridAprobarVisitas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    fnRefrescar();
}
function UpdateMontoVisitaOfSelectedRows(id, isSelected) {
    var montoVisitaTotalOfSelectedRows = [];
    var index = $.inArray(id, idsOfSelectedRows);
    if (!isSelected && index >= 0) {

        idsOfSelectedRows.splice(index, 1);
        montoVisitaOfSelectedRows.splice(index, 1); // remove id from the list
        var $check = $('#gridAprobarVisitas tr[id^="' + id + '"]').find('input:checkbox:first');
        $check.prop("checked", false);
    } else if (index < 0) {
        idsOfSelectedRows.push(id);
        var $check = $('#gridAprobarVisitas tr[id^="' + id + '"]').find('input:checkbox:first');
        $check.prop("checked", true);
        var montoCliente = {
            deudaSelected: parseFloat($('#gridAprobarVisitas').jqGrid('getCell', id, 'deuda')),
            pclidSelected: id.split('|')[0],
            ctcidSelected: id.split('|')[1]
        };

        montoVisitaOfSelectedRows.push(montoCliente);
    }
    //quitar duplicados
    var result = montoVisitaOfSelectedRows.reduce(function (memo, e1) {
        var matches = memo.filter(function (e2) {
            return e1.pclidSelected == e2.pclidSelected && e1.ctcidSelected == e2.ctcidSelected
        })
        if (matches.length == 0) {
            memo.push(e1);
        }
        return memo;
    }, [])
    //console.log(result);

    $.each(result, function (index, value) {
        //alert("pclid: " + value.pclidSelected + " and deuda: " + value.deudaSelected + " and ctcid: " + value.ctcidSelected);
        montoVisitaTotalOfSelectedRows.push(value.deudaSelected);

    });

    $('#MontoVisita').val(formatThousands(sum(montoVisitaTotalOfSelectedRows), 2));

}

function fnGuardarAprobarVisitaTerreno() {
    var s;
    s = idsOfSelectedRows;
    var optionSelected = $("#frmGestorSolicitudVisita select[id=lstGestoresTerreno]").find("option:selected");
    var postData = { ids: s, gestorId: optionSelected.val(), gestorNombre: optionSelected.text() };
    if ($('#frmGestorSolicitudVisita').valid()) {
        if (s.length > 0) {
            $.ajax({
                type: 'POST',
                url: "/Cartera/GrabarAceptarVisitas", // we are calling json method
                dataType: 'json',
                traditional: true,
                async: true,
                data: postData,
                beforeSend: function () { $("body").addClass("loading"); },
                success: function (data) {
                    $("body").removeClass("loading");
                    if (data != "" && data != null) {
                        $.each(data, function (index, value) {
                            //alert("direccion: " + value.direccion + " and comuna: " + value.comuna + " and solicitud: " + value.solicitudId);
                            if (value.direccion != "" && value.direccion != null) {
                                InsertarVisitaTerrenoSolicitudCoordenadas(value.direccion, value.comuna, value.solicitudId);
                            }
                            
                        });
                    }
                },
                error: function (jqXHR, textStatus, ex) {
                    $("body").removeClass("loading");
                    //alert(JSON.stringify(jqXHR));
                    //alert(textStatus);
                    alert('Error al aprobar las visitas.' + ex);
                }

            });
            $('#ppGestorSolicitudVisita').dialog('close');
            fnCargarAceptarVisitaTerreno();
            fnRefrescar();
        }
    }
    
}
function fnGuardarRechazarVisitaTerreno() {
    var s;
    s = idsOfSelectedRows;
    var postData = { ids: s };
    if (s.length > 0) {
        $.ajax({
            type: 'POST',
            url: "/Cartera/GrabarRechazarVisitas", // we are calling json method
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
                alert('Error al rechazar las visitas.' + ex);
            }

        });
        fnCargarAceptarVisitaTerreno();
        fnRefrescar();
    }
}
function fnCargarAceptarVisitaTerreno() {
    jQuery("#gridAprobarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
}
function fnGenerarVisitaTerreno() {
    var s;
    s = idsOfSelectedRows;
    var optionSelected = $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").find("option:selected");
    var postData = { ids: s, gestorId: optionSelected.val(), gestorNombre: optionSelected.text() };
    if ($('#frmBuscarVisitaTerrenoGenerar').valid()) {
        if (s.length > 0) {
            $.ajax({
                type: 'POST',
                url: "/Cartera/GenerarVisitasTerreno", // we are calling json method
                dataType: 'json',
                traditional: true,
                async: false,
                data: postData,
                //beforeSend: function () { $("body").addClass("loading"); },
                success: function (json) {
                    //$("body").removeClass("loading");
                    if (json.messageError != "" && json.messageError != null) {
                    
                        alert("Verifique la latitud y longitud de alguna de las direcciones que intenta generar");
                    }
                    if (json.data != "" && json.data != null && json.data.jcsv != null && json.data.jcsv != "") {
                        //download csv
                        //window.location.href = json.data.filepath + json.data.filename;
                         window.location = '/Cartera/Download?filePath=' + json.data.filepath  + json.data.filename +'&file=' + json.data.filename;
                       
                        //var csv = JSON.parse(json.data.jcsv.replace(/"([\w]+)":/g, function ($0, $1) { return ('"' + $1.toLowerCase() + '":'); }));
                        //downloadFile(json.data.filename, encodeURIComponent(csv));
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //alert(jqXHR.status);
                    //alert(textStatus);
                    //alert(errorThrown);
                    alert('Error al rechazar las visitas.' + errorThrown);
                    //$("body").removeClass("loading");
                }

            });
                        
            idsOfSelectedRows = [];
            jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
        }
    }
    
}
function showMarker(data) {
    //1 - getting lat and long of vendor
    var vendorLatLng = null;
    var geocoder = new google.maps.Geocoder();

    geocoder.geocode({
        'address': data['direccion'] + " " + data['municipio'] + ", " + data['capital'] + " " + data['ciudad'] + " " + data['provincia'] + ", Italia"
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            // not required, results[0].geometry.location is a google.maps.LatLng
            vendorLatLng = {
                lat: results[0].geometry.location.lat(),
                lng: results[0].geometry.location.lng()
            };
            //build new google maps marker with google maps api
            var map = new google.maps.Map(document.getElementById('ppGoogleMaps'), {
                zoom: 4,
                center: results[0].geometry.location
            });

            var marker = new google.maps.Marker({
                position: results[0].geometry.location,
                map: map,
                title: data['gestor']
            });
        } else alert("Geocode failed, status: " + status);
    });
   
}

function MostrarColumnaTipoDireccion() {
    if ($("#Pclid").val() != "") {
        if ($("#Pclid").val() != 522) {
            var width = $("#gridDireccion").jqGrid('getGridParam', 'width');
            $("#gridDireccion").jqGrid('hideCol', "TipoDireccion");
            $("#gridDireccion").setGridWidth(width);
        }
        else {
            var width = $("#gridDireccion").jqGrid('getGridParam', 'width');
            $("#gridDireccion").jqGrid('showCol', "TipoDireccion");
            $("#gridDireccion").setGridWidth(width);
        }
    }
}
//end visita terreno

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
        var index = 2;
        if ($("#Pclid").val() == 86 && $("#FlagCliente").val()) {
            index = 1;
        }
        $("#notCausas").text(count);
        $('#tabDetallesDeudor').tabs('enable', index);
        $("#notCausas").addClass("nav-counter");
    } else {
        $("#notCausas").text("");
        $('#tabDetallesDeudor').tabs('disable', index);
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

function PaisSeleccionado(controlOrigen, controlDestino){
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
function ListarCpbt() {
    $("#ListaCpbt").empty();
    var newUrl = "/Cartera/ListarCpbt/?"
    //newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() 
    newUrl += "Ctcid=" + $("#Ctcid").val() + "&Pclid=" + $("#Pclid").val() + "&EstadoCpbt=" + $("#EstadoCpbt").val()
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (documentos) {
            $.each(documentos, function (i, documento) {
                $("#ListaCpbt").append('<option value="' + documento.Value + '">' +
                        documento.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los comprobantes.' + ex);
        }

    });
}

function ListarTiposImagenesCpbt() {
    $("#ListaTipoImagenesCpbt").empty();
    var newUrl = "/Cartera/ListarTiposImagenesCpbt"
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: true,
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (TiposImagenesCpbt) {
            $.each(TiposImagenesCpbt, function (i, TipoImageneCpbt) {
                $("#ListaTipoImagenesCpbt").append('<option value="' + TipoImageneCpbt.Value + '">' +
                        TipoImageneCpbt.Text + '</option>');
                // here we are adding option for States
            });
        },
        error: function (ex) {
            alert('Error al recuperar los comprobantes.' + ex);
        }

    });
}
function MostrarColumnaEstadoJudicial() {
    if ($('#Tipo').val() != "") {
        if ($('#Tipo').val() == "V") {
            var width = $("#Documentos").jqGrid('getGridParam', 'width');
            $("#Documentos").jqGrid('hideCol', "EstadoJudicial");
            $("#Documentos").setGridWidth(width);
        }
        else {
            var width = $("#Documentos").jqGrid('getGridParam', 'width');
            $("#Documentos").jqGrid('showCol', "EstadoJudicial");
            $("#Documentos").setGridWidth(width);
        }
    }

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
    newUrl += "&NombreFantasia=" + $("#NombreFantasia").val() //+ "&Telefono=" + $("#Telefono").val() + "&Email=" + $("#Email").val()
    //newUrl += "&Direccion=" + $("#DireccionBuscar").val() +
    newUrl += "&Sbcid=" + $("#Sbcid").val() + "&Rol=" + $("#Rol").val() + "&TipoDocumento=" + $("#TipoDocumento").val() + "&NumeroCPBT=" + $("#NumeroCPBT").val()
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

    if ($("#TipoCartera").val() != '' && $("#TipoDocumento").val() != '' && $("#Numero").val() != '' && $("#FechaDocumento").val() != '' && $("#FechaVencimiento").val() != '' && $("#EstadoCpbt").val() != '' && $("#EstadoCartera").val() != '' && $("#Moneda").val() != '' && $("#TipoCambio").val() != '' && $("#Contrato").val() != '' && $("#MotivoCobranza").val() != '' && $("#Monto").val() != '' && $("#MontoDocumento").val() != '' && $("#Saldo").val() != '') {
        if ($('#frmDeudorCpbt').valid()) {
            $.ajax({
                type: 'POST',
                url: newUrl,
                dataType: 'json',
                async: true,
                data: postData,
                success: function (data) {
                    if (data != -1) {
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
    else {
        alert("Debe ingresar todos los valores obligatorios");
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
function VerImagenesCpbt() {
    var newUrl = "/Cartera/CargarImagenesCpbt/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#tabDetallesDeudor').tabs('select', 2);
            if (data != '') {
                //$("#tabDetallesDeudor").tabs({ disabled: [6] });
                $(".es-carousel").html('<ul style="padding:0px;margin:0px">' + data + '</ul>');
                $(".rg-view-full").click();
                $(".rg-view-thumbs").click();
            } else {
                //$("#tabDetallesDeudor").tabs({ disabled: [6] });
            }

        },
        error: function (ex) {
            alert('Error al recuperar totales moneda.' + ex);
        }

    });

}
function GrabarImagenesCpbt() {
    var newUrl = "/Cartera/GrabarImagenesCpbt/?"
    newUrl += "pclid=" + $("#Pclid").val() + "&ctcid=" + $("#Ctcid").val() + "&ccbid=" + $("#ListaCpbt").val() + "&tpcid=" + $("#ListaTipoImagenesCpbt").val() + "&rutaImagen=" + $('#Archivo').val()
    if ($("#ListaCpbt").val() == "" || $("#ListaTipoImagenesCpbt").val() == ""
        || $("#Archivo").val() == "") {

        alert("Debe ingresar todos los datos mandatorios.");
    }
    else {
        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            async: true,
            success: function (data) {
                VerImagenesCpbt();
                if (data != -1) {
                    alert('Imagen guardada con exito');
                }
               
            },
            error: function (ex) {
                alert('Error al guardar Imagen.' + ex);
            }

        });
    }
  
}

function fnLimpiarCargaImagen() {
    $('#frmCargaImagen').reset();
    $("#imgSubirArchivo").removeClass("ok").removeClass("error");
    $("#btnCargar").attr("disabled", "disabled");
    $("#btnSubmit").removeAttr("disabled");
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
            success: function (resultData) {
                jQuery("#grdCargaMasiva").jqGrid("clearGridData");

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data.length > 0) {
                        for (var i = 0; i <= resultData.data.length; i++)
                            $("#grdCargaMasiva").jqGrid('addRowData', i + 1, resultData.data[i]);
                        alert('Archivo cargado con errores');
                    } else {
                        alert('Archivo cargado con exito');
                    }
                } else {
                    $("body").removeClass("loading");
                    alert('Error al cargar el archivo. ' + 'Ha ocurrido un error interno');
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
    
    var rowIds = $('#grdCargaMasiva').jqGrid('getDataIDs');
    
    for (var i = 0, len = rowIds.length; i < len; i++) {
        var currRow = rowIds[i];
        $('#grdCargaMasiva').jqGrid('delRowData', currRow);
    }
}

function OnSelectClienteCargaMasiva() {
    ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    ActualizaComboContrato("Pclid","TipoCartera", "Contrato");
}

function OnSelectTipoCartera() {
    ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    ActualizaComboContrato("Pclid", "TipoCartera", "Contrato");

    MostrarOcultarCamposPrevisionales();
}

function ActualizaComboCodigoCarga(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();

    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarCodigoCarga",
        dataType: 'json',
        async: false,
        data: { codemp: 1, pclid: $("#" + controlOrigen).val() },
        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
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
        url: "/Cartera/ListarContrato",
        dataType: 'json',
        async: false,
        data: { codemp: 1, pclid: $("#" + controlOrigen).val(), tipoCartera: $("#" + controlCartera).val() },
        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
            });
        },
        error: function (ex) {
            alert('Ingrese el cliente y seleccione el codigo de carga.');
        }
    });
}
function MostrarOcultarCamposPrevisionales() {
    var str = $("#NombreRutCliente").val()
    var RutCliente = str.split(" ");

    $.ajax({
        type: 'POST',
        url: "/Cartera/VerificarEsClientePrevisional",
        dataType: 'json',
        async: false,
        data: { codemp: 1, RutCliente: RutCliente[0] },
        success: function (data) {
            if (data.esPrevisional) {
                $("#DatosPrevisionales").show();
            } else {
                $("#DatosPrevisionales").hide();
            }
        },
        error: function (ex) {
            alert('Ingrese el cliente y seleccione el codigo de carga.');
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
            url: "/Cartera/AprobarCarga",
            dataType: 'json',
            traditional: true,
            async: true,
            data: postData ,
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
            url: newUrl,
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

/*   Carga Panel Coopeuch   */

function fnCargarPanelDemandasMasivas() {
    var newUrl = "/Cartera/ProcesoCargaPanelDemandasMasivas/"
    var postData = $("#frmCargaPanelCoopeuch").serializeArray();
    if ($('#frmCargaPanelCoopeuch').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (resultData) {

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data.length > 0) {
                        for (var i = 0; i <= resultData.data.length; i++)
                            $("#grdCargaPanelCoopeuch").jqGrid('addRowData', i + 1, resultData.data[i]);
                        alert('Archivo cargado con errores');
                    } else {
                        alert('Archivo cargado con exito');
                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al cargar el archivo. ' + 'Ha ocurrido un error interno');
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al cargar el archivo.' + ex);
            }

        });
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
    newUrl += "&Tipo=" + $("#Tipo").val() + "&Cartera=" + $("#Cartera").val() + "&Ctcid=" + $("#Ctcid").val() + "&Tribunal=" + $("#Tribunal").val() + "&Rol=" + $("#Rol").val()
    newUrl += "&FechaDesdeEmision=" + $("#FechaEmisionDesde").val() + "&FechaHastaEmision=" + $("#FechaEmisionHasta").val()
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

//Luego ver
//function test() {
//    loadComplete: function(){   
//        var i, count, $grid = $("#list");
//        var rowArray = $("#list").jqGrid('getDataIDs');
//        for (i = 0, count = rowArray.length; i < count; i += 1) {
//            $grid.jqGrid('setSelection', rowArray[i], true);
//        }
//    }

//    $("#accountGrid").jqGrid({
//        url: g_account_url,
//        datatype: "json",
//        caption: "AccountList",
//        colNames:[ "account_id", "Label", "Currency", "Balance" ],
//        colModel:[
//            {name:"account_id",index:"account_id", width:1, hidden:true, key:true},
//            {name:"account_label",index:"account_label", width:180},
//            {name:"currency_code",index:"currency_code", width:180},
//            {name:"balance_amount",index:"balance_amount", width:180},
//        ],
//        height: "150px",
//        autowidth: true,
//        onSelectRow: f_select_account,
//        loadComplete: function(){
//            $("#accountGrid").setSelection($("#accountGrid").getDataIDs()[0],true);
//        },
//        gridComplete: function(){
//            $("#accountGrid").setSelection($("#accountGrid").getDataIDs()[0],true);
//        }
//    });

//    var myGrid = $('#list'),
//    selRowId = myGrid.jqGrid ('getGridParam', 'selrow'),
//    celValue = myGrid.jqGrid ('getCell', selRowId, 'columnName');

//    myGrid.jqGrid('setSelection', rowid);

//    //creo que esta esta
//    var i, count, $grid = $("#myTable");
//    for (i = 0, count = rowArray.length; i < count; i += 1) {
//        $grid.jqGrid('setSelection', rowArray[i], false);
//    }
//}

//
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
    var  i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i] ).prop("checked", true);
    }
}
//Torta Dinamica
function CargarDeudorRutCliente() {
    if ($("#Rut").val() != "") {
        $.ajax({
            type: 'POST',
            url: "/Cartera/GetDeudorCli/?Rut=" + $("#Rut").val() + "&Cli=" + $("#Cli").val(),
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

function fnGenerarTortaDinamica() {
    var newUrl = "/Cartera/TortaAgrupada?Pclid=" + $("#frmTortaDinamica #Pclid").val() +
                    "&tipcart=" + $("#frmTortaDinamica #TipoCartera").val() +
                    "&estcpbt=" + $("#frmTortaDinamica #EstadoDocumento").val() +
                    "&codid=" + $("#frmTortaDinamica #CodigoCarga").val() +
                    "&gesid=" + ($("#frmTortaDinamica #Gestor").val() == '' ? 0 : $("#frmTortaDinamica #Gestor").val());

    var ValorDocsVencidos = $('#frmTortaDinamica input[name=DocsVencidos]:checked').val();
    if (ValorDocsVencidos != null && ValorDocsVencidos != "-1") {
        newUrl = newUrl + "&docsvencidos=" + ValorDocsVencidos;
    }

    $.ajax({
        type: 'POST',
        url: newUrl,
        datatype: 'json',
        async: false,
        success: function (data) {
            $('#frmTortaDinamica #idTortaDinamica').remove();
            $('#frmTortaDinamica').append('<div class="tabla" id ="idTortaDinamica"></div>');

            $.each(data.rows, function (i, array) {
                $('#frmTortaDinamica #idTortaDinamica').append('<div id="idPanel' + i + '" class="panel panel-primary"><div class="panel-heading"><div class="fila"><b>GRUPO DE ESTADOS : ' + array.Agrupa + '</b></div></div></div>');
                $('#frmTortaDinamica #idTortaDinamica #idPanel' + i).append('<div class="panel-body"><table id ="idTorta' + i + '" style="border-radius:10px;-webkit-border-radius:10px;"></table></div>');
                $('#frmTortaDinamica #idTortaDinamica #idPanel' + i + ' table#idTorta' + i).append('<tr style="background:#e6e6e6;font-weight:bold;"><td>ESTADO</td><td style="text-align:center;padding-left:4px;padding-right:4px;">Nº DEUDORES</td><td style="text-align:center;padding-left:4px;padding-right:4px;">Nº DOCUMENTOS</td><td style="text-align:center;padding-left:4px;padding-right:4px;">SALDO</td><td style="text-align:center;padding-left:4px;padding-right:4px;">% SALDO/TOTAL</td></tr>');

                $.each(array.Est, function (j, datos) {
                    $('#frmTortaDinamica #idTortaDinamica #idPanel' + i + ' table#idTorta' + i).append('<tr><td style="cursor:pointer;" onclick="fnHideShow($(\'#idUl' + i + j + '\'))">' + datos.Estado + '</td><td style="text-align:center;padding-left:4px;padding-right:4px;">' + datos.Deudores + '</td><td style="text-align:center;padding-left:4px;padding-right:4px;">' + datos.Documentos + '</td><td style="text-align:center;padding-left:4px;padding-right:4px;">' + formatThousands(datos.Saldo, 2) + '</td><td style="background-color:#fbf9ee;-moz-border-radius:5px;-webkit-border-radius:5px;border-radius:5px;border:1px solid grey;padding:0px;text-align:center;"><div style="width:' + datos.Regularizado.toFixed(2) + '%;background-color:#64B5F6;-moz-border-radius:4px;-webkit-border-radius:4px;border-radius:4px;font-weight:bold;font-size:0.875em;text-align:center;"><span style="margin-left:40px;">' + datos.Regularizado.toFixed(2) + '%</span></div></td></tr>');
                    $('#frmTortaDinamica #idTortaDinamica #idPanel' + i + ' table#idTorta' + i).append('<tr><td style="border: white;" colspan="4"><table id="idUl' + i + j + '" style="display:none;margin-left:30px;font-family:Verdana,Arial,sans-serif/*{ffDefault}*/;font-size:0.85em/*{fsDefault}*/;margin-bottom:10px;"></table></td></tr>');

                    $.each(datos.Deu, function (k, deudores) {
                        $('#frmTortaDinamica #idTortaDinamica #idPanel' + i + ' table#idTorta' + i + ' table#idUl' + i + j).append('<tr><td style="text-align:center;border:1px solid #ddd;">' + (k + 1) + '</td><td nowrap style="padding:0px;border:1px solid #ddd;"><a style="text-decoration:none;" href="/Cartera/Index?tipo=V&pag=355&r=' + deudores.Rut + '&cli=' + $("#frmTortaDinamica #Pclid").val() + '" target="_blank" onclick="fnLinkTorta($(this))">' + deudores.Rut + ' - ' + deudores.Nombre + '</a></td><td style="padding:0px;border:1px solid #ddd;">' + ((deudores.Acciones > 0 || deudores.Historial > 0) ? '<img alt="Signo" src="/Images/green_check.png" height="20px" width="20px" style="margin-bottom:-6px">' : '') + '</td><td style="padding:0px;border:1px solid #ddd;text-align:center;"><span class="badge badge-info">' + formatThousands(deudores.Saldo, 2) + '</span></td></tr>');
                    });
                });             
            });

            $('#frmTortaDinamica #idTortaDinamica').append('<div class="panel panel-primary"><div class="panel-heading"><b>RESUMEN</b></div><div class="panel-body"><ul class="list-group"><li class="list-group-item"><label>TOTAL DEUDORES</label><span class="badge badge-info">' + data.deudores + '</span></li><li class="list-group-item"><label>TOTAL DOCUMENTOS</label><span class="badge badge-info">' + data.documentos + '</span></li><li class="list-group-item"><label>TOTAL CARTERA</label><span class="badge badge-info">' + formatThousands(data.saldo, 2) + '</span></li></ul></div></div>');

        },
        error: function (ex) {
            alert('Error al generar la información.' + ex);
        }
    });
}

function fnLinkTorta(obj) {
    
    if ($('#EstadoDocumento').val() == 'J') {
        obj.prop('href', obj.prop('href').replace('tipo=V', 'tipo=J'));        
    }
    else {
        obj.prop('href', obj.prop('href').replace('tipo=J', 'tipo=V'));        
    }
      
}
//Sii

function fnCargarTimbrajeSII() {

    var newUrl = "/Cartera/GetDatosSII/?"
    newUrl += "Ctcid=" + $("#Ctcid").val()

    $.ajax({
        type: 'POST',
        url: newUrl,
        datatype: 'json',
        async: false,
        success: function (data) {

        $('#tblSii #idDataSii').remove();

            if (data.registrado != null){            

                if (data.registrado != "N") {
                
                    $('#tblSii').append('<div id ="idDataSii"></div>');
                    $('#tblSii #idDataSii').append('<table><tr><td width=160px><strong>Nombre o Razón Social: </strong></td><td width=420px>' + data.razonSocial + '</td><td><strong>Fecha consulta: </strong></td><td>' + data.fecConsulta + ' hrs</td></tr><tr><td><strong>RUT Contribuyente: </strong></td><td>' + data.rutContr + '</td></tr></table><p>');
                    $('#tblSii #idDataSii').append('<table><tr><td>Contribuyente presenta Inicio de Actividades: </td><td>' + data.inicioAct + '</td></tr>' + ((data.inicioAct == "SI") ? '<tr><td>Fecha de Inicio de Actividades: </td><td>' + data.fechaInicio + '</td></tr>' : '') + '<tr><td>Contribuyente autorizado para declarar y pagar sus impuestos en moneda extranjera: </td><td>' + data.contrAutoriza + '</td></tr><tr><td>Contribuyente es EMPRESA DE MENOR TAMAÑO PRO-PYME: </td><td>' + data.contrProPyme + '</td></tr></table><p>');
            

                    if (data.rowsAct.length > 0) $('#tblSii #idDataSii').append('<strong>Actividades Económicas vigentes:</strong><p><table id="idAct"><tr><td style="border:1px solid #005F9D;padding-left:5px;"><strong>Actividades</strong></td><td style="border:1px solid #005F9D;padding:5px;text-align:right;"><strong>Código</strong></td><td style="border:1px solid #005F9D;padding-left:5px;"><strong>Categoría</strong></td><td style="border:1px solid #005F9D;padding:5px;text-align:center;"><strong>Afecta IVA</strong></td></tr></table>');

                    $.each(data.rowsAct, function (i, array) {
                        $('#tblSii #idDataSii table#idAct').append('<tr><td style="border:1px solid #005F9D;padding-left:5px;">' + array.Actividades + '</td><td style="border:1px solid #005F9D;padding:5px;text-align:right;">' + array.Codigo + '</td><td style="border:1px solid #005F9D;padding-left:5px;">' + array.Categoria + '</td><td style="border:1px solid #005F9D;padding:5px;text-align:center;">' + array.AfectaIVA + '</td></tr>');
                    });

                    $('#tblSii #idDataSii').append('<p>' + data.comentario + '<p>');
                
                    if (data.rows.length > 0) $('#tblSii #idDataSii').append('<strong>Documentos Timbrados:</strong><p><table id="idDocs"><tr><td style="border:1px solid #005F9D;padding-left:5px;"><strong>Documento</strong></td><td style="border:1px solid #005F9D;padding:5px;text-align:center;"><strong>Año</strong></td></tr></table>');

                    $.each(data.rows, function (i, array) {                
                        $('#tblSii #idDataSii table#idDocs').append('<tr><td style="border:1px solid #005F9D;padding-left:5px;">' + array.Documento + '</td><td style="border:1px solid #005F9D;padding:5px;text-align:center;">' + array.Anio + '</td></tr>');
                    });

                    if (data.observacion != null) $('#tblSii #idDataSii').append('<p><strong>Observación:</strong><span style="margin-left: 40px;text-align:justify">' + data.observacion + '</span>');
                        
                    $('#tblSii table#idAct').prop('style', 'margin: auto');
                    $('#tblSii table#idDocs').prop('style', 'margin: auto');                                           
                
                }
                else {
                    $('#tblSii').append('<div id ="idDataSii">No ha sido posible completar su solicitud. Esto debido a que el Rut <strong>' + data.rutContr + '</strong>, no existe en las Bases de Datos del Servicio.</div>');
                }

                switch (data.color) {
                    case 0:
                        $('#idImgSii').prop('src', '/Images/yellow-ex.png');
                        break;
                    case 1:
                        $('#idImgSii').prop('src', '/Images/green_check.png');
                        break;
                    case 2:
                        $('#idImgSii').prop('src', '/Images/red-x.png');
                        break;
                    default:
                        break;
                }
            }
            else {
                $('#tblSii').append('<div id ="idDataSii">Caso permanece pendiente de actualización de SII. Favor consultar en la siguiente <a href="https://zeus.sii.cl/cvc/stc/stc.html">página</a>.</div>');
            }
        }

    });
}

//Mail Coopeuch
function fnEnviarEmailCoopeuch() {
    var mailsDestino = "";

    $('#frmEmailCoopeuch div#mailsCoopeuch div div').each(function () {
        mailsDestino += $(this).text().substring(0, $(this).text().length - 2) + ",";
    });

    mailsDestino = mailsDestino.substring(0, mailsDestino.length - 1);

        if ($("#frmEmailCoopeuch input[id=PclidCoopeuch]").val() != '' && $("#frmEmailCoopeuch input[id=Ctcid]").val() != '') {
            
            if (mailsDestino.length > 0 && mailsDestino != '') {

                var newUrl = "/Email/EnviarEmailCoopeuch/?"
                                
                newUrl += "Pclid=" + $("#frmEmailCoopeuch input[id=PclidCoopeuch]").val() + "&Ctcid=" + $("#frmEmailCoopeuch input[id=Ctcid]").val() + "&Email=" + mailsDestino

                if ($("#frmEmailCoopeuch input[id=CuotaVencida]").is(":checked")) {
                    newUrl += "&TipoReporte=4"
                }
                else {
                    newUrl += "&TipoReporte=5"
                }
                
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
                        $('#ppEnvioEmailCoopeuch').dialog('close');
                        ResetMailMutual("frmEmailCoopeuch", "mailsCoopeuch");
                        fnBuscarCpbtDeudor();
                        fnBuscarHistorialDeudor();
                    },
                    error: function (ex) {
                        alert('Error al enviar correos.' + ex);
                    }

                });
            }

            else {
                alert("No hay destinatarios para enviar mail");
            }

       
        }
        else {
            alert("Debe ingresar cliente y deudor");
        }

}
//Mail Mutual Pagos
function fnGuardarEjecutivoMutual() {       

    if ($('#frmEjecutivosMutual #idDdEjec').is(":visible")) {
        if ($('#frmEjecutivosMutual #NombreRutCliente').val() != '' && $('#frmEjecutivosMutual #Ejecutivos').val() != '' && $('#frmEjecutivosMutual #Email').val() != '' && $('#frmEjecutivosMutual #Oficina').val() != '') {
            if (validarEmail($('#frmEjecutivosMutual #Email').val().split(",")) == true) {
                                
                $.ajax({
                    type: 'POST',
                    url: '/Cartera/InsertarEjecutivoMutual',
                    dataType: 'json',
                    async: false,
                    data: { pclid: $('#frmEjecutivosMutual #Pclid').val(), ejecutivo: '', email: $('#frmEjecutivosMutual #Email').val(), oficina: $('#frmEjecutivosMutual #Oficina').val(), idEjecutivo: $('#frmEjecutivosMutual #Ejecutivos').val() },
                    success: function (states) {

                        if (states < 1) {
                            alert('No se pudo actualizar los datos del ejecutivo');
                        }
                        else {
                            alert('Los datos del ejecutivo ' + $('#frmEjecutivosMutual #Ejecutivos').find(':selected').text() + ' fueron actualizados exitosamente.');
                            $('#frmEjecutivosMutual #idDdEjec').show();
                            $('#frmEjecutivosMutual #idTxtEjec').hide();
                            $('#frmEjecutivosMutual #idDDCta').show();
                            $('#frmEjecutivosMutual #idTxtCta').hide();
                            $('#frmEjecutivosMutual #Cuenta').prop("disabled", false);
                            $('#frmEjecutivosMutual #btnNuevo').val('Nuevo');
                            $('#frmEjecutivosMutual #btnAdd').show();
                            $('#frmEjecutivosMutual #btnDel').show();
                            $('#frmEjecutivosMutual #btnEliminar').show();
                            $('#frmEjecutivosMutual #NombreEjecutivo').val('');
                            $('#frmEjecutivosMutual #Cuenta').val('');
                            fnTraeEjecutivos('frmEjecutivosMutual #Ejecutivos', $('#frmEjecutivosMutual #Pclid').val());
                            fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                            $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", false);
                            
                        }
                    },
                    error: function (ex) {
                        alert('Error al actualizar los datos del ejecutivo.' + ex);
                    }

                });
                
            }
        }
        else {
            alert('Debe ingresar cliente, nombre, email y oficina del ejecutivo');
        }
    }
    else {
        if ($('#frmEjecutivosMutual #NombreRutCliente').val() != '' && $('#frmEjecutivosMutual #NombreEjecutivo').val() != '' && $('#frmEjecutivosMutual #Email').val() != '' && $('#frmEjecutivosMutual #Oficina').val() != '') {
            if (validarEmail($('#frmEjecutivosMutual #Email').val().split(",")) == true) {
                
                $.ajax({
                    type: 'POST',
                    url: '/Cartera/InsertarEjecutivoMutual',
                    dataType: 'json',
                    async: false,
                    data: { pclid: $('#frmEjecutivosMutual #Pclid').val(), ejecutivo: $('#frmEjecutivosMutual #NombreEjecutivo').val(), email: $('#frmEjecutivosMutual #Email').val(), oficina: $('#frmEjecutivosMutual #Oficina').val(), idEjecutivo: 0 },
                    success: function (states) {

                        if (states < 1) {
                            alert('No se pudo agregar el nuevo ejecutivo');
                        }
                        else {
                            alert('El ejecutivo ' + $('#frmEjecutivosMutual #NombreEjecutivo').val() + ' fue agregado exitosamente.');
                            $('#frmEjecutivosMutual #idDdEjec').show();
                            $('#frmEjecutivosMutual #idTxtEjec').hide();
                            $('#frmEjecutivosMutual #idDDCta').show();
                            $('#frmEjecutivosMutual #idTxtCta').hide();
                            $('#frmEjecutivosMutual #Cuenta').prop("disabled", false);
                            $('#frmEjecutivosMutual #btnNuevo').val('Nuevo');
                            $('#frmEjecutivosMutual #btnAdd').show();
                            $('#frmEjecutivosMutual #btnDel').show();
                            $('#frmEjecutivosMutual #btnEliminar').show();
                            $('#frmEjecutivosMutual #NombreEjecutivo').val('');
                            $('#frmEjecutivosMutual #Cuenta').val('');
                            fnTraeEjecutivos('frmEjecutivosMutual #Ejecutivos', $('#frmEjecutivosMutual #Pclid').val());
                            fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                            $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", false);
                            $('#frmEjecutivosMutual #btnSearch').show();
                        }
                    },
                    error: function (ex) {
                        alert('Error al agregar el ejecutivo.' + ex);
                    }

                });
            }
        }
        else {
            alert('Debe ingresar cliente, nombre, email y oficina del ejecutivo');
        }
    }
        
}

function fnEliminaEjecutivoMutual() {

    if ($('#frmEjecutivosMutual #idDdEjec').is(":visible")){
        if ($("#frmEjecutivosMutual #Ejecutivos").val() != null && $("#frmEjecutivosMutual #Ejecutivos").val() != '') {
            if (confirm("¿Está seguro de eliminar al ejecutivo " + $("#frmEjecutivosMutual #Ejecutivos").find(":selected").text() + "?")) {
                $.ajax({
                    type: 'POST',
                    url: '/Cartera/EliminarEjecutivoMutual',
                    dataType: 'json',
                    async: false,
                    data: { idejecutivo: $('#frmEjecutivosMutual #Ejecutivos').val() },
                    success: function (states) {
                        if (states < 1) {
                            alert('No se pudo eliminar el ejecutivo');
                        }
                        else {
                            alert('El ejecutivo ' + $('#frmEjecutivosMutual #Ejecutivos').find(':selected').text() + ' fue eliminado con éxito.');
                            fnTraeEjecutivos('frmEjecutivosMutual #Ejecutivos', $('#frmEjecutivosMutual #Pclid').val());
                            fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                        }
                    },
                    error: function (ex) {
                        alert('Error al eliminar la cuenta del ejecutivo.' + ex);
                    }

                });
            }
        }
    }
}

function fnEliminaCuentaEjecutivo() {

    if($("#frmEjecutivosMutual #CuentaBanco").val() != null && $("#frmEjecutivosMutual #CuentaBanco").val() != ''){
        if (confirm("¿Está seguro de eliminar la cuenta " + $("#frmEjecutivosMutual #CuentaBanco").find(":selected").text() + " del ejecutivo " + $("#frmEjecutivosMutual #Ejecutivos").find(":selected").text() + "?")) {
            $.ajax({
                type: 'POST',
                url: '/Cartera/EliminarCuentaEjecutivo',
                dataType: 'json',
                async: false,
                data: { cuenta: $('#frmEjecutivosMutual #CuentaBanco').val() },
                success: function (states) {
                    if (states < 1) {
                        alert('No se pudo eliminar la cuenta del ejecutivo');
                    }
                    else {
                        alert('La cuenta ' + $('#frmEjecutivosMutual #CuentaBanco').find(':selected').text() + ' del ejecutivo ' + $('#frmEjecutivosMutual #Ejecutivos').find(':selected').text() + ' fue eliminada con éxito.');
                        fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                    }
                },
                error: function (ex) {
                    alert('Error al eliminar la cuenta del ejecutivo.' + ex);
                }

            });
        }
    }

}

function fnAgregaCuentaEjecutivo() {

    if ($('#frmEjecutivosMutual #Cuenta').val() != '' && $('#frmEjecutivosMutual #Ejecutivos').val() != '' && $('#frmEjecutivosMutual #TipoBanco').val() != '' && $('#frmEjecutivosMutual #Cuenta').val() != null && $('#frmEjecutivosMutual #Ejecutivos').val() != null && $('#frmEjecutivosMutual #TipoBanco').val() != null) {
        $.ajax({
            type: 'POST',
            url: '/Cartera/InsertarCuentaEjecutivo',
            dataType: 'json',
            async: false,
            data: { cuenta: $('#frmEjecutivosMutual #Cuenta').val(), idEjecutivo: $('#frmEjecutivosMutual #Ejecutivos').val(), idBanco: $('#frmEjecutivosMutual #TipoBanco').val() },
            success: function (states) {
                
                if (states < 1) {
                    alert('No se pudo agregar la cuenta del ejecutivo');
                }
                else {
                    alert('La cuenta ' + $('#frmEjecutivosMutual #Cuenta').val() + ' fue asignada exitosamente al ejecutivo ' + $('#frmEjecutivosMutual #Ejecutivos').find(':selected').text());
                    $('#frmEjecutivosMutual #btnDel').show();
                    $('#frmEjecutivosMutual #btnSave').hide();
                    $('#frmEjecutivosMutual #TipoBanco').prop("disabled", true);
                    $('#frmEjecutivosMutual #idDDCta').show();
                    $('#frmEjecutivosMutual #idTxtCta').hide();
                    $('#frmEjecutivosMutual #btnAdd').prop('value', 'Agregar');
                    $('#frmEjecutivosMutual #Ejecutivos').prop("disabled", false);
                    $('#frmEjecutivosMutual #Email').prop("disabled", false);
                    $('#frmEjecutivosMutual #Oficina').prop("disabled", false);
                    $('#frmEjecutivosMutual #btnNuevo').show();
                    $('#frmEjecutivosMutual #btnGuardar').show();
                    $('#frmEjecutivosMutual #btnEliminar').show();
                    $('#frmEjecutivosMutual #Cuenta').val('');
                    $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", false);
                    fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                    $('#frmEjecutivosMutual #btnSearch').show();
                }
            },
            error: function (ex) {
                alert('Error al agregar la cuenta del ejecutivo.' + ex);
            }

        });
    }
    else {
        alert('Debe ingresar todos los datos solicitados: Ejecutivo, Banco y Nro de Cuenta');
    }
}

function fnVisualizaEjecutivos(tipo) {

    if (tipo == 1) {
        if ($('#frmEjecutivosMutual #idDdEjec').is(":visible")) {
            $('#frmEjecutivosMutual #idDdEjec').hide();
            $('#frmEjecutivosMutual #idTxtEjec').show();
            $('#frmEjecutivosMutual #idDDCta').hide();
            $('#frmEjecutivosMutual #idTxtCta').show();
            $('#frmEjecutivosMutual #Cuenta').prop("disabled", true);
            $('#frmEjecutivosMutual #btnSearch').hide();
            $('#frmEjecutivosMutual #btnNuevo').val('Descartar');
            $('#frmEjecutivosMutual #btnAdd').hide();
            $('#frmEjecutivosMutual #btnDel').hide();
            $('#frmEjecutivosMutual #btnEliminar').hide();
            $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", true);
            $('#frmEjecutivosMutual #Email').val('');
            $('#frmEjecutivosMutual #Oficina').val('');

        }
        else {
            if (confirm("¿Está seguro?") == true) {
                $('#frmEjecutivosMutual #idDdEjec').show();
                $('#frmEjecutivosMutual #idTxtEjec').hide();
                $('#frmEjecutivosMutual #idDDCta').show();
                $('#frmEjecutivosMutual #idTxtCta').hide();
                $('#frmEjecutivosMutual #Cuenta').prop("disabled", false);
                $('#frmEjecutivosMutual #btnSearch').show();
                $('#frmEjecutivosMutual #btnNuevo').val('Nuevo');
                $('#frmEjecutivosMutual #btnAdd').show();
                $('#frmEjecutivosMutual #btnDel').show();
                $('#frmEjecutivosMutual #btnEliminar').show();
                $('#frmEjecutivosMutual #NombreEjecutivo').val('');
                $('#frmEjecutivosMutual #Cuenta').val('');
                fnTraeEjecutivosCuentas(1, $('#frmEjecutivosMutual #Pclid').val());
                $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", false);

            }
        }
    }
    else if (tipo == 2) {
        if ($('#frmEjecutivosMutual #TipoBanco').prop("disabled")) {
            $('#frmEjecutivosMutual #btnSearch').hide();
            $('#frmEjecutivosMutual #btnDel').hide();
            $('#frmEjecutivosMutual #btnSave').show();
            $('#frmEjecutivosMutual #TipoBanco').prop("disabled", false);
            $('#frmEjecutivosMutual #idDDCta').hide();
            $('#frmEjecutivosMutual #idTxtCta').show();
            $('#frmEjecutivosMutual #btnAdd').prop('value', 'Descartar');
            $('#frmEjecutivosMutual #Ejecutivos').prop("disabled", true);
            $('#frmEjecutivosMutual #Email').prop("disabled", true);
            $('#frmEjecutivosMutual #Oficina').prop("disabled", true);
            $('#frmEjecutivosMutual #btnNuevo').hide();
            $('#frmEjecutivosMutual #btnGuardar').hide();
            $('#frmEjecutivosMutual #btnEliminar').hide();
            $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", true);
            fnListarBancos("frmEjecutivosMutual #TipoBanco");
        }
        else {
            if (confirm("¿Está seguro?") == true) {
                $('#frmEjecutivosMutual #btnSearch').show();
                $('#frmEjecutivosMutual #btnDel').show();
                $('#frmEjecutivosMutual #btnSave').hide();
                $('#frmEjecutivosMutual #TipoBanco').prop("disabled", true);
                $('#frmEjecutivosMutual #idDDCta').show();
                $('#frmEjecutivosMutual #idTxtCta').hide();
                $('#frmEjecutivosMutual #btnAdd').prop('value', 'Agregar');
                $('#frmEjecutivosMutual #Ejecutivos').prop("disabled", false);
                $('#frmEjecutivosMutual #Email').prop("disabled", false);
                $('#frmEjecutivosMutual #Oficina').prop("disabled", false);
                $('#frmEjecutivosMutual #btnNuevo').show();
                $('#frmEjecutivosMutual #btnGuardar').show();
                $('#frmEjecutivosMutual #btnEliminar').show();
                $('#frmEjecutivosMutual #NombreRutCliente').prop("disabled", false);
                $('#frmEjecutivosMutual #Cuenta').val('');
                fnTraeEjecutivosCuentas(2, $('#frmEjecutivosMutual #Pclid').val());
                fnListarTipoBancos("frmEjecutivosMutual #TipoBanco", $('#Pclid').val());
            }
        }
    }

}

function fnTraeEjecutivos(controlDestino, pclid) {

    $("#" + controlDestino).empty();

    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarEjecutivosMutual",
        dataType: 'json',
        async: false,
        data: { pclid: pclid == "" ? 0 : pclid },

        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' + state.Text + '</option>');

            });
        },
        error: function (ex) {
            alert('Error al cargar los ejecutivos.' + ex);
        }

    });
}

function fnTraeEjecutivosCuentas(tipo, pclid) {       
    
    var newUrl = "/Cartera/ListarEjecutivoMutual/?pclid=" + (pclid == "" || pclid == null ? 0 : pclid);

    if (tipo == 1) {
        newUrl += "&ejecutivo=" + ($("#frmEjecutivosMutual #Ejecutivos").val() == "" || $("#frmEjecutivosMutual #Ejecutivos").val() == null ? 0 : $("#frmEjecutivosMutual #Ejecutivos").val()) + "&cuenta=0";
    }
    else if(tipo==2){
        newUrl += "&ejecutivo=0&cuenta=" + ($("#frmEjecutivosMutual #CuentaBanco").val() == "" || $("#frmEjecutivosMutual #CuentaBanco").val() == null ? 0 : $("#frmEjecutivosMutual #CuentaBanco").val());
    }
    
    $.ajax({
        type: 'POST',
        url: newUrl,
        dataType: 'json',
        async: false,
        success: function (states) {

            if (tipo == 1) {
                //$("#frmEjecutivosMutual #TipoBanco").empty();
                $("#frmEjecutivosMutual #CuentaBanco").empty();
                $("#frmEjecutivosMutual #Email").val('');
                $("#frmEjecutivosMutual #Oficina").val('');

            }

            $.each(states.rows, function (i, state) {
                
                //$('#frmEjecutivosMutual #TipoBanco > option[value="' + state.IdTipoBanco + '"]').attr('selected', 'selected');

                if (tipo == 1) {                    

                    //$("#frmEjecutivosMutual #TipoBanco").append('<option value="' + state.IdTipoBanco + '">' + state.NombreBanco + '</option>');
                    if(state.IdCuentaEjecutivo != 0) $("#frmEjecutivosMutual #CuentaBanco").append('<option value="' + state.IdCuentaEjecutivo + '">' + state.Cuenta + '</option>');
                    $("#frmEjecutivosMutual #Email").val(state.Email);
                    $("#frmEjecutivosMutual #Oficina").val(state.Oficina);
                }

                //else if (tipo == 2) {                  
                    
                    $('#frmEjecutivosMutual #TipoBanco > option[value="' + state.IdTipoBanco + '"]').attr('selected', 'selected');
                    $('#frmEjecutivosMutual #CuentaBanco > option[value="' + state.IdCuentaEjecutivo + '"]').attr('selected', 'selected');
                //}                

            });
        },
        error: function (ex) {
            alert('Error al cargar los datos de los ejecutivos.' + ex);
        }

    });

}

function fnMuestraRetiroCheques() {
    if ($("#frmEmailMutualPagos #Gestion").val() == "51") {
        $("#frmEmailMutualPagos #divComentarioRetiro").show();
        $("#frmEmailMutualPagos #divBancos").hide();
        $("#frmEmailMutualPagos label[for='FechaMail']").text("Fecha Retiro");
    }
    else {
        $("#frmEmailMutualPagos #divComentarioRetiro").hide();
        $("#frmEmailMutualPagos #divBancos").show();
        $("#frmEmailMutualPagos label[for='FechaMail']").text("Fecha");
    }
}

function fnBuscarDeudoresCpbtMutualPagos() {

    ResetMailMutualPagos();

    if ($("#Pclid").val() != '') {

        var newUrl = "/Cartera/GetDeudoresMailMutualPagosCpbt/?"
        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val()

        jQuery("#gridDeudorCpbtMutualPagos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnListarBancos(controlDestino) {

    $("#" + controlDestino).empty();

    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarBancos",
        dataType: 'json',
        async: false,        

        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' + state.Text + '</option>');
            });
        },
        error: function (ex) {
            alert('Error al cargar los bancos.' + ex);
        }

    });
}

function fnListarTipoBancos(controlDestino, pclid) {
    
    $("#" + controlDestino).empty();

    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarTipoBancos",
        dataType: 'json',
        async: false,
        data: { pclid: pclid == "" ? 0 : pclid },

        success: function (states) {
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' + state.Text + '</option>');

            });
        },
        error: function (ex) {
            alert('Error al cargar los tipos de banco.' + ex);
        }

    });
}

function fnCuentaBancoSeleccionada(controlOrigen, controlDestino, pclid) {

    $("#" + controlDestino).empty();    
    
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarCuentaTipoBanco",
        dataType: 'json',
        async: false,
        data: { tipoBanco: $(controlOrigen).val() == "" ? 0 : $(controlOrigen).val(), pclid: pclid == "" ? 0 : pclid },        

        success: function (states) {
            
            $.each(states, function (i, state) {
                $("#" + controlDestino).append('<option value="' + state.Value + '">' + state.Text + '</option>');

            });
        },
        error: function (ex) {
            //alert('Error al cargar las cuentas del banco.' + ex);
        }

    });
}

function UpdateSaldoGenerico(id, isSelected, nombreGrilla, nombreForm, nombreCampo) {

    var index = $.inArray(id, idsOfSelectedRows);

    if (!isSelected && index >= 0) {

        idsOfSelectedRows.splice(index, 1);
        saldoOfSelectedRows.splice(index, 1); // remove id from the list

    } else if (index < 0) {
        idsOfSelectedRows.push(id);
        saldoOfSelectedRows.push(parseFloat($('#' + nombreGrilla).jqGrid('getCell', id, nombreCampo)));
    }

    $('#' + nombreForm + ' input[id=' + nombreCampo + ']').val(formatThousands(sum(saldoOfSelectedRows), 2));

}

function fnOnSelectAllGenerico(aRowids, status, nombreGrilla, nombreForm, nombreCampo) {
    var i, count, id;

    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateSaldoGenerico(id, status, nombreGrilla, nombreForm, nombreCampo);
    }
    $('#' + nombreForm + ' input[id=' + nombreCampo + ']').val(formatThousands(sum(saldoOfSelectedRows), 2));
}

function fnEnviarEmailMutualPagos() {

    if ($("#frmEmailMutualPagos input[id=Pclid]").val() != '' && $("#frmEmailMutualPagos input[id=Ctcid]").val() != '') {

        if ($("#frmEmailMutualPagos #Monto").val() == $("#frmEmailMutualPagos #Saldo").val()) {

            if ($("#frmEmailMutualPagos #FechaMailMutual").val() != '') {

                if ($("#frmEmailMutualPagos #Saldo").val() != "" && $("#frmEmailMutualPagos #Saldo").val() != 0) {

                    var newUrl = "/Email/EnviarEmailMutualPagos/?"

                    var documentosRows = JSON.stringify(jQuery('#gridDeudorCpbtMutualPagos').jqGrid('getGridParam', 'selarrrow'));

                    newUrl += "Pclid=" + $("#frmEmailMutualPagos input[id=Pclid]").val() + "&Ctcid=" + $("#frmEmailMutualPagos input[id=Ctcid]").val() + "&Cuenta=" + $("#frmEmailMutualPagos #CuentaBanco").val() + "&Banco=" + $("#frmEmailMutualPagos #TipoBanco").val() + "&Saldo=" + $("#frmEmailMutualPagos #Saldo").val().replace(/\./g, '').replace(/\,/g, '.') //+ "&Documentos=" + documentosRows
                    newUrl += "&Monto=" + $("#frmEmailMutualPagos #Monto").val().replace(/\./g, '').replace(/\,/g, '.') + "&FechaMail=" + $("#frmEmailMutualPagos #FechaMailMutual").val() + "&ComentarioMail=" + $("#frmEmailMutualPagos #ComentarioMail").val() + "&Gestion=" + $("#frmEmailMutualPagos #Gestion").val()

                    if ($("#frmEmailMutualPagos input[id=Pclid]").val() == "609") {

                        if ($("#frmEmailMutualPagos #Gestion").val() == "51") {
                            newUrl += "&TipoReporte=10";
                        }
                        else {
                            newUrl += "&TipoReporte=9";
                        }

                    }
                    else {

                        if ($("#frmEmailMutualPagos #Gestion").val() == "51") {
                            newUrl += "&TipoReporte=7";
                        }
                        else {
                            newUrl += "&TipoReporte=6";
                        }

                    }


                    $.ajax({
                        type: 'POST',
                        url: newUrl, // we are calling json method
                        dataType: 'json',
                        async: true,
                        data: { Documentos: documentosRows },
                        beforeSend: function () { $("body").addClass("loading"); },
                        success: function (data) {
                            $("body").removeClass("loading");
                            if (data === false) {
                                alert("Correos enviados con éxito.");
                            } else {
                                alert("Correos enviados con éxito.");
                            }
                            $('#ppEnvioEmailMutualPagos').dialog('close');
                            ResetMailMutualPagos();
                            fnBuscarCpbtDeudor();
                            fnBuscarHistorialDeudor();
                        },
                        error: function (ex) {
                            alert('Error al enviar correos.' + ex);
                        }

                    });

                }
                else {
                    alert("No se han seleccionado facturas");
                }

            }
            else {
                alert("Debe seleccionar la fecha de envío de mail");
            }
        }
        else {
            alert("El monto ingresado y el monto seleccionado deben ser iguales");
        }
    }
    else {
        alert("Debe ingresar cliente y deudor");
    }

}

function ResetMailMutualPagos() {

    $("#frmEmailMutualPagos input[id=Monto]").val('');
    $("#frmEmailMutualPagos input[id=Saldo]").val('');
    $("#frmEmailMutualPagos input[id=FechaMailMutual]").val('');
    $("#frmEmailMutualPagos #ComentarioMail").val('');
}
//Mail Mutual

function fnBuscarEmailsMasivo(formName, div) {

    idsOfSelectedRows = [];
    saldoOfSelectedRows = [];
    ResetMailMutual(formName, div);

    var newUrl = "/Cartera/GetEmailMutual/?"
    newUrl += "Ctcid=" + $("#" + formName + " input[id=Ctcid]").val() + "&email="

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {            
            $.each(data.rows, function (i, array) {
                $('#' + formName + ' div#' + div).append('<div class="col"><div class="mails-lbl">' + array.cell[1] + '<span onclick="$(this).parent().parent().remove()" style="cursor:pointer">&nbsp;&times;</span></div></div>');
            });
        }

    });

}

function fnBuscarDeudoresCpbtMutual() {
    
    if ($("#Pclid").val() != '') {

        var newUrl = "/Cartera/GetDeudoresMailMutualCpbt/?"
        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val()

        jQuery("#gridDeudorCpbtMutual").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnEnviarEmailMutual() {
    var mailsDestino = "";

    $('#frmEmailMutual div#mailsMutual div div').each(function () {
        mailsDestino += $(this).text().substring(0, $(this).text().length - 2) + ",";
    });

    mailsDestino = mailsDestino.substring(0, mailsDestino.length - 1);

    if ($("#frmEmailMutual input[id=Pclid]").val() != '' && $("#frmEmailMutual input[id=Ctcid]").val() != '') {

        if (idsOfSelectedRows != '' || $("#frmEmailMutual input[id=Pclid]").val() == 559) {

            if (mailsDestino.length > 0 && mailsDestino != '') {

                var newUrl = "http://localhost:57065/Email/EnviarEmailMutual/?"

                var documentosRows = JSON.stringify(idsOfSelectedRows);

                newUrl += "Pclid=" + $("#frmEmailMutual input[id=Pclid]").val() + "&Ctcid=" + $("#frmEmailMutual input[id=Ctcid]").val() + "&Email=" + mailsDestino //+ "&Documentos=" + documentosRows + "&Email=" + mailsDestino

                if ($("#frmEmailMutual input[id=Pclid]").val() == 318) {
                    newUrl += "&TipoReporte=2&FechaMail=" + $("#frmEmailMutual #FechaMailMut").val()
                }
                else if ($("#frmEmailMutual input[id=Pclid]").val() == 559) {
                    newUrl += "&TipoReporte=3"
                }
                else if ($("#frmEmailMutual input[id=Pclid]").val() == 609) {
                    newUrl += "&TipoReporte=8"
                }

                $.ajax({
                    type: 'POST',
                    url: newUrl, // we are calling json method{
                    dataType: 'json',
                    async: true,
                    data: { Documentos: documentosRows },
                    beforeSend: function () { $("body").addClass("loading"); },
                    success: function (data) {
                        $("body").removeClass("loading");
                        if (data === false) {
                            alert("Correos enviados con éxito.");
                        } else {
                            alert("Correos enviados con éxito.");
                        }
                        $('#ppEnvioEmailMutual').dialog('close');
                        ResetMailMutual("frmEmailMutual", "mailsMutual");
                        fnBuscarCpbtDeudor();
                        fnBuscarHistorialDeudor();
                    },
                    error: function (ex) {
                        alert('Error al enviar correos.' + ex);
                    }

                });
            }

            else {
                alert("No hay destinatarios para enviar mail");
            }

        }
        else {
            alert("No hay facturas para seleccionar");
        }
    }
    else {
        alert("Debe ingresar cliente y deudor");
    }

}

function ResetMailMutual(formName, div) {
    $('#' + formName + ' div#' + div + ' .col .mails-lbl').parent().remove();
    $("#" + formName + " input[id=Saldo]").val('');
}

function UpdateSaldoMutual(id, isSelected) {
        
        var index = $.inArray(id, idsOfSelectedRows);

        if (!isSelected && index >= 0) {
            
            idsOfSelectedRows.splice(index, 1);
            saldoOfSelectedRows.splice(index, 1); // remove id from the list

        } else if (index < 0) {
            idsOfSelectedRows.push(id);
            saldoOfSelectedRows.push(parseFloat($('#gridDeudorCpbtMutual').jqGrid('getCell', id, 'Saldo')));            
        }

        $('#frmEmailMutual input[id=Saldo]').val(formatThousands(sum(saldoOfSelectedRows), 2));
        
}

function fnOnSelectAllMutual(aRowids, status) {
    var i, count, id;

    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateSaldoMutual(id, status);
    }
    $('#frmEmailMutual input[id=Saldo]').val(formatThousands(sum(saldoOfSelectedRows), 2));
}

//Mail Cocha
var saldoOfSelectedRows = [];
var idsOfDolaresRows = [];
var saldoOfDolaresRows = [];

function UpdateSaldoOfSelectedRows(id, isSelected) {

    if ($('#gridDeudorCpbt').jqGrid('getCell', id, 'Moneda') == "PESOS") {
        var index = $.inArray(id, idsOfSelectedRows);

        if (!isSelected && index >= 0) {

            /* $('#gridDeudorCpbt').setColProp('Saldo', { editable: false });
             $('#gridDeudorCpbt').setColProp('Moneda', { editable: false });
             $('#gridDeudorCpbt').jqGrid('editRow', id, false);*/

            idsOfSelectedRows.splice(index, 1);
            saldoOfSelectedRows.splice(index, 1); // remove id from the list

        } else if (index < 0) {
            idsOfSelectedRows.push(id);
            saldoOfSelectedRows.push(parseFloat($('#gridDeudorCpbt').jqGrid('getCell', id, 'Saldo')));

            /*  $('#gridDeudorCpbt').jqGrid('editRow', id, true);
              $('#gridDeudorCpbt').setColProp('Saldo', { editable: true });
              $('#gridDeudorCpbt').setColProp('Moneda', { editable: true });*/

        }
        
        $('#Saldo').val(formatThousands(sum(saldoOfSelectedRows),2));
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

        $('#SaldoDolar').val(formatThousands(sum(saldoOfDolaresRows), 2));

        $('#Banco > option:contains("USD")').attr('selected', 'selected');
        TraeCuentaProvcliBanco();
    }
}

function fnOnSelectAllCocha(aRowids, status) {
    var i, count, id;

    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];

        UpdateSaldoOfSelectedRows(id, status);
    }
    $('#Saldo').val(formatThousands(sum(saldoOfSelectedRows), 2));
    $('#SaldoDolar').val(formatThousands(sum(saldoOfDolaresRows), 2));
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
    idsOfDolaresRows = [];
    saldoOfDolaresRows = [];
    ResetEnvioMail();

    //$('#Saldo').val('');

    if ($("#Pclid").val() != '') {

        var newUrl = "/Cartera/GetDeudoresMailCochaCpbt/?"
        newUrl += "RutCliente=" + $("#RutCliente").val() + "&NombreCliente=" + $("#NombreClienteBuscar").val() + "&Pclid=" + $("#Pclid").val()
        newUrl += "&Rut=" + $("#RutDeudorBuscar").val() + "&Ctcid=" + $("#Ctcid").val() + "&Email=" + $("#Email").val()

        jQuery("#gridDeudorCpbt").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
}

function fnEnviarEmailCocha() {
    
    //var mails = $("#frmEmail input[id=Email]").val().split(","), estadoMail = false;
    
    //estadoMail = validarEmail(mails);

    if ($("#frmEmail input[id=Pclid]").val() != '' && $("#frmEmail input[id=Ctcid]").val() != '') {

        if (fnValidaMontoSaldo() == true && $("#MontoDolar").val() == $("#SaldoDolar").val()) {

            if ($("#FechaMail").val() != '') {

                //if ($("#TipoReporte").val() != '') {

                    var newUrl = "/Email/EnviarEmailCocha/?"
                    
                    var documentosRows = JSON.stringify(jQuery('#gridDeudorCpbt').jqGrid('getGridParam', 'selarrrow'));

                    newUrl += "Pclid=" + $("#frmEmail input[id=Pclid]").val() + "&Ctcid=" + $("#frmEmail input[id=Ctcid]").val() + "&Cuenta=" + $("#Cuenta").val() + "&Banco=" + $("#Banco").find(":selected").text() + "&Saldo=" + $("#Saldo").val().replace(/\./g, '').replace(/\,/g, '.') //+ "&Documentos=" + documentosRows
                    newUrl += "&TipoReporte=1&Monto=" + $("#Monto").val().replace(/\./g, '').replace(/\,/g, '.') + "&SaldoDolar=" + $("#SaldoDolar").val().replace(/\./g, '').replace(/\,/g, '.') + "&MontoDolar=" + $("#MontoDolar").val().replace(/\./g, '').replace(/\,/g, '.') + "&FechaMail=" + $("#FechaMail").val() + "&ValidaNotaCredito=" + fnValidaMontoSaldo()

                    if ($("#CheckNotaCredito").prop('checked')) {
                        newUrl += "&NotaCredito=" + $("#NotaCredito").val().replace(/\./g, '').replace(/\,/g, '.') + "&ComentarioMail=" + $("#ComentarioMail").val() + "&CheckNotaCredito=" + $("#CheckNotaCredito").prop('checked')
                    }
                    
                    $.ajax({
                        type: 'POST',
                        url: newUrl, // we are calling json method
                        dataType: 'json',
                        async: true,
                        data: { Documentos: documentosRows },
                        beforeSend: function () { $("body").addClass("loading"); },
                        success: function (data) {
                            $("body").removeClass("loading");
                            if (data === false) {
                                alert("Correos enviados con éxito.");
                            } else {
                                alert("Correos enviados con éxito.");
                            }
                            $('#ppEnvioEmail').dialog('close');
                            ResetEnvioMail();
                            fnBuscarCpbtDeudor();
                            fnBuscarHistorialDeudor();
                        },
                        error: function (ex) {
                            alert('Error al enviar correos.' + ex);
                        }

                    });
                /*}
                else {
                    alert("Debe seleccionar un tipo de reporte");
                }*/
            }
            else {
                alert("Debe seleccionar la fecha de envío de mail");
            }
        }
        else {
            alert("El monto y el saldo deben ser iguales");
        }
    }
    else {
        alert("Debe ingresar cliente y deudor");
    }
    //$("#frmEmail").trigger("reset");
    
}

function validarEmail(email) {
    expr = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    
    for (var i = 0; i < email.length; i++) {
        if (!expr.test(email[i].trim())) {
            alert("Error: La dirección de correo " + email[i] + " es incorrecta.");
            return false;
        }
    }

    return true;
}

function fnCochaOnCreate() {
        
    if ($('#Ctcid').val() != '') {
        if ($('#Pclid').val() == "22") {
            
            $('#frmEmail input[id=Ctcid]').val($('#Ctcid').val());
            $('#frmEmail input[id=NombreRutDeudor]').val($('#Rut').val() + ' -' + $('#Nombre').val());
        }
        else if ($('#Pclid').val() == "318" || $('#Pclid').val() == "559" || $('#Pclid').val() == "609") {
            
            $('#frmEmailMutual input[id=Ctcid]').val($('#Ctcid').val());
            $('#frmEmailMutual input[id=NombreRutDeudor]').val($('#Rut').val() + ' -' + $('#Nombre').val());

            $('#frmEmailMutualPagos input[id=Ctcid]').val($('#Ctcid').val());
            $('#frmEmailMutualPagos input[id=NombreRutDeudor]').val($('#Rut').val() + ' -' + $('#Nombre').val());
        }
        else if ($('#Pclid').val() == "279") {
            $('#frmEmailCoopeuch input[id=Ctcid]').val($('#Ctcid').val());
            $('#frmEmailCoopeuch input[id=NombreRutDeudor]').val($('#Rut').val() + ' -' + $('#Nombre').val());
        }
    }
    
    //$('#TipoReporte > option[value="1"]').attr('selected', 'selected');
    if ($('#Pclid').val() == "22") {        
        fnBuscarDeudoresCpbtCocha();
        TraeCuentaProvcliBanco();
    }
    else if ($('#Pclid').val() == "318" || $('#Pclid').val() == "609") {
        fnBuscarEmailsMasivo("frmEmailMutual", "mailsMutual");
        
        $('#frmEmailMutual #idSaldo').show();
        $('#idGridMutual').show();
        
        fnBuscarDeudoresCpbtMutual();
        fnBuscarDeudoresCpbtMutualPagos();
        fnListarTipoBancos("frmEmailMutualPagos #TipoBanco", $('#Pclid').val());
        fnCuentaBancoSeleccionada($('#frmEmailMutualPagos #TipoBanco'), "CuentaBanco", $('#Pclid').val());
    }
    else if ($('#Pclid').val() == "559") {
        fnBuscarEmailsMasivo("frmEmailMutual", "mailsMutual");
        $('#frmEmailMutual #idSaldo').hide();
        $('#idGridMutual').hide();
    }
    else if ($('#Pclid').val() == "279") {
        fnBuscarEmailsMasivo("frmEmailCoopeuch", "mailsCoopeuch");
    }
}

function ActivarBotonMail() {
    
    if ($('#Pclid').val() == "22") {
        $('#frmEmail input[id=Pclid]').val($('#Pclid').val());
        $('#frmEmail input[id=NombreRutCliente]').val($('#Cliente').val());

        $("#btnEnviarEmail").show();
        $("#btnEnviarEmailMutual").hide();
        $("#btnEnviarEmailCoopeuch").hide();
    }
    else if ($('#Pclid').val() == "318" || $('#Pclid').val() == "559" || $('#Pclid').val() == "609") {
        $('#frmEmailMutual input[id=Pclid]').val($('#Pclid').val());
        $('#frmEmailMutual input[id=NombreRutCliente]').val($('#Cliente').val());

        $('#frmEmailMutualPagos input[id=Pclid]').val($('#Pclid').val());
        $('#frmEmailMutualPagos input[id=NombreRutCliente]').val($('#Cliente').val());

        $("#btnEnviarEmail").hide();
        $("#btnEnviarEmailMutual").show();
        $("#btnEnviarEmailCoopeuch").hide();
    }
    else if ($('#Pclid').val() == "279") {
        $('#frmEmailCoopeuch input[id=PclidCoopeuch]').val($('#Pclid').val());
        $('#frmEmailCoopeuch input[id=NombreRutCliente]').val($('#Cliente').val());

        $("#btnEnviarEmail").hide();
        $("#btnEnviarEmailMutual").hide();

        $.ajax({
            type: 'POST',
            url: '/Cartera/GetDeudorCodigoCargaCount/?Pclid=' + $('#Pclid').val() + '&Ctcid=' + $('#Ctcid').val() + '&EstCpbt=V&CodCarga=8', // we are calling json method
            dataType: 'json',
            async: true,
            success: function (data) {
                if (data > 0) {
                    $("#btnEnviarEmailCoopeuch").show();
                } else {
                    $("#btnEnviarEmailCoopeuch").hide();
                }
            },
            error: function (ex) {
                $("#btnEnviarEmailCoopeuch").hide();
            }

        });
        
    }
    else {
        $("#btnEnviarEmail").hide();
        $("#btnEnviarEmailMutual").hide();
    }
}

function ResetEnvioMail() {
    $("#frmEmail input[id=Email]").val('');
    $("#frmEmail input[id=Cuenta]").val('');
    $("#frmEmail input[id=Monto]").val('');
    $("#frmEmail input[id=MontoDolar]").val('');
    $("#frmEmail input[id=Saldo]").val('');
    $("#frmEmail input[id=SaldoDolar]").val('');
    $("#frmEmail input[id=Banco]").val('');
    $("#frmEmail input[id=CheckNotaCredito]").prop('checked', false);
    $("#divNotaCredito").hide();
    $("#frmEmail input[id=NotaCredito]").val('');
    $("#ComentarioMail").val('');
    $("#frmEmail input[id=FechaMail]").val('');
}

function TraeCuentaProvcliBanco() {

    if ($("#Pclid").val() != '') {

        var newUrl = "/Cartera/GetCuentaProvcliBanco/?";
        newUrl += "Pclid=" + $("#Pclid").val() + "&Tipo=" + $("#Banco").val()

        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: false,
            success: function (data) {
                $("#Cuenta").val(data);
            }

        });

    }
        
}

function fnMostrarNotaCredito() {
    
    if ($("#CheckNotaCredito").prop('checked')) {
        $("#divNotaCredito").show();
    } else {
        $("#divNotaCredito").hide();
    }

}

function fnValidaMontoSaldo() {
    if ($("#CheckNotaCredito").prop('checked')) {
        if (parseFloat($("#Monto").val().replace(/\./g, '').replace(/\,/g, '.')) + parseFloat($("#NotaCredito").val().replace(/\./g, '').replace(/\,/g, '.')) == parseFloat($("#Saldo").val().replace(/\./g, '').replace(/\,/g, '.'))) return true;
        else return false;
    } else {
        if ($("#Monto").val() == $("#Saldo").val()) return true;
        else return false;
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
    $(".ui-dialog-content").html('<object data="'+rowid+'" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="'+rowid+'">.</a></p></object>');
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
        data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), NuevoEnte: $("#EnteJudicial").val(), ListaEntes: s+'' },
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
            url:  "/Judicial/GuardarRol/", // we are calling json method
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
                        newUrl += "Pclid=" + $("#Pclid").val()+"&Ctcid=" + $("#Ctcid").val();
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
                            fnGuardarDocumentos(JSON.stringify( agregar),JSON.stringify( eliminar));
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

function FormatCurrency(ctrl,event) {
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
    if (row.TipoCliente == 'P') {
        $('#tabGestion ul:first li:eq(1) a span').text("Previsional");
    } else {
        $('#tabGestion ul:first li:eq(1) a span').text("Judicial");
    }
    CargarDeudor();
    $(".ui-dialog-content").dialog().dialog("close");
    $("#frmBuscarDeudor").reset();
    jQuery("#Deudor").jqGrid().setGridParam({ url: "/Cartera/GetDeudores/?NumeroCPBT=-1119999" }).trigger('reloadGrid', [{ page: 1 }])
    $('.email-pclid>#PclidEmail').attr('data-pclidbase', row.Pclid);
    $('.email-pclid>#NombreRutCliente').attr('cliente', row.NombreCliente);

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
                    if (row.TipoCliente == 'P') {
                        $('#tabGestion ul:first li:eq(1) a span').text("Previsional");
                    } else {
                        $('#tabGestion ul:first li:eq(1) a span').text("JudicialNNN");
                    }

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

function fnMuestraDocumentoCliente(id)
{
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
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() ;
    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        async: false,
        success: function (data) {
            $('#tabDetallesDeudor').tabs('select', 0);
            if (data != '') {
                //$("#tabDetallesDeudor").tabs({ disabled: [ 6] });
                $("#carouselDocumentos").html('<ul style="padding:0px;margin:0px">' + data + '</ul>');
                SelectGalleryFotos();
                $(".rg-view-full").click();
                $(".rg-view-thumbs").click();
               
            } else {
                //$("#tabDetallesDeudor").tabs({ disabled: [6] });
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
        url: "/Cartera/ListarEstadosCobranzaClientePerfil", // ListarEstadosHistorial we are calling json method
        dataType: 'json',
        async: false,
        //data: { grupo: $("#Agrupa").val(), tipo: $("#Tipo").val(), estadoXDoc: estados },
        data: { grupo: $("#Agrupa").val(), pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val(), estadoXDoc: estados },
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
                d.dialog("option", "height", $("#divTabla").outerHeight() +50);
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
            if(data.Compromiso == "S" && data.Utiliza == "D"){
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
        if (!item.Compromiso) { item.Compromiso = "0";}
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
    $("#divIdPais").hide();
    var d = $("#ppContactoTelefono").dialog();
    $("#frmContactoTelefono").reset();
    $("#Ddcid").val("");
    fnActualizarTipoDireccionSitrel();
    switch (tipo) {
        case "T":
            $("#TipoForm").val(tipo);
            $("#divContactoEmail").hide();
            $("#divDireccionesContacto").hide();
            $('#NombreContacto').show();
            if ($("#Pclid").val() == 522) {
                $("#divAnexo").show();
                //$("#divDireccionesContacto").show();
                //$("#divTipoDireccion").show();
            } else {
                $("#divAnexo").hide();
                //$("#divDireccionesContacto").hide();
                //$("#divTipoDireccion").hide();
            }    
            $("#divContactoTelefono").show();
            $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Teléfono Contacto');
            break;
        case "E":
            $("#TipoForm").val(tipo);
            $("#divContactoTelefono").hide();
            $("#divContactoEmail").show();
            $("#divDireccionesContacto").hide();
            //if ($("#Pclid").val() == 522) {
            //    $("#divDireccionesContacto").show();
            //    $("#divTipoDireccion").show();
            //} else {
            //    $("#divDireccionesContacto").hide();
            //    $("#divTipoDireccion").hide();
            //}

            $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Email Contacto');
            break;
        case "D":
            $("#TipoForm").val(tipo);
            $("#divContactoTelefono").hide();
            $("#divContactoEmail").hide();
            $("#divDireccionesContacto").show();
            $("#divTipoDireccion").hide();
            if ($("#Pclid").val() == 522)
            {
                $("#divTipoDireccion").show();
                $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Dirección Contacto');
            } else {
                $("#divTipoDireccion").hide();
                $('#ppContactoTelefono').dialog('option', 'title', 'Agregar Dirección de Visita Terreno');
            }
           
            break;
    }
    $('#IdPais').val($('#Pais').val());
    PaisSeleccionado($('#IdPais'), "IdRegion");
    $("#IdRegion").val($('#Region').val());
    RegionSeleccionada($('#IdRegion'), "IdCiudad");
    $("#IdCiudad").val($('#Ciudad').val());
    CiudadSeleccionada($('#IdCiudad'), "IdComuna");
    $('#IdComuna').val($('#Comuna').val());

    $('#DireccionContacto').val($('#Direccion').val());
    $('#NombreContacto').val($('#Nombre').val());
    $('#TipoContacto').val(6);


    $('#TipoTelefono').val("M");

    $('#ppContactoTelefono').dialog('open');
    d.dialog("option", "height", $("#divTablaContacto").outerHeight() + 50);
}

function ActualizarContactoTelefono() {
    setTimeout(function () {
        $('.email-pclid>#NombreRutCliente').val($('.email-pclid>#NombreRutCliente').attr('cliente'));
        $('.email-pclid>#PclidEmail').val($('.email-pclid>#PclidEmail').attr('data-pclidbase'));
    }, 1000);
}

function fnGuardarContacto() {
    var newUrl = "/Cartera/GuardarContacto/?"
    var postData = $("#frmContactoTelefono").serialize();
    var pclid = $('.email-pclid>#PclidEmail').val();
    if (!pclid) {
        pclid = $('.email-pclid>#PclidEmail').attr('data-pclidbase');
    }

    var extraData = {
        'Pclid': pclid,
        'Ctcid': $("#Ctcid").val(),
        'TipoForm': $("#TipoForm").val()
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
                fnBuscarEmailDeudorProv();
                fnBuscarTelefonosDeudor();
                fnBuscarDireccionDeudor();
                $("body").removeClass("loading");
                $('#ppContactoTelefono').dialog('close');
                //$("#btnGuardarDeudor").attr("disabled", "disabled");

            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar el deudor.' + ex);
                debugger;
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
        if ($("#Reporte").val() == '1' && $("#pag").val() == 357) {
            newUrl += "&tipo=" + $("#SituacionCartera").val() + "&codigoCarga=" + $("#CodigoCarga").val() + "&gestor=" + $("#Gestor").val()
        }
        if ($("#Reporte").val() == '7' && $("#pag").val() == 357) {
            newUrl += "&tipo=" + $("#SituacionCartera").val() + "&desde=" + $("#FechaDesde").val() + "&hasta=" + $("#FechaHasta").val()
        }
        if ($("#Reporte").val() == '21' && $("#pag").val() == 357) {
            if ($("#Pclid").val() == '') {
                newUrl = "/Reportes/GeneraReporte/?pclid=0&rep=" + $("#Reporte").val() + "&tipoCartera=" + $("#TipoCartera").val() + "&pag=" + $("#pag").val() + "&gestor=" + $("#Gestor").val()
            }
            else
            {
                newUrl += "&gestor=" + $("#Gestor").val()
            }
            
        }
        if (($("#Reporte").val() == '3' || $("#Reporte").val() == '4') && $("#pag").val() == 358) {
            if (!$("#CodigoCarga").val()) {
                return false;
            }
            newUrl += "&codigoCarga=" + $("#CodigoCarga").val()
        }
        if ($("#Reporte").val() == '9' && $("#pag").val() == 358) {
            newUrl += "&ctcid=" + $("#Ctcid").val() + "&tipo=" + $("#SituacionCartera").val()
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

        if (formato != 1){
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
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarTelefonoContacto(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarTelefonoContacto(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnBotonesContactoEmail(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarEmailContacto(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarEmailContacto(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
}

function fnBotonesContactoEmailProv(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarEmailContacto(\'' + rowobject + '\')" >Editar</button></div><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarEmailContactoProv(\'' + rowobject + '\')">Eliminar</button></div></div></div>';
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
    $("#frmContactoTelefono select[id=TipoEmail]").val(datos[1].substring(0,1));// + ")").attr('selected', 'selected');
    //$('#frmContactoTelefono select[id=TipoEmail]').val(datos[6]);
    if (datos[14] == 'S') {
        $('#frmContactoTelefono input[id=EmailMasivo]').prop( "checked", true );
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

function fnEliminarEmailContactoProv(id) {
    var r = confirm("Desea eliminar el email de contacto?");
    if (r == true) {
        var datos = id.split(',');
        var url = "/Cartera/EliminarContactoEmailProv/?id=" + datos[4] + '|' + datos[0];
        $.ajax({
            type: 'POST',
            url: url,
            dataType: 'json',
            async: true,
            success: function (data) {
                fnBuscarEmailDeudorProv();
            },
            error: function (ex) {
                alert('Error al eliminar email.' + ex);
            }

        });
        
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

function fnBtnEnviarEmail() {
    $('#ppEnvioEmail').dialog('open');
    fnCochaOnCreate();
    return false;
}

function fnBtnTipoMailMutual() {
    $('#ppTipoMailMutual').dialog('open');
    return false;
}

function fnBtnEnviarEmailMutual() {
    $('#ppTipoMailMutual').dialog('close');
    $('#ppEnvioEmailMutual').dialog('open');
    fnCochaOnCreate();
    return false;
}

function fnBtnEnviarEmailMutualPagos() {
    if ($("#Pclid").val() == "318" || $("#Pclid").val() == "609") {
        $('#ppTipoMailMutual').dialog('close');
        $('#ppEnvioEmailMutualPagos').dialog('open');
        fnCochaOnCreate();
    }
    return false;
}

function fnBtnEnviarEmailCoopeuch() {
    $('#ppEnvioEmailCoopeuch').dialog('open');
    fnCochaOnCreate();
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

function fnCargarArchivosSuseso() {
    var newUrl = "/Cartera/ProcesoCargaSuseso/";
    var postData = $("#frmCargaMasivaSuseso").serializeArray();

    //Limpiar tablas
    $("#grdCargaMasivaSusesoIntereses").jqGrid('clearGridData');
    $("#grdCargaMasivaSusesoReajuste").jqGrid('clearGridData');

    //Llamada ajax
    if ($('#frmCargaMasivaSuseso').valid()) {
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

                if (data.Error) {
                    for (var i = 0; i < data.ListaSusesoArchivoInteres.length; i++) {
                        var item = data.ListaSusesoArchivoInteres[i];

                        if (item.Error != "" && item.Error != null) {
                            $("#grdCargaMasivaSusesoIntereses").jqGrid('addRowData', i + 1, item);
                        }
                    }

                    for (var i = 0; i < data.ListaSusesoArchivoReajuste.length; i++) {
                        var item = data.ListaSusesoArchivoReajuste[i];

                        if (item.Error != "" && item.Error != null) {
                            $("#grdCargaMasivaSusesoReajuste").jqGrid('addRowData', i + 1, item);
                        }
                    }

                    alert('No se han podido cargar los archivos porque contienen errores');
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
    if (!$("#AccionSitrel").val().indexOf("BUSQANT") !=-1) {
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
    if ($("#RespuestaSitrel").val().indexOf("COMP")!=-1) {
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
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val() },//$("#Pclid").val() == "" ? 0 : $("#Pclid").val() },
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
    if ($("#RespuestaSitrel").val().indexOf("COMPAG")> -1) {
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
    
    var rowIds = $('#grdCargaMasivaItau').jqGrid('getDataIDs');
    
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

function fnLimpiarCargaSuseso() {
    $('#frmCargaMasiva').reset();
    $("#imgSubirArchivo").removeClass("ok").removeClass("error");
    $("#btnCargar").attr("disabled", "disabled");
    $("#btnProcesar").attr("disabled", "disabled");
    $("#btnSubmit").removeAttr("disabled");

    var rowIds = $('#grdCargaMasivaSuseso').jqGrid('getDataIDs');
    
    for (var i = 0, len = rowIds.length; i < len; i++) {
        var currRow = rowIds[i];
        $('#grdCargaMasivaSuseso').jqGrid('delRowData', currRow);
    }

    $("#imgSubirArchivoT1").removeClass("ok");
    $("#imgSubirArchivoT2").removeClass("ok");
    $("#imgSubirArchivoT3").removeClass("ok");
    $("#imgSubirArchivoT4").removeClass("ok");
    $("#imgSubirArchivoT5").removeClass("ok");
    $("#imgSubirArchivoT6").removeClass("ok");
    $("#imgSubirArchivoT7").removeClass("ok");
    $("#imgSubirArchivoT8").removeClass("ok");
}

function fnBuscarDireccionDeudor() {
    var newUrl = "/Cartera/GetDireccion/?"
    newUrl += "Ctcid=" + $("#Ctcid").val() 
    jQuery("#gridDireccion").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function kpComentarioItau( e) {
    if (e.keyCode != 13 && e.charCode != 59 ) {
        $("#lblComentarioSitrel").text(250 - $("#ComentarioSitrel").val().length-1);
    } else {
        e.preventDefault();
        $("#lblComentarioSitrel").text(250 - $("#ComentarioSitrel").val().length);
    }

}

function fnProcesarCargaItau(){
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

function fnProcesarCargaSuseso() {
    var newUrl = "/Cartera/SPCargaSuseso/";
    var postData = $("#frmCargaMasivaSuseso").serializeArray();

    if ($('#frmCargaMasivaSuseso').valid()) {
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
                        $("#grdCargaMasivaSuseso").jqGrid('addRowData', i + 1, data[i]);
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
    $("#TodosSeleccionados").val(0 );
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
            return "<div align='center'><button type='button' class='ui-icon ui-icon-trash' style='height:20px;width:20px'  onclick='fnEliminarGestionRol(\""+cellvalue+"\")'>Eliminar</button></div>";
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
    var desde =  $('#FechaDesde').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    var hasta = $('#FechaHasta').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
    var newUrl = "/Judicial/GetTraspasoJudicialHecho/?"
    newUrl += "fechaDesde=" + desde + "&fechaHasta=" +hasta;
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
    if (revertir == "" || $("#Estado").val() =="") {
        alert("Debe seleccionar uno o mas documentos para hacer la reversa el traspaso y debe seleccionar un nuevo estado.");
    } else {
        var postData = {
            ids: JSON.stringify(revertir),
            estid : $("#Estado").val(),
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
        data: { pclid: $("#Pclid").val() == "" ? 0 : $("#Pclid").val()  },
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
        DetalleGlosa: JSON.stringify(jQuery('#gridItemC').getCol('Abreviado')),

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
                    if(data.repetido < 1) alert(data.mensaje);
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
        $('#subirArchivoEstampe').val('');
        $('#idDocumentoEstampe').children().remove();
        $('#FecAccJud').val('');
        $('#ppCabeceraDetalleCompra').dialog('open');
    }
}

function fnAutocompleteItems( request, response ) {
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
            $("#PclidRol").val(data.Pclid);
            $("#RutNombreDeudor").val(data.NombreDeudor);
            $("#Ctcid").val(data.Ctcid);
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
    if ($('#idDocumentoEstampe').children().length != 1) {
        alert('Seleccione un archivo para cargar.');
        return false;
    }
    if ($('#FecAccJud').val() == "") {
        alert('Ingrese la fecha de acción judicial.');
        return false;
    }
    
    fnGuardarComprobante();
    
    var insId = $("#Insid").val(),
        item = $("#Item").val();

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

                if ($("#RutNombreDeudor").val() != '' && $('#subirArchivoEstampe').val() != '' && ($('#subirArchivoEstampe').val().split('\\').pop() == $('#idDocumentoEstampe').find('div').find('span').html())) {
                    var formdata = new FormData(); //FormData object
                    var fileInput = document.getElementById('subirArchivoEstampe');
                    //Iterating through each files selected in fileInput
                    for (i = 0; i < fileInput.files.length; i++) {
                        //Appending each file to FormData object
                        formdata.append(fileInput.files[i].name, fileInput.files[i]);
                    }
                    //Creating an XMLHttpRequest and sending
                    var xhr = new XMLHttpRequest();

                    xhr.open('POST', '/Cartera/Upload/?tipo=Estampe&rut=' + $("#RutNombreDeudor").val() + '&Ctcid=' + $("#Ctcid").val() + '&Pclid=' + $("#PclidRol").val() + '&TipoDocumento=' + ($("#Rolid").val() + "|" + insId + "|" + (item < 1 ? jQuery("#gridItemC").jqGrid('getGridParam', 'records') + 1 : item) + "|" + $("#TipoComprobante").val() + "|" + $("#CabeceraId").val() + "|" + $("#FecAccJud").val()));
                    xhr.send(formdata);
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            $('#ArchivoEstmp').val(xhr.responseText.replace(/\"/g, ''))
                            if (xhr.responseText == '""') {
                                alert("Error al subir el archivo al servidor.");
                                $("#imgSubirArchivo").removeClass("ok").addClass("error");
                            } else if (xhr.responseText == '-1') {
                                alert("El tipo de archivo no es válido, sólo se permiten archivos .doc, .docx, .odf o .pdf");
                                $("#imgSubirArchivo").removeClass("ok").addClass("error");
                            } else {
                                CargarDetalleCompra();
                                alert("Archivos guardados con exito.");
                                $("#imgSubirArchivo").removeClass("error").addClass("ok");
                            }
                        }
                    }
                } else {
                    if ($('#idDocumentoEstampe').find('div').find('span').html() == '') alert("Debe eliminar el archivo existente si desea cargar otro o bien falta ingresar deudor.");
                    CargarDetalleCompra();
                }

                
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
        $('#subirArchivoEstampe').val('');
        $('#idDocumentoEstampe').children().remove();
        $("#FecAccJud").val(row.FecJud);

        if (row.NombreArchivo != '') {
            $('#idDocumentoEstampe').append('<div><span>' + row.NombreArchivo + '</span><span onclick=\'$(this).parent().remove()\' style=\'cursor:pointer;color:red\'>&nbsp;&times;</span></div>');
        }
                
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
    $("#FechaEnvioRecordatorio").val(date.getDate()  + '/' + (date.getMonth()+ 1) + '/' + date.getFullYear());
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
    $("#FechaEnvioRecordatorio").val(date.getDate()  + '/' + (date.getMonth()+ 1)  + '/' + date.getFullYear());
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
        data: {  Rolid: $("#Rolid").val()},
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
                $("#xdivSituacionCartera").show();
                $("#xdivCodigoCarga").show();
                $("#xdivVencidos").hide();
                break;
            case '7':
                $("#xdivRangoFecha").show();
                $("#xdivGestor").hide();
                $("#xdivSituacionCartera").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivVencidos").hide();
                break;
            case '6':
            case '19':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                break;
            case '16':
                //$("#xdivCliente").show();
                //$("#xdivTipoCartera").show();
                $("#xdivCodigoCarga").show();
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").hide();
                $("#xdivVencidos").hide();
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                break;
            case '21':
                $("#xdivRangoFecha").hide();
                $("#xdivGestor").show();
                $("#xdivSituacionCartera").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivVencidos").show();
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
                break;
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
                ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
                break;
            case '9':
                $("#xdivNombreRutDeudor").show();
                $("#xdivCodigoCarga").hide();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").show();
                $("#xdivSituacionCartera").show();
                $('#SituacionCartera > option[value="J"]').attr('selected', 'selected');
                $("#xdivDiasPrescr").hide();
                break;
            case '14':
                $("#xdivGridAbogado").show();
                $("#xdivNombreRutCliente").hide();
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").hide();
                break;
            case '15':
                $("#xdivNombreRutDeudor").hide();
                $("#xdivCodigoCarga").hide();
                $("#xdivGridAbogado").hide();
                $("#xdivNombreRutCliente").hide();
                $("#xdivSituacionCartera").hide();
                $("#xdivDiasPrescr").show();
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

function fnExtraerBH(){
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
    if ($("#Pclid").val() != 0 && $("#Ctcid").val() != 0 && $("#Estado").val()) {
    var newUrl = "/Cartera/GetCastigoDevolucion/?"
    newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Estado=" + $("#Estado").val() + "&Cartera=" + $("#Cartera").val() + "&Tipo=" + $("#Tipo").val();


    jQuery("#gridDocumentos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}
}

function fnGrabarCastDev() {
    if ($("#Pclid").val() != '' && $("#TipoComprobante").val() != '' && $("#Estado").val() != '' && idsOfSelectedRowsDocDeudor.length > 0) {
        var optionSelected = $("#frmCastigoDevolucion select[id=Estado]").find("option:selected");
        var estado = optionSelected.val();

        var comprobanteSelected = $("#frmCastigoDevolucion select[id=TipoComprobante]").find("option:selected");
        var comprobante = comprobanteSelected.val();

        var postData = {
            Pclid: $("#Pclid").val(),
            Ctcid: $("#Ctcid").val(),
            Tipo: $("#Tipo").val(),
            TipoComprobante: comprobante,
            Estado: estado,
            Cartera: $("#Cartera").val(),
            Glosa: $("#Glosa").val(),
            Ids: JSON.stringify(idsOfSelectedRowsDocDeudor),
            IdMotivos: JSON.stringify(idsOfSelectedRowsMotivo)
        };
        //alert(JSON.stringify(idsOfSelectedRows));
        $.ajax({
            type: 'POST',
            //url: "/Cartera/GrabarCastigoDevolucion/", // we are calling json method
            url: "/Cartera/ProcesarCastigoDevolucion/",
            dataType: 'json',
            async: false,
            data: postData,
            success: function (data) {
                if (data.success) {
                    showAlert(data.mensaje, "success");
                    setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
                    fnRefrescarFormularioCastigoDevolucion();
                } else {
                    if (data.mensaje != "") {
                        showAlert(data.mensaje, "");
                        setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
                    }
                    else {
                        showAlert("No se pudo grabar el comprobante", "error");
                        setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Error al guardar curso el comprobante.' + errorThrown);
            }
        });
        
    } else {
        showAlert("Ingrese todos los datos!", "warning");
        setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
       
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
    //if (data.rows.length > 0) {
    //    for (var i = 0; i < data.rows.length; i++) {
    //        if (data.rows[i].cell[7] != cartera) {
    //            //alert(data.rows[i].cell[7]);
    //            $("[id='jqg_" + grilla.id + "_" + data.rows[i].id+"']").attr("disabled", "disabled");
    //        }
    //    }
    //}
    var i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
    }
    $('#DocumentosSeleccionados').val(idsOfSelectedRowsDocDeudor);
    
    var $grid = jQuery("#" + grilla.id),
        sumMonto = $grid.jqGrid("getCol", "Monto", false, "sum");
        sumSaldo = $grid.jqGrid("getCol", "Saldo", false, "sum");
        $grid.jqGrid("footerData", "set", { FechaVencimiento: "Total:", Monto: sumMonto, Saldo: sumSaldo });

        rowColorJudicialSinRol(grilla);
           
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

// G E S T O R
function ListarGestorGrilla() {
    var newUrl = "/Cartera/ListarGestorGrilla/?"
    
    jQuery("#gridGestores").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnBotonesGridGestores(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarGestor(\'' + rowobject + '\')" >Editar</button></div></div></div>';
}

function fnEditarGestor(id) {
    //alert(id);
    var d = $("#ppGestor").dialog();
    $("#frmGestor").reset();
    $('#ppGestor').dialog('option', 'title', 'Editar Gestor');
    $('#ppGestor').dialog('open');
    //d.dialog("option", "height", $("#divTabla").outerHeight() + 50);

    var datos = id.split(',');
    $('#frmGestor input[id=Nombre]').val(datos[0]);
    $('#frmGestor input[id=Telefono]').val(datos[1]);
    $('#frmGestor input[id=Email]').val(datos[2]);
    $('#frmGestor input[id=GesId]').val(datos[4]);
    $('#frmGestor select[id=LstTipoCartera]').val(datos[5]);
    $('#frmGestor select[id=LstGrupoCobranza]').val(datos[6]);
    $('#frmGestor select[id=LstEmpleado]').val(datos[11]);
    //
    ((datos[7] == "S") ? $("#frmGestor input[id=GestorRemoto]").prop('checked', true) : $("#frmGestor input[id=GestorRemoto]").prop('checked', false));
    ((datos[3] == "Activo") ? $("#frmGestor input[id=GestorActivo]").prop('checked', true) : $("#frmGestor input[id=GestorActivo]").prop('checked', false));
    ((datos[8] == "S") ? $("#frmGestor input[id=GestorTerreno]").prop('checked', true) : $("#frmGestor input[id=GestorTerreno]").prop('checked', false));
    $('#frmGestor input[id=TelefonoTerreno]').val(datos[9]);
    $('#frmGestor input[id=TelefonoImei]').val(datos[10]);
}
function fnRulesSectionfrmGestor() {
    
    $("#frmGestor select[id=LstEmpleado]").rules("add", {
        valueNotEquals: "-1",
        messages: {
            valueNotEquals: "por favor, seleccione el empleado"
        }
    });
}
function fnEventsfrmGestor() {
    $("#frmGestor select[id=LstEmpleado]").change(function (){
        //var optionSelected = $("option:selected", this);
        var optionSelected = $(this).find("option:selected");
        var valueSelected = this.value;
        $('#frmGestor input[id=Nombre]').val(optionSelected.text());
    });
    //sort by
    $("#frmGestor select[id=LstEmpleado] option[value='-1']").remove();//elimina seleccione
    var my_options = $("#frmGestor select[id=LstEmpleado] option");
    my_options.sort(function (a, b) {
        if (a.text > b.text) return 1;
        if (a.text < b.text) return -1;
        return 0
    })
    $("#frmGestor select[id=LstEmpleado]").empty().append("<option value='-1'>SELECCIONE</option>").append(my_options);
    
}

function fnRulesSectionTerreno() {
    
    $("#frmGestor input[id=TelefonoTerreno]").rules("add", {
        required: function () {
                return $('#frmGestor input[id=GestorTerreno]').prop('checked');
           
        },
        minlength: function () {
            if ($("#frmGestor input[id=GestorTerreno]").is(':checked')) {
                return 9;
            } else {
                return 0;
            }
        },
        maxlength: function () {
                if($("#frmGestor input[id=GestorTerreno]").is(':checked')){
                    return 9;
                } else {
                    return 99;
                }
           
        },
        digits: true,
        messages: {
            required: "este campo es requerido",
            minlength: "este campo, minimo debe tener al menos 9 dígitos",
            maxlength: "este campo, maximo debe tener al menos 9 dígitos",
            digits: "este campo debe contener sólo números"
        }
    });

    $("#frmGestor input[id=TelefonoImei]").rules("add", {
        required: function () {
            return $('#frmGestor input[id=GestorTerreno]').prop('checked') && ($("#frmGestor input[id=TelefonoTerreno]").val().length > 8);
        },
        messages: {
            required: "este campo es requerido"
        }
    });
}
function fnGuardarGestor() {
    var newUrl = "/Cartera/GuardarGestor/"
    var postData = $("#frmGestor").serializeArray();
    if ($('#frmGestor').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (resultData) {

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data == -1) {
                        alert('Error al guardar gestor, ' + 'ha ocurrido un error interno');
                    } else {
                        alert('Gestor guardado con éxito');
                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al guardar gestor. ' + 'Ha ocurrido un error interno');
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar gestor.' + ex);
            }

        });
        $('#ppGestor').dialog('close');
        jQuery("#gridGestores").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
    }
}
function fnAgregarGestor() {
    //alert(id);
    var d = $("#ppGestor").dialog();
    $("#frmGestor").reset();
    $('#ppGestor').dialog('option', 'title', 'Agregar Gestor');
    $('#ppGestor').dialog('open');
    $("#frmGestor input[id=GestorActivo]").prop('checked', true);
   
}
function fnAgregarGestorSolicitudVisita() {
    //alert(id);
    var d = $("#ppGestorSolicitudVisita").dialog();
    $("#frmGestorSolicitudVisita").reset();
    $('#ppGestorSolicitudVisita').dialog('open');
    
}
function fnRulesSectionfrmGestorSolicitudVisita() {
    
    $("#frmGestorSolicitudVisita select[id=lstGestoresTerreno]").rules("add", {
        valueNotEquals: "-1",
        messages: {
            valueNotEquals: "por favor, seleccione el Gestor"
        }
    });
}
function fnEventsfrmGestorSolicitudVisita() {
    //sort by
   
    var my_options = $("#frmGestorSolicitudVisita select[id=lstGestoresTerreno] option");
    my_options.sort(function (a, b) {
        if (a.text > b.text) return 1;
        if (a.text < b.text) return -1;
        return 0
    })
    $("#frmGestorSolicitudVisita select[id=lstGestoresTerreno]").empty().append("<option value='-1'>SELECCIONE</option>").append(my_options);
    
}

function fnEventsfrmBuscarVisitaTerrenoGenerar() {
    //Events
    $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").change(function () {
        var optionSelected = $(this).find("option:selected");
        if ($('#frmBuscarVisitaTerrenoGenerar').valid()) {
            var newUrl = "/Cartera/ListarVisitaTerrenoGenerar/?"
            newUrl += "lstGestoresTerreno=" + optionSelected.val().split('|')[0]
            jQuery("#gridGenerarVisitas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        } 
       
    });

    //sort by
    var my_options = $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno] option");
    my_options.sort(function (a, b) {
        if (a.text > b.text) return 1;
        if (a.text < b.text) return -1;
        return 0
    })
    $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").empty().append("<option value='-1'>SELECCIONE</option>").append(my_options);
    $('#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]').val(-1);

    
    //Rules
    $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").rules("add", {
        valueNotEquals: "-1",
        messages: {
            valueNotEquals: function () {
               jQuery("#gridGenerarVisitas").jqGrid().trigger('clearGridData', [{ page: 1 }]);
                
                return "por favor, seleccione el Gestor";
            }
        }
    });
    
}
function downloadFile(fileName, csv) {

    if (navigator.userAgent.indexOf('MSIE') !== -1
        || navigator.appVersion.indexOf('Trident/') > 0) {

        var IEwindow = window.open("", "", "Width=0px; Height=0px");
        IEwindow.document.write('sep=,\r\n' + csv);
        IEwindow.document.close();
        IEwindow.document.execCommand('SaveAs', true, fileName);
        IEwindow.close();
    }
    else {
        var aLink = document.createElement('a');
        var evt = document.createEvent("MouseEvents");
        evt.initMouseEvent("click", true, true, window,
            0, 0, 0, 0, 0, false, false, false, false, 0, null);
        aLink.download = fileName;
        aLink.href = 'data:text/csv;charset=UTF-8,' + encodeURIComponent(csv);
        aLink.dispatchEvent(evt);
    }
}

// Perfil Estado Cobranza

var idsOfSelectedRowsPerfil = [];

function UpdateIdsOfSelectedRowsPerfil(id, isSelected) {
    var index = $.inArray(id, idsOfSelectedRowsPerfil);
    var optionSelectedPerfil = $("#frmPerfilesEstadoCobranza select[id=lstPerfil]").find("option:selected");
    if (!isSelected && index >= 0) {
        
        idsOfSelectedRowsPerfil.splice(index, 1); // remove id from the list
        
        $.ajax({
            type: 'POST',
            url: "/Cartera/EliminarPerfilEstado", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: false,
            data: { perfil: optionSelectedPerfil.val(), estid: id, accion: 0 },
            success: function (data) {
                if (data == -1) {
                    alert("No se pudo guardar el estado del perfil");
                }
            },
            error: function (ex) {
                alert('Error al EliminarPerfilEstado.' + ex);
            }

        });
    } else if (index < 0) {
        idsOfSelectedRowsPerfil.push(id);
       
        $.ajax({
            type: 'POST',
            url: "/Cartera/InsertarPerfilEstado", // we are calling json method
            dataType: 'json',
            traditional: true,
            async: false,
            data: { perfil: optionSelectedPerfil.val(), estid: id, accion: 1 },
            success: function (data) {
                if (data == -1) {
                    alert("No se pudo guardar el estado del perfil");
                }
            },
            error: function (ex) {
                alert('Error al InsertarPerfilEstado.' + ex);
            }

        });
    }
}

function fnOnSelectAllPerfil(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateIdsOfSelectedRowsPerfil(id, status);
    }
}

function fnOnLoadCompletePerfil(grilla) {
    var $grid = jQuery("#" + grilla.id), rows = $grid[0].rows, cRows = rows.length,
    iRow, rowId, row, cellsOfRow, iCol;

    for (iRow = 0; iRow < cRows; iRow++) {
        row = rows[iRow];
        if ($(row).hasClass("jqgrow")) {
            cellsOfRow = row.cells;
            if ($(cellsOfRow[1]).text() == 'true') {
                idsOfSelectedRowsPerfil.push($(cellsOfRow[2]).text());
            }
        }
    }
    var i, count;
    for (i = 0, count = idsOfSelectedRowsPerfil.length; i < count; i++) {
      
        $grid.setSelection(idsOfSelectedRowsPerfil[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsPerfil[i]).prop("checked", true);
    }
    
    
   
}

function fnRulesEventsfrmPerfilesEstadoCobranza() {

    $("#frmPerfilesEstadoCobranza select[id=lstPerfil]").rules("add", {
        valueNotEquals: "-1",
        messages: {
            valueNotEquals: "por favor, seleccione un Perfil"
        }
    });

    $("#frmPerfilesEstadoCobranza select[id=lstTipoEstado]").rules("add", {
        valueNotEquals: "-1",
        messages: {
            valueNotEquals: "por favor, seleccione un Estado"
        }
    });
    //sort by

    var my_options = $("#frmPerfilesEstadoCobranza select[id=lstPerfil] option");
    my_options.sort(function (a, b) {
        if (a.text > b.text) return 1;
        if (a.text < b.text) return -1;
        return 0
    })
    $("#frmPerfilesEstadoCobranza select[id=lstPerfil]").empty().append("<option value='-1'>SELECCIONE</option>").append(my_options);

    
}

function fnBuscarPerfilEstadoCobranza() {
    idsOfSelectedRowsPerfil = [];
    var optionSelectedPerfil = $("#frmPerfilesEstadoCobranza select[id=lstPerfil]").find("option:selected");
    var optionSelectedAgrupa = $("#frmPerfilesEstadoCobranza select[id=lstTipoEstado]").find("option:selected");
    if ($('#frmPerfilesEstadoCobranza').valid()) {
        var newUrl = "/Cartera/ListarEstadosCarteraPerfil/?"
        newUrl += "lstPerfil=" + optionSelectedPerfil.val() + "&lstTipoEstado=" + optionSelectedAgrupa.val()
        
        jQuery("#gridPerfilesEstadoCobranza").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
       
       }
   
}
//Terreno apis
var msgsuccessEnviarTerreno = '';

function fnEnviarVisitaTerrenoGeoGestion() {
    var solicitudes;
    solicitudes = idsOfSelectedRows;
    splitted = [];
    if ($('#frmBuscarVisitaTerrenoGenerar').valid()) {
        var optionSelected = $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").find("option:selected");
        var imei = optionSelected.val().split('|')[1];
        var gestorid = optionSelected.val().split('|')[0]
        var carteraId = optionSelected.val().split('|')[2];
        var msgerror = '';
              
        if (carteraId == '') {
            alert("El Gestor no posee asociada la cartera de Geogestión")

        } else {
            if (solicitudes.length > 0) {
                //$.each(solicitudes.split('|'), function () {
                for (var i = 0; i < solicitudes.length; i++) {
                    splitted = solicitudes[i].split('|');
                   
                    if (splitted[1] != "0") {
                        //setTimeout(function () { EnviarVisitaTerrenoGestor(datos, x) }, 5000 * x);
                        //Immediately-Invoked Function Expression (IIFE)
                        //(function (index) {
                        //    setTimeout(function () { alert(index); }, i * 1000);
                        //})(i);
                        (function (index) {
                            var datos = solicitudes[index];
                            //console.log(datos);
                            $("body").addClass("loading");
                            setTimeout(function () {
                                EnviarVisitaTerrenoGestorDatos(datos, gestorid, imei, carteraId, index);
                                $("body").addClass("loading");
                            }, index * 2500)
                            
                        }(i));
                    }
                    else {
                        msgerror = 'Verifique la latitud y longitud de direcciones que intenta generar';
                    }
                }
     
                if (msgerror != "" && msgerror != null) {
                    alert(msgerror);
                }
                idsOfSelectedRows = [];
                jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
            }
        }
    }
}
function EnviarVisitaTerrenoGestorDatos(solicitudes, gestorid, imei, carteraId, icount) {
    var splitted = [];
    splitted = solicitudes.split('|');
    var userGeo = $('#frmBuscarVisitaTerrenoGenerar input[id=userGeoGestion]').val()
    var passwordGeo = $('#frmBuscarVisitaTerrenoGenerar input[id=passGeoGestion]').val()
                    var postData = {
         "user": userGeo,
         "pass": passwordGeo,
        "name": splitted[4] + "-" + splitted[0],
                        "address": splitted[3],
                        "phone": "",
                        "radius": 50,
                        "imei": imei,
                        "layer_id": carteraId,
                        "latitude": parseFloat(splitted[1].replace(",", ".").replace(/[^0-9-\.]+/g, '')),
                        "longitude": parseFloat(splitted[2].replace(",", ".").replace(/[^0-9-\.]+/g, ''))
                    }
    
    var jqXHR = $.ajax({
                        type: 'POST',
                        url: "https://apigeogestion.erictelm2m.com:7443/newClientPoi", 
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
        async: true,
        beforeSend: function () { $("body").addClass("loading"); },
                        data: JSON.stringify(postData),
                        success: function (data) {
            console.log("datos: " + JSON.stringify(postData) + " iterator: X = " + icount + "result: " + data.id)
            //alert(data.id);
            guardarEnviarVisitaTerreno(data.id, splitted[4], gestorid,imei);
            //return data.id;
        },
        error: function (jqXHR, textStatus, errorThrown) {
            //alert(JSON.stringify(jqXHR));
            //alert(textStatus);
            alert('Error al Enviar Visita Terreno Geo Gestion.' + errorThrown);
        },
        complete: function () {
            $("body").removeClass("loading");
            jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
        },
    });
    return jqXHR.responseText;
}

function guardarEnviarVisitaTerreno(idclienteGeoGestion, idVisita, gestorId,imeiTelefono) {
    $.ajax({
        type: 'POST',
        url: "/Cartera/EnviarVisitaTerreno", // we are calling json method
        dataType: 'json',
        //traditional: true,
        //async: false,
        data: { idClienteGeoGestion: idclienteGeoGestion, idvisita: idVisita, gestorId: gestorId, telefonoImei: imeiTelefono },
        success: function (result) {
            if (result != "" && result != null) {
                //alert("Informacion generada con exito");
                msgsuccessEnviarTerreno = 'Informacion generada con exito';
            }
        },
        error: function (ex) {
            //alert('Error al EnviarVisitaTerreno.' + ex);
        }
    });
}
//Terreno cartera apis
function fnAgregarVisitaTerrenoCarteraGestor() {
    //alert(id);
    var d = $("#ppCarteraVisitaGestor").dialog();
    $("#frmVisitaTerrenoCarteraGestor").reset();
    $('#ppCarteraVisitaGestor').dialog('option', 'title', 'Agregar Cartera Visita Terreno');
    $('#ppCarteraVisitaGestor').dialog('open');
   
}

function fnBotonesgridVisitaTerrenoCarteraGestores(cellvalue, options, rowobject) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnEditarVisitaTerrenoCarteraGestor(\'' + rowobject + '\')" >Editar</button></div></div></div>';
}

function fnEditarVisitaTerrenoCarteraGestor(id) {
    //alert(id);
    var d = $("#ppCarteraVisitaGestor").dialog();
    $("#frmVisitaTerrenoCarteraGestor").reset();
    $('#ppCarteraVisitaGestor').dialog('option', 'title', 'Editar Cartera Visita Terreno');
    $('#ppCarteraVisitaGestor').dialog('open');
    
    var datos = id.split(',');
    
    $('#frmVisitaTerrenoCarteraGestor input[id=CarteraNombre]').val(datos[0]);
    $('#frmVisitaTerrenoCarteraGestor input[id=Descripcion]').val(datos[1]);
    $('#frmVisitaTerrenoCarteraGestor input[id=CarteraId]').val(datos[5]);
    $('#frmVisitaTerrenoCarteraGestor select[id=LstGestor]').val(datos[6] + '|' + datos[4]);
   
}


function fnCrearVisitaTerrenoCarteraGestor() {
    var optionSelected = $("#frmVisitaTerrenoCarteraGestor select[id=LstGestor]").find("option:selected");
    var imei = optionSelected.val().split('|')[1];
    var gestorid = optionSelected.val().split('|')[0]
    var userGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=userGeoGestion]').val()
    var passGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=passGeoGestion]').val()
    if ($('#frmVisitaTerrenoCarteraGestor').valid()) {
        if (imei != "" && imei != null) {
            var carteraNombre = $('#frmVisitaTerrenoCarteraGestor input[id=CarteraNombre]').val();
            var carteraDescripcion = $('#frmVisitaTerrenoCarteraGestor input[id=Descripcion]').val();
            var postData = {
                "user": userGeoGestion,
                "pass": passGeoGestion,
                "imei": imei,
                "name": carteraNombre,
                "description": carteraDescripcion
            }
            
            var jqXHR = fnCountVisitaTerrenoCarteraGestor(gestorid);
            if (JSON.parse(jqXHR.status) == 200) {
                if (JSON.parse(jqXHR.responseText) == -1) {
                    alert("Ha ocurrido un problema al consultar las carteras");
                } else {
                    if (JSON.parse(jqXHR.responseText) > 0) {
                        alert("La Cartera no puede ser creada, El Gestor ya tiene una cartera");
                    } else {
                        if (JSON.parse(jqXHR.responseText) == 0) {
                            $.ajax({
                                type: 'POST',
                                url: "https://apigeogestion.erictelm2m.com:7443/newClientLayer",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                beforeSend: function () { $("body").addClass("loading"); },
                                data: JSON.stringify(postData),
                                success: function (data, textStatus, xhr) {
                                    console.log(xhr.status);
                                    if (xhr.status == 200) {
                                        setTimeout(function () { fnListarVisitaTerrenoCarteraGestor(userGeoGestion, passGeoGestion, imei, carteraNombre, carteraDescripcion, gestorid); }, 1000);
                                    }
                                },
                                error: function (jqXHR, textStatus, errorThrown) {
                                    $("body").removeClass("loading");
                                    if (jqXHR.status == 403) {
                                        alert("El Telefono imei del Gestor no se encuentra registrado en GeoGestion");
                                        console.log(JSON.stringify(jqXHR));
                                    }
                                    if (jqXHR.status == 500) {
                                        alert("Ha ocurrido un problema en GeoGestion");
                                        console.log(JSON.stringify(jqXHR));
                                    }
                                },
                                complete: function () {
                                    $("body").removeClass("loading");
                                },
                            });
                        }
                    }
                }
            }
            else {
                alert("Ha ocurrido un problema al consultar las carteras")
            }
        } else {
            alert("el Gestor no tiene registrado el imei")
        }
    }
}

function fnLimpiarCargaTerreno() {
    $('#frmCargaVisitaTerreno').reset();
    $("#imgSubirArchivo").removeClass("ok").removeClass("error");
    $("#btnCargar").attr("disabled", "disabled");
    $("#btnSubmit").removeAttr("disabled");
}

function fnListarVisitaTerrenoCarteraGestor(user, pass, imei, carteraNombre, carteraDescripcion, gestorid) {
    var postData = {
        "user": user,
        "pass": pass,
        "imei": imei
    }
    var idCartera = 0;
    $.ajax({
        type: 'POST',
        url: "https://apigeogestion.erictelm2m.com:7443/getClientLayers",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: true,
        beforeSend: function () { $("body").addClass("loading"); },
        data: JSON.stringify(postData),
        success: function (data, textStatus, xhr) {
            if (xhr.status == 200) {
               $.each(data, function (index, value) {
                    if ((value.name == carteraNombre)&&( index == 0)){
                        //console.log(value.name);
                        idCartera = value.id;
                        return false;
                    }
                });
                fnguardarVisitaTerrenoCarteraGestor(idCartera, gestorid, carteraNombre, carteraDescripcion);
            }

                        },
                        error: function (jqXHR, textStatus, errorThrown) {
            //alert(JSON.stringify(jqXHR));
                            //alert(textStatus);
            $("body").removeClass("loading");
            $('#ppCarteraVisitaGestor').dialog('close');
            jQuery("#gridVisitaTerrenoCarteraGestores").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            alert('Error al Enviar Visita Terreno Geo Gestion.' + errorThrown);
        },
        complete: function () {
            $("body").removeClass("loading");
            $('#ppCarteraVisitaGestor').dialog('close');
            jQuery("#gridVisitaTerrenoCarteraGestores").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            
        },
    });
}

function fnguardarVisitaTerrenoCarteraGestor(carteraId, gestorId, CarteraNombre, carteraDescripcion) {
    $.ajax({
        type: 'POST',
        url: "/Cartera/GuardarVisitaTerrenoCarteraGestor", // we are calling json method
        dataType: 'json',
        data: { carteraId: carteraId, gestorId: gestorId, carteraNombre: CarteraNombre, carteraDescripcion: carteraDescripcion },
        success: function (result) {
            if (result > 0) {
                alert("Cartera guardada con exito");
                
            }
            if (result = -2) {
                alert("La Cartera no puede ser creada, El Gestor ya tiene una cartera");
            } else {
                if (result = -1) {
                    alert("Error al guardar la cartera");
                }
            }

        },
        error: function (ex) {
            alert('Error al guardar la cartera.' + ex);
        }
    });
                        }

function fnCountVisitaTerrenoCarteraGestor(gestorid) {
    
    var jqXHR = $.ajax({
        type: 'POST',
        url: "/Cartera/CountVisitaTerrenoCarteraGestor",
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ gestorId: gestorid }),
        success: function (data) {
          
        },
        error: function (jqXHR, textStatus, errorThrown) {
            
        },
        complete: function () {
            
        },
                    });
    return jqXHR;
}
//terreno
function VerMapaVisitaTerreno(cellvalue, options, rowdata) {
    if (cellvalue != '') {
        return "<div align='center'><button style=\"vertical-align:middle\" onclick=\" fnMapaVisitasTerrenoGPS('" + rowdata[7] + "','" + rowdata[8] + "','" + rowdata[9] + "');\"  class=\"search\"><img width=\"20px\" height=\"20px\" alt=\"Documento\" title=\"Ver\" src=\"/Images/botones/Search.png\"></button></div>";
    } else {
        return "";
    }
}

function fnCargarArchivoTerreno() {

    var newUrl = "/Cartera/DescargarVisitaTerreno/?"
    newUrl += "Archivo=" + $('#Archivo').val()
    if ($("#Archivo").val() == "") {
        alert("Debe ingresar todos los datos mandatorios.");
    } else {
        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (resultData) {

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data.length > 0) {
                        for (var i = 0; i <= resultData.data.length; i++) {
                            $("#gridCargaVisitaTerreno").jqGrid('addRowData', i + 1, resultData.data[i]);
                        }

                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al cargar el archivo. ' + 'Ha ocurrido un error interno');
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al cargar el archivo.' + ex);
            }

        });
    }



    function ActualizaEstado() {
        $("#TipoComprobante").empty();
        $.ajax({
            type: 'POST',
            url: "/Cartera/ListarTipoComprobante", // we are calling json method
            dataType: 'json',
            async: false,
            data: { cartera: $("#Estado").val() == "" ? "V" : $("#Estado").val() },
            // here we are get value of selected country and passing same value as input to json method GetStates.
            success: function (states) {
                //$("#TipoComprobante").append('<option value="">-- Seleccione Operación --</option>');
                $.each(states, function (i, state) {
                    $("#TipoComprobante").append('<option value="' + state.Value + '">' +
                            state.Text + '</option>');
                    // here we are adding option for States
                });
                //fnBuscarDocsCastDev();
            },
            error: function (ex) {
                alert('Error al recuperar la operación.' + ex);
            }

        });
    }

    // Cliente Estado Cobranza

    var idsOfSelectedRowsEstadoCliente = [];

    function UpdateIdsOfSelectedRowsEstadoCliente(id, isSelected) {
        var index = $.inArray(id, idsOfSelectedRowsEstadoCliente);
        var SelectedCliente = $("#frmClienteEstadoCobranza input[id=Pclid]").val()
        if (!isSelected && index >= 0) {

            idsOfSelectedRowsEstadoCliente.splice(index, 1); // remove id from the list

            $.ajax({
                type: 'POST',
                url: "/Cartera/EliminarClienteEstado", // we are calling json method
                dataType: 'json',
                traditional: true,
                async: false,
                data: { pclid: SelectedCliente, estid: id, accion: 0 },
                success: function (data) {
                    if (data == -1) {
                        alert("No se pudo guardar el estado del cliente");
                    }
                },
                error: function (ex) {
                    alert('Error al EliminarClienteEstado.' + ex);
                }

            });
        } else if (index < 0) {
            idsOfSelectedRowsEstadoCliente.push(id);

            $.ajax({
                type: 'POST',
                url: "/Cartera/InsertarClienteEstado", // we are calling json method
                dataType: 'json',
                traditional: true,
                async: false,
                data: { pclid: SelectedCliente, estid: id, accion: 1 },
                success: function (data) {
                    if (data == -1) {
                        alert("No se pudo guardar el estado del cliente");
                    }
                },
                error: function (ex) {
                    alert('Error al InsertarClienteEstado.' + ex);
                }

            });
        }
    }

    function fnOnSelectAllEstadoCliente(aRowids, status) {
        var i, count, id;
        for (i = 0, count = aRowids.length; i < count; i++) {
            id = aRowids[i];
            UpdateIdsOfSelectedRowsEstadoCliente(id, status);
        }
    }

    function fnOnLoadCompleteEstadoCliente(grilla) {
        var $grid = jQuery("#" + grilla.id), rows = $grid[0].rows, cRows = rows.length,
        iRow, rowId, row, cellsOfRow, iCol;

        for (iRow = 0; iRow < cRows; iRow++) {
            row = rows[iRow];
            if ($(row).hasClass("jqgrow")) {
                cellsOfRow = row.cells;
                if ($(cellsOfRow[1]).text() == 'true') {
                    idsOfSelectedRowsEstadoCliente.push($(cellsOfRow[2]).text());
                }
            }
        }
        var i, count;
        for (i = 0, count = idsOfSelectedRowsEstadoCliente.length; i < count; i++) {

            $grid.setSelection(idsOfSelectedRowsEstadoCliente[i], false);
            $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsEstadoCliente[i]).prop("checked", true);
        }



    }
    function fnBuscarClienteEstadoCobranza() {
        idsOfSelectedRowsEstadoCliente = [];
        var SelectedCliente = $("#frmClienteEstadoCobranza input[id=Pclid]").val()
        if (SelectedCliente != '' && SelectedCliente != null) {
            var optionSelectedAgrupa = $("#frmClienteEstadoCobranza select[id=lstTipoEstado]").find("option:selected");
            if ($('#frmClienteEstadoCobranza').valid()) {
                var newUrl = "/Cartera/ListarEstadosCarteraCliente/?"
                newUrl += "Pclid=" + SelectedCliente + "&lstTipoEstado=" + optionSelectedAgrupa.val()

                jQuery("#gridClientesEstadoCobranza").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

            }
        } else {
            alert("Ingrese un cliente");
        }


    }
    //Mejoras terreno
    function fnCrearEnviarVisitasTerreno() {
        var solicitudes;
        solicitudes = idsOfSelectedRows;
        splitted = [];
        if ($('#frmBuscarVisitaTerrenoGenerar').valid()) {
            var optionSelected = $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").find("option:selected");
            var imei = optionSelected.val().split('|')[1];
            var gestorid = optionSelected.val().split('|')[0]
            var carteraId = optionSelected.val().split('|')[2];
            var userGeoGestion = $('#frmBuscarVisitaTerrenoGenerar input[id=userGeoGestion]').val()
            var passGeoGestion = $('#frmBuscarVisitaTerrenoGenerar input[id=passGeoGestion]').val()
            if (carteraId == '') {
                alert("El Gestor no posee asociada la cartera de Geogestión")

            } else {
                if (imei == '') {
                    alert("El Gestor no posee imei registrado")
                } else {
                    if (solicitudes.length > 0) {
                        var postData = {
                            ids: solicitudes,
                            user: userGeoGestion,
                            pass: passGeoGestion,
                            gestorId: optionSelected.val(),
                            gestorNombre: optionSelected.text()
                        };
                        $.ajax({
                            type: 'POST',
                            url: "/Cartera/CrearEnviarVisitasTerreno", // we are calling json method
                            dataType: 'json',
                            contentType: "application/json; charset=utf-8",
                            async: true,
                            beforeSend: function () { $("body").addClass("loading"); },
                            data: JSON.stringify(postData),
                            success: function (json) {
                                $("body").removeClass("loading");
                                if (json.messageError != "" && json.messageError != null) {

                                    alert(json.messageError);
                                }
                                if (json.messagesuccess != "" && json.messagesuccess != null) {
                                    alert(json.messagesuccess);
                                }
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                $("body").removeClass("loading");
                                alert('Error al generar las visitas.' + errorThrown);

                            }
                        });

                        idsOfSelectedRows = [];
                        jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                    }
                }
            }
        }
    }

    function fnCrearClientLayerVisitaTerrenoCarteraGestor() {
        var optionSelected = $("#frmVisitaTerrenoCarteraGestor select[id=LstGestor]").find("option:selected");
        var imei = optionSelected.val().split('|')[1];
        var gestorid = optionSelected.val().split('|')[0]
        var userGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=userGeoGestion]').val()
        var passGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=passGeoGestion]').val()
        if ($('#frmVisitaTerrenoCarteraGestor').valid()) {
            if (imei != "" && imei != null) {
                var carteraNombre = $('#frmVisitaTerrenoCarteraGestor input[id=CarteraNombre]').val();
                var carteraDescripcion = $('#frmVisitaTerrenoCarteraGestor input[id=Descripcion]').val();
                var postData = {
                    gestorId: gestorid,
                    userGeo: userGeoGestion,
                    passGeo: passGeoGestion,
                    imei: imei,
                    carteraNombre: carteraNombre,
                    carteraDescripcion: carteraDescripcion
                };
                $.ajax({
                    type: 'POST',
                    url: "/Cartera/CrearClientLayerVisitaTerrenoCarteraGestor",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    beforeSend: function () { $("body").addClass("loading"); },
                    data: JSON.stringify(postData),
                    success: function (data, textStatus, xhr) {
                        $("body").removeClass("loading");
                        alert(data);
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("body").removeClass("loading");
                        alert("Ha ocurrido un problema. " + errorThrown);

                    },
                    complete: function () {
                        $("body").removeClass("loading");
                    },
                });

            } else {
                alert("el Gestor no tiene registrado el imei")
            }
        }
    }
}

function ActualizaEstado() {
    $("#TipoComprobante").empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarTipoComprobante", // we are calling json method
        dataType: 'json',
        async: false,
        data: { cartera: $("#Estado").val() == "" ? "V": $("#Estado").val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            //$("#TipoComprobante").append('<option value="">-- Seleccione Operación --</option>');
            $.each(states, function (i, state) {
                $("#TipoComprobante").append('<option value="' + state.Value + '">' +
                        state.Text + '</option>');
                // here we are adding option for States
            });
            fnBuscarDocsCastDev();
        },
        error: function (ex) {
            alert('Error al recuperar la operación.' + ex);
        }

    });
}


 // Cliente Estado Cobranza

 var idsOfSelectedRowsEstadoCliente = [];

 function UpdateIdsOfSelectedRowsEstadoCliente(id, isSelected) {
     var index = $.inArray(id, idsOfSelectedRowsEstadoCliente);
     var SelectedCliente = $("#frmClienteEstadoCobranza input[id=Pclid]").val()
     if (!isSelected && index >= 0) {
        
         idsOfSelectedRowsEstadoCliente.splice(index, 1); // remove id from the list

         $.ajax({
             type: 'POST',
             url: "/Cartera/EliminarClienteEstado", // we are calling json method
             dataType: 'json',
             traditional: true,
             async: false,
             data: { pclid: SelectedCliente, estid: id, accion: 0 },
             success: function (data) {
                 if (data == -1) {
                     alert("No se pudo guardar el estado del cliente");
                 }
             },
             error: function (ex) {
                 alert('Error al EliminarClienteEstado.' + ex);
             }

         });
     } else if (index < 0) {
         idsOfSelectedRowsEstadoCliente.push(id);

         $.ajax({
             type: 'POST',
             url: "/Cartera/InsertarClienteEstado", // we are calling json method
             dataType: 'json',
             traditional: true,
             async: false,
             data: { pclid: SelectedCliente, estid: id, accion: 1 },
             success: function (data) {
                 if (data == -1) {
                     alert("No se pudo guardar el estado del cliente");
                 }
             },
             error: function (ex) {
                 alert('Error al InsertarClienteEstado.' + ex);
             }

         });
     }
 }

 function fnOnSelectAllEstadoCliente(aRowids, status) {
     var i, count, id;
     for (i = 0, count = aRowids.length; i < count; i++) {
         id = aRowids[i];
         UpdateIdsOfSelectedRowsEstadoCliente(id, status);
     }
 }

 function fnOnLoadCompleteEstadoCliente(grilla) {
     var $grid = jQuery("#" + grilla.id), rows = $grid[0].rows, cRows = rows.length,
     iRow, rowId, row, cellsOfRow, iCol;

     for (iRow = 0; iRow < cRows; iRow++) {
         row = rows[iRow];
         if ($(row).hasClass("jqgrow")) {
             cellsOfRow = row.cells;
             if ($(cellsOfRow[1]).text() == 'true') {
                 idsOfSelectedRowsEstadoCliente.push($(cellsOfRow[2]).text());
             }
         }
     }
     var i, count;
     for (i = 0, count = idsOfSelectedRowsEstadoCliente.length; i < count; i++) {

         $grid.setSelection(idsOfSelectedRowsEstadoCliente[i], false);
         $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsEstadoCliente[i]).prop("checked", true);
     }



 }
 function fnBuscarClienteEstadoCobranza() {
    idsOfSelectedRowsEstadoCliente = [];
    var SelectedCliente = $("#frmClienteEstadoCobranza input[id=Pclid]").val()
    if (SelectedCliente != '' && SelectedCliente != null) {
        var optionSelectedAgrupa = $("#frmClienteEstadoCobranza select[id=lstTipoEstado]").find("option:selected");
        if ($('#frmClienteEstadoCobranza').valid()) {
            var newUrl = "/Cartera/ListarEstadosCarteraCliente/?"
            newUrl += "Pclid=" + SelectedCliente + "&lstTipoEstado=" + optionSelectedAgrupa.val()

            jQuery("#gridClientesEstadoCobranza").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

        }
    } else {
        alert("Ingrese un cliente");
    }
     

 }
//Mejoras terreno
 function fnCrearEnviarVisitasTerreno() {
     var solicitudes;
     solicitudes = idsOfSelectedRows;
     splitted = [];
     if ($('#frmBuscarVisitaTerrenoGenerar').valid()) {
         var optionSelected = $("#frmBuscarVisitaTerrenoGenerar select[id=lstGestoresTerreno]").find("option:selected");
         var imei = optionSelected.val().split('|')[1];
         var gestorid = optionSelected.val().split('|')[0]
         var carteraId = optionSelected.val().split('|')[2];
         var userGeoGestion = $('#frmBuscarVisitaTerrenoGenerar input[id=userGeoGestion]').val()
         var passGeoGestion = $('#frmBuscarVisitaTerrenoGenerar input[id=passGeoGestion]').val()
         if (carteraId == '') {
             alert("El Gestor no posee asociada la cartera de Geogestión")

         } else {
             if (imei == '') {
                 alert("El Gestor no posee imei registrado")
             } else {
                 if (solicitudes.length > 0) {
                     var postData = {
                         ids: solicitudes,
                         user: userGeoGestion,
                         pass: passGeoGestion,
                         gestorId: optionSelected.val(),
                         gestorNombre: optionSelected.text()
                     };
                     $.ajax({
                         type: 'POST',
                         url: "/Cartera/CrearEnviarVisitasTerreno", // we are calling json method
                         dataType: 'json',
                         contentType: "application/json; charset=utf-8",
                         async: true,
                         beforeSend: function () { $("body").addClass("loading"); },
                         data: JSON.stringify(postData),
                         success: function (json) {
                             $("body").removeClass("loading");
                             if (json.messageError != "" && json.messageError != null) {

                                 alert(json.messageError);
                             }
                             if (json.messagesuccess != "" && json.messagesuccess != null) {
                                 alert(json.messagesuccess);
                             }
                             idsOfSelectedRows = [];
                             jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                         },
                         error: function (jqXHR, textStatus, errorThrown) {
                             $("body").removeClass("loading");
                            alert('Error al generar las visitas.' + errorThrown);
                             
                         }
                    });

                     idsOfSelectedRows = [];
                     jQuery("#gridGenerarVisitas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                 }
             }
         }
     }
 }

 function fnCrearClientLayerVisitaTerrenoCarteraGestor() {
     var optionSelected = $("#frmVisitaTerrenoCarteraGestor select[id=LstGestor]").find("option:selected");
     var imei = optionSelected.val().split('|')[1];
     var gestorid = optionSelected.val().split('|')[0]
     var userGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=userGeoGestion]').val()
     var passGeoGestion = $('#frmVisitaTerrenoCarteraGestor input[id=passGeoGestion]').val()
     if ($('#frmVisitaTerrenoCarteraGestor').valid()) {
         if (imei != "" && imei != null) {
             var carteraNombre = $('#frmVisitaTerrenoCarteraGestor input[id=CarteraNombre]').val();
             var carteraDescripcion = $('#frmVisitaTerrenoCarteraGestor input[id=Descripcion]').val();
             var postData = {
                 gestorId: gestorid,
                 userGeo: userGeoGestion,
                 passGeo: passGeoGestion,
                 imei: imei,
                 carteraNombre: carteraNombre,
                 carteraDescripcion: carteraDescripcion
             };
             $.ajax({
                 type: 'POST',
                 url: "/Cartera/CrearClientLayerVisitaTerrenoCarteraGestor",
                 dataType: "json",
                 contentType: "application/json; charset=utf-8",
                 async: true,
                 beforeSend: function () { $("body").addClass("loading"); },
                 data: JSON.stringify(postData),
                 success: function (data, textStatus, xhr) {
                     $("body").removeClass("loading");
                     alert(data);
                 },
                 error: function (jqXHR, textStatus, errorThrown) {
                    $("body").removeClass("loading");
                    alert("Ha ocurrido un problema. " + errorThrown);
                      
                 },
                 complete: function () {
                     $("body").removeClass("loading");
                     $('#ppCarteraVisitaGestor').dialog('close');
                     jQuery("#gridVisitaTerrenoCarteraGestores").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                 },
             });
             
         } else {
             alert("el Gestor no tiene registrado el imei")
         }
     }
 }

 function VerMapaVisitaTerrenoFormulario(cellvalue, options, rowdata) {
     if (cellvalue != '') {
         return "<div align='center'><button style=\"vertical-align:middle\" onclick=\" fnMapaVisitasTerrenoGPS('" + rowdata[12].split(',')[0].trim() + "','" + rowdata[12].split(',')[1].trim() + "','" + rowdata[11] + "');\"  class=\"search\"><img width=\"20px\" height=\"20px\" alt=\"Documento\" title=\"Ver\" src=\"/Images/botones/Search.png\"></button></div>";
     } else {
         return "";
     }
 }

// on-off rol

 function fnInterruptorRol(cellvalue, options, rowobject) {
     //alert(cellvalue);
     var button = '';
     if (cellvalue == "N") {
         button = '<button type="button" class="proceso-on"  onclick="fnBloquearRolC(\'' + rowobject + '\')" ></button>';
     } else {
         button = '<button type="button" class="proceso-off" onclick="fnBloquearRolC(\'' + rowobject + '\')" ></button>';
     }
     return button;
 }

 function fnBloquearRolC(rowobject) {
     var data = rowobject.split(',');
     var bloquearRol;
     if (data[4] == 'N') {
         bloquearRol=true;
     } else {
         bloquearRol=false;
     }
     $.ajax({
         type: 'POST',
         url: "/Cartera/BloquearRol", // we are calling json method
         dataType: 'json',
         async: false,
         data: { Rolid: data[0], BloquearRol: bloquearRol },
         success: function (data) {
             if (data > 0) {
                fnBuscarRolDeudor();
             }
         },
         error: function (ex) {
             alert('Error al (des)bloquear el Rol.');
         }

     });
 }

//terceros
 function fnBuscarTercerosDocumentos() {
     if ($("#Pclid").val() != '' && $("#Ctcid").val() != '') {
         var newUrl = "/Cartera/ListarTerceros/?"
         newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();

         jQuery("#gridTerceros").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        
     }
 }
 function fnMostrarAlertaercerosDocumentos() {
     var count = jQuery("#gridTerceros").getGridParam("records");
     if (count > 0) {
         $("#notTerceros").text(count);
         $("#notTerceros").addClass("nav-counter nav-counter-orange");
     } else {
         $("#notTerceros").text("");
         $("#notTerceros").removeClass("nav-counter nav-counter-orange");
     }
 }

//
  function UpdateIdsOfSelectedRowsVisita(id, isSelected) {
     var index = $.inArray(id, idsOfSelectedRows);
     
     if (!isSelected && index >= 0) {

         idsOfSelectedRows.splice(index, 1); // remove id from the list
         var $check = $('#gridAprobarVisitas tr[id^="' + id + '"]').find('input:checkbox:first');
         //alert($check.prop("checked"));
         $check.prop("checked", false);
       
     } else if (index < 0) {
         idsOfSelectedRows.push(id);
         var $check = $('#gridAprobarVisitas tr[id^="' + id + '"]').find('input:checkbox:first');
         $check.prop("checked", true);
     }
 }
 function fnOnSelectAllVisita(aRowids, status) {
     $('#MontoVisita').val('');
     var i, count, id;
     for (i = 0, count = aRowids.length; i < count; i++) {
         id = aRowids[i];
        // UpdateMontoVisitaOfSelectedRows(id, status);
         UpdateIdsOfSelectedRowsVisita(id, status);
     }
 }

 function fnOnLoadCompleteVisita(grilla) {
     var $grid = jQuery("#" + grilla.id), rows = $grid[0].rows, cRows = rows.length,
     iRow, rowId, row, cellsOfRow, iCol;
     
     var i, count;
     for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $grid.setSelection(idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
         //var $check = $('#' + grilla.id + ' tr[id^="' + idsOfSelectedRows[i] + '"]').find('input:checkbox:first');
     }
 }
//Castigo Devolucion
 function fnBuscarDocumentosCastigoDevolucion() {
     var optionSelected = $("#frmCastigoDevolucion select[id=Estado]").find("option:selected");
    
     var estado = optionSelected.val();
     if ($("#Pclid").val() != '' && $("#Pclid").val() != 0) {

         if (estado != '') {
             var r = $("#ppDocumentosDeudor").dialog();
             $('#ppDocumentosDeudor').dialog('open');
             $('#frmDocumentosDeudores').reset();
             $("#gridDocumentosDeudor").jqGrid('clearGridData');
         }
         else {
             showAlert("Debe igresar el estado del documento!", "warning");
             setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
         }
         
     } else {
         showAlert("Debe igresar el cliente!", "warning");
         setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
         
     }
 }

 function showAlert(message, claseAlerta) {
     switch (claseAlerta) {
         case "success":
             $('#alert').html("<div class='alert-message alert-message-success'><span class='closebtn'>&times;</span><h4>Mensaje de Exito</h4><p>" + message + "</p></div>");
             break;
         case "error":
             $('#alert').html("<div class='alert-message alert-message-danger'><span class='closebtn'>&times;</span><h4>Mensaje de Error</h4><p>" + message + "</p></div>");
             break;
         case "warning":
             $('#alert').html("<div class='alert-message alert-message-warning'><span class='closebtn'>&times;</span><h4>Mensaje de Alerta</h4><p>" + message + "</p></div>");
             break;
         default:
             $('#alert').html("<div class='alert-message alert-message-info'><span class='closebtn'>&times;</span><h4>Mensaje de Información</h4><p>" + message + "</p></div>");
     }
     $('#alert').show();
 }

 function fnBuscarDocsDeudorSelect() {
     var optionSelected = $("#frmCastigoDevolucion select[id=Estado]").find("option:selected");

     var estado = optionSelected.val();
     if ($("#Pclid").val() != 0 && $("#Ctcid").val() != 0) {
         var newUrl = "/Cartera/GetDocumentosDeudorSelect/?"
         newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Estado=" + estado;
         jQuery("#gridDocumentosDeudor").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
     }
 }
//Paginacion Grid Poppup Documentos Deudor
 var idsOfSelectedRowsDocDeudor = [];

 function UpdateIdsOfSelectedRowsDocDeudor(id, isSelected) {
     var index = $.inArray(id, idsOfSelectedRowsDocDeudor);
     if (!isSelected && index >= 0) {
         idsOfSelectedRowsDocDeudor.splice(index, 1); // remove id from the list
         var $check = $('#gridDocumentosDeudor tr[id^="' + id + '"]').find('input:checkbox:first');
         $check.prop("checked", false);
         
     } else if (index < 0) {
         idsOfSelectedRowsDocDeudor.push(id);
         var $check = $('#gridDocumentosDeudor tr[id^="' + id + '"]').find('input:checkbox:first');
         $check.prop("checked", true);
     }
     $("#DocumentosSeleccionados").val(idsOfSelectedRowsDocDeudor);
    
 }

 function fnOnSelectAllDocDeudor(aRowids, status) {
     var i, count, id;
     for (i = 0, count = aRowids.length; i < count; i++) {
         id = aRowids[i];
         UpdateIdsOfSelectedRowsDocDeudor(id, status);
     }
 }

 function fnOnLoadCompleteDocDeudor(grilla, data) {
     var $grid = jQuery("#" + grilla.id)
          
     var i, count;
     for (i = 0, count = idsOfSelectedRowsDocDeudor.length; i < count; i++) {
         $grid.jqGrid('setSelection', idsOfSelectedRowsDocDeudor[i], false);
         //$("#jqg_" + grilla.id + "_" + idsOfSelectedRowsDocDeudor[i]).prop("checked", true);
         var $check = $('#' + grilla.id + ' tr[id^="' + idsOfSelectedRowsDocDeudor[i] + '"]').find('input:checkbox:first');
         $check.prop("checked", true);
     }
     $("#DocumentosSeleccionados").val(idsOfSelectedRowsDocDeudor);
     rowColorJudicialSinRol(grilla);
 }
 function fnloadDocsSelectedCastigoDevolucion() {
     $("#DocumentosSeleccionados").val(idsOfSelectedRowsDocDeudor);
     
     $.ajax({
         type: 'POST',
         url: "/Cartera/GetDocsSelectedCastigoDevolucion/", // we are calling json method
         dataType: 'json',
         async: true,
         data: { DocumentosSeleccionados: JSON.stringify(idsOfSelectedRowsDocDeudor) },
         success: function (resultData) {
             $("#gridDocumentos").jqGrid('clearGridData');
             if (resultData.length > 0) {
                for (var i = 0; i <= resultData.length; i++)
                     $("#gridDocumentos").jqGrid('addRowData', i + 1, resultData[i]);
             }
             var groupingName = 'Deudor';
             $("#gridDocumentos").jqGrid('groupingGroupBy', groupingName, {
                 groupOrder: ['desc'],
                 groupColumnShow: [false],
                 groupCollapse: false,
                 groupSummary: [true],
                 groupText: ['<b>{0} - {1} Documento(s)</b>'],

             });
             //groupText: ['<b>{0}</b> Total Documentos: {Estado}', '{0} Total Documentos: {Estado}'],
             //else{
             //   $("#gridDocumentos").jqGrid('groupingRemove');
             //var thegrid = jQuery("#gridDocumentos")[0];
             //thegrid.addJSONData(resultData)
         },
         error: function (jqXHR, textStatus, errorThrown) {
             //alert(JSON.stringify(jqXHR));
             //alert(textStatus);
             alert('Error al generar comprobante.' + errorThrown);
         }
     });
 }
// paginacion motivo
var idsOfSelectedRowsMotivo = [];

function UpdateIdsOfSelectedRowsMotivo(id, isSelected) {
    var index = $.inArray(id, idsOfSelectedRowsMotivo);
    if (!isSelected && index >= 0) {
        idsOfSelectedRowsMotivo.splice(index, 1); // remove id from the list
    } else if (index < 0) {
        idsOfSelectedRowsMotivo.push(id);
    }
}

function fnOnSelectAllMotivo(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateIdsOfSelectedRowsMotivo(id, status);
    }
}

function fnOnLoadCompleteMotivo(grilla) {
    var  i, count;
    for (i = 0, count = idsOfSelectedRowsMotivo.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRowsMotivo[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsMotivo[i]).prop("checked", true);
    }
}
 
function fnBuscarMotivosCastigoDevolucion() {
    var newUrl = "/Cartera/ListarMotivoCastigoDevolucion"
    jQuery("#gridMotivoCastigoDevolucion").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnAbrirMotivosCastigoDevolucion() {
    var r = $("#ppMotivoCastigo").dialog();
    $('#ppMotivoCastigo').dialog('open');
    fnBuscarMotivosCastigoDevolucion();
}

function fnRefrescarFormularioCastigoDevolucion() {
    $("#frmCastigoDevolucion").reset();
    idsOfSelectedRows = [];
    $("#gridDocumentos").jqGrid('clearGridData');
    idsOfSelectedRowsDocDeudor = [];
    $("#gridDocumentosDeudor").jqGrid('clearGridData');
    $('#NombreRutDeudor').val('');
    $('#Ctcid').val('');
    idsOfSelectedRowsMotivo = [];
    $("#gridMotivoCastigoDevolucion").jqGrid('clearGridData');
    $("#gridDocumentos").jqGrid("footerData", "set", { FechaVencimiento: '', Monto: '', Saldo: '' });
    ActualizaEstado();
}

function fnBotonesGridCastigoDevolucion(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="btn btn-info btn-sm" style="height:20px;width:100px" onclick="fnFormularioAprobarCastigoDevolucion(\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[3] + '\',\'' + rowobject[2] + '\',\'' + rowobject[4] + '\')" >Ver Comprobante</button></div></div></div>';
}
function fnFormularioAprobarCastigoDevolucion(Pclid, tipoComprobanteId, folio, nombreComprobante, cliente) {
    var d = $("#ppAprobarCastigoDevolucion").dialog();
    $('#ppAprobarCastigoDevolucion').dialog('option', 'title', 'Aprobar/Rechazar ' + nombreComprobante + ' Número: ' + folio + ' Cliente: ' + cliente);
    $('#ppAprobarCastigoDevolucion').dialog('open');
    fnRefrescarFormAprobarCastigoDevolucionDetalle();
    $("#ComprobanteIdHidden").val(tipoComprobanteId);
    $("#FolioHidden").val(folio);
    $("#PclidHidden").val(Pclid);
    $("#ComprobanteNombreHidden").val(nombreComprobante);
    var newUrl = "/Cartera/ListarPanelAprobarCastigoDevolucionDetalleGrilla/?"
    newUrl += "tipoComprobante=" + tipoComprobanteId + "&folio=" + folio + "&pclid=" + Pclid;
    jQuery("#gridCastigoDevolucionDetalle").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}
function fnRefrescarFormAprobarCastigoDevolucionDetalle() {
    $("#ComprobanteIdHidden").val('');
    $("#FolioHidden").val('');
    $("#PclidHidden").val('');
    $("#ComprobanteNombreHidden").val('');
}

function fnAprobarCastigoDevolucionComprobante() {
    var idsOfReportes = [];
    $('input[name="reporte"]:checked').each(function () {
        idsOfReportes.push(this.value);
    });
    console.log(idsOfReportes);
  var postData = {
        comprobanteId: $("#ComprobanteIdHidden").val(),
        folio: $("#FolioHidden").val(),
        pclid: $("#PclidHidden").val(),
        nombreComprobante: $("#ComprobanteNombreHidden").val(),
        reportsIs: JSON.stringify(idsOfReportes)
    };
    $.ajax({
        type: 'POST',
        url: "/Cartera/AprobarComprobanteCastigoDevolucion",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        async: true,
        beforeSend: function () { $("body").addClass("loading"); $('#loadingmessage').css('display', 'block'); },
        data: JSON.stringify(postData),
        success: function (data, textStatus, xhr) {
            $("body").removeClass("loading");
            var zip = new JSZip();
            if (data.resultado > 0) {
                $.each(data.zipList, function (index, value) {
                    console.log(value);
                                      
                    var url = value
                    var filename = url.replace(/.*\//g, "");

                    filename = filename.split("/");
                    filename = filename[filename.length - 1]; 
                    zip.file(filename, urlToPromise(url), { binary: true });
                });
                zip.generateAsync({ type: "blob" })
               .then(function callback(blob) {
                   // see FileSaver.js
                   saveAs(blob, $("#PclidHidden").val() + "-" + $("#FolioHidden").val() + "-" + $("#ComprobanteNombreHidden").val() + ".zip");
               });
                showAlert("Aprobado con exito", "success");
                setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
               
            }
            
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("body").removeClass("loading");
            $('#loadingmessage').css('display', 'none');
            showAlert("Ha ocurrido un problema. " + errorThrown, "error");
            setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);
         

        },
        complete: function () {
            $('#loadingmessage').css('display', 'none');
            $("body").removeClass("loading");
            $('#ppAprobarCastigoDevolucion').dialog('close');
            jQuery("#gridCastigoDevolucionesPendientes").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            
        },
    });
}

function getColumnIndexByName(mygrid, columnName) {
    var cm = mygrid.jqGrid('getGridParam', 'colModel');
    for (var i = 0, l = cm.length; i < l; i++) {
        if (cm[i].name === columnName) {
            return i; // return the index
        }
    }
    return -1;
}

function rowColorJudicialSinRol(grilla) {
    var $grid = jQuery("#" + grilla.id)
    var icolEstado = getColumnIndexByName($grid, 'EstadoCpbt'),
        cRows = $grid[0].rows.length, iRow, row, className;
    var icolRolId = getColumnIndexByName($grid, 'RolId');
    for (iRow = 0; iRow < cRows; iRow++) {
        row = $grid[0].rows[iRow];
        className = row.className;
       
        if ($.inArray('jqgrow', className.split(' ')) > 0) { // $(row).hasClass('jqgrow')
            var colEstado = $(row.cells[icolEstado]).text();
            var colRolId = $(row.cells[icolRolId]).text();
            if (colEstado == 'J' && colRolId == 0)
                if ($.inArray('rowWarningSinRolEnJudicial', className.split(' ')) === -1)
                    row.className = className + ' rowWarningSinRolEnJudicial';
        }
    }
}

function fnMostrarIngresoMotivoRechazo() {
    //alert(id);
    var d = $("#ppMotivoRechazoComprobante").dialog();
    $('#ppMotivoRechazoComprobante').dialog('open');

}
function fnRechazarComprobanteCastigoDevolucion() {
    if ($("#MotivoRechazo").val() != '') {
        $.ajax({
            type: 'POST',
            url: "/Cartera/RechazarComprobanteCastigoDevolucion/",
            dataType: 'json',
            async: true,
            data: {
                comprobanteId: $("#ComprobanteIdHidden").val(),
                folio: $("#FolioHidden").val(),
                pclid: $("#PclidHidden").val(),
                motivo: $("#MotivoRechazo").val()
            },
            success: function (data) {
                if (data > 0) {
                    showAlert("El Comprobante fue rechazado", "success");
                    setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);

                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("body").removeClass("loading");
                showAlert("Ha ocurrido un problema. " + errorThrown, "error");
                setTimeout(function () { $('#alert').hide("scale", 500) }, 5000);


            },
            complete: function () {
                $("body").removeClass("loading");
                $('#ppMotivoRechazoComprobante').dialog('close');
                $('#ppAprobarCastigoDevolucion').dialog('close');
                jQuery("#gridCastigoDevolucionesPendientes").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    } else {
        alert("Ingrese el motivo del rechazo");
    }
    
}

//Carga Cocha
function fnCargarArchivoCocha() {
    var newUrl = "/Cartera/ProcesoCargaCocha/"
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
// bienes
function ListarBienes() {
    if ($("#Ctcid").val() != "") {
        var newUrl = "/Cartera/ListarBienesRaicesGrilla/?"
        newUrl += "ctcid=" + $("#Ctcid").val();
        jQuery("#gridBienesRaices").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
       
        var newUrlVeh = "/Cartera/ListarBienesVehiculosGrilla/?"
        newUrlVeh += "ctcid=" + $("#Ctcid").val();
        jQuery("#gridBienesVehiculo").jqGrid().setGridParam({ url: newUrlVeh }).trigger('reloadGrid', [{ page: 1 }]);

        $.ajax({
            type: 'POST',
            url: "/Cartera/DetalleBienes/?ctcid=" + $("#Ctcid").val(),
            async: false,
            success: function (data) {
                $("#Observacion").val(data.Observacion);

            }
        });
    }
}

function fnAgregarVehiculo() {
    if ($("#Ctcid").val() != "") {
        var d = $("#ppVehiculo").dialog();
        $("#frmBienVehiculo").reset();
        $('#ppVehiculo').dialog('open');
        $("#imgSubirArchivoComprobante").removeClass("ok").removeClass("error");
        $("#btnSubmitComprobante").removeAttr("disabled");
    }
}
function fnLimpiarCargaComprobante() {
    $("#imgSubirArchivoComprobante").removeClass("ok").removeClass("error");
    $("#btnSubmitComprobante").removeAttr("disabled");
    $('#ArchivoComprobante').val('')
    $('#subirArchivoComprobante').val('')
}

function fnMarcaSeleccionada(controlOrigen, controlDestino){
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Cartera/ListarModeloVehiculo", // we are calling json method
        dataType: 'json',
        async: false,
        data: { marcaId: $(controlOrigen).val() == "" ? 0 : $(controlOrigen).val() },
        // here we are get value of selected country and passing same value as input to json method GetStates.
        success: function (states) {
            $("#" + controlDestino).append('<option value="">-- Seleccione Modelo --</option>');
            $.each(states, function (i, state) {
                $("#"+ controlDestino).append('<option value="' + state.Value + '">' +  
                        state.Text + '</option>');                                                                                                
            });
        },
        error: function (ex) {
            alert('Error al recuperar datos.' + ex);
        }

    });
}
function fnRulesFormVehiculo() {

    $("#frmBienVehiculo input[id=Anio]").rules("add", {
        required: false,
        regx: /^(\b(19|20)\d{2}\b)$/,
        messages: {
            regx: "el año es invalido"
        }
    });
    $("#frmBienVehiculo input[id=Patente]").rules("add", {
        required: true,
        minlength: 4,
        maxlength: 60,
        messages: {
            required: "campo requerido",
            minlength:"Ingrese al menos 4 caracteres"
        }
    });
}
function fnGuardarBienVehiculo() {
    $("#frmBienVehiculo :input[type='checkbox']").each(function () {
        var hdnFldId = "input:hidden[name='" + $(this).attr('id') + "']" ;
        if ($(this)[0].checked)
            $(hdnFldId).val(true);
        else 
            $(hdnFldId).val(false);
     });
    var postData = {};
    $.each($("#frmBienVehiculo").serializeArray(), function (i, field) {
        postData[field.name] = field.value;
    });
    
    if ($('#frmBienVehiculo').valid()) {
        
        $.ajax({
            type: 'POST',
            url: "/Cartera/GuardarBienVehiculo/", // we are calling json method
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ model: postData, ctcid: $("#Ctcid").val() }),
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (resultData) {

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data == -1) {
                        alert('Error al guardar bien, ' + 'ha ocurrido un error interno');
                    } else {
                        alert('registro guardado con éxito');
                        $('#ppVehiculo').dialog('close');
                        jQuery("#gridBienesVehiculo").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al guardar bien. ' + 'Ha ocurrido un error interno');
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar bien.' + ex);
                
            }

        });
        
    }
}

function fnDoubleClickGridVehiculo(rowid) {
    $("#frmBienVehiculo").reset();
    $("#imgSubirArchivoComprobante").removeClass("ok").removeClass("error");
    $("#btnSubmitComprobante").removeAttr("disabled");
    var $grid = jQuery("#gridBienesVehiculo")
    var rowData = $grid.getRowData(rowid);
    console.log(rowData)
    $("#VehiculoId").val(rowData['VehiculoId'])
   
    $('#frmBienVehiculo select[id=MarcaId]').val(rowData['MarcaId']);
    fnMarcaSeleccionada($('#frmBienVehiculo select[id=MarcaId]'), 'ModeloId');
    $('#frmBienVehiculo select[id=ModeloId]').val(rowData['ModeloId']);
    $('#frmBienVehiculo input[id=Patente]').val(rowData['Patente']);
    $('#frmBienVehiculo input[id=Anio]').val(rowData['Anio'] == 0 ? '' : rowData['Anio']);
       
    $("#frmBienVehiculo input[id=Propietario]").prop('checked', rowData['Propietario'] === 'true')
    $("#frmBienVehiculo input[id=Embargo]").prop('checked', rowData['Embargo'] === 'true')
    $("#frmBienVehiculo input[id=Prenda]").prop('checked', rowData['Prenda'] === 'true')
    $('#ppVehiculo').dialog('open');

}

function fnAgregarBienesRaices() {
    if ($("#Ctcid").val() != "") {
        var d = $("#ppBienesRaices").dialog();
        $("#frmBienPropiedad").reset();
        $('#ppBienesRaices').dialog('open');
        $("#imgSubirArchivoCertificado").removeClass("ok").removeClass("error");
        $("#btnSubmitCertificado").removeAttr("disabled");
    }
}

function fnRulesFormPropiedad() {

    $("#frmBienPropiedad input[id=Anio]").rules("add", {
        required: true,
        regx: /^(\b(19|20)\d{2}\b)$/,
        messages: {
            required: "campo requerido",
            regx: "el año es invalido"
        }
    });
    $("#frmBienPropiedad input[id=Rol]").rules("add", {
        required: true,
        minlength: 4,
        maxlength: 60,
        messages: {
            required: "campo requerido",
            minlength: "Ingrese al menos 4 caracteres"
        }
    });
    $("#frmBienPropiedad input[id=Foja]").rules("add", {
        required: true,
        minlength: 4,
        maxlength: 60,
        messages: {
            required: "campo requerido",
            minlength: "Ingrese al menos 4 caracteres"
        }
    });
}
function fnLimpiarCargaCertificado() {
    $("#imgSubirArchivoCertificado").removeClass("ok").removeClass("error");
    $("#btnSubmitCertificado").removeAttr("disabled");
    $('#ArchivoCertificado').val('')
    $('#subirArchivoCertificado').val('')
}

function fnDoubleClickGridBienesRaices(rowid) {
    $("#frmBienPropiedad").reset();
    $("#imgSubirArchivoCertificado").removeClass("ok").removeClass("error");
    $("#btnSubmitCertificado").removeAttr("disabled");
    var $grid = jQuery("#gridBienesRaices")
    var rowData = $grid.getRowData(rowid);
    console.log(rowData)
    $("#BienesRaicesId").val(rowData['BienesRaicesId'])

    $('#frmBienPropiedad select[id=ConservadorId]').val(rowData['ConservadorId']);
  
    $('#frmBienPropiedad input[id=Rol]').val(rowData['Rol']);
    $('#frmBienPropiedad input[id=Foja]').val(rowData['Foja']);
    $('#frmBienPropiedad input[id=Anio]').val(rowData['Anio'] == 0 ? '' : rowData['Anio']);
    $('#frmBienPropiedad input[id=Direccion]').val(rowData['Direccion']);
    $("#frmBienPropiedad input[id=Propietario]").prop('checked', rowData['Propietario'] === 'true')
    $("#frmBienPropiedad input[id=EvaluoFiscal]").prop('checked', rowData['EvaluoFiscal'] === 'true')
    $("#frmBienPropiedad input[id=Verificado]").prop('checked', rowData['Verificado'] === 'true')
    $("#frmBienPropiedad input[id=Hipotecado]").prop('checked', rowData['Hipotecado'] === 'true')
    $("#frmBienPropiedad input[id=Embargo]").prop('checked', rowData['Embargo'] === 'true')
    
    $('#ppBienesRaices').dialog('open');

}

function fnGuardarBienesRaices() {
    $("#frmBienPropiedad :input[type='checkbox']").each(function () {
        var hdnFldId = "input:hidden[name='" + $(this).attr('id') + "']";
        if ($(this)[0].checked)
            $(hdnFldId).val(true);
        else 
            $(hdnFldId).val(false);
    });
    var postData = {};
    $.each($("#frmBienPropiedad").serializeArray(), function (i, field) {
        postData[field.name] = field.value;
    });

    if ($('#frmBienPropiedad').valid()) {

        $.ajax({
            type: 'POST',
            url: "/Cartera/GuardarBienPropiedad/", // we are calling json method
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ model: postData, ctcid: $("#Ctcid").val() }),
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (resultData) {

                if (resultData.success) {
                    $("body").removeClass("loading");
                    if (resultData.data == -1) {
                        alert('Error al guardar bien, ' + 'ha ocurrido un error interno');
                    } else {
                        alert('registro guardado con éxito');
                        $('#ppBienesRaices').dialog('close');
                        jQuery("#gridBienesRaices").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al guardar bien. ' + 'Ha ocurrido un error interno');
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al guardar bien.' + ex);

            }

        });

    }
}

function fnLinkVerArchivo(cellvalue, options, rowobject) {
    var extension = cellvalue.split('.');
    if (extension.length > 1) 
        return "<a href=\"javascript:fnMuestraCertificado('" + cellvalue + "')\" > <u> Ver Documento </ u> </a>"
    else
        return "Sin Documento"
  
}

function fnMuestraCertificado(url) {
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
               
            } else if (ext.indexOf("do?TIP_Documento") > -1) {
                $('#ppDocto').html('<object data="' + url + '" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="' + url + '">.</a></p></object>');
            } else {
                $('#ppDocto').html('El archivo de extesion ' + ext + ' no puede ser visualizado');
            }
    }
    $('#ppDocto').dialog('open');
}

