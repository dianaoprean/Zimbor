var url = relativeRoot + 'PrimireTuristica';
var entityName = 'structura de primire turistica';

$(document).ready(function () {
    loadGrid();

    $(document).on('click', '.edit', function (e) {
        e.preventDefault();
        Api.loadPartialDialog(url, $(this).attr('id'), entityName);
    });

    $(document).on('click', '#btn-save', function () {
        var form = $('#edit-form');
        var images = $(".image");
        var imageIds = [];
        
        $(".image").each(function (k, v) {
            imageIds.push(v.id);
        });

        var data = form.serialize() + "&ImageIds=" + imageIds;

        if (form.valid()) {
            $.ajax({
                url:  url + '/Save',
                type: 'POST',
                data: data,
                success: function (data) {
                    if (data.success == true) {
                        $('#partial').modal('hide');
                        $("#list").trigger("reloadGrid");
                    } else {
                        $('.modal-body').html(data);
                    }
                }
            });
        }
    });

    $(document).on('click', '.delete', function (e) {
        e.preventDefault();
        Api.deleteWithConfirm(url, $(this).attr('id'), entityName);
    });
});

function loadGrid() {
    var colModel = [
                { name: 'Nume', index: 'Nume', label: 'Nume', align: 'left', search: true, width: 3 },
                { name: 'Strada', index: 'Strada', label: 'Strada', search: true, align: 'left', width: 3 },
                { name: 'Localitate', index: 'Localitate', label: 'Localitate', search: true, align: 'left', width: 3 },
                { name: 'CodPostal', index: 'CodPostal', label: 'Cod Postal', search: true, align: 'left', width: 3 },
                { name: 'Zona.Nume', index: 'Zona', label: 'Zona', search: true, align: 'left', width: 3 },
                { name: 'NumeProprietar', index: 'NumeProprietar', label: 'Nume Proprietar', search: true, align: 'left', width: 3 },
                { name: 'Telefon', index: 'Telefon', label: 'Telefon', search: true, align: 'left', width: 3 },
                { name: 'Actions', index: 'Actions', label: 'Actiuni', align: 'center', search: false, sortable: false, formatter: actionsLink, width: 105, fixed: true }
    ];

    Api.loadGrid(url, colModel, 'Nume');
}


