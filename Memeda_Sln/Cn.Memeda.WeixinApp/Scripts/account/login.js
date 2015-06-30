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
                url: "http://120.24.228.51:8080/20150623/weixin/login/validationSms.jhtml",
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
            url: " http://120.24.228.51:8080/20150623/weixin/register/submit.jhtml",
            data: { code: smscode, phone: phone,username:phone},
            dataType: "json",
            jsonp: "jsoncallback",
            crossDomain: true,
            beforeSend: function () { },
            success: function (data) {
                if (data.type == 'success') {
                    location.href = "/home/index";
                    SetCookie('phone',phone,7);
                }
                else { loginErrorEle.html(data.content); }
            },
            error: function () { },
            complete: function () { }
        });
    });
});
//设置Cookie
function SetCookie(c_name,value,expiredays)
{
    var exdate=new Date()
    exdate.setDate(exdate.getDate()+expiredays)
    document.cookie=c_name+ "=" +escape(value)+
    ((expiredays==null) ? "" : ";expires="+exdate.toGMTString())
}
//验证手机号码
function ValidPhone(phoneNumber) {
    if (phoneNumber == undefined || phoneNumber == '') {
        loginErrorEle.html('请输入手机号码！');
        return false;
    }
    var myreg = /^(((1[0-9]{2}))+\d{8})$/;
    if (phoneNumber.length != 11 || !myreg.test(phoneNumber)) {
        loginErrorEle.html('请输入有效的手机号码！');
        return false;
    }
    return true;
}
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