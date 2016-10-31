$(document).ready(function () {
    
    var validNewEmail = false;

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
                    $('#validationEmailExists').text('Email address already exists.');
                    validNewEmail = false;
                }
                else {
                    $('#validationEmailExists').text(' ');
                    validNewEmail = true;
                }
                ValidEmailFormCheck();
            }
        });
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
        ValidEmailFormCheck();
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
        ValidEmailFormCheck();
    });

    function ValidEmailFormCheck() {
        if (($('#editEmailForm').valid() == true)  && (validNewEmail == true)) {
            $('#btnSaveEmail').prop('disabled', false);
        }
        else {
            $('#btnSaveEmail').prop('disabled', true);
        }
    }

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
        ValidPassFormCheck();
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
        ValidPassFormCheck();
    });

    $('#txtConfirmPassword').focusout(function () {
        if ($('#txtConfirmPassword').val() == '') {
            $('#validationConfirmPassword').text('Confirm Password is required.');
        }
        else if ($('#txtConfirmPassword').val().length > 50) {
            $('#validationConfirmPassword').text('Confirm Password can only have up to 50 characters.');
        }
        else if ($('#txtConfirmPassword').val() != $('#txtNewPassword').val()) {
            $('#validationConfirmPassword').text('Passwords do not match.');
        }
        else {
            $('#validationConfirmPassword').text(' ');
        }
        ValidPassFormCheck();
    });

    function ValidPassFormCheck() {
        if ($('#editPassForm').valid() == true) {
            $('#btnSavePassword').prop('disabled', false);
        }
        else {
            $('#btnSavePassword').prop('disabled', true);
        }
    }

    var validNewUsername = true;

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
                    $('#validationUsernameExists').text('Username already exists.');
                    validNewUsername = false;
                }
                else {
                    $('#validationUsernameExists').text(' ');
                    validNewUsername = true;
                }
                ValidDetailsCheck();
            }
        });
    });

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
        ValidDetailsCheck();
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
        ValidDetailsCheck();
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
        ValidDetailsCheck();
    });

    $('#txtBirthday').focusout(function () {
        if ($('#txtBirthday').val() == '') {
            $('#validationBirthday').text('Birthday is required.');
        }
        else {
            $('#validationBirthday').text(' ');
        }
        ValidDetailsCheck();
    });

    $('#txtNumber').focusout(function () {
        if ($('#txtNumber').val().length > 50) {
            $('#validationNumber').text('Mobile Number can only have up to 50 characters.');
        }
        else {
            $('#validationNumber').text(' ');
        }
        ValidDetailsCheck();
    });

    function ValidDetailsCheck() {
        if (($('#editDetailsForm').valid() == true) && (validNewUsername == true)) {
            $('#btnSaveProfile').prop('disabled', false);
        }
        else {
            $('#btnSaveProfile').prop('disabled', true);
        }
    }

});