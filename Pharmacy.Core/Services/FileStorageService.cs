using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pharmacy.Core.IServiceContracts;

namespace Pharmacy.Core.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly string _wwwRootPath;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public FileStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {

            _wwwRootPath = env.WebRootPath;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {


            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file");


            var folderPath = Path.Combine(_wwwRootPath, folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.Combine(folderName, fileName).Replace("\\", "/");
        }

        public void DeleteFile(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return;

            var fullPath = Path.Combine(_wwwRootPath, relativePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public string GetFullImageUrl(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
                return string.Empty;

            var request = _httpContextAccessor.HttpContext?.Request;
            if (request == null)
                return relativePath.Replace("\\", "/");

            var baseUrl = $"{request.Scheme}://{request.Host}";

            return $"{baseUrl}/{relativePath.Replace("\\", "/")}";
        }
    }

}
