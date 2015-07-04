$(document).ready(function (e) {
    var request = new Object();
    request = GetRequest();
    var cataID = request["CataID"];
    var merchantsId = request["merchantsId"];
    LoadShopInfomation(merchantsId);
    LoadShopGoodsList(merchantsId,cataID);
    //购物车
    $(".button_add_car").click(function () {
        $(this).css("z-index", "1");
        $(".count_group").css("z-index", "2");
    });
    var product_account = parseInt($(".now_count").text());
    //减少商品
    $(".count_odd").click(function () {
        if (product_account > 1) {
            product_account = product_account - 1;
            $(".now_count").text(product_account)
            $(".car_shop span").text(product_account);
            $(".trolley_count span").text(product_account);
        }
        else {
            product_account = 0;
            $(".now_count").text(product_account);
            $(".car_shop span").text(product_account);
            $(".count_group").css("z-index", "1");
            $(".button_add_car").css("z-index", "2");
        }
        car_span();
    });
    //增加商品
    $(".count_add").click(function () {
        product_account = product_account + 1;
        $(".now_count").text(product_account)
        $(".car_shop span").text(product_account);
        $(".trolley_count span").text(product_account);
        car_span();
    });
    //增加商品到购物车
    $("#btn-add-to-car").click(function () {
        var id = $(this).attr("value");
        var data = AddToCar(id, product_account);
    });
});
/*购物车图标*/
function car_span() {
    var car_span = $(".car_shop span").text();
    if (car_span > 0) {
        $(".car_shop span").show()
    }
    else {
        $(".car_shop span").hide()
    }
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
            if (data != null) {
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
function LoadShopGoodsList(merchantsId,cataID) {
    $.ajax({
        type: "Get",
        url: " http://120.24.228.51:8080/20150623/weixin/product/list/"+cataID+".jhtml",
        data: { merchantsId: merchantsId },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.content != null) {
                var productList = '';
                $.each(data.content, function (i, item) {
                    productList += '<li>';
                    productList += '<a href="#" value="'+item.id+'">';
                    productList += '<img src="' + item.thumbnail + '" class="fruit_img" url="' + data.image + '">';
                    productList += '<p><span class="fr"><i class="weight">' + item.weight + '</i>/' + item.unit + '</span><i>' + item.name + '</i></p>';
                    productList += '<div class="product_price"><img src="/Content/images/car_07.jpg" /><i>￥' + item.price + '</i><span>￥' + item.price + '</span></div>';
                    productList += '</a>';
                    productList += '</li>';
                    if (i == data.content.length - 1) {
                        //加载元素
                        var strShopGoodsListEle = $('#shop-goods-list').html().replace('<div class="clear"></div>', '');
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
                            ShowGoodsDetail($(this).attr("value"));
                        });
                        ClearThirdBorderRight();
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

/*超市第3个块 去除右边线*/
function ClearThirdBorderRight() {
    var shop_product_N = $(".producter_con_shop li a")
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
    $(".product_detail").css({ "top": "20%", "margin-top": -margin_top });
    $("#btn-add-to-car").attr("value",id);
    $(".fixed").show();
    $(".product_detail").show();
}