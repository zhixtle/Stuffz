var AddLikeUrl = '/Post/AddLike/';
var RemoveLikeUrl = '/Post/RemoveLike/';
var GetPostsUrl = '/Home/NewsFeedPosts/';
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
            $('#userPosts').load(GetPostsUrl);
        }
    });

    $(document).on('click', '.btnUnlike', function () {

        var likeData = {
            postID: this.id
        }

        $.ajax({
            url: RemoveLikeUrl,
            data: likeData,
            success: function (data) {
                UnlikeSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function UnlikeSuccess(data) {
            $('#userPosts').load(GetPostsUrl);
        }
    });
});