var ConfirmFriendRequestUrl = '/UserProfile/ConfirmFriendRequest/';
var DeleteFriendRequestUrl = '/UserProfile/DeleteFriendRequest/'

$(document).ready(function () {
    $('#btnFriendConfirm').on('click', function () {

        var friendConfirmData = {
            profileUsername: $('#username').val()
        }

        $.ajax({
            url: ConfirmFriendRequestUrl,
            data: friendConfirmData,
            success: function (data) {
                ConfirmFriendRequestSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function ConfirmFriendRequestSuccess(data) {
            //goodness, please change this after you fix everything else
            if (data.Status == true) {
                location.reload();
            }
        }
    });

    $('#btnFriendDelete').on('click', function () {

        var friendDeleteData = {
            profileUsername: $('#username').val()
        }

        $.ajax({
            url: DeleteFriendRequestUrl,
            data: friendDeleteData,
            success: function (data) {
                DeleteFriendRequestSuccess(data)
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });

        function DeleteFriendRequestSuccess(data) {
            //goodness, please change this after you fix everything else
            if (data.Status == true) {
                location.reload();
            }
        }
    });
});