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
                    $('#emailStatus').text('Current password is correct. Loading...');
                    setTimeout(SaveEmail, 1000);
                }
                else {
                    $('#emailStatus').text('Current password is incorrect.');
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
                        $('#emailStatus').text('Email successfully changed.');
                        setTimeout(PageReload,1000);
                    }
                    else {
                        $('#emailStatus').text('Error, email could not be changed.');
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
                    $('#passwordStatus').text('Current password is correct. Loading...');
                    setTimeout(SavePassword, 1000);
                }
                else {
                    $('#passwordStatus').text('Current password is incorrect.');
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
                        $('#passwordStatus').text('Password successfully changed.');
                        setTimeout(PageReload, 1000);
                    }
                    else {
                        $('#passwordStatus').text('Error, password could not be changed.');
                    }
                }
            });
        }        
    });

    function PageReload() {
        location.reload();
    }

    $('#txtUsername').focusout(function () {
        if ($('#txtUsername').val() == '') {
            $('#validationUsername').text('Username is required.');
        }
        else if ($('#txtUsername').val().length > 50) {
            $('#validationUsername').text('Username can only have up to 50 characters.');
        }
        else {
            $('#validationUsername').text(' ');
        }
    });

    $('#txtFirstName').focusout(function () {
        if ($('#txtFirstName').val() == '') {
            $('#validationFirstName').text('First Name is required.');
        }
        else if ($('#txtFirstName').val().length > 50) {
            $('#validationFirstName').text('First Name can only have up to 50 characters.');
        }
        else {
            $('#validationFirstName').text(' ');
        }
    });

    $('#txtLastName').focusout(function () {
        if ($('#txtLastName').val() == '') {
            $('#validationLastName').text('Last Name is required.');
        }
        else if ($('#txtLastName').val().length > 50) {
            $('#validationLastName').text('Last Name can only have up to 50 characters.');
        }
        else {
            $('#validationLastName').text(' ');
        }
    });

    $('#txtEmail').focusout(function () {
        if ($('#txtEmail').val() == '') {
            $('#validationEmail').text('Email Address is required.');
        }
        else if ($('#txtEmail').val().length > 50) {
            $('#validationEmail').text('Email Address can only have up to 50 characters.');
        }
        else {
            $('#validationEmail').text(' ');
        }
    });

    $('#txtOldPasswordEmail').focusout(function () {
        if ($('#txtOldPasswordEmail').val() == '') {
            $('#validationOldPasswordEmail').text('Password is required.');
        }
        else if ($('#txtOldPasswordEmail').val().length > 50) {
            $('#validationOldPasswordEmail').text('Password can only have up to 50 characters.');
        }
        else {
            $('#validationOldPasswordEmail').text(' ');
        }
    });

    $('#txtOldPasswordPass').focusout(function () {
        if ($('#txtOldPasswordPass').val() == '') {
            $('#validationOldPasswordPass').text('Password is required.');
        }
        else if ($('#txtOldPasswordPass').val().length > 50) {
            $('#validationOldPasswordPass').text('Password can only have up to 50 characters.');
        }
        else {
            $('#validationOldPasswordPass').text(' ');
        }
    });

    $('#txtNewPassword').focusout(function () {
        if ($('#txtNewPassword').val() == '') {
            $('#validationNewPassword').text('Password is required.');
        }
        else if ($('#txtNewPassword').val().length > 50) {
            $('#validationNewPassword').text('Password can only have up to 50 characters.');
        }
        else {
            $('#validationNewPassword').text(' ');
        }
    });

    $('#txtConfirmPassword').focusout(function () {
        if ($('#txtConfirmPassword').val() == '') {
            $('#validationConfirmPassword').text('Confirm Password is required.');
        }
        else if ($('#txtConfirmPassword').val().length > 50) {
            $('#validationConfirmPassword').text('Confirm Password can only have up to 50 characters.');
        }
        else if ($('#txtConfirmPassword').val() != $('#txtPassword').val()) {
            $('#validationConfirmPassword').text('Passwords do not match.');
        }
        else {
            $('#validationConfirmPassword').text(' ');
        }
    });

    $('#txtBirthday').focusout(function () {
        if ($('#txtBirthday').val() == '') {
            $('#validationBirthday').text('Birthday is required.');
        }
        else {
            $('#validationBirthday').text(' ');
        }
    });

    $('#txtNumber').focusout(function () {
        if ($('#txtNumber').val().length > 50) {
            $('#validationNumber').text('Mobile Number can only have up to 50 characters.');
        }
        else {
            $('#validationNumber').text(' ');
        }
    });

});