using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;

namespace CleanArchitecture.Application.Vehiculos.SearchVehiculos;

/// <summary>
/// Se utiliza para manejar consultas de obtención de vehículos disponibles para alquilar.
/// Esta clase es internal porque su funcionalidad no debe ser accesible desde fuera del ensamblado en el que se encuentra.
/// La clase que sí es pública a la capa de presentación es <see cref="SearchVehiculosQuery"/>.
/// </summary>
internal sealed class SearchVehiculosQueryHandler
 : IQueryHandler<SearchVehiculosQuery, IReadOnlyList<VehiculoResponse>>
{
    /// <summary>
    /// Define los estados de alquiler considerados como activos.
    /// </summary>
    private static readonly int[] ActiveAlquilerStatuses =
    {
        (int)AlquilerStatus.Reservado,
        (int)AlquilerStatus.Confirmado,
        (int)AlquilerStatus.Completado
    };

    private readonly ISqlConnectionFactory _sqlConectionFactory;

    public SearchVehiculosQueryHandler(ISqlConnectionFactory sqlConectionFactory)
    {
        _sqlConectionFactory = sqlConectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehiculoResponse>>> Handle(
        SearchVehiculosQuery request,
        CancellationToken cancellationToken
    )
    {
        // Si la fecha de inicio es posterior a la fecha de fin, se devuelve una lista vacía.
        if (request.fechaInicio > request.fechaFin)
        {
            return new List<VehiculoResponse>();
        }

        using var connection = _sqlConectionFactory.CreateConnection();

        const string sql = """
             SELECT
                a.id as Id,
                a.modelo as Modelo,
                a.vin as Vin,
                a.precio_monto as Precio,
                a.precio_tipo_moneda as TipoMoneda,
                a.direccion_pais as Pais,
                a.direccion_departamento as Departamento,
                a.direccion_provincia as Provincia,
                a.direccion_ciudad as Ciudad,
                a.direccion_calle as Calle
             FROM vehiculos AS a
             WHERE NOT EXISTS
             (
                    SELECT 1
                    FROM alquileres AS b
                    WHERE
                        b.vehiculo_id = a.id AND
                        b.duracion_inicio <= @EndDate AND
                        b.duracion_fin >= @StartDate AND
                        b.status = ANY(@ActiveAlquilerStatuses)
             )
        """;

        // Se ejecuta la consulta SQL y se espera el resultado asincrónico.
        // Devuelve un VehiculoResponse y un DireccionResponse, que está incluido en un VehiculoResponse
        var vehiculos = await connection
            .QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>
            (
                sql,
                //Indica que el vehículo va a tener una dirección.
                (vehiculo, direccion) =>
                {
                    vehiculo.Direccion = direccion;
                    return vehiculo;
                },
                new
                {
                    StartDate = request.fechaInicio,
                    EndDate = request.fechaFin,
                    ActiveAlquilerStatuses
                },
                splitOn: "Pais"
            );

        return vehiculos.ToList();
    }
}