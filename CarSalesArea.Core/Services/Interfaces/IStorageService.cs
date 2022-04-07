using System.IO;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    public interface IStorageService
    {
        Task Initialize();

        Task Save(Stream fileStream, string name);

        Task<Stream> Load(string name);
    }
}
