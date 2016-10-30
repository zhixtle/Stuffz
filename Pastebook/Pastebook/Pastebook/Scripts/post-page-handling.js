$(document).ready(function () {

    $(document).on('click', '.btnLike', function () {

        var likeData = {
            postID: this.id
        }

        $.ajax({
            url: AddLikeUrl,
            data: likeData,
            success: function (data) {
                LocationReload();
            }
        });


    });

    $(document).on('click', '.btnUnlike', function () {

        var likeData = {
            postID: this.id
        }

        $.ajax({
            url: RemoveLikeUrl,
            data: likeData,
            success: function (data) {
                LocationReload();
            }
        });
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
                    LocationReload();
                }
            }
        });
    });

    function LocationReload() {
        location.reload();
    }

    $('.comment-add-content').focusout(function () {
        if ($('.comment-add-content').val().length > 1000) {
            $(this).val('');
            $(this).attr('placeholder', 'Can\'t comment anything longer than 1000 characters!');
        }
    });

});