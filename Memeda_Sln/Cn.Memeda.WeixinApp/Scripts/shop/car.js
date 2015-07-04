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