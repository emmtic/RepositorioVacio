using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace CapaDatos
{
    public class CD_Especialidades
    {
        CD_Conexion conexion = new CD_Conexion();


        public List<Especialidades> GetEspecialidades()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Especialidades> EspecialidadesDelaBdD = new List<Especialidades>();
            Especialidades EspecialidadBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT * FROM especialidad";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                EspecialidadBD = new Especialidades();
                EspecialidadBD.id_especialidad = Convert.ToInt32(leer["id_especialidad"]);
                EspecialidadBD.especialidad = leer["especialidad"].ToString();
                EspecialidadesDelaBdD.Add(EspecialidadBD);
            }
            conexion.CerrarConexion();
            return EspecialidadesDelaBdD;
        }
        public void InsertarEspecialidad(Especialidades especialidad)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "INSERT INTO especialidad (especialidad) " +
            "VALUES (@especialidad)";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.AddWithValue("@especialidad", especialidad.especialidad);

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }

        public void ModificarEspecialidad(Especialidades especialidad)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "UPDATE especialidad " +
                "SET especialidad=@especialidad" +
                " WHERE id_especialidad=@id_especialidad";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.AddWithValue("@id_especialidad", especialidad.id_especialidad);
            comando.Parameters.AddWithValue("@especialidad", especialidad.especialidad);

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }
        public void EliminarEspecialidad(string idEspecialidad)
        {
            MySqlCommand comando = new MySqlCommand();

            string cadena = "DELETE FROM especialidad WHERE id_especialidad=@id_especialidad";

            comando.Parameters.AddWithValue("@id_especialidad", idEspecialidad);

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = cadena;
            comando.ExecuteNonQuery();

            comando.Connection = conexion.CerrarConexion();
        }
    }
}
