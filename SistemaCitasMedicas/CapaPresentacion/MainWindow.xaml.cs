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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CapaPresentacion.Vistas;
using System.Data;
using Entidades;
using CapaCitasMedicas;
using Entidades.Cache;
using NLog;
namespace CapaPresentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CCM_Login ObjetoCCM = new CCM_Login();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private void btn_ingresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txt_nombreUsuario.Text != "")
                {
                    if (psw_contraseña.Password != "")
                    {
                        CCM_Login Login = new CCM_Login();
                        var ValidarLogin = Login.CheckUsuarios(txt_nombreUsuario.Text, psw_contraseña.Password);
                        if (ValidarLogin == true)
                        {

                            if (UsuarioLoginCache.EsActivo == false)
                            {
                                MensajeError("Usuario Inhabilitado.\nConsulte con un Administrador.");
                            }
                            else
                            {
                                Administracion VentanaAdministracion = new Administracion();
                                VentanaAdministracion.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MensajeError("Usuario y/o Contraseña Incorrectos.\n Verifique e Intente Nuevamente.");
                        }
                    }
                    else
                    {
                        MensajeError("Ingrese una Contraseña");
                    }

                }
                else
                {
                    MensajeError("Ingrese un Usuario");
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Intento Fallido de inicio de sesion");
                MessageBox.Show("No Se puede Loguear"+ ex.ToString());;
            }
            //if (txt_nombreUsuario.Text!="")
            //{
            //    if (psw_contraseña.Password!="")
            //    {
            //        CCM_Login Login = new CCM_Login();
            //        var ValidarLogin = Login.CheckUsuarios(txt_nombreUsuario.Text, psw_contraseña.Password);
            //        if (ValidarLogin==true)
            //        {

            //            if (UsuarioLoginCache.EsActivo==false)
            //            {
            //                MensajeError("Usuario Inhabilitado.\nConsulte con un Administrador.");
            //            }
            //            else
            //            {
            //                Administracion VentanaAdministracion = new Administracion();
            //                VentanaAdministracion.Show();
            //                this.Hide();
            //            }
            //        }
            //        else
            //        {
            //            MensajeError("Usuario y/o Contraseña Incorrectos.\n Verifique e Intente Nuevamente.");
            //        }
            //    }
            //    else
            //    {
            //        MensajeError("Ingrese una Contraseña");
            //    }

            //}
            //else 
            //{
            //    MensajeError("Ingrese un Usuario");
            //}
            
        }
        private void MensajeError(string msj) {

            lbl_mensajeDeError.Content = msj;
            lbl_mensajeDeError.Visibility = Visibility.Visible;
        }
        //private void CerrarSesion(object sender, clos e) {
        //    txt_nombreUsuario.Clear();
        //    psw_contraseña.Clear();
        //    lbl_mensajeDeError.Visibility = Visibility.Hidden;
        //    this.Show();
        //    txt_nombreUsuario.Focus();
        
        //}
    }
}
