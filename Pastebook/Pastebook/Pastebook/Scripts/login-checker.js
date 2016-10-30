$(document).ready(function () {
    $('#btnLogin').on('click', function () {
        
        var loginEmail = $('#txtLoginEmail').val();
        var loginPassword = $('#txtLoginPassword').val()
        loginEmail = loginEmail.replace(/</g, '&lt;').replace(/>/g, '&gt;');
        loginPassword = loginPassword.replace(/</g, '&lt;').replace(/>/g, '&gt;');

        var loginData = {
            email: loginEmail,
            password: loginPassword
        }

        $.ajax({
            url: LoginAccountUrl,
            data: loginData,
            success: function (data) {
                LoginSuccess(data)
            }
        });

        function LoginSuccess(data) {
            $('#loginStatus').text(' ');
            if (loginData.email == '' || loginData.password == '') {
                $('#loginStatus').text('Please input username and password.');
            }
            else {
                if (data.UserExists == true && data.PasswordMatch == true) {
                    $('#loginStatus').text('Login Successful.');
                    setTimeout(ToIndex, 1000);
                }
                else if (data.UserExists == false) {
                    $('#loginStatus').text('Login unsuccessful.\nUser does not exist.');
                }
                else if (data.PasswordMatch == false) {
                    $('#loginStatus').text('Login unsuccessful.\nIncorrect password.');
                }
            }
        }

        function ToIndex() {
            $(location).attr('href', IndexUrl);
        }

    });

    $('#txtLoginEmail').focusout(function () {
        if ($('#txtLoginEmail').val() == '') {
            $('#validationLoginEmail').text('Email Address is required.');
        }
        else {
            $('#validationLoginEmail').text(' ');
        }
    });

    $('#txtLoginPassword').focusout(function () {
        if ($('#txtLoginPassword').val() == '') {
            $('#validationLoginPassword').text('Password is required.');
        }
        else {
            $('#validationLoginPassword').text(' ');
        }
    });
});