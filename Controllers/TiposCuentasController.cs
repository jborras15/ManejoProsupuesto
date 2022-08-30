using System.Data.SqlClient;
using Dapper;
using ManejoProsupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoProsupuesto.Controllers;

public class TiposCuentasController: Controller
{
    private readonly string connectionString;

    public TiposCuentasController(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public IActionResult Crear()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            // select 1 ,trae un 1 de respuesta
            var query = connection.Query("SELECT 1").FirstOrDefault();
        }
        return View();
    }

    [HttpPost]
    public IActionResult Crear(TipoCuenta tipoCuenta)
    {
        // si el modelo es invalido entra hay 
        if (!ModelState.IsValid)
        {
            return View(tipoCuenta);
        }
        return View();
    }
}