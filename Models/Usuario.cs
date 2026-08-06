namespace TP05.Models;
public class Usuario
{
    public string nombre {get; set;}
    public string apellido {get; set;}
    public string usuario {get; set;}
    public string clave {get; set;}
    public string tipo {get; set;}
    public int id {get; set;}
    
    public Usuario (string nombre, string apellido, string usuario, string clave, string tipo, int id)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.usuario = usuario;
        this.clave = clave;
        this.tipo = tipo;
        this.id = id;
    }
    public Usuario ()
    {

    }
}