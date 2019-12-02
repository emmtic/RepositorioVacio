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
using Entidades;
using CapaCitasMedicas;

namespace CapaPresentacion.Vistas.CP_Reserva
{
    /// <summary>
    /// Lógica de interacción para ListaPacientes.xaml
    /// </summary>
    public partial class ListaPacientes : Window
    {
        private List<Paciente> listPacientes = null;
        private List<Paciente> lstauxPac = null;
        public ListaPacientes(List<Paciente> reclistPacientes)
        {
            InitializeComponent();
            this.listPacientes = reclistPacientes;
            Datosparainiciar();
        }

        private void Datosparainiciar()
        {
            txtboxDnipac.Text = "";
            txtblockPaciente.Text = "";
            try
            {
                dtgridPacientes.ItemsSource = this.listPacientes;
                dtgridPacientes.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datagrid: " + ex);
                throw;
            }

        }

        private void txtboxDni_TextChanged(object sender, TextChangedEventArgs e) //Para el filtro del Dni
        {
            //List<Paciente> lstauxPac = null;
            if (this.listPacientes != null)
            {
                if (txtboxDnipac.Text == "") //(Preferentemente iniciar los textbox de busqueda vacios) Ojo! si le agrego un placeholder deberia ir aqui tambien con un || para que cargue desde el principio ya que estos textbox son los que inician primero (initializ..) y mas el cambio de tener
                {                                 // un texto cargado lo activaria primero y puede no haber cargado la lista todavia y abajo empieza a buscar en una lista NULA y da error
                    txtblockPaciente.Text = "";
                    dtgridPacientes.ItemsSource = null;
                    dtgridPacientes.ItemsSource = this.listPacientes;
                }
                else
                {
                    //Paciente oPac = listPacientes.Find(x => x.dni.Contains(txtboxFiltroDni.Text.Replace(" ", "")) || x.apellido.Contains(txtboxFiltroDni.Text.Replace(" ", "")));
                    //if (oPac != null) txtblockPaciente.Text = FormatoPac(oPac.id_paciente, oPac.apellido, oPac.nombre, oPac.dni);

                    lstauxPac = this.listPacientes.FindAll(x => x.dni.Contains(txtboxDnipac.Text.Replace(" ", "")));
                    dtgridPacientes.ItemsSource = null;
                    dtgridPacientes.ItemsSource = lstauxPac;
                }
            }

        }

        private void dtgridPacientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Paciente oPac = null;
            oPac = (Paciente)dtgridPacientes.SelectedItem;
            if (oPac != null)
            {
                txtblockPaciente.Text = FormatoPac(oPac.id_paciente, oPac.apellido, oPac.nombre, oPac.dni);
            }
        }

        private string FormatoPac(int id, string apellido, string nombre, string dni) //Para armar las cadenas de string para la presentacion de esta ventana en el combobox sea paciente
        {
            return $"Paciente: {apellido} {nombre}, DNI: {dni}, Cod-{id}";
        }

        public string getPacString()
        {
            return txtblockPaciente.Text;
        }

        private void btnAgregarPac_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
