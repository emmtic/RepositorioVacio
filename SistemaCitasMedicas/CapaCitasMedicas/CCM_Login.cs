using CapaDatos;
using Entidades;
using System.Collections.Generic;

namespace CapaCitasMedicas
{
    public class CCM_Login
    {
        CD_Login ObjetoCD = new CD_Login();
        public bool CheckUsuarios(string NombreUser, string Pass) {
           return ObjetoCD.CheckUsuario(NombreUser,Pass);
        }
    }
}
