using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DesafioBackend.Application.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(IFormFile file);
    }
}

