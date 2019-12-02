using CapaCitasMedicas;
using Entidades;
using System;
using System.Windows;


namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para Especialidades_Medicos.xaml
    /// </summary>
    public partial class Especialidades_Medicos : Window
    {
        public Especialidades_Medicos()
        {
            InitializeComponent();
            MostrarEspecialidades();
            LimpiarCampos();
            
        }
        
        CCM_Especialidades ObjetoCCM = new CCM_Especialidades();

        private string idEspecialidad = null;
        public bool Editar = false;
        private void MostrarEspecialidades()
        {
            dtg_verEspecialidades.ItemsSource = ObjetoCCM.MostrarEspecialidades();
        }

        private void btn_guardar_especialidad_Click(object sender, RoutedEventArgs e)
        {
            Especialidades EspecialidadesAdd = new Especialidades();
            if (Editar == false)
            {
                try
                {
                    EspecialidadesAdd.especialidad = txt_especialidad.Text;

                    ObjetoCCM.InsertarEspecialidad(EspecialidadesAdd);
                    MessageBox.Show("El Registro se Ingreso Satisfactoriamente");
                    MostrarEspecialidades();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No Se Pudo Ingresar el Registro: " + ex.ToString());
                }


            }

            if (Editar == true)
            {

                try
                {
                    EspecialidadesAdd.especialidad = txt_especialidad.Text;
                    EspecialidadesAdd.id_especialidad = Convert.ToInt32(idEspecialidad);
                    ObjetoCCM.ModificarEspecialidad(EspecialidadesAdd);
                    MessageBox.Show("El Registro se Modificó Satisfactoriamente");
                    MostrarEspecialidades();
                    LimpiarCampos();
                    Editar = false;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No Se Pudo Modificar El Registro: " + ex.ToString());
                }
            }
        }
        private void btn_modificar_especialidad_Click(object sender, RoutedEventArgs e)
        {


            if (dtg_verEspecialidades.SelectedCells.Count > 0)
            {

                Editar = true;
                Especialidades EspecialidadesMod = (Especialidades)dtg_verEspecialidades.SelectedItem;
                txt_especialidad.Text = EspecialidadesMod.especialidad;
                idEspecialidad = EspecialidadesMod.id_especialidad.ToString();

                ObjetoCCM.ModificarEspecialidad(EspecialidadesMod);
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Modificar");
            }




        }

        private void btn_borrar_especialidad_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_verEspecialidades.SelectedCells.Count > 0)
            {

                Especialidades EspecialidadesMod = (Especialidades)dtg_verEspecialidades.SelectedItem;
                idEspecialidad = EspecialidadesMod.id_especialidad.ToString();

                ObjetoCCM.EliminarEspecialidad(idEspecialidad);
                MessageBox.Show("El Registro se Eliminó Satisfactoriamente");
                MostrarEspecialidades();
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Eliminar");
            }
        }

        private void LimpiarCampos()
        {
            txt_especialidad.Clear();
        }
    }
}
