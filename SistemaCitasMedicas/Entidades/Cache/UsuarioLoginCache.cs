using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Cache
{
    public static class UsuarioLoginCache
    {
        public static int ID_Usuario { get; set; }
        public static string NombreUsuario { get; set; }
        public static string Nombre { get; set; }
        public static string Apellido { get; set; }
        public static string Contraseña { get; set; }
        public static string Email { get; set; }
        public static bool EsAdministrador { get; set; }
        public static bool EsActivo { get; set; }
    }
}
