using System.ComponentModel.DataAnnotations;

public record SetResponse
{
    [Range(0, int.MaxValue)]
    public int SetId { get; init; } 

    [Range(0, 600)]
    public int Weight { get; init; }

    [Range(1, int.MaxValue)]
    public int Reps { get; init; }

    [Range(1, int.MaxValue)]
    public int Order { get; init; }

    public SetResponse(int id, int weight, int reps, int order)
        => (SetId, Weight, Reps, Order) = (id, weight, reps, order);
}