using System;
using System.Collections.Generic;
using System.Text;

namespace MobileAppDonChamba.Models
{
    internal class OrderHandler
    {
        public int iddetailorder { get; set; }
        public Products Product { get; set; }
        public OrderDetails OrderDetail { get; set; }
    }
}
