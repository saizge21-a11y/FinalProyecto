using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using FinalProyecto.Models;
using FinalProyecto.Models.Login;

namespace FinalProyecto.Controllers
{
    public class CuentaController : Controller
    {
        private readonly Entities _db = new Entities();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Completa usuario y contraseña.");
                return View(model);
            }

            var userInput = (model.Username ?? "").Trim();
            var passInput = (model.Password ?? "").Trim();

            var usuario = _db.USUARIOS
                .FirstOrDefault(u => (u.USUARIO ?? "").Trim().ToUpper() == userInput.ToUpper());

            if (usuario == null || !string.Equals((usuario.CONTRASENA ?? "").Trim(), passInput, StringComparison.Ordinal))
            {
                ViewBag.Error = "Usuario o contraseña inválidos.";
                return View(model);
            }

            FormsAuthentication.SetAuthCookie(usuario.USUARIO, model.RememberMe);
            Session["NombreCompleto"] = (usuario.NOMBRE_COMPLETO ?? usuario.USUARIO);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // ====== REGISTRO ======
        [AllowAnonymous]
        public ActionResult Registro()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult Registro(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            string username = (vm.Username ?? "").Trim();

            // Verificar duplicado
            bool existe = _db.USUARIOS.Any(u => (u.USUARIO ?? "").Trim().ToUpper() == username.ToUpper());
            if (existe)
            {
                ViewBag.Error = "El usuario ya existe. Elige otro nombre de usuario.";
                return View(vm);
            }

            // Crear el nuevo usuario (ajustado a tu estructura real)
            var nuevo = new USUARIOS
            {
                USUARIO = username,
                CONTRASENA = (vm.Password ?? "").Trim(),
                NOMBRE_COMPLETO = (vm.NombreCompleto ?? "").Trim(),
                EDAD = vm.Edad,
                CORREO_ELECTRONICO = (vm.Correo ?? "").Trim(),
                DPI = decimal.TryParse(vm.Dpi, out decimal dpi) ? dpi : (decimal?)null
            };

            _db.USUARIOS.Add(nuevo);
            _db.SaveChanges();

            ViewBag.Ok = "Usuario creado correctamente. Ahora puedes iniciar sesión.";
            return View(new RegisterViewModel());
        }

        // ====== OLVIDÉ CONTRASEÑA======
        [AllowAnonymous]
        public ActionResult OlvideContrasena()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public ActionResult OlvideContrasena(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var username = (vm.Username ?? "").Trim();
            var usuario = _db.USUARIOS
                .FirstOrDefault(u => (u.USUARIO ?? "").Trim().ToUpper() == username.ToUpper());

            if (usuario == null)
            {
                ViewBag.Error = "No encontramos ese usuario.";
                return View(vm);
            }
            var temp = "Temp" + new Random().Next(10000, 99999);
            usuario.CONTRASENA = temp;
            _db.SaveChanges();

            ViewBag.Ok = $"Contraseña temporal generada: {temp}. Inicia sesión y cámbiala.";
            return View(new ForgotPasswordViewModel { Username = username });
        }

        [Authorize]
        public ActionResult Perfil()
        {
            var username = User.Identity.Name;
            var usuario = _db.USUARIOS
                .FirstOrDefault(u => (u.USUARIO ?? "").Trim().ToUpper() == username.ToUpper());

            if (usuario == null) return RedirectToAction("Login");
            return View(usuario);
        }

    }
}
