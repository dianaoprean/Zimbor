var relativeRoot = '/';

$(document).ready(function () {
    $.ajaxSetup({ cache: false });
    $.ajaxSetup({ error: onError });
    relativeRoot = $('#relativeRoot').val();

});

$(window).bind('resize', function () {
    var width = $('.box').width() - 20;
    $('#list').setGridWidth(width);
});

var onError = function (xhr, textStatus, errorThrown) {

    BootstrapDialog.alert({
        title: "Error",
        message: "Action was not completed successfully. Please try again",
        //cssClass: "dsf",
        type: BootstrapDialog.TYPE_DANGER,
        closable: true,
        buttonLabel: 'OK'
    });
};

function actionsLink(cellvalue, options, rowObject) {
    return "<div class='actions'><a href='' id=" + cellvalue + " class='edit' title='Editeaza'><i class='fa fa-edit'></i></a>&nbsp;  <a href='#' id=" + cellvalue + " class='delete' title='Sterge'><i class='fa fa-remove'></i></a></div>";
}
