var FriendRequestUrl = '/UserProfile/SendFriendRequest/';

$(document).ready(function () {
    $('#btnFriendRequest').on('click', function () {

        var friendRequestData = {
            profileUsername: $('#username').val()
            }

        $.ajax({
            url: FriendRequestUrl,
            data: friendRequestData,
            success: function (data) {
                FriendRequestSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function FriendRequestSuccess(data) {
            //goodness, please change this after you fix everything else
            if (data.Status == true) {
                location.reload();
            }
        }
    });
});