(function () {
    var IndexPage = function (option) {
        this.paras = option || {};
        this.indexcommuntityUrl = option.hasOwnProperty("indexcommuntityUrl") && option.indexcommuntityUrl || "";
        this.communtityListUrl = option.hasOwnProperty("communtityListUrl") && option.communtityListUrl || "";
    };
    IndexPage.prototype = {
        that: this,
        getIndexCommunity: function () {
            var that = this;
            $.ajax({
                type: "GET",
                async: false,
                dataType: "jsonp",
                crossDomain: true,
                jsonp: "jsoncallback",
                url: that.indexcommuntityUrl,
                data: null,
                success: function (data) {
                    console.log(data);
                    //$("#indexcommuntity a").attr("href", data.id).find("span").val(data.name);
                }
            });
        },
        getCommunitityList:function() {
            var that = this;
            $.ajax({
                type: "GET",
                async: false,
                dataType: "jsonp",
                crossDomain: true,
                jsonp: "jsoncallback",
                url: that.indexcommuntityUrl,
                data: null,
                success: function (data) {
                    $("#indexcommuntity a").attr("href", data.id).find("span").val(data.name);
                }
            });
        },
        getmerchantList:function() {
            var that = this;
            $.ajax({
                type: "GET",
                async: false,
                dataType: "jsonp",
                crossDomain: true,
                jsonp: "jsoncallback",
                url: that.indexcommuntityUrl,
                data: null,
                success: function (data) {
                    $("#indexcommuntity a").attr("href", data.id).find("span").val(data.name);
                }
            });
        },
        init: function () {
            this.getIndexCommunity();
        }
    };
    window.IndexPage = IndexPage;
})();
