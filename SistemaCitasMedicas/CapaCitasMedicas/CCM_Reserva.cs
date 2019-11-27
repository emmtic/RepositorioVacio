using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using CapaDatos;

namespace CapaCitasMedicas
{
    public class CCM_Reserva
    {
        private CD_Reserva reservaABM = new CD_Reserva();

        public void Insertar(Reserva oreserva)
        {
            reservaABM.InsertarReserva(oreserva);
        }

        public void Modificar(Reserva oreserva)
        {
            reservaABM.ModificarReserva(oreserva);
        }

        public void Eliminar(int idReserva)
        {
            reservaABM.EliminarReserva(idReserva);
        }


        public List<Reserva> ListaReservas_All()
        {
            return reservaABM.GetListReservas_All();
        }

        public List<Reserva> ListaReservas_Activas()
        {
            return reservaABM.GetListReservas_Activas();
        }


        public List<EstadoCita> ListaEstadosCita()
        {
            return reservaABM.GetListEstadosCita();
        }

        public List<EstadoPago> ListaEstadosPago()
        {
            return reservaABM.GetListEstadosPago();
        }


    }
}
