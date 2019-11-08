using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaCitaMedica.Clases
{
    public class Usuario
    {
        private int id_usuario;
        private string nombreUsuario;
        private string nombre;
        private string apellido;
        private string contraseña;
        private string email;
        private bool esAdministrador;
        private bool esActivo;
        private DateTime fechaDeAlta;

        public int ID_Usuario { get => id_usuario; set => id_usuario = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido { get => apellido; set => apellido = value; }
        public string Contraseña { get => contraseña; set => contraseña = value; }
        public string Email { get => email; set => email = value; }
        public bool EsAdministrador { get => esAdministrador; set => esAdministrador = value; }
        public bool EsActivo { get => esActivo; set => esActivo = value; }
        public DateTime FechaDeAlta { get => fechaDeAlta; set => fechaDeAlta = value; }
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
