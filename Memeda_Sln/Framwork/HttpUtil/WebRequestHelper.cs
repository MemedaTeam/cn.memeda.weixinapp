using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Web;
using Framework.Logs;

namespace Framework.Web
{
    public static class WebRequestHelper
    {
        /// <summary>
        /// ���ذ�ȫ��Request.Paramsֵ
        /// </summary>
        /// <param name="ParaName">����ֵ</param>
        /// <returns>���</returns>
        public static string GetSafetyParams(string ParaName)
        {
            if (string.IsNullOrEmpty(ParaName))
            {
                return string.Empty;
            }

            try
            {
                if (HttpContext.Current.Request == null)
                {
                    return string.Empty;
                }

                string paramValue = HttpContext.Current.Request.Params[ParaName];
                if (string.IsNullOrEmpty(paramValue))
                {
                    return string.Empty;
                }

                return paramValue;

            }
            catch (System.Exception ex)
            {
                LogHelper.LogException(ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// ��ȡ��̬ͼƬ��JS��Դ������
        /// </summary>
        /// <param name="url">��ǰ��ԴUrl��ַ</param>
        /// <returns>��Դ�ļ�������</returns>
        public static void WriteStreamResponse(string url, string proxyAddress)
        {
            //currentResponse.Clear();
            string resourceContent = string.Empty;
            try
            {
                HttpWebRequest httpWebRequest;
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                //httpWebRequest.UseDefaultCredentials = true;

                WebProxy proxy = new WebProxy(proxyAddress);
                proxy.UseDefaultCredentials = true;
                //proxy.BypassProxyOnLocal = BypassProxyOnLocal;
                httpWebRequest.Proxy = proxy;

                HttpWebResponse httpWebResponse;
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                Stream responseStream = httpWebResponse.GetResponseStream();

                //using (BufferedStream reader = new BufferedStream(responseStream))
                //{
                //    //��HttpWebResponse��ȡ�Ļ������ļ���д��HttpResponse��Ӧ��
                //    const int bufferSize = 1024 * 100;
                //    byte[] buffer = new byte[bufferSize];

                //    int totalSize = 0;
                //    int count = reader.Read(buffer, 0, bufferSize);
                //    while (count > 0)
                //    {
                //        totalSize += count;
                //        currentResponse.OutputStream.Write(buffer, 0, count);
                //        count = reader.Read(buffer, 0, bufferSize);

                //    }

                //    currentResponse.ContentEncoding = Encoding.Default;
                //    currentResponse.ContentType = httpWebResponse.ContentType;
                //    currentResponse.AppendHeader("Content-Length", totalSize.ToString());
                //    currentResponse.AppendHeader("Last-Modified", httpWebResponse.Headers["Last-Modified"]);
                //    currentResponse.StatusCode = (int)httpWebResponse.StatusCode;
                //    currentResponse.StatusDescription = httpWebResponse.StatusDescription;
                //}

                responseStream.Close();
            }
            catch (Exception ex)
            {
                //currentResponse.StatusCode = (int)HttpStatusCode.NotFound;
                throw ex;
            }
        }

        /// <summary>
        /// POST UTF8���ݵ�ָ��URL
        /// </summary>
        /// <param name="data">Ҫpost������</param>
        /// <param name="url">Ŀ��url</param>
        /// <returns>��������Ӧ</returns>
        public static string PostDataToUrl(string data, string url)
        {
            byte[] bytesToPost = Encoding.UTF8.GetBytes(data);

            return WebRequestHelper.PostDataToUrl(bytesToPost, url);
        }

        /// <summary>
        /// POST UTF8���ݵ�ָ��URL
        /// </summary>
        /// <param name="data">Ҫpost������</param>
        /// <param name="url">Ŀ��url</param>
        /// <returns>��������Ӧ</returns>
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
                Console.WriteLine(string.Format("POST���������쳣��{0}", ex.Message));
                // LogHelper.LogException(ex);
                throw ex;
            }
            return response;
        }
    }
}
