$(document).ready(function ()
{
    $('#editEmailForm').submit(function (e) {
        e.preventDefault();

        var emailText = $('#txtEmail').val();
        emailText = emailText.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        var oldPasswordText = $('#txtOldPasswordEmail').val();
        oldPasswordText = oldPasswordText.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var passwordData = {
            oldPassword: oldPasswordText
        }
        var emailData = {
            email: emailText
        }

        $.ajax({
            type: 'POST',
            url: CheckOldPasswordUrl,
            data: passwordData,
            success: function (data) {
                if (data.Status == true) {
                    $('#emailStatusGood').text('Current password is correct. Loading...');
                    $('#emailStatusBad').text('');
                    setTimeout(SaveEmail, 1000);
                }
                else {
                    $('#emailStatusGood').text('');
                    $('#emailStatusBad').text('Current password is incorrect.');
                }
            }
        });

        function SaveEmail() {
            $.ajax({
                type: 'POST',
                url: SaveEmailUrl,
                data: emailData,
                success: function (data) {
                    if (data.Status == true) {
                        $('#emailStatusGood').text('Email successfully changed.');
                        $('#emailStatusBad').text('');
                        setTimeout(PageReload,1000);
                    }
                    else {
                        $('#emailStatusGood').text('');
                        $('#emailStatusBad').text('Error, email could not be changed.');
                    }
                }
            });
        }
    });

    $('#editPassForm').submit(function (e) {
        e.preventDefault();

        var newPasswordText = $('#txtNewPassword').val();
        newPasswordText = newPasswordText.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        var oldPasswordText = $('#txtOldPasswordPass').val();
        oldPasswordText = oldPasswordText.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var passwordData = {
            oldPassword: oldPasswordText
        }
        var newPasswordData = {
            password: newPasswordText
        }

        $.ajax({
            type: 'POST',
            url: CheckOldPasswordUrl,
            data: passwordData,
            success: function (data) {
                if (data.Status == true) {
                    $('#passwordStatusGood').text('Current password is correct. Loading...');
                    $('#passwordStatusBad').text('');
                    setTimeout(SavePassword, 1000);
                }
                else {
                    $('#passwordStatusGood').text('');
                    $('#passwordStatusBad').text('Current password is incorrect.');
                }
            }
        });

        function SavePassword() {
            $.ajax({
                type: 'POST',
                url: SavePassUrl,
                data: newPasswordData,
                success: function (data) {
                    if (data.Status == true) {
                        $('#passwordStatusGood').text('Password successfully changed.');
                        $('#passwordStatusBad').text('');
                        setTimeout(PageReload, 1000);
                    }
                    else {
                        $('#passwordStatusGood').text('');
                        $('#passwordStatusBad').text('Error, password could not be changed.');
                    }
                }
            });
        }        
    });

    function PageReload() {
        location.reload();
    }

});