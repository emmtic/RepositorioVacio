using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        private int id_usuario;
        private string nombreUsuario;
        private string nombre;
        private string apellido;
        private string contraseña;
        private string email;
        private bool esAdministrador;
        private bool esActivo;
        private DateTime fechaDeAlta;

        public int ID_Usuario { get => id_usuario; set => id_usuario = value; }

        [Required(ErrorMessage = "Debe Ingresar un Nombre de Usuario")]
        [StringLength(maximumLength:15,MinimumLength =5,ErrorMessage ="El USUARIO debe tener como maximo 15 digitos")]
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }

        [Required(ErrorMessage ="Debe Ingresar un Nombre")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "El NOMBRE solo debe tener letras")]
        [StringLength(maximumLength: 20, MinimumLength = 5, ErrorMessage = "El nombre debe tener como maximo 20 digitos")]
        public string Nombre { get => nombre; set => nombre = value; }

        [Required(ErrorMessage = "Debe Ingresar un Apellido")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage ="El APELLIDO solo debe tener letras")]
        [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "El nombre debe tener como maximo 20 digitos")]
        public string Apellido { get => apellido; set => apellido = value; }

        [Required(ErrorMessage = "Debe Ingresar una Contraseña")]
        [StringLength(maximumLength: 20, MinimumLength = 4, ErrorMessage = "La CONSTRASEÑA debe tener un minimo de 4 digitos")]
        public string Contraseña { get => contraseña; set => contraseña = value; }

        [Required]
        [EmailAddress]
        public string Email { get => email; set => email = value; }
        public bool EsAdministrador { get => esAdministrador; set => esAdministrador = value; }
        public bool EsActivo { get => esActivo; set => esActivo = value; }
        public DateTime FechaDeAlta { get => fechaDeAlta; set => fechaDeAlta = value; }
        public override string ToString()
        {
            return "Nombre De Usuario: " + NombreUsuario +
                " - Nombre: " + Nombre +
                " - Apellido: " + Apellido +
                " - Email: " + Email +
                " - Administrador: " + EsAdministrador +
                " - Activo: " + EsActivo +
                " - Fecha De Alta: " + FechaDeAlta;
        }
    }
}
