(function () {
    var method = "get";
    var indexPage = function (option) {
        this.paras = option || {};
        this.indexcommuntityUrl = option.hasOwnProperty("indexcommuntityUrl") && option.indexcommuntityUrl || "";
        this.communtityListUrl = option.hasOwnProperty("communtityListUrl") && option.communtityListUrl || "";
        this.merchantsList = option.hasOwnProperty("merchantsList") && option.merchantsList || "";
        this.productlist = option.hasOwnProperty("productlist") && option.productlist || "";
        this.orderList = option.hasOwnProperty("orderList") && option.orderList || "";
    };
    indexPage.prototype = {
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
        getCommunitityList: function () {
            var that = this;
            $.ajax({
                method: method,
                url: that.communtityListUrl,
                data: null,
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
        getAllOrderList: function (paymentStatus, shippingStatus) {
            var that = this;
            $.ajax({
                method: method,
                url: that.orderList,
                data: null,
                success: function (data) {
                    console.log(data);
                    var html = template("productitem", data);
                    parentEle.find("ul").html(html);
                }
            });
        },

        ReisterEvent: function () {
            var that = this;
            if ($("#fruiticon").length > 0) {
                $("#fruiticon").click(function () {
                    that.getProductList(82, $("#fruitlist"));
                });
            }
            if ($("#snacklist").length > 0) {
                $("#snacklist").click(function () {
                    that.getProductList(83, $("#snacklist"));
                });
            }
            if ($("#marketlist").length>0) {
                $("#marketlist").click(function () {
                    that.getProductList(84, $("#marketlist"));
                });
            }
        },
        init: function () {
            this.ReisterEvent();
            this.getIndexCommunity();
        }
    };
    window.IndexPage = indexPage;
})();
