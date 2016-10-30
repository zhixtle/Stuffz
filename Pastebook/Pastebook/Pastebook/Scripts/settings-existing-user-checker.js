$(document).ready(function () {
    
    $('#txtEmail').on('focusout', function () {

        var emailText = $('#txtEmail').val();
            emailText = emailText.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var emailData = {
            email: emailText
        }

        $.ajax({
            url: CheckEmailUrl,
            data: emailData,
            success: function (data) {
                if (data.Status == true) {
                    $('#validationEmail').text('Email address already exists.');
                    $('#btnSaveEmail').prop('disabled', true);
                }
                else {
                    $('#validationEmail').text(' ');
                    $('#btnSaveEmail').prop('disabled', false);
                }
            }
        });
    });

    $('#txtUsername').on('focusout', function () {

        var userText = $('#txtUsername').val();
            userText = userText.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var userData = {
            username: userText
        }

        $.ajax({
            url: CheckUsernameUrl,
            data: userData,
            success: function (data) {
                if (data.Status == true) {
                    $('#validationUsername').text('Username already exists.');
                    $('#btnSaveProfile').prop('disabled', true);
                }
                else {
                    $('#validationUsername').text(' ');
                    $('#btnSaveProfile').prop('disabled', false);
                }
            }
        });
    });

});