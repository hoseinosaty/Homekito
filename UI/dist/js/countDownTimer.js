


function hrb_timer() {
    if ($(".hrb_timer").length) {
        var timer2 = $(".hrb_timer").data("time");
        $(".hrb_timer").html(timer2);
        var timer3 = 0;
        var interval = setInterval(function () {


            var timer = timer2.split(':');
            //by parsing integer, I avoid all extra string processing
            var minutes = parseInt(timer[0], 10);
            var seconds = parseInt(timer[1], 10);
            --seconds;
            minutes = (seconds < 0) ? --minutes : minutes;
            if (minutes < 0) clearInterval(interval);
            seconds = (seconds < 0) ? 59 : seconds;
            seconds = (seconds < 10) ? '0' + seconds : seconds;
            //minutes = (minutes < 10) ?  minutes : minutes;
            $(".hrb_timer").html(minutes + ':' + seconds);
            timer2 = minutes + ':' + seconds;
            timer3 = minutes + seconds;
            if (timer3 <= 0) {
                clearInterval(interval);
                $('.hrb_timer').html(`<a href="javascript:void(0)" onclick="resendSMS()">ارسال مجدد کد</a>`);
            }
        }, 1000);
    }
}
hrb_timer()

function resendSMS() {
    hrb_timer()
}