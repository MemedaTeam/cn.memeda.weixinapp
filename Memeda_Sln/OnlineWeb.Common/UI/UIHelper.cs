using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Log;

namespace OnlineWeb.Common.UI
{
    /// <summary>
    /// UI界面层帮助类
    /// </summary>
    public static class UIHelper
    {
        #region 字符串转换

        /// <summary>
        /// 获取URL编码字符串
        /// </summary>
        /// <param name="text">原文</param>
        /// <returns>结果字符串</returns>
        public static string Encode(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return HttpUtility.UrlEncode(text, Encoding.UTF8);
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取URL解码字符串
        /// </summary>
        /// <param name="text">原文</param>
        /// <returns>结果字符串</returns>
        public static string Decode(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return HttpUtility.UrlDecode(text, Encoding.UTF8);
            }

            return string.Empty;
        }


        /// <summary>
        /// 获取格式化日期
        /// </summary>
        /// <param name="obj">日期</param>
        /// <returns>格式化后的日期</returns>
        public static string FormatDate(object obj)
        {
            if (obj == null)
                return "";
            else if (Convert.ToDateTime(obj).ToString("yyyy-MM-dd") == "1900-01-01")
                return "";
            else
                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取格式化日期 ****年**月**日
        /// </summary>
        /// <param name="obj">日期</param>
        /// <returns>格式化后的日期</returns>
        public static string FormatChineseDate(object obj)
        {

            if (obj == null)
                return "";
            else if (Convert.ToDateTime(obj).ToString("yyyy-MM-dd") == "1900-01-01")
                return "";
            else
                return Convert.ToDateTime(obj).GetDateTimeFormats('D')[0].ToString();
        }

        /// <summary>
        /// 将字符串转换为日期类型
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>日期</returns>
        public static DateTime? ParseDate(string str)
        {
            DateTime result;

            if (DateTime.TryParse(str, out result))
            {
                return result;
            }
            else
            {
                return Convert.ToDateTime("1900-1-1");
            }
        }

        /// <summary>
        /// 获取格式化金额
        /// </summary>
        /// <param name="obj">金额</param>
        /// <returns>格式化后的金额</returns>
        public static string FormatMoney(object obj)
        {
            return Convert.ToDecimal(obj).ToString("#,##0.00");
        }

        /// <summary>
        /// 将字符串转换为整数类型
        /// </summary>
        /// <param name="current">字符串</param>
        /// <returns>整数</returns>
        public static int ParseInt(string current)
        {
            int result = 0;

            int.TryParse(current, out result);
            return result;
        }

        /// <summary>
        /// 将字符串转换为整数类型
        /// </summary>
        /// <param name="current">字符串</param>
        /// <returns>整数</returns>
        public static long ParseLong(string current)
        {
            long result = 0;

            long.TryParse(current, out result);
            return result;
        }

        /// <summary>
        /// 将字符串转换为Decimal类型
        /// </summary>
        /// <param name="current">字符串</param>
        /// <returns>整数</returns>
        public static decimal ParseDecimal(string current)
        {
            decimal result = 0;

            decimal.TryParse(current, out result);

            return result;
        }

        /// <summary>
        /// 获取格式化金额
        /// </summary>
        /// <param name="obj">金额</param>
        /// <returns>格式化后的金额</returns>
        public static string FormatMoneyEdit(object obj)
        {
            return Convert.ToDecimal(obj).ToString("0.##");
        }

        /// <summary>
        /// 返回指定长度字符串,如果长度不够,前面补上对应的字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="tag">补上的字符</param>
        /// <returns>结果</returns>
        public static string GetSubString(string str, int len, string tag)
        {
            while (str.Length < len)
            {
                str = tag + str;
            }
            return str;
        }

        /// <summary>
        /// 获取字符串左几位
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>字符串左几位</returns>
        public static string GetLeft(string text, int length)
        {
            if (!string.IsNullOrEmpty(text) && length > 0)
            {
                return text.Substring(0, text.Length < length ? text.Length : length);
            }

            return text;
        }

        /// <summary>
        /// 获取字符串左几位
        /// </summary>
        /// <param name="text">字符串</param>
        /// <param name="length">长度</param>
        /// <returns>字符串左几位</returns>
        public static string GetUnicodeLeft(string text, int length)
        {
            if (string.IsNullOrEmpty(text) || length == 0)
            {
                return string.Empty;
            }

            string resultText = GetEncodeText(text, "GB2312");
            return resultText.Substring(0, resultText.Length < length ? resultText.Length : length);
        }

        /// <summary>
        /// 获取固定长度的Unicode字符串
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <returns>固定长度字符串</returns>
        public static string GetFixedUnicodeString(string text, int fixedLength)
        {
            StringBuilder result = new StringBuilder();
            int currentlength = 0;
            if (string.IsNullOrEmpty(text) || fixedLength == 0)
            {
                return result.ToString();
            }


            char[] charArray = text.ToCharArray();
            foreach (char current in charArray)
            {
                if (current >= 0x4e00 && current <= 0x9fa5)
                {
                    currentlength += 2;
                }
                else
                {
                    currentlength++;
                }

                if (currentlength > fixedLength)
                {
                    return result.ToString();
                }
                else
                {
                    result.Append(current);
                }
            }

            return result.ToString();
        }

        #endregion

        #region 字符串编码 
        /// <summary>
        /// 获取匹配Encode编码的字符串
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <param name="encodeName">Encode编码名称</param>
        /// <returns>结果字符串</returns>
        public static string GetEncodeText(string text, string encodeName)
        {
            if (string.IsNullOrEmpty(text) ||
                string.IsNullOrEmpty(encodeName))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = Encoding.Default.GetBytes(text);
                return Encoding.GetEncoding(encodeName).GetString(bytes);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取匹配Encode编码的ASCII字符串
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <param name="encodeName">Encode编码名称</param>
        /// <returns>结果ASCII字符串</returns>
        public static string GetASCIIText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            try
            {
                byte[] bytes = Encoding.Default.GetBytes(text);
                return Encoding.ASCII.GetString(bytes);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串按指定编码进行base64编码
        /// </summary>
        /// <param name="code_type">字符串的编码</param>
        /// <param name="code">字符串</param>
        /// <returns>base64编码后的字符串</returns>
        public static string EncodeBase64(string code_type, string text)
        {
            byte[] bytes = Encoding.GetEncoding(code_type).GetBytes(text);
            try
            {
                return Convert.ToBase64String(bytes);
            }
            catch (Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        #endregion

        #region Web页面字符串截取

        /// <summary>
        /// 显示指定长度的子字符串（较长的字符串结尾以省略号显示）
        /// </summary>
        /// <param name="currentString"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetSubString(string currentString, int length)
        {
            if (currentString.Length <= length)
            {
                return currentString;
            }
            string newString = currentString.Substring(0, length) + "...";
            return newString;
        }
        #endregion

        #region Web页面控件处理

        /// <summary>
        /// 绑定下拉列表控件(带有请选择)
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="dictionary">绑定的字典值</param>
        public static void BindDropDownListByDictionry(
            DropDownList control,
            Dictionary<string, string> dictionary)
        {
            control.DataSource = dictionary;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
            control.Items.Insert(0, new ListItem("请选择", string.Empty));
        }

        /// <summary>
        /// 绑定下拉列表控件
        /// </summary>
        /// <param name="control"></param>
        /// <param name="dictionary"></param>
        public static void BindDropDownListByDictionaryWithoutChoice(
            DropDownList control,
            Dictionary<string, string> dictionary)
        {
            control.DataSource = dictionary;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
        }

        /// <summary>
        /// 设置下拉列表控件的选中值
        /// </summary>
        /// <param name="control">下拉列表控件</param>
        /// <param name="currentValue">当前值</param>
        public static void SetDropDownListSelectedValue(
            DropDownList control, string currentValue)
        {
            if (string.IsNullOrEmpty(currentValue))
            {
                return;
            }

            if (control == null || control.Items.Count == 0 ||
                control.Items.FindByValue(currentValue) == null)
            {
                return;
            }
            control.SelectedValue = currentValue;
        }

        /// <summary>
        /// 显示lable中固定长度的数据描述，超长部分以ToolTip方式显示
        /// </summary>
        /// <param name="lblDescription"></param>
        /// <param name="currentDescription"></param>
        /// <param name="displayLength"></param>
        public static void DisplayShortedDescription(Label lblDescription,
            string currentDescription, int displayLength)
        {
            if (lblDescription == null ||
                displayLength <= 0 ||
                string.IsNullOrEmpty(currentDescription))
            {
                return;
            }

            lblDescription.ToolTip = currentDescription;
            if (lblDescription.ToolTip.Length > displayLength)
            {
                lblDescription.Text = lblDescription.ToolTip.Substring(0, displayLength) + "…"; ;
            }
            else
            {
                lblDescription.Text = lblDescription.ToolTip;
            }
        }

        /// <summary>
        /// 在Repeater Item中找TextBox值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetTextBoxValueByRepeater(RepeaterItem item, string controlName)
        {
            TextBox textBox = ((TextBox)item.FindControl(controlName));
            if (textBox != null)
            {
                return textBox.Text;
            }

            return string.Empty;

        }

        /// <summary>
        /// 在Repeater Item中找Label值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetLabelValueByRepeater(RepeaterItem item, string controlName)
        {
            Label label = ((Label)item.FindControl(controlName));
            if (label != null)
            {
                return label.Text;
            }

            return string.Empty;
        }

        /// <summary>
        /// 在Repeater Item中找Image的url
        /// </summary>
        /// <param name="item"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetImageUrlByRepeater(RepeaterItem item, string controlName)
        {
            Image image = ((Image)item.FindControl(controlName));
            if (image != null)
            {
                return image.ImageUrl;
            }

            return string.Empty;
        }

        /// <summary>
        /// 获取绑定Repeater Item数据源的字段值
        /// </summary>
        /// <param name="item">RepeaterItem项</param>
        /// <param name="fieldName">字段名</param>
        /// <returns></returns>
        public static string GetRepeaterItemValue(RepeaterItem item, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                return string.Empty;
            }

            object result = DataBinder.Eval(item.DataItem, fieldName);

            if (result != null)
            {
                return result.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// 在Repeater中找Hidden字段值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="controlName"></param>
        /// <returns></returns>
        public static string GetHiddenValueByRepeater(RepeaterItem item, string controlName)
        {
            HiddenField hdControl = (HiddenField)item.FindControl(controlName);
            if (hdControl != null && !string.IsNullOrEmpty(hdControl.Value))
            {
                return hdControl.Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Repeater中FindControl并赋值
        /// </summary>
        /// <param name="gridViewRow"></param>
        /// <param name="controlName"></param>
        /// <param name="controlValue"></param>
        public static void SetControlValueByRepeater(RepeaterItem item,
            string controlName, string controlValue)
        {
            Control currentControl = (Control)item.FindControl(controlName);
            if (currentControl == null)
            {
                return;
            }

            if (currentControl is HiddenField)
            {
                ((HiddenField)currentControl).Value = controlValue;
                return;
            }

            if (currentControl is TextBox)
            {
                ((TextBox)currentControl).Text = controlValue;
                return;
            }

            if (currentControl is Label)
            {
                ((Label)currentControl).Text = controlValue;
                return;
            }
        }

        #endregion

        #region Web页面JS处理函数

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">提示信息</param>
        public static void Alert(Page page, string msg)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        /// <summary>
        /// 显示消息提示对话框并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void AlertRedirect(Page page, string msg, string url)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            builder.AppendFormat("location.href='{0}'", url);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        /// <summary>
        /// 显示消息提示对话框并执行脚本
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">提示信息</param>
        /// <param name="js">脚本</param>
        public static void AlertExecute(Page page, string msg, string js)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            builder.Append(js);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        /// <summary>
        /// 显示消息提示对话框并执行脚本
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="msg">提示信息</param>
        /// <param name="js">脚本</param>
        public static void Reload(Page page)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript'>");
            builder.Append("setTimeout(function(){window.location.reload();},100}");
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        #endregion

        #region 获取IP地址

        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string GetIPAddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            return GetIPAddress(request);
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string GetIPAddress(HttpRequest request)
        {
            string result;
            // 如果使用代理，获取真实IP
            if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] != string.Empty)
            {
                result = request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            if (result == null || result == string.Empty)
            {
                result = request.UserHostAddress;
            }
            if (result == "::1")
            {
                result = "127.0.0.1";
            }

            return result;
        }

        #endregion


        #region 获取Url地址

        /// <summary>
        /// 获取当前Url地址
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUrl()
        {
            try
            {
                return HttpContext.Current.Request.Url.PathAndQuery;
            }
            catch (Exception e)
            {
                LogHelper.LogException(e);
            }

            return "";
        }

        #endregion

    }
}
