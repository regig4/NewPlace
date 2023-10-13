using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public interface IImageService
    {
        Task<string> GetBase64OfFileAsync(string filePath);
        Task SaveBase64Img(string filename, string base64);
    }
}
