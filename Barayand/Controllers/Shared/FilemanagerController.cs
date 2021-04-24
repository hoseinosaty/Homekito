using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Services;
using Barayand.Models.IncomeData.FileManager;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;
using MimeMapping;
namespace Barayand.Controllers.Shared
{
    [Route("api/fm")]
    [ApiController]
    public class FilemanagerController : ControllerBase
    {
        UploaderService uploaderService;
        FileLocMapperService FileLocMapper;
        public FilemanagerController()
        {
            uploaderService = new UploaderService();
            FileLocMapper = new FileLocMapperService();
        }
        [HttpPost("uploadUserAvatar")]
        public async Task<ActionResult> UploadBase64UserAvatar([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult( await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "USER",200,200));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }

        [HttpPost("uploadentitylogo")]
        public async Task<ActionResult> UploadBase64ImgEntityLogo([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "ELOGO",500,500));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [HttpPost("uploadslider")]
        public async Task<ActionResult> UploadBase64Slider([FromBody] EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "SLIDER", 1080, 349));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [HttpPost("uploadseoimage")]
        public async Task<ActionResult> UploadBase64SeoImage([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "ESEO",200,200));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [HttpPost("uploadGalleryChild")]
        public async Task<ActionResult> uploadGalleryChildBase64([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "GALLERYCHILD",800,500));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [HttpPost("uploadNoticesImage")]
        public async Task<ActionResult> uploadNoticeImageBase64([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "NOTICEIMAGE",500,500));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [Route("uploadVideo/{loc}")]
        [AcceptVerbs]
        public async Task<ActionResult> uploadVideo([FromForm] IFormFile file,string loc)
        {
            try
            {
                return new JsonResult(await uploaderService.UploadVideo(file, "GALLERYCHILD", loc));
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        [Route("uploadDemoAudio/{loc}")]
        [AcceptVerbs]
        public async Task<ActionResult> uploadAudio([FromForm] IFormFile file, string loc)
        {
            try
            {
                return new JsonResult(await uploaderService.UploadAudio(file, "DEMOAUDIO", loc));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("uploadImageFile/{FOLDER}/{ROOT?}")]
        [AcceptVerbs]
        public async Task<ActionResult> uploadImageFile([FromForm] IFormFile file, string FOLDER, string ROOT = null)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                if (ROOT == null)
                    ROOT = "INDEX";
                return new JsonResult(await uploaderService.UploadImageFile(file, ROOT, FOLDER));

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("uploadArticleAttachment/{loc}")]
        [AcceptVerbs]
        public async Task<ActionResult> articleAttachment([FromForm] IFormFile file, string loc)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                return new JsonResult(await uploaderService.uploadDocument(file, "NOTICEMEDIA", loc));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("uploadProductManual")]
        [AcceptVerbs]
        public async Task<ActionResult> ProductManualFile([FromForm] IFormFile file )
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                return new JsonResult(await uploaderService.uploadDocument(file, "PRODMANUAL", "NONE"));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("uploadDigitalPrdVideo/{loc}")]
        [AcceptVerbs]
        public async Task<ActionResult> uploadDpVideo([FromForm] IFormFile file, string loc)
        {
            try
            {
                return new JsonResult(await uploaderService.UploadVideo(file, "DIGITALPRD", loc));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("uploadNewsVideo/{loc}")]
        [AcceptVerbs]
        public async Task<ActionResult> uploadNewsVideo([FromForm] IFormFile file, string loc)
        {
            try
            {
                return new JsonResult(await uploaderService.UploadVideo(file, "NOTICEMEDIA", loc));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [HttpPost("uploadNoticesGalleryImage")]
        public async Task<ActionResult> uploadNoticeImgChildBase64([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(await uploaderService.UploadBase64(elm.DataUrl, elm.Loc, "NOTICEMEDIA",800,500));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [HttpPost("removeNoticesGalleryImage")]
        public ActionResult removeNoticeImgChild([FromBody]EntityLogoModel elm)
        {
            try
            {
                //AuthRequestService.Auth(HttpContext);
                return new JsonResult(uploaderService.RemoveFile(elm.Loc,"NOTICEMEDIA", elm.DataUrl ));
            }
            catch (Exception ex)
            {
                return new JsonResult(ResponseModel.Error(ex.Message, ex.Data));
            }
        }
        [Route("dl/{flg}/{lid}/{fn?}")]
        public ActionResult download(string flg,string lid, string fn="noimage.jpg")
        {
            try
            {
                string root = FileLocMapper.LocateMediaFile(flg,lid);
                string fpath = root + fn;
                if(!System.IO.File.Exists(fpath))
                {
                    fpath = root + "noimage.jpg";
                }
                byte[] filedata = System.IO.File.ReadAllBytes(fpath);
                string contentType = MimeMapping.MimeUtility.GetMimeMapping(fpath);

                var cd = new System.Net.Mime.ContentDisposition
                {
                    FileName = fn,
                    Inline = true,
                };
              
                Response.Headers.Append("Content-Disposition", cd.ToString());

                return File(filedata, contentType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}