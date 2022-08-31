using ManejoProsupuesto.Models;
using ManejoProsupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoProsupuesto.Controllers;

public class TiposCuentasController: Controller
{
    private readonly IRepositorioTipoCuenta _repositorioTipoCuenta;

    public TiposCuentasController(IRepositorioTipoCuenta repositorioTipoCuenta)
    {
        _repositorioTipoCuenta = repositorioTipoCuenta;
    }

    public IActionResult Crear()
    { 
        return View();
    }

    [HttpPost]
    public async Task<IActionResult>Crear(TipoCuenta tipoCuenta)
    {
        // si el modelo es invalido entra hay 
        if (!ModelState.IsValid)
        {
            return View(tipoCuenta);
        }
        // le decimos que use el usuario 4 de la tabla 
        // usuario fue creado directamente en la base de datos
        tipoCuenta.UsuarioId = 4;
        await _repositorioTipoCuenta.Crear(tipoCuenta);
        return View();
    }
}