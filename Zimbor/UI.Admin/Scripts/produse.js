var url = relativeRoot + 'Produse';
var entityName = 'produs';

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
    Api.loadGridDefault(url, 'Nume');
}


