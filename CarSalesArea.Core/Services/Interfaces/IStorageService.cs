using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    public interface IStorageService
    {
        Task Initialize();

        Task Save(Stream fileStream, string name);

        Task<List<string>> SaveCarMedia(IEnumerable<IFormFile> mediaFiles);

        Task<Stream> Load(string name);
    }
}
