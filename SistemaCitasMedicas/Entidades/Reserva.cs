using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase para la reserva del turno medico
    /// </summary>
    public class Reserva
    {
        private string asuntoCita;
        private string dniPaciente;
        private string apellidoPaciente;
        private string nombrePaciente;
        private string matricula;
        private string apellidoMedico;
        private string nombreMedico;
        private string especialidad;
        private string fechaCita;
        private string horaCita;
        private string estadoCita;
        private string observaciones;
        private string sintomas;
        private string enfermedad;
        private string medicamentos;
        private decimal precio;
        private string estadoPago;
        private int idReserva;
        private int idPaciente; 
        private int idMedico; 
        private DateTime fechaAltaCita;
        private string nombreUsuario; 

        public string AsuntoCita { get => asuntoCita; set => asuntoCita = value; }
        public string DniPaciente { get => dniPaciente; set => dniPaciente = value; }
        public string ApellidoPaciente { get => apellidoPaciente; set => apellidoPaciente = value; }
        public string NombrePaciente { get => nombrePaciente; set => nombrePaciente = value; }
        public string Matricula { get => matricula; set => matricula = value; }
        public string ApellidoMedico { get => apellidoMedico; set => apellidoMedico = value; }
        public string NombreMedico { get => nombreMedico; set => nombreMedico = value; }
        public string Especialidad { get => especialidad; set => especialidad = value; }
        public string FechaCita { get => fechaCita; set => fechaCita = value; }
        public string HoraCita { get => horaCita; set => horaCita = value; }
        public string EstadoCita { get => estadoCita; set => estadoCita = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public string Sintomas { get => sintomas; set => sintomas = value; }
        public string Enfermedad { get => enfermedad; set => enfermedad = value; }
        public string Medicamentos { get => medicamentos; set => medicamentos = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public string EstadoPago { get => estadoPago; set => estadoPago = value; }
        public int IdReserva { get => idReserva; set => idReserva = value; }
        public int IdPaciente { get => idPaciente; set => idPaciente = value; } //decia internal en vez de int, solo de prueba
        public int IdMedico { get => idMedico; set => idMedico = value; } //decia internal en vez de int, solo de prueba
        public DateTime FechaAltaCita { get => fechaAltaCita; set => fechaAltaCita = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }  

    }

    /// <summary>
    /// Clase para listar los estados de la reserva de un turno, ej: Pendiente, Cancelada...
    /// </summary>
    public class EstadoCita
    {
        private int idEstadoCita;
        private string estado;

        public int IdEstadoCita { get => idEstadoCita; set => idEstadoCita = value; }
        public string Estado { get => estado; set => estado = value; }

        //constructor
        public EstadoCita(int idEstadoCita, string estado)
        {
            this.IdEstadoCita = idEstadoCita;
            this.Estado = estado;
        }

        public EstadoCita()
        {
        }

        public override string ToString() //Importante por ej para el formato del combobox
        {
            return $"{this.Estado}";
        }
    }


    /// <summary>
    /// Clase para listar los estados de pago de la reserva de un turno, ej: Pagada, Pendiente...
    /// </summary>
    public class EstadoPago
    {
        private int idEstadoPago;
        private string estado;

        public int IdEstadoPago { get => idEstadoPago; set => idEstadoPago = value; }
        public string Estado { get => estado; set => estado = value; }

        public EstadoPago(int idEstadoPago, string estado)
        {
            this.IdEstadoPago = idEstadoPago;
            this.Estado = estado;
        }

        public EstadoPago()
        {
        }

        public override string ToString() //Importante por ej para el formato del combobox
        {
            return $"{this.Estado}";
        }
    }

}
