using System.ComponentModel;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using Dapper;
using ManejoProsupuesto.Models;

namespace ManejoProsupuesto.Servicios;

public interface IRepositorioTipoCuenta
{
    Task Crear(TipoCuenta tipoCuenta);
    Task<bool> Existe(string nombre, int usuarioId);
    Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
    Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    Task Actualizar(TipoCuenta tipoCuenta);
    Task Borrar(int id);
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

    public async Task<bool> Existe(string nombre, int usuarioId)
    {
        using var connection = new SqlConnection(connectionString);
        // QueryFirstOrDefaultAsync sirve para traer el primer elemento o regresar el valor por defecto en este caso, 
        // el valor por defecto de int que es 0 
        
         var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                        @"Select 1 
                                            from ManejoPresupuesto.TiposCuentas 
                                            where [Nombre] = @Nombre AND UsuarioId = @UsuarioId",
                                        new {nombre, usuarioId});
         return existe == 1;
    }

    //lista
    public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
    {
        using var connection = new SqlConnection(connectionString);
        // QueryAsync nos permite hacer un select y mapear , en este caso mapear los tipo de datos a  tipo cuenta
        return await connection.QueryAsync<TipoCuenta>(@"SELECT Id, [Nombre], Orden 
                                                            From ManejoPresupuesto.TiposCuentas  where UsuarioId = @UsuarioId",
            new { usuarioId });
    }

    public async Task Actualizar(TipoCuenta tipoCuenta)
    {
        using var connection = new SqlConnection(connectionString);
        // ExecuteAsync nos permite hacer un squery sin que retorne nada
        await connection.ExecuteAsync(@"UPDATE ManejoPresupuesto.TiposCuentas 
                                                SET [Nombre] = @Nombre
                                                    WHERE Id =@Id", tipoCuenta);
    }

    public async Task<TipoCuenta> ObtenerPorId(int id, int usuarioId)
    {
        using var connection = new SqlConnection(connectionString);
        // where Id = @Id AND UsuarioId=@UsuarioId , Id es el de tipo usuario y UsuarioId es el de usuario
        return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                                            FROM  ManejoPresupuesto.TiposCuentas 
                                                                            where Id = @Id AND UsuarioId=@UsuarioId
                                                            ",new { id, usuarioId });
    }


    public async Task Borrar(int id)
    {
        using var connection = new SqlConnection(connectionString);
        // ExecuteAsync nos permite hacer un squery sin que retorne nada
        await connection.ExecuteAsync("Delete ManejoPresupuesto.TiposCuentas where Id=@Id", new { id });
    }
}