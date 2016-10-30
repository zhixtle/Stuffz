$(document).ready(function () {
    $(document).on('click', '.btnNotif', function () {

        var notifData = {
            notifID : this.id
        }

        $.ajax({
            url: SeeNotifUrl,
            data: notifData
        });
    });
});