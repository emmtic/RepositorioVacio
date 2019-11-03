﻿using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaCitaMedica
{
    public class CCM_Usuario
    {
        private CD_Usuario ObjetoCD = new CD_Usuario();

        public List<CD_Usuario> MostrarUsuarios()
        {
            List<CD_Usuario> Usuarios = new List<CD_Usuario>();
            Usuarios = ObjetoCD.GetUsuarios();
            return Usuarios;

        }

    }
}