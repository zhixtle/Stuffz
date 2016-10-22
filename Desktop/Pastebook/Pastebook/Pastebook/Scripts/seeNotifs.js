var SeeNotifsUrl = '/Home/SeeNotifications/';

$(document).ready(function () {
    $('#notifsBlock').on('click', function () {

        $.ajax({
            url: SeeNotifsUrl,
            success: function (data) {
                SeeSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function SeeSuccess(data) {
            //goodness, please change this after you fix everything else
        }
    });
});