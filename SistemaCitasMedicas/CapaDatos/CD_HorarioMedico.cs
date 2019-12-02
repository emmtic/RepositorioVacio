using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public class CD_HorarioMedico
    {
        //metodos insertar, modificar, quitar de la clase horariomedico
        public void InsertarHorariosMedico(HorarioMedico objHorario)
        {
            string strQuery = "INSERT INTO horariomedico " +  //fijarse bien el nombre de la tabla
                "(id_medico, id_dia, horainicio_a, horafin_a, horainicio_b, horafin_b, " +
                "duracion_turnos)" +
                "VALUES(@idMedico, @idDia, @horaInicioA, @horaFinA, @horaInicioB, @horaFinB, " +
                "@duracionTurnos);";

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;

            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = objHorario.IdMedico;
            comando.Parameters.Add("@idDia", MySqlDbType.Int32).Value = Convert.ToInt32(objHorario.Dia);
            comando.Parameters.Add("@horaInicioA", MySqlDbType.VarChar).Value = objHorario.HoraInicio_A;
            comando.Parameters.Add("@horaFinA", MySqlDbType.VarChar).Value = objHorario.HoraFin_A;
            comando.Parameters.Add("@horaInicioB", MySqlDbType.VarChar).Value = objHorario.HoraInicio_B;
            comando.Parameters.Add("@horaFinB", MySqlDbType.VarChar).Value = objHorario.HoraFin_B;
            comando.Parameters.Add("@duracionTurnos", MySqlDbType.Int32).Value = objHorario.DuracionTurnos;

            comando.ExecuteNonQuery();

            conexion.CerrarConexion();
        }


        public void ModificarHorariosMedico(HorarioMedico objHorario)
        {
            string strQuery = "UPDATE horariomedico " +  //fijarse bien el nombre de la tabla
                "SET horainicio_a=@horaInicioA, horafin_a=@horaFinA, " +
                "horainicio_b=@horaInicioB, horafin_b=@horaFinB, duracion_turnos=@duracionTurnos " +
                "WHERE id_dia=@idDia AND id_medico=@idMedico;";


            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;

            //comando.Parameters.Add("@idhorariomedico", MySqlDbType.Int32).Value = objHorario.IdHorarioMedico;
            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = objHorario.IdMedico;
            comando.Parameters.Add("@idDia", MySqlDbType.Int32).Value = Convert.ToInt32(objHorario.Dia);
            comando.Parameters.Add("@horaInicioA", MySqlDbType.VarChar).Value = objHorario.HoraInicio_A;
            comando.Parameters.Add("@horaFinA", MySqlDbType.VarChar).Value = objHorario.HoraFin_A;
            comando.Parameters.Add("@horaInicioB", MySqlDbType.VarChar).Value = objHorario.HoraInicio_B;
            comando.Parameters.Add("@horaFinB", MySqlDbType.VarChar).Value = objHorario.HoraFin_B;
            comando.Parameters.Add("@duracionTurnos", MySqlDbType.Int32).Value = objHorario.DuracionTurnos;

            comando.ExecuteNonQuery();

            conexion.CerrarConexion();
        }

        public void EliminarHorariosMedico(int idMedico, int idDia)
        {
            string strQuery = "DELETE FROM horariomedico WHERE id_medico=@idMedico AND id_dia=@idDia;"; //fijarse bien el nombre de la tabla

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = idMedico;
            comando.Parameters.Add("@idDia", MySqlDbType.Int32).Value = idDia;
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }

        //metodos con el SELECT
        public List<HorarioMedico> GetlistHorariosMedicos()
        {
            string strQuery = "SELECT * FROM horariomedico ORDER BY id_medico ASC,id_dia ASC;";  //Hacer inner join para traer el dia?
            List<HorarioMedico> listHorario = new List<HorarioMedico>();
            HorarioMedico objHorario;

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            MySqlDataReader rdr = comando.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    objHorario = new HorarioMedico();
                    objHorario.IdHorarioMedico = Convert.ToInt32(rdr["id_horariomedico"]);
                    objHorario.IdMedico = Convert.ToInt32(rdr["id_medico"]);
                    objHorario.Dia = (DayOfWeek)Convert.ToInt32(rdr["id_dia"]); //el Enum.parse devuelve un objeto (?) Enum.Parse(typeof(DayOfWeek), rdr["id_dia"].ToString());
                    objHorario.HoraInicio_A = rdr["horainicio_a"].ToString();
                    objHorario.HoraFin_A = rdr["horafin_a"].ToString();
                    objHorario.HoraInicio_B = rdr["horainicio_b"].ToString();
                    objHorario.HoraFin_B = rdr["horafin_b"].ToString();
                    objHorario.DuracionTurnos = Convert.ToInt32(rdr["duracion_turnos"]);

                    listHorario.Add(objHorario);
                }
            }
            else
            {
                Console.WriteLine("No hay filas cargadas");
            }

            rdr.Close();
            conexion.CerrarConexion();

            return listHorario;
        }

        public List<HorarioMedico> GetlistHorariosByIdMed(int idMedico)
        {
            string strQuery = "SELECT * FROM horariomedico WHERE id_medico=@idMedico ORDER BY id_dia ASC;";  //Hacer inner join para traer el dia?
            List<HorarioMedico> listHorario = new List<HorarioMedico>();
            HorarioMedico objHorario;

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = idMedico;
            MySqlDataReader rdr = comando.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    objHorario = new HorarioMedico();
                    objHorario.IdHorarioMedico = Convert.ToInt32(rdr["id_horariomedico"]);
                    objHorario.IdMedico = Convert.ToInt32(rdr["id_medico"]);
                    objHorario.Dia = (DayOfWeek)Convert.ToInt32(rdr["id_dia"]); //el Enum.parse devuelve un objeto (?) Enum.Parse(typeof(DayOfWeek), rdr["id_dia"].ToString());
                    objHorario.HoraInicio_A = rdr["horainicio_a"].ToString();
                    objHorario.HoraFin_A = rdr["horafin_a"].ToString();
                    objHorario.HoraInicio_B = rdr["horainicio_b"].ToString();
                    objHorario.HoraFin_B = rdr["horafin_b"].ToString();
                    objHorario.DuracionTurnos = Convert.ToInt32(rdr["duracion_turnos"]);

                    listHorario.Add(objHorario);
                }
            }
            else
            {
                Console.WriteLine("No hay filas cargadas");
            }

            rdr.Close();
            conexion.CerrarConexion();

            return listHorario;
        }


    }
}
