using System.Windows;
using Entidades;
using System;
using CapaCitasMedicas;

namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para Pacientes.xaml
    /// </summary>
    public partial class Pacientes : Window
    {

        public Pacientes()
        {
            InitializeComponent();
            MostrarPacientes();
            LimpiarCampos();
            
        }
        CCM_Paciente ObjetoCCM = new CCM_Paciente();
        private string idPaciente = null;
        public bool Editar = false;
        private void MostrarPacientes()
        {
            dtg_verPacientes.ItemsSource = ObjetoCCM.MostrarPacientes();
        }

        private void btn_agregarPaciente_Click(object sender, RoutedEventArgs e)
        {
            Paciente PacienteAdd = new Paciente();
            if (ValidarCampos())
            {
                if (Editar == false)
                {
                    try
                    {
                        PacienteAdd.dni = txt_dni.Text;
                        PacienteAdd.nombre = txt_nombre.Text;
                        PacienteAdd.apellido = txt_apellido.Text;
                        PacienteAdd.email = txt_email.Text;
                        PacienteAdd.direccion = txt_direccion.Text;
                        PacienteAdd.telefono = txt_telefono.Text;

                        if (cmb_genero.Text == "Masculino")
                        {
                            PacienteAdd.genero = "m";
                        }
                        else
                        {
                            PacienteAdd.genero = "f";
                        }

                        PacienteAdd.fechadenacimiento = dt_fecha_nac.SelectedDate.Value;

                        if (cmb_EstadoPaciente.Text == "Habilitado")
                        {
                            PacienteAdd.EsActivo = true;
                        }
                        else
                        {
                            PacienteAdd.EsActivo = false;
                        }

                        PacienteAdd.enfermedad = txt_enfermedad.Text;
                        PacienteAdd.medicamentos = txt_medicamentos.Text;
                        PacienteAdd.alergias = txt_alergia.Text;

                        PacienteAdd.fechaDeAlta = dt_fechaAlta.SelectedDate.Value;

                       
                        ObjetoCCM.InsertarPaciente(PacienteAdd);
                        MessageBox.Show("El Registro se Ingreso Satisfactoriamente");
                        MostrarPacientes();
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
                        PacienteAdd.dni = txt_dni.Text;
                        PacienteAdd.nombre = txt_nombre.Text;
                        PacienteAdd.apellido = txt_apellido.Text;
                        PacienteAdd.email = txt_email.Text;
                        PacienteAdd.direccion = txt_direccion.Text;
                        PacienteAdd.telefono = txt_telefono.Text;

                        if (cmb_genero.Text == "Masculino")
                        {
                            PacienteAdd.genero = "m";
                        }
                        else
                        {
                            PacienteAdd.genero = "f";
                        }

                        PacienteAdd.fechadenacimiento = dt_fecha_nac.SelectedDate.Value;

                        if (cmb_EstadoPaciente.Text == "Habilitado")
                        {
                            PacienteAdd.EsActivo = true;
                        }
                        else
                        {
                            PacienteAdd.EsActivo = false;
                        }

                        PacienteAdd.enfermedad = txt_enfermedad.Text;
                        PacienteAdd.medicamentos = txt_medicamentos.Text;
                        PacienteAdd.alergias = txt_alergia.Text;

                        PacienteAdd.fechaDeAlta = dt_fechaAlta.SelectedDate.Value;

                        PacienteAdd.id_paciente = Convert.ToInt32(idPaciente);
                        if (ValidarCampos())
                        {
                            ObjetoCCM.ModificarPaciente(PacienteAdd);
                            MessageBox.Show("El Registro se Modificó Satisfactoriamente");
                            MostrarPacientes();
                            LimpiarCampos();
                            Editar = false;

                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("No Se Pudo Modificar El Registro: " + ex.ToString());
                    }
                }
            }
            
        }
        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            if (txt_buscar_dni.Text == "")
            {
                dtg_verPacientes.ItemsSource = ObjetoCCM.MostrarPacientes();
            }
            else
            {
                dtg_verPacientes.ItemsSource = ObjetoCCM.Pacientes_DNI(txt_buscar_dni.Text);
            }
        }
        private void btn_modificarPaciente_Click(object sender, RoutedEventArgs e)
        {


            if (dtg_verPacientes.SelectedCells.Count > 0)
            {

                Editar = true;
                Paciente PacienteMod = (Paciente)dtg_verPacientes.SelectedItem;
                txt_dni.Text = PacienteMod.dni;
                txt_nombre.Text = PacienteMod.nombre;
                txt_apellido.Text = PacienteMod.apellido;
                
                if (PacienteMod.genero == "m")
                {
                    cmb_genero.Text = "Masculino";
                }
                else
                {
                    cmb_genero.Text = "Femenino";
                }

                dt_fecha_nac.Text = Convert.ToString(PacienteMod.fechadenacimiento);
                txt_email.Text = PacienteMod.email;
                txt_direccion.Text = PacienteMod.direccion;
                txt_telefono.Text = PacienteMod.telefono;
                txt_enfermedad.Text = PacienteMod.enfermedad;
                txt_medicamentos.Text = PacienteMod.medicamentos;
                txt_alergia.Text = PacienteMod.alergias;

                if (PacienteMod.EsActivo == true)
                {
                    cmb_EstadoPaciente.Text = "Habilitado";
                }
                else
                {
                    cmb_EstadoPaciente.Text = "Deshabilitado";
                }
                dt_fechaAlta.Text = Convert.ToString(PacienteMod.fechaDeAlta);
                idPaciente = PacienteMod.id_paciente.ToString();

                if (ValidarCampos())
                {
                    ObjetoCCM.ModificarPaciente(PacienteMod);
                }
               
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Modificar");
            }




        }

        private void btn_borrarPaciente_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_verPacientes.SelectedCells.Count > 0)
            {

                Paciente PacienteMod = (Paciente)dtg_verPacientes.SelectedItem;
                idPaciente = PacienteMod.id_paciente.ToString();

                ObjetoCCM.EliminarPaciente(idPaciente);
                MessageBox.Show("El Registro se Eliminó Satisfactoriamente");
                MostrarPacientes();
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Eliminar");
            }
        }

        private void LimpiarCampos()
        {
            txt_dni.Clear();
            txt_nombre.Clear();
            txt_apellido.Clear();
            cmb_genero.Text = "Masculino";
            dt_fecha_nac.Text = DateTime.Now.ToString();
            txt_email.Clear();
            txt_direccion.Clear();
            txt_telefono.Clear();
            cmb_EstadoPaciente.Text = "Habilitado";
            txt_enfermedad.Clear();
            txt_medicamentos.Clear();
            txt_alergia.Clear();
            dt_fechaAlta.Text = DateTime.Now.ToString();
        }
        private bool ValidarCampos()
        {
            if ((string.IsNullOrEmpty(txt_dni.Text)) || (txt_nombre.Text == "") || (txt_apellido.Text == "") || (txt_direccion.Text == "") || (txt_email.Text == "") || (txt_alergia.Text == "") || (txt_telefono.Text == "") || (txt_medicamentos.Text == "") || (cmb_genero.Text == "") || (txt_enfermedad.Text == "") || (dt_fechaAlta.Text == "") || (dt_fecha_nac.Text == ""))
            {
                MessageBox.Show("Complete todos los campos");

                return false;
            }
            else
                return true;
        }
    }
}
