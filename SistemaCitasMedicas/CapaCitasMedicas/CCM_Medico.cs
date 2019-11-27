using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using CapaDatos;

namespace CapaCitasMedicas
{
    public class CCM_Medico
    {
        private CD_Medico ObjetoCD = new CD_Medico();
        public List<Medico> MostrarMedicos()
        {
            List<Medico> Medicos = new List<Medico>();
            Medicos = ObjetoCD.GetMedicos();
            return Medicos;
        }
        public List<Medico> Medicos_Matricula(string matricula)
        {
            List<Medico> Medicos_Matricula = new List<Medico>();
            Medicos_Matricula = ObjetoCD.BuscarMedicoMatricula(matricula);
            return Medicos_Matricula;
        }
        public List<string> MostrarEspecialidades()
        {
            List<string> Especialidades = new List<string>();
            Especialidades = ObjetoCD.Especialidades_Medico();
            return Especialidades;
        }

        public void InsertarMedico(Medico MedicoAdd)
        {

            ObjetoCD.InsertarMedico(MedicoAdd);
        }

        public void ModificarMedico(Medico MedicoAdd)
        {
            ObjetoCD.ModificarMedico(MedicoAdd);
        }

        public void EliminarMedico(string IdMedico)
        {
            ObjetoCD.EliminarMedico(IdMedico);
        }
    }
}
