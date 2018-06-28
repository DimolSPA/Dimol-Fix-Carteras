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


function CargarDeudorRutEvento(e) {
    if (e.keyCode == 13) {
        CargarDeudorRut();
    }
}

function fnPostData() {
    return "v:" + $("#CtcidDialog").val();
}

function validaRut(rutCompleto) {
	if (!/^[0-9]+[-|‐]{1}[0-9kK]{1}$/.test( rutCompleto ))
		return false;	
	var tmp 	= rutCompleto.split('-');
	var digv	= tmp[1];
	var rut 	= tmp[0];
	if ( digv == 'K' ) digv = 'k' ;
	return (dv(rut) == digv );
	}
	
function dv(T){
	var M=0,S=1;
	for(;T;T=Math.floor(T/10))
		S=(S+T%10*(9-M++%6))%11;
	return S?S-1:'k';
	}		

function fnGuardarPassword() {
    if ($("#idPssAct").val() != '' && $("#idNewPss").val() != '' && $("#idCnfPss").val() != '') {
        if ($("#idNewPss").val() == $("#idCnfPss").val()) {

            $.ajax({
                type: 'POST',
                url: "/Open/ActPass", // we are calling json method
                dataType: 'json',
                async: false,
                data: { usrid: $("#usrPJ").val(), passAct: $("#idPssAct").val(), newPass: $("#idNewPss").val() },
                success: function (total) {
                    if (total > 0) {
                        alert("Password cambiado con éxito");
                        $('#dlgChgPss').dialog('close');
                    }
                    else {
                        alert("El password actual no coincide");
                    }
                }
            });

        }
        else {
            alert("El nuevo password y la confirmación no coinciden");
        }
    }
    else {
        alert("Ingrese los datos solicitados");
    }
}

function fnTraeUsuariosPJ(userid, button) {

    if (!userid) {
        userid = 0;
        button = 'R';
    }

    $.ajax({
        type: 'POST',
        url: "/Open/GetUserPJ", // we are calling json method
        dataType: 'json',
        async: false,
        data: { userid: userid, button: button },
        success: function (total) {
            
            if (total != '') {

                if (total[0].Left == 1) {
                    $('#idButtonL').prop('disabled', true).css('opacity', 0.5);
                }
                else {
                    $('#idButtonL').prop('disabled', false).css('opacity', 1);
                }

                if (total[0].Right == 1) {
                    $('#idButtonR').prop('disabled', true).css('opacity', 0.5);
                }
                else {
                    $('#idButtonR').prop('disabled', false).css('opacity', 1);
                }

                $('#idUserId').val(total[0].Userid);
                $('#idNombrePers').val(total[0].Nombre);
                $('#idUsername').val(total[0].Login);
                $('#idPass').val(total[0].Password);
                $('#Pclid').val(total[0].Pclid);
                $('#RutCliente').val(total[0].RutCliente);
                $('#Activo').val(total[0].Activa);
                $('#Perfil').val(total[0].Admin);
            }
            else {
                $('#idButtonL').prop('disabled', true).css('opacity', 0.5);
                $('#idButtonR').prop('disabled', true).css('opacity', 0.5);
            }
        }
    });
}

function fnGuardarUserPJ() {
    
    if ($('#idNombrePers').val() != '' && $('#idUsername').val() != '' && $('#idPass').val() != '' && $('#Pclid').val() != '' && $('#Activo').val() != '' && $('#Perfil').val() != '') {

        $.ajax({
            type: 'POST',
            url: "/Open/GuardarUserPJ", // we are calling json method
            dataType: 'json',
            async: false,
            data: { iduser: ($('#idUserId').val() == '') ? 0 : $('#idUserId').val(), nombre: $('#idNombrePers').val(), username: $('#idUsername').val(), pass: $('#idPass').val(), activa: $('#Activo').val(), pclid: $('#Pclid').val(), adm: $('#Perfil').val() },
            success: function (total) {
                if (total > 0) {
                    alert("Usuario ingresado con éxito");
                }
                else {
                    alert("Error al grabar usuario");
                }
                fnTraeUsuariosPJ(0, 'R');
            }
        });
        
    }
    else {
        alert("Debe completar todos los datos");
    }

}

function fnNewUserPJ() {
    
    $('#idUserId').val('');
    $('#idNombrePers').val('');
    $('#idUsername').val('');
    $('#idPass').val('');
    $('#Pclid').val('');
    $('#RutCliente').val('');
    $('#Activo').val('');
    $('#Perfil > option[value="N"]').prop('selected', 'selected');
    $('#idButtonR').prop('disabled', false).css('opacity', 1);
    $('#idButtonL').prop('disabled', false).css('opacity', 1);
}

function fnNuevaEmpresaPJ() {
    $('#idRutaLogo').val('');
    $('#PclidLogo').val('');
    $('#RutClienteLogo').val('');
    $('#idBotonR').prop('disabled', false).css('opacity', 1);
    $('#idBotonL').prop('disabled', false).css('opacity', 1);
    $('#idLogoEmpresa').val('');
    $('#idLogoEmpresaPJ').prop('src', '');
    $('#idNombreLogo').prop('style', 'visibility:visible;width:60px;');
    $('#idLogoEmpresa').prop('style', 'visibility:visible;width:300px;');
    $('#idGuardarLogo').prop('style', 'visibility:visible;');
}

function fnTraeRutaLogoEmpresaPJ(userid, button) {

    if (!userid) {
        userid = 0;
        button = 'R';
    }

    $.ajax({
        type: 'POST',
        url: "/Open/TraeRutaLogoEmpresaPJ", // we are calling json method
        dataType: 'json',
        async: false,
        data: { userid: userid, button: button },
        success: function (total) {

            if (total != '') {

                if (total[0].Left == 1) {
                    $('#idBotonL').prop('disabled', true).css('opacity', 0.5);
                }
                else {
                    $('#idBotonL').prop('disabled', false).css('opacity', 1);
                }

                if (total[0].Right == 1) {
                    $('#idBotonR').prop('disabled', true).css('opacity', 0.5);
                }
                else {
                    $('#idBotonR').prop('disabled', false).css('opacity', 1);
                }
            
                $('#idRutaLogo').val(total[0].Userid);
                $('#PclidLogo').val(total[0].Pclid);
                $('#RutClienteLogo').val(total[0].RutCliente);
                $('#idLogoEmpresaPJ').prop('src', total[0].Nombre);
                $('#idNombreLogo').prop('style', 'visibility:hidden');
                $('#idLogoEmpresa').prop('style', 'visibility:hidden');
                $('#idGuardarLogo').prop('style', 'visibility:hidden;');
            }
            else {
                $('#idBotonL').prop('disabled', true).css('opacity', 0.5);
                $('#idBotonR').prop('disabled', true).css('opacity', 0.5);
            }
        }
    });
}

function fnGridBuscarCausaRut() {
      

    if ($("#Ctcid").val() != '') {
		
       if(validaRut($("#RutDeudor").val() + '-' + $("#DvDeudor").val())) {
		   
			var ids = $("#RutDeudor").val() + $("#DvDeudor").val();        
			var newUrl = "/Open/GetCausaRut/?"
			newUrl += "rut=" + ids;

			jQuery("#gridCausas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
			
			$.ajax({
				type: 'POST',
				url: "/Open/BuscarNombreDeudorPJ", // we are calling json method
				dataType: 'json',
				async: false,
				data: { term: ids },
				success: function (total) {                
					$("#NombreDeudor").html(total);
				}
			});

			$.ajax({
				type: 'POST',
				url: "/Open/GetCausaRut", // we are calling json method
				dataType: 'json',
				async: false,
				data: { rut: ids },
				success: function (total) {
					if (total.records > 0) {
						$("#dvTotalCausas").html('<div style="text-align:center;color:red;font-weight:bold;">Total de Causas Encontradas: ' + total.records + '</div>');
					}
					else {
					    $("#NombreDeudor").html('<font style="color:blue;font-weight:bold;">No se encontraron demandas para este Rut</font>');
						$("#dvTotalCausas").html('');
					}
				}
			});

			fnBuscarRolDeudorCtcid();
			fnRefreshGridCausasInternas();
		}
		else{
			$("#NombreDeudor").html('<font style="color:red;margin-left:10px;font-weight:bold;" >RUT INVÁLIDO</b>');
		}		
    }
}        

function fnBuscarRolDeudorCtcid() {
    var newUrl = "/Open/GetRol/?"
    newUrl += "&Ctcid=" + $("#Ctcid").val()

    jQuery("#gridRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function fnRefreshGridCausasInternas() {
    $.ajax({
        type: 'POST',
        url: "/Open/GetTotalDocRol", // we are calling json method
        dataType: 'json',
        async: false,
        data: { Rolid: 0 },
        success: function (total) {
            totalMontoRol = total.monto;
            totalSaldoRol = total.saldo;
            $("#hddTotalMontoRol").val(total.monto);
            $("#hddTotalSaldoRol").val(total.saldo);
        }

    });

    var newUrl = "/Open/GetDocRol/?"
    newUrl += "Rolid=" + 0;
    jQuery("#gridDocRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrl = "/Open/GetEstados/?"
    newUrl += "Rolid=" + 0;
    jQuery("#gridEstadosRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

}
function RolSeleccionado(id) {
    var ids = id.split('|');

    $.ajax({
        type: 'POST',
        url: "/Open/GetTotalDocRol", // we are calling json method
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

    var newUrl = "/Open/GetDocRol/?"
    newUrl += "Rolid=" + ids[1];
    jQuery("#gridDocRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    var newUrl = "/Open/GetEstados/?"
    newUrl += "Rolid=" + ids[1];
    jQuery("#gridEstadosRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);

    //newUrl = "/Cartera/GetArchivosRol/?"
    //newUrl += "Ctcid=" + ids[0] + "&Rolid=" + ids[1];
    //jQuery("#gridArchivosRol").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }]);
}

function EstadoSeleccionado() {
   
    alert($("#SituacionCartera").val()); alert($("#EstadoCpbt").val());
    //$("#EstadoCpbt").val($("#SituacionCartera").val());
}


function PaisSel() {
    PaisSeleccionado($("#Pais"), "Region");
}


function returnHyperLink(cellValue, options, rowdata, action) {
    return "<a href='" + cellValue + "' >"+rowdata[3]+"</a>";

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
/*   Comprobante       */

function OnSelectClienteComprobante() {
    var cliente = $("#RutNombreCliente").val().split('-');
    $("#RutCliente").val(cliente[0].trim());
    $("#NombreCliente").val(cliente[1].trim());
    //ActualizaComboCodigoCarga("Pclid", "CodigoCarga");
    //ActualizaComboContrato("Pclid", "TipoCartera", "Contrato");
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
    var  i, count;
    for (i = 0, count = idsOfSelectedRows.length; i < count; i++) {
        $("#" + grilla.id).jqGrid('setSelection', idsOfSelectedRows[i], false);
        $("#jqg_" + grilla.id + "_" + idsOfSelectedRows[i] ).prop("checked", true);
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


function MostrarDocumento(rowid) {
    //$("#dialogContent").html(rowid);
    //$(".ui-dialog").attr("id", "dialog");
    $(".ui-dialog-content").html('<object data="'+rowid+'" type="application/pdf" width="100%" height="100%"><p>Aparentemente no tienes el plugin para visualizar PDF en el navegador. No hay problema, puedes descargar el archivo desde este link <a href="'+rowid+'">.</a></p></object>');
    $(".ui-dialog-content").dialog("open");
}

jQuery.fn.extend({
    propAttr: $.fn.prop || $.fn.attr
});

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

function imageFormat(cellValue, rowId, rowData, options) {
    if (cellValue == '')
        return '';
    else
        //return '<img src= "' + cellValue + '"/>';
        return '<img  src= "' + cellValue + '" style="width:80;height:80px;" />';
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


function fnLimpiarDeudor() {
    //alert("limpiar");
    $("#frmDeudor").reset();
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



var idsEstadoHistorial = [];



function MensajeCerrarBorradores() {
    if (window.confirm("Está seguro que desea salir sin guardar los últimos cambios?")) {
        return true;
    } else {
        return false;
    }
}


//
function fnLinkPJ(cellvalue, options, rowdata) {
    //alert(rowdata);
    return "<a href=\'#\' onclick=\"window.open(\'" + rowdata[5] + "\')\" >" + cellvalue + "</a>"; 
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








