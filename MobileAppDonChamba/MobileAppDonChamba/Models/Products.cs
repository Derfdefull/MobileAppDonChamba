using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppDonChamba.Models
{
    internal class Products
    {

        public int PkIdProducto { get; set; }
        public int FkIdCategoria { get; set; }
        public string Nombre { get; set; }   
        public string Descripcion { get; set; }
        public byte Estado { get; set; }
        public string Imagen { get; set; } 
        public decimal Precio { get; set; }

    }
}
