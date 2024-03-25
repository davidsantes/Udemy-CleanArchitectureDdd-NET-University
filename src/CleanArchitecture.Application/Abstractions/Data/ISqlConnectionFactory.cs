using System.Data;

namespace CleanArchitecture.Application.Abstractions.Data;

/// <summary>
/// Factoría de conexión que se utilizará para Dapper (consultas).
/// </summary>
public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}