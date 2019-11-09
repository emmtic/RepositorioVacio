using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaCitaMedica.Clases;
using CapaDatos;
using CapaDatos.Entidades;
namespace CapaCitaMedica
{
    public class CCM_Usuario
    {
        private CD_Usuario ObjetoCD = new CD_Usuario();

        public List<Usuario> MostrarUsuarios()
        {
            List<Usuario> Usuarios = new List<Usuario>();
            //Usuarios = ObjetoCD.GetUsuarios();
            return Usuarios;

        }

    }
}