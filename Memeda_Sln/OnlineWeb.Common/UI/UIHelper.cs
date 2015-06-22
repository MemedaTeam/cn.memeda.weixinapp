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
    /// UI����������
    /// </summary>
    public static class UIHelper
    {
        #region �ַ���ת��

        /// <summary>
        /// ��ȡURL�����ַ���
        /// </summary>
        /// <param name="text">ԭ��</param>
        /// <returns>����ַ���</returns>
        public static string Encode(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return HttpUtility.UrlEncode(text, Encoding.UTF8);
            }

            return string.Empty;
        }

        /// <summary>
        /// ��ȡURL�����ַ���
        /// </summary>
        /// <param name="text">ԭ��</param>
        /// <returns>����ַ���</returns>
        public static string Decode(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                return HttpUtility.UrlDecode(text, Encoding.UTF8);
            }

            return string.Empty;
        }


        /// <summary>
        /// ��ȡ��ʽ������
        /// </summary>
        /// <param name="obj">����</param>
        /// <returns>��ʽ���������</returns>
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
        /// ��ȡ��ʽ������ ****��**��**��
        /// </summary>
        /// <param name="obj">����</param>
        /// <returns>��ʽ���������</returns>
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
        /// ���ַ���ת��Ϊ��������
        /// </summary>
        /// <param name="str">�ַ���</param>
        /// <returns>����</returns>
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
        /// ��ȡ��ʽ�����
        /// </summary>
        /// <param name="obj">���</param>
        /// <returns>��ʽ����Ľ��</returns>
        public static string FormatMoney(object obj)
        {
            return Convert.ToDecimal(obj).ToString("#,##0.00");
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��������
        /// </summary>
        /// <param name="current">�ַ���</param>
        /// <returns>����</returns>
        public static int ParseInt(string current)
        {
            int result = 0;

            int.TryParse(current, out result);
            return result;
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��������
        /// </summary>
        /// <param name="current">�ַ���</param>
        /// <returns>����</returns>
        public static long ParseLong(string current)
        {
            long result = 0;

            long.TryParse(current, out result);
            return result;
        }

        /// <summary>
        /// ���ַ���ת��ΪDecimal����
        /// </summary>
        /// <param name="current">�ַ���</param>
        /// <returns>����</returns>
        public static decimal ParseDecimal(string current)
        {
            decimal result = 0;

            decimal.TryParse(current, out result);

            return result;
        }

        /// <summary>
        /// ��ȡ��ʽ�����
        /// </summary>
        /// <param name="obj">���</param>
        /// <returns>��ʽ����Ľ��</returns>
        public static string FormatMoneyEdit(object obj)
        {
            return Convert.ToDecimal(obj).ToString("0.##");
        }

        /// <summary>
        /// ����ָ�������ַ���,������Ȳ���,ǰ�油�϶�Ӧ���ַ�
        /// </summary>
        /// <param name="str"></param>
        /// <param name="len"></param>
        /// <param name="tag">���ϵ��ַ�</param>
        /// <returns>���</returns>
        public static string GetSubString(string str, int len, string tag)
        {
            while (str.Length < len)
            {
                str = tag + str;
            }
            return str;
        }

        /// <summary>
        /// ��ȡ�ַ�����λ
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <param name="length">����</param>
        /// <returns>�ַ�����λ</returns>
        public static string GetLeft(string text, int length)
        {
            if (!string.IsNullOrEmpty(text) && length > 0)
            {
                return text.Substring(0, text.Length < length ? text.Length : length);
            }

            return text;
        }

        /// <summary>
        /// ��ȡ�ַ�����λ
        /// </summary>
        /// <param name="text">�ַ���</param>
        /// <param name="length">����</param>
        /// <returns>�ַ�����λ</returns>
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
        /// ��ȡ�̶����ȵ�Unicode�ַ���
        /// </summary>
        /// <param name="text">Դ�ַ���</param>
        /// <returns>�̶������ַ���</returns>
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

        #region �ַ������� 
        /// <summary>
        /// ��ȡƥ��Encode������ַ���
        /// </summary>
        /// <param name="text">Դ�ַ���</param>
        /// <param name="encodeName">Encode��������</param>
        /// <returns>����ַ���</returns>
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
        /// ��ȡƥ��Encode�����ASCII�ַ���
        /// </summary>
        /// <param name="text">Դ�ַ���</param>
        /// <param name="encodeName">Encode��������</param>
        /// <returns>���ASCII�ַ���</returns>
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
        /// �ַ�����ָ���������base64����
        /// </summary>
        /// <param name="code_type">�ַ����ı���</param>
        /// <param name="code">�ַ���</param>
        /// <returns>base64�������ַ���</returns>
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

        #region Webҳ���ַ�����ȡ

        /// <summary>
        /// ��ʾָ�����ȵ����ַ������ϳ����ַ�����β��ʡ�Ժ���ʾ��
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

        #region Webҳ��ؼ�����

        /// <summary>
        /// �������б�ؼ�(������ѡ��)
        /// </summary>
        /// <param name="control">�����б�ؼ�</param>
        /// <param name="dictionary">�󶨵��ֵ�ֵ</param>
        public static void BindDropDownListByDictionry(
            DropDownList control,
            Dictionary<string, string> dictionary)
        {
            control.DataSource = dictionary;
            control.DataTextField = "Value";
            control.DataValueField = "Key";
            control.DataBind();
            control.Items.Insert(0, new ListItem("��ѡ��", string.Empty));
        }

        /// <summary>
        /// �������б�ؼ�
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
        /// ���������б�ؼ���ѡ��ֵ
        /// </summary>
        /// <param name="control">�����б�ؼ�</param>
        /// <param name="currentValue">��ǰֵ</param>
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
        /// ��ʾlable�й̶����ȵ���������������������ToolTip��ʽ��ʾ
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
                lblDescription.Text = lblDescription.ToolTip.Substring(0, displayLength) + "��"; ;
            }
            else
            {
                lblDescription.Text = lblDescription.ToolTip;
            }
        }

        /// <summary>
        /// ��Repeater Item����TextBoxֵ
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
        /// ��Repeater Item����Labelֵ
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
        /// ��Repeater Item����Image��url
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
        /// ��ȡ��Repeater Item����Դ���ֶ�ֵ
        /// </summary>
        /// <param name="item">RepeaterItem��</param>
        /// <param name="fieldName">�ֶ���</param>
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
        /// ��Repeater����Hidden�ֶ�ֵ
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
        /// Repeater��FindControl����ֵ
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

        #region Webҳ��JS������

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի���
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        public static void Alert(Page page, string msg)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript' defer>");
            builder.AppendFormat("alert('{0}');", msg);
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        /// <summary>
        /// ��ʾ��Ϣ��ʾ�Ի��򲢽���ҳ����ת
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="url">��ת��Ŀ��URL</param>
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
        /// ��ʾ��Ϣ��ʾ�Ի���ִ�нű�
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="js">�ű�</param>
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
        /// ��ʾ��Ϣ��ʾ�Ի���ִ�нű�
        /// </summary>
        /// <param name="page">��ǰҳ��</param>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="js">�ű�</param>
        public static void Reload(Page page)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript'>");
            builder.Append("setTimeout(function(){window.location.reload();},100}");
            builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", builder.ToString());
        }

        #endregion

        #region ��ȡIP��ַ

        /// <summary>
        /// ��ȡIP��ַ
        /// </summary>
        public static string GetIPAddress()
        {
            HttpRequest request = HttpContext.Current.Request;
            return GetIPAddress(request);
        }

        /// <summary>
        /// ��ȡIP��ַ
        /// </summary>
        public static string GetIPAddress(HttpRequest request)
        {
            string result;
            // ���ʹ�ô�����ȡ��ʵIP
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


        #region ��ȡUrl��ַ

        /// <summary>
        /// ��ȡ��ǰUrl��ַ
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
