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