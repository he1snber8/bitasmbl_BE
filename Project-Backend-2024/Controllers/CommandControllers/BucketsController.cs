using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_Backend_2024.Services.CommandServices.AWS;

namespace Project_Backend_2024.Controllers.CommandControllers;

[Route("api/buckets")]
[ApiController]
public class BucketsController(IAmazonS3 s3Client, S3BucketService s3BucketService) : ControllerBase
{
    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOnly")]
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBucketAsync()
    {
        try
        {
            var data = await s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => b.BucketName);
            return Ok(buckets);
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to fetch buckets: {ex.Message}");
        }
    }

    [Authorize(AuthenticationSchemes = "Cookies", Policy = "AdminOrUser")]
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync([FromForm] string imageUrl,
        [FromForm] string key)
    {
        try
        {
            await s3BucketService.UploadImageFromUrlToS3Async(imageUrl, $"profile-images/{key}");
            return Ok("uploaded!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest("uploaded!");
        }
    }
}