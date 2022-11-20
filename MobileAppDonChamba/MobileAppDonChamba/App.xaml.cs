
using MobileAppDonChamba.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
[assembly: ExportFont("Raleway.ttf", Alias = "Raleway")]
namespace MobileAppDonChamba
{
    public partial class App : Application
    {

        public readonly static string _API_URL = "https://donchamba-api.leverans.cloud/";

        public App()
        {
            InitializeComponent();
 
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
