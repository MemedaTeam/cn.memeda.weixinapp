// JavaScript Document
$(document).ready(function (e) {
    SetCookie("openid","123456",7);
});
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
//获取url中的参数
function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串
    var theRequest = new Object();
    if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
            theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);
        }
    }
    return theRequest;
}
//添加商品到购物车
function AddToCar(id, quantity) {
    var ele = $('#car-error-message');
    if (quantity == 0)
    {
        ele.html('请选择商品数量!');
        return;
    }
    var openid = GetOpenid();
    $.ajax({
        type: "Post",
        url: " http://120.24.228.51:8080/20150623/weixin/cart/add.jhtml",
        data: { id: id, quantity: quantity,openid:openid },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            ele.html(data.content);
        },
        error: function () { },
        complete: function () { }
    });
}
//获取openid
function GetOpenid() {
    return GetCookie("openid");
}
//进入购物车
function GoToCar()
{
    location.href = "/shop/car?openid="+GetOpenid();
}