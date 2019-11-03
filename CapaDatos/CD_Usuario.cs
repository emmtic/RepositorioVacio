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
        private string nombreUsuario;
        private string nombre;
        private string apellido;
        private string contraseña;
        private string email;
        private bool esAdministrador;
        private bool esActivo;
        private DateTime fechaDeAlta;

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Email { get => email; set => email = value; }
        public bool EsAdministrador { get => esAdministrador; set => esAdministrador = value; }
        public bool EsActivo { get => esActivo; set => esActivo = value; }
        public DateTime FechaDeAlta { get => fechaDeAlta; set => fechaDeAlta = value; }

        CD_Conexion conexion = new CD_Conexion();

        MySqlDataReader leer;
        DataTable tabla = new DataTable();
        MySqlCommand comando = new MySqlCommand();
        public List<CD_Usuario> GetUsuarios()
        {
            List<CD_Usuario> UsuariosDelaBD = new List<CD_Usuario>();
            CD_Usuario UsuarioBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM usuario";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                UsuarioBD = new CD_Usuario();
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
        public override string ToString()
        {
            return "Nombre De Usuario: " + NombreUsuario +
                " - Nombre: " + Nombre +
                " - Apellido: " + Apellido +
                " - Email: " + Email +
                " - Administrador: " + EsAdministrador +
                " - Activo: " + EsActivo +
                " - Fecha De Alta: " + FechaDeAlta;
        }

    }
}