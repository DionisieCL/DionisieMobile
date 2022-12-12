using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Schoolager.Web.Helpers
{
    public interface IBlobHelper
    {
        Task<Guid> UploadBlobAsync(byte[] file, string containerName);
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName);
        Task<Guid> UploadBlobAsync(IFormFile file, string containerName, string extension);
        Task<Guid> UploadBlobAsync(string image, string containerName);
    }
}