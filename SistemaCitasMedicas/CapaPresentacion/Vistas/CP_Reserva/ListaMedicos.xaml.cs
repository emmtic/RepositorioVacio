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
    /// Lógica de interacción para ListaMedicos.xaml
    /// </summary>
    public partial class ListaMedicos : Window
    {
        private List<Medico> listMedicos = null;
        private List<Medico> lstauxMed = null;
        TextBox txtboxMed = null;
        CCM_Especialidades oEspCCM = new CCM_Especialidades();
        private List<Especialidades> listEspecialidades = null;
        private List<string> listEspec = null;
        public ListaMedicos(List<Medico> reclistMedicos, TextBox rectextboxMed)
        {
            InitializeComponent();
            this.listMedicos = reclistMedicos;
            this.txtboxMed = rectextboxMed;
            Datosparainiciar();

        }

        private void Datosparainiciar()
        {
            txtboxMatmed.Text = "";
            
            if (this.txtboxMed.Text == "") //por si el textbox viene vacio o cargado por la opcion de modificar una reserva
            {
                txtblockMedico.Text = "";
            }
            else
            {
                txtblockMedico.Text = txtboxMed.Text;
            }
            
            
            try
            {
                dtgridMedicos.ItemsSource = this.listMedicos;
                dtgridMedicos.IsReadOnly = true;
                this.listEspecialidades = oEspCCM.MostrarEspecialidades();

                for (int i = 0; i < listEspecialidades.Count; i++)
                {
                    cmbboxEspec.Items.Add(listEspecialidades[i].especialidad);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos iniciales (datagrid comboboxs): " + ex);
                throw;
            }

        }

        private void txtboxMatmed_TextChanged(object sender, TextChangedEventArgs e) //Para el filtro del Dni
        {
            //List<Paciente> lstauxPac = null;
            if (this.listMedicos != null)
            {
                if (txtboxMatmed.Text == "") //(Preferentemente iniciar los textbox de busqueda vacios) Ojo! si le agrego un placeholder deberia ir aqui tambien con un || para que cargue desde el principio ya que estos textbox son los que inician primero (initializ..) y mas el cambio de tener
                {                                 // un texto cargado lo activaria primero y puede no haber cargado la lista todavia y abajo empieza a buscar en una lista NULA y da error
                    //txtblockMedico.Text = ""; //creo que es preferible no tocar el texblock en estas animaciones
                    dtgridMedicos.ItemsSource = null;
                    dtgridMedicos.ItemsSource = this.listMedicos;
                }
                else
                {
                    //Paciente oPac = listPacientes.Find(x => x.dni.Contains(txtboxFiltroDni.Text.Replace(" ", "")) || x.apellido.Contains(txtboxFiltroDni.Text.Replace(" ", "")));
                    //if (oPac != null) txtblockPaciente.Text = FormatoPac(oPac.id_paciente, oPac.apellido, oPac.nombre, oPac.dni);

                    lstauxMed = this.listMedicos.FindAll(x => x.matricula.Contains(txtboxMatmed.Text.Replace(" ", "")));
                    dtgridMedicos.ItemsSource = null;
                    dtgridMedicos.ItemsSource = lstauxMed;
                }
            }

        }

        private void cmbboxEspec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbboxEspec.SelectedItem != null)
            {
                lstauxMed = this.listMedicos.FindAll(x => x.especialidad==cmbboxEspec.SelectedItem.ToString().Replace(" ", ""));
                dtgridMedicos.ItemsSource = null;
                dtgridMedicos.ItemsSource = lstauxMed;
            }
            else
            {
                if (cmbboxEspec.Text == "") //(Preferentemente iniciar los textbox de busqueda vacios) Ojo! si le agrego un placeholder deberia ir aqui tambien con un || para que cargue desde el principio ya que estos textbox son los que inician primero (initializ..) y mas el cambio de tener
                {                                 // un texto cargado lo activaria primero y puede no haber cargado la lista todavia y abajo empieza a buscar en una lista NULA y da error
                    dtgridMedicos.ItemsSource = null;
                    dtgridMedicos.ItemsSource = this.listMedicos;
                }
                else
                {
                    lstauxMed = this.listMedicos.FindAll(x => x.especialidad==cmbboxEspec.Text.Replace(" ", ""));
                    if (lstauxMed.Count > 0)
                    {
                        dtgridMedicos.ItemsSource = null;
                        dtgridMedicos.ItemsSource = lstauxMed;
                    }
                    else
                    {
                        dtgridMedicos.ItemsSource = null;
                        dtgridMedicos.ItemsSource = this.listMedicos;
                    }
                    
                }
            }


        }

        private void dtgridMedicos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medico oMed = null;
            oMed = (Medico)dtgridMedicos.SelectedItem;
            if (oMed != null)
            {
                txtboxMed.Text = txtblockMedico.Text = FormatoMed(oMed.id_medico, oMed.apellido, oMed.nombre, oMed.matricula, oMed.especialidad);
            }
        }

        private string FormatoMed(int id, string apellido, string nombre, string medMatric, string medEspec) //Para armar las cadenas de string para la presentacion de esta ventana en el combobox sea medico
        {
            return $"{apellido} {nombre}, esp.: {medEspec}, mat.: {medMatric}, Cod-{id}";
        }

        public string getMedString()
        {
            return txtblockMedico.Text;
        }

        private void btnAgregarMed_Click(object sender, RoutedEventArgs e)
        {
            if (txtblockMedico.Text != "")
            {
                txtboxMed.Text = txtblockMedico.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Seleccione un medico");
            }
            
        }
    }
}
