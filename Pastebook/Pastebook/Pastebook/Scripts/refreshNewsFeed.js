var GetPostsUrl = '/Home/NewsFeedPosts/';

$(document).ready(function ()
{
    function refreshFeed() {
        $('#userPosts').load(GetPostsUrl);
    }

    refreshFeed();
    setInterval(function () {
        refreshFeed();
    }, 60000);
});