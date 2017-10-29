using ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public string GetBase64OfFile(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }
    }
}
