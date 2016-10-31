$(document).ready(function () {

    if ($('#textAboutMe').length) {
        var charCount = 2000 - $('#textAboutMe').val().length;
        $('#aboutMeCount').text(charCount);
    }

    $('#textAboutMe').keyup(function () {
        charCount = 2000 - $('#textAboutMe').val().length;
        $('#aboutMeCount').text(charCount);
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

    if ($('#aboutMeStatusGood').length && $('#aboutMeStatusBad').length) {
        $('#aboutMeStatusGood').hide();
        $('#aboutMeStatusBad').hide();
    }

    $('#btnSaveAboutMe').on('click', function () {

        var content = $('#textAboutMe').val();
        content = content.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var aboutMeData = {
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
                $('#aboutMeStatusGood').show();
                $('#aboutMeStatusBad').hide();
                setTimeout(RefreshPage, 1000);
            }
            else {
                $('#aboutMeStatusGood').hide();
                $('#aboutMeStatusBad').show();
            }
        }

        function RefreshPage() {
            location.reload();
        }
    });

    $('#file').change(function () {
        var ext = $('#file').val().split('.').pop().toLowerCase();
        var size = $('#file').prop('files')[0].size;
        if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
            $('#pictureExtStatus').text('Invalid file. Only GIF, PNG, JPG, and JPEG formats are allowed.');
            $('#btnSavePicture').attr('disabled', true);
        } else {
            $('#pictureExtStatus').text('');
            $('#btnSavePicture').attr('disabled', false);
        }
        if (size > 4194304) {
            $('#pictureSizeStatus').text('Invalid file. Maximum file size is 4MB.');
            $('#btnSavePicture').attr('disabled', true);
        } else {
            $('#pictureSizeStatus').text('');
            $('#btnSavePicture').attr('disabled', false);
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

});