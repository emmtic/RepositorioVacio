using MySql.Data.MySqlClient;
using System.Data;

namespace CapaDatos
{
    public class CD_Conexion
    {
        private MySqlConnection Conexion = new MySqlConnection("Server=localhost; DataBase=pruebalibromedico; User=root; Password=mysqle123");
        public MySqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }
        public MySqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}
