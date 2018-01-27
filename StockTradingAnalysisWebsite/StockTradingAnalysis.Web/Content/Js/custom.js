var site = site || {};
site.baseUrl = site.baseUrl || "";

jQuery(document).ready(function () {
    initdatepicker();
    initdynamicrow();
    initfiledynamicrow();
    initasyncload();
    prettyPhotoInit();
    toolTipTransactionInit();
    clickableTable();
    fixDateValidation();
});

function initasyncload() {
    $(".partialContents").each(function (index, item) {
        var url = site.baseUrl + $(item).data("url");
        if (url && url.length > 0) {
            $(item).load(url);
        }
    });
}
function initdatepicker() {
    $(":input[data-datepicker]").datetimepicker({
        timeFormat: 'hh:mm',
        dateFormat: 'dd.mm.yy'
    });
}

function initdynamicrow() {
    $("#addItem").click(function () {
        $.ajax({
            url: this.href,
            cache: false,
            success: function (html) { $("#texteditorRow").append(html); }
        });
        return false;
    });

    $(document).on('click', 'a.deleteRow', function () {
        $(this).parents("div.texteditorRow:first").remove();
    });
}

function initfiledynamicrow() {
    $("#addFileItem").click(function () {
        $.ajax({
            url: this.href,
            cache: false,
            success: function (html) { $("#fileEditorRow").append(html); }
        });
        return false;
    });

    $(document).on('click', 'a.deleteFileRow', function() {
        $(this).parents("div.fileEditorRow:first").remove();
    });
}

function prettyPhotoInit() {
    $("a[rel^='prettyPhoto']").prettyPhoto({
        animation_speed: 'normal', /* fast/slow/normal */
        slideshow: 5000, /* false OR interval time in ms */
        autoplay_slideshow: false, /* true/false */
        opacity: 0.70, /* Value between 0 and 1 */
        show_title: true, /* true/false */
        allow_resize: true, /* Resize the photos bigger than viewport. true/false */
        default_width: 500,
        default_height: 344,
        counter_separator_label: '/', /* The separator for the gallery counter 1 "of" 2 */
        theme: 'dark_rounded', /* pp_default / light_rounded / dark_rounded / light_square / dark_square / facebook */
        horizontal_padding: 20, /* The padding on each side of the picture */
        hideflash: false, /* Hides all the flash object on a page, set to TRUE if flash appears over prettyPhoto */
        wmode: 'opaque', /* Set the flash wmode attribute */
        autoplay: true, /* Automatically start videos: True/False */
        modal: false, /* If set to true, only the close button will close the window */
        deeplinking: false, /* Allow prettyPhoto to update the url to enable deeplinking. */
        overlay_gallery: false, /* If set to true, a gallery will overlay the fullscreen image on mouse over */
        keyboard_shortcuts: true, /* Set to false if you open forms inside prettyPhoto */
        changepicturecallback: function () { }, /* Called everytime an item is shown/changed */
        callback: function () { }, /* Called when prettyPhoto is closed */
        ie6_fallback: true,
        custom_markup: '',
        social_tools: '<br/>'
    });
}

//Shows a tooltip on the transaction page
function toolTipTransactionInit() {
    yOffset = 50;

    $(".ToolTipTransaction").hover(function (e) {
        var param = { id: $(this).attr('id') };

        $("#toolTip").load("/Transaction/ToolTip", param);
        $("#toolTip")
            .css("top", e.pageY + "px")
            .css("left", yOffset + "px")
            .show();
    },
    function () {
        $("#toolTip").hide();
    });
    $("#toolTip").mousemove(function (e) {
        $("#toolTip")
            .css("top", e.pageY + "px")
            .css("left", yOffset + "px");
    });
}

function clickableTable() {
    $(".clickableRow").click(function () {
        window.document.location = $(this).attr("href");
    });
}

function fixDateValidation() {
    jQuery.validator.addMethod(
            'date',
            function (value, element, params) {
                if (this.optional(element)) {
                    return true;
                };
                var result = false;
                try {
                    $.datepicker.parseDate('dd.mm.yy', value);
                    result = true;
                } catch (err) {
                    result = false;
                }
                return result;
            },
            ''
        );
}