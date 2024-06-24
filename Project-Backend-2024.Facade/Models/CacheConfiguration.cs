namespace Project_Backend_2024.Facade.Models;

public class CacheConfiguration
{
    public string CacheKey { get; set; } = null!;
    public double AbsoluteExpirationSeconds { get; set; }
    public double SlidingExpirationSeconds { get; set; }
}