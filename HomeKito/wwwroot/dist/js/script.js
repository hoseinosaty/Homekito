var scrolTop = 0;
var eloffsetTop = 0;
var myValues = [];
if ($(".largPicProd img").length) {
    var picProd = $(".largPicProd img").attr("src");
}
if ($(".accordion .titleAccordion").length) {
    $(".accordion .titleAccordion").click(function () {
        var $t = $(this).parent();
        if ($t.hasClass("active")) {
            $t.removeClass("active")
        } else {
            $t.addClass("active")
        }
    })
}
if ($(".showRagePrice").length) {
    var el = $(".showRagePrice");
    var startrange = (el.attr("data-start").length) ? parseInt(el.attr("data-start")) : 0;
    var endrange = (parseInt(el.attr("data-end")) > 0) ? parseInt(el.attr("data-end")) : 200000;
    var steprange = (parseInt(el.attr("data-step")) > 0) ? parseInt(el.attr("data-step")) : 100;
    var minrange = (parseInt(el.attr("data-min")) > 0) ? parseInt(el.attr("data-min")) : 0;
    var maxrange = (parseInt(el.attr("data-max")) > 0) ? parseInt(el.attr("data-max")) : 300000;
    var dir = (parseInt($(".showRagePrice").attr("data-rtl")) == 1) ? 'rtl' : 'ltr';
    var stepsSlider = document.getElementById('steps-slider');
    var input0 = document.getElementById('input-with-keypress-0');
    var input1 = document.getElementById('input-with-keypress-1');
    var inputs = [input0, input1];
    noUiSlider.create(stepsSlider, {
        // tooltips: true,
        start: [startrange, endrange],
        step: steprange,
        connect: true,
        direction: dir,
        range: {
            'min': minrange,
            'max': maxrange
        }
    });

    stepsSlider.noUiSlider.on('update', function (values, handle) {
        console.log(values[handle].split(".")[0])
        //inputs[handle].value = values[handle].split(".")[0];
        separateNum(values[handle].split(".")[0], inputs[handle])
        myValues = values;

    });
    function separateNum(value, input) {
        /* seprate number input 3 number */
        var nStr = value + '';
        nStr = nStr.replace(/\,/g, "");
        x = nStr.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        if (input !== undefined) {

            input.value = x1 + x2;
        } else {
            return x1 + x2;
        }
    }
}
if ($("input.Repetitiouscheck").length) {
    $("input.Repetitiouscheck").click(function () {
        if ($(this).is(':checked')) {
            $(".RepetitiousProduct").addClass("show")
        } else {
            $(".RepetitiousProduct").removeClass("show")
        }
    })
}
if ($(".searchFilterBrand").length) {
    $(".searchFilterBrand").keyup(function (e) {
        var txt = $(this).val().toLowerCase();
        $(this).parent().children(".BoxCheck.resultSearchCheckbox").children(".checkboxs").css({ 'display': "none" });
        // $(".BoxCheck.resultSearchCheckbox .checkboxs").css({'display':"none"});
        $(this).parent().children(".BoxCheck.resultSearchCheckbox").children("h4.label").children("small")
        // $(".BoxCheck.resultSearchCheckbox h4.label small").each(function (i) {
        $(this).parent().children(".BoxCheck.resultSearchCheckbox").children(".checkboxs").children(".lblinput").each(function (i) {
            if (txt.length > 0) {
                if ($(this).html().toLowerCase().search(txt) >= 0) {
                    $(this).parent().attr("data", "ok")
                    $(this).parent().css({ 'display': "flex" });
                }
            } else {
                $(this).parent().css({ 'display': "flex" })


            }
        });
    })
}

$(window).on("scroll", function () {
    if ($(this).scrollTop() > 20) {
        if (!$("body").hasClass("news")) {
            if ($(window).width() > 980) {
                $("body").addClass("scrollDown");
                if ($(".contentTabsDP").length) {
                    eloffsetTop = $(".contentTabsDP").offset().top;
                }
                if (scrolTop == 0) {
                    setTimeout(function () {
                        window.scrollTo({
                            top: 50,
                            // left: 100,
                            // behavior: 'smooth'
                        });
                    }, 200)
                    scrolTop++;
                }
            }
        }
    } else {
        $("body").removeClass("scrollDown");
        scrolTop = 0;
    }

    if ($(window).width() > 980) {
        if ($(".tabDetailProduct").length) {
            if ($(this).scrollTop() >= eloffsetTop - 150) {
                $(".tabDetailProduct").addClass("fixed");
            } else {
                $(".tabDetailProduct").removeClass("fixed");
            }
        }
    }
});



if ($(".alertNav i.remove").length) {
    $(".alertNav i.remove").click(function () {
        $(this).parent().addClass("hide");
        $("body").addClass("noalert");
        setTimeout(function () {
            $(".alertNav").remove();
        }, 500)
    });
}
if ($(".menuTopSlider").length) {
$(".menuTopSlider").each(function(i){
    var cnt = $(this).children(".swiper-wrapper").children('.swiper-slide').length;
    var elm = Math.round((cnt-1)/2);
    
    $(this).addClass(`sliderTopmenu${i}`);
    $(this).attr('data-count',elm)


    var sliderSix = new Swiper(`.sliderTopmenu${i}`, {
       // autoHeight: true, //enable auto height
        slidesPerView: 13,
        spaceBetween: 0,
        slidesPerGroup: 1,
        initialSlide: elm,
        centeredSlides: true,
        roundLengths: true,
        loop: false,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        breakpoints: {
            0: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            360: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            480: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            680: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            890: {
                slidesPerView: 7,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 9,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1100: {
                slidesPerView: 11,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1200: {
                slidesPerView: 13,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1360: {
                slidesPerView: 13,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1500: {
                slidesPerView: 13,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            2500: {
                slidesPerView: 15,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
        },
    });
})
}
if ($(".SliderSurprising").length) {
    var sliderSix = new Swiper('.SliderSurprising', {
        autoHeight: true, //enable auto height
        slidesPerView: 6,
        spaceBetween: 20,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
            680: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            890: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1300: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1800: {
                slidesPerView: 6,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            }
        }
    });
}
if ($(".SlidPromotion3").length) {
    var SlidPromotion3 = new Swiper('.SlidPromotion3', {
        autoHeight: true, //enable auto height
        slidesPerView: 6,
        spaceBetween: 20,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
            680: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            890: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1300: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1800: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            }
        }
    });
}
if ($(".SliderAllDayDiscount").length) {
    var SliderAllDayDiscount = new Swiper('.SliderAllDayDiscount', {
        autoHeight: true, //enable auto height
        slidesPerView: 6,
        spaceBetween: 20,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
            680: {
                slidesPerView: 1,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            890: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1200: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1800: {
                slidesPerView: 6,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            }
        }
    });
}
if ($(".SlidPromotion6").length) {
    var SlidPromotion6 = new Swiper('.SlidPromotion6', {
        autoHeight: true, //enable auto height
        slidesPerView: 6,
        spaceBetween: 20,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            980: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1100: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1200: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1800: {
                slidesPerView: 8,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            }
        }
    });
}



if ($(".BrandFooter").length) {
    var BrandFooter = new Swiper('.BrandFooter', {
        autoHeight: true, //enable auto height
        slidesPerView: 4,
        // spaceBetween: 30,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
        }
    });
}
if ($(".slider5").length) {
    var slider5 = new Swiper('.slider5', {
        autoHeight: true, //enable auto height
        slidesPerView: 4,
        // spaceBetween: 30,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
            1200: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
        }
    });
}
if ($(".SliderOneNews").length) {
    var SliderOneNews = new Swiper('.SliderOneNews', {
        autoHeight: true, //enable auto height
        slidesPerView: 1,
        // spaceBetween: 30,
        slidesPerGroup: 1,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        autoplay: {
            delay: 7000,
        }
    });
}
if ($(".SliderOneVideo").length) {
    var SliderOneVideo = new Swiper('.SliderOneVideo', {
        autoHeight: true, //enable auto height
        slidesPerView: 1,
        // spaceBetween: 30,
        slidesPerGroup: 1, simulateTouch: false,
        // loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        autoplay: {
            delay: 7000,
        }
    });
    SliderOneVideo.on('slideChangeTransitionStart', function () {

    });
}

$(".scroll").mCustomScrollbar({
    axis: "y",
    theme: "dark-3",
    scrollButtons: {
        enable: true
    },
});

if ($(".SliderOne").length) {
    var sliderSix = new Swiper('.SliderOne', {
        autoHeight: true, //enable auto height
        slidesPerView: 1,
        spaceBetween: 10,
        slidesPerGroup: 1,
        loop: true,
        effect: "cube",
        cube: {
            slideShadows: true,
            shadow: true,
            shadowOffset: 50,
            shadowScale: 5
        },
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
        }
    });
}
if ($(".SliderSingle").length) {
    var sliderSix = new Swiper('.SliderSingle', {
        autoHeight: true, //enable auto height
        slidesPerView: 1,
        spaceBetween: 10,
        slidesPerGroup: 1,
        loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
            clickable: true,
        },
        navigation: {
            nextEl: '.swiper-button-next',
            prevEl: '.swiper-button-prev',
        },
        autoplay: {
            delay: 7000,
        }
    });
}

var autoplaySliderProgress = 7000;
if ($(".SliderOneProgress").length) {
    var sliderSix = new Swiper('.SliderOneProgress', {
        autoHeight: true, //enable auto height
        slidesPerView: 1,
        spaceBetween: 0,
        slidesPerGroup: 1,
        pagination: false, simulateTouch: false, allowSwipeToNext: false, allowSwipeToPrev: false,
        watchSlidesProgress: true,
        on: {
            progress: progressSlider
        },
        loop: true,
        autoplay: {
            delay: autoplaySliderProgress,
        }
    });
}
function progressSlider() {
    var elem = document.getElementById("progressslider");
    var width = 1;
    var autoplayTime = autoplaySliderProgress / 100;
    var id = setInterval(frame, autoplayTime);
    function frame() {
        if (width >= 100) {
            width = 1;
            clearInterval(id);
        } else {
            width++;
            elem.style.width = width + '%';
        }
    }
}
if ($(".itemCatNav.level1[mega-cat]").length) {
    $(".itemCatNav.level1").hover(
        function () {
            var mcat = $(this).attr("mega-cat");
            // alert(mcat)
            $(`.MegaMenuContent`).each(function (i) {
                if ($(this).attr("mega-cat") == mcat) {
                    $(this).addClass("show")
                } else {
                    $(this).removeClass("show")
                }
            })
            $(`.MegaMenuContent[mega-cat=${mcat}]`).addClass('show');
        }
    );
    $(".contentMain , footer , .MenuTopNav ,.NavContent ,.BestServicesNav,.alertNav,.contentPanel").mouseover(function () {
        $(`.MegaMenuContent`).removeClass("show");
    });
}



if ($(".Timer").length) {
    $(".Timer").each(function (i) {
        TimerReverce($(this));
    });
}

function TimerReverce(element) {
    // Set the date we're counting down to
    var time1 = element.attr("data");
    var countDownDate = new Date(time1).getTime();

    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get today's date and time
        var now = new Date().getTime();

        // Find the distance between now and the count down date
        var distance = countDownDate - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        var h, m, s;
        if (days.toString().length < 2) d = `0` + days.toString(); else d = days.toString();
        if (hours.toString().length < 2) h = `0` + hours.toString(); else h = hours.toString();
        if (minutes.toString().length < 2) m = `0` + minutes.toString(); else m = minutes.toString();
        if (seconds.toString().length < 2) s = `0` + seconds.toString(); else s = seconds.toString();
        // Output the result in an element with id="demo"
        element.html("<span class='day'>" + d + "</span><span class='hours'>" + h + "</span>:<span class='minute'>" + m + "</span>:<span class='secound'>" + s + "</span>");


        // If the count down is over, write some text
        if (distance < 0) {
            clearInterval(x);
            element.html("Ù¾Ø§ÛŒØ§Ù† ÛŒØ§ÙØª");
        }
    }, 1000);
}


if ($(".accItem").length) {
    $(".accItem h3.titleAccItem").click(function () {
        if ($(this).parent().hasClass("open")) {
            $(this).parent().removeClass("open")
        } else {
            // $(".accItem").removeClass("open")
            $(this).parent().addClass("open")
        }
    });
}

if ($("button.btnTabDP").length) {
    $("button.btnTabDP").click(function () {
        var eltab = $(this).attr("data-tabdp");
        $("button.btnTabDP").removeClass('active');
        $(`section.contentTabDP`).removeClass("active");
        $(this).addClass("active");
        $(`section.contentTabDP[data-tabdp="${eltab}"]`).addClass("active");
        setTimeout(function () {

            var contentTop = $(`section.contentTabDP[data-tabdp="${eltab}"]`).offset().top;
            window.scrollTo({
                top: contentTop - 140,
                left: 0,
                behavior: 'smooth'
            });
        }, 500)
    });
}

//if ($("span.btnfunc.like").length) {
//    $("span.btnfunc.like").click(function () {
//        if ($(this).hasClass("active")) {
//            $(this).removeClass("active")
//        } else {
//            $(this).addClass("active")
//            hrb_notify([
//                'danger',
//                picProd,
//                'fa-heart',
//                "Ãƒâ„¢Ã¢â‚¬Â¦ÃƒËœÃ‚Â­ÃƒËœÃ‚ÂµÃƒâ„¢Ã‹â€ Ãƒâ„¢Ã¢â‚¬Å¾ Ãƒâ„¢Ã¢â‚¬Â¦Ãƒâ„¢Ã‹â€ ÃƒËœÃ‚Â±ÃƒËœÃ‚Â¯ Ãƒâ„¢Ã¢â‚¬ ÃƒËœÃ‚Â¸ÃƒËœÃ‚Â± ÃƒËœÃ‚Â¨ÃƒËœÃ‚Â§ Ãƒâ„¢Ã¢â‚¬Â¦Ãƒâ„¢Ã‹â€ Ãƒâ„¢Ã‚ÂÃƒâ„¢Ã¢â‚¬Å¡Ãƒâ€ºÃ…â€™ÃƒËœÃ‚Âª ÃƒËœÃ‚Â¨Ãƒâ„¢Ã¢â‚¬Â¡ Ãƒâ„¢Ã¢â‚¬Å¾Ãƒâ€ºÃ…â€™ÃƒËœÃ‚Â³ÃƒËœÃ‚Âª ÃƒËœÃ‚Â¹Ãƒâ„¢Ã¢â‚¬Å¾ÃƒËœÃ‚Â§Ãƒâ„¢Ã¢â‚¬Å¡Ãƒâ„¢Ã¢â‚¬Â¦Ãƒâ„¢Ã¢â‚¬ ÃƒËœÃ‚Â¯Ãƒâ€ºÃ…â€™ Ãƒâ„¢Ã¢â‚¬Â¡ÃƒËœÃ‚Â§ ÃƒËœÃ‚Â§ÃƒËœÃ‚Â¶ÃƒËœÃ‚Â§Ãƒâ„¢Ã‚ÂÃƒâ„¢Ã¢â‚¬Â¡ ÃƒËœÃ‚Â´ÃƒËœÃ‚Â¯",
//                'bottomLeft',
//                'flipInY',
//                'flipOutX',
//                '5'
//            ]);
//        }
//    })
//}
if ($(".itemModal[data-modalName]")) {
    $(".itemModal").click(function () {
        var modalName = $(this).attr('data-modalName');
        $(".modal").removeClass("active");
        $(`.modal[data-modalName="${modalName}"]`).addClass("active")
    });
}

function closeModal(closeVideo = false) {
    $(".modal").removeClass("active");
    if (closeVideo) {
        $("#aparatVideo").remove();
    }
    if ($("#myimage1").length || $(".img-zoom-result").length) {
        zoomRemove();
    }
}




if ($("span.itemPriceRight.no").length) {
    $("span.itemPriceRight.no").click(function () {
        $(this).parent().addClass("hide");
    })
}
function copyLink() {
    var copyText = document.querySelector(".tempUrl");
    copyText.focus();
    copyText.select();
   // copyText.setSelectionRange(0, 99999);
    var state = document.execCommand('copy');
    hrb_notify([
        'success',
        picProd,
        'fa-copy',
        "لینک محصول با موفقیت کپی شد",
        'bottomLeft',
        'flipInY',
        'flipOutX',
        '5'
    ]);
}

if ($('.copyPathUrl').length) {

    $('.copyPathUrl').on('click', function () {
        
    });
}

//if ($("button.addToCardDetail").length) {
//    $("button.addToCardDetail").click(function () {
//        hrb_notify([
//            'homekito',
//            picProd,
//            'fa-basket',
//            "Ù…Ø­ØµÙˆÙ„ Ù…ÙˆØ±Ø¯ Ù†Ø¸Ø± Ø¨Ø§ Ù…ÙˆÙÙ‚ÛŒØª Ø¨Ù‡ Ø³Ø¨Ø¯ Ø®Ø±ÛŒØ¯ Ø§Ø¶Ø§ÙÙ‡ Ø´Ø¯",
//            'bottomLeft',
//            'flipInY',
//            'flipOutX',
//            '5'
//        ]);
//    })
//}

function addToCompare(t) {
    if ($(t).hasClass("okAdd")) {
        hrb_notify([
            'warning',
            picProd,
            'fa-exchange',
            "Ø§ÛŒÙ† Ù…Ø­ØµÙˆÙ„ Ù‚Ø¨Ù„Ø§ Ø¨Ù‡ Ù„ÛŒØ³Øª Ù…Ù‚Ø§ÛŒØ³Ù‡ Ø§Ø¶Ø§ÙÙ‡ Ø´Ø¯Ù‡ Ø§Ø³Øª",
            'bottomLeft',
            'flipInY',
            'flipOutX',
            '5'
        ]);
    } else {
        hrb_notify([
            'info',
            picProd,
            'fa-exchange',
            "Ù…Ø­ØµÙˆÙ„ Ù…ÙˆØ±Ø¯ Ù†Ø¸Ø± Ø¨Ø§ Ù…ÙˆÙÙ‚ÛŒØª Ø¨Ù‡ Ù„ÛŒØ³Øª Ù…Ù‚Ø§ÛŒØ³Ù‡ Ø§Ø¶Ø§ÙÙ‡ Ø´Ø¯",
            'bottomLeft',
            'flipInY',
            'flipOutX',
            '5'
        ]);
        $(t).addClass("okAdd")
    }

}



if ($(".getAddressShop").length) {
    $(".getAddressShop").on("change", function () {
        getAddressShop($(this))
    })
}
getAddressShop($(".getAddressShop"))

function getAddressShop(t) {
    if (t.is(':checked')) {
        $(".formContent").css({ "display": "none" })
        $(".formContent.c1").css({ "display": "block" })
    } else {
        $(".formContent").css({ "display": "none" })
        $(".formContent.c2").css({ "display": "block" })
    }
}




function removeAllFilter() {
    showLoading()
    $(`.BoxesFilterSelected .itemSelectedFilter`).remove()
    $(".FilterBox input[type=checkbox] , .FilterBox input[type=radio]").each(function () {
        $(this).prop("checked", false);
    });
    stepsSlider.noUiSlider.set([startrange, endrange]);
    var to = window.setTimeout(function () { FilterSearch(); window.clearTimeout(to); }, 1000);
}
function removeIS(id, $t) {
    showLoading()
    var el1 = $(`#${id}`);


    if (id == "rageSlider") {
        stepsSlider.noUiSlider.set([startrange, endrange]);
    } else {
        if (el1.prop('checked')) {
            el1.click();
        }
    }
    $t.parent().remove();
}


if ($(".FilterBox").length) {
    $('.FilterBox input').click(function () {

        var tid = $(this).attr("id");
        var tname = $(this).attr("name");
        var ttype = $(this).attr("type");
        var ttitle = $(this).parent().children("label").children("span").html();
        if ($(this).attr("type") == 'checkbox') {
            showLoading()
            if ($(this).prop("checked")) {
                $(".BoxesFilterSelected").append(`
                    <div class="itemSelectedFilter" data-id="${tid}" data-name="${tname}" data-type="${ttype}">
                        <span>${ttitle}</span>
                        <i class="remove" onclick="removeIS('${tid}',$(this))"></i>
                    </div>
                `)
            } else {
                $(`.BoxesFilterSelected .itemSelectedFilter[data-id='${tid}']`).remove()
            }
        }
        if ($(this).attr("type") == 'radio') {
            showLoading()
            if ($(this).prop("checked")) {
                $(`.BoxesFilterSelected .itemSelectedFilter`).each(function () {
                    if ($(this).attr("data-name") == tname && $(this).attr("data-type") == ttype) {
                        $(this).remove()
                    }
                })
                $(".BoxesFilterSelected").append(`
                    <div class="itemSelectedFilter" data-id="${tid}" data-name="${tname}" data-type="${ttype}">
                        <span>${ttitle}</span>
                        <i class="remove" onclick="removeIS('${tid}',$(this))"></i>
                    </div>
                `)
            } else {
                $(`.BoxesFilterSelected .itemSelectedFilter[data-id='${tid}']`).remove()
            }
        }


    })
}

function showLoading() {
    $(".loading").addClass("show");
}
function hideLoading() {
    $(".loading").removeClass("show");
}
if ($("button.btnsubmitFillter").length) {
    $("button.btnsubmitFillter").click(function () {
        showLoading()
        $(".BoxesFilterSelected .itemSelectedFilter[data-id=rageSlider]").remove()
        $(".BoxesFilterSelected").append(`
            <div class="itemSelectedFilter" data-id="rageSlider" data-name="rageSlider" data-type="rageSlider">
                <span>Ãƒâ„¢Ã¢â‚¬Å¡Ãƒâ€ºÃ…â€™Ãƒâ„¢Ã¢â‚¬Â¦ÃƒËœÃ‚Âª ÃƒËœÃ‚Â§ÃƒËœÃ‚Â² : ${myValues[0].split(".")[0]} ÃƒËœÃ‚ÂªÃƒËœÃ‚Â§ : ${myValues[1].split(".")[0]}</span>
                <i class="remove" onclick="removeIS('rageSlider',$(this))"></i>
            </div>
        `)
    });
}
if ($("span.itemSort").length) {
    $("span.itemSort").click(function () {
        showLoading()
        $("span.itemSort").removeClass("active");
        if (!$(this).hasClass("active")) {
            $(this).addClass("active");
        }
    })
}
if ($(".textMore span.more").length) {
    $(".textMore span.more").click(function () {
        if ($(this).parent().hasClass("more")) {
            $(this).parent().removeClass("more");
            $(this).html($(this).attr("data-show"))
        } else {
            $(this).parent().addClass("more");
            $(this).html($(this).attr("data-hide"))
        }
    })
}
function lightBoxPic(e, el) {
    if (!e) e = window.event;
    $(".lightboxPic").remove();
    $("body").append("<div class='lightboxPic'><i class='bglightboxPic'></div>");
    var pic = $(el).attr('data-pic');
    $(".lightboxPic").append("<img src='" + pic + "'/>")
    setTimeout(function () {
        $(".lightboxPic").addClass("show");
    }, 200);
    $(".lightboxPic i").click(function () {
        $(this).parent().removeClass("show");
    });
    e.preventDefault();
};
if ($(".slider5Item").length) {
    var sliderSix = new Swiper('.slider5Item', {
        autoHeight: true, //enable auto height
        slidesPerView: 6,
        // spaceBetween: 30,
        slidesPerGroup: 1,
        //loop: true,
        pagination: {
            el:  $(this).find('.swiper-pagination'),
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
            640: {
                slidesPerView: 2,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            768: {
                slidesPerView: 3,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1000: {
                slidesPerView: 4,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1200: {
                slidesPerView: 5,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
            1500: {
                slidesPerView: 6,
                // spaceBetween: 30,
                slidesPerGroup: 1,
            },
        }
    });
}







if ($(".CompareContent").length) {
    $(".CompareContent").mCustomScrollbar({
        axis: "x",
        theme: "dark-3",
        scrollButtons: {
            enable: true
        },
    });
}

if ($(".resultSearchCompare").length) {
    $(".resultSearchCompare").mCustomScrollbar({
        axis: "y",
        theme: "dark-3",
        scrollButtons: {
            enable: true
        },
    });
}



if ($("button[data-tab]").length) {
    $("button[data-tab]").click(function () {
        var data = $(this).attr("data-tab");
        $("button[data-tab]").removeClass("active");
        $(this).addClass("active");
        $(".tabContent[data-tab]").removeClass("active");
        setTimeout(function () {
            $(".tabContent[data-tab='" + data + "']").addClass("active");
            var elTop = parseInt($(".tabContent").offset().top - 110);
            $("html, body").animate({ scrollTop: elTop }, "500");

            $("button.gotoTabsBtn").click(function () {
                var TabTop = parseInt($(".TabsBoxes .TabButtons").offset().top - 110);
                $("html, body").animate({ scrollTop: TabTop }, "500");
            });
        }, 300)
    });
}
if ($(".accotdionFAQ").length) {
    $(".accotdionFAQ span.AnswerQuestion").click(function () {
        if ($(this).parent().parent().parent().hasClass('active')) {
            $(this).parent().parent().parent().removeClass('active')
        } else {
            $(this).parent().parent().parent().addClass('active')
        }
    });
}



if ($(".js-map").length) {
    !function () {
        !function () {
            var t = document.getElementsByTagName("head")[0],
                e = document.createElement("link");
            e.rel = "stylesheet", e.type = "text/css", e.href = "https://api.mapbox.com/mapbox-gl-js/v0.49.0/mapbox-gl.css", e.media = "all", t.appendChild(e)
        }();
        !function (t, e) {
            var n = document.createElement("script");
            n.onload = function () {
                e()
            }, document.body.appendChild(n), n.src = t
        }("https://api.mapbox.com/mapbox-gl-js/v0.49.0/mapbox-gl.js", function () {
            mapboxgl.accessToken = "pk.eyJ1IjoicG9veWExMjEiLCJhIjoiY2ptNDZ3bmd4MGo2czN2cnI5bDdjbnQwYiJ9.2HrxZBck4tffmbfFn7YW4w", mapboxgl.setRTLTextPlugin("https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-rtl-text/v0.2.0/mapbox-gl-rtl-text.js");
            for (var t = document.querySelectorAll(".js-map"), e = 0; e < t.length; e++) {
                var n = t[e],
                    i = [n.dataset.lat, n.dataset.lng],
                    r = n.dataset.zoom,
                    o = new mapboxgl.Map({
                        container: n,
                        style: 'mapbox://styles/mapbox/light-v10',
                        center: i,
                        zoom: r
                    });
                o.addControl(new mapboxgl.NavigationControl), o.addControl(new mapboxgl.FullscreenControl), (new mapboxgl.Marker).setLngLat(i).addTo(o);
                o.scrollZoom.disable();
            }

        })
    }();
}

if ($(".itemVideoSlide .pic").length) {
    $(".itemVideoSlide .pic").click(function () {
        var aparat = $(this).data("aparat");
        var htmlaparat = `<div class="h_iframe-aparat_embed_frame" id="aparatVideo"><span style="display: block;padding-top: 57%"></span><iframe src="https://www.aparat.com/video/video/embed/videohash/${aparat}/vt/frame" allowFullScreen="true" webkitallowfullscreen="true" mozallowfullscreen="true"></iframe></div>`
        $(".showVideoAparat").html("");
        $(".showVideoAparat").html(htmlaparat);
    })
}

function closeSPopup() {
    $(".showLightBoxSlider").removeClass("active");
};


if ($("i.openparent").length) {
    $("i.openparent , i.openchild , i.openchildchild").click(function () {
        if ($(this).parent().hasClass("open")) {
            $(this).parent().removeClass("open")
        }
        else {
            $(this).parent().addClass("open")
        }
    });
}


if ($(".lightBoxADS").length) {
    function showLight() {
        var img = $(".lightBoxADS").data('img');
        var link = $(".lightBoxADS").data('link');
        $(".lightBoxADS").html(`
        <i class="bg" onclick="closeLight()"></i>
        <div class="ContentlightBoxADS">
        <i class="close fal fa-times" onclick="closeLight()"></i>
        <a href="${link}" target="_blank">
            <img src="${img}" alt="">
        </a>
        </div>
    `);
        setTimeout(function () {
            $(".lightBoxADS").addClass("show");
        }, 1000)
    }
    showLight()
    function closeLight() {
        $(".lightBoxADS").removeClass("show");
    }
}

function hideMenuTop() {
    if ($(window).width() <= 890) {
        if($("body").hasClass("news")){
            // $('.sidebar .contentSidebar').html('');
            // $('.sidebar .ContentSideBar').prepend($("nav.navbar .Menu:nth-child(3)").html());
            // $('.sidebar .ContentSideBar').prepend($("nav.navbar .Menu:nth-child(1)").html());
            // $('.sidebar .ContentSideBar').prepend($(".BestServicesNav .container").html());
            // $('.sidebar .ContentSideBar a').removeAttr("class").removeAttr("style");
        }else{
            // $(".contentMenuTop .navLink").addClass('hide');
            // $('.sidebar .contentSidebar').html('');
            // $('.sidebar .ContentSideBar').prepend($(".MenuTopNav .container .MenuTop").html());
            // $('.sidebar .ContentSideBar').prepend($(".BestServicesNav .container").html());
            // $('.sidebar .ContentSideBar a').removeAttr("class").removeAttr("style");
        }
        
    } else {
        // $('.sidebar .contentSidebar').html('');
        // $(".contentMenuTop .navLink").removeClass("hide")
    }
}
hideMenuTop()
$(window).resize(function () {
    hideMenuTop()
})

$(".showSidebar").click(function () {
    if ($(this).hasClass('open')) {
        $("body").removeClass("lock")
        $(this).removeClass('open');
        $(".sidebar").removeClass('show');
        $("i.bgsidebar").removeClass('show');
    } else {
        $("body").addClass("lock")
        $(this).addClass('open');
        $(".sidebar").addClass('show');
        $("i.bgsidebar").addClass('show');
    }
})

$("i.bgsidebar").click(function () {
    $("body").removeClass("lock")
    $(".showSidebar").removeClass('open');
    $(".sidebar").removeClass('show');
    $("i.bgsidebar").removeClass('show');
})












// --------------------- | New Js Add | --------------------- \\

if ($("button.btnFilterMobile").length) {
    $("button.btnFilterMobile").click(function () {
        $("body").addClass("overHidden")
        $(".ContentListProducts .col1").addClass("fixed");
    })
}
if ($("button.closeFilterMobile").length) {
    $("button.closeFilterMobile").click(function () {
        $("body").removeClass("overHidden")
        $(".ContentListProducts .col1").removeClass("fixed");
    })
}



// --------------------- | New Js Add | --------------------- \\