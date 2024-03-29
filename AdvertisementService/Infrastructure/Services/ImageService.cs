﻿using ApplicationCore.Services;

namespace Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public const string DefaultFilePath = @"..\AdvertisementService\Infrastructure\Content\Images\default.jpg";

        public async Task<string> GetBase64OfFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                filePath = DefaultFilePath;
            }

            byte[] bytes = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(bytes);
        }

        public async Task SaveBase64Img(string filename, string base64)
        {
            try
            {
                await File.WriteAllBytesAsync(@"..\AdvertisementService\Infrastructure\Content\Images\" + filename, Convert.FromBase64String(base64.Substring(23)));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
