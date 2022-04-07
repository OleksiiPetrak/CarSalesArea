using Azure.Storage.Blobs;
using CarSalesArea.Core.Models;
using Microsoft.Extensions.Options;
using System.IO;
using System.Threading.Tasks;

namespace CarSalesArea.Core.Services.Interfaces
{
    public class StorageService : IStorageService
    {
        private readonly AzureStorageConfig _azureStorageConfig;

        public StorageService(IOptions<AzureStorageConfig> azureStorageConfig)
        {
            _azureStorageConfig = azureStorageConfig.Value;
        }

        public Task Initialize()
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageConfig.ConnectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageConfig.FileContainerName);
            return containerClient.CreateIfNotExistsAsync();
        }

        public Task Save(Stream fileStream, string name)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageConfig.ConnectionString);

            // Get the container (folder) the file will be saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageConfig.FileContainerName);

            // Get the Blob Client used to interact with (including create) the blob
            BlobClient blobClient = containerClient.GetBlobClient(name);

            // Upload the blob
            return blobClient.UploadAsync(fileStream);
        }

        public Task<Stream> Load(string name)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageConfig.ConnectionString);

            // Get the container the blobs are saved in
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageConfig.FileContainerName);

            // Get a client to operate on the blob so we can read it.
            BlobClient blobClient = containerClient.GetBlobClient(name);

            return blobClient.OpenReadAsync();
        }
    }
}
