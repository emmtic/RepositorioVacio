using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace CapaDatos
{
    public class CD_Paciente
    {
        CD_Conexion conexion = new CD_Conexion();


        public List<Paciente> GetPacientes()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Paciente> PacientesDelaBD = new List<Paciente>();
            Paciente PacienteBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM paciente";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                PacienteBD = new Paciente();
                PacienteBD.id_paciente = Convert.ToInt32(leer["id_paciente"]);
                PacienteBD.dni = leer["dni"].ToString();
                PacienteBD.nombre = leer["nombre"].ToString();
                PacienteBD.apellido = leer["apellido"].ToString();
                PacienteBD.genero = leer["genero"].ToString();
                PacienteBD.fechadenacimiento = Convert.ToDateTime(leer["fecha_nac"]);
                PacienteBD.email = leer["email"].ToString();
                PacienteBD.direccion = leer["direccion"].ToString();
                PacienteBD.telefono = leer["telefono"].ToString();
                PacienteBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                PacienteBD.enfermedad = leer["enfermedad"].ToString();
                PacienteBD.medicamentos = leer["medicamentos"].ToString();
                PacienteBD.alergias = leer["alergia"].ToString();
                PacienteBD.fechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                PacientesDelaBD.Add(PacienteBD);
            }
            conexion.CerrarConexion();
            return PacientesDelaBD;
        }
        public List<Paciente> BuscarPacienteDNI(string dni)
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Paciente> PacientesDelaBD = new List<Paciente>();
            Paciente PacienteBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM paciente WHERE dni=@dni";

            comando.Parameters.AddWithValue("@dni", dni);
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                PacienteBD = new Paciente();
                PacienteBD.id_paciente = Convert.ToInt32(leer["id_paciente"]);
                PacienteBD.dni = leer["dni"].ToString();
                PacienteBD.nombre = leer["nombre"].ToString();
                PacienteBD.apellido = leer["apellido"].ToString();
                PacienteBD.genero = leer["genero"].ToString();
                PacienteBD.fechadenacimiento = Convert.ToDateTime(leer["fecha_nac"]);
                PacienteBD.email = leer["email"].ToString();
                PacienteBD.direccion = leer["direccion"].ToString();
                PacienteBD.telefono = leer["telefono"].ToString();
                PacienteBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                PacienteBD.enfermedad = leer["enfermedad"].ToString();
                PacienteBD.medicamentos = leer["medicamentos"].ToString();
                PacienteBD.alergias = leer["alergia"].ToString();
                PacienteBD.fechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                PacientesDelaBD.Add(PacienteBD);
            }
            conexion.CerrarConexion();
            return PacientesDelaBD;
        }

        public void InsertarPaciente(Paciente paciente)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "INSERT INTO paciente " +
                "(dni,nombre,apellido,genero,fecha_nac,email,direccion,telefono,es_activo,enfermedad,medicamentos,alergia,fecha_alta) " +
                "VALUES (@dni,@nombre,@apellido,@genero,@fecha_nac,@email,@direccion,@telefono,@es_activo,@enfermedad,@medicamentos,@alergia,@fecha_alta)";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;


            comando.Parameters.AddWithValue("@dni", paciente.dni);
            comando.Parameters.AddWithValue("@nombre", paciente.nombre);
            comando.Parameters.AddWithValue("@apellido", paciente.apellido);
            comando.Parameters.AddWithValue("@genero", paciente.genero);
            comando.Parameters.AddWithValue("@fecha_nac", paciente.fechadenacimiento);
            comando.Parameters.AddWithValue("@email", paciente.email);
            comando.Parameters.AddWithValue("@direccion", paciente.direccion);
            comando.Parameters.AddWithValue("@telefono", paciente.telefono);
            comando.Parameters.AddWithValue("@es_activo", paciente.EsActivo);
            comando.Parameters.AddWithValue("@enfermedad", paciente.enfermedad);
            comando.Parameters.AddWithValue("@medicamentos", paciente.medicamentos);
            comando.Parameters.AddWithValue("@alergia", paciente.alergias);
            comando.Parameters.AddWithValue("@fecha_alta", paciente.fechaDeAlta);

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }

        public void ModificarPaciente(Paciente paciente)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "UPDATE paciente " +
                "SET dni=@dni,"+
                "nombre=@nombre,"+
                "apellido=@apellido," +
                "genero=@genero,"+
                "fecha_nac=@fecha_nac,"+
                "email=@email,"+
                "direccion=@direccion," +
                "telefono=@telefono,"+
                "enfermedad=@enfermedad,"+
                "medicamentos=@medicamentos,"+
                "alergia=@alergia,"+
                "fecha_alta=@fecha_alta " +
                "WHERE id_paciente=@id_paciente";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.AddWithValue("@id_paciente",paciente.id_paciente);
            comando.Parameters.AddWithValue("@dni", paciente.dni);
            comando.Parameters.AddWithValue("@nombre", paciente.nombre);
            comando.Parameters.AddWithValue("@apellido", paciente.apellido);
            comando.Parameters.AddWithValue("@genero", paciente.genero);
            comando.Parameters.AddWithValue("@fecha_nac", paciente.fechadenacimiento);
            comando.Parameters.AddWithValue("@email", paciente.email);
            comando.Parameters.AddWithValue("@direccion", paciente.direccion);
            comando.Parameters.AddWithValue("@telefono", paciente.telefono);
            comando.Parameters.AddWithValue("@es_activo", paciente.EsActivo);
            comando.Parameters.AddWithValue("@enfermedad", paciente.enfermedad);
            comando.Parameters.AddWithValue("@medicamentos", paciente.medicamentos);
            comando.Parameters.AddWithValue("@alergia", paciente.alergias);
            comando.Parameters.AddWithValue("@fecha_alta", paciente.fechaDeAlta);


            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }
        public void EliminarPaciente(string idPaciente)
        {
            MySqlCommand comando = new MySqlCommand();

            string cadena = "UPDATE paciente SET es_activo=false WHERE id_paciente=@id_paciente";

            comando.Parameters.AddWithValue("@id_paciente", idPaciente);

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = cadena;
            comando.ExecuteNonQuery();

            comando.Connection = conexion.CerrarConexion();
        }
    }
}
