using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Especialidades
    {
        public Especialidades()
        {
        }
        private int ID_especialidad;
        private string Especialidad;

        public int id_especialidad { get => ID_especialidad; set => ID_especialidad = value; }
        public string especialidad { get => Especialidad; set => Especialidad = value; }
    }
}
