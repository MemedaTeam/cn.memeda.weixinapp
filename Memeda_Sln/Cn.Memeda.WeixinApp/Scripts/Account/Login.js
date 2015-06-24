var errorEle = $('.error-message');
$(document).ready(function () {
    
    var phoneEle = $('#phone-number');
    var smscode = '';
    //send valid code
    $('#btn-sms-valid-code').click(function () {
        errorEle.html('');
        smscode = '';
        var phoneNumber = phoneEle.val().trim();
        if (ValidPhone(phoneNumber))
        {
            //验证手机号码是否已经注册
            $.ajax({
                type: "post",
                url: "http://120.24.228.51:8080/20150623/weixin/register/check_username.jhtml",
                data: { username: phoneNumber },
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {
                    if (data.type == 'success') {
                        //发送验证码
                        $.ajax({
                            type: "post",
                            url: "http://120.24.228.51:8080/20150623/weixin/register/sendSms.jhtml",
                            data: { phone: phoneNumber },
                            dataType: "json",
                            beforeSend: function () { },
                            success: function (data) {
                                if (data.type == 'success') {
                                    //发送验证码
                                    $('#sms-valid-code').val(data.content);
                                    smscode = data.content;
                                }
                                else {
                                    errorEle.html(data.content);
                                }
                            },
                            error: function () { },
                            complete: function () { }
                        });
                    }
                    else {
                        errorEle.html(data.content);
                    }
                },
                error: function () { },
                complete: function () { }
            });
        }

    });
    //Login or register
    $('#btn-login').click(function () {
        errorEle.html('');
        var currentSmsCode = $('#sms-valid-code').val().trim();
        var regCode = /^(\d{4})$/;
        if (smsCode == undefined || smsCode == '') {
            errorEle.html('请输入验证码！');
            return ;
        }//!regCode.test(smsCode) &&
        if (currentSmsCode != smscode) {
            errorEle.html('验证码不正确！');
            return;
        }
        $.ajax({
            type: "POST",
            url: " http://120.24.228.51:8080/20150623/weixin/register/validationSms.jhtml",
            data: { code: smsCode, phone: phoneEle.val() },
            dataType: "json",
            beforeSend: function () { },
            success: function (data) {
                if (data.type == 'success')
                {
                    local
                }
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