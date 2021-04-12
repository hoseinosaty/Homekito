using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.Common.Services
{
    public class FileLocMapperService
    {
        private string data;
        private string BaseDirectory;
        public FileLocMapperService()
        {
            BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            data = System.IO.File.ReadAllText(BaseDirectory + "/DefaultSetting.json");
        }
        public string LocateMediaFile(string FireFlag,string FireFolder)
        {
            try
            {
                JToken MainSetting = JObject.Parse(data);
                JToken UploadAddress = JObject.Parse(MainSetting.SelectToken("UploadAddress").ToString());
                string FileLocation = MainSetting.SelectToken("BASEMEDIAROOT").ToString();
                switch(FireFlag)
                {
                    case "ELOGO":
                        FileLocation += MainSetting.SelectToken("BASEENTITYLOGO").ToString();
                        break;
                    case "CONTENT":
                        FileLocation += MainSetting.SelectToken("CONTENT").ToString();
                        break;
                    case "ESEO":
                        FileLocation += MainSetting.SelectToken("BASESEOIMAGE").ToString();
                        break;
                    case "GALLERYCHILD":
                        FileLocation += MainSetting.SelectToken("GALLERY").ToString();
                        break;
                    case "NOTICEIMAGE":
                        FileLocation += MainSetting.SelectToken("NOTICES").ToString();
                        break;
                    case "NOTICEMEDIA":
                        FileLocation += MainSetting.SelectToken("NOTICEMEDIA").ToString();
                        break;
                    case "DIGITALPRD":
                        FileLocation += MainSetting.SelectToken("DIGITALPRD").ToString();
                        break;
                    case "USER":
                        FileLocation += MainSetting.SelectToken("USERROOT").ToString();
                        break;
                    case "INDEX":
                        FileLocation += MainSetting.SelectToken("INDEX").ToString();
                        break;
                    case "DEMOAUDIO":
                        FileLocation += MainSetting.SelectToken("PRODUCTDEMO").ToString();
                        break;
                    case "SLIDER":
                        FileLocation += MainSetting.SelectToken("SLIDER").ToString();
                        break;
                    case "PRODMANUAL":
                        FileLocation += MainSetting.SelectToken("PRODMANUAL").ToString();
                        break;
                }
                
                FileLocation += UploadAddress.SelectToken(FireFolder).ToString();
                
                string uri = BaseDirectory + FileLocation;
                if(!System.IO.Directory.Exists(uri))
                {
                    System.IO.Directory.CreateDirectory(uri);
                }
                return uri;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
       
    }
}
