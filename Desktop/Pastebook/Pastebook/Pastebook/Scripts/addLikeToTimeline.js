var AddLikeUrl = '/Post/AddLike/';
var GetPostsUrl = '/UserProfile/TimelinePosts/';
var IndexUrl = '/Home/Index/';

$(document).ready(function () {
    $(document).on('click', '.btnLike', function () {

        var likeData = {
            postID: this.id
        }

        $.ajax({
            url: AddLikeUrl,
            data: likeData,
            success: function (data) {
                LikeSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function LikeSuccess(data) {
            $('#userPosts').load(GetPostsUrl, { username: $('#username').val() });
        }
    });
});