

/* Ensure layout - @Scripts.Render("~/bundles/jqueryVal") */

//VALIDATION SUPPORT FOR MODAL DIALOGS
//http://stackoverflow.com/questions/14902581/unobtrusive-validation-not-working-with-dynamic-content
$('.modal').on('shown.bs.modal', function (event) {
    if ($.validator.unobtrusive != undefined) {
        $.validator.unobtrusive.parse("form");
    }
});

//VALIDATION SUPPORT FOR BOOTSTRAP TOOLTIPS
jQuery.validator.setDefaults({
    onkeyup: false,
    showErrors: function (errorMap, errorList) {
        $.each(this.successList, function (index, value) {
            return $(value).popover("hide");
        });
        return $.each(errorList, function (index, value) {
            var _popover;
            _popover = $(value.element).popover({
                trigger: "manual",
                placement: $(value.element).attr('data-placement') || "auto", //Allow override
                content: value.message,
                template: "<div class=\"popover\"><div class=\"arrow\"></div><div class=\"popover-inner\"><div class=\"popover-content\"><p></p></div></div></div>"
            });
            // Bootstrap 3:
            _popover.data("bs.popover").options.content = value.message;
            // Bootstrap 2.x:
            // _popover.data("popover").options.content = value.message;
            return $(value.element).popover("show");
        });
    }
});

