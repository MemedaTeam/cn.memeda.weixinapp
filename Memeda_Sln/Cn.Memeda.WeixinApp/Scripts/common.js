// JavaScript Document
//登录界面的错误信息
var loginErrorEle = $('#login-error-message');
/*登录和注册*/
$(document).ready(function (e) {
    var phoneEle = $('#login-phone-number');
    var smscode = '';
    //send valid code
    $('#btn-login-valid-code').click(function () {
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
                crossDomain:true,
                beforeSend: function () { },
                success: function (data) {
                    if (data.type == 'success') {
                        smscode = data.content;
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
        $.ajax({
            type: "POST",
            url: " http://120.24.228.51:8080/20150623/weixin/register/validationSms.jhtml",
            data: { code: smscode, phone: phoneEle.val() },
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
//加载店铺信息
function LoadShopInfomation(merchantsId) {
    $.ajax({
        type: "Get",
        url: " http://120.24.228.51:8080/20150623/weixin/merchants/getMerchantsById.jhtml",
        data: { merchantsId: merchantsId },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data !=null) {
                var userid = data.id;
                var gpsX = data.gpsx;
                var gpsY = data.gpsY;
                $('#shop-shopkeeper').html(data.name);
                $('#shop-address').html(data.address);
                $('#shop-shopkeeper-image').html(data.image);
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}
//加载店铺商品
function LoadShopGoodsList(merchantsId) {
    $.ajax({
        type: "Get",
        url: " http://120.24.228.51:8080/20150623/weixin/product/list/81.jhtml",
        data: { merchantsId: merchantsId },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data!=null && data.content!=null) {
                var productList='';
                $.each(data.content, function (i, item) {
                    productList += '<li>';
                    productList += '<a href="#">';
                    productList += '<img src="'+item.thumbnail+'" class="fruit_img" url="'+data.image+'">';
                    productList += '<p><span class="fr"><i class="weight">' + item.weight + '</i>/' + item.unit + '</span><i>' + item.name + '</i></p>';
                    productList += '<div class="product_price"><img src="/Content/images/car_07.jpg" /><i>￥' + data.price + '</i><span>￥' + data.price + '</span></div>';
                    productList += '</a>';
                    productList += '</li>';
                    if (i == data.content.length) {
                        //加载元素
                        var strShopGoodsListEle = $('#shop-goods-list').innerHTML.replace('<div class="clear"></div>', '');
                        strShopGoodsListEle += productList;
                        strShopGoodsListEle += '<div class="clear"></div>';
                        $('#shop-goods-list').html(strShopGoodsListEle);

                        //商品信息弹出层
                        $('#shop-goods-list>li>a').click(function () {

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

                            $(".fixed").show();
                            $(".product_detail").show();
                        });
                        //关闭弹出层
                        $(".close_detail").click(function () {
                            $(".fixed").hide();
                            $(".product_detail").hide();

                        })
                    }
                });
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}

/*超市*/
$(document).ready(function (e) {
    $(".product_menu li a").click(function () {
        var attr_this = $(this).attr("tip");
        $(".product_menu li a").removeClass("now_product")
        $(this).addClass("now_product")
        $(attr_this).show().siblings().hide();
    })
})

/*超市第3个块 去除右边线*/
$(function () {
    var shop_product_N = $(".producter_con_shop li a")
    var n = shop_product_N.length;
    for (i = 0; i < n; i++) {
        var index_now = shop_product_N.index(shop_product_N[i]) + 1;
        if (index_now % 3 == 0) {
            shop_product_N[i].style.borderRight = "1px solid #ffffff"
        }
    }

})

$(document).ready(function (e) {
    var password = $("#block_input");
    password.focus(function () {
        if ($(this).val() == "请输入小区名称") {
            $(this).val("");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("请输入小区名称");
        }
    });
});
$(document).ready(function (e) {
    var password = $("#customer");
    password.focus(function () {
        if ($(this).val() == "请输入收货人名字") {
            $(this).val("");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("请输入收货人名字");
        }
    });
});
$(document).ready(function (e) {
    var password = $("#house_number");
    password.focus(function () {
        if ($(this).val() == "请输入您的单元号、门牌号等") {
            $(this).val("");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("请输入您的单元号、门牌号等");
        }
    });
});

/*商品详细信息弹出层 */
$(function () {

    var product_alert = parseInt($(".product_detail").height());
    var margin_top = product_alert / 2 + 20;
    $(".product_detail").css({ "top": "20%", "margin-top": -margin_top })

})
$(function () {

    //购物车 与 商品数量
    $(".button_add_car").click(function () {
        $(this).css("z-index", "1");
        $(".count_group").css("z-index", "2");
    })

    var product_account = parseInt($(".now_count").text());
    function car_span() {
        var car_span = $(".car_shop span").text();
        if (car_span > 0) {
            $(".car_shop span").show()
        }
        else {
            $(".car_shop span").hide()
        }
    }
    $(".count_odd").click(function () {
        if (product_account > 1) {
            product_account = product_account - 1;
            $(".now_count").text(product_account)
            $(".car_shop span").text(product_account);
            $(".trolley_count span").text(product_account);
        }
        else {
            $(".now_count").text(1)
            $(".count_group").css("z-index", "1");
            $(".button_add_car").css("z-index", "2");
        }
        car_span();
    })
    $(".count_add").click(function () {
        product_account = product_account + 1;
        $(".now_count").text(product_account)
        $(".car_shop span").text(product_account);
        $(".trolley_count span").text(product_account);
        car_span();
    })

})

/*付款多选框背景*/
$(function () {
    $(".pay_right  a").click(function () {
        var checked_input = $(this).children("input");
        if (checked_input.is(':checked')) {
            $(this).css("background-image", "url(images/order_19.jpg)");
        }
        else {
            $(this).css("background-image", "url(images/order_18.jpg)");
        }
    })

})

/****订单信息 tab切换***/
$(document).ready(function (e) {
    var tabs = $(".order_tab a")

    var divs = $("#order_change").children("li")

    for (var i = 0; i < tabs.length; i++) {

        tabs[i].onclick = function () { change(this); }

    }
    function change(obj) {

        for (var i = 0; i < tabs.length; i++) {

            if (tabs[i] == obj) {

                tabs[i].className = "order_click";
                divs[i].style.display = "block";
                $(this)
            }

            else {
                tabs[i].className = "";
                divs[i].style.display = "none";
                $(this).parent(".kemu_member_tab").css("background", "url(images/member_06.jpg) no-repeat 0 0")
            }

        }

    }

})
