var NotifsUrl = '/Home/NotificationsButton/';

$(document).ready(function () {
    function refreshFeed() {
        $('#menu-notifs').load(NotifsUrl);
    }

    refreshFeed();
    setInterval(function () {
        refreshFeed();
    }, 60000);
});