using CapaDatos;
using Entidades;
using System;
using System.Collections.Generic;


namespace CapaCitasMedicas
{
    public class CCM_Especialidades
    {
        private CD_Especialidades ObjetoCD = new CD_Especialidades();
        public List<Especialidades> MostrarEspecialidades()
        {
            List<Especialidades> Especialidades = new List<Especialidades>();
            Especialidades = ObjetoCD.GetEspecialidades();
            return Especialidades;
        }
        public void InsertarEspecialidad(Especialidades EspecialidadAdd)
        {

            ObjetoCD.InsertarEspecialidad(EspecialidadAdd);
        }

        public void ModificarEspecialidad(Especialidades EspecialidadAdd)
        {
            ObjetoCD.ModificarEspecialidad(EspecialidadAdd);
        }

        public void EliminarEspecialidad(string IDEspecialidad)
        {
            ObjetoCD.EliminarEspecialidad(IDEspecialidad);
        }
    }
}
