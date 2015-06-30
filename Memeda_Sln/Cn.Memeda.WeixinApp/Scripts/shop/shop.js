$(document).ready(function (e) {
    LoadShopInfomation(5);
    LoadShopGoodsList(5);
});

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
function LoadShopGoodsList(merchantsId) {
    $.ajax({
        type: "Get",
        url: " http://120.24.228.51:8080/20150623/weixin/product/list/84.jhtml",
        data: { merchantsId: merchantsId },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.content != null) {
                var productList = '';
                $.each(data.content, function (i, item) {
                    productList += '<li>';
                    productList += '<a href="#">';
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