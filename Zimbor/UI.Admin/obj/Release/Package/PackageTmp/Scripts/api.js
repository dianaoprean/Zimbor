var Api = Api || {};

Api = (function () {
    var saveUrl = '/Save';
    var deleteUrl = '/Delete';
    var getUrl = '/Get';
    var getAllUrl = '/GetAll';

    var defaultColModel = [
            { name: 'Nume', index: 'Nume', label: 'Nume', align: 'left', search: true, width: 3 },
            { name: 'Descriere', index: 'Descriere', label: 'Descriere', search: true, align: 'left', width: 3 },
            { name: 'DataAdaugarii', index: 'DataAdaugarii', label: 'Data Adaugarii', search: false, align: 'left', width: 3 },
            { name: 'Actions', index: 'Actions', label: 'Actiuni', align: 'center', search: false, sortable: false, formatter: actionsLink, width: 105, fixed: true }
    ];


    var saveFromDialog = function (entity, form) {
        //if (form.valid()) {
            $.ajax({
                url: entity + saveUrl,
                type: 'POST',
                data: form.serialize(),
                success: function (data) {
                    if (data.success == true) {
                        $('#partial').modal('hide');
                        $("#list").trigger("reloadGrid");
                    } else {
                        $('.modal-body').html(data);
                    }
                }
            });
       // }
    }

    var deleteWithConfirm = function (entity, id, obj) {
        var title = 'Sunteti sigur ca doriti sa stergeti ' + obj + '?';

        BootstrapDialog.confirm(title, function (result) {
            if (result) {
                $.ajax({
                    url: entity + deleteUrl + '?id=' + id,
                    type: 'POST',
                    success: function (data) {
                        if (data.success == true) {
                            $("#list").trigger("reloadGrid");
                        } else {
                            alert('err');
                        }
                    }
                });
            }
        });
    }

    var loadPartialDialog = function (entity, id, obj) {
        var title = 'Editare ' + obj;
        if ((id == 0) || (id == '0')) {
            title = 'Adaugare ' + obj;
        }
        var url = entity + getUrl + '?id=' + id;

        $('.modal-body').load(url, function (data) {
            $('.modal-body').html(data);
            $('.modal-title').text(title);
            $('#partial').modal();
        });
    }

    var loadGrid = function (entity, colModel, sortName) {
        if ((colModel == undefined) || (colModel == null)) {
            colModel = defaultColModel;
        }

        $('#list').trigger('GridUnload');
        $('#list').jqGrid({
            url: entity + getAllUrl,
            datatype: 'json',
            mtype: 'POST',
            colModel: colModel,
            sortname: sortName,
            sortorder: 'asc',
            loadonce: false,
            rowList: [5, 10, 20, 30],
            viewrecords: true,
            pager: $('#pager'),
            shrinkToFit: true,
            autowidth: true,
            height: 400,
            altRows: true,
            altclass: 'jqgrid-alt',
        });

        $('#list').jqGrid('filterToolbar', { stringResult: true });
    }

    var loadGridDefault = function (entity, sortName) {
        loadGrid(entity, defaultColModel, sortName);
    }


    return {
        saveFromDialog: saveFromDialog,
        deleteWithConfirm: deleteWithConfirm,
        loadPartialDialog: loadPartialDialog,
        loadGrid: loadGrid,
        loadGridDefault: loadGridDefault
    };
})();
