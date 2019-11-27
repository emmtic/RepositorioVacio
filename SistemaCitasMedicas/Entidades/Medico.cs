using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Medico : Persona
    {
        public Medico()
        {
        }
        private int ID_Medico;
        private string Matricula;
        private string Especialidad;


        public int id_medico { get => ID_Medico; set => ID_Medico = value; }
        public string matricula { get => Matricula; set => Matricula = value; }
        public string especialidad { get => Especialidad; set => Especialidad = value; }



    }
}
