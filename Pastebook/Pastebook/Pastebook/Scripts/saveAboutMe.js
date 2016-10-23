var SaveAboutMeUrl = '/UserProfile/SaveAboutMe/';

$(document).ready(function () {
    $('#btnSaveAboutMe').on('click', function () {

        var aboutMeData = {
            username: $('#username').val(),
            aboutMeContent: $('#textAboutMe').val()
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
            //goodness, please change this after you fix everything else
            if (data.Status == true) {
                location.reload();
            }
        }
    });
});