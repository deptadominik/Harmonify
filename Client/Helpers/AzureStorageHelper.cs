using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;

namespace Harmonify.Client.Helpers;

public class AzureStorageHelper
{
    private const string BaseUrl = "https://harmonifystorage.blob.core.windows.net/";
    private const string ContainerName = "harmonifyimages";
    private const string StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=harmonifystorage;AccountKey=FhyHxdWRS5BMmnf/OB3Iekyda/CRqsViJrTc63EULpWJX9ZgbWR1VH6geR7RdG852vcjZcY6Cg4X+ASt/nGMGg==;EndpointSuffix=core.windows.net";
    private readonly string _cloudStorageConnectionString;

    public AzureStorageHelper()
    {
    }
        
    public AzureStorageHelper(string cloudStorageConnectionString)
    {
        _cloudStorageConnectionString = cloudStorageConnectionString;
    }

    public async Task<string> UploadFileToAzureAsync(byte[] file, string destFileName)
    {
        var container = OpenContainer(ContainerName);
        if (container == null)
            throw new InvalidOperationException("Couldn't open the container.");

        destFileName = destFileName.Replace(' ', '_');

        var blobContainerClient = new BlobContainerClient(StorageConnectionString, ContainerName);
        var blockBlobClient = blobContainerClient.GetBlockBlobClient(destFileName);
        using(var ms = new MemoryStream(file, false))
            await blockBlobClient.UploadAsync(ms);
        
        return $"{BaseUrl}{ContainerName}\\{ destFileName}";
    }
    
    BlobContainerClient OpenContainer(string containerName)
    {
        try
        {
            string? connectionString = StorageConnectionString;

            // Create a BlobServiceClient object which will be used to create a container client
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString ?? _cloudStorageConnectionString);

            // Create the container and return a container client object
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
        catch (Exception ex)
        {
            var msg = ex.Message;
            return null;
        }
    }

    public async Task DeleteImageFromAzure(string imageName)
    {
        var container = OpenContainer(ContainerName);
        await container.DeleteBlobAsync(imageName);
    }
}