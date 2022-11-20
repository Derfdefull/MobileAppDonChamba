using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppDonChamba.Models
{
    internal class Login
    {
        public Login() { }

        public static Usuario user { get; set; }
        public static Sucursal sucursal { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public static bool Logged = false;
    }
}
