using Microsoft.AspNetCore.Http;
  

 
namespace Pharmacy.Core.IServiceContracts
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);

        void DeleteFile(string relativePath);

        string GetFullImageUrl(string? relativePath);
    }

}
