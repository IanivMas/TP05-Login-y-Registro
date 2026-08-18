namespace TP05.Models;
using Microsoft.Data.SqlClient;
using Dapper;
public class BD
{
    private string conexion = @"Server=localhost;DataBase=TP05; Integrated Security=True; TrustServerCertificate=True;";
    public void agregarUsuario (Usuario u)
    {
        string query = "INSERT INTO Usuario (nombre,apellido,usuario,clave,tipo) VALUES (@nombre,@apellido,@usuario,@clave,@tipo)";
        using (SqlConnection connection = new SqlConnection(conexion))
        {
            connection.Execute(query, new {nombre = u.nombre, apellido = u.apellido, usuario = u.usuario, clave = u.clave, tipo = u.tipo });
        }
    }

    public Usuario encontrarUsuario(string usuario, string clave)
    {
        string query = "SELECT id, nombre, apellido, usuario, clave, tipo FROM Usuario WHERE usuario = @usuario AND clave = @clave";
        using (SqlConnection connection = new SqlConnection(conexion))
        {
            return connection.QueryFirstOrDefault<Usuario>(query, new { usuario, clave });
        }
    }

    public Usuario buscarPorNombreUsuario(string usuario)
    {
        string query = "SELECT nombre, apellido, usuario, clave, tipo FROM Usuario WHERE usuario = @usuario";
        using (SqlConnection connection = new SqlConnection(conexion))
        {
            return connection.QueryFirstOrDefault<Usuario>(query, new { usuario });
        }
    }

}