using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public async Task<string> GetBase64OfFileAsync(string filePath)
        {
            byte[] bytes = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(bytes);
        }
    }
}
