$(document).ready(function () {
    // $(".checkboxs").click(function () {
    //     if($(this).hasClass("checked")){
    //         $(this).removeClass("checked");
    //         $(this).children("input").click();
    //         $(this).children("input").removeAttr("checked");
    //     }else{
    //         $(this).addClass("checked");
    //         $(this).children("input").click();
    //         $(this).children("input").attr("checked","checked");
    //     }
    // });
    //
    // $('.checkboxSave').change(function () {
    //     if ($(this).is(':checked')) {
    //         $(this).parent().addClass("checked");
    //     }else{
    //         $(this).parent().removeClass("checked");
    //     }
    // });
    // $("#squaredOne").click(function () {
    //     alert($(this).attr("type"))
    // })
    $(".checkboxs .label").click(function () {
        $(this).parent().children(".squaredOne").children("input").click();
    });
    $(".OnOf").click(function () {
        if ($(this).hasClass("active")) {
            $(this).removeClass("active");
            $(this).children("input").val("0")
        } else {
            $(this).addClass("active");
            $(this).children("input").val("1")
        }
    })

    $(".searchFilterBrand").keyup(function (e) {
        var txt = $(this).val().toLowerCase();
        $(this).parent().children(".BoxCheck.brandss").children(".checkboxs").css({ 'display': "none" });
        // $(".BoxCheck.brandss .checkboxs").css({'display':"none"});
        $(this).parent().children(".BoxCheck.brandss").children("h4.label").children("small")
        // $(".BoxCheck.brandss h4.label small").each(function (i) {
        $(this).parent().children(".BoxCheck.brandss").children(".checkboxs").children("h4.label").children("small").each(function (i) {
            if (txt.length > 0) {
                if ($(this).html().toLowerCase().search(txt) >= 0) {
                    $(this).parent().parent().attr("data", "ok")
                    $(this).parent().parent().css({ 'display': "flex" });
                }
            } else {
                $(this).parent().parent().css({ 'display': "flex" })
              

            }
        });
    })
})
