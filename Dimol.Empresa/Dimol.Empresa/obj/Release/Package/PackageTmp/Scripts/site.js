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

function fnBuscarEmpleado() {
    var newUrl = "/Empresa/GetEmpleado/?"
    newUrl += "Rut=" + $("#RutBuscar").val() + "&Nombre=" + $("#NombreBuscar").val()
    newUrl += "&ApellidoPaterno=" + $("#ApellidoPaterno").val()
    newUrl += "&ApellidoMaterno=" + $("#ApellidoMaterno").val()
    newUrl += "&Estado=" + $("#TipoEstado").val() 
    //newUrl += "&Direccion=" + $("#DireccionBuscar").val() + "&Rol=" + $("#Rol").val() + "&SituacionCartera=" + $("#SituacionCartera").val() + "&NumeroCPBT=" + $("#NumeroCPBT").val()
    //newUrl += "&Gestor=" + $("#gesid").val()
    //alert('paso');
    jQuery("#Empleado").jqGrid().setGridParam({ url: newUrl }).trigger('reloadGrid', [{ page: 1 }])
}

function BuscarEmpleadoSeleccionado(id) {
    var url = "/Empresa/Empleado/?idd=" + id;
    window.location.href = url;//'@Url.Action("Deudores", "CarteraController", new{id='+id+'}';

}


function imageFormat(cellValue, rowId, rowData, options) {
    if (cellValue=='')
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


