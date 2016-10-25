var AddPostUrl = '/Post/AddPost/';
var GetPostsUrl = '/UserProfile/TimelinePosts/';
var IndexUrl = '/Home/Index/';

$(document).ready(function () {
    $('#btnAddPost').on('click', function () {

        var content = $('#txtPostContent').val();
        content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var postData = {
            content: content,
            user: $('#username').val()
        }

        $.ajax({
            url: AddPostUrl,
            data: postData,
            success: function (data) {
                PostSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function PostSuccess(data) {
            $('#postValidation').text(' ');
            if ($.trim(postData.content).length === 0) {
                $('#postValidation').text('You haven\'t written anything!');
            }
            else if (data.Status == false) {
                $('#postValidation').text('Post unsuccessful.');
            }
            else {
                $('#postValidation').text('Post successful!');
                $('#userPosts').load(GetPostsUrl, { username: $('#username').val() });
                setTimeout(ClearText, 1000);
            }
        }

        function ClearText() {
            $('#postValidation').text('');
            $('#txtPostContent').val('')
        }
    });
});