var url = relativeRoot + 'StudiuPiata';
var entityName = 'studiul de piata';

$(document).ready(function () {
    loadGrid();

    $(document).on('click', '.edit', function (e) {
        e.preventDefault();
        Api.loadPartialDialog(url, $(this).attr('id'), entityName);
    });

    $(document).on('click', '#btn-save', function () {
        Api.saveFromDialog(url, $('#edit-form'));
    });

    $(document).on('click', '.delete', function (e) {
        e.preventDefault();
        Api.deleteWithConfirm(url, $(this).attr('id'), entityName);
    });
});

function loadGrid() {
    var colModel = [
                { name: 'Nume', index: 'Nume', label: 'Nume', align: 'left', search: true, width: 3 },
                { name: 'NrTuristi', index: 'NrTuristi', label: 'Numar Turisti', search: true, align: 'left', width: 3 },
                { name: 'DataStudiu', index: 'DataStudiu', label: 'Data Studiului', search: true, align: 'left', width: 3 },
                { name: 'Detalii', index: 'Detalii', label: 'Detalii', search: true, align: 'left', width: 3 },
                { name: 'Actions', index: 'Actions', label: 'Actiuni', align: 'center', search: false, sortable: false, formatter: actionsLink, width: 105, fixed: true }
    ];

    Api.loadGrid(url, colModel, 'Nume');
}


