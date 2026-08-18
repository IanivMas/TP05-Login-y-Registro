using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP05.Models;

namespace TP05.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    BD bd = new BD();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Registro(Usuario usuario)
{
    if (bd.buscarPorNombreUsuario(usuario.usuario) == null)
    {
        HttpContext.Session.SetString("usuario", usuario.usuario);
        HttpContext.Session.SetString("clave", usuario.clave);
        HttpContext.Session.SetString("tipo", usuario.tipo);
        HttpContext.Session.SetString("nombre", usuario.nombre);
        HttpContext.Session.SetString("apellido", usuario.apellido);

        bd.agregarUsuario(usuario);

        return RedirectToAction("InicioSesion", "Home");
    }
    else
    {
        ViewBag.error = "El nombre de usuario ya existe.";
        return View(usuario);
    }
}
    [HttpPost]
public IActionResult InicioSesion(Usuario usuario)
{
    Usuario usuarioEncontrado = bd.encontrarUsuario(usuario.usuario, usuario.clave);

    if (usuarioEncontrado == null)
    {
        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View(usuario);
    }

    HttpContext.Session.SetString("usuario", usuarioEncontrado.usuario);
    HttpContext.Session.SetString("tipo", usuarioEncontrado.tipo);
    HttpContext.Session.SetString("nombre", usuarioEncontrado.nombre);
    HttpContext.Session.SetString("apellido", usuarioEncontrado.apellido);

    return RedirectToAction("PaginaPrincipal", "Home");
}

    

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
