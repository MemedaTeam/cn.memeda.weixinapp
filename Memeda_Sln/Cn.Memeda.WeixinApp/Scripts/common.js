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
