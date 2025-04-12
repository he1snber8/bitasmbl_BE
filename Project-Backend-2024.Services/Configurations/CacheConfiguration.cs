namespace Project_Backend_2024.Services.Configurations;

public class CacheConfiguration
{
    public double AbsoluteExpirationSeconds { get; set; }
    public double SlidingExpirationSeconds { get; set; }
}

public class S3Settings
{
    public string Region { get; init; } = string.Empty;
    public string BucketName { get; init; } = string.Empty;
}