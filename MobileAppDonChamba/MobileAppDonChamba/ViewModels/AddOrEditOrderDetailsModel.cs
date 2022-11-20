using MobileAppDonChamba.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobileAppDonChamba.ViewModels
{
    internal class AddOrEditOrderDetailsModel
    {
        public OrderHandler OrderDetail { get; set; }

        public ObservableCollection<Products> productList { get; set; }

        private Products _selectedProduct;
        public Products SelectedProduct { get { return _selectedProduct; } set { _selectedProduct = value; } }
         

        public AddOrEditOrderDetailsModel()
        {
            OrderDetail = new OrderHandler();
        }

    }
}
