var AddCommentUrl = '/Post/AddComment/';
var GetPostsUrl = '/Home/NewsFeedPosts/';
var IndexUrl = '/Home/Index/';

$(document).ready(function () {
    $('.btnComment').on('click', function () {

        var commentData = {
            postID: this.id,
            content: $(this).closest('.comment-add').find('.comment-add-content').val()
        }

        $.ajax({
            url: AddCommentUrl,
            data: commentData,
            success: function (data) {
                CommentSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function CommentSuccess(data) {
            //goodness, please change this after you fix everything else
            location.reload();
        }
    });
});