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
    };
    indexPage.prototype = {
        getIndexCommunity: function () {
            var that = this;
            $.ajax({
                method: method,
                url: that.indexcommuntityUrl+"?openId=987654",
                data: null,
                success: function (data) {

                    $("#indexcommuntity a").attr("href", locationUrl + data.id).find("span").val(data.name);
                    that.getmerchantList(data.id);
                }
            });
        },
        getmerchantList: function (id) {
            var that = this;
            $.ajax({
                method: method,
                url: that.merchantsList + id,
                data: null,
                success: function (data) {
                    var html = template("shopitem", data);
                    $("#shoplist").html(html);
                }
            });
        },
        getCommunitityList: function (sn) {
            var that = this;
            $.ajax({
                method: method,
                url: that.communtityListUrl,
                data: { "sn": sn },
                success: function (data) {
                    var list = { "content": data };
                    var html = template("locationitem", list);
                    $("#locationlist").html(html);
                }
            });
        },
        getProductList: function (cataid, parentEle) {
            var that = this;
            $.ajax({
                method: method,
                url: that.productlist.replace("{cata}", cataid),
                data: null,
                success: function (data) {
                    console.log(data);
                    var html = template("productitem", data);
                    parentEle.find("ul").html(html);
                }
            });
        },
        getAllOrderList: function (parentEle, paymentStatus, shippingStatus) {
            var that = this;
            $.ajax({
                method: method,
                url: that.orderList,
                data: { "tokenM": "N193YW5na2U1OQ==", "paymentStatus": paymentStatus, "shippingStatus": shippingStatus },
                success: function (data) {
                    console.log(data);
                    var html = template("orderitem", data.content);
                    parentEle.find("ul").html(html);
                }
            });
        },
        getOrderItemInfo: function () {
            var that = this;
            $.ajax({
                method: method,
                url: that.orderitemurl,
                data: { "sn": this.GetParameter("sn") },
                success: function (data) {
                    var list = { "content": data };
                    var html = template("orderItems", list);
                    $("#sendtime").before(html);
                }
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
            this.getIndexCommunity();
        },
        RegisterOrder: function () {
            var that = this;
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
        },
        RegisterOrderInfo: function () {
            this.getOrderItemInfo();
        },
        GetParameter: function (pName) {
            var reg = new RegExp("(^|&)" + pName + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null)
                return unescape(r[2]);
            return "";
        }
    };
    window.IndexPage = indexPage;
})();
