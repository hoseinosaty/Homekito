using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinifyAPI;
namespace Barayand.Common.Services
{
    public  class CompressorService
    {
        public void PrepareApiKey()
        {
            Tinify.Key = "hdrdBNqcCwkPQqVYml22fqsqtGf71xdC";
        }
        public static async Task<byte[]> CompressBase64(byte[] buffer,int w = 500,int h = 500)
        {
            try
            {
                Tinify.Key = "hdrdBNqcCwkPQqVYml22fqsqtGf71xdC";
                return await Tinify.FromBuffer(buffer).Resize(new {
                    method = "fit",
                    width = w,
                    height = h
                }).ToBuffer();
            }
            catch(TinifyAPI.Exception ex)
            {
                return new byte[] { };
            }
        }
        public static async Task<byte[]> CompressFile(byte[] buffer, int w = 500, int h = 500)
        {
            try
            {
                return await Tinify.FromBuffer(buffer).Resize(new
                {
                    method = "fit",
                    width = w,
                    height = h
                }).ToBuffer();
            }
            catch (TinifyAPI.Exception ex)
            {
                return new byte[] { };
            }
        }
    }
}
