var SaveAboutMeUrl = '/UserProfile/SaveAboutMe/';

$(document).ready(function () {
    $('#btnSaveAboutMe').on('click', function () {

        var content = $('#textAboutMe').val();
        content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var aboutMeData = {
            username: $('#username').val(),
            aboutMeContent: content
        }

        $.ajax({
            url: SaveAboutMeUrl,
            data: aboutMeData,
            success: function (data) {
                SaveAboutMeSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function SaveAboutMeSuccess(data) {
            if (data.Status == true) {
                location.reload();
            }
        }
    });
});