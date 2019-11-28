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
    /// Lógica de interacción para CP_ReservaRegistros.xaml
    /// </summary>
    public partial class CP_ReservaRegistros : Window
    {
        private List<Reserva> listReservasAll = null;
        private List<Reserva> listReservasFiltros = null;
        private List<Reserva> listReservasFiltrosFechas = null;
        private CCM_Reserva reservaCCM = new CCM_Reserva();
        public CP_ReservaRegistros()
        {
            InitializeComponent();

            DatosParaIniciar();
        }

        private void DatosParaIniciar()
        {
            this.listReservasAll = new List<Reserva>();
            CargaDataGrid();
            txtboxFiltro.Text = "";
            dtpickerFiltro.IsEnabled = false;
        }

        public void CargaDataGrid()
        {
            try
            {
                this.listReservasAll = reservaCCM.ListaReservas_All();
                dtgridRegistrosCita.ItemsSource = this.listReservasAll;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la lista de citas: " + ex.ToString());
            }

        }

        //Metodos eventos
        private void btnVentanaNuevaCita_Click(object sender, RoutedEventArgs e)
        {
            CP_ReservaCarga ventanaNuevaCita = new CP_ReservaCarga();
            ventanaNuevaCita.ShowDialog();
            DatosParaIniciar();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dtgridRegistrosCita.SelectedCells.Count > 0) //dtg_verUsuarios.SelectedCells.Count > 0 
            {
                Reserva oreserva = (Reserva)dtgridRegistrosCita.SelectedItem;
                CP_ReservaCarga ventanaEditarCita = new CP_ReservaCarga(this.listReservasAll, oreserva);
                ventanaEditarCita.ShowDialog();
                DatosParaIniciar();
            }
            else
            {
                MessageBox.Show("No seleccionó ninguna cita");
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dtgridRegistrosCita.SelectedCells.Count > 0) //dtg_verUsuarios.SelectedCells.Count > 0 
                {
                    Reserva oreserva = (Reserva)dtgridRegistrosCita.SelectedItem;
                    reservaCCM.Eliminar(oreserva.IdReserva);
                    MessageBox.Show("Reserva eliminada correctamente");
                    DatosParaIniciar();
                }
                else
                {
                    MessageBox.Show("No seleccionó ninguna cita");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la reserva: " + ex);
                throw;
            }

        }

        private void txtboxFiltro_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.listReservasAll != null) //verifica que exista reservas creadas
            {
                if (txtboxFiltro.Text == "") //(Preferentemente iniciar el txtbox de busqueda vacio) Ojo! si le agrego un placeholder deberia ir aqui tambien con un || para que cargue desde el principio ya que estos textbox son los que inician primero (initializ..) 
                {                           //y mas el cambio de tener un texto cargado lo activaria primero y puede no haber cargado la lista todavia y abajo empieza a buscar en una lista NULA y da error
                    dtgridRegistrosCita.ItemsSource = this.listReservasAll;
                    dtpickerFiltro.IsEnabled = false;
                }
                else
                {
                    dtgridRegistrosCita.ItemsSource = null;
                    this.listReservasFiltros = this.listReservasAll.FindAll(x => x.DniPaciente.Contains(txtboxFiltro.Text.Replace(" ", "")) || x.Matricula.Contains(txtboxFiltro.Text.Replace(" ", "")) || x.NombreUsuario.Contains(txtboxFiltro.Text.Replace(" ", ""))); //Arreglar || x.Usuario.NombreUsuario.Contains(txtboxFiltro.Text.Replace(" ", ""))
                    dtgridRegistrosCita.ItemsSource = this.listReservasFiltros;
                    dtpickerFiltro.IsEnabled = true;
                    if (dtpickerFiltro.SelectedDate != null) filtradoFecha(); //la llamo aqui tambien al filtrado xq cuando cambie la seleccion del txtbox se actualizara el filtro de la fecha seleccionada actual, probar si le saco, se pierde la animacion
                }
            }

        }

        private void dtpickerFiltro_SelectedDateChanged(object sender, SelectionChangedEventArgs args)
        {
            filtradoFecha();
        }

        private void rdBtnFechaFiltro_Checked(object sender, RoutedEventArgs e)
        {
            filtradoFecha();
        }



        private void filtradoFecha()
        {
            if (dtpickerFiltro.SelectedDate != null) //los campos se inicializan antes que las listas y sus eventos change habilitan las funciones entonces por ej los radiobutton habilitados dan problemas al inicio con a listareservas == null
            {
                if (rdBtnFechaSelec.IsChecked == true)
                {
                    dtgridRegistrosCita.ItemsSource = null;
                    this.listReservasFiltrosFechas = this.listReservasFiltros.FindAll(x => x.FechaCita == dtpickerFiltro.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    dtgridRegistrosCita.ItemsSource = this.listReservasFiltrosFechas;
                }
                else if (rdBtnDesdeFecha.IsChecked == true)
                {
                    dtgridRegistrosCita.ItemsSource = null;
                    this.listReservasFiltrosFechas = this.listReservasFiltros.FindAll(x => Convert.ToDateTime(x.FechaCita) >= dtpickerFiltro.SelectedDate.Value);
                    dtgridRegistrosCita.ItemsSource = this.listReservasFiltrosFechas;
                }
                else if (rdBtnTodos.IsChecked == true)
                {
                    dtgridRegistrosCita.ItemsSource = null;
                    this.listReservasFiltrosFechas = this.listReservasAll.FindAll(x => x.DniPaciente.Contains(txtboxFiltro.Text.Replace(" ", "")) || x.Matricula.Contains(txtboxFiltro.Text.Replace(" ", "")) || x.NombreUsuario.Contains(txtboxFiltro.Text.Replace(" ", ""))); //Arreglar || x.Usuario.NombreUsuario.Contains(txtboxFiltro.Text.Replace(" ", ""))
                    dtgridRegistrosCita.ItemsSource = this.listReservasFiltrosFechas;
                }
            }

        }


    }
}
