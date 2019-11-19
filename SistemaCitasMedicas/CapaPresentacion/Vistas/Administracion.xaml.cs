using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Entidades.Cache;
namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para Administracion.xaml
    /// </summary>
    public partial class Administracion : Window
    {
        public Administracion()
        {
            InitializeComponent();
            CargarDatosDelUsuario();
        }
        private void CargarDatosDelUsuario() {
            if (UsuarioLoginCache.EsAdministrador==true) {
                lbl_categoria.Content = "Administrador";
                lbl_nombre.Content = UsuarioLoginCache.Nombre + " " + UsuarioLoginCache.Apellido;
                
            }
            else
            {
                lbl_categoria.Content = "Usuario Estandar";
                lbl_nombre.Content = UsuarioLoginCache.Nombre + " " + UsuarioLoginCache.Apellido;
                btn_abmMedico.IsEnabled = false;
                btn_abmUsuario.IsEnabled = false;

            }
        }

        private void btn_abmUsuario_Click(object sender, RoutedEventArgs e)
        {
            Usuarios VentanaUsuario = new Usuarios();
            VentanaUsuario.ShowDialog();
        }
    }
}
