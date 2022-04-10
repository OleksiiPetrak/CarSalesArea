using Azure.Storage.Blobs;
using CarSalesArea.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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

        public async Task<List<string>> SaveCarMedia(IEnumerable<IFormFile> mediaFiles)
        {
            var uniqueMediaPathCollection = new List<string>();

            foreach (IFormFile file in mediaFiles)
            {
                string uniqueMediaName = GetUniqueMediaName(file.FileName);
                using (Stream stream = file.OpenReadStream())
                {
                    BlobServiceClient blobServiceClient = new BlobServiceClient(_azureStorageConfig.ConnectionString);

                    // Get the container (folder) the file will be saved in
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(_azureStorageConfig.FileContainerName);

                    // Get the Blob Client used to interact with (including create) the blob
                    BlobClient blobClient = containerClient.GetBlobClient(uniqueMediaName);

                    // Upload the blob
                    await blobClient.UploadAsync(stream);
                }
                uniqueMediaPathCollection.Add(GetPathToMedia(uniqueMediaName));
            }

            return uniqueMediaPathCollection;
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

        private static string GetUniqueMediaName(string mediaName)
            => Guid.NewGuid() + mediaName;

        private static string GetPathToMedia(string mediaName) 
            => "https://carsalesareastorage.blob.core.windows.net/cars-media-container/" + mediaName;
    }
}
