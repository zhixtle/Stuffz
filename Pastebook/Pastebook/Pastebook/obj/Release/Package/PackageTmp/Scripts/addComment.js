var AddCommentUrl = '/Post/AddComment/';
var GetPostsUrl = '/Home/NewsFeedPosts/';
var IndexUrl = '/Home/Index/';

$(document).ready(function () {
    $('.btnComment').on('click', function () {

        var postContent = $(this).closest('.comment-add').find('.comment-add-content');
        var content = postContent.val();
        content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var commentData = {
            postID: this.id,
            content: content
        }

        $.ajax({
            url: AddCommentUrl,
            data: commentData,
            success: function (data) {
                if ($.trim(commentData.content).length == 0) {
                    $(postContent).val('');
                    $(postContent).attr('placeholder', 'You haven\'t written anything!');
                }
                else {
                    location.reload();
                }
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });
    });
});