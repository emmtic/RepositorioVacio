using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Entidades;
using Entidades.Cache;
using System.Windows;
using System.Data;


namespace CapaDatos
{
    public class CD_Login
    {
        CD_Conexion conexion = new CD_Conexion();


        public bool CheckUsuario(string NombreUser, string Contraseña) {

            MySqlCommand comando = new MySqlCommand();
            MySqlDataReader leer;
            using (comando.Connection = conexion.AbrirConexion())
            {
                comando.CommandText = "SELECT * FROM usuario WHERE nombre_usuario=@nombre_usuario AND contraseña=@contraseña";
                comando.Parameters.AddWithValue("@nombre_usuario", NombreUser);
                comando.Parameters.AddWithValue("@contraseña", Contraseña);
                comando.CommandType = CommandType.Text;
                leer = comando.ExecuteReader();
                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        UsuarioLoginCache.ID_Usuario = leer.GetInt32(0);
                        UsuarioLoginCache.NombreUsuario = leer.GetString(1);
                        UsuarioLoginCache.Nombre = leer.GetString(2);
                        UsuarioLoginCache.Apellido = leer.GetString(3);
                        UsuarioLoginCache.Email = leer.GetString(4);
                        UsuarioLoginCache.EsActivo = leer.GetBoolean(6);
                        UsuarioLoginCache.EsAdministrador = leer.GetBoolean(7);
                    }
                    return true;

                }
                else
                {
                    return false;
                }
            }

        }

      
  
    }
}
