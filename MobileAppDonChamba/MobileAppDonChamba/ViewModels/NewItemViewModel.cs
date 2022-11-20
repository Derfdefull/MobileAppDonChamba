using MobileAppDonChamba.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileAppDonChamba.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private decimal count;
        private int product;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(count.ToString())
                && !String.IsNullOrWhiteSpace(product.ToString());
        }

        public decimal Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        public int Product
        {
            get => product;
            set => SetProperty(ref product, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
