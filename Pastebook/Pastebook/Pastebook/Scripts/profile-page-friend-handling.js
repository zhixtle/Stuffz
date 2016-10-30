$(document).ready(function () {

    $('#btnFriendRequest').on('click', function () {

        var friendRequestData = {
            profileID: userID
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
            profileID: userID
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
            profileID: userID
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