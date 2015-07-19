//登录界面的错误信息
var loginErrorEle = $('#login-error-message');
/*登录和注册*/
$(document).ready(function (e) {
    var phoneEle = $('#login-phone-number');
    var smscode = '';
    //send valid code
    $('#btn-login-valid-code').click(function () {
        loginErrorEle.html('');
        var phoneNumber = phoneEle.val().trim();
        if (ValidPhone(phoneNumber)) {
            seconds = 60;
            $('#btn-login-valid-code').hide();
            $('#login-count-down').show();
            TimerCountDown();
            //发送验证码
            $.ajax({
                type: "post",
                url: "http://s.memeda.cn/weixin/login/sendSms.jhtml",
                data: { phone: phoneNumber },
                dataType: "json",
                jsonp: "jsoncallback",
                crossDomain: true,
                beforeSend: function () { },
                success: function (data) {
                    if (data.type == 'success') {
                    }
                    else { loginErrorEle.html(data.content); }
                },
                error: function () { },
                complete: function () { }
            });
        }

    });
    //Login or register
    $('#btn-login').click(function () {
        var smscode = $('#login-valid-code').val().trim();
        var regCode = /^(\d{4})$/;
        if (smscode == undefined || smscode == '') {
            loginErrorEle.html('请输入验证码！');
            return;
        }//
        if (!regCode.test(smscode)) {
            loginErrorEle.html('验证码不正确！');
            return;
        }
        var phone=phoneEle.val();
        $.ajax({
            type: "POST",
            url: " http://s.memeda.cn/weixin/register/submit.jhtml",
            data: { code: smscode, phone: phone,username:phone},
            dataType: "json",
            jsonp: "jsoncallback",
            crossDomain: true,
            beforeSend: function () { },
            success: function (data) {
                if (data.type == 'success') {
                    location.href = "/home/index";
                    SetCookie('tokenM', data.content, 7);
                }
                else { loginErrorEle.html(data.content); }
            },
            error: function () { },
            complete: function () { }
        });
    });
});
var seconds = 0;
//倒计时
function TimerCountDown() {
    if (seconds != 0) {
        $('#login-count-down>span').html(seconds);
        setTimeout("TimerCountDown()", 1000);
    }
    else {
        $('#btn-login-valid-code').show();
        $('#login-count-down').hide();

    }
    seconds--;
}