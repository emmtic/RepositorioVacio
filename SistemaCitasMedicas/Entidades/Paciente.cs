using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Paciente : Persona
    {
        public Paciente()
        {
        }
        private int ID_Paciente;
        private string Enfermedad;
        private string Medicamentos;
        private string Alergias;

        public int id_paciente { get => ID_Paciente; set => ID_Paciente = value; }
        public string enfermedad { get => Enfermedad; set => Enfermedad = value; }
        public string medicamentos { get => Medicamentos; set => Medicamentos = value; }
        public string alergias { get => Alergias; set => Alergias = value; }


    }
}
