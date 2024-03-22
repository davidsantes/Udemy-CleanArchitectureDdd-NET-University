namespace CleaArchitecture.Domain.Vehiculos;

/// <summary>
/// Value object de tipo de moneda que se pueda utilizar en el proyecto.
/// </summary>
public record TipoMoneda
{
    public static readonly TipoMoneda None = new("");
    public static readonly TipoMoneda Usd = new("USD");
    public static readonly TipoMoneda Eur = new("EUR");

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="codigo">USD / EUR</param>
    private TipoMoneda(string codigo) => Codigo = codigo;

    public string? Codigo { get; init; }

    /// <summary>
    /// Devuelve todos los TipoMoneda registrados.
    /// </summary>
    public static readonly IReadOnlyCollection<TipoMoneda> All = new[]
    {
        Usd,
        Eur
    };

    /// <summary>
    /// Devuelve un TipoMoneda concreto.
    /// </summary>
    public static TipoMoneda FromCodigo(string codigo)
    {
        return All.FirstOrDefault(c => c.Codigo == codigo) ??
            throw new ApplicationException("El tipo de moneda es invalido");
    }
}