using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public abstract class Persona
    {
        public Persona()
        {
        }
        private string Nombre;
        private string Apellido;
        private string DNI;
        private DateTime FechaDeNacimiento;
        private string Genero;
        private string Email;
        private string Direccion;
        private string Telefono;
        private DateTime FechaDeAlta;
        private bool esActivo;

        public string nombre { get => Nombre; set => Nombre = value; }
        public string apellido { get => Apellido; set => Apellido = value; }
        public string dni { get => DNI; set => DNI = value; }
        public DateTime fechadenacimiento { get => FechaDeNacimiento; set => FechaDeNacimiento = value; }
        public string genero { get => Genero; set => Genero = value; }
        public string direccion { get => Direccion; set => Direccion = value; }
        public string email { get => Email; set => Email = value; }
        public string telefono { get => Telefono; set => Telefono = value; }
        public DateTime fechaDeAlta { get => FechaDeAlta; set => FechaDeAlta = value; }
        public bool EsActivo { get => esActivo; set => esActivo = value; }
        public int CalcularEdad()
        {
            return (DateTime.Today.AddTicks(-FechaDeNacimiento.Ticks).Year - 1);
        }
        public int Antiguedad()
        {
            return (DateTime.Today.AddTicks(-FechaDeAlta.Ticks).Year - 1);
        }
    }
}
