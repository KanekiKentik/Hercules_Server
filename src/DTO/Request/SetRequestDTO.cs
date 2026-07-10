using System.ComponentModel.DataAnnotations;

public record SetRequestDTO
{
    [Range(0, 600)]
    public int Weight { get; init; }

    [Range(1, int.MaxValue)]
    public int Reps { get; init; }

    [Range(1, int.MaxValue)]
    public int Order { get; init; }

    public SetRequestDTO() {}
    public SetRequestDTO(int sessionId, int weight, int reps, int order)
        => (Weight, Reps, Order) = (weight, reps, order);
}