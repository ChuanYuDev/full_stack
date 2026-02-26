using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace WebAPITest.Services;

public class AzureFileStorage
{
    public async Task Store(string container, IFormFile file)
    {
        string connectionString = "";
        string filename = "";
        
        var client = new BlobContainerClient(connectionString, container);

        var blob = client.GetBlobClient(filename);
        var blobHttpHeaders = new BlobHttpHeaders();
        await blob.UploadAsync(file.OpenReadStream(), blobHttpHeaders);
    }
}