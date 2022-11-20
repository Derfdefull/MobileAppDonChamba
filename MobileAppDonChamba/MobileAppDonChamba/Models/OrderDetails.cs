using System;

namespace MobileAppDonChamba.Models
{
   public class OrderDetails
    {
        public int PkIdDetalle { get; set; }
        public int FkIdOrden { get; set; }
        public int FkIdProducto { get; set; }
        public decimal Preciounidad { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }
         
    }
}