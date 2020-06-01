using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace PLMS.BLL
{
    public class LicensePlateOCR
    {
        // 车牌识别
        public static string Get(Bitmap bitmap)
        {
            if (bitmap == null) return null;
            string token = ConfigurationManager.AppSettings["access_token"].ToString();
            string host = "https://aip.baidubce.com/rest/2.0/ocr/v1/license_plate?access_token=" + token;
            Encoding encoding = Encoding.Default;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(host);
            request.Method = "post";
            request.KeepAlive = true;
            // 图片的base64编码            
            string base64 = getBase64FromBitmap(bitmap);
            string str = "image=" + HttpUtility.UrlEncode(base64);
            byte[] buffer = encoding.GetBytes(str);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            Root root = JsonConvert.DeserializeObject<Root>(result);
            if (root.words_result == null) return null;
            return root.words_result.number;
        }

        protected static string getBase64FromBitmap(Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] base64 = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(base64, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(base64);
        }
    }
}
