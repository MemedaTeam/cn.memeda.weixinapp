var errorEle = $('.error-message');
$(document).ready(function () {
    
    var phoneEle=$('#phone-number');
    //send valid code
    $('#btn-sms-valid-code').click(function () {
        errorEle.html('');
        var phoneNumber = phoneEle.val().trim();
        if (ValidPhone(phoneNumber))
        {
            $.ajax({
                type: "POST",
                url: "",
                data: { edit: "queryurl", addtype: addtype, url: encodeURIComponent(url) },
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {
                },
                error: function () { },
                complete: function () { }
            });
        }

    });
    //Login or register
    $('#btn-login').click(function () {
        errorEle.html('');
        var smsCode = $('#sms-valid-code').val().trim();
        var regCode = /^(\d{4})$/;
        if (smsCode == undefined || smsCode == '') {
            errorEle.html('请输入验证码！');
            return ;
        }
        if (!regCode.test(smsCode)) {
            errorEle.html('验证码不正确！');
            return;
        }
        $.ajax({
            type: "POST",
            url: "",
            data: { edit: "queryurl", addtype: addtype, url: encodeURIComponent(url) },
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
            },
            error: function () { },
            complete: function () { }
        });
    });
});
//Valid phone number
function ValidPhone(phoneNumber)
{
    if (phoneNumber == undefined || phoneNumber == '') {
            errorEle.html('请输入手机号码！');
            return false;
        }
        var myreg = /^(((1[0-9]{2}))+\d{8})$/;
        if (phoneNumber.length != 11 || !myreg.test(phoneNumber)) {
            errorEle.html('请输入有效的手机号码！');
            return false;
        }
        return true;
}