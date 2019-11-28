using CapaCitaMedica;
using System.Data;
using System.Windows;
using Entidades;
using System;
using CapaPresentacion.Vistas.CP_Reserva;

namespace CapaPresentacion.Vistas
{
    /// <summary>
    /// Lógica de interacción para VerUsuarios.xaml
    /// </summary>
    public partial class Usuarios : Window
    {
        public Usuarios()
        {
            InitializeComponent();
            MostrarUsuarios();
            LimpiarCampos();
        }
        CCM_Usuario ObjetoCCM = new CCM_Usuario();
        private string idUsuario = null;
        public bool Editar = false;
        private void MostrarUsuarios()
        {
            dtg_verUsuarios.ItemsSource = ObjetoCCM.MostrarUsuarios();
        }

        private void btn_agregarUsuario_Click(object sender, RoutedEventArgs e)
        {
            // INSERTAR
            Usuario UsuarioAdd = new Usuario();
            if (Editar==false) {
                try
                {
                    UsuarioAdd.NombreUsuario = txt_nombreUsuario.Text;
                    UsuarioAdd.Nombre = txt_nombre.Text;
                    UsuarioAdd.Apellido = txt_apellido.Text;
                    UsuarioAdd.Email = txt_email.Text;
                    UsuarioAdd.Contraseña = pswb_contraseña.Password;

                    if (cmb_tipoUsuario.Text == "Administrador")
                    {
                        UsuarioAdd.EsAdministrador = true;
                    }
                    else
                    {
                        UsuarioAdd.EsAdministrador = false;
                    }

                    if (cmb_estadoUsuario.Text == "Habilitado")
                    {
                        UsuarioAdd.EsActivo = true;
                    }
                    else
                    {
                        UsuarioAdd.EsActivo = false;
                    }
                    UsuarioAdd.FechaDeAlta = dt_fechaAlta.SelectedDate.Value;

                    ObjetoCCM.InsertarUsuarios(UsuarioAdd);
                    MessageBox.Show("El Registro se Ingreso Satisfactoriamente");
                    MostrarUsuarios();
                    LimpiarCampos();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No Se Pudo Ingresar el Registro: " + ex.ToString());
                }
                

            }
            //EDITAR
            if (Editar==true)
            {
                
                try
                {
                    UsuarioAdd.NombreUsuario = txt_nombreUsuario.Text;
                    UsuarioAdd.Nombre = txt_nombre.Text;
                    UsuarioAdd.Apellido = txt_apellido.Text;
                    UsuarioAdd.Email = txt_email.Text;
                    UsuarioAdd.Contraseña = pswb_contraseña.Password;

                    if (cmb_tipoUsuario.Text == "Administrador")
                    {
                        UsuarioAdd.EsAdministrador = true;
                    }
                    else
                    {
                        UsuarioAdd.EsAdministrador = false;
                    }

                    if (cmb_estadoUsuario.Text == "Habilitado")
                    {
                        UsuarioAdd.EsActivo = true;
                    }
                    else
                    {
                        UsuarioAdd.EsActivo = false;
                    }
                    UsuarioAdd.FechaDeAlta = dt_fechaAlta.SelectedDate.Value;
                    UsuarioAdd.ID_Usuario = Convert.ToInt32(idUsuario);
                    ObjetoCCM.ModificarUsuarios(UsuarioAdd);
                    MessageBox.Show("El Registro se Modificó Satisfactoriamente");
                    MostrarUsuarios();
                    LimpiarCampos();
                    Editar = false;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No Se Pudo Modificar El Registro: " + ex.ToString());
                }
            }
        }
        private void btn_modificarUsuario_Click(object sender, RoutedEventArgs e)
        {


            if (dtg_verUsuarios.SelectedCells.Count > 0)
            {

                Editar = true;
                Usuario UsuarioMod = (Usuario)dtg_verUsuarios.SelectedItem;
                txt_nombreUsuario.Text = UsuarioMod.NombreUsuario;
                txt_nombre.Text = UsuarioMod.Nombre;
                txt_apellido.Text = UsuarioMod.Apellido;
                txt_email.Text = UsuarioMod.Email;
                pswb_contraseña.Password = UsuarioMod.Contraseña;
                dt_fechaAlta.Text = Convert.ToString(UsuarioMod.FechaDeAlta);
                if (UsuarioMod.EsAdministrador == true)
                {
                    cmb_tipoUsuario.Text = "Administrador";
                }
                else
                {
                    cmb_tipoUsuario.Text = "Estándar";
                }

                if (UsuarioMod.EsActivo == true)
                {
                    cmb_estadoUsuario.Text = "Habilitado";
                }
                else
                {
                    cmb_estadoUsuario.Text = "Deshabilitado";
                }
                idUsuario = UsuarioMod.ID_Usuario.ToString();

                ObjetoCCM.ModificarUsuarios(UsuarioMod);
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Modificar");
            }

           
            
           
        }

        private void btn_borrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (dtg_verUsuarios.SelectedCells.Count > 0)
            {

                Usuario UsuarioMod = (Usuario)dtg_verUsuarios.SelectedItem;
                idUsuario = UsuarioMod.ID_Usuario.ToString();

                ObjetoCCM.EliminarUsuarios(idUsuario);
                MessageBox.Show("El Registro se Eliminó Satisfactoriamente");
                MostrarUsuarios();
            }
            else
            {
                MessageBox.Show("Debe Elegir un Registro a Eliminar");
            }
        }

        private void LimpiarCampos() {
            txt_nombreUsuario.Clear();
            txt_nombre.Clear();
            txt_apellido.Clear();
            txt_email.Clear();
            pswb_contraseña.Clear();
            cmb_tipoUsuario.Text = "Estándar";
            cmb_estadoUsuario.Text = "Habilitado";
            dt_fechaAlta.Text= DateTime.Now.ToString();
            
            
        
        }
    }
}
