namespace TP05.Models;
using Microsoft.Data.SqlClient;
using Dapper;
public class BD 
{
    private string conexion = @"Server=localhost;DataBase=TP05; Integrated Security=True; TrustServerCertificate=True;";
    public void agregarUsuarios (Usuario u)
    {
       string query = "INSERT INTO Usuario (id,nombre,apellido,usuario,clave,tipo) VALUES (@nombre,@apellido,@usuario,@clave,@tipo,@id)";
       using(SqlConnection connection = new SqlConnection(_connectionString) )
       {
        connection.Execute(query,new{nombre = u.nombre, apellido = u.apellido, usuario = u.usuario, clave = u.clave, tipo = u.tipo, id = u.id});  
       }
    }
}