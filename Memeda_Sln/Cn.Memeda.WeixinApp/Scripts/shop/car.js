/*登录和注册*/
var loginErrorEle = $('#car-error-message');
var openid;
$(document).ready(function (e) {
    openid = GetCookie("openid");
    var phoneEle = $('#car-phone-number');
    //send valid code
    $('#btn-car-valid-code').click(function () {
        loginErrorEle.html('');
        smscode = '';
        var phoneNumber = phoneEle.val().trim();
        if (ValidPhone(phoneNumber)) {
            seconds = 60;
            $('#btn-car-valid-code').hide();
            $('#car-count-down').show();
            TimerCountDown();
            //发送验证码
            $.ajax({
                type: "post",
                url: "http://120.24.228.51:8080/20150623/weixin/register/sendSms.jhtml",
                data: { phone: phoneNumber,openid:openid },
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
//加载购物车信息
function LoadShopCarInformation() {
    $.ajax({
        type: "post",
        url: "http://120.24.228.51:8080/20150623/weixin/cart/list.jhtml",
        data: { openid: openid },
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