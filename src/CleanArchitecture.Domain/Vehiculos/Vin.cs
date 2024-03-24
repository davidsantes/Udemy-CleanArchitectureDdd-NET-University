namespace CleanArchitecture.Domain.Vehiculos;

/// <summary>
/// Value object de número de identificación del coche (Vehicle Identification Number). Una vez creado, no cambia de valor.
/// </summary>
public record Vin(string Value);