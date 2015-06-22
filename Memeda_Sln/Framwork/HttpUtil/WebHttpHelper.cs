using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framwork.HttpUtil
{
    public class WebHttpHelper
    {
        public static string GetWebImg(string url, bool refresh)
        {
            string DirName = System.Configuration.ConfigurationManager.AppSettings["ImageCache"] + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!System.IO.Directory.Exists(DirName)) System.IO.Directory.CreateDirectory(DirName);
            string FileName = DirName + GetCacheFileName(url);

            if (!System.IO.File.Exists(FileName) || refresh)
            {
                //缓存图片不存在或需要刷新
                System.Net.HttpWebRequest Req;
                System.Net.HttpWebResponse Resp = null;

                System.Net.CookieContainer cc = new System.Net.CookieContainer();

                string contentType = "";
                try
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                    Req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    try
                    {
                        Req.CookieContainer = cc;
                    }
                    catch
                    {

                    }
                    Req.Accept = "*/*";
                    Req.UserAgent = " Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; MAXTHON 2.0)";

                    if (url.ToLower().IndexOf(".woshao.net") != -1)
                    {
                        Req.Referer = "http://woshao.com/article/93ae631c03e511e09ecf000c295b2b8d/";
                    }
                    else if (url.ToLower().IndexOf(".sina.com") == -1 && url.ToLower().IndexOf(".ydstatic.com/") == -1 && url.ToLower().IndexOf("http://126.fm/") == -1 && url.ToLower().IndexOf(".126.net") == -1)
                    {
                        Req.Referer = url;
                    }
                    Req.KeepAlive = true;
                    Req.AllowAutoRedirect = true;
                    Req.Method = "GET";

                    Resp = (System.Net.HttpWebResponse)Req.GetResponse();
                    if (Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        contentType = Resp.ContentType;
                        if (contentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            //的确是图片
                            System.IO.Stream netStream = Resp.GetResponseStream();


                            System.IO.FileStream fs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

                            const int bufferLength = 2048;
                            byte[] contents = new byte[bufferLength];
                            int count = 0;
                            int readBytes = 0;
                            do
                            {
                                readBytes = netStream.Read(contents, 0, bufferLength);
                                if (readBytes > 0)
                                    fs.Write(contents, 0, readBytes);
                                count += readBytes;
                            }
                            while (readBytes != 0);
                            fs.Close();
                            netStream.Close();
                            return "";

                        }
                        else
                        {
                            return "NotImage";
                        }
                    }
                    else
                    {
                        return "ServerError";
                    }
                }
                catch (WebException wex)
                {
                    contentType = wex.Response.ContentType;
                    if (contentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        //的确是图片
                        System.IO.Stream netStream = wex.Response.GetResponseStream();


                        System.IO.FileStream fs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

                        const int bufferLength = 2048;
                        byte[] contents = new byte[bufferLength];
                        int count = 0;
                        int readBytes = 0;
                        do
                        {
                            readBytes = netStream.Read(contents, 0, bufferLength);
                            if (readBytes > 0)
                                fs.Write(contents, 0, readBytes);
                            count += readBytes;
                        }
                        while (readBytes != 0);
                        fs.Close();
                        netStream.Close();
                        return "";

                    }
                    else
                    {
                        return "NotImage";
                    }
                }
                catch
                {
                    return "HandleError";
                }
            }
            else
            {
                //存在缓存图片
                return "";
            }
        }


        /// <summary>
        /// 获取网页内容
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isOK"></param>
        /// <returns></returns>
        public static string GetWebString(string url, out bool isOK)
        {
            isOK = true;
            try
            {
                System.Net.WebClient wc = new WebClient();
                return wc.DownloadString(url);
            }
            catch (Exception)
            {
                isOK = false;
                return "";
            }
        }


        /// <summary>
        /// 返回某个URL的页面内容
        /// </summary>
        /// <param name="url">网站URL，可能会重定向改变，所以需要返回</param>
        /// <param name="encode">页面内容编码格式</param>
        /// <param name="refresh">是否强制刷新</param>
        /// <param name="isOK">是否下载成功</param>
        /// <param name="isSame">是否和缓存数据一致</param>
        /// <param name="isGet">true：GET，false：POST</param>
        /// <param name="postdata">提交的数据</param>
        /// <returns>网页内容</returns>
        public static string GetWebContent(ref string url, System.Text.Encoding encode, bool refresh, out bool isOK, out bool isSame, bool isGet, byte[] postdata)
        {
            //返回的字符串
            string returninfo = "";
            //返回文件的CONTENT-TYPE
            string contentType = "";
            System.Net.HttpWebRequest Req;
            System.Net.HttpWebResponse Resp = null;

            //先设置为空
            isOK = false;
            //默认设置有改变
            isSame = false;

            //处理URL地址（如果是GET方式且有数据，则需要变动URL地址）
            if (isGet && postdata != null && postdata.LongLength > 0L)
            {
                string QueryStr = System.Text.Encoding.UTF8.GetString(postdata);
                if (url.IndexOf("?") != -1)
                {
                    url += QueryStr;
                }
                else
                {
                    url += "?" + QueryStr;
                }
            }

            //判断网页编码格式是否有所明确
            bool encodConfirmed = false;

            //匹配编码用 
            Regex regEncoding = new Regex(@"charset=([\w-]*)\.*?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match encodeM;

            string DirName = System.Configuration.ConfigurationManager.AppSettings["HtmlCache"] + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!System.IO.Directory.Exists(DirName)) System.IO.Directory.CreateDirectory(DirName);
            string FileName = DirName + GetCacheFileName(url);
            string ContentEncoding = "";
            System.IO.FileStream fs;
            if (System.IO.File.Exists(FileName) && !refresh)
            {
                //OK
                isOK = true;
            }
            else if (!System.IO.File.Exists(FileName) || refresh)
            {
                string OrgFileMD5 = "";

                if (System.IO.File.Exists(FileName))
                {
                    OrgFileMD5 = FileInfo.GetFileMD5(FileName);
                }

                #region 下载文件
                try
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                    Req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    try
                    {
                        System.Net.CookieContainer cc = new System.Net.CookieContainer();
                        try
                        {
                            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Session != null)
                            {
                                cc = (System.Net.CookieContainer)System.Web.HttpContext.Current.Session["HttpCookieContainer"];
                                Req.CookieContainer = cc;
                            }

                        }
                        catch
                        {
                        }
                    }
                    catch
                    {

                    }

                    Req.Accept = "*/*";
                    //Req.Headers.Add("Accept-Encoding", "gzip, deflate");
                    Req.Headers.Add("Accept-Language", "zh-CN");
                    Req.Headers.Add("Cache-Control", "no-cache");
                    Req.AutomaticDecompression = System.Net.DecompressionMethods.GZip |
                                                 System.Net.DecompressionMethods.Deflate;
                    Req.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1)";
                    Req.KeepAlive = true;

                    Req.AllowAutoRedirect = true;
                    if (isGet)
                    {
                        Req.Method = "GET";
                    }
                    else
                    {
                        Req.Method = "POST";
                        Req.ContentLength = postdata.LongLength;
                        System.IO.Stream postStream = Req.GetRequestStream();
                        postStream.Write(postdata, 0, postdata.Length);
                        postStream.Close();
                    }
                    Resp = (System.Net.HttpWebResponse)Req.GetResponse();
                    ContentEncoding = Resp.ContentEncoding.ToLower();
                    if (Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        contentType = Resp.ContentType;
                        if (contentType.Length > 4 && contentType.Substring(0, 4) != "text")
                        {
                            //System.Web.HttpContext.Current.Response.Redirect(url, true);
                            //else
                            //{
                            //    returninfo = "文件格式不正确,现在的格式为" + contentType;
                            //    return returninfo;
                            //}
                        }
                        url = Resp.ResponseUri.ToString();
                        //先判断CONTENTTYPE头部信息
                        encodeM = regEncoding.Match(contentType);
                        if (encodeM.Success)
                        {
                            //如果与初始化的ENCODE不同，则重设ENCODE
                            System.Text.Encoding getEnc;
                            try
                            {
                                //有些网站编码是错的，比如niwota.com
                                getEnc = System.Text.Encoding.GetEncoding(encodeM.Groups[1].Value);
                                if (getEnc != encode)
                                {
                                    encode = getEnc;
                                }
                                encodConfirmed = true;
                            }
                            catch
                            {
                            }
                        }

                        System.IO.Stream netStream = Resp.GetResponseStream();
                        if (ContentEncoding == "gzip" || ContentEncoding == "deflate")
                        {
                            fs = new System.IO.FileStream(FileName + "_ZIP", System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                        }
                        else
                        {
                            fs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                        }
                        const int bufferLength = 2048;
                        byte[] contents = new byte[bufferLength];
                        int count = 0;
                        int readBytes = 0;
                        do
                        {
                            readBytes = netStream.Read(contents, 0, bufferLength);
                            if (readBytes > 0)
                                fs.Write(contents, 0, readBytes);
                            count += readBytes;
                        }
                        while (readBytes != 0);
                        fs.Close();
                        netStream.Close();

                        //网页下载完成
                        isOK = true;
                        //将缓存文件删掉
                        //System.IO.File.Delete(FileName);

                        //有可能是GZIP或DEFLATE压缩过的网页
                        if (ContentEncoding == "gzip")
                        {
                            FileZip.DecompressFile(FileName + "_ZIP", FileName);
                        }
                        else if (ContentEncoding == "deflate")
                        {
                            FileZip.DecompressFileDeflate(FileName + "_ZIP", FileName);
                        }

                        string NewFileMD5 = FileInfo.GetFileMD5(FileName);
                        if (OrgFileMD5 != "" && OrgFileMD5 == NewFileMD5)
                        {
                            //文件新老内容一致
                            isSame = true;
                        }

                    }
                    else
                    {
                        returninfo = "网站返回状态信息：" + Resp.StatusCode.ToString();
                    }
                }
                catch (System.Net.WebException ex)
                {
                    returninfo = "目标网站错误，可能是网页不存在或解析网址";
                }
                catch (Exception ex)
                {
                    returninfo = ex.Message.ToString();
                }
                finally
                {
                    //关闭读取的资源
                    //if (netStream != null)netStream.Close()
                    if (Resp != null) Resp.Close();
                }
                #endregion

            }

            try
            {
                if (System.IO.File.Exists(FileName))
                {
                    //将内容信息读入数组
                    fs = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    byte[] rawtext = new byte[fs.Length];
                    fs.Read(rawtext, 0, (int)fs.Length);
                    fs.Close();

                    if (UTF8_Probability(rawtext) > 0)
                    {
                        encode = System.Text.Encoding.UTF8;
                        encodConfirmed = true;
                    }

                    if (!encodConfirmed)
                    {
                        returninfo = System.Text.Encoding.Default.GetString(rawtext);

                        //判断网页内信息
                        if (returninfo.StartsWith("<?xml ") || (contentType != "" && contentType.ToLower().IndexOf("/xml") > 0))
                        {
                            Regex regE2 = new Regex(@"\s*<\?xml [^>]*encoding=""([\w-]*)""[^>]*?\?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                            encodeM = regE2.Match(returninfo);
                            if (encodeM.Success)
                            {
                                try
                                {
                                    System.Text.Encoding getEnc = System.Text.Encoding.GetEncoding(encodeM.Groups[1].Value);
                                    if (getEnc != encode)
                                    {
                                        encode = getEnc;
                                    }
                                }
                                catch
                                {

                                }
                            }
                        }
                        else
                        {
                            Regex regE3 = new Regex(@"<meta[^>]*?charset=([\w-]*)[^>]*?>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                            encodeM = regE3.Match(returninfo);
                            if (encodeM.Success)
                            {
                                try
                                {
                                    System.Text.Encoding getEnc = System.Text.Encoding.GetEncoding(encodeM.Groups[1].Value);
                                    if (getEnc != encode)
                                    {
                                        encode = getEnc;
                                    }
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                encode = System.Text.Encoding.Default;
                            }

                        }
                    }

                    returninfo = encode.GetString(rawtext);
                }
            }
            catch
            {

            }

            return returninfo;
        }


        /// <summary>
        /// 获取BASE64编码的URL
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public static string GetBase64URL(string url)
        {
            return System.Web.HttpUtility.UrlEncode(EncodeBase64(url));
        }

        public static string WebContentHandle(string w)
        {
            return "";
        }


        /// <summary>
        /// 检测是否是UTF-8
        /// </summary>
        /// <param name="rawtext">内容</param>
        /// <returns></returns>
        public static int UTF8_Probability(byte[] rawtext)
        {
            int score = 0;
            int i, rawtextlen = 0;
            int goodbytes = 0, asciibytes = 0;

            // Maybe also use UTF8 Byte Order Mark:  EF BB BF

            // Check to see if characters fit into acceptable ranges
            rawtextlen = rawtext.Length;
            for (i = 0; i < rawtextlen; i++)
            {
                if ((rawtext[i] & (byte)0x7F) == rawtext[i])
                {  // One byte
                    asciibytes++;
                    // Ignore ASCII, can throw off count
                }
                else if (i < rawtextlen - 2)
                {
                    int m_rawInt0 = Convert.ToInt16(rawtext[i]);
                    int m_rawInt1 = Convert.ToInt16(rawtext[i + 1]);
                    int m_rawInt2 = Convert.ToInt16(rawtext[i + 2]);

                    if (256 - 64 <= m_rawInt0 && m_rawInt0 <= 256 - 33 && // Two bytes
                     i + 1 < rawtextlen &&
                     256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65)
                    {
                        goodbytes += 2;
                        i++;
                    }
                    else if (256 - 32 <= m_rawInt0 && m_rawInt0 <= 256 - 17 && // Three bytes
                     i + 2 < rawtextlen &&
                     256 - 128 <= m_rawInt1 && m_rawInt1 <= 256 - 65 &&
                     256 - 128 <= m_rawInt2 && m_rawInt2 <= 256 - 65)
                    {
                        goodbytes += 3;
                        i += 2;
                    }
                }
            }
            if (asciibytes == rawtextlen) { return 0; }

            score = (int)(100 * ((float)goodbytes / (float)(rawtextlen - asciibytes)));

            // If not above 98, reduce to zero to prevent coincidental matches
            // Allows for some (few) bad formed sequences
            if (score > 98)
            {
                return score;
            }
            else if (score > 95 && goodbytes > 30)
            {
                return score;
            }
            else
            {
                return 0;
            }

        }

        /// <summary>
        /// 编码URL地址
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>编码后文件名</returns>
        public static string GetCacheFileName(string url)
        {
            return System.Web.HttpUtility.UrlEncode(WebHttpHelper.EncodeBase64(Encrypt.EncryptMD5(url)));
        }

        /// <summary>
        /// 获取URL里文件名称部分
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>文件名</returns>
        public static string GetUrlFileName(string url)
        {
            int startPos = url.LastIndexOf("/");
            if (startPos > -1)
            {
                return url.Substring(startPos + 1, url.Length - startPos - 1);
            }
            else
            {
                return url;
            }
        }

        public static string GetUrlNoFileName(string url)
        {
            int startPos = url.LastIndexOf("/");
            if (startPos > -1)
            {
                return url.Substring(0, startPos + 1);
            }
            else
            {
                return url;
            }
        }
        /// <summary>
        /// 获取URL里文件扩展名部分
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>文件扩展名</returns>
        public static string GetUrlFileExt(string url)
        {
            int startPos = url.LastIndexOf(".");
            if (startPos > -1)
            {
                string ext = url.Substring(startPos + 1, url.Length - startPos - 1);
                //可能原本就没有扩展名
                if (ext.IndexOf("/") != -1)
                {
                    return "";
                }
                else
                {
                    return ext;
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 网页是否是纯文本
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <returns>文件扩展名</returns>
        public static bool isPlainTxt(string url)
        {
            if (GetUrlFileExt(url).ToLower() == "txt")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断URL地址是否有效
        /// </summary>
        /// <param name="url">URL字符串</param>
        /// <returns></returns>
        public static bool isUrlValid(string url)
        {
            try
            {
                Uri u = new Uri(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetCurrentHost()
        {
            if (System.Web.HttpContext.Current.Request.Url.Port != 80)
            {
                return System.Web.HttpContext.Current.Request.Url.Host + ":" + System.Web.HttpContext.Current.Request.Url.Port;
            }
            else
            {
                return System.Web.HttpContext.Current.Request.Url.Host;
            }
        }

        /// <summary>
        /// 编码URL为BASE64
        /// </summary>
        /// <param name="code">编码的URL</param>
        /// <returns>BASE64编码后的页面</returns>
        public static string EncodeBase64(string code)
        {
            try
            {
                string encode = "";
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(code);
                //由于在本地保存缓存文件的时候文件名不能超过255，而有些URL地址很大，所以需要进行反转操作
                //Array.Reverse(bytes);
                try
                {
                    encode = Convert.ToBase64String(bytes);
                }
                catch
                {
                    encode = code;
                }
                return encode;
            }
            catch
            {
                return "";
            }

        }
        /// <summary>
        /// 解码BASE64的URL
        /// </summary>
        /// <param name="code">BASE64字串</param>
        /// <returns>解码后的URL</returns>
        public static string DecodeBase64(string code)
        {
            string decode = "";
            //由于在本地保存缓存文件的时候文件名不能超过255，而有些URL地址很大，所以需要进行反转操作
            //Array.Reverse(bytes);
            try
            {
                byte[] bytes = Convert.FromBase64String(code);
                decode = System.Text.Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        public static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
        { // Always accept 
            return true;
        }


        public static byte[] GetWebImgBytes(string url, bool refresh)
        {
            byte[] bytes;

            string DirName = System.Configuration.ConfigurationManager.AppSettings["ImageCache"] + DateTime.Now.ToString("yyyyMMdd") + "\\";
            if (!System.IO.Directory.Exists(DirName)) System.IO.Directory.CreateDirectory(DirName);
            string FileName = DirName + GetCacheFileName(url);

            if (!System.IO.File.Exists(FileName) || refresh)
            {
                //缓存图片不存在或需要刷新
                System.Net.HttpWebRequest Req;
                System.Net.HttpWebResponse Resp = null;

                System.Net.CookieContainer cc = new System.Net.CookieContainer();

                string contentType = "";
                try
                {
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                    Req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    try
                    {
                        Req.CookieContainer = cc;
                    }
                    catch
                    {

                    }
                    Req.Accept = "*/*";
                    Req.UserAgent = " Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; MAXTHON 2.0)";
                    if (url.ToLower().IndexOf(".sina.com.") == -1)
                    {
                        Req.Referer = url;
                    }
                    Req.KeepAlive = true;
                    Req.AllowAutoRedirect = true;
                    Req.Method = "GET";

                    Resp = (System.Net.HttpWebResponse)Req.GetResponse();
                    if (Resp.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        contentType = Resp.ContentType;
                        if (contentType.IndexOf("image", StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            //的确是图片
                            System.IO.Stream netStream = Resp.GetResponseStream();


                            System.IO.FileStream fs = new System.IO.FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);

                            const int bufferLength = 2048;
                            byte[] contents = new byte[bufferLength];
                            int count = 0;
                            int readBytes = 0;
                            do
                            {
                                readBytes = netStream.Read(contents, 0, bufferLength);
                                if (readBytes > 0)
                                    fs.Write(contents, 0, readBytes);
                                count += readBytes;
                            }
                            while (readBytes != 0);
                            fs.Close();
                            netStream.Close();
                            //return "";

                            System.IO.FileStream fsr = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                            bytes = new byte[(int)fsr.Length];
                            fsr.Read(bytes, 0, (int)fsr.Length);
                            fsr.Close();
                        }
                        else
                        {
                            //return "NotImage";
                            bytes = new byte[0];
                        }
                    }
                    else
                    {
                        //return "ServerError";
                        bytes = new byte[0];
                    }
                }
                catch
                {

                    //return "HandleError";
                    bytes = new byte[0];
                }
            }
            else
            {
                //存在缓存图片
                System.IO.FileStream fsr = new System.IO.FileStream(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
                bytes = new byte[(int)fsr.Length];
                fsr.Read(bytes, 0, (int)fsr.Length);
                fsr.Close();
            }

            return bytes;
        }
    }
}
