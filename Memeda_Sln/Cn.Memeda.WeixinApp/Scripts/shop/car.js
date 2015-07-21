/*登录和注册*/
var loginErrorEle = $('#car-error-message');
var hasValidCode=false;
$(document).ready(function (e) {
    var phoneEle = $('#car-phone-number');
    //send valid code
    $('#btn-car-valid-code').click(function () {
        LoadStatusShow();
        loginErrorEle.html('');
        var phoneNumber = $('#phone_number').val().trim();
        if (ValidPhone(phoneNumber)) {
            hasValidCode=true;
            seconds = 60;
            $('#btn-car-valid-code').hide();
            $('#car-count-down').show();
            TimerCountDown();
            $('#car-show-valid-code').show();
            //发送验证码
            $.ajax({
                type: "post",
                url: "http://s.memeda.cn/weixin/register/sendSms.jhtml",
                data: { phone: phoneNumber, openid: GetOpenid() },
                dataType: "json",
                jsonp: "jsoncallback",
                crossDomain: true,
                beforeSend: function () { },
                success: function (data) {
                    if (data.type == 'success') {
                    }
                    else { loginErrorEle.html(data.content); }
                    LoadStatusHide();
                },
                error: function () { LoadStatusHide(); },
                complete: function () { }
            });
        }

    });
    //进入支付页面
    $('#btn-go-pay').click(function () {
        LoadStatusShow();
        var totalPrice = parseFloat($('#car-goods-price').html());
        if (totalPrice <= 0)
        {
            loginErrorEle.html("您的购物车是空的！");
            LoadStatusHide();
            return;
        }
        //判断是否发送过验证码
        var code='';
        if (hasValidCode) {
             code = $('#code').val().trim();
            if (code == '') {
                loginErrorEle.html('请输入验证码！');
                LoadStatusHide();
                return;
            }
            var regCode = /^(\d{4})$/;
            if (!regCode.test(code)) {
                loginErrorEle.html('验证码不正确！');
                LoadStatusHide();
                return;
            }
        }
        var name = $('#receiveName').val().trim();
        if (name == '')
        {
            loginErrorEle.html('收货人姓名不能为空！');
            LoadStatusHide();
            return;
        }
        var city = $('#receive-city').val().trim();
        var address = $('#receive-address').val().trim();
        if (city == '' || address=='') {
            loginErrorEle.html('收货人城市和地址不能为空！');
            LoadStatusHide();
            return;
        }
        var phoneNumber = $('#phone_number').val().trim();
        if (ValidPhone(phoneNumber)) {
            $.ajax({
                type: "post",
                url: "http://s.memeda.cn/weixin/member/order/save_receiver.jhtml",
                data: { areaId:792,openid: GetOpenid(),consignee:name,areaName:city,address:address,phone:phoneNumber,code: code,isDefault:true,zipCode:200000},
                dataType: "json",
                jsonp: "jsoncallback",
                crossDomain: true,
                beforeSend: function () { },
                success: function (data) {
                    if (data != null && data.type=='success') {
                        hasValidCode = false;
                        location.href = "/order/info" ;
                    }
                    else { loginErrorEle.html(data.content); }
                    LoadStatusHide();
                },
                error: function () { LoadStatusHide();},
                complete: function () { }
            })
        }
    });
    LoadStatusShow();
    LoadUserInfomation();
    LoadShopCarInformation();
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
        url: "http://s.memeda.cn/weixin/member/order/receiverList.jhtml",
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
                else {
                    hasValidCode = true;
                }
                $('#receive-city').val(data.areaName);
                $('#receive-address').val(data.address);
            }
            else { loginErrorEle.html(data.content); }
                LoadStatusHide();
        },
        error: function () { LoadStatusHide(); },
        complete: function () { }
    });
}
//加载购物车信息
function LoadShopCarInformation() {
    $.ajax({
        type: "get",
        url: "http://s.memeda.cn/weixin/cart/list.jhtml",
        data: { openid:GetOpenid() },
        dataType: "json",
        jsonp: "jsoncallback",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.merchants != null) {
                var carGoodsList = '';
                $.each(data.merchants, function (i, item) {
                    var carObject = item.cartItem[0];

                    carGoodsList += '<div class="trolley_block container" id="index' + carObject.product.merchantsId + '">';
                        carGoodsList += '<div class="trolley_top">';
                        carGoodsList += '<a href="#" class="car-remove-goods" ><img src="/Content/images/shop_28.jpg"></a>';
                            carGoodsList += '<div class="trolley_name fl">';
                            carGoodsList += '<a href="/shop/index?merchantsId=' + carObject.product.merchantsId + '&cataid=' + carObject.product.productCategoryId + '">' + item.merchantsName + '</a>';
                                //carGoodsList += '<p>满￥30起送</p>';
                            carGoodsList += '</div>';
                            carGoodsList += '<span class="fr">￥' + item.subtotal + '</span>';
                            carGoodsList += '<div class="clear"></div>';
                        carGoodsList += '</div>';
                        $.each(item.cartItem, function (j, caritem) {
                        carGoodsList += '<div class="trolley_bottom">';
                        carGoodsList += '<img src="'+caritem.product.image+'" class="fl">';
                            carGoodsList += '<div class="tro_bottom_con">';
                            carGoodsList += '<p>' + caritem.product.fullName + '</p>';
                            carGoodsList += '<span>' + caritem.product.price + '元/' + caritem.product.weight + caritem.product.unit + '</span>';
                            carGoodsList += '<i>' + caritem.product.price + '元</i>';
                            carGoodsList += '</div>';
                            carGoodsList += '<div class="add_trolley">';
                                carGoodsList += '<div class="count_group trolley_group car-trolley-group">';
                                carGoodsList += '<div class="count_odd" value=' + caritem.id + '></div>';
                                carGoodsList += '<span class="now_count" value="' + caritem.product.merchantsId + '">' + caritem.quantity + '</span>';
                                    carGoodsList += '<div class="count_add" value=' + caritem.id + '></div>';
                                carGoodsList += '</div>';
                            carGoodsList += '</div>';
                        carGoodsList += '</div>';
                    });
                    carGoodsList += '</div>';
                    if (i == data.merchants.length - 1)
                    {
                        $('#car-goods-list').html(carGoodsList);
                        $('#car-goods-total').html(data.cart.quantity);
                        $('#car-goods-price').html(data.cart.price);
                        //移除商品
                        //$('.car-remove-goods').click(function () {
                        //    var id = $(this).attr("id");
                        //    $.ajax({
                        //        type: "post",
                        //        url: " http://s.memeda.cn/weixin/cart/delete.jhtml",
                        //        data: { id: id, openid: GetOpenid() },
                        //        dataType: "json",
                        //        crossDomain: true,
                        //        beforeSend: function () { },
                        //        success: function (data) {
                        //            if (data != null && data.message!=null && data.message.type=="success") {
                        //                $('#car-goods-total').html(data.quantity);
                        //                $('#car-goods-price').html(data.effectivePrice);
                        //            }
                        //            else {
                        //                loginErrorEle.html(data.message.content);
                        //            }
                        //        },

                        //        error: function () { },
                        //        complete: function () { }
                        //    });
                        //});

                        //减少商品
                        $(".car-trolley-group>.count_odd").click(function () {
                            var goodsEle = $(this).parent().find(".now_count");
                            var product_account = parseInt(goodsEle.text());
                            var id = $(this).attr("value");
                            if (product_account >= 1) {                                
                                product_account = product_account - 1;
                                EditCarGoodsCount(id, product_account,goodsEle);
                            }
                        });
                        //增加商品
                        $(".car-trolley-group>.count_add").click(function () {
                            var goodsEle = $(this).parent().find(".now_count");
                            var product_account = parseInt(goodsEle.text());
                            var id = $(this).attr("value");
                            product_account = product_account + 1;
                            EditCarGoodsCount(id,product_account,goodsEle);
                        });
                    }
                });
            }
            else { loginErrorEle.html(data.content); }
            LoadStatusHide();
        },
        error: function () { LoadStatusHide(); },
        complete: function () { }
    });
}
//修改购物车商品数量
function EditCarGoodsCount(id,count,goodsEle)
{
    LoadStatusShow();
    $.ajax({
        type: "post",
        url: " http://s.memeda.cn/weixin/cart/edit.jhtml",
        data: { id: id,quantity:count, openid: GetOpenid() },
        dataType: "json",
        crossDomain: true,
        beforeSend: function () { },
        success: function (data) {
            if (data != null && data.message != null && data.message.type == "success") {
                goodsEle.text(count);
                var value = goodsEle.attr("value");
                $.each(data.merchants, function (i, item) {
                    $("#index" + value + " .fr").html("￥" + item.subtotal);
                });
                $('#car-goods-total').html(data.quantity);
                $('#car-goods-price').html(data.subtotal);
            }
            else {
                loginErrorEle.html(data.message.content);
            }
            LoadStatusHide();
        },

        error: function () { LoadStatusHide(); },
        complete: function () { }
    });
}