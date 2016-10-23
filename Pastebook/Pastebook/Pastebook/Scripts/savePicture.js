var SavePictureUrl = '/UserProfile/SaveProfilePicture/';

//http://stackoverflow.com/questions/166221/how-can-i-upload-files-asynchronously

$(document).ready(function () {
    $('#btnSavePicture').on('click', function () {
        alert('HERE IN CLICK');

        var picData = new FormData($('#pictureInput'));

        alert('AGAIN');
        $.ajax({
            url: SavePictureUrl,
            type: 'POST',
            data: picData,
            success: function (data) {
                alert('HERE');
                SavePictureSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            },
            cache: false,
            contentType: false,
            processData: false
        });

        function SavePictureSuccess(data) {
            //goodness, please change this after you fix everything else
            if (data.Status == true) {
                location.reload();
            }
        }
    });
});