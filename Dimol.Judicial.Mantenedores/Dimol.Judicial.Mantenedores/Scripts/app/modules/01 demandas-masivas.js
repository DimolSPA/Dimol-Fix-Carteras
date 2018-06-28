/*******************************************
01             DEMANDAS MASIVAS
*******************************************/
function fnCargarOrgChartPanelDemandaMasiva() {
    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarPanelDemandaControlGestionMasivas", // we are calling json method
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
                    '<div onclick="fnPanelDemandaMasivaReporte(\'' + chartsdata[i].Id + '\',\'' + chartsdata[i].Item + '\')"style=\"cursor:pointer\"><div style="color:blue; font-style:italic">' + chartsdata[i].Total + '</div></div>';
                //'<div onclick="fnPanelDemandaReporte(\'' + chartsdata[i].Id + '\','+ chartsdata[i].Item + '\)"style=\"cursor:pointer\"><div style="color:blue; font-style:italic">' + chartsdata[i].Total + '</div></div>';
                data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Parent, chartsdata[i].Item]);

            }
            var chart = new google.visualization.OrgChart(document.getElementById('chartPanelDemandaMasiva'));
            chart.draw(data, { allowHtml: true });

        },
        error: function (ex) {
            alert('Error al cargar el panel.');
        }
    });
}

function handleCKEditorPostDemandaMasiva() {
    var htmlData = CKEDITOR.instances.editor1.getData();

    $.ajax({
        type: 'POST',
        url: "/Judicial/GuardarBorradorDemandasMasivas",
        dataType: 'json',
        async: false,
        data: {
            PanelDemandaId: $("#IdDM").val(),
            TipoBorradorId: $("#TipoBorrador").val(),
            HtmlBorrador: htmlData
        },
        success: function(chartsdata) {
            CKEDITOR.instances.editor1.setData(chartsdata);
            alert("Borrador guardado exitosamente.");
        },
        error: function(ex) {
            alert('Error al cargar el borrador.');
        }
    });
}

function fnBorradorSeleccionadoPanelDemandasMasivas() {
    if ($("#TipoBorrador").val() != '' && $("#TipoBorrador").val() != null) {
        $.ajax({
            type: 'POST',
            url: "/Judicial/GetBorradorDemandasMasivas",
            dataType: 'json',
            async: false,
            data: {
                IdDM: $("#IdDM").val(),
                TipoBorrador: $("#TipoBorrador").val()
            },
            success: function(chartsdata) {
                fnTraeHistorialBorradorDemandaMasiva();

                CKEDITOR.instances.editor1.setData(chartsdata);
            },
            error: function(ex) {
                alert('Error al cargar el borrador.');
            }
        });
    }
}

function fnTraeHistorialBorradorDemandaMasiva() {
    var newUrl = "/Judicial/GetHistoriaBorradorDemandasMasivas/";

    var postData = {
        IdDM: $("#IdDM").val(),
        TipoBorrador: $("#TipoBorrador").val()
    };

    $.ajax({
        type: 'POST',
        url: newUrl,
        dataType: 'json',
        async: true,
        data: postData,
        success: function(data) {
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

function fnBotonesGridPanelDemandasMasivas(cellvalue, options, rowobject) {
    return '<div class="tabla"><div class="fila" style="margin-left:8px"><div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnEditarPanelMasivas(\'' + rowobject + '\')" >Editar</button></div></div></div>';
}

function fnBotonesGridConfeccionDemanda(cellvalue, options, rowobject) {
    return '<div class="tabla">\
                <div class="fila" style="margin-left:36px">\
                    <div class="col">\
                        <button type="button" class="ui-icon ui-icon-disk" style="height:20px;width:20px" onclick="fnTipoDemandaMasiva(\'' + rowobject + '\')" >Confeccionar Demanda</button>\
                    </div>\
                </div>\
            </div>';
}

function fnBotonesGridDescargarDemanda(cellvalue, options, rowobject) {
    return '<div class="tabla">\
                <div class="fila" style="margin-left:36px">\
                    <div class="col">\
                          <button disabled type="button" class="ui-icon ui-icon-arrowthickstop-1-s" style="height:20px;width:19px;cursor:not-allowed;">Descargar PDF</button>\
                    </div>\
                </div>\
            </div>';

    //return '<div class="tabla">\
    //            <div class="fila" style="margin-left:36px">\
    //                <div class="col">\
    //                      <button type="button" class="ui-icon ui-icon-arrowthickstop-1-s" style="height:20px;width:19px" onclick="fnDescargarPDFDemandaMasivaGrilla(\'' + rowobject + '\')" >Descargar PDF</button>\
    //                </div>\
    //            </div>\
    //        </div>';
}

function fnTipoDemandaMasiva(id) {
    var datos = id.split(',');
    $("#IdDM").val(datos[0]);

    $('#ppBorradoresTipoDemandaMasiva').dialog('open');

    var editor = CKEDITOR.instances['editor1'];
    if (editor) {
        editor.destroy(true);
    }
    CKEDITOR.replace('editor1');

    fnBorradorSeleccionadoPanelDemandasMasivas();

    return false;
}

//Función para boton que descarga PDF, define los parámetros para el boton en el popup
function fnDescargarPDFDemandaMasivaPopUp(id) {
    if ($("#TipoBorrador").val() != "") {
        var datos = id.split(',');
        $("#IdDM").val(datos[0]);

        fnDescargarPDFDemandaMasiva($("#IdDM").val(), $("#TipoBorrador").val());
    }

    return false;
}

//Función para boton que descarga PDF, define los parámetros para el boton en la grilla
function fnDescargarPDFDemandaMasivaGrilla(id) {
    var datos = id.split(',');
    $("#IdDM").val(datos[0]);

    fnDescargarPDFDemandaMasiva($("#IdDM").val(), 7);

    return false;
}

//Función para boton que descarga PDF, realiza la llamada ajax
function fnDescargarPDFDemandaMasiva(idDemanda, idTipoBorrador) {
    $('#loading-gif').show();

    var params = "IdDM=" + idDemanda + "&IdTipoBorrador=" + idTipoBorrador;
    var req = new XMLHttpRequest();
    req.open("POST", "/Judicial/ExportToPDFDemandaMasiva", true);
    req.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    req.responseType = "blob";

    req.onload = function (event) {
        if (req.status === 200) {
            var disposition = req.getResponseHeader('content-disposition');
            var matches = /"([^"]*)"/.exec(disposition);
            var filename = (matches != null && matches[1] ? matches[1] : 'file.pdf');

            // The actual download
            var blob = new Blob([req.response], { type: 'application/pdf' });
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);
            link.download = "DemandaMasiva_" + new Date() + ".pdf";
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }

        $('#loading-gif').hide();
    };

    req.send(params);
}

function fnEditarPanelMasivas(id) {
    var datos = id.split(',');

    var d = $("#ppAvanceDemanda").dialog();
    //$("#frmAvanceDemanda").reset();
    frmAvanceDemandaReset();
    $('#frmAvanceDemanda input[id=PanelProcesado]').val(datos[17]);

    if (datos[17] == 'S') {
        //estoy deshabilitando panel procesado
        $("#EnviaFechaEntrega").prop("disabled", true);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
        $("#btnGuardar").prop('disabled', true);
    } else {
        //estoy habilitando panel no procesado
        $("#EnviaFechaEntrega").prop("disabled", false);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
        $("#btnGuardar").prop('disabled', false);
    }

    //Dehabilitar para un usuario que no corresponda al usuario encargado
    if (datos[19] != 0) {
        if (datos[19] != $('#frmAvanceDemanda input[id=GetUsuario]').val()) {

            $("#EnviaFechaEntrega").prop("disabled", true);
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
            $("#btnGuardar").prop('disabled', true);
        }
    }
    //Habilitar para Control de Gestion
    if (($('#frmAvanceDemanda input[id=GetPerfil]').val() == 13) || ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 10)) {

        $("#EnviaFechaEntrega").prop("disabled", false);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
        $("#btnGuardar").prop('disabled', false);
        //quit mindate temporal
        //$('#frmAvanceDemanda input[id=FechaConfeccion]').datepicker('enable');
        //$('#frmAvanceDemanda input[id=FechaConfeccion]').datepicker("option", "minDate", null);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker("option", "minDate", null);
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker("option", "maxDate", null);
    }
    //Habilitar para Judicial
    if ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 6) {

        $("#EnviaFechaEntrega").prop("disabled", false);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker('enable');
        $("#btnGuardar").prop('disabled', false);

    }
    //Habilitar para Asistente Judicial
    if ($('#frmAvanceDemanda input[id=GetPerfil]').val() == 7) {
        $("#EnviaFechaEntrega").prop("disabled", false);
        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker('enable');
        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
        $("#btnGuardar").prop('disabled', false);

    }
    var datos = id.split(',');
    $('#frmAvanceDemanda input[id=PanelId]').val(datos[0]);
    $('#frmAvanceDemanda input[id=usridHidden]').val(datos[19]);
    $('#frmAvanceDemanda input[id=rutDeudorHidden]').val(datos[5]);
    $('#frmAvanceDemanda input[id=nombreDeudorHidden]').val(datos[6]);
    $('#frmAvanceDemanda input[id=rutClienteHidden]').val(datos[22]);
    $('#frmAvanceDemanda input[id=nombreClienteHidden]').val(datos[4]);
    $('#frmAvanceDemanda input[id=pclidHidden]').val(datos[20]);
    $('#frmAvanceDemanda input[id=ctcidHidden]').val(datos[21]);
    $('#frmAvanceDemanda input[id=CountFechaEntrega]').val(datos[23]);
    $('#FechaTribunal').datepicker("option", "dateFormat", "dd/mm/yy");
    $('#FechaEntrega').datepicker("option", "dateFormat", "dd/mm/yy");

    $('#ppAvanceDemanda').dialog('open');
    //Si no se ha enviado fecha de entrega
    if (datos[23] == 0) {
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
    if (datos[11] == '' || datos[11] == null) {
        $('#FechaConfeccion').datepicker().datepicker('setDate', 'today');
    } else {
        var jsonDate = datos[11].replace(/\D+/g, '');//reemplaza todos los caracteres a excepcion de numeros
        var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
        var fechaConfeccion = ('0' + date.getDate()).slice(-2) + '/'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
            + date.getFullYear()

        $('#frmAvanceDemanda input[id=FechaConfeccion]').val(fechaConfeccion);
    }
    
    if (!datos[12]) {

        $('#frmAvanceDemanda input[id=FechaEntrega]').val('');
        $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val('');
        $('#FechaEntrega').datepicker("option", "minDate", 0);
        $('#FechaEntrega').datepicker("option", "maxDate", 0);
    } else {
        var jsonDate = datos[12].replace(/\D+/g,'');
        //reemplaza todos los caracteres a excepcion de numeros
        var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
        var dateToPicker = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            $('#FechaEntrega').datepicker().datepicker('setDate', dateToPicker);
            $('#FechaEntrega').datepicker("option", "minDate", dateToPicker);
            $('#FechaEntrega').datepicker("option", "maxDate", dateToPicker);
            var fechaEntrega = ('0' + date.getDate()).slice(-2) + '/'
                + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
                + date.getFullYear()
            $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val(fechaEntrega);
    }
    if (datos[13] == '' || datos[13] == null) {
        $('#frmAvanceDemanda input[id=FechaTribunal]').val('');
    } else {
        var jsonDate = datos[13].replace(/\D+/g, '');//reemplaza todos los caracteres a excepcion de numeros
        var date = new Date(parseInt(jsonDate)); //jsonDate.substr(6), 10)
        var fechaTribunal = ('0' + date.getDate()).slice(-2) + '/'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
            + date.getFullYear();

        $('#frmAvanceDemanda input[id=FechaTribunal]').val(fechaTribunal);
    }

    $('#frmAvanceDemanda textarea[id=Comentarios]').val(datos[16]);
}

function fnGuardarAvanceDemandaMasiva() {
    var encargado = $('#frmAvanceDemanda input[id=usridHidden]').val();
    var newUrl = "/Judicial/GuardarAvanceDemandaMasiva?"
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
            url: newUrl, // we are calling json method
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            data: JSON.stringify(datos),
            success: function(data) {
                if (data > 0) {
                    fnRefreshGridPanelDemandasMasivas();
                    $('#ppAvanceDemanda').dialog('close');
                } else {
                    alert(data);
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                alert('Error al guardar Avance demanda.' + errorThrown);
            }

        });
    } else {
        alert("Debe ingresar el encargado de la demanda");
    }
}

function fnGuardarAvanceDemandaMasivaRol() {

    if ($('#frmAvanceDemanda input[id=ctcidHidden]').val() == "" || $('#frmAvanceDemanda input[id=pclidHidden]').val() == "" || $('#Rol').val() == "" || $("#TribunalSelect").val() == "" || $("#TipoCausa").val() == "" || $("#MateriaJudicial").val() == "") {
        alert("Debe ingresar todos los datos mandatorios.");
    }
    else {

        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarAvanceDemandaMasivaRol/", // we are calling json method
            dataType: 'json',
            async: true,
            beforeSend: function() { $("body").addClass("loading"); },
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
            success: function(resultData) {
                $("body").removeClass("loading");
                if (resultData.success) {
                    $("#Rolid").val(resultData.rolId);
                    if ($("#Rolid").val() != "0" && $("#Rolid").val() != '') {
                        alert('El Rol fue guardado con exito!');
                        fnGuardarAvanceDemandaMasiva();
                        fnRefreshGridPanelDemandasMasivas();
                        $('#ppAgragarRol').dialog('close');
                        $('#ppAvanceDemanda').dialog('close');
                        $("#EnviaFechaEntrega").prop("disabled", true)
                        $("#EnviaFechaEntrega").prop('checked', false);
                        $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
                        $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
                    }
                }
                else {
                    $("body").removeClass("loading");
                    alert('Error al guardar Rol.');
                }
            }
        });
    }
}

function fnRefreshGridPanelDemandasMasivas() {
    var newUrl = "/Judicial/ListarPanelDemandasMasivas/"
    jQuery("#gridPanelDemandas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnCursoDemandaMasiva(panelId) {
    var $optionSelected = $('#gridPanelDemandas tr[id="' + panelId + '"] select[id="' + panelId + '_CursoDemanda"]').val();
    console.log($optionSelected);
    fnGuardarCursoDemandaMasiva(panelId, $optionSelected);
}

function formatCursoDemandaMasiva(cellvalue, options, rowObject) {
    return tipoCursoDemandaMasiva(cellvalue, rowObject);
}

function tipoCursoDemandaMasiva(cellvalue, rowObject) {
    var selectSi = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
    var selectNo = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
    var seleccione = '<select role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

    var selectSiDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1" selected="selected">SI</option><option role="option" value="0">NO</option></select>';
    var selectNoDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1">--</option><option role="option" value="1">SI</option><option role="option" value="0" selected="selected">NO</option></select>';
    var seleccioneDisabled = '<select disabled role="select" id="' + rowObject[0] + '_CursoDemanda" name="CursoDemanda" size="1" class="editable" onchange="fnCursoDemandaMasiva(' + rowObject[0] + ');"><option role="option" value="-1" selected="selected">--</option><option role="option" value="1">SI</option><option role="option" value="0">NO</option></select>';

    if (rowObject[17] == "S") {
        if (cellvalue == "SI")
            return selectSiDisabled;
        else
            return seleccione;
    }
    if (rowObject[17] == "N")
        return seleccioneDisabled;

}

function fnGuardarCursoDemandaMasiva(PanelId, DemandaCursoSelect) {
    var newUrl = "/Judicial/GuardarCursoDemandaMasiva/?"
    var datos = {
        panelId: PanelId,
        cursoDemanda: DemandaCursoSelect,
        motivo: ''
    };

    $.ajax({
        type: 'POST',
        url: newUrl, // we are calling json method
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        async: true,
        data: JSON.stringify(datos),
        beforeSend: function() { $("body").addClass("loading"); },
        success: function(data) {
            $("body").removeClass("loading");
            jQuery("#gridPanelDemandas").jqGrid().trigger('reloadGrid', [{ page: 1 }])

        },
        error: function(jqXHR, textStatus, errorThrown) {
            $("body").removeClass("loading");
            alert('Error al guardar curso de la demanda.' + errorThrown);
        }

    });
}

function fnExcelPanelDemandasMasivas() {
    var url = "/Judicial/ExportToExcelPanelDemandasMasivas";
    window.location.href = url;
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
