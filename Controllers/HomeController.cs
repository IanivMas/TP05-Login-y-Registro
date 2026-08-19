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
    public IActionResult Registro()
{
    return View();
}

public IActionResult InicioSesion()
{
    return View();
}

    [HttpPost]
    public IActionResult Registro(string nombre, string apellido, string usuario, string clave, string tipo, int id)
{
    Usuario u = new Usuario(nombre, apellido, usuario, clave, tipo, id);
    if (bd.buscarPorNombreUsuario(u.usuario) == null)
    {
       bd.agregarUsuario(u);
        return RedirectToAction("InicioSesion", "Home");
    }
    else
    {
        ViewBag.error = "El nombre de usuario ya existe.";
         return RedirectToAction("Registro", "Home");
    }
    
}
    [HttpPost]
public IActionResult InicioSesion(string usuario, string clave)
{
    Usuario usuarioEncontrado = bd.encontrarUsuario(usuario, clave);

    if (usuarioEncontrado == null)
    {
        ViewBag.Error = "Usuario o contraseña incorrectos.";
        return View();
    }
    HttpContext.Session.SetString("usuario", usuarioEncontrado.usuario);
   
    return RedirectToAction("PaginaPrincipal", "Home");
}

    

    public IActionResult PaginaPrincipal()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("usuario")))
        {
            
            return RedirectToAction("InicioSesion", "Home");
        }
        return View();

        
    }
    public IActionResult CerrarSesion()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
