using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;

namespace Project_Backend_2024.Services.CommandServices.AWS;

public class S3BucketService(IAmazonS3 amazonS3)
{
    public async Task<List<string>> UploadFilesToS3Async(IEnumerable<IFormFile> files)
    {
        var uploadedUrls = new List<string>();

        foreach (var file in files)
        {
            var fileUrl = await UploadFileAsync(file, "my-bucket-bitasmbl");
            uploadedUrls.Add(fileUrl);
        }

        return uploadedUrls;
    }

    public async Task<string> UploadImageFromUrlToS3Async(string imageUrl, string key)
    {
        // Fetch the image and its content type
        var (imageStream, contentType) = await FetchImageFromUrlAsync(imageUrl);

        // Upload the image to S3
        return await UploadImageToS3Async(imageStream, "my-bucket-bitasmbl", key, contentType);
    }

    private async Task<string> UploadImageToS3Async(Stream imageStream, string bucketName, string key, string contentType)
    {
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = imageStream,
            BucketName = bucketName,
            Key = key,
            ContentType = contentType
        };

        var transferUtility = new TransferUtility(amazonS3);
        await transferUtility.UploadAsync(uploadRequest);

        // Return the S3 object URL
        return $"https://{bucketName}.s3.amazonaws.com/{key}";
    }

    public async Task<string> UploadFileAsync(IFormFile file, string bucketName)
    {
        var key = Guid.NewGuid() + "_" + file.FileName; // Generate a unique key for the file

        using var stream = file.OpenReadStream();
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await amazonS3.PutObjectAsync(request);

        // Return the file URL
        return $"https://{bucketName}.s3.amazonaws.com/{key}";
    }

    public static async Task<(Stream ImageStream, string ContentType)> FetchImageFromUrlAsync(string imageUrl)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(imageUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to fetch image from URL: {response.ReasonPhrase}");
        }

        var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
        var imageStream = await response.Content.ReadAsStreamAsync();

        return (imageStream, contentType);
    }
}
