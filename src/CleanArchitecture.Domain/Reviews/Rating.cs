using CleanArchitecture.Domain.Abstractions;

namespace CleaArchitecture.Domain.Reviews;

/// <summary>
/// Rating a puntuar entre 1 y 5 estrellas.
/// </summary>
public sealed record Rating
{
    public static readonly Error Invalid = new("Rating.Invalid", "El rating es invalid");

    public int Value { get; init; }

    //Opción constructor 1:
    // private Rating(int value)
    // {
    //     Value = value;
    // }
    //Opción constructor 2:
    private Rating(int value) => Value = value;

    public static Result<Rating> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Rating>(Invalid);
        }

        return new Rating(value);
    }
}