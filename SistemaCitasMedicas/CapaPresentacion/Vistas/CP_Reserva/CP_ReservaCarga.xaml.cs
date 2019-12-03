using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Entidades.Cache;
using CapaCitasMedicas;


namespace CapaPresentacion.Vistas.CP_Reserva
{
    /// <summary>
    /// Lógica de interacción para CP_ReservaCarga.xaml
    /// </summary>
    public partial class CP_ReservaCarga : Window
    {
        //atributoss y objetos instanciados para usar sus metodos
        private CCM_Reserva reservaCCM = new CCM_Reserva();
        private CCM_Paciente pacienteCCM = new CCM_Paciente();
        private CCM_Medico medicoCCM = new CCM_Medico();
        private CCM_HorarioMedico horariomedicoCCM = new CCM_HorarioMedico();
        private bool editar = false;  //Para diferenciar cuando sean instrucciones para crear una nueva reserva o para modificar una reserva
        private Reserva recReservaMod; //sera cargada en la ventana de registros para modificar

        //listados que necesitare para no conectar de nuevo con la BD
        private List<string> listEspec = null;
        private List<EstadoPago> listEstadoPagos = null;
        private List<EstadoCita> listEstadoCitas = null;
        private List<Paciente> listPacientes = null;
        private List<Medico> listMedicos = null;
        private List<HorarioMedico> listHorariosMedicos = null;
        private List<Reserva> listReservasAll = null;  //Esta es la que usa la ventana modificar, alli se va a cargar y aca la va a recibir, no la cargo si solo voy a crear una nueva reserva
        private List<Reserva> listReservasActivas = null; //En esta se guardan las que tienen su fechacita pendientes desde hoy, o sea reservadas que todavia no vencieron
        private int limDiasTurno = 90; //Dias del dtpicker, limites de cuantos dias se van a mostrar en el datepicker (si es para modificar una reserva se le suman sus valores con los de la fecha de su alta)


        //CONSTRUCTORES       
        public CP_ReservaCarga() //Para instanciar si hay que crear una NUEVA reserva
        {
            InitializeComponent();

            CargoListas();
            DatosParaIniciar();
            btnAgregarCita.Content = "Reservar Turno";
        }

        public CP_ReservaCarga(List<Reserva> reclistReservasAll, Reserva recReservaMod) //Para instancia si hay que MODIFICAR una reserva
        {
            InitializeComponent();

            this.editar = true;
            this.recReservaMod = recReservaMod;
            this.listReservasAll = reclistReservasAll;
            CargoListas();
            DatosParaIniciar();
            this.CargaFormMod();
            btnAgregarCita.Content = "Modificar Turno";
        }


        //METODOS
        private void CargoListas() //Cargo las listas dentro de un try catch para controlar los errores en las funciones
        {
            try
            {
                this.listEspec = medicoCCM.MostrarEspecialidades();
                this.listEstadoPagos = reservaCCM.ListaEstadosPago(); //las cargo a las globales para luego usarlo en cualquier otro metodo no solo en este, por ej cuando busque por id
                this.listEstadoCitas = reservaCCM.ListaEstadosCita();
                this.listPacientes = pacienteCCM.MostrarPacientes(); //las cargo a las globales para luego usarlo en cualquier otro metodo no solo en este, por ej cuando busque por id
                this.listMedicos = medicoCCM.MostrarMedicos();
                this.listHorariosMedicos = horariomedicoCCM.listHorariosMedicos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las listas: " + ex.ToString());
                throw;
            }
        }

        private void DatosParaIniciar() //cargo los combobox para iniciar
        {
            cmbboxEstPago.ItemsSource = this.listEstadoPagos;
            cmbboxEstCita.ItemsSource = this.listEstadoCitas;
            txtboxPaciente.IsReadOnly = true;
            txtboxMedico.IsReadOnly = true;
            dtpickerFechaCita.IsEnabled = false; //dtpicker deshabilitado al inicio
        }

        private void PreparaDatepicker() //Para iniciar el datepicker cuando lo habilite
        {
            this.listReservasActivas = reservaCCM.ListaReservas_Activas();

            if (this.editar == false) dtpickerFechaCita.DisplayDateStart = DateTime.Today; //si la ventana es para cargar una nueva Reserva la fecha empieza desde el dia mismo
            else dtpickerFechaCita.DisplayDateStart = this.recReservaMod.FechaAltaCita;  //si es para editar, que empiece desde la fecha de alta de ese dia

            dtpickerFechaCita.DisplayDateEnd = DateTime.Today.AddDays(limDiasTurno);
        }
        
        private string FormatoPac(int id, string apellido, string nombre, string dni) //Para armar las cadenas de string para la presentacion de esta ventana en el combobox sea paciente
        {
            return $"Paciente: {apellido} {nombre}, DNI: {dni}, Cod-{id}";
        }
        private string FormatoMed(int id, string apellido, string nombre, string medMatric, string medEspec) //Para armar las cadenas de string para la presentacion de esta ventana en el combobox sea medico
        {
            return $"{apellido} {nombre}, esp.: {medEspec}, mat.: {medMatric}, Cod-{id}";
        }

        private void btnListPac_Click(object sender, RoutedEventArgs e)
        {
            txtboxFiltroDni.Text = "";
            ListaPacientes ventanaListaPac = new ListaPacientes(this.listPacientes);
            ventanaListaPac.ShowDialog();
            this.txtboxPaciente.Text = ventanaListaPac.txtblockPaciente.Text;
        }

        private void btnListMed_Click(object sender, RoutedEventArgs e)
        {
            ListaMedicos ventanaListaMed = new ListaMedicos(this.listMedicos, txtboxMedico);
            ventanaListaMed.ShowDialog();
        }

        private void CargaFormMod() //Carga el formulario de la vista con los valores traidos desde la ventana para modificar
        {
            txtboxAsunto.Text = this.recReservaMod.AsuntoCita;
            txtboxPaciente.Text = FormatoPac(recReservaMod.IdPaciente, recReservaMod.ApellidoPaciente, recReservaMod.NombrePaciente, recReservaMod.DniPaciente);
            txtboxMedico.Text = FormatoMed(recReservaMod.IdMedico, recReservaMod.ApellidoMedico, recReservaMod.NombreMedico, recReservaMod.Matricula, recReservaMod.Especialidad);
            dtpickerFechaCita.Text = this.recReservaMod.FechaCita;
            cmbboxHoraCita.Text = this.recReservaMod.HoraCita;
            txtboxObserv.Text = this.recReservaMod.Observaciones;
            txtboxSintomas.Text = this.recReservaMod.Sintomas;
            txtboxEnfermedad.Text = this.recReservaMod.Enfermedad;
            txtboxMedicamnt.Text = this.recReservaMod.Medicamentos;
            txtboxPrecio.Text = this.recReservaMod.Precio.ToString();
            cmbboxEstPago.Text = this.recReservaMod.EstadoPago;
            cmbboxEstCita.Text = this.recReservaMod.EstadoCita;
        }

        private Reserva RecibeDelForm() //Recibe los datos del form y lo devuelve en un objeto de tipo Reserva
        {
            Reserva oreserva = new Reserva();
            oreserva.AsuntoCita = txtboxAsunto.Text;

            string[] datoPaciente = txtboxPaciente.Text.Replace(" ", "").Split('-');
            oreserva.IdPaciente = Convert.ToInt32(datoPaciente[1]);

            string[] datoMedico = txtboxMedico.Text.Replace(" ", "").Split('-');
            oreserva.IdMedico = Convert.ToInt32(datoMedico[1]);
            oreserva.FechaCita = dtpickerFechaCita.SelectedDate.Value.ToString("yyyy-MM-dd"); //lo transformo a FORMATO DE BASE DATOS para que el string sea facil de manejar para el orden
            oreserva.HoraCita = cmbboxHoraCita.Text;
            oreserva.Observaciones = txtboxObserv.Text;
            oreserva.Sintomas = txtboxSintomas.Text;
            oreserva.Medicamentos = txtboxMedicamnt.Text;
            oreserva.Enfermedad = txtboxEnfermedad.Text;
            oreserva.EstadoCita = cmbboxEstCita.Text; //this.listEstadoCitas.Find(x => x.Estado == cmbboxEstCita.Text).IdEstadoCita; //ATENCION
            oreserva.EstadoPago = cmbboxEstPago.Text; //this.listEstadoPagos.Find(x => x.Estado == cmbboxEstPago.Text).IdEstadoPago; //ATENCION
            oreserva.Precio = Convert.ToDecimal(txtboxPrecio.Text.Replace(".", ","));

            return oreserva;
        }


        //METODOS EVENTOS
        private void btnAgregarCita_Click(object sender, RoutedEventArgs e)
        {
            if (VerificarVacios())
            {
                if (this.editar == false)
                {
                    try
                    {    //Probando con cualquier usuario
                        Reserva oreservaNueva = RecibeDelForm();
                        oreservaNueva.NombreUsuario = UsuarioLoginCache.NombreUsuario; //carga del usuario que cargó esta reserva
                        oreservaNueva.FechaAltaCita = DateTime.Now;

                        reservaCCM.Insertar(oreservaNueva);

                        MessageBox.Show("Turno reservado satisfactoriamente");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al insertar nueva Reserva: " + ex.ToString());
                    }
                }
                else
                {
                    try
                    {   //Probando con cualquier usuario
                        Reserva oreservaModificada = RecibeDelForm();
                        oreservaModificada.IdReserva = this.recReservaMod.IdReserva;
                        oreservaModificada.NombreUsuario = UsuarioLoginCache.NombreUsuario; //modificacion del nuevo usuario
                        oreservaModificada.FechaAltaCita = DateTime.Now;

                        reservaCCM.Modificar(oreservaModificada);

                        MessageBox.Show("Turno modificado satisfactoriamente");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al modificar Reserva: " + ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("Complete los campos vacios");
            }
        }

        private void txtboxMedico_TextChanged(object sender, TextChangedEventArgs e)
        {
            dtpickerFechaCita.IsEnabled = true; //habilita el elemento dtpickerFechaCita
            PreparaDatepicker();
            cmbboxHoraCita.ItemsSource = null; //limpia el combobox con datos cargados anteriormente en modo itemssource
            cmbboxHoraCita.Items.Clear();     //limpia los items en cada cambio de seleccion
            lstBoxHsAtencion.Items.Clear();   //limpia los items en cada cambio de seleccion

            if (dtpickerFechaCita.SelectedDate != null && txtboxMedico.Text != null) logicaTurnosDisponibles();

            if (txtboxMedico.Text != null) CargalstBoxHsAtencion();
        }

        private void dtpickerFechaCita_SelectedDateChanged(object sender, SelectionChangedEventArgs args)
        {
            if (txtboxMedico.Text != "") logicaTurnosDisponibles();//para controlar que el combobox no este vacio
            //else MessageBox.Show("Seleccione un medico"); //mensaje de control por las dudas quedaba vacio
        }

        private void logicaTurnosDisponibles()
        {
            //MessageBox.Show(cmbboxMedico.Text); Para controlar los eventos
            DateTime dtFechaSelected = dtpickerFechaCita.SelectedDate.Value;
            string fechaSelected = dtpickerFechaCita.SelectedDate.Value.ToString("yyyy-MM-dd");
            string[] datos_MedicSelec = txtboxMedico.Text.Replace(" ", "").Split('-');
            int idMedSelec = Convert.ToInt32(datos_MedicSelec[1]);
            bool activarDia = false;
            List<string> listHorarioToday = new List<string>();
            List<string> turnosAvista = new List<string>();

            if (editar == false) //Cuando hay que preparar CREAR una nueva reserva 
            {
                List<HorarioMedico> HorariosMedicSelec = new List<HorarioMedico>();  //para listar los horarios del medico seleccionado

                for (int i = 0; i < this.listHorariosMedicos.Count; i++)  //Filtrar los horarios del medico seleccionado 
                {
                    if (this.listHorariosMedicos[i].IdMedico == idMedSelec)
                    {
                        HorariosMedicSelec.Add(listHorariosMedicos[i]);
                    }
                }

                for (int i = 0; i < HorariosMedicSelec.Count; i++) //cantidad de dias 5 o 6 depende de si se agrega el sabado, no cuento domingo; filtro los horarios del dia seleccionado, si es lunes, si es martes, etc
                {
                    if (dtFechaSelected.DayOfWeek == HorariosMedicSelec[i].Dia)
                    {
                        activarDia = true;
                        listHorarioToday = HorariosMedicSelec[i].ListarTurnosString();
                        turnosAvista = listHorarioToday; //turnosAvista es una lista que sera vaciada segun los horarios que esten ocupados ese dia seleccionado
                    }
                }

                if (activarDia)
                {
                    for (int i = 0; i < this.listReservasActivas.Count; i++) //cuenta las reservas con turnos ocupados (fechacita) a partir de hoy, para ver los turnos disponibles desde hoy
                    {
                        //las canceladas se filtran para que su turno este disponible && (this.listReservasActivas[i].EstadoCita != "Cancelada")
                        if ((idMedSelec == listReservasActivas[i].IdMedico) && (fechaSelected == listReservasActivas[i].FechaCita) && (this.listReservasActivas[i].EstadoCita != "Cancelada"))
                        {
                            for (int j = 0; j < listHorarioToday.Count; j++)
                            {
                                if (listHorarioToday[j] == listReservasActivas[i].HoraCita) turnosAvista.RemoveAt(j);
                            }
                        }
                    }
                }

                cmbboxHoraCita.ItemsSource = turnosAvista;
            }
            else //Cuando hay que preparar para MODIFICAR la reserva 
            {
                List<HorarioMedico> HorariosMedicSelec = new List<HorarioMedico>();

                for (int i = 0; i < this.listHorariosMedicos.Count; i++)
                {
                    if (this.listHorariosMedicos[i].IdMedico == idMedSelec)
                    {
                        HorariosMedicSelec.Add(listHorariosMedicos[i]);
                    }
                }

                for (int i = 0; i < HorariosMedicSelec.Count; i++) //cantidad de dias 5 o 6 depende si agrego el sabado, no cuento domingo;
                {
                    if (dtFechaSelected.DayOfWeek == HorariosMedicSelec[i].Dia)
                    {
                        activarDia = true;
                        listHorarioToday = HorariosMedicSelec[i].ListarTurnosString();
                        turnosAvista = listHorarioToday;
                    }
                }

                if (activarDia)
                {
                    for (int i = 0; i < this.listReservasActivas.Count; i++) //Como se puede editar algo por un error de carga del turno del pasado necesito ver los turnos disponibles que habia antes  
                    {                                                        //Pero la fecha de alta marca el comienzo del calendario ya que el calendario esta para que comience desde la fecha alta, ya que antes de alta de reserva no se le hubiese permitido sacar turno para dia anterior
                        if (this.listReservasAll[i].IdReserva != this.recReservaMod.IdReserva) //IMPORTANTE PARA QUE NO FILTRE EL HORARIO DE LA CITA A EDITAR! 
                        {
                            if ((idMedSelec == listReservasAll[i].IdMedico) && (fechaSelected == listReservasAll[i].FechaCita) && (this.listReservasAll[i].EstadoCita != "Cancelada")) // incluye tambien que no filtre las canceladas con distinto !=
                            {
                                for (int j = 0; j < listHorarioToday.Count; j++)
                                {
                                    if (listHorarioToday[j] == listReservasAll[i].HoraCita) turnosAvista.RemoveAt(j);
                                }
                            }
                        }

                    }
                }

                cmbboxHoraCita.ItemsSource = turnosAvista;
            }
        }


        private void CargalstBoxHsAtencion() //Para mostrar en el listbox los horarios de atencion del medico y tener una referencia 
        {
            if (txtboxMedico.Text != null)
            {
                lstBoxHsAtencion.Items.Clear();
                string[] datos_MedicSelec = txtboxMedico.Text.Replace(" ", "").Split('-');

                for (int i = 0; i < this.listHorariosMedicos.Count; i++)
                {
                    if (this.listHorariosMedicos[i].IdMedico == Convert.ToInt32(datos_MedicSelec[1])) lstBoxHsAtencion.Items.Add(listHorariosMedicos[i].ToString());
                }
            }
            //else{MessageBox.Show("Seleccione un medico");} //para controlar cuando quedaba vacio en los lapsos de cambio de especialidades
        }


        private void txtboxFiltroDni_TextChanged(object sender, TextChangedEventArgs e) //Para el filtro del Dni
        {
            //List<Paciente> lstauxPac = null;
            if (this.listPacientes != null)
            {
                if (txtboxFiltroDni.Text == "") //(Preferentemente iniciar los textbox de busqueda vacios) Ojo! si le agrego un placeholder deberia ir aqui tambien con un || para que cargue desde el principio ya que estos textbox son los que inician primero (initializ..) y mas el cambio de tener
                {                                 // un texto cargado lo activaria primero y puede no haber cargado la lista todavia y abajo empieza a buscar en una lista NULA y da error
                    txtboxPaciente.Text = "";
                }
                else
                {
                    Paciente oPac = listPacientes.Find(x => x.dni.Contains(txtboxFiltroDni.Text.Replace(" ", "")) || x.apellido.Contains(txtboxFiltroDni.Text.Replace(" ", "")));
                    if (oPac != null) txtboxPaciente.Text = FormatoPac(oPac.id_paciente, oPac.apellido, oPac.nombre, oPac.dni);
                    else txtboxPaciente.Text = "";
                }
            }

        }

        private void PrecioDecimalValidation(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            if (regex.IsMatch(e.Text) && !(e.Text == "." && ((TextBox)sender).Text.Contains(e.Text)))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool VerificarVacios()
        {
            bool completos = true;
            if (txtboxAsunto.Text == "" || txtboxPaciente.Text == "" || txtboxMedico.Text == "" ||
                dtpickerFechaCita.SelectedDate == null || cmbboxHoraCita.Text == "" ||
                txtboxObserv.Text == "" || txtboxSintomas.Text == "" || txtboxEnfermedad.Text == "" ||
                txtboxMedicamnt.Text == "" || txtboxPrecio.Text == "" || cmbboxEstPago.Text == "" ||
                cmbboxEstCita.Text == "")
            {
                completos = false;
            }

            return completos;
        }

     

        /*private int getIdFormatoPacMed(string cadena)
        {
            string[] datos_PersonaSelec = cadena.Replace(" ", "").Split('-');
            return Convert.ToInt32(datos_PersonaSelec[1]);
           
        }*/


    }
}
