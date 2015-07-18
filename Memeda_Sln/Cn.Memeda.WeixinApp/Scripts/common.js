// JavaScript Document
$(document).ready(function (e) {
    //SetCookie("openid", "pkN2yMNAruJ5pw8PwLpY", 7);

    //进入购物车
    $("#btn-goto-car").click(function () {
        GoToCar();
    });
});
//绑定添加商品到购物车的事件
function BindAddToCarEvent()
{
    //减少商品
    $(".count_odd").click(function () {
        var product_account = parseInt($(".now_count").text());
        var carGoodsCount = parseInt($("#btn-goto-car>span").html());
        if (product_account >1) {
            product_account = product_account - 1;
            carGoodsCount = carGoodsCount - 1;
            $(".now_count").text(product_account)
            $(".car_shop span").text(carGoodsCount);
        }
        else {
            product_account = 1;
            $(".now_count").text(product_account);
            $(".count_group").css("z-index", "1");
            $(".button_add_car").css("z-index", "2");
        }
        car_span();
    });
    //增加商品
    $(".count_add").click(function () {

        var product_account = parseInt($(".now_count").text());
        product_account = product_account + 1;
        $(".now_count").text(product_account)

        //购物车商品数量
        var carGoodsEle=$("#btn-goto-car>span");
        var carGoodsCount = parseInt(carGoodsEle.html());
        carGoodsCount = carGoodsCount + 1;
        carGoodsEle.text(carGoodsCount);

        car_span();
    });
    //增加商品到购物车
    $("#btn-add-to-car").click(function () {
        var id = $(this).attr("value");
        var product_account = parseInt($(".now_count").text());
        var data = AddToCar(id, product_account);
    });
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

//获取购物车商品数量
function GetCarGoodsCount() {
    var openid= GetOpenid();
    $.ajax({
        type: "get",
        url: " http://120.24.228.51:8080/20150623/weixin/cart/quantity.jhtml",
        data: { openid:openid },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.quantity > 0)
                $(".car_shop span").text(data.quantity);
        },
        error: function () { },
        complete: function () { }
    });
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
//获取openid
function GetOpenid() {
    return GetCookie("openid");
}
//加载店铺商品
function LoadShopGoodsList(merchantsId, cataID) {
    $.ajax({
        type: "Get",
        url: " http://120.24.228.51:8080/20150623/weixin/product/list/" + cataID + ".jhtml",
        data: { merchantsId: merchantsId, openid: GetOpenid() },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.content != null) {
                LoadGoodsList(data.content);                
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}
//加载商品列表
function LoadGoodsList(goodsList) {
    var productList = '';
    $.each(goodsList, function (i, item) {
        productList += '<li>';
        productList += '<a href="#" value="' + item.id + '">';
        productList += '<img src="' + item.image + '" class="fruit_img" url="' + item.image + '">';
        productList += '<p><span class="fr"><i class="weight">' + item.weight + '</i>/' + item.unit + '</span><i>' + item.name + '</i></p>';
        productList += '<div class="product_price"><img src="/Content/images/car_07.jpg" /><i>￥' + item.price + '</i><span>￥' + item.price + '</span></div>';
        productList += '</a>';
        productList += '</li>';
        if (i == goodsList.length - 1) {
            //加载元素
            var strShopGoodsListEle = $('#shop-goods-list').html().replace('<div class="clear"></div>', '');
            strShopGoodsListEle += productList;
            strShopGoodsListEle += '<div class="clear"></div>';
            $('#shop-goods-list').html(strShopGoodsListEle);

            //商品信息弹出层
            var goodsListEle = $('#shop-goods-list>li>a');
            BindGoodsListEvent(goodsListEle);
            BindAddToCarEvent();
        }
    });
}
//绑定商品列表元素的点击事件
function BindGoodsListEvent(goodsListEle) {
    //商品信息弹出层
    goodsListEle.click(function () {
        $(".now_count").html("1");
        //改变图片
        var urls = $(this).children(".fruit_img").attr("url");
        var href2 = '<img  src="' + urls + '" />';
        $(".product_picture").html(href2);
        /*改变店铺名*/
        $(".shop_name span").text($("#shop-shopkeeper").text());
        /*商品名称与价格*/
        var product_names = $(this).children("p").children("i").text();
        $(".product_name i").text(product_names);
        var product_pric = $(this).children(".product_price").children("i").text();
        $(".product_name span").text(product_pric);
        /*商品规格*/
        $(".weight span").text($(this).children("p").children("span").children(".weight").text());
        ShowGoodsDetail($(this).attr("value"));

        //购物车商品数量
        var carGoodsEle = $("#btn-goto-car>span");
        var carGoodsCount = parseInt(carGoodsEle.html());
        carGoodsCount = carGoodsCount + 1;
        carGoodsEle.text(carGoodsCount);
    });
    ClearThirdBorderRight(goodsListEle);
    CloseGoodsDetail();
}
//三列布局，去除最后一列div的右边框
function ClearThirdBorderRight(goodsListEle) {
    var shop_product_N = goodsListEle
    var n = shop_product_N.length;
    for (i = 0; i < n; i++) {
        var index_now = shop_product_N.index(shop_product_N[i]) + 1;
        if (index_now % 3 == 0) {
            shop_product_N[i].style.borderRight = "1px solid #ffffff"
        }
    }
}
/*显示商品详情*/
function ShowGoodsDetail(id) {
    var product_alert = parseInt($(".product_detail").height());
    var margin_top = product_alert / 2 + 20;
    $(".product_detail").css({ "top": "40%", "margin-top": -margin_top });
    $("#btn-add-to-car").attr("value", id);
    $(".fixed").show();
    $(".product_detail").show();
}
//关闭商品详情弹出页
function CloseGoodsDetail()
{
    //关闭弹出层
    $(".close_detail").click(function () {
        GetCarGoodsCount();
        HidePop();
    })
}
//隐藏商品详情弹出框
function HidePop() {

    $(".fixed").hide();
    $(".product_detail").hide();
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
            HidePop();
        },
        error: function () { },
        complete: function () { }
    });
}
/*购物车图标*/
function car_span(carEle) {
    var car_span = $(".car_shop span").text();
    if (car_span > 0) {
        $(".car_shop span").show()
    }
    else {
        $(".car_shop span").hide()
    }
}
//进入购物车
function GoToCar()
{
    location.href = "/shop/car?openid="+GetOpenid();
}