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
            }
        });

        function SaveAboutMeSuccess(data) {
            if (data.Status == true) {
                $('#aboutMeStatus').text('About me saved successfully.');
                setTimeout(RefreshPage, 1000);
            }
            else {
                $('#aboutMeStatus').text('Unable to save about me.');
            }
        }

        function RefreshPage() {
            location.reload();
        }
    });

    $('#btnSavePicture').on('click', function () {
        var picData = new FormData($('#pictureInput'));

        $.ajax({
            url: SavePictureUrl,
            type: 'POST',
            data: picData,
            cache: false,
            contentType: false,
            processData: false
        });
    });

    $('#textAboutMe').focusout(function () {
        if ($('#textAboutMe').val().length > 2000) {
            $('#aboutMeStatus').text('About me cannot be more than 2000 characters.');
            $('#btnSaveAboutMe').attr('disabled', true);
        }
        else {
            $('#aboutMeStatus').text('');
            $('#btnSaveAboutMe').attr('disabled', false);
        }
    });

    $('#file').change(function () {
        var ext = $('#file').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
            $('#pictureExtStatus').text('Invalid file. Only GIF, PNG, JPG, and JPEG formats are allowed.');
            $('#btnSavePicture').attr('disabled', true);
        } else {
            $('#pictureExtStatus').text('');
            $('#btnSavePicture').attr('disabled', false);
        }
    });

});