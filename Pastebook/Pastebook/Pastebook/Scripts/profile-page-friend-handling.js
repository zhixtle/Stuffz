$(document).ready(function () {

    $('#btnFriendRequest').on('click', function () {

        var friendRequestData = {
            profileUsername: $('#username').val()
        }

        $.ajax({
            url: FriendRequestUrl,
            data: friendRequestData,
            success: function (data) {
                FriendRequestProcessSuccess(data)
            }
        });

    });

    $('#btnFriendConfirm').on('click', function () {

        var friendConfirmData = {
            profileUsername: $('#username').val()
        }

        $.ajax({
            url: ConfirmFriendRequestUrl,
            data: friendConfirmData,
            success: function (data) {
                FriendRequestProcessSuccess(data)
            }
        });

    });

    $('#btnFriendDelete').on('click', function () {

        var friendDeleteData = {
            profileUsername: $('#username').val()
        }

        $.ajax({
            url: DeleteFriendRequestUrl,
            data: friendDeleteData,
            success: function (data) {
                FriendRequestProcessSuccess(data)
            }
        });

    });

    function FriendRequestProcessSuccess(data) {
        if (data.Status == true) {
            location.reload();
        }
    }

});