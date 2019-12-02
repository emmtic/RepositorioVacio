using System.Windows;
using Entidades;
using System;
using CapaCitasMedicas;
using System.Collections.Generic;

namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para Medicos.xaml
    /// </summary>
    public partial class Medicos : Window
    {
        public Medicos()
        {
            InitializeComponent();
            MostrarMedicos();
            LlenarCMBAtributos();
            LlenarComboboxEspecialidades();
            LimpiarCampos();
        }
        CCM_Medico ObjetoCCM = new CCM_Medico();
        
        private string idMedico = null;
        public bool Editar = false;

        private void LlenarCMBAtributos()
        {
            List<string> atributos = ObjetoCCM.MostrarCampos();
            for (int i = 0; i < atributos.Count; i++)
            {
                cmb_seleccionar_atributo.Items.Add(atributos[i]);
            }
        }
        private void LlenarComboboxEspecialidades()
        {
            
            List<string> especialidad = ObjetoCCM.MostrarEspecialidades();
            for (int i = 0; i < especialidad.Count; i++)
            {
                cmb_especialidad.Items.Add(especialidad[i]);
            }
        }
        private void MostrarMedicos()
        {
            dtg_verMedicos.ItemsSource = ObjetoCCM.MostrarMedicos();
        }

        private void btn_agregarMedico_Click(object sender, RoutedEventArgs e)
        {
            Medico MedicoAdd = new Medico();

            if (ValidarCampos())
            {
                if (Editar == false)
                {
                    try
                    {
                        MedicoAdd.dni = txt_dni.Text;
                        MedicoAdd.nombre = txt_nombre.Text;
                        MedicoAdd.apellido = txt_apellido.Text;
                        MedicoAdd.email = txt_email.Text;
                        MedicoAdd.direccion = txt_direccion.Text;
                        MedicoAdd.telefono = txt_telefono.Text;

                        if (cmb_genero.Text == "Masculino")
                        {
                            MedicoAdd.genero = "m";
                        }
                        else
                        {
                            MedicoAdd.genero = "f";
                        }

                        MedicoAdd.fechadenacimiento = dt_fecha_nac.SelectedDate.Value;

                        if (cmb_EstadoMedico.Text == "Habilitado")
                        {
                            MedicoAdd.EsActivo = true;
                        }
                        else
                        {
                            MedicoAdd.EsActivo = false;
                        }

                        MedicoAdd.matricula = txt_matricula.Text;
                        MedicoAdd.especialidad = cmb_especialidad.Text;
                        MedicoAdd.fechaDeAlta = dt_fechaAlta.SelectedDate.Value;

                        if (ValidarCampos())
                        {
                            ObjetoCCM.InsertarMedico(MedicoAdd);
                            MessageBox.Show("El Registro se Ingreso Satisfactoriamente");
                            MostrarMedicos();
                            LimpiarCampos();
                        }
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
                        MedicoAdd.dni = txt_dni.Text;
                        MedicoAdd.nombre = txt_nombre.Text;
                        MedicoAdd.apellido = txt_apellido.Text;
                        MedicoAdd.email = txt_email.Text;
                        MedicoAdd.direccion = txt_direccion.Text;
                        MedicoAdd.telefono = txt_telefono.Text;

                        if (cmb_genero.Text == "Masculino")
                        {
                            MedicoAdd.genero = "m";
                        }
                        else
                        {
                            MedicoAdd.genero = "f";
                        }

                        MedicoAdd.fechadenacimiento = dt_fecha_nac.SelectedDate.Value;

                        if (cmb_EstadoMedico.Text == "Habilitado")
                        {
                            MedicoAdd.EsActivo = true;
                        }
                        else
                        {
                            MedicoAdd.EsActivo = false;
                        }

                        MedicoAdd.matricula = txt_matricula.Text;
                        MedicoAdd.especialidad = cmb_especialidad.Text;
                        MedicoAdd.fechaDeAlta = dt_fechaAlta.SelectedDate.Value;

                        MedicoAdd.id_medico = Convert.ToInt32(idMedico);

                        if (ValidarCampos())
                        {
                            ObjetoCCM.ModificarMedico(MedicoAdd);
                            MessageBox.Show("El Registro se Modificó Satisfactoriamente");
                            MostrarMedicos();
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
            if (txt_buscar.Text == "")
            {
                dtg_verMedicos.ItemsSource = ObjetoCCM.MostrarMedicos();
            }
            else
            {
                dtg_verMedicos.ItemsSource = ObjetoCCM.Medicos_Campo_Busqueda(cmb_seleccionar_atributo.Text,txt_buscar.Text);
            }
        }
        private void btn_modificarMedico_Click(object sender, RoutedEventArgs e)
        {


            if (dtg_verMedicos.SelectedCells.Count > 0)
            {

                Editar = true;
                Medico MedicoMod = (Medico)dtg_verMedicos.SelectedItem;
                txt_dni.Text = MedicoMod.dni;
                txt_nombre.Text = MedicoMod.nombre;
                txt_apellido.Text = MedicoMod.apellido;

                if (MedicoMod.genero == "m")
                {
                    cmb_genero.Text = "Masculino";
                }
                else
                {
                    cmb_genero.Text = "Femenino";
                }

                dt_fecha_nac.Text = Convert.ToString(MedicoMod.fechadenacimiento);
                txt_email.Text = MedicoMod.email;
                txt_direccion.Text = MedicoMod.direccion;
                txt_telefono.Text = MedicoMod.telefono;

                if (MedicoMod.EsActivo == true)
                {
                    cmb_EstadoMedico.Text = "Habilitado";
                }
                else
                {
                    cmb_EstadoMedico.Text = "Deshabilitado";
                }
                txt_matricula.Text = MedicoMod.matricula;
                cmb_especialidad.Text = MedicoMod.especialidad;
                dt_fechaAlta.Text = Convert.ToString(MedicoMod.fechaDeAlta);
                idMedico = MedicoMod.id_medico.ToString();

                if (ValidarCampos())
                {
                    ObjetoCCM.ModificarMedico(MedicoMod);
                }
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Modificar");
            }




        }

        private void btn_borrarMedico_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_verMedicos.SelectedCells.Count > 0)
            {

                Medico MedicoMod = (Medico)dtg_verMedicos.SelectedItem;
                idMedico = MedicoMod.id_medico.ToString();

                ObjetoCCM.EliminarMedico(idMedico);
                MessageBox.Show("El Registro se Eliminó Satisfactoriamente");
                MostrarMedicos();
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
            cmb_EstadoMedico.Text="Habilitado";
            txt_matricula.Clear();
            dt_fechaAlta.Text = DateTime.Now.ToString();



        }

        private void btn_cargar_especialidades_medicos_Click(object sender, RoutedEventArgs e)
        {
            Especialidades_Medicos ventana_especialidades_medico = new Especialidades_Medicos();
            ventana_especialidades_medico.ShowDialog();
            cmb_especialidad.Items.Clear();
            LlenarComboboxEspecialidades();
            ventana_especialidades_medico.Close();
        }

        private bool ValidarCampos()
        {
            if ((txt_dni.Text == "") || (txt_nombre.Text == "") || (txt_apellido.Text == "") || (txt_direccion.Text == "") || (txt_email.Text == "") || (txt_matricula.Text == "") || (txt_telefono.Text == "") || (cmb_genero.Text == "") || (cmb_especialidad.Text == "") || (dt_fechaAlta.Text == "") || (dt_fecha_nac.Text == ""))
            {
                MessageBox.Show("Complete todos los campos");

                return false;
            }
            else
                return true;
        }

        private void btn_cargar_horarios_medico_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_verMedicos.SelectedCells.Count > 0)
            {
                Medico MedicoMod = (Medico)dtg_verMedicos.SelectedItem;
                idMedico = MedicoMod.id_medico.ToString();
                
                if (idMedico != null)
                {
                    HorariosMedicos ventanaHM = new HorariosMedicos(int.Parse(idMedico));
                    ventanaHM.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("Seleccione un medico para gestionar horarios");
            }
        }
    }

}
