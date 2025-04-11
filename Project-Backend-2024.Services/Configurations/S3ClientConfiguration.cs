namespace Project_Backend_2024.Services.Configurations;

public class S3ClientConfiguration
{
    public string Region { get; set; }
    public string BucketName { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
}