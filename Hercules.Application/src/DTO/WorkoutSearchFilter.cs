using System.ComponentModel.DataAnnotations;

public record WorkoutSearchFilter
{
    public DateTimeOffset? DateFrom { get; init; } = default;
    public DateTimeOffset? DateTo { get; init; } = default;
}