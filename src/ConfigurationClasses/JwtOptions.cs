public class JwtOptions
{
    public string SecretKey { get; init; } = string.Empty;
    public int LifespanHours { get; init; }
}