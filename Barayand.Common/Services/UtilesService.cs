using Barayand.OutModels.Miscellaneous;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Barayand.Common.Services
{
    public class UtilesService
    {
        public static string RandomDigit(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string getFileExt(string contenttype)
        {
            string ext = "Unknown";
            switch (contenttype)
            {
                case "image/jpeg":
                case "image/jpg ":
                    ext = ".jpg";
                    break;
                case "image/png":
                    ext = ".png";
                    break;
            }
            return ext;
        }
       
        public static string GenerateNewRandom()
        {
            Random generator = new Random();
            String r = generator.Next(0, 1000000).ToString("D6");
            if (r.Distinct().Count() == 1)
            {
                r = GenerateNewRandom();
            }
            return r;
        }
        public static string getFileExtByContentType(string raw)
        {
            var split = raw.Split(';');
            var split1 = split[0].Split(':');
            return getFileExt(split1[1]);
        }
        public static DateTime PersianToMiladi(object d)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                var date = d.ToString().Split(new[] { "/" }, StringSplitOptions.None);
                DateTime dt = pc.ToDateTime(int.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]), 0, 0, 0, 0);
                return dt;
            }
            catch(Exception ex)
            {
                return DateTime.Now;
            }
        }
        
        public static string MediaUrls(string identifier)
        {
            //string host = "http://valhalla.barayand.net/";
            //string host = "http://localhost:44369/";
            //string host = "https://valhallaplanet.art/";
            //string host = "http://homekito.barayand.net/";
            string host = "https://homekito.com/";
            switch (identifier)
            {
                case "Root":
                    return host + "api/fm/dl/ELOGO/NONE/";
                case "ProductMainImage":
                    return host+ "api/fm/dl/ELOGO/PRDIMG/";
                case "Content":
                    return host+ "api/fm/dl/ELOGO/CONTENT/";
                case "ProductVideo":
                    return host + "api/fm/dl/DIGITALPRD/NONE/";
                case "ProductCatMainImage":
                    return host + "api/fm/dl/ELOGO/PRDCATIMG/";
                case "Seo":
                    return host + "api/fm/dl/ESEO/PRDCATSEOIMG/";
                case "ProductImageGallery":
                    return host + "api/fm/dl/GALLERYCHILD/PRODUCTGALLERY/";
                case "UserProfile":
                    return host + "api/fm/dl/USER/PROFILE/";
                case "UPLOADUSERPROFILE":
                    return host + "api/fm/uploadUserAvatar";
                case "Index":
                    return host + "api/fm/dl/INDEX/SECTION/";
                case "Favicon":
                    return host + "api/fm/dl/INDEX/NONE/";
                case "NoticeCat":
                    return host + "api/fm/dl/ELOGO/NOTICESCATIMG/";
                case "Notice":
                    return host + "api/fm/dl/NOTICEIMAGE/NEWSIMAGE/";
                case "NoticeMedia":
                    return host + "api/fm/dl/NOTICEMEDIA/NEWSMEDIA/";
                case "ArticleAttachment":
                    return host + "api/fm/dl/NOTICEMEDIA/NONE/";
                case "GalleryCategory":
                    return host + "api/fm/dl/ELOGO/GALLERYCATIMG/";
                case "ImageGallery":
                    return host + "api/fm/dl/GALLERYCHILD/IMAGEGALLERY/";
                case "VideoGalleryCover":
                    return host + "api/fm/dl/GALLERYCHILD/VIDEOCOVERIMAGE/";
                case "VideoGaller":
                    return host + "api/fm/dl/GALLERYCHILD/VIDEOGALLERY/";
                case "ProductDemo":
                    return host + "api/fm/dl/DEMOAUDIO/NONE/";
                case "BrandImage":
                    return host + "api/fm/dl/ELOGO/PRDBRANDIMG/";
                case "Slider":
                    return host + "api/fm/dl/SLIDER/NONE/";
                case "Label":
                    return host + "api/fm/dl/ELOGO/PRDLABEL/";
            }
            return "";
        }


        public static SeoStructure ParseSeoData(string data)
        {
            try
            {
                var parsed = JsonConvert.DeserializeObject<SeoStructure>(data);
                return parsed;
            }
            catch (Exception ex)
            {
                return new SeoStructure() { };
            }
        }
        public static string FormatCurrency(decimal number,string culture = "fr-FR")
        {
            var oldCultur = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            var outVal = number.ToString("C");
            Thread.CurrentThread.CurrentCulture = oldCultur;
            return outVal;
        }
        public static decimal CalculateRate(int a,int b,int c,int d,int e)
        {
            int res = (5 * a + 4 * b + 3 * c + 2 * d + 1 * e);
            if(res == 0)
            {
                return 0;
            }
            return  res/ (a+b+c+d+e);
        }
        

    }
}
