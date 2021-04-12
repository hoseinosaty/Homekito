async function ValidateForm(frm) {
    var EMAILFORMAT = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var errorCount = 0;
    $(`#${frm} input,#${frm} select,#${frm} textarea`).each(function (i, el) {
        var require = $(el).attr('required');
        var related = $(el).attr('data-related');
        var min = $(el).attr('data-min');
        
        if (typeof require !== typeof undefined && require !== false) {

            if ($(el).val() == "" || $(el).val() == null || $(el).val() == "NONE") {
                $(el).next(".input-error").addClass("active").html("این فیلد اجباری میباشد");
                errorCount++;
            }
            else if ($(el).attr("type") == "email") {
                if (!EMAILFORMAT.test($(el).val())) {
                    $(el).next(".input-error").addClass("active").html("پست الکترونیکی صحیح نمیباشد");
                    errorCount++;
                }
            }
        }

        if (typeof related !== typeof undefined && related !== false) {
            let targetEl = $(el).attr("data-related");
            if ($(el).val() !== $(targetEl).val()) {
                $(el).next(".input-error").addClass("active").html("Confirm Password Not Correct.");
                errorCount++;
            }
        }
        if (typeof min !== typeof undefined && min !== false) {
            let val = $(el).attr("data-min");
            if ($(el).val().length < val) {
                $(el).next(".input-error").addClass("active").html("Your password most be at least " + val + " characters long.");
                errorCount++;
            }
        }
       

    });
    return errorCount;
}