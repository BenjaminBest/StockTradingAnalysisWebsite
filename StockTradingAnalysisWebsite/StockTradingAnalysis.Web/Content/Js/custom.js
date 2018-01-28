var site = site || {};
site.baseUrl = site.baseUrl || "";

jQuery(document).ready(function () {
    changeValidationToWorkWithBootstrap();
    initdynamicrow();
    initfiledynamicrow();
    initasyncload();
    clickableTable();
    fixDateValidation();
});

function changeValidationToWorkWithBootstrap() {
    $('span.field-validation-valid, span.field-validation-error').each(function () {
        $(this).addClass('invalid-feedback');
    });

    $('form').submit(function () {
        if ($(this).valid()) {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length == 0) {
                    $(this).removeClass('has-danger');
                }

                var input = $(this).find('input.input-validation-error');
                if ($(input).length == 0) {
                    $(input).removeClass('is-invalid');
                }
            });
        }
        else {
            $(this).find('div.form-group').each(function () {
                if ($(this).find('span.field-validation-error').length > 0) {
                    $(this).addClass('has-danger');
                }

                var input = $(this).find('input.input-validation-error');
                if ($(input).length > 0) {
                    $(input).addClass('is-invalid');
                }
            });
        }
    });

    $('form').each(function () {
        $(this).find('div.form-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('has-danger');
            }

            var input = $(this).find('input.input-validation-error');
            if ($(input).length > 0) {
                $(input).addClass('is-invalid');
            }
        });
    });
}

function initasyncload() {
    $(".partialContents").each(function (index, item) {
        var url = site.baseUrl + $(item).data("url");
        if (url && url.length > 0) {
            $(item).load(url);
        }
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