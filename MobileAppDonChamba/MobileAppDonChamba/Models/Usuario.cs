﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppDonChamba.Models
{
    internal class Usuario
    {
        public int PkIdUsuario { get; set; }
        public int FkIdSucursal { get; set; }
        public string Usuario1 { get; set; } 
        public string Contrasena { get; set; } 
        public string Nombres { get; set; } 
        public string Apellidos { get; set; } 
        public string Celular { get; set; }
        public string Telefono { get; set; }
        public byte Nivel { get; set; }
    }
}
