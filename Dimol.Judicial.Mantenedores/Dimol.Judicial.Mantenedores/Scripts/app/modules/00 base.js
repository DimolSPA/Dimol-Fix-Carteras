/*******************************************
00                  BASE
*******************************************/
    var totalMontoAsignado = 0;
    var totalSaldoAsignado = 0;
    var totalMontoPorAsignar = 0;
    var totalSaldoPorAsignar = 0;

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

    function PaisSeleccionado(controlOrigen, controlDestino) {
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
                url: "/Judicial/GrabarEnte",
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

    function fnRolSeleccionado(id) {
        var url_string = window.location.href
        var url = new URL(url_string);

        window.location.href = "/Judicial/Rol/?idd=" + id + "&tipoComp=" + url.searchParams.get("tipoComp");
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
                if (data.success) {
                    fnRefreshGridEstados();
                    $('#ppHistorial').dialog('close');
                    var rolidvisto = data.RolId;

                    $.ajax(
                        {
                            type: 'POST',
                            url: "/Judicial/GetDatosRol?idd=" + rolidvisto,
                            success: function (data, textStatus) {
                                console.log(data);
                                $("#MateriaJudicial").val(data.MateriaJudicial);
                                $("#Estado").val(data.Estado)
                                if (data.IdMateriaJudicial == 3 || data.IdMateriaJudicial == 9 || data.IdMateriaJudicial == 10) {
                                    $('#tabGestionRol').tabs('enable', 1);
                                    jQuery("#gridRolLiquidacionGrilla").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                                } else {
                                    $('#tabGestionRol').tabs('disable', 1);
                                }
                            }
                        });

                }
                else {
                    alert(data.message);
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
                // states contains the JSON formatted list
                // of states passed from the controller
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
        $.ajax({
            type: 'POST',
            url: "/Judicial/ActualizarDocumentosRol",
            dataType: 'json',
            async: false,
            data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val(), DocumentosAsignar: agregar, DocumentosEliminar: eliminar },
            success: function (data) {
                if (data > 0) {
                    jQuery("#gridDocSinAsignar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    jQuery("#gridDocAsignado").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
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
        $('#loading-gif').show();

        if ($('#Ctcid').val() == "" || $('#Pclid').val() == "" || $('#Rol').val() == "" || $("#Tribunal").val() == "" || $("#TipoCausa").val() == "") {
            alert("Debe ingresar todos los datos mandatorios.");
        } else {
            var edita = $("#Rolid").val();
            var url_string = window.location.href;
            var url = new URL(url_string);
            var tipoComp = url.searchParams.get("tipoComp");

            $.ajax({
                type: 'POST',
                url: "/Judicial/GuardarRol",
                dataType: 'json',
                async: true,
                beforeSend: function () {
                    $("body").addClass("loading");
                },
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
                    ActualizarRolPoderJudicial: $("#ActualizarRolPoderJudicial").prop("checked"),
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
                    InteresDemanda: $("#InteresDemanda").val(),

                    tipoComp: tipoComp
                },
                success: function (RolId) {
                    if (RolId != -1) {
                        $("#Rolid").val(RolId);

                        if (edita > 0) {
                            //CONFIGURACION DE PARAMETROS PARA GRID RELOAD
                            //Parámetro para grid DocumentosPorAsignar
                            var newUrl = "/Judicial/GetDocumentosPorAsignar?";
                            newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val();
                            jQuery("#gridDocSinAsignar").jqGrid().setGridParam({ url: newUrl });

                            //Parámetro para grid DocumentosAsignados
                            newUrl = "/Judicial/GetDocumentosAsignados?";
                            newUrl += "Rolid=" + RolId;
                            jQuery("#gridDocAsignado").jqGrid().setGridParam({ url: newUrl });

                            //Parámetro para grid RolEntes
                            newUrl = "/Judicial/GetRolEntes?";
                            newUrl += "Rolid=" + RolId;
                            jQuery("#gridEnte").jqGrid().setGridParam({ url: newUrl });

                            //Parámetro para grid Demandados
                            newUrl = "/Judicial/GetDemandados?"
                            newUrl += "Rolid=" + RolId;
                            jQuery("#gridDemandados").jqGrid().setGridParam({ url: newUrl });

                            //Guardado de documentos
                            if (tipoComp == "P") {
                                var agregar = $("#gridDocSinAsignarPrevisional").jqGrid('getGridParam', 'selarrrow');
                                var eliminar = $("#gridDocAsignadoPrevisional").jqGrid('getGridParam', 'selarrrow');

                                fnGuardarDocumentosPrevisional(JSON.stringify(agregar), JSON.stringify(eliminar));
                            } else {
                                var agregar = $("#gridDocSinAsignar").jqGrid('getGridParam', 'selarrrow');
                                var eliminar = $("#gridDocAsignado").jqGrid('getGridParam', 'selarrrow');
                                fnGuardarDocumentos(JSON.stringify(agregar), JSON.stringify(eliminar));
                            }

                            fnCargarAsociados();

                            //RECARGA DE LOS GRIDs
                            //Recarga grid DocumentosPorAsignar
                            jQuery("#gridDocSinAsignar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            //Recarga grid DocumentosAsignados
                            jQuery("#gridDocAsignado").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            fnRefreshGridEstados();

                            //Recarga grid RolEntes
                            jQuery("#gridEnte").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            //Recarga grid Demandados
                            jQuery("#gridDemandados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            fnCargarAsociados(); //Verificar Funcion

                            $("#tabRol").show();

                            fnCargarTotales();
                        } else {
                            var url_string = window.location.href
                            var url = new URL(url_string);

                            if (url_string.indexOf("?") > 0) {
                                url_string = url_string.substring(0, url_string.indexOf("?"));
                            }

                            url_string += "?idd=" + RolId;
                            url_string += "&tipoComp=" + tipoComp;
                            window.location.replace(url_string);
                        }

                        $("body").removeClass("loading");
                    } else {
                        $("body").removeClass("loading");

                        alert('Error al guardar Rol.');
                    }

                    $('#loading-gif').hide();
                }
            });
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
            url: "/Judicial/ListarAsociados",
            dataType: 'json',
            async: false,
            data: {
                ctcid: $("#Ctcid").val() == "" ? 0 : $("#Ctcid").val()
            },
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

    function fnBtnBorradores() {
        $('#ppBorradores').dialog('open');
        var editor = CKEDITOR.instances['editor1'];
        if (editor) {
            editor.destroy(true);
        }
        CKEDITOR.replace('editor1');

        fnBorradorSeleccionado();

        return false;
    }

    function fnBtnAsociados() {
        $('#ppAsociados').dialog('open');

        return false;
    }

    function handleCKEditorPost() {
        var htmlData = CKEDITOR.instances.editor1.getData();

        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarBorrador",
            dataType: 'json',
            async: false,
            data: {
                Rolid: $("#Rolid").val(),
                Borradores: $("#Borradores").val(),
                HTMLBorrador: htmlData
            },
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
            } else {
                return "";
            }
        } else {
            return "";
        }
    }

   

   
    function fnCargarTotales() {
        var url_string = window.location.href;
        var url = new URL(url_string);
        var tipoComp = url.searchParams.get("tipoComp");

        if (tipoComp == "P") {
            $.ajax({
                type: 'POST',
                url: "/Judicial/GetTotalDocsAsignadosPrevisional",
                dataType: 'json',
                async: false,
                data: { Rolid: $("#Rolid").val() },
                success: function (total) {
                    totalMontoAsignado = total.monto;
                    totalSaldoAsignado = total.saldo;
                }
            });

            $.ajax({
                type: 'POST',
                url: "/Judicial/GetTotalDocsPorAsignarPrevisional",
                dataType: 'json',
                async: false,
                data: { Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val() },
                success: function (total) {
                    totalMontoPorAsignar = total.monto;
                    totalSaldoPorAsignar = total.saldo;
                }
            });
        } else {
            $.ajax({
                type: 'POST',
                url: "/Judicial/GetTotalDocsAsignados",
                dataType: 'json',
                async: false,
                data: { Rolid: $("#Rolid").val() },
                success: function (total) {
                    totalMontoAsignado = total.monto;
                    totalSaldoAsignado = total.saldo;
                }
            });

            $.ajax({
                type: 'POST',
                url: "/Judicial/GetTotalDocsPorAsignar",
                dataType: 'json',
                async: false,
                data: { Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val() },
                success: function (total) {
                    totalMontoPorAsignar = total.monto;
                    totalSaldoPorAsignar = total.saldo;
                }
            });
        }
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
    function rowColor(grilla) {
        var $grid = jQuery("#" + grilla.id)
        var iCol = getColumnIndexByName($grid, 'DiasTranscurso'),
            cRows = $grid[0].rows.length, iRow, row, className;
        console.log(iCol);
        console.log(cRows);
        for (iRow = 0; iRow < cRows; iRow++) {
            row = $grid[0].rows[iRow];
            className = row.className;
            console.log(className);
            if ($.inArray('jqgrow', className.split(' ')) > 0) { // $(row).hasClass('jqgrow')
                var x = $(row.cells[iCol]).text()//.children("input:checked");
                console.log(x);
                //if (x.length>0) {
                //    if ($.inArray('myAltRowClass', className.split(' ')) === -1) {
                //        row.className = className + ' myAltRowClass';
                //    }
                //}
            }
        }
    }
    function TipoAlerta(cellvalue) {
        //console.log(cellvalue);
        var iconMalo = '<span class="ui-state-error" style="border:0"><span class="ui-icon ui-icon-circle-close" style="float: left; margin-right: .3em;"></span></span>';
        var iconAlert = '<span class="ui-icon ui-icon-alert yellow" style="float: left; margin-right: .3em;"></span>';
        var iconBien = '<span class="ui-icon ui-icon-circle-check green" style="float: left; margin-right: .3em;"></span>';

        if (cellvalue < 3) {
            return iconBien;
        }
        else {
            if (cellvalue >= 3 && cellvalue < 5) {
                return iconAlert;
            }
            else {
                return iconMalo;
            }
        }

    }

    function formatAlerta(cellvalue, options, rowObject) {
        return TipoAlerta(cellvalue) +
            "<span>" + cellvalue + "</span>";
        //return "<a href='/page.html?q_id="+ cellvalue +"'> <u> Editar </ u> </a>" +
        //     "<a href='/page2.html?q_id="+ cellvalue +"'> <u> Eliminar </ u> </a>";
    }
    function TipoTrackingDemanda(cellvalue) {
        console.log(cellvalue);
        var iconMalo = '<span class="ui-state-error" style="border:0"><span class="ui-icon ui-icon-circle-close" style="float: left; margin-right: .3em;"></span></span>';
        var iconAlert = '<span class="ui-icon ui-icon-alert yellow" style="float: left; margin-right: .3em;"></span>';
        var iconBien = '<span class="ui-icon ui-icon-circle-check green" style="float: left; margin-right: .3em;"></span>';

        if (cellvalue <= 14) {
            return iconBien;
        }
        else {
            if (cellvalue > 14 && cellvalue <= 18) {
                return iconAlert;
            }
            else {
                return iconMalo;
            }
        }
    }

    function formatTrackingDemanda(cellvalue, options, rowObject) {
        return TipoTrackingDemanda(cellvalue) +
            "<span>" + cellvalue + "</span>";
    }
    function formatPercentage(cellvalue, options, rowObject) {
        return ShowProgress(cellvalue);
    }
    function ShowProgress(cellvalue) {

        return '<div class="progress"><div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="' + cellvalue.replace("%", "") + '" aria-valuemin="0" aria-valuemax="100" style="width:' + cellvalue + ';"><span>' + cellvalue + '</span></div></div>'
    }


    function fnBloquearRolJ(control) {
        if ($(control).hasClass("proceso-on")) {
            $("#BloquearRol").val(true);
            //$(control).removeClass("proceso-on");
            //$(control).addClass("proceso-off");
        } else {
            $("#BloquearRol").val(false);
            //$(control).removeClass("proceso-off");
            //$(control).addClass("proceso-on");
        }
        $.ajax({
            type: 'POST',
            url: "/Judicial/BloquearRol", // we are calling json method
            dataType: 'json',
            async: false,
            data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), BloquearRol: $("#BloquearRol").val() },
            // here we are get value of selected country and passing same value as input to json method GetStates.
            success: function (data) {
                if (data > 0) {
                    if ($(control).hasClass("proceso-on")) {
                        $(control).removeClass("proceso-on");
                        $(control).addClass("proceso-off");
                    } else {
                        $(control).removeClass("proceso-off");
                        $(control).addClass("proceso-on");
                    }
                    location.reload();
                }
            },
            error: function (ex) {
                alert('Error al (des)bloquear el Rol.');
            }

        });
    }
    function formatItemAlerta(cellvalue, options, rowObject) {
        return '<div onclick="fnPanelAlertaTipoReporte(\'' + rowObject[0] + '\',\'' + cellvalue + '\')"style=\"cursor:pointer\"><div>' + cellvalue + '</div></div>'
    }
    function fnPanelAlertaTipoReporte(reporte, tituloReporte) {
        var r = $("#ppPanelAlertaTipoReporte").dialog();
        $('#ppPanelAlertaTipoReporte').dialog('option', 'title', tituloReporte);
        $('#ppPanelAlertaTipoReporte').dialog('open');
        var newUrl = "/Judicial/ListarPanelAlertaTipoReporte/?"
        newUrl += "TipoReporte=" + reporte;
        jQuery("#gridPanelAlertaTipoReporte").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
    function formatItemCliente(cellvalue, options, rowObject) {
        return '<div onclick="fnPanelAlertaReporteAnalisisCliente(\'' + rowObject[0] + '\',\'' + cellvalue + '\')"style=\"cursor:pointer\"><div>' + cellvalue + '</div></div>'
    }
    function fnPanelAlertaReporteAnalisisCliente(pclid, tituloReporte) {
        var r = $("#ppPanelAlertaReporteAnalisisCliente").dialog();
        $('#ppPanelAlertaReporteAnalisisCliente').dialog('option', 'title', tituloReporte);
        $('#ppPanelAlertaReporteAnalisisCliente').dialog('open');
        var newUrl = "/Judicial/ListarPanelAlertaReporteAnalisisCliente/?"
        newUrl += "Pclid=" + pclid;
        jQuery("#gridPanelAlertaReporteAnalisisCliente").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
    function fnExcelPanelDEmandas() {
        var url = "/Judicial/ExportToExcelPanelDemandas";
        window.location.href = url;
    }

    function fnBotonEliminarEstampe(cellvalue, options, rowobject) {
        if ($("#Prf").val() == "1" || $("#Prf").val() == "9" || $("#Prf").val() == "13") {
            return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px"  onclick="fnEliminarEstampe(\'' + rowobject.slice(0, 5) + '\')">Eliminar</button></div></div></div>';
        }
        else {
            return ''
        }
    }

    function fnEliminarEstampe(id) {
        if (confirm("¿Desea eliminar el archivo seleccionado?") == true) {
            var datos = id.split(',');
            var url = "/Judicial/EliminarEstampe/?id=" + datos[0] + '|' + datos[1] + '|' + datos[2] + '|' + datos[3] + '|' + datos[4];
            $.ajax({
                type: 'POST',
                url: url,
                dataType: 'json',
                async: true,

                success: function (data) {
                    alert(data);
                },
                error: function (ex) {
                    alert('Error al eliminar el archivo.' + ex);
                }
            });
            fnBuscarEstampes();
        }
    }

    function fnBuscarEstampes() {
        var newUrl = "/Judicial/GetEstampes/?"
        newUrl += "Pclid=" + $("#Pclid").val() + "&Ctcid=" + $("#Ctcid").val() + "&Rolid=" + $("#Rolid").val()
        jQuery("#gridEstampes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
    }

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

    function comprimirEstampes() {
        var zip = new JSZip();

        $('#gridEstampes').find('tr').find('td').find('a').each(function () {
            var url = $(this).attr("href");

            var filename = url.replace(/.*\//g, "");

            filename = filename.split("\\");
            filename = filename[filename.length - 1];

            zip.file(filename, urlToPromise(url), { binary: true });
        });
        // when everything has been downloaded, we can trigger the dl
        zip.generateAsync({ type: "blob" })
            .then(function callback(blob) {
                // see FileSaver.js
                saveAs(blob, $("#Pclid").val() + "-" + $("#NombreRutDeudor").val() + ".zip");
            });
    }

    //PanelDemanda V2
    function fnCursoDemanda(panelId) {
        var $optionSelected = $('#gridPanelDemandas tr[id="' + panelId + '"] select[id="' + panelId + '_CursoDemanda"]').val();
        fnGuardarCursoDemanda(panelId, $optionSelected);
    }

    function formatCursoDemanda(cellvalue, options, rowObject) {
        return tipoCursoDemanda(cellvalue, rowObject);
    }
    function tipoCursoDemanda(cellvalue, rowObject) {
        var selectSi = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
        var selectNo = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
        var seleccione = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

        var selectSiDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
        var selectNoDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
        var seleccioneDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemanda(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

        if (rowObject[19] == "S") {
            if (cellvalue == "SI")
                return selectSiDisabled;
            else
                return seleccione;
        }
        if (rowObject[19] == "N")
            return seleccioneDisabled;

    }

    function fnGuardarCursoDemanda(PanelId, DemandaCursoSelect) {
        var newUrl = "/Judicial/GuardarCursoDemanda/?"
        var datos = {
            panelId: PanelId,
            cursoDemanda: DemandaCursoSelect,
            motivo: ''
        };

        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            data: JSON.stringify(datos),
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("body").removeClass("loading");
                jQuery("#gridPanelDemandas").jqGrid().trigger('reloadGrid', [{ page: 1 }])

            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("body").removeClass("loading");
                alert('Error al guardar curso de la demanda.' + errorThrown);
            }

        });
    }

    function fnBotonesGridDeudorQuiebra(cellvalue, options, rowobject) {
        return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnFormularioDeudorQuiebra(\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[2] + '\',\'' + rowobject[3] + '\',\'' + rowobject[4] + '\',\'' + rowobject[5] + '\',\'' + rowobject[6] + '\',\'' + rowobject[7] + '\',\'' + rowobject[8] + '\',\'' + rowobject[9] + '\')" >Editar</button></div></div></div>';
    }
    function fnFormularioDeudorQuiebra(tribunalId, tipoCausaId, materiaJodicialId, rut, deudor, rolNumero, tribunal, causa, materia, ctcid) {
        var d = $("#ppDeudorQuiebra").dialog();
        $('#ppDeudorQuiebra').dialog('option', 'title', 'Editar Deudor en Quiebra');
        $('#ppDeudorQuiebra').dialog('open');
        $('#frmDeudorQuiebra input[id=NombreDeudor]').val(deudor);
        $('#frmDeudorQuiebra input[id=RutDeudor]').val(rut);
        $('#frmDeudorQuiebra input[id=Rol]').val(rolNumero);
        $('#frmDeudorQuiebra input[id=TribunalSelect]').val(tribunalId);
        $('#frmDeudorQuiebra input[id=Tribunal]').val(tribunal);
        $('#frmDeudorQuiebra select[id=TipoCausa]').val(tipoCausaId);
        $('#frmDeudorQuiebra select[id=MateriaJudicial]').val(materiaJodicialId);
        $('#btnGuardarQuiebraDeudor').hide();
    }
    function fnAgregarDeudorQuiebra() {
        refrescarFrmDeudorQuiebra();
        var d = $("#ppDeudorQuiebra").dialog();
        $('#btnGuardarQuiebraDeudor').show();
        refrescarFrmDeudorQuiebra();
        $('#ppDeudorQuiebra').dialog('option', 'title', 'Agregar Deudor en Quiebra');
        $('#ppDeudorQuiebra').dialog('open');


    }
    function refrescarFrmDeudorQuiebra() {
        $('#frmDeudorQuiebra input[id=RutDeudor]').val('');
        $('#frmDeudorQuiebra input[id=NombreDeudor]').val('');
        $('#frmDeudorQuiebra input[id=Ctcid]').val('');
        $('#frmDeudorQuiebra input[id=Rol]').val('');
        $('#frmDeudorQuiebra input[id=TribunalSelect]').val('');
        $('#frmDeudorQuiebra input[id=Tribunal]').val('');
        $('#frmDeudorQuiebra select[id=TipoCausa]').val('');
        $('#frmDeudorQuiebra select[id=MateriaJudicial]').val('');
    }
    function fnGuardarDeudorQuiebra() {

        if ($('#RutDeudor').val() == "" || $('#NombreDeudor').val() == "" || $('#Rol').val() == "" || $("#TribunalSelect").val() == "") {
            alert("Debe ingresar todos los datos mandatorios.");
        }
        else {

            $.ajax({
                type: 'POST',
                url: "/Judicial/GuardarDeudorQuiebra/", // we are calling json method
                dataType: 'json',
                async: true,
                beforeSend: function () { $("body").addClass("loading"); },
                data: {
                    Rol: $("#Rol").val(),
                    Tribunal: $("#TribunalSelect").val(),
                    TipoCausa: $("#TipoCausa").val(),
                    MateriaJudicial: $("#MateriaJudicial").val(),
                    RutDeudor: $('#RutDeudor').val(),
                    NombreDeudor: $('#NombreDeudor').val()
                },
                success: function (resultData) {
                    $("body").removeClass("loading");
                    if (resultData == 0) {
                        alert("El deudor ya existe en la quiebra");
                    }
                    if (resultData > 1) {
                        alert("Registro almacenado con exito");
                        $('#ppDeudorQuiebra').dialog('close');
                    }
                    if (resultData < 0) {
                        alert("Error al guardar el registro");
                    }
                    jQuery("#gridDeudorQuiebra").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                }
            });
        }
    }

    function refrescarFrmAvanceDemandaRol() {
        $('#frmAvanceDemandaRol input[id=Rol]').val('');
        $('#frmAvanceDemandaRol select[id=TipoRol]').val('');
        $('#frmAvanceDemandaRol input[id=TribunalSelect]').val('');
        $('#frmAvanceDemandaRol input[id=Tribunal]').val('');
        $('#frmAvanceDemandaRol select[id=TipoCausa]').val('');
        $('#frmAvanceDemandaRol select[id=MateriaJudicial]').val('');
    }

    //PanelQuiebra
    function detailFormatter(cellvalue, options, rowObject) {

        var content = "<form>";
        content += "<fieldset>";
        content += "<legend>Personalia:</legend>";
        content += "Name: <input type='text' value='" + rowObject[3] + "' /><br />";
        content += "Email: <input type='text' /><br />";
        content += "Date of birth: <input type='text' />";
        content += "</fieldset>";
        content += "</form>";

        return content;
    }
    function formatRowQuiebra(cellvalue, options, rowObject) {
        return '<div id="div_' + cellvalue + '" class="ui-icon ui-icon-plus" style="left: @(left)px;"/><script type="text/javascript">$("#div_' + cellvalue + '").click(function () {toggleExpCol("div_' + cellvalue + '", "' + cellvalue + '", "' + rowObject[4] + '");});</script>'
    }
    function toggleExpCol(elementId, row_id, materia) {
        //$('#loadingmessage').css('display', 'block');
        //window.setTimeout($('#loadingmessage').css('display', 'none'), 400);

        var iconElement = $('#' + elementId);
        if (iconElement.hasClass('ui-icon-plus')) {
            iconElement.removeClass('ui-icon-plus');
            iconElement.addClass('ui-icon-minus');
            $("#gridRolLiquidacionGrilla").closest(".ui-jqgrid").find('.loading').show();
            $.ajax(
                {
                    type: 'get',
                    contentType: 'application/json; charset=utf-8',
                    url: "/Judicial/AvancePanelQuiebra?rowId=" + row_id + '&materia=' + materia,
                    success: function (data, textStatus) {
                        //console.log(data);
                        var newTr = $(data);

                        $(newTr).each(function (i) {
                            $(this).attr('isExpanded', false);
                            $(this).attr('parent', row_id);
                        });
                        $($('#gridRolLiquidacionGrilla tr#' + row_id)).attr('isExpanded', true);
                        var $rowSelected = $('#gridRolLiquidacionGrilla tr[id="' + row_id + '"]');
                        $rowSelected.after(newTr);
                        $("#gridRolLiquidacionGrilla").closest(".ui-jqgrid").find('.loading').hide();
                        //console.log($($('#gridRolLiquidacionGrilla tr#' + row_id)));
                        //console.log($rowSelected);
                    }
                });
        }
        else {
            iconElement.removeClass('ui-icon-minus');
            iconElement.addClass('ui-icon-plus');
            var grid = $("#gridRolLiquidacionGrilla").jqGrid();

            var getChildrenNode = function (row_id) {
                var result = [];
                var children = $(grid).find('tr[parent=' + row_id + ']');
                $(children).each(function (i) {
                    if ($(this).attr("isExpanded") == "true") {
                        var chl = getChildrenNode(this.id);
                        $(chl).each(function (i) {
                            result.push(this);
                        });
                    }
                    result.push(this);
                });
                return result;
            };

            var childern = getChildrenNode(row_id);
            $.each(childern, function (index, value) { $(value).remove(); });
        }
    }
    function fnGuardarAvancePanelQuiebra(cellValue) {

        var $solicitante = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] select[id="ComboSolicitante"]').val();
        var $fechaCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecCostasPersonales"]').val();
        var $mtoCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="MtoCostasPersonales"]').val();
        var $fechaIngresoSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecIngresoSolicitud"]').val();
        var $fechaNotificacionSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecNotificacionSolicitud"]').val();
        var $fechaAudienciaInicial = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAudienciaInicial"]').val();
        var $fechaAudienciaPrueba = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAudienciaPrueba"]').val();
        var $fechaAudienciaFallo = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAudienciaFallo"]').val();
        var $fechaResolucionLiquidacion = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecResolucionLiquidacion"]').val();
        var $fechaResolucionLiquidacionBC = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecResolucionLiquidacionBC"]').val();
        var $fechaResolucionReorganizacionBC = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecResolucionReorganizacionBC"]').val();
        var $fechaVerificacion = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecVerificacion"]').val();
        var $fechaAcreditacionPoder = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAcreditacionPoder"]').val();
        var $fechaJuntaConstitutiva = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecJuntaConstitutiva"]').val();
        var $fechaJuntaDeliberativa = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecJuntaDeliberativa"]').val();
        var $comboApruebaRechaza = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] select[id="ComboApruebaRechaza"]').val();
        var $fechaAcuerdo = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAcuerdo"]').val();
        var $fechaVerificadoAcreditado = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecVerificadoAcreditado"]').val();
        var $fechaNomCreditoVerificado = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecNomCreditoVerificado"]').val();
        var $fechaImpugnacion = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecImpugnacion"]').val();
        var $fechaNomCreditoReconocido = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecNomCreditoReconocido"]').val();
        var $fechaSolAntecedente = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecSolAntecedente"]').val();
        var $fechaRecepcionAntecedente = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecRecepcionAntecedente"]').val();
        var $fechaEnvioAntecedente = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecEnvioAntecedente"]').val();
        var $fechaEmisionND = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecEmisionND"]').val();
        var $mtoEmision = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="MtoEmision"]').val();
        var $fechaRepartos = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecRepartos"]').val();
        var $mtoRepartos = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="MtoRepartos"]').val();
        var $fechaDevolucion = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecDevolucion"]').val();
        var $fechaPgoCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecPgoCostasPersonales"]').val();
        var $pgoCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="PgoCostasPersonales"]').val();
        var $fechaAprobacionCtaFinal = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecAprobacionCtaFinal"]').val();
        var $fechaCertificadoIncobrable = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] input[id="fecCertificadoIncobrable"]').val();
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarAvancePanelQuiebra/",
            dataType: 'json',
            async: true,
            data: {
                rolId: $("#Rolid").val(),
                quiebraId: cellValue,
                solicitante: $solicitante,
                mtoCostasPersonales: $mtoCostasPersonales,
                fecCostasPersonales: $fechaCostasPersonales,
                fecIngrSolicitud: $fechaIngresoSolicitud,
                fecNotSolicitud: $fechaNotificacionSolicitud,
                fecAudienciaIni: $fechaAudienciaInicial,
                fecAudienciaPrueba: $fechaAudienciaPrueba,
                fecAudienciaFallo: $fechaAudienciaFallo,
                fecResolLiqui: $fechaResolucionLiquidacion,
                fecResolLiquiBC: $fechaResolucionLiquidacionBC,
                fecResolReorgBC: $fechaResolucionReorganizacionBC,
                fecVerificacion: $fechaVerificacion,
                fecAcreditaPoder: $fechaAcreditacionPoder,
                fecJuntaConsti: $fechaJuntaConstitutiva,
                fecJuntaDelibe: $fechaJuntaDeliberativa,
                statusAcuerdo: $comboApruebaRechaza,
                fecAcuerdo: $fechaAcuerdo,
                fecVerificaAcredita: $fechaVerificadoAcreditado,
                fecNomCreditoVeri: $fechaNomCreditoVerificado,
                fecImpugnacion: $fechaImpugnacion,
                fecNomCreditoRec: $fechaNomCreditoReconocido,
                fecSolAntecedente: $fechaSolAntecedente,
                fecRecepAntecedente: $fechaRecepcionAntecedente,
                fecEnvAntecedente: $fechaEnvioAntecedente,
                fecEmisionND: $fechaEmisionND,
                mtoEmisionND: $mtoEmision,
                fecRepartos: $fechaRepartos,
                MtoRepartos: $mtoRepartos,
                fecDevolucion: $fechaDevolucion,
                pgoCostPersonales: $pgoCostasPersonales,
                fecpgoCostPersonales: $fechaPgoCostasPersonales,
                fecAprobCtaFinal: $fechaAprobacionCtaFinal,
                fecCertiIncobrable: $fechaCertificadoIncobrable
            },
            success: function (data) {
                if (data != -1) {
                    alert('Avance guardado con éxito.');

                } else {
                    alert('Error al guardar Avance Panel Quiebra.');
                }
            }
        });
    }
    function fnLiquidacionCampos(cellValue, valueCombo) {
        var $liCostasValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idCostasValue"]');
        var $liCostas = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idCostas"]');
        var $liIngresoSolicitudValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idIngresoSolicitudValue"]');
        var $liIngresoSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idIngresoSolicitud"]');
        var $liNotificacionSolicitudValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idNotificacionSolicitudValue"]');
        var $liNotificacionSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idNotificacionSolicitud"]');
        var $liAudienciaInicialValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaInicialValue"]');
        var $liAudienciaInicial = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaInicial"]');
        var $liAudienciaPruebaValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaPruebaValue"]');
        var $liAudienciaPrueba = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaPrueba"]');
        var $liAudienciaFalloValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaFalloValue"]');
        var $liAudienciaFallo = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idAudienciaFallo"]');
        var $liResolucionLiquiValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idResolucionLiquiValue"]');
        var $liResolucionLiqui = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idResolucionLiqui"]');

        var $liDevolucionValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idDevolucionValue"]');
        var $liDevolucion = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idDevolucion"]');
        var $liPgoCostasPersonalesValue = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idPgoCostasPersonalesValue"]');
        var $liPgoCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + cellValue + 'Details"] li[id="idPgoCostasPersonales"]');


        if (valueCombo == 'N') {
            $liCostasValue.hide(400);
            $liCostas.hide(400);
            $liIngresoSolicitudValue.hide(400)
            $liIngresoSolicitud.hide(400)
            $liNotificacionSolicitudValue.hide(400)
            $liNotificacionSolicitud.hide(400)
            $liAudienciaInicialValue.hide(400)
            $liAudienciaInicial.hide(400)
            $liAudienciaPruebaValue.hide(400)
            $liAudienciaPrueba.hide(400)
            $liAudienciaFalloValue.hide(400)
            $liAudienciaFallo.hide(400)


            $liDevolucionValue.hide(400)
            $liDevolucion.hide(400)
            $liPgoCostasPersonalesValue.hide(400)
            $liPgoCostasPersonales.hide(400)

        } else {
            $liCostasValue.show(400);
            $liCostas.show(400);
            $liIngresoSolicitudValue.show(400)
            $liIngresoSolicitud.show(400)
            $liNotificacionSolicitudValue.show(400)
            $liNotificacionSolicitud.show(400)
            $liAudienciaInicialValue.show(400)
            $liAudienciaInicial.show(400)
            $liAudienciaPruebaValue.show(400)
            $liAudienciaPrueba.show(400)
            $liAudienciaFalloValue.show(400)
            $liAudienciaFallo.show(400)

            $liDevolucionValue.show(400)
            $liDevolucion.show(400)
            $liPgoCostasPersonalesValue.show(400)
            $liPgoCostasPersonales.show(400)
        }
    }
    function fnExcelPanelQuiebras() {
        var url = "/Judicial/ExportToExcelPanelQuiebras";
        window.location.href = url;

    }
    function fnGuardarPanelQuiebraSindico() {
        $.ajax({
            type: 'POST',
            url: "/Judicial/InsertUpdatePanelQuiebraSindico/",
            dataType: 'json',
            async: true,
            data: {
                rolId: $("#Rolid").val(),
                sindico: $("#Sindico").val(),
                veedor: $("#Veedor").val(),
                interventor: $("#Interventor").val()
            },
            success: function (data) {
                if (data != -1) {
                    alert('guardado con éxito.');

                } else {
                    alert('Error al guardar Registro');
                }
            }
        });
    }

    //Traspaso Avenimiento
    function fnTraspasarAvenimiento(rolId) {
        //Se verifica si se ha guardado el Avenimiento

        $.ajax(
            {
                type: 'POST',
                url: "/Judicial/GetDatosAvenimiento?idd=" + rolId,
                success: function (data, textStatus) {

                    if (data.FechaAvenimiento == '' || data.MontoAvenimiento == '0,00' || data.CuotasAvenimiento == 0 || data.MontoCuotaAvenimiento == '0,00' || data.MontoUltimaCuotaAvenimiento == '0,00' || data.FechaPrimeraCuotaAvenimiento == '' || data.FechaUltimaCuotaAvenimiento == '')
                        alert("Debe guardar el avenimiento, antes de traspasar");
                    else {
                        if ($("#FechaAvenimiento").val() == '' || $("#MontoAvenimiento").val() == '0' || $("#MontoAvenimiento").val() == '' || $("#MontoAvenimiento").val() == '0,00' || data.FechaPrimeraCuotaAvenimiento == '' || data.FechaUltimaCuotaAvenimiento == ''
                            || $("#CuotasAvenimiento").val() == '0' || $("#CuotasAvenimiento").val() == '' || $("#CuotasAvenimiento").val() == '0,00'
                            || $("#MontoCuotaAvenimiento").val() == '0' || $("#MontoCuotaAvenimiento").val() == '' || $("#MontoCuotaAvenimiento").val() == '0,00'
                            || $("#MontoUltimaCuotaAvenimiento").val() == '0' || $("#MontoUltimaCuotaAvenimiento").val() == '' || $("#MontoUltimaCuotaAvenimiento").val() == '0,00')
                            alert("Los datos de Avenimiento son requeridos")
                        else {
                            $.ajax({
                                type: 'POST',
                                url: "/Judicial/InsertarPanelAvenimiento/",
                                dataType: 'json',
                                async: true,
                                data: {
                                    rolId: $("#Rolid").val(),
                                    rolNumero: $("#Rol").val(),
                                    pclid: $("#Pclid").val(),
                                    ctcid: $("#Ctcid").val(),
                                    tribunalId: $("#Tribunal").val()

                                },
                                success: function (data) {
                                    if (data != -1) {
                                        alert('traspasado con éxito.');

                                    } else {
                                        alert('Error al traspasar Registro');
                                    }
                                }
                            });
                        }
                    }
                }
            });
    }

    function fnBotonesGridPanelAvenimiento(cellvalue, options, rowobject) {
        return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="btn btn-info btn-sm" style="height:20px;width:100px" onclick="fnVerAprobarAvenimiento(\'' + cellvalue + '\',\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[2] + '\',\'' + rowobject[5] + '\',\'' + rowobject[6] + '\')" >Ver Documentos</button></div></div></div>';
    }
    function fnVerAprobarAvenimiento(rolId, Rol, cliente, deudor, pclid, ctcid) {
        var d = $("#ppAprobarPanelAvenimiento").dialog();
        $('#ppAprobarPanelAvenimiento').dialog('option', 'title', 'Aprobar Avenimiento ' + '' + ' Rol: ' + Rol + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
        $('#ppAprobarPanelAvenimiento').dialog('open');

        $("#RolIdHidden").val(rolId);
        $("#PclidHidden").val(pclid);
        $("#CtcidHidden").val(ctcid);
        var newUrl = "/Judicial/GetDocumentosAsignados/?"
        newUrl += "Rolid=" + rolId;
        jQuery("#gridDocAvenimiento").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        //Avenimiento
        $.ajax(
            {
                type: 'POST',
                url: "/Judicial/GetDatosAvenimiento?idd=" + rolId,
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                async: true,
                beforeSend: function () { $('#loadingmessage').css('display', 'block'); },
                success: function (data, textStatus) {
                    $("#FechaAvenimiento").val(data.FechaAvenimiento);
                    $("#MontoAvenimiento").val(data.MontoAvenimiento);
                    $("#CuotasAvenimiento").val(data.CuotasAvenimiento);
                    $("#MontoCuotaAvenimiento").val(data.MontoCuotaAvenimiento);
                    $("#MontoUltimaCuotaAvenimiento").val(data.MontoUltimaCuotaAvenimiento);
                    $("#FechaPrimeraCuotaAvenimiento").val(data.FechaPrimeraCuotaAvenimiento);
                    $("#FechaUltimaCuotaAvenimiento").val(data.FechaUltimaCuotaAvenimiento);
                    $("#InteresAvenimiento").val(data.InteresAvenimiento);
                    //if (data.FechaAvenimiento == '' || data.MontoAvenimiento == '0,00' || data.CuotasAvenimiento == 0 || data.MontoCuotaAvenimiento == '0,00' || data.MontoUltimaCuotaAvenimiento == '0,00' || data.FechaPrimeraCuotaAvenimiento == '' || data.FechaUltimaCuotaAvenimiento == '')
                    //    alert("Debe guardar el avenimiento, antes de aprobar el traspaso");
                    //else {

                    //}
                },
                complete: function () {
                    $('#loadingmessage').css('display', 'none');
                }
            });
    }
    function fnAprobarAvenimiento() {

        var postData = {
            rolId: $("#RolIdHidden").val(),
            pclid: $("#PclidHidden").val(),
            ctcid: $("#CtcidHidden").val(),
            fechaAvenimiento: $("#FechaAvenimiento").val(),
            montoAvenimiento: $("#MontoAvenimiento").val(),
            cuotasAvenimiento: $("#CuotasAvenimiento").val(),
            montoCuotaAvenimiento: $("#MontoCuotaAvenimiento").val(),
            montoUltimaCuotaAvenimiento: $("#MontoUltimaCuotaAvenimiento").val(),
            fechaPrimeraCuotaAvenimiento: $("#FechaPrimeraCuotaAvenimiento").val(),
            fechaUltimaCuotaAvenimiento: $("#FechaUltimaCuotaAvenimiento").val(),
            interesAvenimiento: $("#InteresAvenimiento").val()
        };
        $.ajax({
            type: 'POST',
            url: "/Judicial/AprobarAvenimiento",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: true,
            beforeSend: function () { $("body").addClass("loading"); $('#loadingmessage').css('display', 'block'); },
            data: JSON.stringify(postData),
            success: function (data, textStatus, xhr) {
                $("body").removeClass("loading");
                if (data >= 0) {
                    alert("Aprobado con exito");
                } else {
                    if (data == -2) {
                        alert("No hay documentos en el traspaso avenimiento")
                    }
                    else {
                        if (data == -3)
                            alert("Los documentos deben estar en Judicial")
                        else
                            alert("Error al aprobar avenimiento")
                    }
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                $('#loadingmessage').css('display', 'none');
                $("body").removeClass("loading");
                alert("Ha ocurrido un problema. " + errorThrown)

            },
            complete: function () {
                $('#loadingmessage').css('display', 'none');
                $("body").removeClass("loading");
                $('#ppAprobarPanelAvenimiento').dialog('close');
                jQuery("#gridPanelAvenimiento").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

            },
        });
    }

    //Panel Quiebra Version II
    function fnMostrarRepartos(quiebraId) {
        $("#QuiebraIdHidden").val(quiebraId);
        var d = $("#ppPanelQuiebraRepartos").dialog();
        $('#ppPanelQuiebraRepartos').dialog('open');
        var newUrl = "/Judicial/ListarPanelQuiebraRepartos/?"
        newUrl += "quiebraId=" + quiebraId;
        jQuery("#gridRepartosPanelQuiebra").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }
    function fnMostrarIngresoReparto() {
        var d = $("#ppAddReparto").dialog();
        $('#ppAddReparto').dialog('open');
        $('#MtoReparto').val(0);
        $("#fecReparto").val('');
    }
    function formatfloat(n, dp) {
        var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
        while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
        return s.substr(0, i + 3) + r +
            (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
    };
    function fnGrabarPanelQuiebraReparto() {
        if ($("#fecReparto").val() != '' && $("#MtoReparto").val() != '' && $("#MtoReparto").val() != 0) {
            $.ajax({
                type: 'POST',
                url: "/Judicial/InsertPanelQuiebraReparto/",
                dataType: 'json',
                async: true,
                data: {
                    quiebraId: $("#QuiebraIdHidden").val(),
                    fecReparto: $("#fecReparto").val(),
                    MtoReparto: $("#MtoReparto").val()
                },
                success: function (data) {
                    if (data > 0) {
                        alert("Reparto ingresado")

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("body").removeClass("loading");
                    alert("Ha ocurrido un problema. " + errorThrown);

                },
                complete: function () {
                    $("body").removeClass("loading");
                    $('#ppAddReparto').dialog('close');
                    var newUrl = "/Judicial/ListarPanelQuiebraRepartos/?"
                    newUrl += "quiebraId=" + $("#QuiebraIdHidden").val();
                    jQuery("#gridRepartosPanelQuiebra").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                    $.ajax(
                        {
                            type: 'POST',
                            url: "/Judicial/GetDatosPanelQuiebraItem?quiebraId=" + $("#QuiebraIdHidden").val(),
                            dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            async: true,
                            success: function (data, textStatus) {
                                $("#fecRepartos").val(data.FechaReparto);
                                $("#MtoRepartos").val(data.MontoReparto);
                                $('#MtoRepartos').val(formatfloat($('#MtoRepartos').val(), 0));

                            }
                        });
                }
            });
        } else {
            alert("Ingrese los registros");
        }

    }
    function fnPanelQuiebraValidacionesEventos(quiebraId) {
        var $solicitante = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] select[id="ComboSolicitante"]').val();
        var $liCostasValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idCostasValue"]');
        var $liCostas = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idCostas"]');
        var $liIngresoSolicitudValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idIngresoSolicitudValue"]');
        var $liIngresoSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idIngresoSolicitud"]');
        var $liNotificacionSolicitudValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idNotificacionSolicitudValue"]');
        var $liNotificacionSolicitud = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idNotificacionSolicitud"]');
        var $liAudienciaInicialValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaInicialValue"]');
        var $liAudienciaInicial = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaInicial"]');
        var $liAudienciaPruebaValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaPruebaValue"]');
        var $liAudienciaPrueba = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaPrueba"]');
        var $liAudienciaFalloValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaFalloValue"]');
        var $liAudienciaFallo = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idAudienciaFallo"]');
        var $liResolucionLiquiValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idResolucionLiquiValue"]');
        var $liResolucionLiqui = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idResolucionLiqui"]');

        var $liDevolucionValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idDevolucionValue"]');
        var $liDevolucion = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idDevolucion"]');
        var $liPgoCostasPersonalesValue = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idPgoCostasPersonalesValue"]');
        var $liPgoCostasPersonales = $('#gridRolLiquidacionGrilla tr[id="' + quiebraId + 'Details"] li[id="idPgoCostasPersonales"]');
        if ($solicitante == 'N') {
            $liCostasValue.hide(400);
            $liCostas.hide(400);
            $liIngresoSolicitudValue.hide(400)
            $liIngresoSolicitud.hide(400)
            $liNotificacionSolicitudValue.hide(400)
            $liNotificacionSolicitud.hide(400)
            $liAudienciaInicialValue.hide(400)
            $liAudienciaInicial.hide(400)
            $liAudienciaPruebaValue.hide(400)
            $liAudienciaPrueba.hide(400)
            $liAudienciaFalloValue.hide(400)
            $liAudienciaFallo.hide(400)


            $liDevolucionValue.hide(400)
            $liDevolucion.hide(400)
            $liPgoCostasPersonalesValue.hide(400)
            $liPgoCostasPersonales.hide(400)
        }
        if ($solicitante == 'S') {

            $liCostasValue.show(400);
            $liCostas.show(400);
            $liIngresoSolicitudValue.show(400)
            $liIngresoSolicitud.show(400)
            $liNotificacionSolicitudValue.show(400)
            $liNotificacionSolicitud.show(400)
            $liAudienciaInicialValue.show(400)
            $liAudienciaInicial.show(400)
            $liAudienciaPruebaValue.show(400)
            $liAudienciaPrueba.show(400)
            $liAudienciaFalloValue.show(400)
            $liAudienciaFallo.show(400)

            $liDevolucionValue.show(400)
            $liDevolucion.show(400)
            $liPgoCostasPersonalesValue.show(400)
            $liPgoCostasPersonales.show(400)
        }

        $("#fecCostasPersonales").datepicker({
            showButtonPanel: true, maxDate: 0,
            maxDate: 0//,
            //closeText: "Ok",
            //onSelect: function (event) {
            //    event.preventDefault();
            //}
        });
        $("#fecIngresoSolicitud").datepicker({ showButtonPanel: true, maxDate: 0, maxDate: 0 });
        $("#fecNotificacionSolicitud").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAudienciaInicial").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAudienciaPrueba").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAudienciaFallo").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecResolucionLiquidacion").datepicker({ showButtonPanel: true, maxDate: 0 });
        //if ($("#fecResolucionLiquidacion").val() != '') {
        //    $("#fecResolucionLiquidacion").datepicker().datepicker('disable');
        //} else {
        //    $("#fecResolucionLiquidacion").datepicker('enable');
        //}
        $("#fecResolucionLiquidacionBC").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecResolucionReorganizacionBC").datepicker({ showButtonPanel: true, maxDate: 0 });
        $('#MtoCostasPersonales').val(formatfloat($('#MtoCostasPersonales').val(), 0));
        $('#MtoCostasPersonales').keyup(function (event) {
            // skip for arrow keys
            if (event.which >= 37 && event.which <= 40) return;
            // format number
            $(this).val(function (index, value) {
                return value
                    .replace(/^0+/, "")
                    .replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                    ;
            });
        });
        $("#fecVerificacion").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAcreditacionPoder").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecJuntaConstitutiva").datepicker({ showButtonPanel: true, maxDate: 0 });

        $("#fecJuntaDeliberativa").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAcuerdo").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecVerificadoAcreditado").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecNomCreditoVerificado").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecImpugnacion").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecNomCreditoReconocido").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#MtoEmision").prop('disabled', true);
        $("#fecSolAntecedente").datepicker({
            showButtonPanel: true,
            maxDate: 0,
            onSelect: function (e) {
                $("#fecRecepcionAntecedente").datepicker('enable');
            }
        });
        $("#fecRecepcionAntecedente").datepicker({
            showButtonPanel: true,
            maxDate: 0,
            disabled: true,
            onSelect: function (e) {
                $("#fecEnvioAntecedente").datepicker('enable');
            }
        });
        $("#fecEnvioAntecedente").datepicker({
            showButtonPanel: true,
            maxDate: 0,
            disabled: true,
            onSelect: function (e) {
                $("#fecEmisionND").datepicker('enable');
                $("#MtoEmision").prop('disabled', false);
            }
        });
        $("#fecEmisionND").datepicker({
            showButtonPanel: true,
            maxDate: 0,
            disabled: true
        });

        //reglas
        if ($("#fecSolAntecedente").val() != '') {
            $("#fecRecepcionAntecedente").datepicker('enable');
        }
        if ($("#fecRecepcionAntecedente").val() != '') {
            $("#fecEnvioAntecedente").datepicker('enable');
        }
        if ($("#fecEnvioAntecedente").val() != '') {
            $("#fecEmisionND").datepicker('enable');
            $("#MtoEmision").prop('disabled', false);
        }

        $("#fecRepartos").datepicker({ showButtonPanel: true, maxDate: 0, disabled: true });
        $("#fecDevolucion").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecPgoCostasPersonales").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecAprobacionCtaFinal").datepicker({ showButtonPanel: true, maxDate: 0 });
        $("#fecCertificadoIncobrable").datepicker({ showButtonPanel: true, maxDate: 0 });
        $('#MtoEmision').val(formatfloat($('#MtoEmision').val(), 0));
        $('#MtoEmision').keyup(function (event) {

            // skip for arrow keys
            if (event.which >= 37 && event.which <= 40) return;

            // format number

            $(this).val(function (index, value) {
                return value
                    .replace(/^0+/, "")
                    .replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                    ;
            });
        });
        $("#MtoRepartos").prop('disabled', true);
        $('#MtoRepartos').val(formatfloat($('#MtoRepartos').val(), 0));
        $('#MtoRepartos').keyup(function (event) {

            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return value
                    .replace(/^0+/, "")
                    .replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                    ;
            });
        });
        $('#PgoCostasPersonales').val(formatfloat($('#PgoCostasPersonales').val(), 0));
        $('#PgoCostasPersonales').keyup(function (event) {

            if (event.which >= 37 && event.which <= 40) return;

            $(this).val(function (index, value) {
                return value
                    .replace(/^0+/, "")
                    .replace(/\D/g, "")
                    .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                    ;
            });
        });
        $("#ppPanelQuiebraRepartos").dialog({
            autoOpen: false, modal: true, title: "Datos de Repartos", maxWidth: 250,
            maxHeight: 400,
            width: 250,
            height: 400,
        });
    }

    //Panel Monitoreo Demonio
    function fnListarPanelMonitoreoExternoRol(listaZona) {

        var zona = $(listaZona).val() == "" ? 0 : $(listaZona).val()
        if ($("#Pclid").val() != 0 && $("#Ctcid").val() != 0) {
            var newUrl = "/Judicial/ListarPanelMonitoreoExternoRol/?"
            newUrl += "zonaId=" + zona;
            jQuery("#gridMonitoreoExternoRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        }
    }
    function fnOnLoadCompleteExternoDemandas(grilla, data) {
        var $grid = jQuery("#" + grilla.id),
            sumSaldoCartera = $grid.jqGrid("getCol", "SaldoCartera", false, "sum");
        sumSaldoSinDemanda = $grid.jqGrid("getCol", "SaldoSinDemanda", false, "sum");
        sumPorSaldoSinDemanda = sumSaldoSinDemanda * 100 / sumSaldoCartera;
        sumSaldoDemandado = $grid.jqGrid("getCol", "SaldoDemandado", false, "sum");
        sumPorSaldoDemandado = sumSaldoDemandado * 100 / sumSaldoCartera;
        sumSaldoDemandadoDosAnios = $grid.jqGrid("getCol", "SaldoDemandadoDosAnios", false, "sum");
        sumPorSaldoDemandadoDosAnios = sumSaldoDemandadoDosAnios * 100 / sumSaldoCartera;
        $grid.jqGrid("footerData", "set", {
            Cliente: "Total General:",
            SaldoCartera: sumSaldoCartera,
            SaldoSinDemanda: sumSaldoSinDemanda,
            PorSaldoSinDemanda: sumPorSaldoSinDemanda.toFixed(2).toString() + '%',
            SaldoDemandado: sumSaldoDemandado,
            PorSaldoDemandado: sumPorSaldoDemandado.toFixed(2).toString() + '%',
            SaldoDemandadoDosAnios: sumSaldoDemandadoDosAnios,
            PorSaldoDemandadoDosAnios: sumPorSaldoDemandadoDosAnios.toFixed(2).toString() + '%'
        });
    }
    function fnOnLoadCompleteSiiClientes(grilla, data) {
        var $grid = jQuery("#" + grilla.id),
            sumSaldoCartera = $grid.jqGrid("getCol", "SaldoCartera", false, "sum");
        sumSaldoVerde = $grid.jqGrid("getCol", "SaldoVerde", false, "sum");
        sumPorSaldoVerde = sumSaldoVerde * 100 / sumSaldoCartera;
        sumSaldoAmarillo = $grid.jqGrid("getCol", "SaldoAmarillo", false, "sum");
        sumPorSaldoAmarillo = sumSaldoAmarillo * 100 / sumSaldoCartera;
        sumSaldoRojo = $grid.jqGrid("getCol", "SaldoRojo", false, "sum");
        sumPorSaldoRojo = sumSaldoRojo * 100 / sumSaldoCartera;
        $grid.jqGrid("footerData", "set", {
            Cliente: "Total General:",
            SaldoCartera: sumSaldoCartera,
            SaldoVerde: sumSaldoVerde,
            PorSaldoVerde: sumPorSaldoVerde.toFixed(2).toString() + '%',
            SaldoAmarillo: sumSaldoAmarillo,
            PorSaldoAmarillo: sumPorSaldoAmarillo.toFixed(2).toString() + '%',
            SaldoRojo: sumSaldoRojo,
            PorSaldoRojo: sumPorSaldoRojo.toFixed(2).toString() + '%'
        });
    }

    function fnOnLoadCompleteInternoClientes(grilla, data) {
        var $grid = jQuery("#" + grilla.id),
            sumTotalCausas = $grid.jqGrid("getCol", "TotalCausas", false, "sum");
        sumActualizadasCount = $grid.jqGrid("getCol", "ActualizadasCount", false, "sum");
        sumNoActualizadasCount = $grid.jqGrid("getCol", "NoActualizadasCount", false, "sum");
        sumPorcentaje = sumActualizadasCount * 100 / sumTotalCausas;
        sumACount = $grid.jqGrid("getCol", "ACount", false, "sum");
        sumBCount = $grid.jqGrid("getCol", "BCount", false, "sum");
        sumCCount = $grid.jqGrid("getCol", "CCount", false, "sum");
        sumDCount = $grid.jqGrid("getCol", "DCount", false, "sum");

        $grid.jqGrid("footerData", "set", {
            Cliente: "Total General:",
            TotalCausas: sumTotalCausas,
            ActualizadasCount: sumActualizadasCount,
            NoActualizadasCount: sumNoActualizadasCount,
            Porcentaje: sumPorcentaje.toFixed(2).toString() + '%',
            ACount: sumACount,
            BCount: sumBCount,
            CCount: sumCCount,
            DCount: sumDCount
        });
    }

    //Alerta Panel Quiebra
    function fnCargarOrgChartPanelQuiebra() {
        $.ajax({
            type: 'POST',
            url: "/Judicial/ListarPanelQuiebraGrafico", // we are calling json method
            dataType: 'json',
            async: false,
            data: {},
            success: function (chartsdata) {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Nombre');
                data.addColumn('string', 'Padre');
                data.addColumn('string', 'ToolTip');
                for (var i = 0; i < chartsdata.length; i++) {
                    var contenido;
                    contenido = '<div style="color:red; font-style:italic">' + chartsdata[i].Item + '</div>' +
                        '<div style="color:blue; font-style:italic">' + chartsdata[i].Total + '</div>' +
                        '<span class="badge badge-info">' + chartsdata[i].MtoAsignado + '</span>';
                    data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Parent, chartsdata[i].Item]);

                }
                var chart = new google.visualization.OrgChart(document.getElementById('chartPanelQuiebra'));
                //data.setProperty(0, 0, { style: 'width:50px' });
                chart.draw(data, { allowHtml: true });

            },
            error: function (ex) {
                alert('Error al cargar el grafico.');
            }

        });
    }

    function TipoTrackingPublicacionLiquidacion(cellvalue) {
        console.log(cellvalue);
        var iconMalo = '<span class="ui-state-error" style="border:0"><span class="ui-icon ui-icon-circle-close" style="float: left; margin-right: .3em;"></span></span>';
        var iconAlert = '<span class="ui-icon ui-icon-alert yellow" style="float: left; margin-right: .3em;"></span>';
        var iconBien = '<span class="ui-icon ui-icon-circle-check green" style="float: left; margin-right: .3em;"></span>';

        if (cellvalue <= 14) {
            return iconBien;
        }
        else {
            if (cellvalue > 14 && cellvalue <= 25) {
                return iconAlert;
            }
            else {
                return iconMalo;
            }
        }
    }

    function formatTrackingPublicacionLiquidacion(cellvalue, options, rowObject) {
        return TipoTrackingDemanda(cellvalue) +
            "<span>" + cellvalue + "</span>";
    }

    function TipoTrackingPublicacionReorganizacion(cellvalue) {
        console.log(cellvalue);
        var iconMalo = '<span class="ui-state-error" style="border:0"><span class="ui-icon ui-icon-circle-close" style="float: left; margin-right: .3em;"></span></span>';
        var iconAlert = '<span class="ui-icon ui-icon-alert yellow" style="float: left; margin-right: .3em;"></span>';
        var iconBien = '<span class="ui-icon ui-icon-circle-check green" style="float: left; margin-right: .3em;"></span>';

        if (cellvalue <= 2) {
            return iconBien;
        }
        else {
            if (cellvalue > 2 && cellvalue <= 5) {
                return iconAlert;
            }
            else {
                return iconMalo;
            }
        }
    }

    function formatTrackingPublicacionReorganizacion(cellvalue, options, rowObject) {
        return TipoTrackingDemanda(cellvalue) +
            "<span>" + cellvalue + "</span>";
    }

    function CargarProyeccion() {
        var newUrl = "/Judicial/ListarPanelQuiebraProyeccion/"
        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            async: false,
            success: function (data) {
                if (data != '') {
                    $("#panelQuiebraProyeccion").html(data);
                } else {

                }
            },
            error: function (ex) {
                alert('Error al recuperar Proyeccion.' + ex);
            }

        });

    }

    function change_checkbox(el) {
        var currentCB = $(el);
        var $grid = jQuery('#gridPorTraspasar');

        var isChecked = el.checked;
        if (currentCB.is(".groupHeader")) {	//if group header is checked, to check all child checkboxes		

            var checkboxes = currentCB.closest('tr').nextUntil('tr.gridPorTraspasarghead_0').find('.cbox[type="checkbox"]');

            checkboxes.each(function () {
                if (!this.checked || !isChecked)
                    $grid.setSelection($(this).closest('tr').attr('id'), true);
            });
            //$(this).prop("checked", true);
        } else {  //when child checkbox is checked
            var allCbs = currentCB.closest('tr').prevAll("tr.gridPorTraspasarghead_0:first").
                nextUntil('tr.gridPorTraspasarghead_0').andSelf().find('[type="checkbox"]');
            var allSlaves = allCbs.filter('.cbox');
            var master = allCbs.filter(".groupHeader");

            var allChecked = !isChecked ? false : allSlaves.filter(":checked").length === allSlaves.length;
            master.prop("checked", allChecked);
        }

    }

    function fnOnSelectAllTraspaso(aRowids, status) {
        $("input.groupHeader").attr('checked', status);
    }
    function fnAbrirMotivoNoDemandables() {
        $("#MotivoRechazoDemanda").val('');
        $('#ppMotivoNoDemanda').dialog('open');
    }
    function fnDocumentosNoDemandables() {
        var noDemandables;
        noDemandables = $("#gridPorTraspasar").jqGrid('getGridParam', 'selarrrow');
        if (noDemandables != "") {
            if ($("#MotivoRechazo").val() != '') {
                $.ajax({
                    type: 'POST',
                    url: "/Judicial/DocumentosNoDemandables/",
                    dataType: 'json',
                    async: true,
                    data: {
                        ids: JSON.stringify(noDemandables),
                        motivo: $("#MotivoRechazoDemanda").val()
                    },
                    success: function (data) {
                        if (data > 0) {
                            alert("Realizado con exito")
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        $("body").removeClass("loading");
                    },
                    complete: function () {
                        $("body").removeClass("loading");
                        $('#ppMotivoNoDemanda').dialog('close');
                        jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    }
                });
            } else {
                alert("Ingrese el motivo");
            }

        } else {
            alert("Debe seleccionar uno o mas documentos");
        }
    }

    function fnExcelReport() {
        //$("td:hidden,th:hidden", "#gridPanelDemandaReporte").remove();
        //var tab = document.getElementById('gridPanelDemandaReporte'); // id of table

        // clone the table
        var $clonedTable = $("#gridPanelDemandaReporte").clone();
        $clonedTable.find('[style*="display:none;"]').remove();

        tab = $clonedTable;

        var headers = new Array();
        var reportName = 'reporte';

        switch ($("#TipoReportePanelDemanda").val()) {
            case "1":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Ingreso Judicial", "Fecha de Envío de Confección", "Fecha de Entrega", "Fecha de Ingreso Tribunal", "Correcciones", "Comentarios"]);
                reportName = 'Total Meses Anteriores';
                break;
            case "2":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Ingreso Judicial", "Fecha de Envío de Confección", "Fecha de Entrega", "Fecha de Ingreso Tribunal", "Correcciones", "Comentarios"]);
                reportName = 'Total Mes Actual';
                break;
            case "3":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Ingreso Judicial", "Fecha de Envío de Confección", "Fecha de Entrega", "Fecha de Ingreso Tribunal", "Correcciones", "Comentarios"]);
                reportName = 'Total Demandas';
                break;
            case "4":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Ingreso Judicial", "Alerta", "Comentarios"]);
                reportName = 'Demandas No Asignadas';
                break;
            case "5":
                headers.push(["Encargado", "Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Fecha de Envío de Confección", "Comentarios"]);
                reportName = 'Demandas Asignadas';
                break;
            case "6":
                headers.push(["Encargado", "Cliente", "Rut", "Deudor", "Asegurado", "Fecha de Envío de Confección", "Correciones", "Alerta", "Comentarios"]);
                reportName = 'Demandas Sin Confeccionar';
                break;
            case "7":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso", "Traspaso", "Ingreso Judicial", "Fecha de Envío de Confección", "Fecha de Entrega", "Correcciones", "Cantidad de Correcciones", "Comentarios"]);
                reportName = 'Demandas Confeccionadas';
                break;
            case "8":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Ingreso Judicial", "Fecha de Envío de Confección", "Fecha de Entrega", "Fecha de Ingreso Tribunal", "Alerta", "Alerta Tribunal", "Tracking Demanda", "Comentarios"]);
                reportName = 'Demandas Ingresadas a Tribunal';
                break;
            case "9":
                headers.push(["Cliente", "Rut", "Deudor", "Asegurado", "Fecha de Envío de Confección", "Fecha de Entrega", "Alerta", "Comentarios"]);
                reportName = 'Demandas No Ingresadas a Tribunal';
                break;
        }
        //Se obtiene el numero de columnas.
        var columnCount = headers[0].length;
        // Obtener la referencia del elemento body
        var body = document.getElementsByTagName("body")[0];
        // Crea un elemento <table> y un elemento <tbody>
        var tabla = document.createElement("table");
        tabla.id = "tbltes3";

        //Agregar el header.
        var row = tabla.insertRow(-1);
        for (var x = 0; x < columnCount; x++) {
            var headerCell = document.createElement("th");
            headerCell.innerHTML = headers[0][x];
            row.appendChild(headerCell);
        }

        var tblBody = document.createElement("tbody");

        // Crea las celdas
        for (var i = 1; i < tab["0"].rows.length; i++) {//tab.rows.length; i++) {
            // Crea las filas de la tabla
            var fila = document.createElement("tr");
            for (var j = 0; j < columnCount; j++) {
                // Crea un elemento <td> y un nodo de texto, haz que el nodo de
                // texto sea el contenido de <td>, ubica el elemento <td> al final
                // de la fila de la tabla
                var celda = document.createElement("td");
                var textoCelda = document.createTextNode($(tab["0"].rows[i]).find('td:eq(' + j + ')').text());//.html()
                celda.appendChild(textoCelda);
                fila.appendChild(celda);
            }
            // agrega la fila al final de la tabla (al final del elemento tblbody)
            tblBody.appendChild(fila);
        }
        // posiciona el <tbody> debajo del elemento <table>
        tabla.appendChild(tblBody);
        // appends <table> into <body>
        body.appendChild(tabla);
        tabla.setAttribute("style", "display:none;");
        tableToExcel("tbltes3", reportName, reportName);
        //Se elimina DOM element
        var element = document.getElementById("tbltes3");
        element.parentNode.removeChild(element);
    }

    var tableToExcel = (function () {

        var uri = 'data:application/vnd.ms-excel;base64,',
            template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--><meta http-equiv="content-type" content="text/plain; charset=UTF-8"/></head><body><table>{table}</table></body></html>',
            base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) },
            format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        return function (table, name, filename) {
            if (!table.nodeType) table = document.getElementById(table);
            var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
            //window.location.href = uri + base64(format(template, ctx));

            var url = uri + base64(format(template, ctx));
            //var link = document.createElement('a');
            //link.href = url;
            //link.download = filename + '.xls';
            //link.click();
            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                if (window.navigator.msSaveBlob) {
                    var blob = new Blob([template], {
                        type: "application/csv;charset=utf-8;"
                    });
                    navigator.msSaveBlob(blob, filename + '.xls');
                }
            } else {
                $('#linkExport').attr('href', url);
                $('#linkExport').attr('download', filename + '.xls');
            }


        }
    })();

    function fnBuscarDocsDeudorTraspasados() {

        if ($("#Ctcid").val() != 0) {
            var newUrl = "/Judicial/GetTraspasoJudicialHechoDeudor/?"
            newUrl += "Ctcid=" + $("#Ctcid").val();
            jQuery("#gridPorRevertir").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
        }
    }

    /* Funciones para Previsionales
    ******************************************************************/
    function fnRefreshGridPanelDemandasPrevisional() {
        var newUrl = "/Judicial/ListarPanelDemandasPrevisional"
        jQuery("#gridPanelDemandas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
    }

    function fnGuardarDocumentosPrevisional(agregar, eliminar) {
        $.ajax({
            type: 'POST',
            url: "/Judicial/ActualizarDocumentosRolPrevisional",
            dataType: 'json',
            async: false,
            data: { Rolid: $("#Rolid").val() == "" ? 0 : $("#Rolid").val(), Pclid: $("#Pclid").val(), Ctcid: $("#Ctcid").val(), DocumentosAsignar: agregar, DocumentosEliminar: eliminar },
            success: function (data) {
                if (data > 0) {
                    jQuery("#gridDocSinAsignarPrevisional").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    jQuery("#gridDocAsignadoPrevisional").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                }
            },
            error: function (ex) {
                alert('Error al grabar los documentos.' + ex);
            }
        });
    }

    function fnCursoDemandaPrevisional(panelId) {
        var $optionSelected = $('#gridPanelDemandas tr[id="' + panelId + '"] select[id="' + panelId + '_CursoDemanda"]').val();
        fnGuardarCursoDemandaPrevisional(panelId, $optionSelected);
    }

    function fnGuardarCursoDemandaPrevisional(PanelId, DemandaCursoSelect) {
        var newUrl = "/Judicial/GuardarCursoDemandaPrevisional?"
        var datos = {
            panelId: PanelId,
            cursoDemanda: DemandaCursoSelect,
            motivo: ''
        };

        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            data: JSON.stringify(datos),
            beforeSend: function () { $("body").addClass("loading"); },
            success: function (data) {
                $("body").removeClass("loading");
                jQuery("#gridPanelDemandas").jqGrid().trigger('reloadGrid', [{ page: 1 }])

            },
            error: function (jqXHR, textStatus, errorThrown) {
                $("body").removeClass("loading");
                alert('Error al guardar curso de la demanda.' + errorThrown);
            }

        });
    }

    function formatCursoDemandaPrevisional(cellvalue, options, rowObject) {
        return tipoCursoDemandaPrevisional(cellvalue, rowObject);
    }

    function tipoCursoDemandaPrevisional(cellvalue, rowObject) {
        var selectSi = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
        var selectNo = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
        var seleccione = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

        var selectSiDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
        var selectNoDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
        var seleccioneDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaPrevisional(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

        if (rowObject[17] == "S") {
            if (cellvalue == "SI")
                return selectSiDisabled;
            else
                return seleccione;
        }
        if (rowObject[17] == "N")
            return seleccioneDisabled;
    }

    function fnBotonesGridPanelDemandasPrevisional(cellvalue, options, rowobject) {
        var html = '\
        <div class="tabla"><div class="fila">\
            <div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnEditarPanelPrevisional(\'' + rowobject + '\')">Editar</button></div>\
        </div></div>';

        return html;
    }

    function fnBotonEliminarPanelDemandasPrevisional(cellvalue, options, rowobject) {
        var html = '\
        <div class="tabla"><div class="fila">\
            <div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px" onclick="fnEliminarPanelDemandaPrevisional(\'' + rowobject + '\')">Eliminar</button></div>\
        </div></div>';

        return html;
    }

    function fnEditarPanelPrevisional(id) {
        var datos = id.split(',');
        var item = {};
        item.PanelId = datos[0];
        item.NombreCliente = datos[5];
        item.RutDeudor = datos[6];
        item.NombreDeudor = datos[7];
        item.EncarConfec = datos[10]; //12
        item.FecConfec = datos[11]; //13
        item.FecEntrega = datos[12]; //14
        item.FecTribunal = datos[13]; //15
        item.Comentarios = datos[16]; //18
        item.PanelProcesado = datos[17]; //19
        item.UsrId = datos[21];
        item.Pclid = datos[22];
        item.Ctcid = datos[23];
        item.RutCliente = datos[24];
        item.CountFechaEntrega = datos[25];

        var d = $("#ppAvanceDemanda").dialog();

        frmAvanceDemandaReset();
        $('#frmAvanceDemanda input[id=PanelProcesado]').val(item.PanelProcesado);
        if (item.PanelProcesado == 'S') {
            //Deshabilita panel procesado
            $("#EnviaFechaEntrega").prop("disabled", true)
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
            $("#btnGuardar").prop('disabled', true);
        } else {
            //Habilita panel no procesado
            $("#EnviaFechaEntrega").prop("disabled", false)
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
            $("#btnGuardar").prop('disabled', false);
        }

        //Dehabilitar para un usuario que no corresponda al usuario encargado
        if (item.UsrId != 0) {
            if (item.UsrId != $('#frmAvanceDemanda input[id=GetUsuario]').val()) {
                $("#EnviaFechaEntrega").prop("disabled", true)
                $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
                $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
                $("#btnGuardar").prop('disabled', true);
            }
        }

        //Habilitar para Control de Gestion
        if (($('#frmAvanceDemanda input[id=GetPerfil]').val() == 13) || ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 10)) {
            $("#EnviaFechaEntrega").prop("disabled", false)
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
            $("#btnGuardar").prop('disabled', false);
            //quit mindate temporal
            $('#frmAvanceDemanda input[id=FechaConfeccion]').datepicker('enable');
            $('#frmAvanceDemanda input[id=FechaConfeccion]').datepicker("option", "minDate", null);
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker("option", "minDate", null);
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker("option", "maxDate", null);
        }

        //Habilitar para Judicial
        if ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 6) {
            $("#EnviaFechaEntrega").prop("disabled", false)
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
            $("#btnGuardar").prop('disabled', false);
        }

        //Habilitar para Asistente Judicial
        if ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 7) {
            $("#EnviaFechaEntrega").prop("disabled", false)
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
            $("#btnGuardar").prop('disabled', false);
        }

        $('#frmAvanceDemanda input[id=PanelId]').val(item.PanelId);
        $('#frmAvanceDemanda input[id=usridHidden]').val(item.UsrId);
        $('#frmAvanceDemanda input[id=rutDeudorHidden]').val(item.RutDeudor);
        $('#frmAvanceDemanda input[id=nombreDeudorHidden]').val(item.NombreDeudor);
        $('#frmAvanceDemanda input[id=rutClienteHidden]').val(item.RutCliente);
        $('#frmAvanceDemanda input[id=nombreClienteHidden]').val(item.NombreCliente);
        $('#frmAvanceDemanda input[id=pclidHidden]').val(item.Pclid);
        $('#frmAvanceDemanda input[id=ctcidHidden]').val(item.Ctcid);
        $('#frmAvanceDemanda input[id=CountFechaEntrega]').val(item.CountFechaEntrega);

        $('#ppAvanceDemanda').dialog('open');

        // si no Encargado Cofeccion
        if (item.EncarConfec == '' || item.EncarConfec == null) {
            $("#Confeccion").show();
            $("#Correcciones").hide();
            $("#IngresoTribunal").hide();
            $("#buscarEncargado").show();
            $("#MostrarEncargado").hide();
            d.dialog("option", "height", $("#divTabla").outerHeight() + 100);
        } else {
            $("#Confeccion").show();
            $("#Correcciones").show();
            $("#IngresoTribunal").show();
            $("#buscarEncargado").hide();
            $("#MostrarEncargado").show();
            $('#frmAvanceDemanda label[for=Encargado]').text(item.EncarConfec);
            d.dialog("option", "height", $("#divTabla").outerHeight() + 100);
        }

        //Si no se ha enviado fecha de entrega
        if (item.CountFechaEntrega == 0) {
            $("#IngresoTribunal").hide();
            $("#flagTituloEnviaFechaEntrega").hide();
            $("#EnviaFechaEntrega").hide();
            $("#EnviaFechaEntrega").prop('checked', true);
            $("#FechaEntrega").datepicker('enable');
            d.dialog("option", "height", $("#divTabla").outerHeight() + 100);
        } else {
            $("#IngresoTribunal").show();
            $("#flagTituloEnviaFechaEntrega").show();
            $("#EnviaFechaEntrega").show();
            $("#IngresoTribunal").show();
            d.dialog("option", "height", $("#divTabla").outerHeight() + 100);
        }

        if (item.FecConfec == '' || item.FecConfec == null) {
            $('#FechaConfeccion').datepicker().datepicker('setDate', 'today');
        } else {
            var jsonDate = item.FecConfec.replace(/\D+/g, ''); //reemplaza todos los caracteres a excepcion de numeros
            var date = new Date(parseInt(jsonDate));
            var fechaConfeccion = ('0' + date.getDate()).slice(-2) + '/'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                + date.getFullYear()

            $('#frmAvanceDemanda input[id=FechaConfeccion]').val(fechaConfeccion);
        }

        if (item.FecEntrega == '' || item.FecEntrega == null) {
            $('#frmAvanceDemanda input[id=FechaEntrega]').val('');
            $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val('');
        } else {
            var jsonDate = item.FecEntrega.replace(/\D+/g, ''); //reemplaza todos los caracteres a excepcion de numeros
            var date = new Date(parseInt(jsonDate));
            var fechaEntrega = ('0' + date.getDate()).slice(-2) + '/'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                + date.getFullYear()

            $('#frmAvanceDemanda input[id=FechaEntrega]').val(fechaEntrega);
            $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val(fechaEntrega);
        }

        if (item.FecTribunal == '' || item.FecTribunal == null) {
            $('#frmAvanceDemanda input[id=FechaTribunal]').val('');
        } else {
            var jsonDate = item.FecTribunal.replace(/\D+/g, ''); //reemplaza todos los caracteres a excepcion de numeros
            var date = new Date(parseInt(jsonDate));
            var fechaTribunal = ('0' + date.getDate()).slice(-2) + '/'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                + date.getFullYear()

            $('#frmAvanceDemanda input[id=FechaTribunal]').val(fechaTribunal);
        }

        $('#frmAvanceDemanda textarea[id=Comentarios]').val(item.Comentarios);
    }

    function fnEliminarPanelDemandaPrevisional(id) {
        var r = confirm("Desea eliminar esta demanda?");

        if (r == true) {
            var datos = id.split(',');
            var url = "/Judicial/EliminarPanelDemandaPrevisional?IdPanelDemanda=" + datos[0];

            $.ajax({
                type: 'POST',
                url: url,
                dataType: 'json',
                async: true,
                success: function (data) {
                    jQuery("#gridPanelDemandas").jqGrid().trigger('reloadGrid', [{ page: 1 }])
                },
                error: function (ex) {
                    alert('Error al eliminar la demanda.' + ex);
                }
            });
        }
    }

    function fnGuardarAvanceDemandaPrevisional() {
        var encargado = $("#usrid").val() == '' ? $('#frmAvanceDemanda input[id=usridHidden]').val() : $("#usrid").val();
        var newUrl = "/Judicial/GuardarAvanceDemandaPrevisional?";

        var datos = {
            panelId: $("#PanelId").val(),
            userIdEncargado: encargado,
            fechaEnvio: $("#FechaConfeccion").val(),
            flagEnviaFechaEntrega: $("#EnviaFechaEntrega").prop('checked'),
            fechaEntrega: $("#FechaEntrega").val(),
            fechaTribunales: $("#FechaTribunal").val(),
            comentarios: $("#Comentarios").val(),
        };

        if (encargado != '' && encargado != '0') {
            $.ajax({
                type: 'POST',
                url: newUrl,
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                async: true,
                data: JSON.stringify(datos),
                success: function (data) {
                    if (data > 0) {
                        fnRefreshGridPanelDemandasPrevisional();
                        $('#ppAvanceDemanda').dialog('close');
                    } else {
                        alert(data);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('Error al guardar Avance demanda.' + errorThrown);
                }
            });
        } else {
            alert("Debe ingresar el encargado de la demanda");
        }
    }

    function fnGuardarAvanceDemandaRolPrevisional() {
        if ($('#frmAvanceDemanda input[id=ctcidHidden]').val() == "" || $('#frmAvanceDemanda input[id=pclidHidden]').val() == "" || $('#Rol').val() == "" || $("#TribunalSelect").val() == "" || $("#TipoCausa").val() == "" || $("#MateriaJudicial").val() == "") {
            alert("Debe ingresar todos los datos mandatorios.");
        } else {
            $.ajax({
                type: 'POST',
                url: "/Judicial/GuardarAvanceDemandaRolPrevisional/",
                dataType: 'json',
                async: true,
                beforeSend: function () {
                    $("body").addClass("loading");
                },
                data: {
                    TipoRol: $("#TipoRol").val(),
                    Rol: $("#Rol").val(),
                    Tribunal: $("#TribunalSelect").val(),
                    TipoCausa: $("#TipoCausa").val(),
                    MateriaJudicial: $("#MateriaJudicial").val(),
                    Rolid: $("#Rolid").val(),
                    Pclid: $('#frmAvanceDemanda input[id=pclidHidden]').val(),
                    Ctcid: $('#frmAvanceDemanda input[id=ctcidHidden]').val(),
                    ComboQuiebra: $("#ComboQuiebra").val(),
                    panelId: $("#PanelId").val(),
                    fechaTribunales: $("#FechaDemanda").val(),
                    flagEnviaFechaEntrega: $("#EnviaFechaEntrega").prop('checked'),
                    fechaEntrega: $('#frmAvanceDemanda input[id=FechaEntrega]').val(),
                    RutDeudor: $('#frmAvanceDemanda input[id=rutDeudorHidden]').val()
                },
                success: function (resultData) {
                    $("body").removeClass("loading");
                    if (resultData.success) {
                        $("#Rolid").val(resultData.rolId);
                        if ($("#Rolid").val() != "0" && $("#Rolid").val() != '') {
                            alert('El Rol fue guardado con exito!');
                            fnRefreshGridPanelDemandasPrevisional();
                            $('#ppAgragarRol').dialog('close');
                            $("#EnviaFechaEntrega").prop("disabled", true)
                            $("#EnviaFechaEntrega").prop('checked', false);
                            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
                            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
                        }
                    } else {
                        $("body").removeClass("loading");
                        alert('Error al guardar Rol.');
                    }
                }
            });
        }
    }

    function fnExcelPanelDemandasPrevisional() {
        var url = "/Judicial/ExportToExcelPanelDemandasPrevisional";
        window.location.href = url;
    }

    //Borrador Previsional
    function fnBotonesGridConfeccionDemandaPrevisional(cellvalue, options, rowobject) {
        return '<div class="tabla">\
                <div class="fila" style="margin-left:36px">\
                    <div class="col">\
                        <button type="button" class="ui-icon ui-icon-disk" style="height:20px;width:20px" onclick="fnTipoDemandaPrevisional(\'' + rowobject + '\')" >Confeccionar Demanda</button>\
                    </div>\
                </div>\
            </div>';
    }

    function fnTipoDemandaPrevisional(id) {
        var datos = id.split(',');
        $("#IdDP").val(datos[0]);

        $('#ppBorradoresTipoDemandaPrevisional').dialog('open');

        var editor = CKEDITOR.instances['editor1'];
        if (editor) {
            editor.destroy(true);
        }
        CKEDITOR.replace('editor1');

        fnBorradorSeleccionadoPanelDemandasPrevisional();

        return false;
    }

    function handleCKEditorPostDemandaPrevisional() {
        var htmlData = CKEDITOR.instances.editor1.getData();

        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarBorradorDemandasPrevisional",
            dataType: 'json',
            async: false,
            data: {
                PanelDemandaId: $("#IdDP").val(),
                TipoBorradorId: $("#TipoBorrador").val(),
                HtmlBorrador: htmlData
            },
            success: function (chartsdata) {
                CKEDITOR.instances.editor1.setData(chartsdata);
                alert("Borrador guardado exitosamente.");
            },
            error: function (ex) {
                alert('Error al cargar el borrador.');
            }
        });
    }

    function fnBorradorSeleccionadoPanelDemandasPrevisional() {
        if ($("#TipoBorrador").val() != '' && $("#TipoBorrador").val() != null) {
            $.ajax({
                type: 'POST',
                url: "/Judicial/GetBorradorDemandasPrevisional",
                dataType: 'json',
                async: false,
                data: {
                    IdDP: $("#IdDP").val(),
                    TipoBorrador: $("#TipoBorrador").val()
                },
                success: function (chartsdata) {
                    fnTraeHistorialBorradorDemandaPrevisional();

                    CKEDITOR.instances.editor1.setData(chartsdata);
                },
                error: function (ex) {
                    alert('Error al cargar el borrador.');
                }
            });
        }
    }

    function fnTraeHistorialBorradorDemandaPrevisional() {
        var newUrl = "/Judicial/GetHistoriaBorradorDemandasPrevisional";

        var postData = {
            IdDP: $("#IdDP").val(),
            TipoBorrador: $("#TipoBorrador").val()
        };

        $.ajax({
            type: 'POST',
            url: newUrl,
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

    //Traspaso Judicial
    function fnGuardarTraspasosPrevisional() {
        var traspasar = $("#gridPorTraspasar").jqGrid('getGridParam', 'selarrrow');

        if (traspasar == "") {
            alert("Debe seleccionar uno o más resoluciones para hacer el traspaso.");
        } else {
            var postData = {
                ids: JSON.stringify(traspasar)
            };

            $.ajax({
                type: 'POST',
                url: "/Judicial/GuardarTraspasosPrevisional",
                dataType: 'json',
                async: true,
                beforeSend: function () { $("body").addClass("loading"); },
                data: postData,
                success: function (data) {
                    if (data != -1) {
                        $("body").removeClass("loading");
                        jQuery("#gridPorTraspasar").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                        fnCargarTraspasosPendientesPrevisional();
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

    function fnCargarTraspasosPendientesPrevisional() {
        var desde = $('#FechaDesde').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
        var hasta = $('#FechaHasta').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
        var newUrl = "/Judicial/GetTraspasoJudicialHechoPrevisional?"
        newUrl += "fechaDesde=" + desde + "&fechaHasta=" + hasta;
        jQuery("#gridTraspasados").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    }

    function fnExcelTraspasosPendientesPrevisional() {
        var url = "/Judicial/ExcelTraspasosHechosPrevisional?fechaDesde=" + $('#FechaDesde').datepicker({ dateFormat: 'dd-mm-yyyy' }).val() + "&fechaHasta=" + $('#FechaHasta').datepicker({ dateFormat: 'dd-mm-yyyy' }).val();
        window.location.href = url;
    }

