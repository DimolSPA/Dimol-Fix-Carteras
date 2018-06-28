/*******************************************
03             PANEL DEMANDAS
*******************************************/
function fnBotonesGridPanelDemandas(cellvalue, options, rowobject) {
    var html = '\
        <div class="tabla"><div class="fila">\
            <div class="col"><button type="button" class="ui-icon ui-icon-document" style="height:20px;width:20px" onclick="fnEditarPanel(\'' + rowobject + '\')">Editar</button></div>\
        </div></div>';

    return html;
}

function fnBotonEliminarPanelDemandas(cellvalue, options, rowobject) {
    var html = '\
        <div class="tabla"><div class="fila">\
            <div class="col"><button type="button" class="ui-icon ui-icon-trash" style="height:20px;width:20px" onclick="fnEliminarPanelDemanda(\'' + rowobject + '\')">Eliminar</button></div>\
        </div></div>';

    return html;
}

function fnEditarPanel(id) {
    var datos = id.split(',');
    var d = $("#ppAvanceDemanda").dialog();

    frmAvanceDemandaReset();
    $('#frmAvanceDemanda input[id=PanelProcesado]').val(datos[19]);

    if (datos[19] == 'S') {
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
    if (datos[23] != 0) {
        if (datos[23] != $('#frmAvanceDemanda input[id=GetUsuario]').val()) {
            $("#EnviaFechaEntrega").prop("disabled", true);
            $('#frmAvanceDemanda input[id=FechaEntrega]').datepicker().datepicker('disable');
            $('#frmAvanceDemanda input[id=FechaTribunal]').datepicker().datepicker('disable');
            $("#btnGuardar").prop('disabled', true);
        }
    }

    //Habilitar para Control de Gestión
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

    var datos = id.split(',');
    $('#frmAvanceDemanda input[id=PanelId]').val(datos[0]);
    $('#frmAvanceDemanda input[id=usridHidden]').val(datos[23]);
    $('#frmAvanceDemanda input[id=rutDeudorHidden]').val(datos[6]);
    $('#frmAvanceDemanda input[id=nombreDeudorHidden]').val(datos[7]);
    $('#frmAvanceDemanda input[id=rutClienteHidden]').val(datos[26]);
    $('#frmAvanceDemanda input[id=nombreClienteHidden]').val(datos[5]);
    $('#frmAvanceDemanda input[id=pclidHidden]').val(datos[24]);
    $('#frmAvanceDemanda input[id=ctcidHidden]').val(datos[25]);
    $('#frmAvanceDemanda input[id=CountFechaEntrega]').val(datos[27]);

    $('#ppAvanceDemanda').dialog('open');

    //Si no Encargado confección
    if (datos[12] == '' || datos[12] == null) {
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
        $('#frmAvanceDemanda label[for=Encargado]').text(datos[12]);
        d.dialog("option", "height", $("#divTabla").outerHeight() + 100);
    }

    //Si no se ha enviado fecha de entrega
    if (datos[27] == 0) {
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

    if (datos[13] == '' || datos[13] == null) {
        $('#FechaConfeccion').datepicker().datepicker('setDate', 'today');
    } else {
        var jsonDate = datos[13].replace(/\D+/g, ''); //reemplaza todos los caracteres a excepcion de numeros
        var date = new Date(parseInt(jsonDate));
        var fechaConfeccion = ('0' + date.getDate()).slice(-2) + '/'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
            + date.getFullYear()

        $('#frmAvanceDemanda input[id=FechaConfeccion]').val(fechaConfeccion);
    }


    if (datos[14] == '' || datos[14] == null) {
        $('#frmAvanceDemanda input[id=FechaEntrega]').val('');
        $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val('');
    } else {
        var jsonDate = datos[14].replace(/\D+/g, ''); //reemplaza todos los caracteres a excepción de números
        var date = new Date(parseInt(jsonDate));
        var fechaEntrega = ('0' + date.getDate()).slice(-2) + '/'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
            + date.getFullYear()

        $('#frmAvanceDemanda input[id=FechaEntrega]').val(fechaEntrega);
        $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val(fechaEntrega);
    }

    if (datos[15] == '' || datos[15] == null) {
        $('#frmAvanceDemanda input[id=FechaTribunal]').val('');
    } else {
        var jsonDate = datos[15].replace(/\D+/g, ''); //Reemplaza todos los caracteres a excepción de números
        var date = new Date(parseInt(jsonDate));
        var fechaTribunal = ('0' + date.getDate()).slice(-2) + '/'
            + ('0' + (date.getMonth() + 1)).slice(-2) + '/'
            + date.getFullYear()

        $('#frmAvanceDemanda input[id=FechaTribunal]').val(fechaTribunal);
    }

    $('#frmAvanceDemanda textarea[id=Comentarios]').val(datos[18]);
}

function fnEliminarPanelDemanda(id) {
    var r = confirm("Desea eliminar esta demanda?");

    if (r == true) {
        var datos = id.split(',');
        var url = "/Judicial/EliminarPanelDemanda?IdPanelDemanda=" + datos[0];

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

function frmAvanceDemandaReset() {
    $("#EnviaFechaEntrega").prop('checked', false);
    $('#frmAvanceDemanda input[id=PanelId]').val('');
    $('#frmAvanceDemanda input[id=usridHidden]').val('');
    $('#frmAvanceDemanda input[id=rutDeudorHidden]').val('');
    $('#frmAvanceDemanda input[id=nombreDeudorHidden]').val('');
    $('#frmAvanceDemanda input[id=rutClienteHidden]').val('');
    $('#frmAvanceDemanda input[id=nombreClienteHidden]').val('');
    $('#frmAvanceDemanda input[id=pclidHidden]').val('');
    $('#frmAvanceDemanda input[id=ctcidHidden]').val('');
    $('#frmAvanceDemanda input[id=FechaConfeccion]').val('');
    $('#frmAvanceDemanda input[id=FechaEntrega]').val('');
    $('#frmAvanceDemanda input[id=FechaEntregaHidden]').val('');
    $('#frmAvanceDemanda input[id=FechaTribunal]').val('');
    $('#frmAvanceDemanda textarea[id=Comentarios]').val('');
    $('#frmAvanceDemanda input[id=NombreUsuario]').val('');
    $('#frmAvanceDemanda label[for=Encargado]').text('');
}

function fnCambiarFechaEntrega() {
    if ($("#EnviaFechaEntrega").is(':checked')) {
        $("#FechaEntrega").val('')
        $("#FechaEntrega").datepicker('enable');

    } else {
        $("#FechaEntrega").val($("#FechaEntregaHidden").val())
        $('#FechaEntrega').datepicker().datepicker('disable');
    }
}
function fnGuardarAvanceDemanda() {
    var encargado = $("#usrid").val() == '' ? $('#frmAvanceDemanda input[id=usridHidden]').val() : $("#usrid").val();
    var newUrl = "/Judicial/GuardarAvanceDemanda/?"
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
            success: function (data) {
                if (data > 0) {
                    fnRefreshGridPanelDemandas();
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

function fnRefreshGridPanelDemandas() {
    var newUrl = "/Judicial/ListarPanelDemandas"
    jQuery("#gridPanelDemandas").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function fnIngresarRol(dateText) {
    if ($('#frmAvanceDemanda input[id=FechaEntrega]').val() == '') {
        alert("Debe ingresar y guardar la fecha de entrega");
        $('#frmAvanceDemanda input[id=FechaTribunal]').val('');
    } else {
        if ($('#frmAvanceDemanda input[id=PanelProcesado]').val() == 'N') {
            var r = $("#ppAgragarRol").dialog();

            $('#ppAgragarRol').dialog('open');
            refrescarFrmAvanceDemandaRol();

            $.ajax({
                type: 'POST',
                url: "/Judicial/TraeDeudorQuiebra",
                dataType: 'json',
                async: true,
                data: {
                    rutDeudor: $('#frmAvanceDemanda input[id=rutDeudorHidden]').val()
                },
                success: function (resultData) {
                    if (resultData.success) {
                        $('#frmAvanceDemandaRol input[id=Rol]').val(resultData.rolNumero);
                        $('#frmAvanceDemandaRol input[id=TribunalSelect]').val(resultData.tribunalId);
                        $('#frmAvanceDemandaRol input[id=Tribunal]').val(resultData.tribunal);
                        $('#frmAvanceDemandaRol select[id=TipoCausa]').val(resultData.tipoCausaId);
                        $('#frmAvanceDemandaRol select[id=MateriaJudicial]').val(resultData.materiaJodicialId);
                        $('#ComboQuiebra > option[value="S"]').attr('selected', 'selected');
                    }
                }
            });
            $('#frmAvanceDemandaRol input[id=FechaDemanda]').val(dateText);
            $('#frmAvanceDemandaRol input[id=NombreRutDeudor]').val($('#frmAvanceDemanda input[id=rutDeudorHidden]').val() + " - " + $('#frmAvanceDemanda input[id=nombreDeudorHidden]').val())
            $('#frmAvanceDemandaRol input[id=NombreRutCliente]').val($('#frmAvanceDemanda input[id=rutClienteHidden]').val() + " - " + $('#frmAvanceDemanda input[id=nombreClienteHidden]').val())
        }
    }
}

function fnGuardarAvanceDemandaRol() {
    if ($('#frmAvanceDemanda input[id=ctcidHidden]').val() == "" || $('#frmAvanceDemanda input[id=pclidHidden]').val() == "" || $('#Rol').val() == "" || $("#TribunalSelect").val() == "" || $("#TipoCausa").val() == "" || $("#MateriaJudicial").val() == "") {
        alert("Debe ingresar todos los datos mandatorios.");
    } else {
        $.ajax({
            type: 'POST',
            url: "/Judicial/GuardarAvanceDemandaRol/", // we are calling json method
            dataType: 'json',
            async: true,
            beforeSend: function () { $("body").addClass("loading"); },
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
                        debugger;
                        fnGuardarAvanceDemandaMasiva();
                        debugger;
                        fnRefreshGridPanelDemandas();
                        $('#ppAgragarRol').dialog('close');
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

function fnCargarOrgChartPanelDemanda() {
    $.ajax({
        type: 'POST',
        url: "/Judicial/ListarPanelDemandaControlGestion", // we are calling json method
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
                    '<div onclick="fnPanelDemandaReporte(\'' + chartsdata[i].Id + '\',\'' + chartsdata[i].Item + '\')"style=\"cursor:pointer\"><div style="color:blue; font-style:italic">' + chartsdata[i].Total + '</div></div>';
                //'<div onclick="fnPanelDemandaReporte(\'' + chartsdata[i].Id + '\','+ chartsdata[i].Item + '\)"style=\"cursor:pointer\"><div style="color:blue; font-style:italic">' + chartsdata[i].Total + '</div></div>';
                data.addRow([{ v: chartsdata[i].Id, f: contenido }, chartsdata[i].Parent, chartsdata[i].Item]);

            }
            var chart = new google.visualization.OrgChart(document.getElementById('chartPanelDemanda'));
            chart.draw(data, { allowHtml: true });

        },
        error: function (ex) {
            alert('Error al cargar el panel.');
        }

    });
}