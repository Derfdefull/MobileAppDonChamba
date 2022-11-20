using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MobileAppDonChamba.Models;
using MobileAppDonChamba.Views;
using Xamarin.Forms;

namespace MobileAppDonChamba.ViewModels
{
    internal class OrderDetailsModel
    {

        public ObservableCollection<OrderHandler> OrderDetails { get; set; }

        public OrderDetailsModel()
        { 
            OrderDetails = new ObservableCollection<OrderHandler>();
        }


    }
}
