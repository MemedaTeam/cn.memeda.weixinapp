using System;
using System.Net;
using System.Text;
using System.IO;
using Framework.Log;

namespace Framework.Common
{
    /// <summary>
    /// 支付平台-请求工具类
    /// </summary>
    /// <history>
    ///     <date>2011-03-23</date>
    ///     <programmer>taoqingxue</programmer>
    /// </history>
    public static class RequestUtil
    {
        /// <summary>
        /// POST UTF8数据到指定URL
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        public static string PostDataToUrl(string data, string url)
        {
            byte[] bytesToPost = Encoding.UTF8.GetBytes(data);

            return RequestUtil.PostDataToUrl(bytesToPost, url);
        }

        /// <summary>
        /// POST UTF8数据到指定URL
        /// </summary>
        /// <param name="data">要post的数据</param>
        /// <param name="url">目标url</param>
        /// <returns>服务器响应</returns>
        public static string PostDataToUrl(byte[] data, string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentLength = data.Length;
            httpWebRequest.Timeout = 180000;
            httpWebRequest.ReadWriteTimeout = 180000;

            Stream requestStream = httpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            string response = string.Empty;
            try
            {
                using (Stream responseStream = httpWebRequest.GetResponse().GetResponseStream())
                {
                    using (StreamReader responseReader = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        response = responseReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("POST操作发生异常：{0}", ex.Message));
                LogHelper.LogException(ex);
                throw ex;
            }

            return response;
        }
    }
}