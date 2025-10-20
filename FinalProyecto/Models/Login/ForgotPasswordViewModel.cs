using System.ComponentModel.DataAnnotations;

namespace FinalProyecto.Models.Login
{
    public class ForgotPasswordViewModel
    {
        [Required, Display(Name = "Usuario")]
        public string Username { get; set; }

        [EmailAddress, Display(Name = "Correo (opcional)")]
        public string Correo { get; set; }
    }
}
