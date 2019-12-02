using CapaDatos;
using Entidades;
using System.Collections.Generic;


namespace CapaCitasMedicas
{
    public class CCM_Paciente
    {
        private CD_Paciente ObjetoCD = new CD_Paciente();
        public List<Paciente> MostrarPacientes()
        {
            List<Paciente> Pacientes = new List<Paciente>();
            Pacientes = ObjetoCD.GetPacientes();
            return Pacientes;
        }
        public List<Paciente> Pacientes_DNI(string dni)
        {
            List<Paciente> Pacientes_DNI = new List<Paciente>();
            Pacientes_DNI = ObjetoCD.BuscarPacienteDNI(dni);
            return Pacientes_DNI;
        }

        public void InsertarPaciente(Paciente PacienteAdd)
        {

            ObjetoCD.InsertarPaciente(PacienteAdd);
        }

        public void ModificarPaciente(Paciente PacienteAdd)
        {
            ObjetoCD.ModificarPaciente(PacienteAdd);
        }

        public void EliminarPaciente(string IdPaciente)
        {
            ObjetoCD.EliminarPaciente(IdPaciente);
        }
    }
}
