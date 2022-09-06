using System.Collections.Specialized;
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
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<ActionResult> Editar(int id )
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await _repositorioTipoCuenta.ObtenerPorId(id, usuarioId);

        if (tipoCuenta is null)
        {
            return RedirectToAction("NoEncontrado", "Home");
        }

        return View(tipoCuenta);
    }
    
    [HttpPost]
    public async Task<ActionResult> Editar(TipoCuenta tipoCuenta )
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var tipoCuentaExiste = await _repositorioTipoCuenta.ObtenerPorId(tipoCuenta.Id, usuarioId);

        if (tipoCuentaExiste is null)
        {
            return RedirectToAction("NoEncontrado", "Home");
        }

        await _repositorioTipoCuenta.Actualizar(tipoCuenta);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Borrar(int id)
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await _repositorioTipoCuenta.ObtenerPorId(id, usuarioId);

        if (tipoCuenta is null)
        {
            return RedirectToAction("NoEncontrado", "Home");
        }

        return View(tipoCuenta);

    }

    [HttpPost]
    public async Task<IActionResult> BorrarTipoCuenta(int id)
    {
        var usuarioId = _servicioUsuarios.ObtenerUsuarioId();
        var tipoCuenta = await _repositorioTipoCuenta.ObtenerPorId(id, usuarioId);

        if (tipoCuenta is null)
        {
            return RedirectToAction("NoEncontrado", "Home");
        }

        await _repositorioTipoCuenta.Borrar(id);
        return RedirectToAction("Index");
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