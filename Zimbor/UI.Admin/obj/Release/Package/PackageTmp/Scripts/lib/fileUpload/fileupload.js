/// <reference path="../../lib/_references.js" />

var dlgUploadAds;
var adsDisplayLeftCookieName = 'LIBRARYLEFTDISPLAYTYPE';
$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload({
        dropZone: $('#dropzone'),
        acceptFileTypes: /(\.|\/)(jpe?g|gif|bmp|png|tif|wmv|avi|mpeg|mpg|swf|txt|csv|doc|docx|mkv|mp4)$/i
    });

    $('#fileupload').bind('fileuploaddestroyed', function (e, data) {
        var isChecked = getCheckedItems();
        if ($('input.assetChk').length == 0 || isChecked) {
            $('.deleteAllBtn').attr("disabled", "disabled");
            $('.assocAllBtn').attr("disabled", "disabled");
            $('.selectAllChk').attr("disabled", "disabled");
        }
    });

    $('#fileupload').bind('fileuploadcompleted', function (e, data) {
        alert('completed');
        $('.selectAllChk').removeAttr("disabled");
        if ($('.template-upload').length == 0) {
            $('.uploadAllBtn').attr("disabled", "disabled");
            $('.cancelAllBtn').attr("disabled", "disabled");
        }
        //$('.deleteAllBtn').removeAttr("disabled");
        //$('.assocAllBtn').removeAttr("disabled");
    });

    $('#fileupload').bind('fileuploadadded', function (e, data) {
        $('.selectAllActions').removeClass('hide');
        if ($('.template-upload').length > 0) {
            $('.uploadAllBtn').removeAttr("disabled");
            $('.cancelAllBtn').removeAttr("disabled");
        }
    });

    // Initialize drag and drop functionality
    $(document).bind('dragover', function (e) {
        var dropZone = $('#dropzone'),
            timeout = window.dropZoneTimeout;
        if (!timeout) {
            dropZone.addClass('in');
        } else {
            clearTimeout(timeout);
        }
        var found = false,
            node = e.target;
        do {
            if (node === dropZone[0]) {
                found = true;
                break;
            }
            node = node.parentNode;
        } while (node != null);
        if (found) {
            dropZone.addClass('hover');
        } else {
            dropZone.removeClass('hover');
        }
        window.dropZoneTimeout = setTimeout(function () {
            window.dropZoneTimeout = null;
            dropZone.removeClass('in hover');
        }, 100);
    });


    $(".btnUploadAds").click(function () {
        openUploadAdsDialog();
    });




});

function UploadDone() {
    $('#assetPropertyContainer').html('');
    $('#divUploadAdsDealersAndClientsContainer').html('');
    $('.selectAllActions').addClass('hide');
    dlgUploadAds.dialog('close');
    pageNo = 1;
    if (typeof (loadLibraryList) == 'function') loadLibraryList("loadRightContent");
    if (typeof (loadLeftContent) == 'function') loadLeftContent(true);

    //IF ScreenConfiguration or Loop->ViewSlots
    var displayType = $.cookie(adsDisplayLeftCookieName);
    displayType = (displayType != "list") ? "grid" : displayType;

    if (typeof (LoadAssetsGrid) == 'function' && displayType == "grid") LoadAssetsGrid();
    if (typeof (LoadAssetsList) == 'function' && displayType == "list") LoadAssetsList();
}

function openUploadAdsDialog(multipleAds) {
    if (dlgUploadAds == undefined || dlgUploadAds.length == 0)
        dlgUploadAds = $("#dlgUploadAds");

    dlgUploadAds.dialog({
        width: 960, height: 740, modal: true, resizable: false,
        close: function () {
            // clear the dialog
            dlgUploadAds.find("tr.template-upload, tr.template-download").remove();
            dlgUploadAds.find('.dealerCheckbox, .clientCheckbox').removeAttr('checked');
            $('#clientContainerDialog .multiselectList').html('');
            populateClientsForSelectedDealers('');

            UploadDone();
        }
    });
}
