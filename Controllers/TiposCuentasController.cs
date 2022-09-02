using ManejoProsupuesto.Models;
using ManejoProsupuesto.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ManejoProsupuesto.Controllers;

public class TiposCuentasController: Controller
{
    private readonly IRepositorioTipoCuenta _repositorioTipoCuenta;
    private readonly IServicioUsuarios _servicioUsuarios;

    public TiposCuentasController(IRepositorioTipoCuenta repositorioTipoCuenta, IServicioUsuarios servicioUsuarios)
    {
        _repositorioTipoCuenta = repositorioTipoCuenta;
        _servicioUsuarios = servicioUsuarios;
    }

    public async Task<IActionResult> Index()
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var tiposCuentas = await _repositorioTipoCuenta.Obtener(usuarioId);
        return View(tiposCuentas);
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
        tipoCuenta.UsuarioId = _servicioUsuarios.ObtenerUsuarioId();

        
        var yaExisteTipoCuenta = await _repositorioTipoCuenta.Existe(tipoCuenta.Nombre, tipoCuenta.UsuarioId);

        if (yaExisteTipoCuenta)
        {
            // nameOf indicamos el campo que lanzara  el error 
            ModelState.AddModelError(nameof(tipoCuenta.Nombre),
                $"El nombre {tipoCuenta.Nombre} ya existee");
            return View(tipoCuenta);
        }
        await _repositorioTipoCuenta.Crear(tipoCuenta);
        return Redirect("Index");
    }

    [HttpGet]
    public async Task<IActionResult> VerificarExisteTipoCuenta(string nombre)
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var yaExisteTipoCuenta = await _repositorioTipoCuenta.Existe(nombre, usuarioId);
    
        if (yaExisteTipoCuenta)
        {
            // indica el mensaje que se mostrara
            return Json($"El nombre {nombre} ya existe");
        }
    
        return Json(true);
    }
}