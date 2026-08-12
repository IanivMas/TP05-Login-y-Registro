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
            ViewBag.agregar = bd.agregarUsuario(usuario);
            HttpClient.Context.Session.SetString("usuario", usuario.usuario);
            HttpClient.Context.Session.SetString("clave", usuario.clave);
            HttpClient.Context.Session.SetString("tipo", usuario.tipo);
            HttpClient.Context.Session.SetString("nombre", usuario.nombre);
            HttpClient.Context.Session.SetString("apellido", usuario.apellido);
            HttpClient.Context.Session.SetString("id", usuario.id.ToString());
            
            return RedirectToAction("InicioSesion", "Home");
        }
        else
        {
            ViewBag.error = "El nombre de usuario ya existe.";

            return View();
        }
    }
    [HttpPost]
    public IActionResult InicioSesion(string usuario, string clave)
    {

        Usuario UsuarioEncontrado = bd.encontrarUsuario(usuario, clave);
        if (UsuarioEncontrado == null)
        {
            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return RedirectToAction("InicioSesion", "Home");
        }
        else
        {
            return RedirectToAction("PaginaPrincipal", "Home");

        }

    }
    public IActionResult PaginaPrincipal()
    {

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
