using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileAppDonChamba.Models;
using Prism.Navigation;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using MobileAppDonChamba.ViewModels;

namespace MobileAppDonChamba.Views
{
    public partial class AboutPage : ContentPage
    {
 

        public AboutPage()
        {
            InitializeComponent();
            
            if (Login.Logged == false && Login.user == null)
                goLogin();
             
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadOrders();
            txtUsername.Text = Login.user.Nombres + " "+ Login.user.Apellidos;
        }

        public async void loadOrders()
        { 

            Uri request = new Uri(String.Format(App._API_URL + "api/Ordenes/byUsuarios/{0}", Login.user.PkIdUsuario)); 

            var client = new HttpClient();  

            try
            {
                var response = await client.GetAsync(request);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    
                    ((MainPageViewModel)BindingContext).orderHistory = JsonConvert.DeserializeObject<ObservableCollection<Orders>>(content);

                    if(((MainPageViewModel)BindingContext).orderHistory.Count > 0)
                        listSubTitle.IsVisible = false;


                    listOrderHistory.ItemsSource = ((MainPageViewModel)BindingContext).orderHistory;
                }
                else
                    await DisplayAlert("Alerta", "No se encontraron ordenes...", "Ok");


            }
            catch (Exception ex) { await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }
        }


        private async void goLogin()
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemsPage());
        }

        
    }
}