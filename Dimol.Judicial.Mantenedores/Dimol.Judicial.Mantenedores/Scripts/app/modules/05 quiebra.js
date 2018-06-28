function fnPanelQuiebraLoaded() {
    $('#gridPanelQuiebra>tbody>.jqgroup').each(function (i, e) {
        var nodes = $('#gridPanelQuiebra>tbody>tr:eq(' + (e.rowIndex + 1) + ')')[0].childNodes;
        var tribunal = $('#gridPanelQuiebra>tbody>tr:eq(' + (e.rowIndex + 1) + ')')[0].childNodes[nodes.length - 1].innerText;
        var headArr = e.getElementsByTagName('b')[0].innerText.split("-");
        e.getElementsByTagName('b')[0].innerText = headArr[0].trim() + "-" + headArr[0].trim() + " - " + tribunal + " - " + headArr[2].trim();
    });
}