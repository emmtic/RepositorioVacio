using Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace CapaDatos
{
    public class CD_Medico
    {
        CD_Conexion conexion = new CD_Conexion();


        public List<Medico> GetMedicos()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Medico> MedicosDelaBD = new List<Medico>();
            Medico MedicoBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id_medico,matricula,dni,nombre,apellido," +
            "genero,fecha_nac,email,direccion,telefono,es_activo,especialidad,fecha_alta"+
            " FROM medico inner join especialidad USING (id_especialidad)";
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                MedicoBD = new Medico();
                MedicoBD.id_medico = Convert.ToInt32(leer["id_medico"]);
                MedicoBD.matricula = leer["matricula"].ToString();
                MedicoBD.dni = leer["dni"].ToString();
                MedicoBD.nombre = leer["nombre"].ToString();
                MedicoBD.apellido = leer["apellido"].ToString();
                MedicoBD.genero = leer["genero"].ToString();
                MedicoBD.fechadenacimiento = Convert.ToDateTime(leer["fecha_nac"]);
                MedicoBD.email = leer["email"].ToString();
                MedicoBD.direccion = leer["direccion"].ToString();
                MedicoBD.telefono = leer["telefono"].ToString();
                MedicoBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                MedicoBD.especialidad = leer["especialidad"].ToString();
                MedicoBD.fechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                MedicosDelaBD.Add(MedicoBD);
            }
            conexion.CerrarConexion();
            return MedicosDelaBD;
        }
        public List<string> GetCampos()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<string> CamposDeTabla = new List<string>();
            string CampoDeTabla;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT column_name "+
            "FROM INFORMATION_SCHEMA.COLUMNS "+
            "WHERE TABLE_NAME = 'medico'";

            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                CampoDeTabla = leer["column_name"].ToString();
                if (CampoDeTabla == "id_especialidad")
                {
                    CampoDeTabla = "especialidad";
                }
                CamposDeTabla.Add(CampoDeTabla);
            }
            conexion.CerrarConexion();
            return CamposDeTabla;
        }
        public List<Medico> BuscarMedicoCampo(string campo,string buscado)
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<Medico> MedicosDelaBD = new List<Medico>();
            Medico MedicoBD;
            

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT id_medico,matricula,dni,nombre,apellido," +
            "genero,fecha_nac,email,direccion,telefono,es_activo,especialidad,fecha_alta" +
            " FROM medico inner join especialidad USING (id_especialidad) "+
            "WHERE "+campo+"=@buscado";

            comando.Parameters.AddWithValue("@buscado", buscado);
            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                MedicoBD = new Medico();
                MedicoBD.id_medico = Convert.ToInt32(leer["id_medico"]);
                MedicoBD.matricula = leer["matricula"].ToString();
                MedicoBD.dni = leer["dni"].ToString();
                MedicoBD.nombre = leer["nombre"].ToString();
                MedicoBD.apellido = leer["apellido"].ToString();
                MedicoBD.genero = leer["genero"].ToString();
                MedicoBD.fechadenacimiento = Convert.ToDateTime(leer["fecha_nac"]);
                MedicoBD.email = leer["email"].ToString();
                MedicoBD.direccion = leer["direccion"].ToString();
                MedicoBD.telefono = leer["telefono"].ToString();
                MedicoBD.EsActivo = Convert.ToBoolean(leer["es_activo"]);
                MedicoBD.especialidad = leer["especialidad"].ToString();
                MedicoBD.fechaDeAlta = Convert.ToDateTime(leer["fecha_alta"]);
                MedicosDelaBD.Add(MedicoBD);
            }
            conexion.CerrarConexion();
            return MedicosDelaBD;
        }
        public List<string> Especialidades_Medico()
        {
            MySqlDataReader leer;
            MySqlCommand comando = new MySqlCommand();

            List<string> EspecialidadesBD = new List<string>();
            string EspecialidadBD;

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "SELECT especialidad FROM especialidad";

            leer = comando.ExecuteReader();
            while (leer.Read())
            {
                EspecialidadBD = leer["especialidad"].ToString();
                EspecialidadesBD.Add(EspecialidadBD);
            }
            conexion.CerrarConexion();
            return EspecialidadesBD;
        }
        public void InsertarMedico(Medico medico)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "INSERT INTO medico " +
            "(matricula,dni,nombre,apellido,genero,fecha_nac,email,direccion,"+
            "telefono,es_activo,id_especialidad,fecha_alta) " +
            "VALUES (@matricula,@dni,@nombre,@apellido,@genero,@fecha_nac,@email,@direccion,@telefono,"+
            "@es_activo,(SELECT id_especialidad FROM especialidad " +
            "WHERE especialidad=@especialidad),@fecha_alta)";
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.AddWithValue("@matricula", medico.matricula);
            comando.Parameters.AddWithValue("@dni", medico.dni);
            comando.Parameters.AddWithValue("@nombre", medico.nombre);
            comando.Parameters.AddWithValue("@apellido", medico.apellido);
            comando.Parameters.AddWithValue("@genero", medico.genero);
            comando.Parameters.AddWithValue("@fecha_nac", medico.fechadenacimiento);
            comando.Parameters.AddWithValue("@email", medico.email);
            comando.Parameters.AddWithValue("@direccion", medico.direccion);
            comando.Parameters.AddWithValue("@telefono", medico.telefono);
            comando.Parameters.AddWithValue("@es_activo", medico.EsActivo);
            comando.Parameters.AddWithValue("@especialidad", medico.especialidad);
            
            comando.Parameters.AddWithValue("@fecha_alta", medico.fechaDeAlta);

            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }

        public void ModificarMedico(Medico medico)
        {
            MySqlCommand comando = new MySqlCommand();
            string Cadena = "UPDATE medico " +
                "SET matricula=@matricula," +
                "dni=@dni," +
                "nombre=@nombre," +
                "apellido=@apellido," +
                "genero=@genero," +
                "fecha_nac=@fecha_nac," +
                "email=@email," +
                "direccion=@direccion," +
                "telefono=@telefono," +
                "id_especialidad=(SELECT id_especialidad FROM especialidad " +
                "WHERE especialidad=@especialidad),"+
                "fecha_alta=@fecha_alta " +
                "WHERE id_medico=@id_medico";

            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = Cadena;

            comando.Parameters.AddWithValue("@id_medico", medico.id_medico);
            comando.Parameters.AddWithValue("@matricula", medico.matricula);
            comando.Parameters.AddWithValue("@dni", medico.dni);
            comando.Parameters.AddWithValue("@nombre", medico.nombre);
            comando.Parameters.AddWithValue("@apellido", medico.apellido);
            comando.Parameters.AddWithValue("@genero", medico.genero);
            comando.Parameters.AddWithValue("@fecha_nac", medico.fechadenacimiento);
            comando.Parameters.AddWithValue("@email", medico.email);
            comando.Parameters.AddWithValue("@direccion", medico.direccion);
            comando.Parameters.AddWithValue("@telefono", medico.telefono);
            comando.Parameters.AddWithValue("@es_activo", medico.EsActivo);
            comando.Parameters.AddWithValue("@especialidad", medico.especialidad);
            
            comando.Parameters.AddWithValue("@fecha_alta", medico.fechaDeAlta);


            comando.ExecuteNonQuery();
            comando.Connection = conexion.CerrarConexion();

        }
        public void EliminarMedico(string idMedico)
        {
            MySqlCommand comando = new MySqlCommand();

            string cadena = "UPDATE medico SET es_activo=false WHERE id_medico=@id_medico";

            comando.Parameters.AddWithValue("@id_medico", idMedico);

            comando.Connection = conexion.AbrirConexion();

            comando.CommandText = cadena;
            comando.ExecuteNonQuery();

            comando.Connection = conexion.CerrarConexion();
        }
    }
}
