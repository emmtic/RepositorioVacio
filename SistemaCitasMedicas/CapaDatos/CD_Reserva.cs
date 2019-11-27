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
    public class CD_Reserva
    {
        //metodos insertar, modificar, quitar de la clase reserva 
        public void InsertarReserva(Reserva oreserva)
        {
            string strQuery = "INSERT INTO reservacion " +  //fijarse bien el nombre de la tabla
                "(asunto_cita, observaciones, fecha_cita, hora_cita, fecha_alta, " +
                "id_paciente, sintomas, enfermedad, medicamentos, id_usuario, id_medico, " +
                "precio, id_pago, id_estado, es_activo) " +
                "VALUES(@asuntoCita, @observaciones, @fechaCita, @horaCita, @fechaAlta, " +
                "@idPaciente, @sintomas, @enfermedad, @medicamentos, (SELECT id_usuario FROM usuario WHERE nombre_usuario = @nombreUsuario), @idMedico, " +
                "@precio, (SELECT id_pago FROM pago WHERE estado_pago=@estadoPago), (SELECT id_estado FROM estado WHERE estado=@estadoCita), @esActivo);";

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;

            comando.Parameters.Add("@asuntoCita", MySqlDbType.VarChar).Value = oreserva.AsuntoCita;
            comando.Parameters.Add("@observaciones", MySqlDbType.Text).Value = oreserva.Observaciones;
            comando.Parameters.Add("@fechaCita", MySqlDbType.VarChar).Value = oreserva.FechaCita;
            comando.Parameters.Add("@horaCita", MySqlDbType.VarChar).Value = oreserva.HoraCita;
            comando.Parameters.Add("@fechaAlta", MySqlDbType.Text).Value = oreserva.FechaAltaCita.ToString("yyyy-MM-dd HH:mm:ss");
            comando.Parameters.Add("@idPaciente", MySqlDbType.Int32).Value = oreserva.IdPaciente; //COMPLETAR CON EL ID PACIENTE
            comando.Parameters.Add("@sintomas", MySqlDbType.Text).Value = oreserva.Sintomas;
            comando.Parameters.Add("@enfermedad", MySqlDbType.Text).Value = oreserva.Enfermedad;
            comando.Parameters.Add("@medicamentos", MySqlDbType.Text).Value = oreserva.Medicamentos;
            comando.Parameters.Add("@nombreUsuario", MySqlDbType.VarChar).Value = oreserva.NombreUsuario;
            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = oreserva.IdMedico; //COMPLETAR CON EL ID PACIENTE
            comando.Parameters.Add("@precio", MySqlDbType.Double).Value = oreserva.Precio; //ver esto si la base de datos tambien puede ser decimal
            comando.Parameters.Add("@estadoPago", MySqlDbType.VarChar).Value = oreserva.EstadoPago; //idpago
            comando.Parameters.Add("@estadoCita", MySqlDbType.VarChar).Value = oreserva.EstadoCita; //idestadoCita
            comando.Parameters.Add("@esActivo", MySqlDbType.Byte).Value = 1; //ingresa como activo

            comando.ExecuteNonQuery();

            conexion.CerrarConexion();
        }


        public void ModificarReserva(Reserva oreserva)
        {
            string strQuery = "UPDATE reservacion " +  //fijarse bien el nombre de la tabla
                "SET asunto_cita=@asuntoCita, observaciones=@observaciones, " +
                "fecha_cita=@fechaCita, hora_cita=@horaCita, fecha_alta=@fechaAlta, " +
                "id_paciente=@idPaciente, sintomas=@sintomas, enfermedad=@enfermedad, medicamentos=@medicamentos, " +
                "id_usuario=(SELECT id_usuario FROM usuario WHERE nombre_usuario = @nombreUsuario), id_medico=@idMedico, precio=@precio, id_pago=(SELECT id_pago FROM pago WHERE estado_pago=@estadoPago), " +
                "id_estado=(SELECT id_estado FROM estado WHERE estado=@estadoCita) " +
                "WHERE id_reservacion=@idReserva";

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;

            comando.Parameters.Add("@idReserva", MySqlDbType.Int32).Value = oreserva.IdReserva;
            comando.Parameters.Add("@asuntoCita", MySqlDbType.VarChar).Value = oreserva.AsuntoCita;
            comando.Parameters.Add("@observaciones", MySqlDbType.Text).Value = oreserva.Observaciones;
            comando.Parameters.Add("@fechaCita", MySqlDbType.VarChar).Value = oreserva.FechaCita;
            comando.Parameters.Add("@horaCita", MySqlDbType.VarChar).Value = oreserva.HoraCita;
            comando.Parameters.Add("@fechaAlta", MySqlDbType.Text).Value = oreserva.FechaAltaCita.ToString("yyyy-MM-dd HH:mm:ss");
            comando.Parameters.Add("@idPaciente", MySqlDbType.Int32).Value = oreserva.IdPaciente; //COMPLETAR CON EL ID PACIENTE
            comando.Parameters.Add("@sintomas", MySqlDbType.Text).Value = oreserva.Sintomas;
            comando.Parameters.Add("@enfermedad", MySqlDbType.Text).Value = oreserva.Enfermedad;
            comando.Parameters.Add("@medicamentos", MySqlDbType.Text).Value = oreserva.Medicamentos;
            comando.Parameters.Add("@nombreUsuario", MySqlDbType.VarChar).Value = oreserva.NombreUsuario;
            comando.Parameters.Add("@idMedico", MySqlDbType.Int32).Value = oreserva.IdMedico; //COMPLETAR CON EL ID PACIENTE
            comando.Parameters.Add("@precio", MySqlDbType.Double).Value = oreserva.Precio; //ver esto si la base de datos tambien puede ser decimal
            comando.Parameters.Add("@estadoPago", MySqlDbType.VarChar).Value = oreserva.EstadoPago; //idpago
            comando.Parameters.Add("@estadoCita", MySqlDbType.VarChar).Value = oreserva.EstadoCita; //idpestadoCita

            comando.ExecuteNonQuery();

            conexion.CerrarConexion();
        }


        public void EliminarReserva(int idReserva)
        {
            string strQuery = "UPDATE reservacion SET es_activo=0 WHERE id_reservacion=@idReserva;"; //fijarse bien el nombre de la tabla

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            comando.Parameters.Add("@idReserva", MySqlDbType.Int32).Value = idReserva;
            comando.ExecuteNonQuery();
            conexion.CerrarConexion();
        }


        //metodos SELECT devolver listas, todas las citas o con la fecha de cita actual 
        public List<Reserva> GetListReservas_All()
        {
            string strQuery = "SELECT r.*, m.apellido as 'apellido_med', m.nombre as 'nombre_med', m.matricula, especialidad.especialidad, " +
                "p.apellido as 'apellido_pac', p.nombre as 'nombre_pac', p.dni, estado.estado, pago.estado_pago, u.nombre_usuario " +
                "FROM reservacion as r INNER join medico as m ON r.id_medico = m.id_medico " +
                "INNER join especialidad ON m.id_especialidad = especialidad.id_especialidad " +
                "INNER join paciente as p ON r.id_paciente = p.id_paciente " +
                "INNER join estado ON r.id_estado = estado.id_estado " +
                "INNER join pago ON r.id_pago = pago.id_pago " +
                "INNER join usuario as u ON r.id_usuario = u.id_usuario " +
                "WHERE r.es_activo=1 " +
                "ORDER BY id_reservacion ASC;";
            //"SELECT * FROM reservacion;";

            return GetListReservaPriv(strQuery);
        }

        public List<Reserva> GetListReservas_Activas() //lista de reservas con su fecha de cita aun no vencida, fecha de cita desde hoy, no sirve para las ediciones, solo para las nuevas
        {
            string strQuery = "SELECT r.*, m.apellido as 'apellido_med', m.nombre as 'nombre_med', m.matricula, especialidad.especialidad, " +
                "p.apellido as 'apellido_pac', p.nombre as 'nombre_pac', p.dni, estado.estado, pago.estado_pago, u.nombre_usuario " +
                "FROM reservacion as r INNER join medico as m ON r.id_medico = m.id_medico " +
                "INNER join especialidad ON m.id_especialidad = especialidad.id_especialidad " +
                "INNER join paciente as p ON r.id_paciente = p.id_paciente " +
                "INNER join estado ON r.id_estado = estado.id_estado " +
                "INNER join pago ON r.id_pago = pago.id_pago " +
                "INNER join usuario as u ON r.id_usuario = u.id_usuario " +
                "WHERE r.es_activo=1 AND STR_TO_DATE(fecha_cita, '%Y-%m-%d') >= date_format(curdate(), '%Y-%m-%d') ORDER BY STR_TO_DATE(fecha_cita, '%Y-%m-%d') ASC;";
            //"SELECT * FROM reservacion WHERE STR_TO_DATE(fecha_cita, '%Y-%m-%d') >= date_format(curdate(), '%Y-%m-%d') ORDER BY STR_TO_DATE(fecha_cita, '%Y-%m-%d') ASC;"; //curdate() fecha actual desde donde esté la base de datos

            return GetListReservaPriv(strQuery);
        }

        private List<Reserva> GetListReservaPriv(string strQuery) //Metodo privado para llamarlo en las funciones de listas posteriores
        {
            List<Reserva> listReservas = new List<Reserva>();
            Reserva oreserva;

            CD_Conexion conexion = new CD_Conexion(); //¿este puede ir afuera del metodo como mysql.comando tambien PERO el cerrar la conexion cuando termine la funcion y no en otra funcion puede ser problema porq son parte de la misma instancia?

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            MySqlDataReader rdr = comando.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    oreserva = new Reserva();
                    oreserva.IdReserva = Convert.ToInt32(rdr["id_reservacion"]);
                    oreserva.AsuntoCita = rdr["asunto_cita"].ToString();
                    oreserva.Observaciones = rdr["observaciones"].ToString();
                    oreserva.FechaCita = rdr["fecha_cita"].ToString();
                    oreserva.HoraCita = rdr["hora_cita"].ToString();
                    oreserva.FechaAltaCita = Convert.ToDateTime(rdr["fecha_alta"]);

                    //un getPacientebyId
                    oreserva.IdPaciente = Convert.ToInt32(rdr["id_paciente"]);   //ATENCION corregir             
                    oreserva.ApellidoPaciente = rdr["apellido_pac"].ToString();
                    oreserva.NombrePaciente = rdr["nombre_pac"].ToString();
                    oreserva.DniPaciente = rdr["dni"].ToString();
                    oreserva.Sintomas = rdr["sintomas"].ToString();
                    oreserva.Enfermedad = rdr["enfermedad"].ToString();
                    oreserva.Medicamentos = rdr["medicamentos"].ToString();

                    //un getUsuariobyId
                    oreserva.NombreUsuario = rdr["nombre_usuario"].ToString();   //ATENCION corregir //Necesitaria un getUsuarioById() 

                    //un getMedicobyId
                    oreserva.IdMedico = Convert.ToInt32(rdr["id_medico"]);     //ATENCION corregir    
                    oreserva.ApellidoMedico = rdr["apellido_med"].ToString();
                    oreserva.NombreMedico = rdr["nombre_med"].ToString();
                    oreserva.Matricula = rdr["matricula"].ToString();
                    oreserva.Especialidad = rdr["especialidad"].ToString();
                    oreserva.Precio = Convert.ToDecimal(rdr["precio"]);       //ATENCION corregir
                    oreserva.EstadoPago = rdr["estado_pago"].ToString(); ; //ATENCION corregir  
                    oreserva.EstadoCita = rdr["estado"].ToString(); ;

                    listReservas.Add(oreserva);
                }
            }
            else
            {
                Console.WriteLine("No hay filas cargadas");
            }

            rdr.Close();
            conexion.CerrarConexion();

            return listReservas;
        }


        //Estado Cita, lista las opciones a cargar
        public List<EstadoCita> GetListEstadosCita()
        {
            string strQuery = "SELECT * FROM estado;";
            List<EstadoCita> listEstadosCita = new List<EstadoCita>();

            CD_Conexion conexion = new CD_Conexion();
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            MySqlDataReader rdr = comando.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    listEstadosCita.Add(new EstadoCita(Convert.ToInt32(rdr["id_estado"]), rdr["estado"].ToString()));
                }
            }
            else
            {
                Console.WriteLine("No hay filas cargadas");
            }

            rdr.Close();
            conexion.CerrarConexion();

            return listEstadosCita;
        }

        public EstadoPago GetEstadoPagoById(int recId) //IMPORTANTE TAL VEZ SIN USO. BORRAR SI NO SE USA Ejemplo de usar el listado traido de la BD, tambien podria ser WHERE id= etc.
        {
            List<EstadoPago> estadoPagos = this.GetListEstadosPago();
            return estadoPagos.Find(x => x.IdEstadoPago == recId);
        }

        //Estado Pago, lista los estados de pago 
        public List<EstadoPago> GetListEstadosPago()
        {
            string strQuery = "SELECT * FROM pago;";
            List<EstadoPago> listEstadosPago = new List<EstadoPago>();

            CD_Conexion conexion = new CD_Conexion();
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = strQuery;
            MySqlDataReader rdr = comando.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    listEstadosPago.Add(new EstadoPago(Convert.ToInt32(rdr["id_pago"]), rdr["estado_pago"].ToString()));
                }
            }
            else
            {
                Console.WriteLine("No hay filas cargadas");
            }

            rdr.Close();
            conexion.CerrarConexion();

            return listEstadosPago;
        }
    }
}
