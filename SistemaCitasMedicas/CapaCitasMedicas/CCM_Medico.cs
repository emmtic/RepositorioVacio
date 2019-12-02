using CapaDatos;
using Entidades;
using System.Collections.Generic;

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
        public List<string> MostrarCampos()
        {
            List<string> Mostrar_Campos = new List<string>();
            Mostrar_Campos = ObjetoCD.GetCampos();
            return Mostrar_Campos;
        }
        public List<Medico> Medicos_Campo_Busqueda(string campo,string matricula)
        {
            List<Medico> Medicos_Busqueda = new List<Medico>();
            Medicos_Busqueda = ObjetoCD.BuscarMedicoCampo(campo,matricula);
            return Medicos_Busqueda;
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
