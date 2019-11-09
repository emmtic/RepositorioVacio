using CapaDatos;
using Entidades;
using System.Collections.Generic;

namespace CapaCitaMedica
{
    public class CCM_Usuario
    {
        private CD_Usuario ObjetoCD = new CD_Usuario();
        public List<Usuario> MostrarUsuarios()
        {
            List<Usuario> Usuarios = new List<Usuario>();
            Usuarios = ObjetoCD.GetUsuarios();
            return Usuarios;
        }

        public void InsertarUsuarios(Usuario UsuarioAdd) {

            ObjetoCD.InsertarUsuarios(UsuarioAdd);
        }

        public void ModificarUsuarios(Usuario UsuarioAdd) {
            ObjetoCD.ModificarUsuarios(UsuarioAdd);
        }

        public void EliminarUsuarios(string IdUser) {
            ObjetoCD.EliminarUsuario(IdUser);
        }
    }
}
