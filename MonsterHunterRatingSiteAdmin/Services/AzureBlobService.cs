using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MonsterHunterRatingSiteAdmin.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _containerName;

        public AzureBlobService(IConfiguration configuration)
        {
            _containerName =
                configuration["AZ_STORAGE_CONTAINER_NAME"]
                ?? throw new InvalidOperationException(
                    "Storage container name is missing."
                );

            string connectionString =
                configuration.GetConnectionString(
                    "AZ_STORAGE_CONNECTION_STRING"
                ) ?? throw new InvalidOperationException(
                    "Storage connection string is missing."
                );

            _blobServiceClient =
                new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(
            IFormFile file,
            string filename)
        {
            var containerClient =
                _blobServiceClient.GetBlobContainerClient(
                    _containerName
                );

            var blobClient =
                containerClient.GetBlobClient(filename);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            using var stream = file.OpenReadStream();

            await blobClient.UploadAsync(stream, options);

            return blobClient.Uri.ToString();
        }
    }
}
