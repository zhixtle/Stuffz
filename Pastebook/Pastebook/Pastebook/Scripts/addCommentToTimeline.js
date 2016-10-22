var AddCommentUrl = '/Post/AddComment/';
var GetPostsUrl = '/UserProfile/TimelinePosts/';
var IndexUrl = '/Home/Index/';

$(document).ready(function () {
    $(document).on('click', '.btnComment', function () {

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
            $('#userPosts').load(GetPostsUrl, { username: $('#username').val() });
        }
    });
});