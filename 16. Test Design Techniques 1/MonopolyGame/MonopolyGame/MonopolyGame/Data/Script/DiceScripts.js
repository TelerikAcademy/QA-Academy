function msieversion() {

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
        // If Internet Explorer, return version number
        return true;
        //return parseInt(ua.substring(msie + 5, ua.indexOf(".", msie)));
    }


    return false;
}
$(function () {
    var showNotification = 4000;
    if (msieversion())
        showNotification = 0;
    $('.roll').on('click', function () {
        var result = Math.round(Math.random() * 5 + 1),
            angle = {};
        $(this).data('n', $(this).data('n') ? 0 : 5);
        var n = $(this).data('n');
        $('.cube').attr('style', '');
        angle = { x: 360 * n, y: 360 * n }
        switch (result) {
            case 1:
                break;
            case 2:
                angle.y = 360 * n + 90;
                break;
            case 3:
                angle.x = 360 * n - 90;
                break;
            case 4:
                angle.x = 360 * n + 90;
                break;
            case 5:
                angle.y = 360 * n - 90;
                break;
            case 6:
                angle.x = 360 * n + 180;
                break;
        }
        var showNotification;
        if (msieversion()) {
            $('.side').hide();
            $('.cube, .side').css({
                '-ms-transform': 'translateZ(0px) rotateX(0deg) rotateY(0deg)',
                '-ms-transition': '0s',
                'transform': 'translateZ(0px) rotateX(0deg) rotateY(0deg)',
                'transition': '0s'
            });
            switch (result) {
                case 1:
                    $('.front').show();
                    break;
                case 2:
                    $('.right').show();
                    break;
                case 3:
                    $('.top').show();
                    break;
                case 4:
                    $('.left').show();
                    break;
                case 5:
                    $('.bottom').show();
                    break;
                case 6:
                    $('.back').show();
                    break;
            }
        } else {
            $('.cube').css({ '-webkit-transform': 'translateZ(-100px) rotateX(' + angle.x + 'deg) rotateY(' + angle.y + 'deg)', '-webkit-transition': '3s',
                'transform': 'translateZ(-100px) rotateX(' + angle.x + 'deg) rotateY(' + angle.y + 'deg)', 'transition': '3s',
                '-ms-transform': 'translateZ(-100px) rotateX(' + angle.x + 'deg) rotateY(' + angle.y + 'deg)', '-ms-transition': '3s'
            });
        }

        if (result == 6) {
            setTimeout(function showNotif() {
                var radNotification1 = $find("StartGame");
                radNotification1.show();
            }, showNotification);
        }
    });
})