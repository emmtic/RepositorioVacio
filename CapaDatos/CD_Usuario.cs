using CapaDatos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Usuario
    {
        public CD_Usuario()
        {
        }


        CD_Conexion conexion = new CD_Conexion();


        public List<UsuarioDatos> GetUsuarios()
        {
            MySqlDataReader leer;
            //DataTable tabla = new DataTable();
            MySqlCommand comando = new MySqlCommand();

            List<UsuarioDatos> UsuariosDelaBD = new List<UsuarioDatos>();
            UsuarioDatos UsuarioBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM usuario";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                UsuarioBD = new UsuarioDatos();
                UsuarioBD.ID_Usuario = Convert.ToInt32(leer["id_usuario"]);
                UsuarioBD.NombreUsuario = leer["nombre_usuario"].ToString();
                UsuarioBD.Nombre = leer["nombre"].ToString();
                UsuarioBD.Apellido = leer["apellido"].ToString();
                UsuarioBD.Email = leer["email"].ToString();
                UsuarioBD.EsAdministrador = Convert.ToBoolean(leer["es_admin"]);
                UsuarioBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                UsuarioBD.FechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                UsuariosDelaBD.Add(UsuarioBD);
            }
            conexion.CerrarConexion();
            return UsuariosDelaBD;
        }

        public void InsertarUsuarios(UsuarioDatos usuario) {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "INSERT INTO usuario " + 
                "VALUES (@nombre_usuario,@nombre,@apellido,@email,@contraseña,@es_activo,@es_admin,@fecha_alta)";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;
             
            comando.Parameters.Add("@nombre_usuario", MySqlDbType.VarChar).Value= usuario.NombreUsuario;
            comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = usuario.Nombre;
            comando.Parameters.Add("@apellido", MySqlDbType.VarChar).Value = usuario.Apellido;
            comando.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
            comando.Parameters.Add("@contraseña", MySqlDbType.VarChar).Value = usuario.Contraseña;
            comando.Parameters.Add("@es_activo", MySqlDbType.Int32).Value = usuario.EsActivo;
            comando.Parameters.Add("@es_admin", MySqlDbType.Int32).Value = usuario.EsAdministrador;
            comando.Parameters.Add("@fecha_alta", MySqlDbType.DateTime).Value = usuario.FechaDeAlta;

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }

        public void ModificarUsuarios(UsuarioDatos usuario)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "UPDATE usuario " +
                "SET nombre_usuario=@nombre_usuario,nombre=@nombre,apellido=@apellido," +
                "email=@email,contraseña=@contraseña,es_activo=@es_activo,es_admin=@es_admin," +
                "fecha_alta=@fecha_alta) WHERE id_Usuario=@id_usuario";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.Add("@id_usuario", MySqlDbType.Int32).Value = usuario.ID_Usuario;
            comando.Parameters.Add("@nombre_usuario", MySqlDbType.VarChar).Value = usuario.NombreUsuario;
            comando.Parameters.Add("@nombre", MySqlDbType.VarChar).Value = usuario.Nombre;
            comando.Parameters.Add("@apellido", MySqlDbType.VarChar).Value = usuario.Apellido;
            comando.Parameters.Add("@email", MySqlDbType.VarChar).Value = usuario.Email;
            comando.Parameters.Add("@contraseña", MySqlDbType.VarChar).Value = usuario.Contraseña;
            comando.Parameters.Add("@es_activo", MySqlDbType.Int32).Value = usuario.EsActivo;
            comando.Parameters.Add("@es_admin", MySqlDbType.Int32).Value = usuario.EsAdministrador;
            comando.Parameters.Add("@fecha_alta", MySqlDbType.DateTime).Value = usuario.FechaDeAlta;

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }
        public void EliminarUsuario(int idUsuario) {
            MySqlCommand comando = new MySqlCommand();

            string cadena = "DELETE FROM usuario WHERE id_usuario=@idUsuario";

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = cadena;
            comando.ExecuteNonQuery();

            comando.Connection = conexion.CerrarConexion();
        }
    }
}