using System.Data.SqlClient;
using Dapper;
using ManejoProsupuesto.Models;

namespace ManejoProsupuesto.Servicios;

public interface IRepositorioTipoCuenta
{
    Task Crear(TipoCuenta tipoCuenta);
}

public class RepositorioTipoCuenta: IRepositorioTipoCuenta
{
    public readonly string connectionString;

    public RepositorioTipoCuenta(IConfiguration configuration)
    { 
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task Crear (TipoCuenta tipoCuenta)
    {
        using var connection = new SqlConnection(connectionString);
        //QuerySingle sirve para establecer un query que va a traer un solo elemento
        var  id = await connection.QuerySingleAsync<int>($@"Insert into ManejoPresupuesto.TiposCuentas(UsuarioId, Orden, Nombre)
                                                        values(@UsuarioId, 0, @Nombre);
                                                        SELECT SCOPE_IDENTITY();", tipoCuenta);
        // SCOPE_IDENTITY() es para regresar el id 
        tipoCuenta.Id = id;
        
    }
}