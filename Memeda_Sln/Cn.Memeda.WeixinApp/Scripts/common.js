// JavaScript Document
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
/*首页注册框焦点事件*/
$(document).ready(function (e) {
    var username = $("#number");
    username.focus(function () {
        if ($(this).val() == "请输入您手机号码") {
            $(this).val("");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("请输入您手机号码");
        }
    });
});
$(document).ready(function (e) {
    var password = $("#code");
    password.focus(function () {
        if ($(this).val() == "请输入验证码") {
            $(this).val("");
        }
    }).blur(function () {
        if ($(this).val() == "") {
            $(this).val("请输入验证码");
        }
    });
});

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
    $(".product_detail").css({ "top": "50%", "margin-top": -margin_top })

})
$(function () {

    //商品信息弹出层
    $(".producter_con_shop li a").click(function () {
        //改变图片
        var urls = $(this).children(".fruit_img").attr("src");
        var href2 = '<img  src="' + urls + '" />';
        $(".product_picture").html(href2);
        /*改变店铺名*/
        $(".shop_name span").text($(".store_name").text());
        /*商品名称与价格*/
        var product_names = $(this).children("p").children("i").text();
        $(".product_name i").text(product_names);
        var product_pric = $(this).children(".product_price").children("i").text();
        $(".product_name span").text(product_pric);
        /*商品规格*/
        $(".weight span").text($(this).children("p").children("span").children(".weight").text());

        $(".fixed").show();
        $(".product_detail").show();
    })
    $(".close_detail").click(function () {
        $(".fixed").hide();
        $(".product_detail").hide();

    })
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
