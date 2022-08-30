using ManejoProsupuesto.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManejoProsupuesto.Controllers;

public class TiposCuentasController: Controller
{

    public IActionResult Crear()
    {
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