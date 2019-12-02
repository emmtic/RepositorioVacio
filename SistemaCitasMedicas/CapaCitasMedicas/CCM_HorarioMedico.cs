using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using CapaDatos;

namespace CapaCitasMedicas
{
    public class CCM_HorarioMedico
    {
        private CD_HorarioMedico horarioMedicoABM = new CD_HorarioMedico();

        public void Insertar(HorarioMedico objHorario)
        {
            horarioMedicoABM.InsertarHorariosMedico(objHorario);
        }

        public void Modificar(HorarioMedico objHorario)
        {
            horarioMedicoABM.ModificarHorariosMedico(objHorario);
        }

        public void Eliminar(int idMedico, int idDia)
        {
            horarioMedicoABM.EliminarHorariosMedico(idMedico, idDia);
        }


        public List<HorarioMedico> listHorariosMedicos()
        {
            return horarioMedicoABM.GetlistHorariosMedicos();
        }

        public List<HorarioMedico> listHorariosByMed(int idMedico)
        {
            return horarioMedicoABM.GetlistHorariosByIdMed(idMedico);
        }
    }
}
