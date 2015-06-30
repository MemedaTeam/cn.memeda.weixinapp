/*登录和注册*/
$(document).ready(function (e) {
    var phoneEle = $('#car-phone-number');
    //send valid code
    $('#btn-car-valid-code').click(function () {
        loginErrorEle.html('');
        smscode = '';
        var phoneNumber = phoneEle.val().trim();
        if (ValidPhone(phoneNumber)) {
            seconds = 60;
            $('#btn-login-valid-code').hide();
            $('#login-count-down').show();
            TimerCountDown();
            //发送验证码
            $.ajax({
                type: "post",
                url: "http://120.24.228.51:8080/20150623/weixin/register/sendSms.jhtml",
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
        var currentSmsCode = $('#login-valid-code').val().trim();
        var regCode = /^(\d{4})$/;
        if (smscode == undefined || smscode == '') {
            loginErrorEle.html('请输入验证码！');
            return;
        }//!regCode.test(smsCode) &&
        if (currentSmsCode != smscode) {
            loginErrorEle.html('验证码不正确！');
            return;
        }
        var phone=phoneEle.val();
        $.ajax({
            type: "POST",
            url: " http://120.24.228.51:8080/20150623/weixin/register/validationSms.jhtml",
            data: { code: smscode, phone: phone,username:phone },
            dataType: "json",
            jsonp: "jsoncallback",
            crossDomain: true,
            beforeSend: function () { },
            success: function (data) {
                if (data.type == 'success') {
                    location.href = "/home/index";
                }
                else { loginErrorEle.html(data.content); }
            },
            error: function () { },
            complete: function () { }
        });
    });
    //添加商品到购物车
    $('#btn-add-to-car').click(function () {
        $('#shop-car-goods-count').html(0);
    });
    LoadShopInfomation(1);
    LoadShopGoodsList(1);
});
function SendSMSCode() {
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
//设置Cookie
function SetCookie(c_name, value, expiredays) {
    var exdate = new Date()
    exdate.setDate(exdate.getDate() + expiredays)
    document.cookie = c_name + "=" + escape(value) +
    ((expiredays == null) ? "" : ";expires=" + exdate.toGMTString())
}
//获取Cookie
function GetCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}
//检查Cookie
function CheckCookie(c_name) {
    var cookievalue = GetCookie(c_name)
    if (cookievalue != null && cookievalue != "")
    { return true; }
    else
    {
        return false;
    }
}