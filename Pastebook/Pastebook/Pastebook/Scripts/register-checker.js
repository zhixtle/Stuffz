$(document).ready(function () {

    $('#txtEmail, #txtUsername').on('focusout', function () {

        var emailText = $('#txtEmail').val();
            emailText = emailText.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        var userText = $('#txtUsername').val();
            userText = userText.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var userData = {
            email: emailText,
            username: userText
        }

        $.ajax({
            url: CheckEmailUsernameUrl,
            data: userData,
            success: function (data) {

                if (data.EmailExists == true) {
                    $('#validationEmailChecker').text('Email address already exists.');
                    $('#btnRegister').prop('disabled', true);
                }
                else {
                    $('#validationEmailChecker').text(' ');
                }

                if (data.UserExists == true) {
                    $('#validationUsernameChecker').text('Username already exists.');
                    $('#btnRegister').prop('disabled', true);
                }
                else {
                    $('#validationUsernameChecker').text(' ');
                    
                }
                
                if (data.EmailExists == false && data.UserExists == false) {
                    $('#btnRegister').prop('disabled', false);
                }

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

    $('#txtPassword').focusout(function () {
        if ($('#txtPassword').val() == '') {
            $('#validationPassword').text('Password is required.');
        }
        else if ($('#txtPassword').val().length > 50) {
            $('#validationPassword').text('Password can only have up to 50 characters.');
        }
        else {
            $('#validationPassword').text(' ');
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

