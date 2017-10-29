using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Services
{
    public interface IImageService
    {
        string GetBase64OfFile(string filePath);
    }
}
