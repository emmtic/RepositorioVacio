using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos.Entidades
{
    public class UsuarioDatos
    {
        
        public int ID_Usuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public bool EsAdministrador { get; set; }
        public bool EsActivo { get; set; }
        public DateTime FechaDeAlta { get; set; }
    }
}
