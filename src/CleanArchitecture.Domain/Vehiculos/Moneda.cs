namespace CleaArchitecture.Domain.Vehiculos;

/// <summary>
/// Value object que indica un precio en una moneda concreta.
/// </summary>
/// <param name="Monto">Precio</param>
/// <param name="TipoMoneda">Tipo de moneda utilizado</param>
public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
{
    /// <summary>
    /// Operador "+". para poder sumar dos monedas del mismo tipo.
    /// </summary>
    /// <returns>Montante de la suma de las monedas.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static Moneda operator + (Moneda primero, Moneda segundo)
    {
        if(primero.TipoMoneda != segundo.TipoMoneda)
        {
            throw new InvalidOperationException("El tipo de moneda debe ser el mismo");
        }

        return new Moneda(primero.Monto + segundo.Monto, primero.TipoMoneda); //primero y segundo son el mismo tipo de monea, da igual.
    }

    public static Moneda Zero() => new(0, TipoMoneda.None);
    public static Moneda Zero(TipoMoneda tipoMoneda) => new(0, tipoMoneda);
    public bool IsZero() => this == Zero(TipoMoneda);
}