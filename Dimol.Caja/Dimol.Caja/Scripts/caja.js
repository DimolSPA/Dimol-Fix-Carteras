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
    $("input.groupHeader").attr('checked', status);
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

function fnAgregarDocumento() {
    var d = $("#ppDocumento").dialog();
    $("#frmRecepcionDocumento").reset();
    $('#CreaDeudor').hide();
    $("#ppDocumento").dialog().dialog("option", "height", 300);
    $('#ppDocumento').dialog('option', 'title', 'Ingreso de Documento');
    $('#ppDocumento').dialog('open');
    $('#MontoIngreso').val('0');
    $('#MontoIngreso').val(formatfloat($('#MontoIngreso').val(), 0));
    $("#DocumentoId").val('');
}

function formatfloat(n, dp) {
    var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
    while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
    return s.substr(0, i + 3) + r +
      (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
};

function fnEventsfrmRecepcionDocumento() {
    $('#MontoIngreso').keyup(function (event) {

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

}

function fnGuardarDocumentoCajaRecepcion() {
    var pclidValue = $("#Pclid").val() == "" ? $("#PclidHidden").val() : $("#Pclid").val()

    if ($("#MontoIngreso").val() == '0' || $("#MontoIngreso").val() == '' || $("#MontoIngreso").val() == '0,00' || pclidValue == '' || $("#Ctcid").val() == '' || $("#NumeroDocumento").val() == '') {
        alert("Debe ingresar todos los datos")
    }
    else {
        if (pclidValue == 90 && $("#Sbcid").val() == "")
            alert("Debe ingresar el asegurado")
        else
            $.ajax({
                type: 'POST',
                url: "/Caja/GuardarDocumentoCajaRecepcion/",
                dataType: 'json',
                async: true,
                data: {
                    DocumentoId: $("#DocumentoId").val(),
                    NumeroDocumento: $("#NumeroDocumento").val(),
                    Pclid: $("#Pclid").val() == "" ? $("#PclidHidden").val() : $("#Pclid").val(),
                    Ctcid: $("#Ctcid").val(),
                    Sbcid: $("#Sbcid").val(),
                    Moneda: $("#Moneda").val(),
                    MontoIngreso: $("#MontoIngreso").val()
                },
                success: function (data) {
                    if (data != -1) {
                        alert('Documento guardado con éxito.');

                    } else {
                        alert('Error al guardar Documento.');
                    }
                },
                complete: function () {
                    $('#ppDocumento').dialog('close');
                    jQuery("#gridRecepcionDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                }
            });
        
    }
    
}

function fnBotonesRecepcionDocumentos(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnEditarDocumentoRecepcion(\'' + cellvalue + '\',\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[2] + '\',\'' + rowobject[3] + '\',\'' + rowobject[4] + '\',\'' + rowobject[5] + '\',\'' + rowobject[6] + '\',\'' + rowobject[7] + '\',\'' + rowobject[15] + '\',\'' + rowobject[9] + '\',\'' + rowobject[12] + '\',\'' + rowobject[13] + '\',\'' + rowobject[14] + '\',\'' + rowobject[16] + '\')" >Editar</button></div></div></div>';
}

function fnEditarDocumentoRecepcion(documentoId, numeroDocumento, rutCliente, cliente, rutDeudor, deudor, rutAsegurado, asegurado, fecIngreso, moneda, valorIngreso, pclid, ctcid, sbcid, estatus) {
    
    var d = $("#ppDocumento").dialog();
    $("#frmRecepcionDocumento").reset();
    
    $('#ppDocumento').dialog('option', 'title', 'Actualizar Documento ' + '' + ' Numero: ' + numeroDocumento + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    var newUrl = "/Caja/obtieneEstatusDocumento/?"
    newUrl += "documentoId=" + documentoId
    $.ajax({
        type: 'POST',
        url: newUrl,
        dataType: 'json',
        async: true,
        success: function (data) {

            if (data >= 0) {
                if (data == 1) {
                    $('#ppDocumento').dialog('open');

                    $("#DocumentoId").val(documentoId);
                    $("#PclidHidden").val(pclid);

                    $("#frmRecepcionDocumento input[id=Ctcid]").val(ctcid)
                    $("#frmRecepcionDocumento input[id=Sbcid]").val(sbcid)
                    $('#frmRecepcionDocumento input[id=NombreRutDeudor]').val(rutDeudor + " - " + deudor)
                    $('#frmRecepcionDocumento input[id=NombreRutCliente]').val(rutCliente + " - " + cliente)
                    $('#frmRecepcionDocumento input[id=NombreRutAsegurado]').val(rutAsegurado + " - " + asegurado)

                    $('#frmRecepcionDocumento input[id=NumeroDocumento]').val(numeroDocumento);
                    $('#frmRecepcionDocumento select[id=Moneda]').val(moneda);
                    $('#MontoIngreso').val(valorIngreso);
                    $('#MontoIngreso').val(formatfloat($('#MontoIngreso').val(), 0));

                    var jsonDate = fecIngreso.replace(/\D+/g, '');//reemplaza todos los caracteres a excepcion de numeros
                    var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
                    var fecha = ('0' + date.getDate()).slice(-2) + '/'
                                        + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                                        + date.getFullYear()

                    $('#frmRecepcionDocumento input[id=FechaIngreso]').val(fecha);
                }
                else {
                    if (data == 2)
                        alert("El documento ya se encuentra en Proceso")
                    else
                        if (data == 3)
                            alert("El documento ya se encuentra en comercial")
                        else
                            if (data == 4)
                                alert("El documento ya se encuentra en Finanzas")
                            else
                                if (data == 8)
                                    alert("El documento se encuentra Facturado")
                                else
                                    alert("Ya no puede ver el documento")

                }
            } else {
                alert('Error al recuperar el estatus del documento');
            }
        },
        error: function (ex) {
            alert('Error al recuperar el estatus.' + ex);
        }
    });
}

function fnTraspasoComercial() {
    var traspaso;
    traspaso = $("#gridTraspasoDocumentos").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para hacer el traspaso");
    } else {
        var postData = {
            ids: JSON.stringify(traspaso)
        };
        $.ajax({
            type: 'POST',
            url: "/Caja/TraspasarComercial/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                if (data != -1) {
                    $("body").removeClass("loading");
                    jQuery("#gridTraspasoDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                  
                } else {
                    $("body").removeClass("loading");
                    alert('Error al realizar traspasos.');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al realizar traspasos.' + ex);
            }
        });
    }
}
function fnTraspasoComercialIngresado() {
    var traspaso;
    traspaso = $("#gridTraspasoDocumentos").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para hacer el traspaso");
    } else {
        var postData = {
            ids: JSON.stringify(traspaso)
        };
        $.ajax({
            type: 'POST',
            url: "/Caja/TraspasoComercialIngresado/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                if (data != -1) {
                    $("body").removeClass("loading");
                    jQuery("#gridTraspasoDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                } else {
                    $("body").removeClass("loading");
                    alert('Error al realizar traspasos.');
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al realizar traspasos.' + ex);
            }
        });
    }
}

function fnSelectCriterio(cellvalue, options, rowobject) {
    return ListarCriteriosFacturacion(cellvalue, rowobject); 
}
function ListarCriteriosFacturacion(cellvalue, rowobject) {
    var select = '<select role="select" id="' + rowobject[11] + '_Criterio" name="Criterio" size="1" class="editable" onchange="fnCriterio(' + rowobject[11] + ',' + rowobject[15] + ',' + rowobject[16] + ');"><option role="option" value="-1">--</option>';
    var newUrl = "/Caja/ListarCajaCriterioFacturacionCombo/?"
    newUrl += "pclid=" + rowobject[12] + "&criterioId=" + rowobject[16]
    $.ajax({
        type: 'POST',
        url: newUrl, 
        dataType: 'json',
        async: true,
        success: function (criterios) {
            var postData = {
                pclid: rowobject[12],
                documentoId: rowobject[11]
            };
            var criterioPorDefecto = -1;
            $.ajax({
                type: 'POST',
                url: "/Caja/CriterioPorDefecto/",
                dataType: 'json',
                async: true,
                data: postData,
                success: function (data) {
                    criterioPorDefecto = data;
                    $.each(criterios, function (i, criterios) {
                        //$("#" + rowobject[10] + '_Criterio').append('<option role="option" value="' + criterios.Value + '" selected="' + criterios.Selected + '">' +
                        //            criterios.Text + '</option>');
                        if (criterios.Selected)
                            $("#" + rowobject[11] + '_Criterio').append($("<option>").attr('value', criterios.Value).text(criterios.Text).attr('selected', 'selected'));
                        else {
                            if (criterioPorDefecto == criterios.Value && criterioPorDefecto != -1){
                                $("#" + rowobject[11] + '_Criterio').append($("<option>").attr('value', criterios.Value).text(criterios.Text).attr('selected', 'selected'));
                            }
                            else
                                $("#" + rowobject[11] + '_Criterio').append($("<option>").attr('value', criterios.Value).text(criterios.Text));
                        }
                    });
                }
            });
            
        },
        error: function (ex) {
            alert('Error al recuperar los criterios.' + ex);
        }
    });

    select = select + '</select>';
    
    return select;
}
function fnCriterio(documentoId, statusId, criterioId) {
    if (criterioId == undefined) {
        var $optionSelected = $('#gridTraspasoComercialDocumentos tr[id="' + documentoId + '|' + statusId + '|' + '"] select[id="' + documentoId + '_Criterio"]').val();
        fnEvaluarCriterio(documentoId, $optionSelected);
    } else {
        var $optionSelected = $('#gridTraspasoComercialDocumentos tr[id="' + documentoId + '|' + statusId + '|' + criterioId + '"] select[id="' + documentoId + '_Criterio"]').val();
        fnEvaluarCriterio(documentoId, $optionSelected);
    }
    
}
function fnEvaluarCriterio(documentoId, criterioSeleccionado) {
    if (criterioSeleccionado != -1) {
        var newUrl = "/Caja/SiAplicaCriterio/?"
        newUrl += "documentoId=" + documentoId + "&criterioId=" + criterioSeleccionado
        $.ajax({
            type: 'POST',
            url: newUrl,
            dataType: 'json',
            async: true,
            success: function (data) {
               
                if (data >= 0) {
                    if (data == 0) {
                        alert("el criterio Seleccionado, no es aplicable al monto del documento")
                    } else {
                        $.ajax({
                           type: 'POST',
                           url: "/Caja/DefineCriterio?documentoId=" + documentoId + "&criterioId=" + criterioSeleccionado,
                           success: function (data, textStatus) {
                               if (data.IsEditable == 'S') //Se edita el monto
                                   fnDefinirCriterio(data.IsEditable, documentoId, criterioSeleccionado);
                               else {
                                   if (data.Observaciones != '') {
                                       fnDefinirCriterio('N', documentoId, criterioSeleccionado)
                                   } else {
                                       //Guardar Criterio
                                       fnGuardarCriterio(documentoId, criterioSeleccionado, data.MontoFacturar, data.Observaciones);
                                   }
                               }
                           }
                       });
                    }
                } else {
                    alert('Error al recuperar las condiciones');
                }
            },
            error: function (ex) {
                alert('Error al recuperar las condiciones.' + ex);
            }
        });
    }
   
}

function fnDefinirCriterio(ModificarMonto,documentoId, criterioId) {
    var d = $("#ppAddMontoObservaciones").dialog();
    //$("#frmRecepcionDocumento").reset();
    $('#ppAddMontoObservaciones').dialog('option', 'title', 'Definir Criterio');
    $('#ppAddMontoObservaciones').dialog('open');
    $('#MtoAFacturar').val('0');
    $('#MtoAFacturar').val(formatfloat($('#MtoAFacturar').val(), 0));
    $("#DocumentoIdHidden").val(documentoId);
    $("#CriterioIdHidden").val(criterioId);
   
    if (ModificarMonto == 'S') {
        $("#MontoEditable").show()
        $("#ObservacionesEditable").show()
        $('input[name=observacion]').attr('checked', false);
    } else {
        $("#MontoEditable").hide()
        $("#ObservacionesEditable").show()
        $('input[name=observacion]').attr('checked', true);
    }
    
}
function fnGuardarCriterioAlert() {
    var strObservacion = $("input[name='observacion']:checked").parent('label').text().trim();
    $.ajax({
        type: 'POST',
        url: "/Caja/GuardarCajaRecepcionDocumentosCriterio/",
        dataType: 'json',
        async: true,
        data: {
            documentoId: $("#DocumentoIdHidden").val(),
            criterioId: $("#CriterioIdHidden").val(),
            montoFacturar: $('#MtoAFacturar').val(),
            observaciones: strObservacion

        },
        success: function (data) {
            if (data != -1) {
                alert('Criterio Aplicado.');

            } else {
                alert('Error al guardar criterio');
            }
        },
        complete: function () {
            $('#ppAddMontoObservaciones').dialog('close');
            jQuery("#gridTraspasoComercialDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
        }
    });
}
function fnGuardarCriterio(documentoId, criterioId, montoFacturar, observaciones) {
    $.ajax({
        type: 'POST',
        url: "/Caja/GuardarCajaRecepcionDocumentosCriterio/",
        dataType: 'json',
        async: true,
        data: {
            documentoId: documentoId,
            criterioId: criterioId,
            montoFacturar: montoFacturar,
            observaciones: observaciones

        },
        success: function (data) {
            if (data != -1) {
                alert('Criterio Aplicado.');

            } else {
                alert('Error al guardar criterio');
            }
        },
        complete: function () {
            //$('#ppDocumento').dialog('close');
            jQuery("#gridTraspasoComercialDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
        }
    });
}
function fnGuardarCriterioPorDefecto(documentoId, criterioId, montoFacturar, observaciones) {
    $.ajax({
        type: 'POST',
        url: "/Caja/GuardarCajaRecepcionDocumentosCriterio/",
        dataType: 'json',
        async: true,
        data: {
            documentoId: documentoId,
            criterioId: criterioId,
            montoFacturar: montoFacturar,
            observaciones: observaciones

        },
        success: function (data) {
            if (data != -1) {
                console.log('Criterio Aplicado.');

            } else {
                console.log('Error al guardar criterio');
            }
        }
    });
}
function fnTraspasoFinanzas() {
    var traspaso;
    traspaso = $("#gridTraspasoComercialDocumentos").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para hacer el traspaso");
    } else {
        var postData = {
            ids: JSON.stringify(traspaso)
        };
        $.ajax({
            type: 'POST',
            url: "/Caja/TraspasoFinanzas/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                $("body").removeClass("loading");
                
                if (data != '') {
                    $("body").removeClass("loading");
                    alert(data);
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al realizar traspasos.' + ex);
            },
            complete: function () {
                //$('#ppDocumento').dialog('close');
                jQuery("#gridTraspasoComercialDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    }
}

function fnDefinirFactura() {
    var d = $("#ppAddNroFactura").dialog();
    
    //$("#frmRecepcionDocumento").reset();
    $('#ppAddNroFactura').dialog('option', 'title', 'Ingresar Número Factura');
    $('#ppAddNroFactura').dialog('open');
    $('#NroFactura').val('');
    $('#ObservacionesFinanzas').val('');

    var grid = $("#gridTraspasoFinanzasDocumentos")
    //modify the selarrrow parameter
    grid[0].p.selarrrow = grid.find("tr.jqgrow:has(td > input.cbox:checked)")
        .map(function () { return this.id; }) // convert to set of ids
        .get(); // convert to instance of Array


    var ids = grid.jqGrid('getGridParam', 'selarrrow');
    splitted = [];
    var montoFacturaSelected = [];
   
    $('#MontoFactura').val('');
    for (var i = 0; i < ids.length; i++) {
        splitted = ids[i].split('|');
        console.log(splitted[3])
        montoFacturaSelected.push(parseFloat(splitted[3]));
    }
    $('#MontoFactura').val(formatThousands(sum(montoFacturaSelected), 2));
    
}

function fnSelectCriterioDisabled(cellvalue, options, rowobject) {
    return ListarCriteriosFacturacionDisabled(cellvalue, rowobject);
}
function ListarCriteriosFacturacionDisabled(cellvalue, rowobject) {
    var select = '<select disabled role="select" id="' + rowobject[11] + '_Criterio" name="Criterio" size="1" class="editable"><option role="option" value="-1">--</option>';
    var newUrl = "/Caja/ListarCajaCriterioFacturacionCombo/?"
    newUrl += "pclid=" + rowobject[12] + "&criterioId=" + rowobject[16]
    $.ajax({
        type: 'POST',
        url: newUrl,
        dataType: 'json',
        async: true,
        success: function (criterios) {
            $.each(criterios, function (i, criterios) {
                if (criterios.Selected)
                    $("#" + rowobject[11] + '_Criterio').append($("<option>").attr('value', criterios.Value).text(criterios.Text).attr('selected', 'selected').attr('disabled', 'disabled'));
                else
                    $("#" + rowobject[11] + '_Criterio').append($("<option>").attr('value', criterios.Value).text(criterios.Text).attr('disabled', 'disabled'));
            });
        },
        error: function (ex) {
            alert('Error al recuperar los criterios.' + ex);
        }
    });

    select = select + '</select>';

    return select;
}

function fnTraspasoFacturar() {
    var traspaso;
    traspaso = $("#gridTraspasoFinanzasDocumentos").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para hacer la facturacion");
    } else {
        if ($('#NroFactura').val() == "") {
            alert("Ingrese el numero de factura");
        } else {
            var postData = {
                ids: JSON.stringify(traspaso),
                factura: $('#NroFactura').val(),
                observaciones: $('#ObservacionesFinanzas').val()
            };
            $.ajax({
                type: 'POST',
                url: "/Caja/TraspasoFacturacion/",
                dataType: 'json',
                async: true,
                beforeSend: function () { $("body").addClass("loading"); },
                data: postData,
                success: function (data) {
                    $("body").removeClass("loading");

                    if (data != '') {
                        $("body").removeClass("loading");
                        alert(data);
                    }
                },
                error: function (ex) {
                    $("body").removeClass("loading");
                    alert('Error al realizar la facturacion.' + ex);
                },
                complete: function () {
                    $('#ppAddNroFactura').dialog('close');
                    jQuery("#gridTraspasoFinanzasDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    fnRefrescar();
                }
            });
        }
        
    }
}

function fnBotonesGridCuentaConciliar(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="btn btn-info btn-sm" style="height:20px;width:100px" onclick="fnMostrarFormularioConciliacion(\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[4] + '\')" >Conciliación</button></div></div></div>';
}
function fnMostrarFormularioConciliacion(NumCuenta, TipoCuenta, CuentaId) {
    var d = $("#ppFormularioConciliacion").dialog();
    $('#ppFormularioConciliacion').dialog('option', 'title', 'Conciliación Bancaria ' + TipoCuenta + ' Número: ' + NumCuenta);
    $('#ppFormularioConciliacion').dialog('open');
    $("#NumCuentaHidden").val(NumCuenta);
    $("#IdCuentaHidden").val(CuentaId);
    $("#lblTipoCuenta").text(' ' + TipoCuenta + ' Nº ' + NumCuenta);

    var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
    newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
    jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrlCust = "/Tesoreria/ListarDocumentosCustodiaGrilla/?"
    newUrlCust += "numCuenta=" + $("#NumCuentaHidden").val();
    jQuery("#gridDocumentosCustodiados").jqGrid().setGridParam({ url: newUrlCust }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrlDoc = "/Tesoreria/ListarDocumentoCustodiaProtestadosGrilla/?"
    newUrlDoc += "numCuenta=" + $("#NumCuentaHidden").val();
    jQuery("#gridMovimientosProtestados").jqGrid().setGridParam({ url: newUrlDoc }).trigger('reloadGrid', [{ page: 1 }]);
}
function fnLimpiarCargaCartola() {
    $('#frmCargaCartola').reset();
    $("#imgSubirArchivoCartola").removeClass("ok").removeClass("error");
    $("#btnCargarCartola").attr("disabled", "disabled");
    $("#btnSubmitCartola").removeAttr("disabled");
    $('#ArchivoCartola').val('');
    $('#subirArchivoCartola').val('');
}
function fnCargarArchivoCartola() {
    var newUrl = "/Tesoreria/DescargarCartolaBanco/"
    $("#gridErroresCargaCartola").jqGrid('clearGridData');
    var postData = {
        ArchivoCartola: $('#ArchivoCartola').val(),
        numCuenta: $('#NumCuentaHidden').val()
    };
    //console.log(postData);
    if ($('#frmCargaCartola').valid()) {
        $.ajax({
            type: 'POST',
            url: newUrl, // we are calling json method
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $('#loadingmessage').css('display', 'block'); $('#loadingcargaArchivo').css('display', 'block'); },
            success: function (resultData) {

                if (resultData.success) {
                    alert("Archivo cargado con exito");
                    $('#ppAddArchivoCartola').dialog('close');
                   
                }
                else {
                    if (resultData.data.length > 0) {
                        for (var i = 0; i <= resultData.data.length; i++)
                            $("#gridErroresCargaCartola").jqGrid('addRowData', i + 1, resultData.data[i]);
                        alert('Archivo cargado con errores');
                    }
                }

            },

            error: function (ex) {
                $("body").removeClass("loading");
                $('#loadingmessage').css('display', 'none');
                $('#loadingcargaArchivo').css('display', 'none');
                alert('Error al cargar el archivo.' + ex);
            },
            complete: function () {
                $('#loadingmessage').css('display', 'none');
                $('#loadingcargaArchivo').css('display', 'none');
                var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
                newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
                jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
            }

        });
    }
}

function fnMostrarImportarArchivoCartola() {
    $('#ppAddArchivoCartola').dialog('open');
    fnLimpiarCargaCartola();
    $("#gridErroresCargaCartola").jqGrid('clearGridData');
}
function fnMostrarCriterioBusquedaCarotalMovimientos() {
    $('#ppBusquedaMovimientosCartola').dialog('open');
   
}
function fnSelectEstado(cellvalue, options, rowobject) {
    return ListarEstadoSistema(cellvalue, rowobject);
}
function ListarEstadoSistema(cellvalue, rowobject) {
    var select = '<select role="select" id="' + rowobject[0] + '_Estado" name="Estado" size="1" class="editable" onchange="fnEstadoBancario(' + rowobject[0] + ',' + rowobject[12] + ');">';
    var newUrl = "/Tesoreria/ListarEstadoBancoCombo/?"
    newUrl += "estadoId=" + cellvalue
    $.ajax({
        type: 'POST',
        url: newUrl,
        dataType: 'json',
        async: true,
        success: function (estados) {
            $.each(estados, function (i, estados) {
               
                if (estados.Selected)
                    $("#" + rowobject[0] + '_Estado').append($("<option>").attr('value', estados.Value).text(estados.Text).attr('selected', 'selected'));
                else {
                    if (estados.Value != "3")
                        $("#" + rowobject[0] + '_Estado').append($("<option>").attr('value', estados.Value).text(estados.Text));
                }
                                                      
            });
        },
        error: function (ex) {
            alert('Error al recuperar los estados bancarios.' + ex);
        }
    });

    select = select + '</select>';

    return select;
}

function fnEstadoBancario(movimientoId, cuentaId) {
    console.log(cuentaId);
    var $optionSelected = $('#gridCartolaMovimientos tr[id="' + movimientoId + '"] select[id="' + movimientoId + '_Estado"]').val();
    fnActualizarEstadoMovimiento(movimientoId, $optionSelected, cuentaId);
}
function fnGuardarObservacion() {

    if ($('#ObservacionMovimiento').val().trim().length > 0)
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ActualizarObservacionMovimientoCartola/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $('#loadingmessage').css('display', 'block'); },
            data: {
                movimientoId: $('#MovimientoSelectId').val(),
                cuentaId: $('#CuentaSelectId').val(),
                tipoEstadoId: $('#EstadoSelectId').val(),
                observacion: $('#ObservacionMovimiento').val().trim()
            },
            success: function (data) {
                if (data != -1) {
                    alert('Estado bancario Aplicado');
                    var $check = $('#gridCartolaMovimientos tr[id^="' + $('#MovimientoSelectId').val() + '"]').find('input:checkbox:first');
                    if ($('#EstadoSelectId').val() != 2)
                        $check.attr('disabled', 'disabled')
                    else
                        $check.removeAttr("disabled")

                } else {
                    alert('Error al guardar estado bancario');
                }
            },
            complete: function () {
                $('#loadingmessage').css('display', 'none');
                $('#ppAddObservacion').dialog('close');
                jQuery("#gridCartolaMovimientos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    else
        alert("Debe ingresar la observación");
}
function fnActualizarEstadoMovimiento(movimientoId, estadoId, cuentaId) {

    if (estadoId == 4) {
        $('#ppAddObservacion').dialog('open');
        $('#MovimientoSelectId').val(movimientoId);
        $('#EstadoSelectId').val(estadoId);
        $('#CuentaSelectId').val(cuentaId);
        $('#ObservacionMovimiento').val('');
       
    }
    else
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ActualizarEstadoMovimientoCartola/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $('#loadingmessage').css('display', 'block'); },
            data: {
                movimientoId: movimientoId,
                cuentaId: cuentaId,
                tipoEstadoId: estadoId

            },
            success: function (data) {
                if (data != -1) {
                    alert('Estado bancario Aplicado');
                    var $check = $('#gridCartolaMovimientos tr[id^="' + movimientoId + '"]').find('input:checkbox:first');
                    if (estadoId != 2)
                        $check.attr('disabled', 'disabled')
                    else
                        $check.removeAttr("disabled")
                    if (estadoId == 3) {
                        var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
                        newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
                        jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                    }

                } else {
                    alert('Error al guardar estado bancario');
                }
            },
            complete: function () {
                $('#loadingmessage').css('display', 'none');
          
            }
        });
}

function fnBotonesGridCartolaMovimientos(cellvalue, options, rowobject) {
    return '<div onclick="MovimientoSeleccionado(\'' + rowobject[0] + '\',\'' + rowobject[2] + '\',\'' + rowobject[3] + '\',\'' + rowobject[9] + '\',\'' + rowobject[5] + '\',\'' + rowobject[8] + '\',\'' + rowobject[1] + '\'); return false;" style=\"cursor:pointer\"><div style=\"color:blue\">' + cellvalue + '</div></div>'
}


function MovimientoSeleccionado(id, fecMovimiento, monto, motivoId, Sucursal, estado, numCuenta) {
    var $optionSelected = $('#gridCartolaMovimientos tr[id="' + id + '"] select[id="' + id + '_Estado"]').val();
    if ($optionSelected == 1) {
        //Limpiar formulario
        fnLimpiarCargaComprobante();
        $('#frmConciliacionMovimiento input[id=NombreRutCliente]').val('')
        $('#frmConciliacionMovimiento input[id=Pclid]').val('')
        $('#frmConciliacionMovimiento input[id=NombreRutDeudor]').val('')
        $('#frmConciliacionMovimiento input[id=Ctcid]').val('')
        $('#frmConciliacionMovimiento input[id=NombreRutGestor]').val('')
        $('#frmConciliacionMovimiento input[id=Gesid]').val('')
        $('#frmCargaComprobante input[id=NumeroComprobante]').val('')
       
       
        //Fin Limpiar formulario

        $('#ppAddConciliacionMovimiento').dialog('open');
        $('#frmConciliacionMovimiento input[id=MovimientoId]').val(id);
        $('#frmConciliacionMovimiento input[id=NumCuentaHidden]').val(numCuenta);
        
        $('#frmConciliacionMovimiento input[id=Monto]').val(monto);
        $('#frmConciliacionMovimiento input[id=Monto]').val(formatfloat($('#frmConciliacionMovimiento input[id=Monto]').val(), 0)).prop('disabled', true);

        var jsonDate = fecMovimiento.replace(/\D+/g, '');//reemplaza todos los caracteres a excepcion de numeros
        var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
        var fecha = ('0' + date.getDate()).slice(-2) + '-'
                            + ('0' + (date.getMonth() + 1)).slice(-2) + '-'
                            + date.getFullYear()
        $('#frmConciliacionMovimiento input[id=Fecha]').val(fecha).prop('disabled', true);
        
        $('#frmConciliacionMovimiento select[id=Motivo]').val(motivoId).attr('disabled', 'disabled');

        $('#frmConciliacionMovimiento input[id=Sucursal]').val(Sucursal).prop('disabled', true);

        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ObtConciliacionNumComprobante/",
            dataType: 'json',
            async: true,
            success: function (result) {
                $('#frmCargaComprobante input[id=NumeroComprobante]').val(result);
            }
        });

    } else {
        alert("Para conciliar, el movimiento debe estar en estado LIBERADO")
    }
    
}
function fnLimpiarCargaComprobante() {
    $("#imgSubirArchivoComprobante").removeClass("ok").removeClass("error");
    $("#btnCargarConciliacion").attr("disabled", "disabled");
    $("#btnSubmitComprobante").removeAttr("disabled");
    $('#ArchivoComprobante').val('')
    $('#subirArchivoComprobante').val('')
}
function fnInsertarConciliacionMovimiento() {
    
    if ($('#frmConciliacionMovimiento input[id=Pclid]').val() == "")
        alert("Debe Ingresar el cliente!");
    else
        
        if ($('#frmConciliacionMovimiento input[id=Ctcid]').val() == "")
            alert("Debe ingresar el deudor!")
        else
            
            if ($('#frmConciliacionMovimiento input[id=Gesid]').val() == "")
                alert("Debe ingresar el gestor")
            else
                $.ajax({
                    type: 'POST',
                    url: "/Tesoreria/ExisteConciliacionComprobante/",
                    dataType: 'json',
                    async: true,
                    data: {
                        numComprobante: $('#frmCargaComprobante input[id=NumeroComprobante]').val()
                    },
                    success: function (data) {

                        //Si es cero(0) no existe el comprobante
                        if (data == 0) {

                            //Se procee a conciliar movimiento
                            $.ajax({
                                type: 'POST',
                                url: "/Tesoreria/InsertarConciliacionMovimiento/",
                                dataType: 'json',
                                async: true,
                                data: {
                                    movimientoId: $('#frmConciliacionMovimiento input[id=MovimientoId]').val(),
                                    numComprobante: $('#frmCargaComprobante input[id=NumeroComprobante]').val(),
                                    custodiaId: '',
                                    pclid: $('#frmConciliacionMovimiento input[id=Pclid]').val(),
                                    ctcid: $('#frmConciliacionMovimiento input[id=Ctcid]').val(),
                                    gestorId: $('#frmConciliacionMovimiento input[id=Gesid]').val(),
                                    tipoConciliacion: $('#TipoConciliacion').val(),
                                    pathArchivo: $('#frmCargaComprobante input[id=ArchivoComprobante]').val(),
                                    numCuenta: $('#frmConciliacionMovimiento input[id=NumCuentaHidden]').val()
                                },
                                success: function (data) {

                                    if (data > 0) {
                                        alert("Registro conciliado con éxito");
                                        $('#ppAddConciliacionMovimiento').dialog('close');
                                        var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
                                        newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
                                        jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

                                    } else {
                                        alert("Error al crear la conciliación")

                                    }
                                }
                            });

                        } else {
                            if (data > 0)
                                alert("El número de comprobante ingresado, ya se encuentra registrado en el sistema")

                        }
                    }
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

function moveTableColumn (rows, iCol, className) {
    var rowsCount = rows.length, iRow, row, $row;
    for (iRow = 0; iRow < rowsCount; iRow += 1) {
        row = rows[iRow];
        $row = $(row);
        if (!className || $row.hasClass(className)) {
            $row.append(row.cells[iCol]);
        }
    }
}

function SelectSoloPendiente(rowid, status) {
    var grid = $("#gridCartolaMovimientos")
    //if (status) {
        // uncheck "protected" rows
        var cbsdis = $("tr#" + rowid + ".jqgrow > td > input.cbox:disabled", grid[0]);
        if (cbsdis.length === 0) {
            //cbs.removeAttr("checked");
        } else {
            cbsdis.removeAttr("checked");
        }
            
    //}

}

function fnOnLoadCompleteConciliacion(grilla) {
    var $grid = jQuery("#" + grilla.id)
    // we make all even rows "protected", so that will be not selectable
    //var cbs = $("tr.jqgrow > td > input.cbox:even", $grid[0]);
    //cbs.attr("disabled", "disabled");
    
    var iCol = getColumnIndexByName($grid, 'EstadoId'),
       cRows = $grid[0].rows.length, iRow, row, className;
    var idCol = getColumnIndexByName($grid, 'MovimientoId')
   
    for (iRow = 0; iRow < cRows; iRow++) {
        row = $grid[0].rows[iRow];
        className = row.className;
        
        if ($.inArray('jqgrow', className.split(' ')) > 0) { 
            var estadoId = $(row.cells[iCol]).text()
            var movimientoId = $(row.cells[idCol]).text();
            
            var $check = $('#' + grilla.id + ' tr[id^="' + movimientoId + '"]').find('input:checkbox:first');
            if (estadoId.length > 0) {
                if (estadoId != 2) {
                    $check.attr('disabled', 'disabled');
                } else {
                    $check.removeAttr("disabled");
                }
                    
            }
        }
    }

  
}
function fnOnSelectAllConciliacion(aRowids, status) {
    var grid = $("#gridCartolaMovimientos")
    if (status) {
        // uncheck "protected" rows
        var cbs = $("tr.jqgrow > td > input.cbox:disabled", grid[0]);
        cbs.removeAttr("checked");

        //modify the selarrrow parameter
        grid[0].p.selarrrow = grid.find("tr.jqgrow:has(td > input.cbox:checked)")
            .map(function () { return this.id; }) // convert to set of ids
            .get(); // convert to instance of Array
    }
}

function fnTraspasoPanelProtestado() {
    var grid = $("#gridCartolaMovimientos")
    //var $grid = jQuery("#gridCartolaMovimientos")
    //modify the selarrrow parameter
    grid[0].p.selarrrow = grid.find("tr.jqgrow:has(td > input.cbox:checked)")
        .map(function () { return this.id; }) // convert to set of ids
        .get(); // convert to instance of Array

   
    var ids = grid.jqGrid('getGridParam', 'selarrrow');
    var hayPositivo = 0;
    var hayNegativo = 0;
    var montoA = 0;
    var montoB = 0;
    if (ids.length > 0) {
        if (ids.length > 2) {
            alert("Solo puede seleccionar 2 Movimientos")
        }
        else {
            if (ids.length == 1) {
                alert("Debe seleccionar 2 Movimientos")
            }
            else {
                //Se valida que un monto sea negativo y otro positivo
                for (var i = 0; i < ids.length; i++) {
                    var rowData = jQuery("#gridCartolaMovimientos").getRowData(ids[i]);
                    var montoMovimiento = parseInt(rowData['Monto'], 10);
                    console.log(montoMovimiento);
                    if (montoA == 0)
                        montoA = montoMovimiento;
                    else
                        montoB = montoMovimiento;

                    if (montoMovimiento < 0)
                        hayNegativo = 1;
                    else
                        hayPositivo = 1;

                }
                if (hayNegativo == 1 && hayPositivo == 1) {
                    //Se procede a evaluar montos
                    var resta = 1
                    if (montoA < 0)
                        resta = montoB - (Math.abs(montoA))
                    else
                        resta = montoA - (Math.abs(montoB))
                    if (resta == 0) {
                        var postData = {
                            ids: JSON.stringify(ids),
                            numCuenta: $('#NumCuentaHidden').val(),
                            cuentaId: $('#IdCuentaHidden').val()
                        };
                        $.ajax({
                            type: 'POST',
                            url: "/Tesoreria/TraspasoPanelProtestado/",
                            dataType: 'json',
                            async: true,
                            beforeSend: function () { $('#loadingmessage').css('display', 'block'); },
                            data: postData,
                            success: function (data) {
                                if (data != -1) {
                                    alert('Los registros fueron enviados con éxito');

                                } else {
                                    $('#loadingmessage').css('display', 'none');
                                    alert('Error al realizar envio a panel de protestados.');
                                }
                            },
                            error: function (ex) {
                                $('#loadingmessage').css('display', 'none');
                                alert('Error al realizar envio a panel de protestados.' + ex);
                            },
                            complete: function () {
                                $('#loadingmessage').css('display', 'none');
                                var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
                                newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
                                jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

                                var newUrlDoc = "/Tesoreria/ListarMovimientosProtestadosGrilla/?"
                                newUrlDoc += "numCuenta=" + $("#NumCuentaHidden").val();
                                jQuery("#gridMovimientosProtestados").jqGrid().setGridParam({ url: newUrlDoc }).trigger('reloadGrid', [{ page: 1 }]);
                            }
                        });
                    }
                    else
                        alert("Los montos no son iguales")
                } else {
                    alert("Debe Seleccionar un monto positivo y otro negativo")
                }
            }
        }
            
    } else {
        alert("Debe seleccionar uno o mas registros con estado Pendiente, para enviar a panel de protestados");
    }
   
}

function fnExportarMovimientosCartola() {
    var url = "/Tesoreria/ExportToExcelPanelDemandas/?numCuenta=" + $("#NumCuentaHidden").val();
    window.location.href = url;

}

function fnCargarGestorConciliacion() {
    if ($('#frmConciliacionMovimiento input[id=Ctcid]').val() != '' && $('#frmConciliacionMovimiento input[id=Pclid]').val() != ''){
        var postData = {
            pclid: $('#frmConciliacionMovimiento input[id=Pclid]').val(),
            ctcid: $('#frmConciliacionMovimiento input[id=Ctcid]').val()
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ListarGestorConciliacion/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (gestor) {
                $.each(gestor, function (i, gestor) {
                    $('#frmConciliacionMovimiento input[id=NombreRutGestor]').val(gestor.Text)
                    $('#frmConciliacionMovimiento input[id=Gesid]').val(gestor.Value)

                });
            },
            error: function (ex) {
                alert('Error al recuperar los gestores.' + ex);
            }
        });
    }
    
}

function fnCargarGestorAddCustodia() {
    if ($('#frmIngresoDocumento input[id=CtcidCustodia]').val() != '' && $('#frmIngresoDocumento input[id=PclidCustodia]').val() != '') {
        var postData = {
            pclid: $('#frmIngresoDocumento input[id=PclidCustodia]').val(),
            ctcid: $('#frmIngresoDocumento input[id=CtcidCustodia]').val()
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ListarGestorConciliacion/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (gestor) {
                $.each(gestor, function (i, gestor) {
                    $('#frmIngresoDocumento input[id=NombreRutGestorCustodia]').val(gestor.Text)
                    $('#frmIngresoDocumento input[id=GesidCustodia]').val(gestor.Value)

                });
            },
            error: function (ex) {
                alert('Error al recuperar los gestores.' + ex);
            }
        });
    }

}

function fnCargarGestorConciliacionCustodia() {
    if ($('#frmConciliacionCustodia input[id=CtcidCC]').val() != '' && $('#frmConciliacionCustodia input[id=PclidCC]').val() != '') {
        var postData = {
            pclid: $('#frmConciliacionCustodia input[id=PclidCC]').val(),
            ctcid: $('#frmConciliacionCustodia input[id=CtcidCC]').val()
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ListarGestorConciliacion/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (gestor) {
                $.each(gestor, function (i, gestor) {
                    $('#frmConciliacionCustodia input[id=NombreRutGestorCC]').val(gestor.Text)
                    $('#frmConciliacionCustodia input[id=GesidCC]').val(gestor.Value)

                });
            },
            error: function (ex) {
                alert('Error al recuperar los gestores.' + ex);
            }
        });
    }

}
function fnAgregarCuentaBancaria() {
   
    $('#ppCuentaBancaria').dialog('open');
    $('#NumeroCuenta').val('');
}

function fnCrearCuentaBancaria() {

    if ($('#NumeroCuenta').val() == "")
        alert("Debe Ingresar el cliente!");
    else
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ExisteCuentaBancaria/",
            dataType: 'json',
            async: true,
            data: {
                numCuenta: $('#NumeroCuenta').val()
            },
            success: function (data) {
                //Si es cero(0) no existe la cuenta
                if (data == 0) {

                    //Se procee a grabar la cuenta
                    $.ajax({
                        type: 'POST',
                        url: "/Tesoreria/InsertarCuentaBancaria/",
                        dataType: 'json',
                        async: true,
                        data: {
                            numCuenta: $('#NumeroCuenta').val(),
                            bancoId: $('#Banco').val(),
                            tipoCuentaId: $('#TipoCuenta').val()
                        },
                        success: function (data) {

                            if (data > 0) {
                                alert("Registro creado con éxito");
                                $('#ppCuentaBancaria').dialog('close');
                                jQuery("#gridCuentasBancarias").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            } else {
                                alert("Error al crear la cuenta")

                            }
                        },
                        error: function (ex) {
                            alert('Error.' + ex);
                        }
                    });

                } else {
                    if (data > 0)
                        alert("El número de cuenta ingresado, ya se encuentra registrado en el sistema")

                }
            },
            error: function (ex) {
                alert('Error al verificar la cuenta.' + ex);
            }
        });
                
}

var montoFacturaOfSelectedRows = [];
function fnRefrescar() {
    montoFacturaOfSelectedRows = [];
    idsOfSelectedRows = [];
    $('#MontoFactura').val('');
}
function UpdateMontoFacturar(id, isSelected) {
    
    var montoVisitaTotalOfSelectedRows = [];
    var index = $.inArray(id, idsOfSelectedRows);
    if (!isSelected && index >= 0) {

        idsOfSelectedRows.splice(index, 1);
        montoFacturaOfSelectedRows.splice(index, 1); // remove id from the list
        //var $check = $('#gridTraspasoFinanzasDocumentos tr[id^="' + id + '"]').find('input:checkbox:first');
        //$check.prop("checked", false);
    } else if (index < 0) {
        idsOfSelectedRows.push(id);
        //var $check = $('#gridTraspasoFinanzasDocumentos tr[id^="' + id + '"]').find('input:checkbox:first');
        //$check.prop("checked", true);
       
        montoFacturaOfSelectedRows.push(parseFloat($('#gridTraspasoFinanzasDocumentos').jqGrid('getCell', id, 'MontoFacturar')));
    }
    

    $('#MontoFactura').val(formatThousands(sum(montoFacturaOfSelectedRows), 2));

}
function fnOnSelectAllFacturar(aRowids, status) {
    $('#MontoFactura').val('');
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateMontoFacturar(id, status);
    }
}

function fnOnLoadCompleteFacturar(grilla) {
    var $grid = jQuery("#" + grilla.id), rows = $grid[0].rows, cRows = rows.length,
    iRow, rowId, row, cellsOfRow, iCol;

    var i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $grid.setSelection(idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i]).prop("checked", true);
       
    }
}

function formatThousands(n, dp) {
    var s = '' + (Math.floor(n)), d = n % 1, i = s.length, r = '';
    while ((i -= 3) > 0) { r = '.' + s.substr(i, 3) + r; }
    return s.substr(0, i + 3) + r +
      (d ? ',' + Math.round(d * Math.pow(10, dp || 2)) : '');
}

function sum(rows) {
    var suma = 0;

    for (i = 0; i < rows.length; i++) {
        suma += rows[i];
    }

    return suma;
}

function PaisSeleccionado(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Caja/ListarRegion", // we are calling json method
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
        url: "/Caja/ListarCiudad", // we are calling json method
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

}

function CiudadSeleccionada(controlOrigen, controlDestino) {
    $("#" + controlDestino).empty();
    $.ajax({
        type: 'POST',
        url: "/Caja/ListarComuna", // we are calling json method
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

function validaRut(rutCompleto) {
    if (!/^[0-9]+[-|‐]{1}[0-9kK]{1}$/.test(rutCompleto))
        return false;
    var tmp = rutCompleto.split('-');
    var digv = tmp[1];
    var rut = tmp[0];
    if (digv == 'K') digv = 'k';
    return (dv(rut) == digv);
}

function dv(T) {
    var M = 0, S = 1;
    for (; T; T = Math.floor(T / 10))
        S = (S + T % 10 * (9 - M++ % 6)) % 11;
    return S ? S - 1 : 'k';
}

function checkRutGenerico(campo) {
    var tmpstr = "";
    var rut = campo;
    var i = 0;
    var largo = 0;
     var j = 0;
    var cnt = 0;
    
        
    for (i = 0; i < rut.length ; i++)
        if (rut.charAt(i) != ' ' && rut.charAt(i) != '.' && rut.charAt(i) != '-')
            tmpstr = tmpstr + rut.charAt(i);
    rut = tmpstr;
    largo = rut.length;

    tmpstr = "";
    for (i = 0; rut.charAt(i) == '0' ; i++);
    for (; i < rut.length ; i++)
        tmpstr = tmpstr + rut.charAt(i);
    rut = tmpstr;
    largo = rut.length;

    tmpstr = "";
   
    var invertido = "";
    for (i = (largo - 1), j = 0; i >= 0; i--, j++)
        invertido = invertido + rut.charAt(i);
    var drut = "";
    drut = drut + invertido.charAt(0);
    drut = drut + '-';
    cnt = 0;
    for (i = 1, j = 2; i < largo; i++, j++) {
        if (cnt == 3) {
            drut = drut + '.';
            j++;
            drut = drut + invertido.charAt(i);
            cnt = 1;
        }
        else {
            drut = drut + invertido.charAt(i);
            cnt++;
        }
    }
    invertido = "";
    for (i = (drut.length - 1), j = 0; i >= 0; i--, j++) {
        if (drut.charAt(i) == 'k')
            invertido = invertido + 'K';
        else
            invertido = invertido + drut.charAt(i);
    }

    //ASIGNAR FORMATO A CAJA DE TEXTO DE RUT
    document.getElementById("RutDeudor").value = invertido;

    //var dv = rut.charAt(largo - 1);
    ValidaRutIngresadoFormato();
}
function soloRUT(e) {
    var tecla = (document.all) ? event.keyCode : e.which;

    if (tecla == 8) return true;

    var patron = /^[0-9kK]+$/;  //acepta valores alfanumericos	
    var te = String.fromCharCode(tecla);

    if (!patron.test(te) && tecla == 0) {
        return true;
    }

    //VALIDA LA TECLA ENTER Y ACCIONA EL FOCUS A LA PROXIMA CAJA DE TEXTO.  
    
    if (tecla == 13) {
        if (!document.getElementById("RutDeudor").value == "") {
            document.getElementById("NombreDeudor").focus();
        }
    }//
    return patron.test(te);
}
//QUITA EL FORMATO DE RUT A LA CAJA DE TEXTO EN LA CUAL SE HACE CLICK, ESTO PARA PERMITIR SU MODIFICACION
//EN CASO DE INGRESAR UN RUT ERRONEO.
function limpiaPuntoGuion() {
    var valCheck;
    var obj;

    obj = document.getElementById("RutDeudor").value;
    obj = obj.replace(".", "");
    obj = obj.replace(".", "");
    obj = obj.replace(/-/, "");
    document.getElementById("RutDeudor").value = obj;

}
function ValidaRutIngresadoFormato() {
    var valCheck;
    var obj;

    obj = document.getElementById("RutDeudor").value;
    obj = obj.replace(".", "");
    obj = obj.replace(".", "");
    
    
    if (!validaRut(obj)) {
        $("#ValidaRut").html('<font style="color:red;margin-left:10px;font-weight:bold;" >Rut Inválido</b>');
        document.getElementById("RutDeudor").focus();
        $("#btnAceptarDeudor").attr("disabled", "disabled");
    } else {
        $("#ValidaRut").html('');
        $("#btnAceptarDeudor").removeAttr("disabled");
    }
}

function fnIngresarDeudorDocumento() {
    if (document.getElementById("RutDeudor").value != '' && $('#NombreDeudor').val() != '' &&
        $('#IdComuna').val() != '' && $('#Direccion').val() != '') {
        //Se procee a grabar el deudor
        var obj;

        obj = document.getElementById("RutDeudor").value;
        obj = obj.replace(".", "");
        obj = obj.replace(".", "");
        obj = obj.replace(/-/, "");

        var nomfant = $('#NombreFantasia').val();
        if (nomfant == '' && $("input[name='TipoOpt']:checked").val() == 'P')
            nomfant = $('#NombreDeudor').val() + " " + $('#ApellidoPaterno').val() + " " + $('#ApellidoMaterno').val();

        if (nomfant == '' && $("input[name='TipoOpt']:checked").val() == 'E')
            alert("Para Empresa, debe ingresar el nombre Fantasia")
        else {
            if ($('#ApellidoPaterno').val() == '' && $("input[name='TipoOpt']:checked").val() == 'P')
                alert("Para Particular, debe ingresar el apellido")
            else
                $.ajax({
                    type: 'POST',
                    url: "/Caja/GuardarDeudor/",
                    dataType: 'json',
                    async: true,
                    data: {
                        Rut: obj,
                        Nombre: $('#NombreDeudor').val(),
                        ApellidoPaterno: $('#ApellidoPaterno').val(),
                        ApellidoMaterno: $('#ApellidoMaterno').val(),
                        NombreFantasia: $('#NombreFantasia').val(),
                        IdComuna: $('#IdComuna').val(),
                        ParticularEmpresa: $("input[name='TipoOpt']:checked").val(),
                        Direccion: $('#Direccion').val()
                    },
                    success: function (data) {

                        if (data > 0) {
                            alert("Deudor creado con éxito");
                            $('#CreaDeudor').hide();
                            $("#frmRecepcionDocumento input[id=Ctcid]").val(data)
                            $('#frmRecepcionDocumento input[id=NombreRutDeudor]').val(obj + " - " + nomfant)
                            $("#ppDocumento").dialog().dialog("option", "height", 300);
                        } else {
                            alert("Error al crear el deudor")

                        }
                    },
                    error: function (ex) {
                        alert('Error.' + ex);
                    }
                });
        }
       
    } else
        alert("Debe ingresar los datos mandatorios");
}

function fnBotonesTraspasoDocumentos(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnEditarDocumentoTraspaso(\'' + cellvalue + '\',\'' + rowobject[0] + '\',\'' + rowobject[1] + '\',\'' + rowobject[2] + '\',\'' + rowobject[3] + '\',\'' + rowobject[4] + '\',\'' + rowobject[5] + '\',\'' + rowobject[6] + '\',\'' + rowobject[7] + '\',\'' + rowobject[16] + '\',\'' + rowobject[10] + '\',\'' + rowobject[13] + '\',\'' + rowobject[14] + '\',\'' + rowobject[15] + '\',\'' + rowobject[18] + '\',\'' + rowobject[19] + '\')" >Editar</button></div></div></div>';
}

function fnEditarDocumentoTraspaso(documentoId, numeroDocumento, rutCliente, cliente, rutDeudor, deudor, rutAsegurado, asegurado, fecIngreso, moneda, valorIngreso, pclid, ctcid, sbcid, statusId, statusProceso) {
    //Seleccionar ek registro a editar
    jQuery("#gridTraspasoDocumentos").setSelection(documentoId + '|' + statusId + '|' + statusProceso, true);
    //guardar Seleccion
    saveSelection.call(jQuery("#gridTraspasoDocumentos"));

    var d = $("#ppDocumento").dialog();
   
    $("#frmRecepcionDocumento").reset();
    $('#CreaDeudor').hide();
    if (statusId == 1) {
        $('#MontoIngreso').removeAttr("disabled");
        $('#frmRecepcionDocumento select[id=Moneda]').removeAttr("disabled");
        
    } else {
        $('#MontoIngreso').attr('disabled', 'disabled');
        $('#frmRecepcionDocumento select[id=Moneda]').attr('disabled', 'disabled');
    }
    
    $('#ppDocumento').dialog('option', 'title', 'Actualizar Documento ' + '' + ' Numero: ' + numeroDocumento + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    $('#ppDocumento').dialog('open');
    $("#ppDocumento").dialog().dialog("option", "height", 300);
    $("#DocumentoId").val(documentoId);
    $("#PclidHidden").val(pclid);

    $("#frmRecepcionDocumento input[id=Ctcid]").val(ctcid)
    $("#frmRecepcionDocumento input[id=Sbcid]").val(sbcid)
    $('#frmRecepcionDocumento input[id=NombreRutDeudor]').val(rutDeudor + " - " + deudor)
    $('#frmRecepcionDocumento input[id=NombreRutCliente]').val(rutCliente + " - " + cliente)
    $('#frmRecepcionDocumento input[id=NombreRutAsegurado]').val(rutAsegurado + " - " + asegurado)

    $('#frmRecepcionDocumento input[id=NumeroDocumento]').val(numeroDocumento);
    $('#frmRecepcionDocumento select[id=Moneda]').val(moneda);
    $('#MontoIngreso').val(valorIngreso);
    $('#MontoIngreso').val(formatfloat($('#MontoIngreso').val(), 0));

    var jsonDate = fecIngreso.replace(/\D+/g, '');//reemplaza todos los caracteres a excepcion de numeros
    var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
    var fecha = ('0' + date.getDate()).slice(-2) + '/'
                        + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                        + date.getFullYear()

    $('#frmRecepcionDocumento input[id=FechaIngreso]').val(fecha);
}
	
function fnActualizarDocumentoCajaRecepcion() {
    var pclidValue = $("#Pclid").val() == "" ? $("#PclidHidden").val() : $("#Pclid").val()

    if ($("#MontoIngreso").val() == '0' || $("#MontoIngreso").val() == '' || $("#MontoIngreso").val() == '0,00' || pclidValue == '' || $("#Ctcid").val() == '' || $("#NumeroDocumento").val() == '') {
        alert("Debe ingresar todos los datos")
    }
    else {
        if (pclidValue == 90 && $("#Sbcid").val() == "")
            alert("Debe ingresar el asegurado")
        else
            $.ajax({
                type: 'POST',
                url: "/Caja/GuardarDocumentoCajaRecepcion/",
                dataType: 'json',
                async: true,
                data: {
                    DocumentoId: $("#DocumentoId").val(),
                    NumeroDocumento: $("#NumeroDocumento").val(),
                    Pclid: $("#Pclid").val() == "" ? $("#PclidHidden").val() : $("#Pclid").val(),
                    Ctcid: $("#Ctcid").val(),
                    Sbcid: $("#Sbcid").val(),
                    Moneda: $("#Moneda").val(),
                    MontoIngreso: $("#MontoIngreso").val()
                },
                success: function (data) {
                    if (data != -1) {
                        alert('Documento guardado con éxito.');

                    } else {
                        alert('Error al guardar Documento.');
                    }
                },
                complete: function () {
                    $('#ppDocumento').dialog('close');
                    jQuery("#gridTraspasoDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    
                }
            });
    }

}
function change_checkboxTraspasoDocumentos(el) {
    
    var currentCB = $(el);
    var $grid = jQuery('#gridTraspasoDocumentos');

    var isChecked = el.checked;
    if (currentCB.is(".groupHeader")) {	//if group header is checked, to check all child checkboxes		

        var checkboxes = currentCB.closest('tr').nextUntil('tr.gridTraspasoDocumentosghead_0').find('.cbox[type="checkbox"]');

        checkboxes.each(function () {
            if (!this.checked || !isChecked)
                $grid.setSelection($(this).closest('tr').attr('id'), true);
        });
        //$(this).prop("checked", true);
    } else {  //when child checkbox is checked
        var allCbs = currentCB.closest('tr').prevAll("tr.gridTraspasoDocumentosghead_0:first").
                        nextUntil('tr.'+ grilla + 'ghead_0').andSelf().find('[type="checkbox"]');
        var allSlaves = allCbs.filter('.cbox');
        var master = allCbs.filter(".groupHeader");

        var allChecked = !isChecked ? false : allSlaves.filter(":checked").length === allSlaves.length;
        master.prop("checked", allChecked);
    }

}

function fnOnSelectAllRowsGrid(aRowids, status) {
    //$("input.groupHeader").attr('checked', status);
}

function rowColorIngreso(grilla) {
    var $grid = jQuery("#" + grilla.id)
    var icolEstatusId = getColumnIndexByName($grid, 'EstatusId'),
        cRows = $grid[0].rows.length, iRow, row, className;
   
    for (iRow = 0; iRow < cRows; iRow++) {
        row = $grid[0].rows[iRow];
        className = row.className;

        if ($.inArray('jqgrow', className.split(' ')) > 0) { // $(row).hasClass('jqgrow')
            var colEstatusId = $(row.cells[icolEstatusId]).text();
            console.log(colEstatusId)
            if (colEstatusId == 3)
                if ($.inArray('rowEnIngreso', className.split(' ')) === -1)
                    row.className = className + ' rowEnIngreso';
        }
    }
}
function fnOnLoadCompleteDocsComercial(grilla, data) {
    rowColorIngreso(grilla);

}

function fnExcelDocumentosFinanzas() {
    var url = "/Caja/ExportToExcelDocumentosFinanzas";
    window.location.href = url;

}

lastSelArrRow = [],
//lastScrollLeft = 0,
lastScrollTop = 0,
lastSelRow = null,
saveSelection = function () {
    var $grid = $(this); // myGrid
    lastSelRow = $grid.jqGrid('getGridParam', 'selrow');
   
    //lastSelRow = this.p.selrow;
    lastSelArrRow = $grid.jqGrid('getGridParam', 'selrow');
    lastSelArrRow = lastSelArrRow ? $.makeArray(lastSelArrRow) : null;
    //lastSelArrRow = this.p.selarrrow ? $.makeArray(this.p.selarrrow) : null;
    lastScrollTop = $grid.closest(".ui-jqgrid-bdiv").scrollTop();//$grid.grid.bDiv.scrollLeft;
    
}

restoreSelection = function () {
    var i, l,
        p = this.p,
        $grid = $(this); // myGrid

    p.selrow = null;
    p.selarrrow = [];
   
    if (p.multiselect && lastSelArrRow && lastSelArrRow.length > 0) {
        for (i = 0, l = lastSelArrRow.length; i < l; i += 1) {
            if (lastSelArrRow[i] !== lastSelRow) {
                $grid.jqGrid("setSelection", lastSelArrRow[i], false);
            }
        }
        lastSelArrRow = [];
    }
    if (lastSelRow) {
        $grid.jqGrid("setSelection", lastSelRow, false);
        lastSelRow = null;
    }
    //this.grid.bDiv.scrollLeft = lastScrollLeft;
    $grid.closest(".ui-jqgrid-bdiv").scrollTop(lastScrollTop);
};

function fnloadCompleteGridTraspasoCtrlGestion(grilla) {
    restoreSelection.call(grilla)
}

function fnAbrirFormCargaMasiva() {
    var d = $("#ppCarga").dialog();

    fnLimpiarCarga();
    $('#ppCarga').dialog('open');
   

    

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

function fnCargarArchivo() {
    var traspaso;
    traspaso = $("#gridTraspasoDocumentos").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para hacer el traspaso");
    } else {
        
        var newUrl = "/Caja/ProcesoCargaMasiva/"
        //var postData = $("#frmCargaMasiva").serializeArray();
        var postData = {
            Pclid: $("#Pclid").val(), Contrato: $("#Contrato").val(), Archivo: $('#Archivo').val(),
            RutCliente: $("#RutCliente").val(), CodigoCarga: $("#CodigoCarga").val(),
            TipoCartera: $("#TipoCartera").val(),
            ArchivoQuiebra: $("#ArchivoQuiebra").prop('checked'),
            CargaJudicial: $("#CargaJudicial").prop('checked'),
            ids: JSON.stringify(traspaso)
        };
        if ($('#frmCargaMasiva').valid()) {
            $.ajax({
                type: 'POST',
                url: newUrl, // we are calling json method
                dataType: 'json',
                async: true,
                data: postData,
                beforeSend: function () { $("body").addClass("loading"); },
                success: function (resultData) {

                    if (resultData.success) {
                        jQuery("#gridTraspasoDocumentos").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                        $("body").removeClass("loading");
                        if (resultData.data.length > 0) {
                            for (var i = 0; i <= resultData.data.length; i++)
                                $("#grdCargaMasiva").jqGrid('addRowData', i + 1, resultData.data[i]);
                            alert('Archivo cargado con errores');
                        } else {
                            alert('Archivo cargado con exito');
                            $('#ppCarga').dialog('close');
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
        url: "/Caja/ListarCodigoCarga", // we are calling json method
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
        url: "/Caja/ListarContrato", // we are calling json method
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

function fnExcelDocumentosCargaMasiva() {
    var url = "/Caja/ExportToExcelDocumentosControlGestion";
    window.location.href = url;

}

function fnAprobarTraspasoFinanzas() {
    var traspaso;
    traspaso = $("#gridPanelAprobacion").jqGrid('getGridParam', 'selarrrow');
    if (traspaso == "") {
        alert("Debe seleccionar uno o mas documentos para aprobar");
    } else {
        var postData = {
            ids: JSON.stringify(traspaso)
        };
        $.ajax({
            type: 'POST',
            url: "/Caja/AprobacionTraspasoFinanzas/",
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
            data: postData,
            success: function (data) {
                $("body").removeClass("loading");

                if (data != '') {
                    $("body").removeClass("loading");
                    alert(data);
                }
            },
            error: function (ex) {
                $("body").removeClass("loading");
                alert('Error al realizar traspasos.' + ex);
            },
            complete: function () {
                jQuery("#gridPanelAprobacion").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    }
}

// DOCUMENTOS EN CUSTODIA
var allowSaving = function (rowid) {
    var item = $(this).jqGrid("getRowData", rowid);
    if (item.NumDoc !== "") {
            return confirm("Desea guardar el documento \"" +
                        "\" de la celda \"" +
                        item.id + "\"?");
    } else {
        return false;
    }
    //if (item.NumDoc !== "") {
    //    return confirm("Do you want to save \"" +
    //                item.ParameterValue + "\" value in \"" +
    //                item.ParameterName + "\"?");
    //} else {
    //    return false;
    //}
};

function fnAbrirFormularioDocumentoCustodia() {
    $('#ppAddDocumentoCustodia').dialog('open');
    fnRefreshDocumentosCustodia();

}
function fnRefreshDocumentosCustodia() {
    mydocumentosCustodia = [];
    $('#CantDocumentos').val('');
    $('#NombreRutClienteCustodia').val('');
    $('#PclidCustodia').val('');
    $('#NombreRutDeudorCustodia').val('');
    $('#CtcidCustodia').val('');
    $('#NombreRutGestorCustodia').val('');
    $('#GesidCustodia').val('');
    $("#gridDocumentosIngreso").jqGrid('clearGridData');
}
var delay = (function () {
    var timer = 0;
    return function (callback, ms) {
        clearTimeout(timer);
        timer = setTimeout(callback, ms);
    };
})();

function OnlyNumber(event) {
    var keycode = (event.which) ? event.which : event.keyCode;
    var charcode = event.charCode;
    if (!(event.shiftKey == false && (keycode == 46 || keycode == 8 || keycode == 37 || keycode == 39 || keycode == 0 || (keycode >= 48 && keycode <= 57)) && charcode != 46)) {
        event.preventDefault();
    }
  
}
var mydocumentosCustodia = [];
var t = false
function fnAddNumDocumentos(e) {
    var $this = $('#CantDocumentos')
    
    t = setInterval(

    function () {
        if ($this.val().length == 0) {
            $this.val(1)
        }
        if (($this.val() < 1 || $this.val() > 20) && $this.val().length != 0) {
            if ($this.val() < 1) {
                $this.val(1)
            }

            if ($this.val() > 20) {
                $this.val(20)
            }
        }

    }, 50)

    delay(function () {
        $('#CantDocumentos').val();

        // itemsCount = la cantidad de filas en el grid
        var itemsCount = $("#gridDocumentosIngreso").getGridParam("reccount");
        // si CantDocumentos es mayor que itemsCount, se agrega fila al grid
        if ($('#CantDocumentos').val() > itemsCount) {
            for (var i = 0; i < $('#CantDocumentos').val() - itemsCount; i++) {
                var item = itemsCount + i;
                mydocumentosCustodia.push({ AID: item, NumDoc: "", MontoDoc: "", FechaDoc: "", FechaProDoc: "" });
                $("#gridDocumentosIngreso").jqGrid('addRowData', item + 1, mydocumentosCustodia[item])
            }
        }// si CantDocumentos es menor que itemsCount, se remueven las filas, hasta igualar quantity ingresado 
        else {
            $.each($("#gridDocumentosIngreso").find('tr'), function (index, element) {
               
                if (index > $('#CantDocumentos').val())
                    $(element).slideUp(200, function () {
                        //element.remove();
                        $('#gridDocumentosIngreso').jqGrid('delRowData', index);
                    });
            });

            $.each(mydocumentosCustodia, function (index, value) {
                
                if (index >= $('#CantDocumentos').val())
                    mydocumentosCustodia.splice(index, 1);
            });
        }
        
        
        $("#CantDocumentos").blur();
    }, 500);

}

function blurCantDocumento() {
    if (t != false) {
        window.clearInterval(t)
        t = false;
    }
}

function selectAllFocus() {
    // $(this).select();
    $("#CantDocumentos").select();
}
var initDateSearch = function (elem) {
    setTimeout(function () {
        $(elem).datepicker({
            dateFormat: "dd-mm-yy",
            autoSize: true,
            changeYear: true,
            changeMonth: true,
            showWeek: false,
            showButtonPanel: true,
            //beforeShowDay: function (date) {
            //    var day = date.getDay();
            //    return [day == 1 || day == 2 || day == 3 || day == 4 || day == 5, ""];
            //},
            //maxDate: 0,
            onSelect: function (e) {
                $(this).focus();
                var $row = $(this).parents("tr");
                $("#gridDocumentosIngreso").jqGrid('saveRow', $row.attr("id"), false);
            },
            onClose: function (e) {
                $(this).focus();
            }
        });
    }, 50);
}

var eventAmountCustom = 
    function (elem) {
        $(elem).keyup(function (event) {

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
      
    }

var useCustomDialog = false, oldInfoDialog = $.jgrid.info_dialog;
$.extend($.jgrid,{
    info_dialog: function (caption, content, c_b, modalopt) {
        if (useCustomDialog) {
            // display custom dialog
            useCustomDialog = false;
            alert("ERROR: " + content);
        } else {
            return oldInfoDialog.apply (this, arguments);
        }
    }
});

function val1(val, colname) {
    if (val.length > 0) 
    {
        return [true];
    } else {
        useCustomDialog = true;
        return [false, " el campo '" +
                       colname + "'\nes requerido "];
    }
}

function myValidate1(value, colname) {
    var id = jQuery("#list").jqGrid('getGridParam', 'selrow');
    var fol = jQuery("#list").jqGrid('getRowData', id);
    if (value > fol.max)
        return [false, "Min value can't be < tha Max value"];
    else
        return [true, ""];

    if (value.length > 5) {
        return [false, " is too long for the column '" +
                       colname + "'\nMax 5 char is permitted"];
    } else {
        return [true, ""];
    }
}
function fnIngresarDoumentosCustodia() {
    var datafromgrid = $("#gridDocumentosIngreso").jqGrid('getGridParam', 'data');
    var rows = $("#gridDocumentosIngreso").jqGrid('getDataIDs');
    var hayData = 0
   
    for (var i = 0; i < rows.length; i++) {
        var numDoc = datafromgrid[i].NumDoc;
        var fecDoc = datafromgrid[i].FechaDoc;
        var montoDoc = datafromgrid[i].MontoDoc;

        if (numDoc.length > 0 && fecDoc.length > 0 && montoDoc.length > 0) {
            hayData ++;
        }
}

    if (hayData == 0) 
        alert("Debe ingresar documento")
    else
        if ($('#frmIngresoDocumento input[id=PclidCustodia]').val() == "")
            alert("Debe Ingresar el cliente!");
        else

            if ($('#frmIngresoDocumento input[id=CtcidCustodia]').val() == "")
                alert("Debe ingresar el deudor!")
            else

                if ($('#frmIngresoDocumento input[id=GesidCustodia]').val() == "")
                    alert("Debe ingresar el gestor")
                else {
                    var bancoSelected = $("#frmIngresoDocumento select[id=Banco]").find("option:selected");
                   
                    var postData = {
                        documentos: JSON.stringify(datafromgrid),
                        numCuenta: $("#NumCuentaHidden").val(),
                        pclid: $('#frmIngresoDocumento input[id=PclidCustodia]').val(),
                        ctcid: $('#frmIngresoDocumento input[id=CtcidCustodia]').val(),
                        gestorId: $('#frmIngresoDocumento input[id=GesidCustodia]').val(),
                        bancoId: $('#frmIngresoDocumento select[id=Banco]').val(),
                        recibe: $('#frmIngresoDocumento input[id=Recibe]').val(),
                        gestor: $('#frmIngresoDocumento input[id=NombreRutGestorCustodia]').val(),
                        deudor: $('#frmIngresoDocumento input[id=NombreRutDeudorCustodia]').val(),
                        cliente: $('#frmIngresoDocumento input[id=NombreRutClienteCustodia]').val(),
                        banco: bancoSelected.text()
                    };
                    $.ajax({
                        type: 'POST',
                        url: "/Tesoreria/InsertarDocumentoCustodia/",
                        dataType: 'json',
                        async: true,
                        data: postData,
                        success: function (data) {

                            if (data.result > 0) {
                                alert("Documentos ingresados con exito");
                                var blob = b64StrtoBlob(data.pdf, 'application/pdf');
                                var blobUrl = URL.createObjectURL(blob);
                                var content = String.format("<object data='{0}' type='application/pdf' width='100%' height='100%'></object>", blobUrl);
                                $("#ppDoctoCustodia").empty();
                                $("#ppDoctoCustodia").html(content);
                                $('#ppDoctoCustodia').dialog('open');
                            }
                        },
                        error: function (ex) {
                            alert('Error al guardar documentos.' + ex);
                        },
                        complete: function () {
                            $('#ppAddDocumentoCustodia').dialog('close');
                            jQuery("#gridDocumentosCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                        }
                    });
                }
 
}
function fnDoubleClickDocumentoCustodia(rowid) {
    var $grid = jQuery("#gridDocumentosCustodiados")
    var rowData = $grid.getRowData(rowid);
    //console.log(rowData)
    var custodiaId = rowData['CustodiaId'];
    var numeroDocumento = rowData['NumDocumento'];
    var montoDocumento = rowData['Monto'];
    var fechaDocumento = rowData['FecDoc'];
    var fechaProrroga = rowData['FecProrroga'];
    var estadoDocumento = rowData['EstadoId'];
    var pclid = rowData['Pclid'];
    var rutCliente = rowData['RutCliente'];
    var cliente = rowData['Cliente'];
    var ctcid = rowData['Ctcid'];
    var rutDeudor = rowData['RutDeudor'];
    var deudor = rowData['Deudor'];
    var gesid = rowData['GestorId'];
    var gestor = rowData['Gestor'];
    var banco = "2";//rowData['Gestor'];
    var giroA = rowData['GiradoA'];
    fnEditarDocumentoCustodia(custodiaId, numeroDocumento, montoDocumento, fechaDocumento, fechaProrroga, estadoDocumento,
                                pclid, rutCliente, cliente, ctcid, rutDeudor, deudor, gesid, gestor, banco, giroA);
}
function fnEditarDocumentoCustodia(custodiaId, numeroDocumento, montoDocumento, fechaDocumento, fechaProrroga, estadoDocumento,
                                pclid, rutCliente, cliente, ctcid, rutDeudor, deudor, gesid, gestor, banco, giroA) {
    var d = $("#ppConciliacionCustodia").dialog();

    $("#frmConciliacionCustodia").reset();
    $("#btnConciliacionCustodia").attr('disabled', 'disabled');
    $("#btnGuardarConciliaCustodia").removeAttr("disabled");
    $('#MontoDocumento').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia select[id=EstadoDocumento]').attr('disabled', 'disabled');
    $('#ppConciliacionCustodia').dialog('option', 'title', 'Documento ' + '' + ' Numero: ' + numeroDocumento + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    $('#ppConciliacionCustodia').dialog('open');
    $("#CustodiaId").val(custodiaId);
    $("#pclidHidden").val(pclid);
    $("#ctcidHidden").val(ctcid);
    $("#gesidHidden").val(gesid);
    $('#frmConciliacionCustodia input[id=NombreRutDeudorCC]').val(rutDeudor + " - " + deudor)
    $('#frmConciliacionCustodia input[id=NombreRutDeudorCC]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=NombreRutClienteCC]').val(rutCliente + " - " + cliente)
    $('#frmConciliacionCustodia input[id=NombreRutClienteCC]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=NombreRutGestorCC]').val(gestor)
    $('#frmConciliacionCustodia input[id=NombreRutGestorCC]').attr('disabled', 'disabled');

    $('#frmConciliacionCustodia select[id=Banco]').val(banco);
    $('#frmConciliacionCustodia select[id=Banco]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=GiroA]').val(giroA);
    $('#frmConciliacionCustodia input[id=GiroA]').attr('disabled', 'disabled');

    $('#frmConciliacionCustodia input[id=NumeroDocumento]').val(numeroDocumento);
    $('#frmConciliacionCustodia input[id=NumeroDocumento]').attr('disabled', 'disabled');
    $('#MontoDocumento').val(montoDocumento);
    $('#MontoDocumento').val(formatfloat($('#MontoDocumento').val(), 0));
        

    $('#FechaDocumento').val(fechaDocumento);

    $('#FechaProrroga').val(fechaProrroga);
    

    $('#frmConciliacionCustodia select[id=EstadoDocumento]').val(estadoDocumento);

    var newUrl = "/Tesoreria/ListarCartolaMovimientosPendienteGrilla/?"
    newUrl += "numCuenta=" + $("#NumCuentaHidden").val() + "&fechaDocumento=" + fechaDocumento + "&montoDocumento=" +  $('#MontoDocumento').val();
    jQuery("#gridMovimientosPendientes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
    
    $.ajax({
        type: 'POST',
        url: "/Tesoreria/ObtConciliacionNumComprobante/",
        dataType: 'json',
        async: true,
        success: function (result) {
            $('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val(result);
        }
    });
}
function fnGuardarConciliaCustodia() {
    var $grid = jQuery("#gridMovimientosPendientes")

    var ids = $("#gridMovimientosPendientes").jqGrid('getGridParam', 'selarrrow');
    //console.log(ids.length);
    var hayPositivo = 0;
    var hayNegativo = 0;
    if (ids.length > 0) {
        if (ids.length > 2) {
            alert("Solo puede seleccionar 2 Movimientos")
        } else {
            if (ids.length == 2) {
                //Se valida que un monto sea negativo y otro positivo, que la fecha del positivo sea menor a la fecha del negativo
                for (var i = 0; i < ids.length; i++) {
                    var rowData = $grid.getRowData(ids[i]);
                    var montoMovimiento = parseInt(rowData['Monto'], 10);
                    if (montoMovimiento < 0)
                        hayNegativo = 1;
                    else
                        hayPositivo = 1
                    
                }
                if (hayNegativo == 1 && hayPositivo == 1){
                    //Se protesta el documento
                    $.ajax({
                        type: 'POST',
                        url: "/Tesoreria/ActualizarEstadoDocumentoCustodia/",
                        dataType: 'json',
                        async: true,
                        data: {
                            custodiaId: $('#CustodiaId').val(),
                            tipoEstadoId: 3 //PROTESTADO
                        },
                        success: function (data) {

                            if (data > 0) {
                                $('#frmConciliacionCustodia select[id=EstadoDocumento]').val("3");
                                alert("Documento protestado");
                                //Protestar movimientos
                                fnCustodiaProtestar(ids);

                            } else {
                                alert("Error al protestar documento")

                            }
                        },
                        complete: function () {
                            fnUpdateFormularioConciliacionCustodia();
                        }
                    });
                } else {
                    alert("Debe Seleccionar un monto positivo y otro negativo")
                }
            }
            else {
                if (ids.length == 1) {
                    var rowData = $grid.getRowData(ids[0]);
                    var montoMovimiento = parseInt(rowData['Monto'], 10);
                    if (montoMovimiento < 0)
                        alert("El monto no puede ser negativo")
                    else {
                        //se Guarda y procede a conciliar
                        if ($('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val() != '') {
                            $.ajax({
                                type: 'POST',
                                url: "/Tesoreria/ActualizarEstadoDocumentoCustodia/",
                                dataType: 'json',
                                async: true,
                                data: {
                                    custodiaId: $('#CustodiaId').val(),
                                    tipoEstadoId: 1 //LIBERADO
                                },
                                success: function (data) {

                                    if (data > 0) {
                                        $('#frmConciliacionCustodia select[id=EstadoDocumento]').val("1");
                                        $("#btnConciliacionCustodia").removeAttr("disabled");
                                        
                                        alert("Documento liberado, proceda a Conciliar");
                                        
                                    } else {
                                        alert("Error al crear la conciliación")

                                    }
                                },
                                complete: function () {
                                    fnUpdateFormularioConciliacionCustodia();
                                }
                            });
                            
                        }
                        else {
                            alert("Ingrese el numero de comprobante")
                        }
                        
                    }
                }
            }
        }
    }
//    var fechaMovimiento = rowData['FecMovimiento'];
}
function fnUpdateFormularioConciliacionCustodia() {
    //Se bloquea guardar
    if ($('#frmConciliacionCustodia select[id=EstadoDocumento]').val() == "3") {
        $("#btnGuardarConciliaCustodia").attr('disabled', 'disabled');
    }
    
}
function fnConciliarDocumentoCustodia() {
    var $grid = jQuery("#gridMovimientosPendientes")

    var ids = $("#gridMovimientosPendientes").jqGrid('getGridParam', 'selarrrow');
    console.log(ids.length);
    var hayPositivo = 0;
    var hayNegativo = 0;
    if (ids.length > 0) {
        if (ids.length > 2) {
            alert("Solo puede seleccionar 2 Movimientos")
        } else {
            if (ids.length == 2) {
                //Se valida que un monto sea negativo y otro positivo, que la fecha del positivo sea menor a la fecha del negativo
                for (var i = 0; i < ids.length; i++) {
                    var rowData = $grid.getRowData(ids[i]);
                    var montoMovimiento = parseInt(rowData['Monto'], 10);
                    if (montoMovimiento < 0)
                        hayNegativo = 1;
                    else
                        hayPositivo = 1

                }
                if (hayNegativo == 1 && hayPositivo == 1) {
                    alert("El documento se debe protestar, presione guardar")
                } else {
                    alert("Debe Seleccionar un monto positivo y otro negativo")
                }
            }
            else {
                if (ids.length == 1) {
                    var rowData = $grid.getRowData(ids[0]);
                    var montoMovimiento = parseInt(rowData['Monto'], 10);
                    if (montoMovimiento < 0)
                        alert("El monto no puede ser negativo")
                    else {
                        //se Guarda y procede a conciliar
                        if ($('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val() != '') {
                            var movimientoId = rowData['MovimientoId'];
                            $.ajax({
                                type: 'POST',
                                url: "/Tesoreria/ExisteConciliacionComprobante/",
                                dataType: 'json',
                                async: true,
                                data: {
                                    numComprobante: $('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val()
                                },
                                success: function (data) {
                                    //Si es cero(0) no existe el comprobante
                                    if (data == 0) {
                                        $.ajax({
                                            type: 'POST',
                                            url: "/Tesoreria/InsertarConciliacionCustodia/",
                                            dataType: 'json',
                                            async: true,
                                            data: {
                                                movimientoId: movimientoId,
                                                numComprobante: $('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val(),
                                                custodiaId: $('#CustodiaId').val(),
                                                pclid: $('#pclidHidden').val(),
                                                ctcid: $('#ctcidHidden').val(),
                                                gestorId: $('#gesidHidden').val(),
                                                tipoConciliacion: $('#TipoConciliacion').val(),
                                                numCuenta: $("#NumCuentaHidden").val()
                                            },
                                            success: function (result) {
                                                if (result.success) {
                                                    alert("Registro conciliado con éxito");
                                                    //var blob = b64StrtoBlob(result.pdf, 'application/pdf');
                                                    //var blobUrl = URL.createObjectURL(blob);
                                                    //var content = String.format("<object data='{0}' type='application/pdf' width='100%' height='100%'></object>", blobUrl);
                                                    //$("#ppDoctoCustodia").empty();
                                                    //$("#ppDoctoCustodia").html(content);
                                                    //$('#ppDoctoCustodia').dialog('open');
                                                } else {
                                                    alert("Error al crear la conciliación")
                                                }
                                            },
                                            complete: function () {
                                                $('#ppConciliacionCustodia').dialog('close');
                                                //Actualizo grid de documentos custodia
                                                jQuery("#gridDocumentosCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]); // documentosCustodia
                                                //Actualizo grid conciliacion
                                                //GridConciliacion
                                                var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
                                                newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
                                                jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                                                //Actualizo grid interno
                                                var newUrl = "/Tesoreria/ListarCartolaMovimientosPendienteGrilla/?"
                                                newUrl += "numCuenta=" + $("#NumCuentaHidden").val() + "&fechaDocumento=" + $('#FechaDocumento').val() + "&montoDocumento=" + $('#MontoDocumento').val();
                                                jQuery("#gridMovimientosPendientes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
                                                
                                            }
                                        });
                                    } else {
                                        if (data > 0)
                                            alert("El número de comprobante ingresado, ya se encuentra registrado en el sistema")

                                    }
                                }
                            });
                        }
                        else alert("Ingrese el numero de comprobante")
                    }
                }
            }
        }
    }
    
}

function fnCustodiaProtestar(ids) {
    var postData = {
        ids: JSON.stringify(ids),
        numCuenta: $('#NumCuentaHidden').val(),
        cuentaId: $('#IdCuentaHidden').val()
    };
    $.ajax({
        type: 'POST',
        url: "/Tesoreria/TraspasoPanelProtestado/",
        dataType: 'json',
        async: true,
        data: postData,
        success: function (data) {
            if (data != -1) {
                alert('Los movimientos fueron enviado al panel de protestados');

            } else {
                alert('Error al realizar envio a panel de protestados.');
            }
        },
        error: function (ex) {
            alert('Error al realizar envio a panel de protestados.' + ex);
        },
        complete: function () {
            //Actualizo grid interno
            var newUrl = "/Tesoreria/ListarCartolaMovimientosPendienteGrilla/?"
            newUrl += "numCuenta=" + $("#NumCuentaHidden").val() + "&fechaDocumento=" + $('#FechaDocumento').val() + "&montoDocumento=" + $('#MontoDocumento').val();
            jQuery("#gridMovimientosPendientes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
            //Actualizo grid conciliacion
            //GridConciliacion
            var newUrl = "/Tesoreria/ListarCartolaMovimientosGrilla/?"
            newUrl += "numCuenta=" + $("#NumCuentaHidden").val();
            jQuery("#gridCartolaMovimientos").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
            
            //Actualizo panel protestado
            var newUrlDoc = "/Tesoreria/ListarMovimientosProtestadosGrilla/?"
            newUrlDoc += "numCuenta=" + $("#NumCuentaHidden").val();
            jQuery("#gridMovimientosProtestados").jqGrid().setGridParam({ url: newUrlDoc }).trigger('reloadGrid', [{ page: 1 }]);
        }
    });
}

function fnCloseConciliacionCustodia() {
    //Actualizo grid de documentos custodia
    jQuery("#gridDocumentosCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]); // documentosCustodia

}
function fnBotonEditarLiquidacion(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" title="Liquidacion" class="ui-icon ui-icon-pencil" style="height:20px;width:20px" onclick="fnMostrarDocumentosLiquidacion(\'' + rowobject[2] + '\',\'' + rowobject[5] + '\',\'' + rowobject[6]  + '\');" >Liquidar</button></div></div></div>';
}
function fnDoubleClickLiquidacion(rowid) {
    var $grid = jQuery("#gridMovimientosConciliados")
    var rowData = $grid.getRowData(rowid);
    //console.log(rowData)
    var conciliacionId = rowData['ConciliacionId'];
    var pclid = rowData['Pclid'];
    var ctcid = rowData['Ctcid'];
    
    fnMostrarDocumentosLiquidacion(conciliacionId, pclid, ctcid);
}
function fnMostrarDocumentosLiquidacion(conciliacionId, pclid, ctcid) {
    var d = $("#ppFormLiquidacionDocs").dialog();

    $("#frmLiquidacionDocumento").reset();
    idsOfSelectedRowsCcbid =  [];
    $('#ppFormLiquidacionDocs').dialog('open');
    fnCargarFormularioLiquidacion(conciliacionId, pclid, ctcid);
}
function fnCargarFormularioLiquidacion(conciliacionId, pclid, ctcid) {
    var DTO = {
        conciliacionId: conciliacionId,
        pclid: pclid,
        ctcid: ctcid
    };
    $.ajax({
        url: '/Tesoreria/verFormularioLiquidacion',
        type: "GET",
        dataType: "html",
        data: DTO,
        cache: false,
        beforeSend: function () { $('#loadingmessageLiqui').css('display', 'block') },
        success: function (data) {
            //Fill div with results
            $("#ppFormLiquidacionDocs").html(data);
            $('#loadingmessageLiqui').css('display', 'none')
        },
        error: function (xhr, status, error) {
            $('#loadingmessageLiqui').css('display', 'none')
            alert(xhr.responseText);
        }
    });
}
var $htable, $ftable, iCheckbox
function fnOnLoadGridDocsLiquidacion(grilla) {
    var $grid = jQuery("#" + grilla.id)
    if (iCheckbox === undefined) {
        iCheckbox = getColumnIndexByName($grid, "cb");
    }
    if (iCheckbox >= 0) {
        // if multiselect:true are used, iCheckbox will be 1 in case of rownumbers:true
        // and iCheckbox=0 in case of rownumbers:false

        // first we need to place checkboxes from the table body on the last place
        moveTableColumn($grid[0].rows, iCheckbox, "jqgrow");
    }
}
function fnBotonVerComprobanteLiquidacion(cellvalue, options, rowdata) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" title="ver comprobante" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnMostrarComprobanteImputacion(\'' + rowdata[2] + '\')" >Liquidar</button></div></div></div>';
}

// paginacion Liquidacion documentos
var idsOfSelectedRowsCcbid = [];

function UpdateidsOfSelectedRowsCcbid(id, isSelected) {
    var index = $.inArray(id, idsOfSelectedRowsCcbid);
    if (!isSelected && index >= 0) {
        idsOfSelectedRowsCcbid.splice(index, 1); // remove id from the list
    } else if (index < 0) {
        if (id.toLowerCase().indexOf("ghead") < 0)
            idsOfSelectedRowsCcbid.push(id);
    }
    $("#Docs").val(idsOfSelectedRowsCcbid);
}

function fnOnSelectAllCcbid(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateidsOfSelectedRowsCcbid(id, status);
    }
}

function fnOnLoadCompleteCcbid(grilla) {
    var i, count;
    for (i = 0, count = idsOfSelectedRowsCcbid.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRowsCcbid[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsCcbid[i]).prop("checked", true);
    }
   
}

function fnVerFormularioImputacion() {
    var d = $("#ppFormImputacionDocs").dialog();

    $("#frmImputacionDocumento").reset();

    //$('#ppConciliacionCustodia').dialog('option', 'title', 'Documento ' + '' + ' Numero: ' + numeroDocumento + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    var ids;
    ids = $("#gridDocumentoDeudoresLiquidacion").jqGrid('getGridParam', 'selarrrow');
    if (ids.length > 0) {
        
        $('#ppFormImputacionDocs').dialog('open');
        
        var postData = $("#frmLiquidacionDocumento").serializeArray();
        $.ajax({
            url: '/Tesoreria/verFormularioImputacion',
            type: "POST",
            dataType: 'html',
            async: true,
            data: postData,
            cache: false,
            beforeSend: function () { $('#loadingmessage').css('display', 'block')},
            success: function (data) {
                //Fill div with results
                $("#ppFormImputacionDocs").html(data);
                
                $('#loadingmessage').css('display', 'none');
            },
            error: function (xhr, status, error) {
                $('#loadingmessage').css('display', 'none');
                alert(xhr.responseText);
            }
        });
    }
    else
        alert("Debe seleccionar uno o mas documentos para aprobar");
   
}
function fnCambiarTipoImputacion(valueCombo) {
    if (valueCombo == '1') {
        var DTO = {
            conciliacionId: $("#IConciliacionId").val(),
            pclidLiqui: $("#IpclidLiqui").val(),
            ctcidLiqui: $("#IctcidLiqui").val(),
            Docs: $("#Documentos").val()
        };
        $.ajax({
            url: '/Tesoreria/verFormularioImputacion',
            type: "POST",
            dataType: "html",
            data: DTO,
            cache: false,
            success: function (data) {
                //Fill div with results
                $("#ppFormImputacionDocs").html(data);

            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
    }
    else {
        $("#ICapitalPor").val("0");
        $("#ICapital").val("0");
        $("#IInteresPor").val("0");
        $("#IInteres").val("0");
        $("#IHonorarioPor").val("0");
        $("#IHonorario").val("0");
        $("#IGastoPre").val("0");
        $("#IGastoJud").val("0")

        fnAplicarImputacion();
    }
}
var sumImputacion = 0;
var idsOfSelectedRowsFinalizar = [];
function fnAplicarImputacion() {
    
    if (parseFloat($("#CalculoImputar").val()) > parseFloat($("#ISaldoRebajar").val().replace(".", "").replace(",00", "").replace(/^0+/, "").replace(/\D/g, ""))) {
        alert("El total de los valores ingresados, " + parseFloat($("#CalculoImputar").val()) + " supera el monto a rebajar " + parseFloat($("#ISaldoRebajar").val().replace(".", "").replace(",00", "").replace(/^0+/, "").replace(/\D/g, "")))
    }
    else
    {
        var grid = $("#gridDocumentoDeudoresImputacion")
        var selRowIds = grid.jqGrid("getGridParam", "selarrrow");
        if ($("#ICriterio").val() == "2")
            $("#DocFinalizar").val(selRowIds);
        else
            $("#DocFinalizar").val("");

        idsOfSelectedRowsFinalizar = selRowIds.slice(0);
        var postData = {
            INumComprobante: $("#INumComprobante").val(),
            IRutCliente: $("#IRutCliente").val(),
            ICliente: $("#ICliente").val(),
            IRutDeudor: $("#IRutDeudor").val(),
            IDeudor: $("#IDeudor").val(),
            IMonto: $("#IMonto").val(),
            ISaldo: $("#ISaldo").val(),
            ICapital: $("#ICapital").val() == '' ? 0 : $("#ICapital").val(),
            IInteres: $("#IInteres").val() == '' ? 0 : $("#IInteres").val(),
            IHonorario: $("#IHonorario").val() == '' ? 0 : $("#IHonorario").val(),
            IGastoPre: $("#IGastoPre").val() == '' ? 0 : $("#IGastoPre").val(),
            IGastoJud: $("#IGastoJud").val() == '' ? 0 : $("#IGastoJud").val(),
            ICapitalPor: $("#ICapitalPor").val() == '' ? 0 : $("#ICapitalPor").val(),
            IInteresPor: $("#IInteresPor").val() == '' ? 0 : $("#IInteresPor").val(),
            IHonorarioPor: $("#IHonorarioPor").val() == '' ? 0 : $("#IHonorarioPor").val(),
            ICriterio: $("#ICriterio").val(),
            IpclidLiqui: $("#IpclidLiqui").val(),
            IctcidLiqui: $("#IctcidLiqui").val(),
            IConciliacionId: $("#IConciliacionId").val(),
            Documentos: $("#Documentos").val(),
            DocFinalizar: $("#DocFinalizar").val()
        }

        $.ajax({
            url: '/Tesoreria/verFormularioImputacionManual',
            type: "POST",
            dataType: "html",
            data: postData,
            cache: false,
            success: function (data) {
                //Fill div with results
                $("#ppFormImputacionDocs").html(data);
                //habilitar
                $("#ICapitalPor").removeAttr("disabled");
                $("#IInteresPor").removeAttr("disabled");
                $("#IHonorarioPor").removeAttr("disabled");
                $("#IGastoPre").removeAttr("disabled");
                $("#IGastoJud").removeAttr("disabled");

                $('#ICapitalPor').keyup(function (event) {
                    // skip for arrow keys
                    if (event.which >= 37 && event.which <= 40) return;
                    // format number
                    $(this).val(function (index, value) {
                        $("#ICapital").val(value.replace(/^0+/, "")
                                            .replace(/\D/g, "")
                                            .replace(/\B(?=(\d{3})+(?!\d))/g, "."));

                        return value
                        .replace(/^0+/, "")
                        .replace(/\D/g, "")
                        .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                        ;

                    });
                });

                $('#IInteresPor').keyup(function (event) {
                    // skip for arrow keys
                    if (event.which >= 37 && event.which <= 40) return;
                    // format number
                    $(this).val(function (index, value) {
                        $("#IInteres").val(value.replace(/^0+/, "")
                                            .replace(/\D/g, "")
                                            .replace(/\B(?=(\d{3})+(?!\d))/g, "."));

                        return value
                        .replace(/^0+/, "")
                        .replace(/\D/g, "")
                        .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                        ;

                    });
                });

                $('#IHonorarioPor').keyup(function (event) {
                    // skip for arrow keys
                    if (event.which >= 37 && event.which <= 40) return;
                    // format number
                    $(this).val(function (index, value) {
                        $("#IHonorario").val(value.replace(/^0+/, "")
                                            .replace(/\D/g, "")
                                            .replace(/\B(?=(\d{3})+(?!\d))/g, "."));
                        return value
                        .replace(/^0+/, "")
                        .replace(/\D/g, "")
                        .replace(/\B(?=(\d{3})+(?!\d))/g, ".")
                        ;

                    });
                });

                $('#IGastoPre').keyup(function (event) {
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

                $('#IGastoJud').keyup(function (event) {
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
            },
            error: function (xhr, status, error) {
                alert(xhr.responseText);
            }
        });
       
    }
}

function rowColorImputado(grilla) {
    var $grid = jQuery("#" + grilla.id)
    var icolImputadoId = getColumnIndexByName($grid, 'IndicaImputado'),
        cRows = $grid[0].rows.length, iRow, row, className;

    for (iRow = 0; iRow < cRows; iRow++) {
        row = $grid[0].rows[iRow];
        className = row.className;

        if ($.inArray('jqgrow', className.split(' ')) > 0) { // $(row).hasClass('jqgrow')
            var colEstatusId = $(row.cells[icolImputadoId]).text();
            if (colEstatusId == 1)
                if ($.inArray('rowImputado', className.split(' ')) === -1)
                    row.className = className + ' rowImputado';
        }
    }
}
function fnOnLoadCompleteDocsImputado(grilla) {
    rowColorImputado(grilla);

    var $grid = jQuery("#" + grilla.id),
        sumMonto = $grid.jqGrid("getCol", "Monto", false, "sum");
        sumSaldo = $grid.jqGrid("getCol", "Saldo", false, "sum");
        sumIntereses = $grid.jqGrid("getCol", "Intereses", false, "sum");
        sumHonorarios = $grid.jqGrid("getCol", "Honorarios", false, "sum");
        sumGastoJudicial = $grid.jqGrid("getCol", "GastoJudicial", false, "sum");
        sumGastoPrejudicial = $grid.jqGrid("getCol", "GastoPrejudicial", false, "sum");
        sumTotalDeuda = $grid.jqGrid("getCol", "TotalDeuda", false, "sum");
        $grid.jqGrid("footerData", "set", {
            Moneda: "Total:",
            Monto: sumMonto,
            Saldo: sumSaldo,
            Intereses: sumIntereses,
            Honorarios: sumHonorarios,
            GastoJudicial: sumGastoJudicial,
            GastoPrejudicial: sumGastoPrejudicial,
            TotalDeuda: sumTotalDeuda
        });
        var i, count;
        for (i = 0, count = idsOfSelectedRowsFinalizar.length; i < count; i++) {

            $grid.setSelection(idsOfSelectedRowsFinalizar[i], false);
            $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsFinalizar[i]).prop("checked", true);
        }
}

function fnGuardarImputacion() {
    var grid = $("#gridDocumentoDeudoresImputacion")
    var idsFinalizar = [];
   
    var datafromgrid = jQuery("#gridDocumentoDeudoresImputacion").getRowData();
    var rowData = [];
    for (var i = 0; i < datafromgrid.length; i++) {
        var imputado = datafromgrid[i].IndicaImputado
        if (imputado == "1") {
            var row = datafromgrid[i];
            rowData.push(row);
        }
        if (datafromgrid[i].Saldo == 0 && $('#ICriterio').val() == '2') {
            var row = datafromgrid[i];
            idsFinalizar.push(row);
        }
    }

    if (rowData.length > 0) {
        var postData = {
            documentos: JSON.stringify(rowData),
            conciliacionId: $('#IConciliacionId').val(),
            pclid: $('#IpclidLiqui').val(),
            ctcid: $('#IctcidLiqui').val(),
            docfinalizar: JSON.stringify(idsFinalizar)
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/InsertarDocumentoImputado/",
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $('#btnGuardarImputacion').css('display', 'none') },
            success: function (data) {

                if (data > 0) {
                    alert("Documentos imputados con exito");
                }
            },
            error: function (ex) {
                $('#btnGuardarImputacion').css('display', 'block')
                alert('Error al guardar documentos.' + ex);
            },
            complete: function () {
                $('#btnGuardarImputacion').css('display', 'block')
                $('#ppFormImputacionDocs').dialog('close');
                fnCargarFormularioLiquidacion($('#IConciliacionId').val(), $('#IpclidLiqui').val(), $('#IctcidLiqui').val());
                fnAprobarConciliacion();
                idsOfSelectedRowsCcbid = [];
                idsOfSelectedRowsFinalizar = [];
                $("#DocFinalizar").val("");
            }
        });
    }
    else {
        alert("Deben haber al menos un documento imputado")
    }
}

function fnAprobarConciliacion() {
    //if ($("#EstadoLiquidacionId").val() == "2")
    jQuery("#gridMovimientosConciliados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
    jQuery("#gridMovimientosApr").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
}
function fnFinalizarDocumentos() {
    var grid = $("#gridDocumentoDeudoresLiquidacion")
   
    //var idsFinalizar = grid.jqGrid('getGridParam', 'selarrrow');
    var rowData = [];
    var i, selRowIds = grid.jqGrid("getGridParam", "selarrrow"), n, row;
    for (i = 0, n = selRowIds.length; i < n; i++) {
        row = grid.jqGrid("getRowData", selRowIds[i]);
        if (row.Saldo == 1)
            rowData.push(row);
        // one can uses the data here
    }
   

    if (rowData.length > 0) {
        var postData = {
            conciliacionId: $('#conciliacionId').val(),
            pclid: $('#pclidLiqui').val(),
            ctcid: $('#ctcidLiqui').val(),
            docfinalizar: JSON.stringify(rowData)
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/FinalizarDocumentos/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {

                if (data > 0) {
                    alert("Documentos finalizados con exito");
                }
            },
            error: function (ex) {
                alert('Error al finalizar documentos.' + ex);
            },
            complete: function () {
                fnCargarFormularioLiquidacion($('#conciliacionId').val(), $('#pclidLiqui').val(), $('#ctcidLiqui').val());
                
            }
        });
    }
    else {
        alert("Deben haber al menos un documento imputado")
    }
}
function fnAbrirFormPagoManual() {

    $('#ppAddPagoManual').dialog('open');
    $('#frmPagoManual input[id=Ctcid]').val('')
    $('#frmPagoManual input[id=PclidPM]').val('')
    $('#frmPagoManual input[id=Fecha]').val('')
    $('#frmPagoManual input[id=Monto]').val('')
    $('#frmPagoManual input[id=NombreRutClientePM]').val('')
    $('#frmPagoManual input[id=NombreRutDeudor]').val('')
    $('#TipoConciliacion').val('1')
}

function fnInsertarPagoManualReportado() {

    if ($('#frmPagoManual input[id=PclidPM]').val() == "")
        alert("Debe Ingresar el cliente!");
    else

        if ($('#frmPagoManual input[id=Ctcid]').val() == "")
            alert("Debe ingresar el deudor!")
        else
            if ($('#frmPagoManual input[id=Fecha]').val() == "")
                alert("Debe ingresar la fecha!")
            else
                if ($('#frmPagoManual input[id=Monto]').val() == "")
                    alert("Debe ingresar el monto!")
                else
                    //Se procee a conciliar movimiento
                    $.ajax({
                        type: 'POST',
                        url: "/Tesoreria/InsertarPagoManual/",
                        dataType: 'json',
                        async: true,
                        data: {
                            pclid: $('#frmPagoManual input[id=PclidPM]').val(),
                            ctcid: $('#frmPagoManual input[id=Ctcid]').val(),
                            fecha: $('#frmPagoManual input[id=Fecha]').val(),
                            monto: $('#frmPagoManual input[id=Monto]').val(),
                            tipoConciliacion: $('#TipoConciliacion').val()
                        },
                        success: function (data) {

                            if (data > 0) {
                                alert("Registro ingresado con éxito");
                                $('#ppAddPagoManual').dialog('close');
                                jQuery("#gridMovimientosConciliados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);

                            } else {
                                alert("Error al crear el pago")

                            }
                        }
                    });

}

function fnEventsfrmIngresoManual() {
    $('#Monto').keyup(function (event) {

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

}
// paginacion Remesas
var idsOfSelectedRowsRemesas = [];

function UpdateidsOfSelectedRowsRemesas(id, isSelected) {
    var index = $.inArray(id, idsOfSelectedRowsRemesas);
    if (!isSelected && index >= 0) {
        idsOfSelectedRowsRemesas.splice(index, 1); // remove id from the list
    } else if (index < 0) {
        if (id.toLowerCase().indexOf("ghead") < 0)//contains
            idsOfSelectedRowsRemesas.push(id);
    }
    $("#IdsConciliacion").val(idsOfSelectedRowsRemesas);
}

function fnOnSelectAllRemesas(aRowids, status) {
    var i, count, id;
    for (i = 0, count = aRowids.length; i < count; i++) {
        id = aRowids[i];
        UpdateidsOfSelectedRowsRemesas(id, status);
    }
}


function fnOnLoadCompleteConciliaIds(grilla) {
    
    var $grid = jQuery("#" + grilla.id),
        sumCapital = $grid.jqGrid("getCol", "Capital", false, "sum");
    sumInteres = $grid.jqGrid("getCol", "Interes", false, "sum");
    sumHonorarios = $grid.jqGrid("getCol", "Honorarios", false, "sum");
    sumOtrosGastos = $grid.jqGrid("getCol", "OtrosGastos", false, "sum");
    sumMontoRecuperado = $grid.jqGrid("getCol", "MontoRecuperado", false, "sum");
  
    $grid.jqGrid("footerData", "set", {
        Deudor: "Total:",
        Capital: sumCapital,
        Interes: sumInteres,
        Honorarios: sumHonorarios,
        OtrosGastos: sumOtrosGastos,
        MontoRecuperado: sumMontoRecuperado
    });

    var i, count;
    for (i = 0, count = idsOfSelectedRowsRemesas.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRowsRemesas[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRowsRemesas[i]).prop("checked", true);
    }
}
function fnCargarGridConciliacionAprobados() {
    if ($('#Pclid').val() != '') {
        idsOfSelectedRowsRemesas = [];
        var newUrl = "/Tesoreria/ListarMovimientosConciliadoAprobadoGrilla/?"
        newUrl += "pclid=" + $("#Pclid").val();
        jQuery("#gridMovimientosConciliadosAprobado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
       
    }
}

function fnChangecheckGroupGridLiqDocs(el) {
    var currentCB = $(el);
    var $grid = jQuery('#gridDocumentoDeudoresLiquidacion');

    var isChecked = el.checked;
    if (currentCB.is(".groupHeader")) {	//if group header is checked, to check all child checkboxes		

        var checkboxes = currentCB.closest('tr').nextUntil('tr.gridDocumentoDeudoresLiquidacionghead_0').find('.cbox[type="checkbox"]');

        checkboxes.each(function () {
            if (!this.checked || !isChecked)
                $grid.setSelection($(this).closest('tr').attr('id'), true);
        });
        //$(this).prop("checked", true);
    } else {  //when child checkbox is checked
        var allCbs = currentCB.closest('tr').prevAll("tr.gridDocumentoDeudoresLiquidacionghead_0:first").
                        nextUntil('tr.gridDocumentoDeudoresLiquidacionghead_0').andSelf().find('[type="checkbox"]');
        var allSlaves = allCbs.filter('.cbox');
        var master = allCbs.filter(".groupHeader");

        var allChecked = !isChecked ? false : allSlaves.filter(":checked").length === allSlaves.length;
        master.prop("checked", allChecked);
    }

}

function fnVerFormularioRemesa() {
    var d = $("#ppFormCalculoRemesa").dialog();

    $("#frmCalculoRemesas").reset();
    if ($("#Pclid").val() != '') {
        var ids;
        ids = $("#gridMovimientosConciliadosAprobado").jqGrid('getGridParam', 'selarrrow');
        if (ids.length > 0) {

            $('#ppFormCalculoRemesa').dialog('open');

            var postData = $("#frmGridRemesas").serializeArray();
            $.ajax({
                url: '/Tesoreria/verCalculoRemesa',
                type: "POST",
                dataType: 'html',
                async: true,
                data: postData,
                cache: false,
                beforeSend: function () { $('#loadingmessage').css('display', 'block') },
                success: function (data) {
                    //Fill div with results
                    $("#ppFormCalculoRemesa").html(data);

                    $('#loadingmessage').css('display', 'none');
                },
                error: function (xhr, status, error) {
                    $('#loadingmessage').css('display', 'none');
                    alert(xhr.responseText);
                }
            });
        }
        else
            alert("Debe seleccionar uno o mas comprobantes para generar remesa");
    }
    else
        alert("Debe ingresar el Cliente")
}
function fnOnLoadCompleteGridCalculoRemesa(grilla) {
    var $grid = jQuery("#" + grilla.id),
        sumSaldo = $grid.jqGrid("getCol", "Capital", false, "sum");
    sumIntereses = $grid.jqGrid("getCol", "Interes", false, "sum");
    sumHonorarios = $grid.jqGrid("getCol", "Honorario", false, "sum");
    sumRecuperadoGasto = $grid.jqGrid("getCol", "RecuperadoGasto", false, "sum");
    sumTotalRecuperado = $grid.jqGrid("getCol", "TotalRecuperado", false, "sum");
    sumGananciaCapital = $grid.jqGrid("getCol", "GananciaCapital", false, "sum");
    sumGananciaInteres = $grid.jqGrid("getCol", "GananciaInteres", false, "sum");
    sumGananciaHonorario = $grid.jqGrid("getCol", "GananciaHonorario", false, "sum");
    sumTotalGanancia = $grid.jqGrid("getCol", "TotalGanancia", false, "sum");
    sumTotalCliente = $grid.jqGrid("getCol", "TotalCliente", false, "sum");
    $grid.jqGrid("footerData", "set", {
        NumDocumento: "Totales:",
        Capital: sumSaldo,
        Interes: sumIntereses,
        Honorario: sumHonorarios,
        RecuperadoGasto: sumRecuperadoGasto,
        TotalRecuperado: sumTotalRecuperado,
        GananciaCapital: sumGananciaCapital,
        GananciaInteres: sumGananciaInteres,
        GananciaHonorario: sumGananciaHonorario,
        TotalGanancia: sumTotalGanancia,
        TotalCliente: sumTotalCliente
    });
        
}
function fnBotonVerComprobanteGridRemesa(cellvalue, options, rowdata) {
    //alert(rowobject);
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" title="ver comprobante" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnMostraComprobanteLiquidacion(\'' + rowdata + '\')" ></button></div></div></div>';
}
function fnMostraComprobanteRemesa(datos) {

}
function fnGenerarRemesa() {
   
    var datafromgrid = jQuery("#gridCalculoRemesa").getRowData();
    //alert(JSON.stringify(datafromgrid))
    if (datafromgrid.length > 0) {
        var postData = {
            documentos: JSON.stringify(datafromgrid)
       
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/InsertarRemesa/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {

                if (data > 0) {
                    alert("Remesa Generada con exito");
                } else {
                    alert("Error al guardar remesa");
                }
            },
            error: function (ex) {
                alert('Error al guardar remesa.' + ex);
            },
            complete: function () {
                $('#ppFormCalculoRemesa').dialog('close');
                jQuery("#gridMovimientosConciliadosAprobado").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                jQuery("#gridRemesasGeneradas").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    }
    else {
        alert("Deben haber al menos un documento imputado")
    }
}
function fnCargarGestorAddEfectivo() {
    if ($('#frmIngresoEfectivo input[id=CtcidEfectivo]').val() != '' && $('#frmIngresoEfectivo input[id=PclidEfectivo]').val() != '') {
        var postData = {
            pclid: $('#frmIngresoEfectivo input[id=PclidEfectivo]').val(),
            ctcid: $('#frmIngresoEfectivo input[id=CtcidEfectivo]').val()
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/ListarGestorConciliacion/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (gestor) {
                $.each(gestor, function (i, gestor) {
                    $('#frmIngresoEfectivo input[id=NombreRutGestorEfectivo]').val(gestor.Text)
                    $('#frmIngresoEfectivo input[id=GesidEfectivo]').val(gestor.Value)

                });
            },
            error: function (ex) {
                alert('Error al recuperar los gestores.' + ex);
            }
        });
    }

}

function fnAbrirFormularioEfectivoCustodia() {
    $('#ppAddEfectivoCustodia').dialog('open');
    $('#NombreRutClienteCustodia').val('');
    $('#PclidEfectivo').val('');
    $('#NombreRutDeudorEfectivo').val('');
    $('#CtcidEfectivo').val('');
    $('#NombreRutGestorEfectivo').val('');
    $('#GesidEfectivo').val('');
    $('#FechaIngreso').val('');
    $('#MontoIngreso').val('');
}

function fnIngresarEfectivoCustodia() {
    if ($('#frmIngresoEfectivo input[id=PclidEfectivo]').val() == "")
        alert("Debe Ingresar el cliente!");
    else

        if ($('#frmIngresoEfectivo input[id=CtcidEfectivo]').val() == "")
            alert("Debe ingresar el deudor!")
        else

            if ($('#frmIngresoEfectivo input[id=GesidEfectivo]').val() == "")
                alert("Debe ingresar el gestor")
            else {
                var postData = {
                    pclid: $('#frmIngresoEfectivo input[id=PclidEfectivo]').val(),
                    ctcid: $('#frmIngresoEfectivo input[id=CtcidEfectivo]').val(),
                    gestorId: $('#frmIngresoEfectivo input[id=GesidEfectivo]').val(),
                    fechaDocumento: $('#frmIngresoEfectivo input[id=FechaIngreso]').val(),
                    monto: $('#frmIngresoEfectivo input[id=MontoIngreso]').val(),
                    bancoId: $('#frmIngresoEfectivo select[id=Banco]').val(),
                    recibe: $('#frmIngresoEfectivo input[id=Recibe]').val()
                };
                $.ajax({
                    type: 'POST',
                    url: "/Tesoreria/InsertarEfectivoCustodia/",
                    dataType: 'json',
                    async: true,
                    data: postData,
                    success: function (data) {

                        if (data > 0) {
                            alert("Efectivo ingresado con exito");
                        }
                    },
                    error: function (ex) {
                        alert('Error al guardar Efectivo.' + ex);
                    },
                    complete: function () {
                        $('#ppAddEfectivoCustodia').dialog('close');
                        jQuery("#gridEfectivoCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                    }
                });
            }
}

function fnDoubleClickEfectivo(rowid) {
    var $grid = jQuery("#gridEfectivoCustodiados")
    var rowData = $grid.getRowData(rowid);
    //console.log(rowData)
    var conciliacionId = rowData['ConciliacionId'];
    var pclid = rowData['Pclid'];
    var ctcid = rowData['Ctcid'];

    fnMostrarDocumentosLiquidacionEfectivo(conciliacionId, pclid, ctcid);
}
function fnMostrarDocumentosLiquidacionEfectivo(conciliacionId, pclid, ctcid) {
    var d = $("#ppFormLiquidacionDocs").dialog();

    $("#frmLiquidacionDocumento").reset();
    idsOfSelectedRowsCcbid = [];
    $('#ppFormLiquidacionDocs').dialog('open');
    fnCargarFormularioLiquidacionEfectivo(conciliacionId, pclid, ctcid);
}
function fnCargarFormularioLiquidacionEfectivo(conciliacionId, pclid, ctcid) {
    var DTO = {
        conciliacionId: conciliacionId,
        pclid: pclid,
        ctcid: ctcid
    };
    $.ajax({
        url: '/Tesoreria/verFormularioLiquidacionEfectivo',
        type: "GET",
        dataType: "html",
        data: DTO,
        cache: false,
        beforeSend: function () { $('#loadingmessageLiqui').css('display', 'block') },
        success: function (data) {
            //Fill div with results
            $("#ppFormLiquidacionDocs").html(data);
            $('#loadingmessageLiqui').css('display', 'none')
        },
        error: function (xhr, status, error) {
            $('#loadingmessageLiqui').css('display', 'none')
            alert(xhr.responseText);
        }
    });
}
function fnVerFormularioImputacionEfectivo() {
    var d = $("#ppFormImputacionDocs").dialog();

    $("#frmImputacionDocumento").reset();

    //$('#ppConciliacionCustodia').dialog('option', 'title', 'Documento ' + '' + ' Numero: ' + numeroDocumento + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    var ids;
    ids = $("#gridDocumentoDeudoresLiquidacion").jqGrid('getGridParam', 'selarrrow');
    if (ids.length > 0) {

        $('#ppFormImputacionDocs').dialog('open');

        var postData = $("#frmLiquidacionDocumento").serializeArray();
        $.ajax({
            url: '/Tesoreria/verFormularioImputacionEfectivo',
            type: "POST",
            dataType: 'html',
            async: true,
            data: postData,
            cache: false,
            beforeSend: function () { $('#loadingmessage').css('display', 'block') },
            success: function (data) {
                //Fill div with results
                $("#ppFormImputacionDocs").html(data);

                $('#loadingmessage').css('display', 'none');
            },
            error: function (xhr, status, error) {
                $('#loadingmessage').css('display', 'none');
                alert(xhr.responseText);
            }
        });
    }
    else
        alert("Debe seleccionar uno o mas documentos para aprobar");

}

function fnGuardarImputacionEfectivo() {
    var grid = $("#gridDocumentoDeudoresImputacion")
    var idsFinalizar = [];
    //var index, selRowIds = grid.jqGrid("getGridParam", "selarrrow"), n, row;
    //for (index = 0, n = selRowIds.length; index < n; index++) {
    //    row = grid.jqGrid("getRowData", selRowIds[index]);
    //    if (row.Saldo == 1 && $('#ICriterio').val() == '2')
    //        idsFinalizar.push(row);

    //}

    var datafromgrid = jQuery("#gridDocumentoDeudoresImputacion").getRowData();
    var rowData = [];
    for (var i = 0; i < datafromgrid.length; i++) {
        var imputado = datafromgrid[i].IndicaImputado
        if (imputado == "1") {
            var row = datafromgrid[i];
            rowData.push(row);
        }
        if (datafromgrid[i].Saldo == 0 && $('#ICriterio').val() == '2') {
            var row = datafromgrid[i];
            idsFinalizar.push(row);
        }
    }

    if (rowData.length > 0) {
        var postData = {
            documentos: JSON.stringify(rowData),
            conciliacionId: $('#IConciliacionId').val(),
            pclid: $('#IpclidLiqui').val(),
            ctcid: $('#IctcidLiqui').val(),
            docfinalizar: JSON.stringify(idsFinalizar)
        };
        $.ajax({
            type: 'POST',
            url: "/Tesoreria/InsertarDocumentoImputadoEfectivo/",
            dataType: 'json',
            async: true,
            data: postData,
            beforeSend: function () { $('#btnGuardarImputacion').css('display', 'none') },
            success: function (data) {
                if (data > 0)
                    alert("Documentos imputados con exito");
            },
            error: function (ex) {
                $('#btnGuardarImputacion').css('display', 'block');
                alert('Error al guardar documentos.' + ex);
            },
            complete: function () {
                $('#btnGuardarImputacion').css('display', 'block');
                $('#ppFormImputacionDocs').dialog('close');
                fnCargarFormularioLiquidacionEfectivo($('#IConciliacionId').val(), $('#IpclidLiqui').val(), $('#IctcidLiqui').val());
                jQuery("#gridEfectivoCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
                idsOfSelectedRowsCcbid = [];
                idsOfSelectedRowsFinalizar = [];
                $("#DocFinalizar").val("");
            }
        });
    }
    else {
        alert("Deben haber al menos un documento imputado")
    }
}

function fnBotonesGridEfectivoConciliar(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila"><div class="col"><button type="button" class="btn btn-info btn-sm" style="height:20px;width:100px" onclick="fnVerFormularioConciliacionEfectivo(\'' + rowobject[0] + '\')" >Conciliar</button></div></div></div>';
}

function fnVerFormularioConciliacionEfectivo(rowid) {
   
    var $grid = jQuery("#gridEfectivoCustodiados")
    var rowData = $grid.getRowData(rowid);
    
    var custodiaId = rowData['CustodiaId'];
    var conciliacionId = rowData['ConciliacionId'];
    var numeroDocumento = rowData['NumDocumento'];
    var montoDocumento = rowData['Monto'];
    var fechaDocumento = rowData['FecDoc'];
    var fechaProrroga = rowData['FecProrroga'];
    var estadoDocumento = rowData['EstadoId'];
    var pclid = rowData['Pclid'];
    var rutCliente = rowData['RutCliente'];
    var cliente = rowData['Cliente'];
    var ctcid = rowData['Ctcid'];
    var rutDeudor = rowData['RutDeudor'];
    var deudor = rowData['Deudor'];
    var gesid = rowData['GestorId'];
    var gestor = rowData['Gestor'];
    var banco = "2";//rowData['Gestor'];
    var giroA = rowData['GiradoA'];
    var numeroComprobante = rowData['NumComprobante'].match(/\d+/g);
   
    fnEditarEfectivoCustodia(custodiaId, numeroDocumento, montoDocumento, fechaDocumento, fechaProrroga, estadoDocumento,
                                pclid, rutCliente, cliente, ctcid, rutDeudor, deudor, gesid, gestor, banco, giroA,numeroComprobante,conciliacionId);
}
function fnEditarEfectivoCustodia(custodiaId, numeroDocumento, montoDocumento, fechaDocumento, fechaProrroga, estadoDocumento,
                                pclid, rutCliente, cliente, ctcid, rutDeudor, deudor, gesid, gestor, banco, giroA, numeroComprobante, conciliacionId) {
    var d = $("#ppConciliacionEfectivo").dialog();

    $("#frmConciliacionCustodia").reset();
    $("#btnConciliacionCustodia").attr('disabled', 'disabled');
    $("#btnGuardarConciliaCustodia").removeAttr("disabled");
    $('#MontoDocumento').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia select[id=EstadoDocumento]').attr('disabled', 'disabled');
    $('#ppConciliacionEfectivo').dialog('option', 'title', ' Numero Comprobante: ' + numeroComprobante + ' Cliente: ' + cliente + ' Deudor: ' + deudor);
    $('#ppConciliacionEfectivo').dialog('open');
    $("#CustodiaId").val(custodiaId);
    $("#ConciliacionId").val(conciliacionId);
    $("#pclidHidden").val(pclid);
    $("#ctcidHidden").val(ctcid);
    $("#gesidHidden").val(gesid);
    $('#frmConciliacionCustodia input[id=NombreRutDeudorCC]').val(rutDeudor + " - " + deudor)
    $('#frmConciliacionCustodia input[id=NombreRutDeudorCC]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=NombreRutClienteCC]').val(rutCliente + " - " + cliente)
    $('#frmConciliacionCustodia input[id=NombreRutClienteCC]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=NombreRutGestorCC]').val(gestor)
    $('#frmConciliacionCustodia input[id=NombreRutGestorCC]').attr('disabled', 'disabled');

    $('#frmConciliacionCustodia select[id=Banco]').val(banco);
    $('#frmConciliacionCustodia select[id=Banco]').attr('disabled', 'disabled');
    $('#frmConciliacionCustodia input[id=GiroA]').val(giroA);
    $('#frmConciliacionCustodia input[id=GiroA]').attr('disabled', 'disabled');

    $('#frmConciliacionCustodia input[id=NumeroDocumento]').val(numeroDocumento);
    $('#frmConciliacionCustodia input[id=NumeroDocumento]').attr('disabled', 'disabled');

    $('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val(numeroComprobante);
    $('#frmConciliacionCustodia input[id=NumComprobanteDoc]').attr('disabled', 'disabled');
    $('#MontoDocumento').val(montoDocumento);
    $('#MontoDocumento').val(formatfloat($('#MontoDocumento').val(), 0));


    $('#FechaDocumento').val(fechaDocumento);

    $('#FechaProrroga').val(fechaProrroga);


    $('#frmConciliacionCustodia select[id=EstadoDocumento]').val(estadoDocumento);

    var newUrl = "/Tesoreria/ListarCartolaMovimientosLiberadosGrilla/?"
    newUrl += "numCuenta=" + "&fechaDocumento=" + fechaDocumento + "&montoDocumento=" + $('#MontoDocumento').val();
    jQuery("#gridMovimientosPendientes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

}
function fnBuscarCuenta(cuenta) {
    var newUrl = "/Tesoreria/ListarCartolaMovimientosLiberadosGrilla/?"
    newUrl += "numCuenta=" + cuenta + "&fechaDocumento=" + $('#FechaDocumento').val() + "&montoDocumento=" + $('#MontoDocumento').val();
    jQuery("#gridMovimientosPendientes").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

}
function fnGuardarConciliaEfectivoCustodia() {
    var $grid = jQuery("#gridMovimientosPendientes")

    var ids = $("#gridMovimientosPendientes").jqGrid('getGridParam', 'selarrrow');
    //console.log(ids.length);

    if (ids.length > 0) {
        var rowData = $grid.getRowData(ids[0]);
        var montoMovimiento = parseInt(rowData['Monto'], 10);
        var movimientoId = rowData['MovimientoId'];
        if (montoMovimiento < 0)
            alert("El monto no puede ser negativo")
        else {
            //se Guarda y procede a conciliar
            if ($('#frmConciliacionCustodia input[id=NumComprobanteDoc]').val() != '') {
                $.ajax({
                    type: 'POST',
                    url: "/Tesoreria/ActualizarEstadoDocumentoCustodia/",
                    dataType: 'json',
                    async: true,
                    data: {
                        custodiaId: $('#CustodiaId').val(),
                        tipoEstadoId: 1 //LIBERADO
                    },
                    success: function (data) {

                        if (data > 0) {
                            $('#frmConciliacionCustodia select[id=EstadoDocumento]').val("1");
                            $.ajax({
                                type: 'POST',
                                url: "/Tesoreria/ActualizarMovimientoConciliacionCustodia/",
                                dataType: 'json',
                                async: true,
                                data: {
                                    movimientoId: movimientoId,
                                    custodiaId: $('#CustodiaId').val(),
                                    conciliacionId: $('#ConciliacionId').val()
                                },
                                success: function (data) {

                                    if (data > 0) {
                                        alert("Registro conciliado con éxito");
                                    } else
                                        alert("Error al crear la conciliación")

                                },
                                complete: function () {
                                    $('#ppConciliacionEfectivo').dialog('close');
                                    //Actualizo grid de documentos custodia
                                    jQuery("#gridEfectivoCustodiados").jqGrid().trigger('reloadGrid', [{ page: 1 }]); // documentosCustodia
                                    
                                }
                            });

                        } else {
                            alert("Error al crear la conciliación")

                        }
                    },
                    complete: function () {
                        fnUpdateFormularioConciliacionCustodia();
                    }
                });

            }
            else {
                alert("Ingrese el numero de comprobante")
            }

        }
    }
}

function fnVerComprobanteLiquidacionEfectivo(cellvalue, options, rowdata) {
   
    var html = '\
        <div class="tabla"><div class="fila">\
            <div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnMostrarComprobanteLiquidacion(\'' + rowdata[1] + '\')">reporte</button></div><div class="col"><span>' + cellvalue + '</span></div>\
        </div></div>';

    return rowdata[24] == '2' ? html: '<span>' + cellvalue + '</span>';
   
}
function fnMostrarComprobanteLiquidacion(conciliacionId) {
    var DTO = {
        conciliacionId: conciliacionId
    };
    $.ajax({
        url: '/Tesoreria/ComprobanteImputacion',
        type: "POST",
        data: DTO,
        cache: false,
        //beforeSend: function () { $('#loadingmessageLiqui').css('display', 'block') },
        success: function (result) {
            //Fill div with results
            if (result.success) {
                var blob = b64StrtoBlob(result.pdf, 'application/pdf');
                var blobUrl = URL.createObjectURL(blob);
                var content = String.format("<object data='{0}' type='application/pdf' width='100%' height='100%'></object>", blobUrl);
                $("#ppDocto").empty();
                $("#ppDocto").html(content);
                $('#ppDocto').dialog('open');
            }
           
            
            //$('#loadingmessageLiqui').css('display', 'none')
        },
        error: function (xhr, status, error) {
            //$('#loadingmessageLiqui').css('display', 'none')
            alert(xhr.responseText);
        }
    });
}

function fnMostrarComprobanteImputacion(conciliacionId) {
    var DTO = {
        conciliacionId: conciliacionId
    };
    $.ajax({
        url: '/Tesoreria/ComprobanteDetalleImputacion',
        type: "POST",
        data: DTO,
        cache: false,
        success: function (result) {
            if (result.success) {
                var blob = b64StrtoBlob(result.pdf, 'application/pdf');
                var blobUrl = URL.createObjectURL(blob);
                var content = String.format("<object data='{0}' type='application/pdf' width='100%' height='100%'></object>", blobUrl);
                $("#ppDocto").empty();
                $("#ppDocto").html(content);
                $('#ppDocto').dialog('open');
            }
        },
        error: function (xhr, status, error) {
            alert(xhr.responseText);
        }
    });
}
String.format = function () {
    var s = arguments[0];
    for (var i = 0; i < arguments.length - 1; i++) {
        var reg = new RegExp("\\{" + i + "\\}", "gm");
        s = s.replace(reg, arguments[i + 1]);
    }
    return s;
}//___________________________________

function b64StrtoBlob(b64Data, contentType, sliceSize) {
    contentType = contentType || '';
    sliceSize = sliceSize || 512;
    var byteCharacters = atob(b64Data);
    var byteArrays = [];
    for (var offset = 0; offset < byteCharacters.length; offset += sliceSize) {
        var slice = byteCharacters.slice(offset, offset + sliceSize);
        var byteNumbers = new Array(slice.length);
        for (var i = 0; i < slice.length; i++) {
            byteNumbers[i] = slice.charCodeAt(i);
        }
        var byteArray = new Uint8Array(byteNumbers);
        byteArrays.push(byteArray);
    }
    var blob = new Blob(byteArrays, { type: contentType });
    return blob;
}//___________________________________
function fnCargarGridConciliacionAprGrilla() {
    
    idsOfSelectedRowsRemesas = [];
    console.log($("#FechaBusquedaConcilia").val())
        var newUrl = "/Tesoreria/ListarMovimientosConciliadoPendienteGrilla/?"
        newUrl += "pclid=" + $("#Pclid").val() + "&ctcid=" + $("#CtcidSearch").val() + "&fechaConciliacion=" + $("#FechaBusquedaConcilia").val() + "&numComprobante=" + $("#NumeroComprobante").val();
        jQuery("#gridMovimientosApr").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
       
    
}
function fnBotonReversarImputacion(cellvalue, options, rowdata) {
    return '<div class="tabla"><div class="fila"><div class="col"><button title="ver comprobante" style="height:20px;width:20px" onclick="fnReversarImputacion(\'' + cellvalue + '\',\'' + rowdata[2] + '\')"><i class="fa fa-undo" style="font-size:14px;"></i></button></div></div></div>';
}
function fnReversarImputacion(existeRemesa, conciliacionId) {
    if (existeRemesa > 0)
        alert("No se puede reversar, se encuentra remesada")
    else {
        ConfirmReversa("Está seguro de reversar la imputación?", conciliacionId)
    }
}
function ConfirmReversa(message, conciliacionId) {
    $('<div></div>').appendTo('body')
    .html('<div><h6>' + message + '?</h6></div>')
    .dialog({
        modal: true, title: 'Reversar Imputación', zIndex: 10000, autoOpen: true,
        width: 'auto', resizable: false,
        buttons: {
            "Reversar": function () {
                $.ajax({
                    url: '/Tesoreria/ReversarImputacion',
                    type: "POST",
                    data: { conciliacionId: conciliacionId },
                    cache: false,
                    success: function (result) {
                        if (result > 0) {
                            alert("Imputacion reversada");
                           
                        }
                    },
                    error: function (xhr, status, error) {
                        alert(xhr.responseText);
                    },
                    complete: function () {
                        fnAprobarConciliacion();
                    }
                });

                $(this).dialog("close");
            },
            No: function () {
                //$('body').append('<h1>Confirm Dialog Result: <i>No</i></h1>');
                $(this).dialog("close");
            }
        },
        close: function (event, ui) {
            $(this).remove();
        }
    });
};
function fnBuscarCriteriosRemesaCliente() {
    idsOfSelectedRowsEstadoCliente = [];
    var SelectedCliente = $("#frmCriteriosRemesa input[id=Pclid]").val()
    if (SelectedCliente != '' && SelectedCliente != null) {
        
        if ($('#frmCriteriosRemesa').valid()) {
            var newUrl = "/Mantenedores/ListarCriteriosRemesaCliente/?"
            newUrl += "pclid=" + SelectedCliente

            jQuery("#gridCriterioRemesa").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
            ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
        }
    } else {
        alert("Ingrese un cliente");
    }


}

function fnAgregarCriterioRemesa() {
    var SelectedCliente = $("#frmCriteriosRemesa input[id=Pclid]").val()
    if (SelectedCliente != '' && SelectedCliente != null) {
        $('#ppAddCriterioRemesa').dialog('open');
        $('#frmCriterioRemesa').reset();
        $("#Id").val('')
        $("#Pclid").val($("#frmCriteriosRemesa input[id=Pclid]").val())
        $.ajax({
            url: '/Mantenedores/EmpresaIsAnticipo',
            type: "POST",
            data: { pclid: $("#Pclid").val() },
            cache: false,
            success: function (result) {
                if (result == 'S') 
                    $("#divCondicionAnticipo").show()
                else
                    $("#divCondicionAnticipo").hide()
            }
        });
        $("#Desde").removeAttr("disabled");
        $("#Hasta").removeAttr("disabled");
        $("#TipoRegion").removeAttr("disabled");
        $("#CodigoCarga").removeAttr("disabled");
        $("#TipoConciliacion").removeAttr("disabled");
        $("#CondicionAnticipo").removeAttr("disabled");
    } else {
        alert("Ingrese un cliente");
    }
}
function fnDoubleClickGridRemesa(rowid) {
    var $grid = jQuery("#gridCriterioRemesa")
    var rowData = $grid.getRowData(rowid);

    $("#Id").val(rowData['Id'])
    $("#Pclid").val($("#frmCriteriosRemesa input[id=Pclid]").val())
    var venDesde = rowData['DesdeDiasVencido'] == "0" ? "" : rowData['DesdeDiasVencido'];
    var venHasta = rowData['HastaDiasVencido'] == "0" ? "" : rowData['HastaDiasVencido'];
    $("#Desde").val(venDesde)
    $("#Hasta").val(venHasta)
    var tipoRegion = rowData['RegionMetropolitana'] == "S" ? "6" : "";
    $('#frmCriterioRemesa select[id=TipoRegion]').val(tipoRegion);
    $('#frmCriterioRemesa select[id=CodigoCarga]').val(rowData['CodigoCarga']);
    $('#frmCriterioRemesa select[id=TipoCambioCapital]').val(rowData['SimboloId']);

    $("#Capital").val(rowData['Capital'])
    $("#Interes").val(rowData['Interes'])
    $("#Honorario").val(rowData['Honorario'])
    $('#frmCriterioRemesa select[id=TipoConciliacion]').val(rowData['TipoConciliacionId']);
    $('#frmCriterioRemesa select[id=CondicionAnticipo]').val(rowData['ConcicionId']);
    if (rowData['IsAnticipo'] == 'S')
        $("#divCondicionAnticipo").show()
    else
        $("#divCondicionAnticipo").hide()
    $("#Desde").attr("disabled", "disabled");
    $("#Hasta").attr("disabled", "disabled");
    $("#TipoRegion").attr("disabled", "disabled");
    $("#CodigoCarga").attr("disabled", "disabled");
    $("#TipoConciliacion").attr("disabled", "disabled");
    $("#CondicionAnticipo").attr("disabled", "disabled");
    //$("#btnSubmitCartola").removeAttr("disabled");
    $('#ppAddCriterioRemesa').dialog('open');

}

function fnInsertUpdateCriterioRemesa() {
    var postData = {
        Id: $("#Id").val(),
        Pclid: $("#frmCriteriosRemesa input[id=Pclid]").val(),
        Desde: $("#Desde").val(),
        Hasta: $("#Hasta").val(),
        TipoRegion: $('#frmCriterioRemesa select[id=TipoRegion]').val(),
        CodigoCarga: $('#frmCriterioRemesa select[id=CodigoCarga]').val(),
        TipoCambioCapital: $("#frmCriterioRemesa select[id=TipoCambioCapital]").find("option:selected").text(),
        TipoCambioCapitalId: $("#frmCriterioRemesa select[id=TipoCambioCapital]").find("option:selected").val(),
        Capital: $("#Capital").val(),
        Interes: $("#Interes").val(),
        Honorario: $("#Honorario").val(),
        TipoConciliacion: $('#frmCriterioRemesa select[id=TipoConciliacion]').val(),
        CondicionAnticipo: $('#frmCriterioRemesa select[id=CondicionAnticipo]').val(),
        IsFacturacion: false
    };
    if ($('#frmCriterioRemesa').valid()) {
        $.ajax({
            type: 'POST',
            url: "/Mantenedores/InsertUpdateCriterioRemesa/",
            dataType: 'json',
            async: true,
            data: postData,
            success: function (data) {
                if (data != -1) {
                    if (data == 0)
                        alert('La Definicion de Criterio ya existe');
                    else {
                        if ($("#Id").val().length > 0)
                            alert('Registro actualizado con éxito');
                        else
                            alert('Registro ingresado con éxito');

                        $('#ppAddCriterioRemesa').dialog('close');
                    }
                } else {
                    alert('Error al ingresar registro.');
                }
            },
            error: function (ex) {
                alert('Error al ingresar registro' + ex);
            },
            complete: function () {
                jQuery("#gridCriterioRemesa").jqGrid().trigger('reloadGrid', [{ page: 1 }]);
            }
        });
    }
}
function fnRulesCriterioRemesa() {
    $("#frmCriterioRemesa input[id=Hasta]").rules("add", {
        required: function () {
            return $("#frmCriterioRemesa input[id=Desde]").val() > 0;
        },
        greaterThan: ["#frmCriterioRemesa input[id=Desde]", "Desde"],
        messages: {
            required: "este campo es requerido"
        }
    });
    $("#frmCriterioRemesa input[id=Desde]").rules("add", {
        required: function () {
            return $("#frmCriterioRemesa input[id=Hasta]").val() > 0;
        },
        messages: {
            required: "este campo es requerido"
        }
    });
}
function fnExcelPagos() {
    var url = "/Tesoreria/ExportToExcelPagos";
    window.location.href = url;

}

function fnCargarGridPagos() {
       
    var newUrl = "/Tesoreria/ConsultaDePagos/?"
    newUrl += "pclid=" + $("#Pclid").val() + "&ctcid=" + $("#CtcidSearch").val() + "&fechaCancelacion=" + $("#FechaBusquedaConcilia").val() + "&numComprobante=" + $("#NumeroComprobante").val();
    jQuery("#gridConsultaPago").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);


}
function fnAgregarCriterioFacturacion() {
    $('#ppAddCriterioFacturacion').dialog('open');
    $('#frmCriterioFacturacion').reset();
    $("#Id").val('')
    
}