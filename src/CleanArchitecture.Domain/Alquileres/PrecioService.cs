using CleaArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Shared;

namespace CleaArchitecture.Domain.Alquileres;

public class PrecioService
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;
        var precioTotal = Moneda.Zero();

        //Cálculo del precio base
        var precioPorPeriodo = new Moneda(
            periodo.CantidadDias * vehiculo.Precio.Monto,
            tipoMoneda);
        precioTotal += precioPorPeriodo;

        //Cálculo del precio por accesorios solicitados por el usuario
        decimal porcentageCharge = 0;
        var accesorioCharges = Moneda.Zero(tipoMoneda);

        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentageCharge += accesorio switch
            {
                Accesorio.AppleCar or Accesorio.AndroidCar => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.01m,
                _ => 0 //Similar a un "else"
            };
        }

        if (porcentageCharge > 0)
        {
            accesorioCharges = new Moneda(
                precioPorPeriodo.Monto * porcentageCharge,
                tipoMoneda
            );
        }

        precioTotal += accesorioCharges;

        //Cálculo del precio por mantenimiento
        if (!vehiculo!.Mantenimiento!.IsZero())
        {
            precioTotal += vehiculo.Mantenimiento;
        }

        return new PrecioDetalle(
            precioPorPeriodo,
            vehiculo.Mantenimiento,
            accesorioCharges,
            precioTotal
            );
    }
}