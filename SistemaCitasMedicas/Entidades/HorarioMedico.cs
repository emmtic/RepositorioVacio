using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    /// <summary>
    /// Clase para organizar dias y horarios de atencion que seran usados por cada medico
    /// </summary>
    public class HorarioMedico
    {
        private int idHorarioMedico;
        private int idMedico;
        private DayOfWeek dia;
        private string horaInicio_A;
        private string horaFin_A;
        private string horaInicio_B;
        private string horaFin_B;
        private int duracionTurnos;

        private List<int> listTurnosMinutos = null;
        private List<string> listTurnosString = null;

        public int IdHorarioMedico { get => idHorarioMedico; set => idHorarioMedico = value; }
        public int IdMedico { get => idMedico; set => idMedico = value; }
        public DayOfWeek Dia { get => dia; set => dia = value; }
        public string HoraInicio_A { get => horaInicio_A; set => horaInicio_A = value; }
        public string HoraFin_A { get => horaFin_A; set => horaFin_A = value; }
        public string HoraInicio_B { get => horaInicio_B; set => horaInicio_B = value; }
        public string HoraFin_B { get => horaFin_B; set => horaFin_B = value; }
        public int DuracionTurnos { get => duracionTurnos; set => duracionTurnos = value; }


        //Metodos privados secundarios
        private int ConvertDeStringMin(string horaString)
        {
            string[] arrayString = new string[2];
            int[] arrayHoraMin = new int[2];
            int minutos;

            arrayString = horaString.Replace(" ", "").Split(':');  //limpio espacios por las dudas con replace y con split separo en 2 a la cadena, seran hora y minuto

            arrayHoraMin[0] = int.Parse(arrayString[0]);
            arrayHoraMin[1] = int.Parse(arrayString[1]);

            return minutos = (arrayHoraMin[0] * 60) + arrayHoraMin[1];
        }

        private string ConvertHoraAString(int horaenMin)
        {
            int horas;
            float minutos;

            horas = horaenMin / 60;
            minutos = (((float)horaenMin / 60) - horas) * 60; //aplicando matematica (distribucion) igual a (float)horaenMin - (horas*60)

            if (horas < 10)
            {
                if (minutos < 10) return $"0{horas}:0{minutos}";
                else return $"0{horas}:{minutos}";
            }
            else
            {
                if (minutos < 10) return $"{horas}:0{minutos}";
                else return $"{horas}:{minutos}";
            }
        }

        private void ListarTurnosEnMinutos(string horaIni, string horaFin)
        {
            int horaInienMin = ConvertDeStringMin(horaIni);
            int horaFinenMin = ConvertDeStringMin(horaFin);

            while (horaInienMin < horaFinenMin)
            {
                this.listTurnosMinutos.Add(horaInienMin);     //si lo pongo a este despues de la sentencia de abajo, agregaria al horario de cierre en la lista de turnos
                horaInienMin = horaInienMin + this.DuracionTurnos;
            }
        }


        //METODO PUBLICO funcion principal
        public List<string> ListarTurnosString()
        {

            if ((!String.IsNullOrEmpty(this.HoraInicio_A)) && (!String.IsNullOrEmpty(this.HoraFin_A)))
            {
                this.listTurnosString = new List<string>();
                this.listTurnosMinutos = new List<int>();
                ListarTurnosEnMinutos(this.HoraInicio_A, this.HoraFin_A);

                if ((!String.IsNullOrEmpty(this.HoraInicio_B)) && (!String.IsNullOrEmpty(this.HoraFin_B))) //El segundo rango de horario es opcional
                {
                    ListarTurnosEnMinutos(this.HoraInicio_B, this.HoraFin_B);
                }

                for (int i = 0; i < this.listTurnosMinutos.Count; i++)
                {
                    this.listTurnosString.Add(ConvertHoraAString(this.listTurnosMinutos[i]));
                }

            }
            else
            {
                Console.WriteLine("Sin horarios cargados");
            }

            return this.listTurnosString;
        }

        public string getdaySpanish()
        {
            switch ((int)this.Dia)
            {
                case 1:
                    return "Lunes";
                case 2:
                    return "Martes";
                case 3:
                    return "Miercoles";
                case 4:
                    return "Jueves";
                case 5:
                    return "Viernes";
                case 6:
                    return "Sabado";
                case 0:
                    return "Domingo";
                default:
                    return "";
            }
        }

        public override string ToString()
        {
            if ((String.IsNullOrEmpty(this.HoraInicio_B)) && (String.IsNullOrEmpty(this.HoraFin_B)))
            {
                return $"{getdaySpanish()}: De {HoraInicio_A} a {HoraFin_A} hs.";
            }
            else
            {
                return $"{getdaySpanish()}: De {HoraInicio_A} a {HoraFin_A} hs. y {HoraInicio_B} a {HoraFin_B} hs.";
            }

        }
    }
}
