(function () {
    var method = "get";
    var indexPage = function (option) {
        this.paras = option || {};
        this.indexcommuntityUrl = option.hasOwnProperty("indexcommuntityUrl") && option.indexcommuntityUrl || "";
        this.communtityListUrl = option.hasOwnProperty("communtityListUrl") && option.communtityListUrl || "";
        this.merchantsList = option.hasOwnProperty("merchantsList") && option.merchantsList || "";
        this.productlist = option.hasOwnProperty("productlist") && option.productlist || "";
        this.orderList = option.hasOwnProperty("orderList") && option.orderList || "";
        this.orderitemurl = option.hasOwnProperty("orderitemurl") && option.orderitemurl || "";
        this.ShopCarCount = option.hasOwnProperty("ShopCarCount") && option.ShopCarCount || "";
    };
    indexPage.prototype = {
        GetParameter: function (pName) {
            var reg = new RegExp("(^|&)" + pName + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return "";
        },
        getdate: function (tm) {
            return new Date(parseInt(tm) * 1000).toLocaleString().replace(/年|月/g, "-").replace(/日/g, " ");
        },
        getIndexCommunity: function () {
            var that = this;
            $.ajax({
                method: method,
                url: that.indexcommuntityUrl,
                data: null,
                success: function (data) {

                    $("#indexcommuntity a").attr("href", locationUrl + data.id).find("span").val(data.name);
                    that.getmerchantList(data.id);
                }
            });
            var that = this, loc = that.GetParameter("loc"), locationUrl = "/home/location/";
            if (loc > 0) {
                that.getmerchantList(loc);
            }
            that.innerAjax(that.indexcommuntityUrl, {}, function (data) {
                $("#indexcommuntity a").attr("href", locationUrl + data.id).find("span").val(data.name);
                that.getmerchantList(data.id);
            });
        },
        getmerchantList: function (id) {
            var that = this;
            that.innerAjax(that.merchantsList + id, {}, function (data) {
                var html = template("shopitem", data);
                $("#shoplist").html(html);
                $(".saled_order").each(function () {
                    var num = $(this).attr("data-num"), imglist = $(this).find("img");
                    for (var i = 0; i < 5 - num; i++) {
                        $(imglist[i]).remove();
                    }
                });
            });
        },
        getCommunitityList: function (sn) {
            var that = this;
            that.innerAjax(that.communtityListUrl, { "sn": sn }, function (data) {
                var list = { "content": data };
                var html = template("locationitem", list);
                $("#locationlist").html(html);
            });
            //$.ajax({
            //    method: method,
            //    url: that.communtityListUrl,
            //    data: { "sn": sn },
            //    success: function (data) {
            //        var list = { "content": data };
            //        var html = template("locationitem", list);
            //        $("#locationlist").html(html);
            //    }
            //});
        },
        getProductList: function (cataid, parentEle) {
            var that = this;
            that.innerAjax(that.productlist.replace("{cata}", cataid), {}, function (data) {
                console.log(data);
                var html = template("productitem", data);
                parentEle.find("ul").html(html);
            });
        },
        getAllOrderList: function (parentEle, paymentStatus, shippingStatus) {
            var that = this;
            that.innerAjax(that.orderList, { "paymentStatus": paymentStatus, "shippingStatus": shippingStatus, "openid": "123456" }, function (data) {
                data = data || {};
                if ('content' in data && data.content.length > 0) {
                    var html = '';
                    for (var i = 0; i < data.content.length; i++) {
                        var item = data.content[i];
                        html += "<div class=\"order_number\">" +
                            "<div class=\"order_number_con\">" +
                            "<p>订单号：" + item.order.sn + "</p>" +
                            "<span>下单时间：" + item.order.createDate + "</span> " +
                            "<div class=\"pay_block\">" +
                            "<span>" + item.order.orderStatus + "</span> " +
                            "</div>" +
                            "</div>" +
                            "</div>";
                        if ('merchants' in item && item.merchants.length > 0) {
                            html += ' <div class="order_block container">';
                            for (var j = 0; j < item.merchants.length; j++) {
                                var shop = item.merchants[j];
                                html += '<div class="store_mess">' +
                                    '<img src="' + shop.img + '" class="fl"/>' +
                                    '<p class="fl">' + shop.merchantsName + '</p>' +
                                    '<div class="clear"></div>' +
                                    '</div>' +
                                    '<ul class="order_list_ul">';
                                if ('orderItem' in shop && shop.orderItem.length > 0) {
                                    for (var k = 0; k < shop.orderItem.length; k++) {
                                        var orderItem = shop.orderItem[k];
                                        html += '<li>' +
                                            '<img src="' + orderItem.thumbnail + '" class="fl"/>' +
                                            '<div class="order_con fl">' +
                                            '<p>' + orderItem.name + '<i class="fr">￥<font>' + orderItem.price + '</font>/份</i></p>' +
                                            '<span>X<font>' + orderItem.quantity + '</font></span>' +
                                            '<div class="count_order">小计：￥' + orderItem.subtotal + '</div>' +
                                            '</div>' +
                                            '<div class="clear"></div>' +
                                            '</li>';
                                    }
                                }
                                html += '</ul>';
                            }
                            html += '</div>' +
                                '<div class="dotted_line"></div>';
                        }
                        html += '<div class="order_account_bot container">' +
                                '<span class="goods_mount">一共<i>' + ('quantity' in item.order ? item.order.quantity : 3) + '</i>件商品</span>' +
                                '<span>合计：￥' + item.order.amountPaid + '</span>' +
                                '</div>';
                    }

                    parentEle.html(html);
                }
            });
            //$.ajax({
            //    method: method,
            //    url: that.orderList,
            //    data: { "paymentStatus": paymentStatus, "shippingStatus": shippingStatus, "openid": "123456" },
            //    success: function (data) {
            //        data = data || {};
            //        if ('content' in data && data.content.length > 0) {
            //            var html = '';
            //            for (var i = 0; i < data.content.length; i++) {
            //                var item = data.content[i];
            //                html += "<div class=\"order_number\">" +
            //                    "<div class=\"order_number_con\">" +
            //                    "<p>订单号：" + item.order.sn + "</p>" +
            //                    "<span>下单时间：" + item.order.createDate + "</span> " +
            //                    "<div class=\"pay_block\">" +
            //                    "<span>" + item.order.orderStatus + "</span> " +
            //                    "</div>" +
            //                    "</div>" +
            //                    "</div>";
            //                if ('merchants' in item && item.merchants.length > 0) {
            //                    html += ' <div class="order_block container">';
            //                    for (var j = 0; j < item.merchants.length; j++) {
            //                        var shop = item.merchants[j];
            //                        html += '<div class="store_mess">' +
            //                            '<img src="' + shop.img + '" class="fl"/>' +
            //                            '<p class="fl">' + shop.merchantsName + '</p>' +
            //                            '<div class="clear"></div>' +
            //                            '</div>' +
            //                            '<ul class="order_list_ul">';
            //                        if ('orderItem' in shop && shop.orderItem.length > 0) {
            //                            for (var k = 0; k < shop.orderItem.length; k++) {
            //                                var orderItem = shop.orderItem[k];
            //                                html += '<li>' +
            //                                    '<img src="' + orderItem.thumbnail + '" class="fl"/>' +
            //                                    '<div class="order_con fl">' +
            //                                    '<p>' + orderItem.name + '<i class="fr">￥<font>' + orderItem.price + '</font>/份</i></p>' +
            //                                    '<span>X<font>' + orderItem.quantity + '</font></span>' +
            //                                    '<div class="count_order">小计：￥' + orderItem.subtotal + '</div>' +
            //                                    '</div>' +
            //                                    '<div class="clear"></div>' +
            //                                    '</li>';
            //                            }
            //                        }
            //                        html += '</ul>';
            //                    }
            //                    html += '</div>' +
            //                        '<div class="dotted_line"></div>';
            //                }
            //                html += '<div class="order_account_bot container">' +
            //                        '<span class="goods_mount">一共<i>' + ('quantity' in item.order ? item.order.quantity : 3) + '</i>件商品</span>' +
            //                        '<span>合计：￥' + item.order.amountPaid + '</span>' +
            //                        '</div>';
            //            }

            //            parentEle.html(html);
            //        }
            //    }
            //});
        },
        getOrderItemInfo: function () {
            var that = this;
            that.innerAjax(that.orderitemurl, { "sn": this.GetParameter("sn") }, function (data) {
                var list = { "content": data };
                var html = template("orderItems", list);
                $("#sendtime").before(html);
            });
            //$.ajax({
            //    method: method,
            //    url: that.orderitemurl,
            //    data: { "sn": this.GetParameter("sn") },
            //    success: function (data) {
            //        var list = { "content": data };
            //        var html = template("orderItems", list);
            //        $("#sendtime").before(html);
            //    }
            //});
        },
        getShopCartCount: function () {
            this.innerAjax(this.ShopCarCount, {}, function (data) {
                $(".shopcartNumber").text(data.quantity);
            });
        },
        innerAjax: function (url, data, callback) {
            data = data || {};
            data.openid = '123456';
            $.ajax({
                method: method,
                url: url,
                data: data,
                success: function (result) {
                    callback(result);
                }
            });
        },

        RegisterLocation: function () {
            this.getCommunitityList();
            $("#locationlist").on("click", "li", function () {
                location.href = "/home/index?loc=" + $(this).attr("data-id");
            });
        },
        RegisterIndex: function () {
            var that = this, loadfruit = 0, loadsnack = 0, loadmarket = 0;
            $(".product_menu li a").click(function () {
                var attrThis = $(this).attr("tip");
                $(".product_menu li a").removeClass("now_product");
                $(this).addClass("now_product");
                $(attrThis).show().siblings().hide();
            });

            if ($("#fruiticon").length > 0) {
                $("#fruiticon").click(function () {
                    if (loadfruit == 0) {
                        that.getProductList(82, $("#fruitlist"));
                    }
                    loadfruit = 1;
                });
            }
            if ($("#snackicon").length > 0) {
                $("#snackicon").click(function () {
                    if (loadsnack == 0) {
                        that.getProductList(83, $("#snacklist"));
                    }
                    loadsnack = 1;
                });
            }
            if ($("#marketicon").length > 0) {
                $("#marketicon").click(function () {
                    if (loadmarket == 0) {
                        that.getProductList(84, $("#marketlist"));
                    }
                    loadmarket = 1;
                });
            }
            that.getIndexCommunity();
            that.getShopCartCount();
        },
        RegisterOrder: function () {
            var that = this, loadunpaid = 0, loadunderway = 0;
            var tabs = $(".order_tab a"), divs = $("#order_change").children("li");
            for (var p = 0; p < tabs.length; p++) {
                tabs[p].onclick = function () { change(this); };
            }
            function change(obj) {
                for (var i = 0; i < tabs.length; i++) {
                    if (tabs[i] == obj) {
                        tabs[i].className = "order_click";
                        divs[i].style.display = "block";
                    }
                    else {
                        tabs[i].className = "";
                        divs[i].style.display = "none";
                        $(this).parent(".kemu_member_tab").css("background", "url(images/member_06.jpg) no-repeat 0 0");
                    }
                }
            }

            that.getAllOrderList($("#allorder"));

            if ($("#unpaidhref").length > 0) {
                $("#unpaidhref").click(function () {
                    if (loadunpaid == 0) {
                        that.getAllOrderList($("#unpaid"), 'unpaid');
                    }
                    loadunpaid = 1;
                });
            }
            if ($("#underwayhref").length > 0) {
                $("#underwayhref").click(function () {
                    if (loadunderway == 0) {
                        that.getAllOrderList($("#underway"), '', 'shipped');
                    }
                    loadunderway = 1;
                });
            }
        },
        RegisterOrderInfo: function () {
            this.getOrderItemInfo();
        }

    };
    window.IndexPage = indexPage;
})();
