
$(document).ready(function (e) {

    var request = new Object();
    request = GetRequest();
    var cataID = request["cataid"];
    var merchantsId = request["merchantsid"];
    LoadShopInfomation(merchantsId);
    LoadShopGoodsList(merchantsId, cataID);
    GetCarGoodsCount();
});
//加载店铺信息
function LoadShopInfomation(merchantsId) {
    $.ajax({
        type: "Get",
        url: " http://s.memeda.cn/weixin/merchants/getMerchantsById.jhtml",
        data: { merchantsId: merchantsId,openid:GetOpenid() },
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
                $('#shop-shopkeeper-image').attr("src",data.image);
            }
            else { loginErrorEle.html(data.content); }
        },
        error: function () { },
        complete: function () { }
    });
}
