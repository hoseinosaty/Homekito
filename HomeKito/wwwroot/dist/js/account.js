
$(window).on("scroll", function () {
    if ($(this).scrollTop() > 20) {
        $("body").addClass("scrollDown");
    } else {
        $("body").removeClass("scrollDown");
    }

});

$(document).ready(function () {
    $(".scroll").mCustomScrollbar({
        axis: "y",
        rtl: true,
        theme: "dark-3",
        scrollButtons: {
            enable: true
        },
    });
    if ($("input.inputSearch").length) {
        $("input.inputSearch").on("keyup", function () {
            $(".moreResultSearch").attr('href', "");
            $(".moreResultSearch").attr('href', "/Search/" + $(".moreResultSearch").attr('href') + $(this).val());
            $(".moreResultSearch span").html($(this).val());
            if ($(this).val().length >= 1) {
                $(".contentNavSearch i.removeVal").addClass("show");
            } else {
                $(".contentNavSearch i.removeVal").removeClass("show");
            }
            if ($(this).val().length >= 3) {
                $("nav.navbar .SearchNav").addClass("show");
            } else {
                $("nav.navbar .SearchNav").removeClass("show");
            }
            $("i.bgResultSearch").click(function () {
                $("nav.navbar .SearchNav").removeClass("show");
            });
        })
    }
    function removeVal() {
        $("input.inputSearch").val('');
        $(".contentNavSearch i.removeVal").removeClass("show");
    }

    $(".myRow .myCol.sm a.fal.fa-angle-left").click(function (e) {
        e.preventDefault();
        var det = $(this).parent().parent().data('id');
        $(".content_profile .loading").addClass('show');
        $("html, body").css({ 'overflow': 'hidden' });
        setTimeout(function () {
            // $(".detailThisRow").load('address page');
            $(".detailThisRow").addClass('show');
        }, 500);
        setTimeout(function () {
            $(".content_profile .loading").removeClass('show');
        }, 1000);

        $(".detailThisRow").addClass('show');
    })
    $(".detailThisRow .close").click(function () {
        $("html, body").css({ 'overflow': 'auto' });
        $(this).parent().removeClass('show');
    });
    $(".remove_fav").click(function (e) {
        e.preventDefault();
        $(this).parent().remove();
    });
    $(".other_links.box_profile a , .opentab ").each(function () {
        if ($(this).hasClass('active')) {
            var id = $(this).attr('href');
            $(id).addClass('active');

            $(".Title_left_profile").text($(this).text());
        }
    });
    $("a.editProfilee").click(function (e) {
        e.preventDefault();
        $(".other_links.box_profile a[href='#edit_profile']").click();
    });
    $(".other_links.box_profile a , .opentab").click(function (e) {
        e.preventDefault();
        $t = $(this);
        $(".content_profile .loading").addClass('show');
        setTimeout(function () {
            $(".opentab").removeClass('active');
            $(".other_links.box_profile a").removeClass('active');
            $t.addClass('active');
            $(".Title_left_profile").text($t.text());
            $(".content_profile div.active").removeClass('active');
            var id = $t.attr('href');
            $(id).addClass('active');
            setTimeout(function () {
                $(".content_profile .loading").removeClass('show');
            }, 1000);
        }, 500);

    });
    $(".change_password").click(function () {
        $(".modal_invite_website.md2").addClass('show');
    });
    $(".invite_website").click(function () {
        $(".modal_invite_website.md1").addClass('show');
    });
    $(".content_modal_invite_website .close").click(function () {
        $(this).parent().parent().removeClass('show');
    })
});

if ($("#requests").length || $("#transaction").length) {
    $("head").append(`
        <link rel="stylesheet" href="dist/css/datatable/jquery.dataTables.min.css">
        <link rel="stylesheet" href="dist/css/datatable/select.dataTables.min.css">
        <link rel="stylesheet" href="dist/css/datatable/editor.dataTables.min.css">
        <link rel="stylesheet" href="dist/css/datatable/buttons.dataTables.min.css">
        <link rel="stylesheet" href="dist/css/datatable/colReorder.dataTables.min.css">
    `);
}
//if ($("#requests").length) {
//    $("#requests").DataTable({
//        language: {
//            "url": "dist/js/datatable/Persian.json"
//        },
//        dom: 'Bfrtip',
//        buttons: [
//            // 'copyHtml5',
//            // 'excelHtml5',
//            // 'csvHtml5',
//            // 'pdfHtml5'
//        ]
//    });
}
if ($("table").length) {
    $("table").DataTable({
        language: {
            "url": "dist/js/datatable/Persian.json"
        },
        dom: 'Bfrtip',
        buttons: [
            // 'copyHtml5',
            // 'excelHtml5',
            // 'csvHtml5',
            // 'pdfHtml5'
        ]
    });
}

if ($("span.barRightProfile").length) {
    $("span.barRightProfile").click(function () {
        if ($("section.user_account").hasClass("full")) {
            $("section.user_account").removeClass("full")
        } else {
            $("section.user_account").addClass("full")
        }
    })
}

// if ($(".modal").length) {
//     $("button.openModal").click(function () {
//         var dtLughbox = $(this).attr('data-lightbox');
//         $(".modal[data-lightbox='" + dtLughbox + "']").addClass('show');
//         var dt = $(".modal[data-lightbox='" + dtLughbox + "']").find("button.tabBtn.active").attr("data");

//         $(".modal[data-lightbox='" + dtLughbox + "']").find(".boxTabShow").removeClass("active")

//         $(".modal[data-lightbox='" + dtLughbox + "']").find(".boxTabShow[data='" + dt + "']").addClass("active")
//     });
//     $(".modal .bgModal").click(function () {
//         $(this).parent().addClass('hide');
//         setTimeout(function () {
//             $(".modal").removeClass('hide').removeClass('show');
//         }, 500)
//     });
//     $("i.closeModal").click(function () {
//         $(this).parent().parent().addClass('hide');
//         setTimeout(function () {
//             $(".modal").removeClass('hide').removeClass('show');
//         }, 500)
//     });
// }


if ($(".BrandFooter").length) {
    var BrandFooter = new Swiper('.BrandFooter', {
        slidesPerView: 4,
        // spaceBetween: 30,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el: '.swiper-pagination',
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        autoplay: {
            delay: 7000,
        },
        breakpoints: {
            0: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            480: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            680: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            890: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1100: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1500: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
        }
    });
}
if ($(".itemModal[data-modalName]")) {
    $(".itemModal").click(function () {
        var modalName = $(this).attr('data-modalName');
        $(".modal").removeClass("active");
        $(`.modal[data-modalName="${modalName}"]`).addClass("active")
    });
}
function closeModal() {
    $(".modal").removeClass("active");
}


//function getAddress() {
//    var address = $(".getAddress").val();
//    var oddEven = $("table#addresslist tbody tr:last-child").attr("class"); var eo;
//    if (address.length > 3) {
//        var lastChildAddres = parseInt($("table#addresslist tbody tr:last-child td:first-child").text());
//        (oddEven == "even") ? eo = "odd" : eo = "even";
//        var elRadio = `
//                <tr role="row" class="${eo}">
//                    <td class="sorting_1">
//                        ${lastChildAddres + 1}
//                    </td>
//                    <td>
//                        ${address}
//                    </td>
//                    <td><span class="success">فعال</span></td>
//                    <td><button class="btnremove fas fa-times" onclick="removeTr(this,'address',15)"></button></td>
//                </tr>
//        `;

//        $("table#addresslist tbody").append(elRadio);

//        hrb_notify([
//            'success',
//            '',
//            'fa-map-marked-alt',
//            "آدرس جدید با موفقیت ثبت شد",
//            'bottomLeft',
//            'flipInY',
//            'flipOutX',
//            '5'
//        ]);
//        $("table#transaction tbody").append(elRadio);

//        hrb_notify([
//            'success',
//            '',
//            'fa-map-marked-alt',
//            "آدرس جدید با موفقیت ثبت شد",
//            'bottomLeft',
//            'flipInY',
//            'flipOutX',
//            '5'
//        ]);
//        $(".getAddress").val('');
//    } else {
//        hrb_notify([
//            'danger',
//            '',
//            'fa-map-marked-alt',
//            "لطفا آدرس را به درستی وارد نمایید",
//            'bottomLeft',
//            'flipInY',
//            'flipOutX',
//            '5'
//        ]);
//    }

//}


//function removeTr(t,type, id) {
//    var elTr = $(t).parent().parent();
//    // type = table address
//    // type = id address
//    elTr.remove()
//}