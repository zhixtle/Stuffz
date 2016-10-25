var CheckOldPasswordUrl = '/Account/CheckOldPassword/';
var SaveEmailUrl = '/Account/EditEmail/';
var SavePassUrl = '/Account/EditPassword/';

$(document).ready(function ()
{
    $('#editEmailForm').submit(function (e) {
        e.preventDefault();
        alert('HERE EMAIL');

        var data = {
            oldPassword: $('#txtOldPasswordEmail').val()
        }

        $.ajax({
            type: 'POST',
            url: CheckOldPasswordUrl,
            data: data,
            success: function (data) {
                if (data.Status == true) {
                    alert('TRUE');
                    $('#settings-page').load(SaveEmailUrl, {
                        email: $('#txtEmail').val()
                    });
                }
                else {
                    alert('IZ WRONG');
                }
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });
    });

    $('#editPassForm').submit(function (e) {
        e.preventDefault();
        alert('HERE PASS');

        var data = {
            oldPassword: $('#txtOldPasswordPass').val()
        }

        $.ajax({
            type: 'POST',
            url: CheckOldPasswordUrl,
            data: data,
            success: function (data) {
                if (data.Status == true) {
                    alert('TRUE');
                    $('#settings-page').load(SavePassUrl, {
                        password: $('#txtNewPassword').val()
                    });
                }
                else {
                    alert('IZ WRONG');
                }
            },
            error: function () {
                alert('NOOOOOOOOOOOOOO');
            }
        });
    });
});