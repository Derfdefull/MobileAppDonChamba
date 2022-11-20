using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppDonChamba.Models
{
    internal class Orders
    {

        public int PkIdOrden { get; set; }
        public int FkIdUsuario { get; set; }
        public string Comentario { get; set; }
        public byte Estado { get; set; }
        public decimal Total { get; set; }

    }
}
