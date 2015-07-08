/*登录和注册*/
var loginErrorEle = $('#car-error-message');
$(document).ready(function (e) {
    var phoneEle = $('#car-phone-number');
    //send valid code
    $('#btn-car-valid-code').click(function () {
        loginErrorEle.html('');
        var phoneNumber = $('#phone_number').val().trim();
        if (ValidPhone(phoneNumber)) {
            seconds = 60;
            $('#btn-car-valid-code').hide();
            $('#car-count-down').show();
            TimerCountDown();
            $('#car-show-valid-code').show();
            //发送验证码
            $.ajax({
                type: "post",
                url: "http://120.24.228.51:8080/20150623/weixin/register/sendSms.jhtml",
                data: { phone: phoneNumber, openid: GetOpenid() },
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
    LoadUserInfomation();
    LoadShopCarInformation();
    //提交订单
    $('#btn-submit-order').click(function () {
        $.ajax({
            type: "get",
            url: "http://120.24.228.51:8080/20150623/weixin/member/order/create.jhtml",
            data: { openid: GetOpenid() },
            dataType: "json",
            jsonp: "jsoncallback",
            crossDomain: true,
            beforeSend: function () { },
            success: function (data) {
                if (data != null) {
                    location.href = "/order/index?openid=" + GetOpenid();
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
        $('#car-count-down>#car-time').html(seconds);
        setTimeout("TimerCountDown()", 1000);
    }
    else {
        $('#btn-car-valid-code').show();
        $('#car-count-down').hide();

    }
    seconds--;
}
//加载收货人信息
function LoadUserInfomation() {
    $.ajax({
        type: "get",
        url: "http://120.24.228.51:8080/20150623/weixin/member/order/receiverList.jhtml",
        data: { openid: GetOpenid() },
        dataType: "json",
        jsonp: "jsoncallback",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data!=null) {
                $('#receiveName').val(data.consignee);
                $('#phone_number').val(data.phone);
                if (data.phone != '') {
                    $('#btn-car-valid-code').removeClass('btn-car-valid-code-green').addClass('btn-car-valid-code-gray').html('修改手机号');
                }
                $('#receive-city').val(data.areaName);
                $('#receive-address').val(data.address);
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}
//加载购物车信息
function LoadShopCarInformation() {
    $.ajax({
        type: "get",
        url: "http://120.24.228.51:8080/20150623/weixin/cart/list.jhtml",
        data: { openid:GetOpenid() },
        dataType: "json",
        jsonp: "jsoncallback",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.cartItems!=null) {
                $.each(data.cartItems, function (i, item) {

                });
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}