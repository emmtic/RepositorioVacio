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

namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para HorariosMedicos.xaml
    /// </summary>
    public partial class HorariosMedicos : Window
    {
        private int[] intervalos = { 15, 30, 60 };
        private int idMedico;
        private CCM_HorarioMedico oHmCCM = new CCM_HorarioMedico();
        private List<HorarioMedico> listHmMed = null;
        private List<int> recdiaCheckedMod = new List<int>();
        private bool editar = false;
        public HorariosMedicos(int recidMedico)
        {
            InitializeComponent();
            this.idMedico = recidMedico;
            Datosparainiciar();
        }

        private void Datosparainiciar()
        {
            cmboxdur1.ItemsSource = cmboxdur2.ItemsSource = cmboxdur3.ItemsSource = cmboxdur4.ItemsSource = cmboxdur5.ItemsSource = intervalos;

            for (int i = 0; i < 5; i++)
            {
                ((ComboBox)this.FindName($"horaIni{i + 1}")).IsEnabled = false;
                ((ComboBox)this.FindName($"horaFin{i + 1}")).IsEnabled = false;
                ((ComboBox)this.FindName($"horaIni{i + 1}b")).IsEnabled = false;
                ((ComboBox)this.FindName($"horaFin{i + 1}b")).IsEnabled = false;
                ((ComboBox)this.FindName($"cmboxdur{i + 1}")).IsEnabled = false;
            }

            try
            {
                this.listHmMed = oHmCCM.listHorariosByMed(idMedico); //puede ser nulo si la lista no tiene nada
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al traer datos de lista horarios: " + ex);
                throw;
            }


            if (this.listHmMed.Count > 0)
            {
                btnEnviarABM.Content = "Modificar Horarios";
                CargaFormMod();
                this.editar = true;
            }

        }

        private void CargaFormMod()
        {
            for (int i = 0; i < listHmMed.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    string dia = Enum.GetName(typeof(DayOfWeek), j + 1);
                    if (listHmMed[i].Dia == (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia))
                    {
                        recdiaCheckedMod.Add(j + 1); //para tener una referencia de los dias que tenia el medico chequeado y usarla despues

                        ((CheckBox)this.FindName($"ckBoxDia{j + 1}")).IsChecked = true;

                        ((ComboBox)this.FindName($"horaIni{j + 1}")).Text = listHmMed[i].HoraInicio_A;
                        ((ComboBox)this.FindName($"horaFin{j + 1}")).Text = listHmMed[i].HoraFin_A; ;
                        ((ComboBox)this.FindName($"horaIni{j + 1}b")).Text = listHmMed[i].HoraInicio_B; ;
                        ((ComboBox)this.FindName($"horaFin{j + 1}b")).Text = listHmMed[i].HoraFin_B;
                        ((ComboBox)this.FindName($"cmboxdur{j + 1}")).Text = listHmMed[i].DuracionTurnos.ToString();

                        break; //para romper el bucle for cuando encuentre
                    }
                }
            }
        }

        private void ckBoxdia_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox ckboxactual = (CheckBox)sender;

            switch (ckboxactual.Name)
            {
                case "ckBoxDia1":
                    ControlCheckbox(ckboxactual, horaIni1, cmboxdur1); //manejo solo el checkbox actual con su horaIni y el comboxduracion
                    break;
                case "ckBoxDia2":
                    ControlCheckbox(ckboxactual, horaIni2, cmboxdur2);
                    break;
                case "ckBoxDia3":
                    ControlCheckbox(ckboxactual, horaIni3, cmboxdur3);
                    break;
                case "ckBoxDia4":
                    ControlCheckbox(ckboxactual, horaIni4, cmboxdur4);
                    break;
                case "ckBoxDia5":
                    ControlCheckbox(ckboxactual, horaIni5, cmboxdur5);
                    break;

            }
        }


        private void ControlCheckbox(CheckBox ckboxactual, ComboBox comboboxaux, ComboBox cmboxdur) //Habilito el combobox HoraIni si su checkbox esta chequeado
        {
            if (ckboxactual.IsChecked == true)
            {
                comboboxaux.IsEnabled = true;
                HoraMinPrimerCmb(comboboxaux);
                cmboxdur.IsEnabled = true;
            }
            else
            {
                comboboxaux.Items.Clear();
                comboboxaux.IsEnabled = false;
                cmboxdur.IsEnabled = false;
            }
        }

        private void HoraMinPrimerCmb(ComboBox comboboxIni) //incia el combobox horaIni de la fila indicada (recibe el i iterador)
        {
            for (int i = 0; i < 23; i++)
            {
                for (int j = 0; j < 60; j += 30)
                {
                    if (i < 10)
                    {
                        if (j < 10) comboboxIni.Items.Add($"0{i}:0{j}"); // ((ComboBox)this.FindName($"horaIni{ndia + 1}")).Items.Add($"{i}:0{j}"); //ATENCION Anteriormente diseñado para agregarle el 0, se puede borrar y adaptar
                        else comboboxIni.Items.Add($"0{i}:{j}");
                    }
                    else
                    {
                        if (j < 10) comboboxIni.Items.Add($"{i}:0{j}");
                        else comboboxIni.Items.Add($"{i}:{j}");
                    }

                }
            }
        }

        private void cmbboxH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboboxactual = (ComboBox)sender;  //combobox actual

            for (int i = 0; i < 5; i++)
            {
                if (comboboxactual.Name == $"horaIni{i + 1}")
                {
                    ControlCargaHora(comboboxactual, ((ComboBox)this.FindName($"horaFin{i + 1}")));
                }
                else if (comboboxactual.Name == $"horaFin{i + 1}")
                {
                    ControlCargaHora(comboboxactual, ((ComboBox)this.FindName($"horaIni{i + 1}b")));
                }
                else if (comboboxactual.Name == $"horaIni{i + 1}b")
                {
                    ControlCargaHora(comboboxactual, ((ComboBox)this.FindName($"horaFin{i + 1}b")));
                }
            }

        }

        private void ControlCargaHora(ComboBox Hant, ComboBox Hdep) //Segun la manipulacion del primer combobox, cambio los items del segundo combobox
        {
            Hdep.ItemsSource = null;
            Hdep.Items.Clear();
            if (Hant.SelectedItem != null && Hant.IsEnabled == true)
            {
                Hdep.IsEnabled = true;
                horaMinutesDep(Hant.SelectedItem.ToString(), Hdep);
            }
            else
            {
                Hdep.IsEnabled = false;
            }
        }

        private void horaMinutesDep(string hmAnt, ComboBox cmbbox) //Metodo para que me envie la hora seleccionada del primer combobox y en el segundo lo uso para q empiece desde esa hora
        {
            string[] arrayhm = hmAnt.Split(':');
            int hFinAnt = Convert.ToInt16(arrayhm[0]) + 1; //le sumo 1 hora para que no coincida la hora inicial con la final (minimo 60 minutos de dif)
            int mFinAnt = Convert.ToInt16(arrayhm[1]);
            bool cambiaArranque = false;

            if (mFinAnt == 30)  //para que arranque en 30 minutos si es que el anterior llevaba 30 (una hora de diferencia)
            {
                cambiaArranque = true;
            }

            for (int i = hFinAnt; i < 24; i++)
            {
                for (int j = 0; j < 60; j += 30)
                {
                    if (cambiaArranque == true)
                    {
                        j = 30;
                        cambiaArranque = false;
                    }

                    if (i < 10)
                    {
                        if (j < 10) cmbbox.Items.Add($"0{i}:0{j}");
                        else cmbbox.Items.Add($"0{i}:{j}");
                    }
                    else
                    {
                        if (j < 9) cmbbox.Items.Add($"{i}:0{j}");
                        else cmbbox.Items.Add($"{i}:{j}");
                    }

                }
            }
        }

        private void btnEnviarABM_Click(object sender, RoutedEventArgs e)
        {
            if (Validar())
            {
                if (editar == false)
                {
                    InsertarHM();
                }
                else
                {
                    ModificarHM();
                }
            }
            else
            {
                MessageBox.Show("Complete los campos vacios que esten habilitados al marcar un dia");
            }

        }


        private void InsertarHM()
        {
            try
            {
                for (int i = 0; i < 5; i++)
                {
                    CheckBox oCheckbox = null;
                    oCheckbox = (CheckBox)this.FindName($"ckBoxDia{i + 1}");
                    if (oCheckbox != null && oCheckbox.IsChecked == true)
                    {
                        HorarioMedico oHm = new HorarioMedico();
                        string dia = Enum.GetName(typeof(DayOfWeek), i + 1);
                        oHm.Dia = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia);

                        oHm.IdMedico = this.idMedico; //Para el id del medico
                        oHm.HoraInicio_A = ((ComboBox)this.FindName($"horaIni{i + 1}")).Text;
                        oHm.HoraFin_A = ((ComboBox)this.FindName($"horaFin{i + 1}")).Text;
                        oHm.HoraInicio_B = ((ComboBox)this.FindName($"horaIni{i + 1}b")).Text;
                        oHm.HoraFin_B = ((ComboBox)this.FindName($"horaFin{i + 1}b")).Text;
                        oHm.DuracionTurnos = Convert.ToInt32(((ComboBox)this.FindName($"cmboxdur{i + 1}")).Text);

                        oHmCCM.Insertar(oHm);
                    }
                }

                MessageBox.Show("Informacion horaria actualizada");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar horarios" + ex);
                throw;
            }

        }


        private void ModificarHM()
        {
            bool updateHM;
            List<int> guiaCheckedBorrar = recdiaCheckedMod; //van a quedar los dias que seran eliminados aqui

            try
            {
                for (int i = 0; i < 5; i++)
                {
                    updateHM = false;
                    CheckBox oCheckbox = null;
                    oCheckbox = (CheckBox)this.FindName($"ckBoxDia{i + 1}");
                    if (oCheckbox != null && oCheckbox.IsChecked == true)
                    {
                        for (int j = 0; j < this.recdiaCheckedMod.Count; j++)
                        {
                            if ((i + 1) == recdiaCheckedMod[j])
                            {
                                updateHM = true;
                                guiaCheckedBorrar.RemoveAt(j);
                                break;
                            }
                        }

                        HorarioMedico oHm = new HorarioMedico();
                        string dia = Enum.GetName(typeof(DayOfWeek), i + 1);
                        oHm.Dia = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia);

                        oHm.IdMedico = this.idMedico; //Para el id del medico
                        oHm.HoraInicio_A = ((ComboBox)this.FindName($"horaIni{i + 1}")).Text;
                        oHm.HoraFin_A = ((ComboBox)this.FindName($"horaFin{i + 1}")).Text;
                        oHm.HoraInicio_B = ((ComboBox)this.FindName($"horaIni{i + 1}b")).Text;
                        oHm.HoraFin_B = ((ComboBox)this.FindName($"horaFin{i + 1}b")).Text;
                        oHm.DuracionTurnos = Convert.ToInt32(((ComboBox)this.FindName($"cmboxdur{i + 1}")).Text);

                        if (updateHM == true) oHmCCM.Modificar(oHm);
                        else oHmCCM.Insertar(oHm);

                    }
                }

                for (int i = 0; i < guiaCheckedBorrar.Count; i++)
                {
                    oHmCCM.Eliminar(idMedico, guiaCheckedBorrar[i]);
                }

                MessageBox.Show("Informacion horaria modificada correctamente");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar horarios" + ex);
                throw;
            }
        }


        private bool Validar()
        {
            bool validado = true;

            if (ckBoxDia1.IsChecked == true && validarCmbbox(horaIni1, horaFin1, horaIni1b, horaFin1b, cmboxdur1) == false)
            {
                return false;
            }

            if (ckBoxDia2.IsChecked == true && validarCmbbox(horaIni2, horaFin2, horaIni2b, horaFin2b, cmboxdur2) == false)
            {
                return false;
            }

            if (ckBoxDia3.IsChecked == true && validarCmbbox(horaIni3, horaFin3, horaIni3b, horaFin3b, cmboxdur3) == false)
            {
                return false;
            }

            if (ckBoxDia4.IsChecked == true && validarCmbbox(horaIni4, horaFin4, horaIni4b, horaFin4b, cmboxdur4) == false)
            {
                return false;
            }

            if (ckBoxDia5.IsChecked == true && validarCmbbox(horaIni5, horaFin5, horaIni5b, horaFin5b, cmboxdur5) == false)
            {
                return false;
            }

            if (ckBoxDia1.IsChecked == false && ckBoxDia2.IsChecked == false && ckBoxDia3.IsChecked == false && ckBoxDia4.IsChecked == false && ckBoxDia5.IsChecked == false)
            {
                MessageBox.Show("Se cerrara sin dias marcados");
            }

            return validado;
        }

        private bool validarCmbbox(ComboBox cmb1, ComboBox cmb2, ComboBox cmb3, ComboBox cmb4, ComboBox cmbdur)
        {
            if (cmb1.Text == "" || cmb2.Text == "" || cmbdur.Text == "" || (cmb3.SelectedItem != null && cmb4.Text == "")) //controlo los campos vacios y los combobox 3 y 4 
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
