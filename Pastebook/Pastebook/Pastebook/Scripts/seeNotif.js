var SeeNotifUrl = '/Home/SeeNotification/';

$(document).ready(function () {
    $(document).on('click', '.btnNotif', function () {

        var notifData = {
            notifID : this.id
        }

        $.ajax({
            url: SeeNotifUrl,
            data: notifData,
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