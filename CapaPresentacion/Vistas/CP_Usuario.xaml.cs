using CapaCitaMedica;
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

namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para CP_Usuario.xaml
    /// </summary>
    public partial class CP_Usuario : Window
    {
        CCM_Usuario ObjetoCCM = new CCM_Usuario();

        public CP_Usuario()
        {
            InitializeComponent();
            MostrarUsuarios();
        }
        private void MostrarUsuarios()
        {
            lst_usuario.ItemsSource = ObjetoCCM.MostrarUsuarios();
        }
    }
}
