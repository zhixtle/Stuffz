var NotifsUrl = '/Home/NotificationsButton/';

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();

    var $popover = $('[data-toggle="popover"]').popover({
        html: true,
        trigger: 'click'
    }).click(function (e) {
        e.preventDefault();
    });;

    $popover.on('hidden.bs.popover', function (e) {
        $('#menu-notifs').load(NotifsUrl);
    });;

    $(document).ajaxComplete(function () {
        $('[data-toggle="tooltip"]').tooltip();

        var $popover = $('[data-toggle="popover"]').popover({
            html: true,
            trigger: 'click'
        }).click(function (e) {
            e.preventDefault();
        });;

        $popover.on('hidden.bs.popover', function (e) {
            $('#menu-notifs').load(NotifsUrl);
        });
    });
});