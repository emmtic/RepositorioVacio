using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Entidades;
namespace CapaDatos
{
    public class CD_Usuario
    {
        CD_Conexion conexion = new CD_Conexion();


        public List<Usuario> GetUsuarios()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Usuario> UsuariosDelaBD = new List<Usuario>();
            Usuario UsuarioBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM usuario";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                UsuarioBD = new Usuario();
                UsuarioBD.ID_Usuario = Convert.ToInt32(leer["id_usuario"]);
                UsuarioBD.NombreUsuario = leer["nombre_usuario"].ToString();
                UsuarioBD.Nombre = leer["nombre"].ToString();
                UsuarioBD.Apellido = leer["apellido"].ToString();
                UsuarioBD.Email = leer["email"].ToString();
                UsuarioBD.Contraseña = leer["contraseña"].ToString();
                UsuarioBD.EsAdministrador = Convert.ToBoolean(leer["es_admin"]);
                UsuarioBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                UsuarioBD.FechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                UsuariosDelaBD.Add(UsuarioBD);
            }
            conexion.CerrarConexion();
            return UsuariosDelaBD;
        }

        public void InsertarUsuarios(Usuario usuario)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "INSERT INTO usuario " +
                "(nombre_usuario,nombre,apellido,email,contraseña,es_activo,es_admin,fecha_alta) " +
                "VALUES (@nombre_usuario,@nombre,@apellido,@email,@contraseña,@es_activo,@es_admin,@fecha_alta)";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            
            comando.Parameters.AddWithValue("@nombre_usuario", usuario.NombreUsuario);
            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
            comando.Parameters.AddWithValue("@es_activo", usuario.EsActivo) ;
            comando.Parameters.AddWithValue("@es_admin", usuario.EsAdministrador);
            comando.Parameters.AddWithValue("@fecha_alta", usuario.FechaDeAlta);

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }

        public void ModificarUsuarios(Usuario usuario)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "UPDATE usuario " +
                "SET nombre_usuario=@nombre_usuario,nombre=@nombre,apellido=@apellido," +
                "email=@email,contraseña=@contraseña,es_activo=@es_activo,es_admin=@es_admin," +
                "fecha_alta=@fecha_alta WHERE id_usuario=@id_usuario";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

           
            comando.Parameters.AddWithValue("@nombre_usuario", usuario.NombreUsuario);
            comando.Parameters.AddWithValue("@nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@apellido", usuario.Apellido);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@contraseña", usuario.Contraseña);
            comando.Parameters.AddWithValue("@es_activo", usuario.EsActivo);
            comando.Parameters.AddWithValue("@es_admin", usuario.EsAdministrador);
            comando.Parameters.AddWithValue("@fecha_alta", usuario.FechaDeAlta);
            comando.Parameters.AddWithValue("@id_usuario", usuario.ID_Usuario);


            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }
        public void EliminarUsuario(string idUsuario)
        {
            MySqlCommand comando = new MySqlCommand();

            string cadena = "DELETE FROM usuario WHERE id_usuario=@idUsuario";

            comando.Parameters.AddWithValue("@idUsuario", idUsuario);
            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = cadena;
            comando.ExecuteNonQuery();

            comando.Connection = conexion.CerrarConexion();
        }
    
}
}
