$(document).ready(function () {
    
    if ($(".boxGall").length) {
        
        $("body").append(`
                            <div class="showLightBoxSlider">
                                <i class="bg" onclick="closeSPopup()"></i>
                                <i class="prevBtn fas fa-chevron-left"></i>
                                <i class="nextBtn fas fa-chevron-right"></i>
                            </div>
                        `);
        $(".boxGall").each(function (j) {
            $(this).attr("dataNum", (j));
        });
        $(".boxGall label").click(function () {
            $(".showLightBoxSlider").html('<i class="bg" onclick="closeSPopup()"></i><i class="prevBtn fas fa-chevron-left"></i><i class="nextBtn fas fa-chevron-right"></i><i class="fas fa-window-close" onclick="closeSPopup()"></i>');
            var slidePic = $(this).parent().parent().attr("dataLarg");
            var titlePic = $(this).parent().parent().attr("alt");
            var dataNum = $(this).parent().parent().attr("dataNum");
            var countSlide = $(".boxGall").length;
            var modCenter = 0;
            $(".boxGall").each(function (i) {
                var slidePic1 = $(this).attr("dataLarg");
                var titlePic1 = $(this).attr("alt");
                var dataNum1 = $(this).attr("dataNum");
                if ((countSlide % 2) == 0) modCenter = i - 1; else modCenter = i;
                if (i < dataNum) {
                    $('.showLightBoxSlider').append('<img src="' + slidePic1 + '" alt="' + titlePic1 + '" class="slideBox activeCenter activeRight">');
                }
                if (i == dataNum) {
                    $('.showLightBoxSlider').append('<img src="' + slidePic1 + '" alt="' + titlePic1 + '" class="slideBox activeCenter">');
                    $("i.nextBtn.fas.fa-chevron-right , i.prevBtn.fas.fa-chevron-left").attr("dataNum", dataNum);
                }
                if (i > dataNum) {
                    $('.showLightBoxSlider').append('<img src="' + slidePic1 + '" alt="' + titlePic1 + '" class="slideBox activeCenter activeLeft">');
                }
            });
            $(".showLightBoxSlider").addClass("active");
            // $('.showLightBoxSlider.active i.fas.fa-window-close').click(function () {
                
            // });
            
            
            $("i.nextBtn.fas.fa-chevron-right").click(function () {
                var thisEN = $(this).attr("dataNum");
                $('.showLightBoxSlider .slideBox').each(function (k) {
                    if (!$(this).hasClass("activeLeft") && !$(this).hasClass("activeRight")) {
                        if (thisEN == k) {
                            if (thisEN < (countSlide - 1)) {
                                $(this).addClass("activeRight");
                                $(this).next().removeClass("activeRight").removeClass("activeLeft");
                                $("i.nextBtn.fas.fa-chevron-right , i.prevBtn.fas.fa-chevron-left").attr("dataNum", (k + 1));
                            } else {
                                // alert("End")
                            }
                        }
                    }
                });
            });
            $("i.prevBtn.fas.fa-chevron-left").click(function () {
                var thisEN = $(this).attr("dataNum");
                $('.showLightBoxSlider .slideBox').each(function (k) {
                    if (!$(this).hasClass("activeLeft") && !$(this).hasClass("activeRight")) {
                        if (thisEN == k) {
                            if (thisEN > 0) {
                                $(this).addClass("activeLeft");
                                $(this).prev().removeClass("activeLeft").removeClass("activeRight");
                                $("i.nextBtn.fas.fa-chevron-right , i.prevBtn.fas.fa-chevron-left").attr("dataNum", (k - 1));
                            } else {
                                // alert("End")
                            }
                        }
                    }
                });
            });
        });
        
    }
});