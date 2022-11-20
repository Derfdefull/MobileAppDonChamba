using MobileAppDonChamba.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MobileAppDonChamba.ViewModels
{
    internal class MainPageViewModel
    {

        public ObservableCollection<Orders> orderHistory { get; set; }

        public MainPageViewModel()
        {
            orderHistory = new ObservableCollection<Orders>();
             
        }
    }
}
