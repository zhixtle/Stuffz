$(document).ready(function () {
    
    $('#btnAddPost').on('click', function () {

        var content = $('#txtPostContent').val();
        content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var postData = {
            content: content,
            user: username
        }

        $.ajax({
            url: AddPostUrl,
            data: postData,
            success: function (data) {
                PostSuccess(data)
            }
        });

        function PostSuccess(data) {
            $('#postValidation').text(' ');
            if ($.trim(postData.content).length === 0) {
                $('#postValidation').text('You haven\'t written anything!');
            }
            else if (data.Status == false) {
                $('#postValidationBad').text('Post unsuccessful.');
            }
            else {
                $('#postValidationGood').text('Post successful!');
                $('#userPosts').load(GetPostsUrl, { username: username });
                setTimeout(ClearText, 1000);
            }
        }

        function ClearText() {
            $('#postValidationGood').text('');
            $('#postValidationBad').text('');
            $('#postValidation').text('');
            $('#txtPostContent').val('');
        }
    });

    $(document).on('click', '.btnComment', function () {

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
                    $('#userPosts').load(GetPostsUrl, { username: username });
                }
            }
        });
    });

        $('.comment-add-content').focusout(function () {
        if ($('.comment-add-content').val().length > 1000) {
            $(this).val('');
            $(this).attr('placeholder', 'Can\'t comment anything longer than 1000 characters!');
        }
    });

    $(document).on('click', '.btnLike', function () {

        var likeData = {
            postID: this.id
        }

        $.ajax({
            url: AddLikeUrl,
            data: likeData,
            success: function (data) {
                LikeSuccess(data)
            }
        });

        function LikeSuccess(data) {
            $('#userPosts').load(GetPostsUrl, { username: username });
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
            }
        });

        function UnlikeSuccess(data) {
            $('#userPosts').load(GetPostsUrl, { username: username });
        }
    });

});