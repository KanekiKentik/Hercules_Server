using System.ComponentModel.DataAnnotations;

public record WorkoutSearchFilterDTO
{
    public DateTimeOffset DateFrom { get; init; } = default;
    public DateTimeOffset DateTo { get; init; } = default;
}