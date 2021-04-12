using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Barayand.Common.Services
{
    public class UploaderService
    {
        FileLocMapperService FileLocMapper;
        public UploaderService()
        {
            FileLocMapper = new FileLocMapperService();
        }
        public async Task<ResponseStructure> UploadBase64(string data, string loc, string fireFlag,int w = 500,int h = 500)
        {
            try
            {
                string root = FileLocMapper.LocateMediaFile(fireFlag, loc);
                var split = data.Split(',');
                var strings = split[1].Split('-');
                byte[] imageBytes = Convert.FromBase64String(strings[0]);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                string fileName = DateTime.UtcNow.Millisecond + UtilesService.RandomDigit(15) + UtilesService.getFileExtByContentType(split[0]);
                using (MemoryStream mOutput = new MemoryStream())
                {
                    image.Save(mOutput, image.RawFormat);
                    using (FileStream fs = File.Create(String.Concat(root, fileName).ToLowerInvariant()))
                    using (BinaryWriter bw = new BinaryWriter(fs))
                        bw.Write(mOutput.ToArray());
                }
                
                return ResponseModel.Success(msg: "بارگذاری فایل با موفقیت انجام شد", data: fileName);
            }
            catch (Exception ex)
            {
                return ResponseModel.Error(msg: "خطایی در بارگذاری فایل رخ داده است.لطفا با پشتیبان سایت تماس بگیرید.", data: ex.Message);
            }
        }
        public async Task<ResponseStructure> UploadVideo(IFormFile file, string fireFlag, string loc)
        {
            try
            {

                string ftype = file.ContentType;
                if ((ftype == "video/mp4"))
                {
                    string uploadPath = FileLocMapper.LocateMediaFile(fireFlag, loc);
                    //long fsize = file[i].Length;
                    string fname = DateTime.UtcNow.Millisecond + UtilesService.GenerateNewRandom() + "." + System.IO.Path.GetExtension(file.FileName).Substring(1);
                    var filePath = Path.Combine(uploadPath, fname);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return ResponseModel.Success(msg: "ویدئو با موفقیت آپلود گردید", data: fname);
                }
                else
                {
                    return ResponseModel.Error("نوع فایل ارسال شده صحیح نمیباشد");
                }

            }
            catch (Exception e)
            {
                return ResponseModel.Error("خطای ناشناخته در سیستم رخ داده است" + e.Message);
            }
            return null;
        }
        public async Task<ResponseStructure> UploadAudio(IFormFile file, string fireFlag, string loc)
        {
            try
            {

                string ftype = file.ContentType;
                if ((ftype == "audio/mp3" || ftype == "audio/mpeg"))
                {
                    string uploadPath = FileLocMapper.LocateMediaFile(fireFlag, loc);
                    //long fsize = file[i].Length;
                    string fname = DateTime.UtcNow.Millisecond + UtilesService.GenerateNewRandom() + "." + System.IO.Path.GetExtension(file.FileName).Substring(1);
                    var filePath = Path.Combine(uploadPath, fname);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return ResponseModel.Success(msg: "فایل صوتی با موفقیت آپلود گردید", data: fname);
                }
                else
                {
                    return ResponseModel.Error("نوع فایل ارسال شده صحیح نمیباشد");
                }

            }
            catch (Exception e)
            {
                return ResponseModel.Error("خطای ناشناخته در سیستم رخ داده است" + e.Message);
            }
        }
        public async Task<ResponseStructure> UploadImageFile(IFormFile file, string fireFlag, string loc)
        {
            try
            {
                string[] allowtype = new string[] { "image/x-icon", "image/png", "image/jpg", "image/jpeg", "image/gif", "image/tiff" };
                string ftype = file.ContentType;
                if ((allowtype.Contains(ftype)))
                {
                    string uploadPath = FileLocMapper.LocateMediaFile(fireFlag, loc);
                    //long fsize = file[i].Length;
                    string fname = DateTime.UtcNow.Millisecond + UtilesService.GenerateNewRandom() + "." + System.IO.Path.GetExtension(file.FileName).Substring(1);
                    var filePath = Path.Combine(uploadPath, fname);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return ResponseModel.Success(msg: "فایل با موفقیت آپلود گردید", data: fname);
                }
                else
                {
                    return ResponseModel.Error("نوع فایل ارسال شده صحیح نمیباشد");
                }

            }
            catch (Exception e)
            {
                return ResponseModel.Error("خطای ناشناخته در سیستم رخ داده است" + e.Message);
            }
            return null;
        }
        public async Task<ResponseStructure> uploadDocument(IFormFile file, string fireFlag, string loc)
        {
            try
            {
                string[] allowtype = new string[] { "application/msword", "application/vnd.ms-excel", "application/vnd.ms-powerpoint", "text/plain", "application/pdf" };
                string ftype = file.ContentType;
                if ((allowtype.Contains(ftype)))
                {
                    string uploadPath = FileLocMapper.LocateMediaFile(fireFlag, loc);
                    //long fsize = file[i].Length;
                    string fname = DateTime.UtcNow.Millisecond + UtilesService.GenerateNewRandom() + "." + System.IO.Path.GetExtension(file.FileName).Substring(1);
                    var filePath = Path.Combine(uploadPath, fname);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return ResponseModel.Success(msg: "فایل با موفقیت آپلود گردید", data: fname);
                }
                else
                {
                    return ResponseModel.Error("نوع فایل ارسال شده صحیح نمیباشد");
                }

            }
            catch (Exception e)
            {
                return ResponseModel.Error("خطای ناشناخته در سیستم رخ داده است" + e.Message);
            }
            return null;
        }
        public ResponseStructure RemoveFile(string loc, string fireFlag, string filename)
        {
            try
            {
                string root = String.Concat(FileLocMapper.LocateMediaFile(fireFlag, loc), filename);
                if (!File.Exists(root))
                {
                    return ResponseModel.Error("فایل مورد نظر یافت نشد");
                }
                File.Delete(root);
                return ResponseModel.Success("فایل مورد نظر با موفقیت حذف گردید");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
