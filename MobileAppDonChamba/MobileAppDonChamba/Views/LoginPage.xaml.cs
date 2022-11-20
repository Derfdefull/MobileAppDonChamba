using MobileAppDonChamba.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileAppDonChamba.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace MobileAppDonChamba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();

        }
         

        private async void btnLogIn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text.Trim() != "" && txtUsername.Text.Trim() != "")
                {
                    
                    Loading.IsVisible = true;
                    await Task.Delay(1000);

                    var Login = new Login() { Usuario = txtUsername.Text, Contrasena = txtPassword.Text };

                    Uri request = new Uri(App._API_URL +  "api/Usuarios/Auth");

                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(Login);
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    try { 
                        var response = await client.PostAsync(request, body);


                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            Login.user = JsonConvert.DeserializeObject<Usuario>(content);

                            request = new Uri(String.Format(App._API_URL + "api/Sucursales/{0}", Login.user.FkIdSucursal));
                            client = new HttpClient();
                            response = await client.GetAsync(request);

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                content = await response.Content.ReadAsStringAsync();
                                Login.sucursal = JsonConvert.DeserializeObject<Sucursal>(content);
                                Loading.IsVisible = false;
                                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                            }
                            else
                                await DisplayAlert("No se pudo Iniciar Sesión", "Contraseña o Usuario Invalidos...", "Ok");
                            
                        }
                        else
                            await DisplayAlert("No se pudo Iniciar Sesión", "Contraseña o Usuario Invalidos...", "Ok");


                    }
                    catch (Exception ex) { await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }

                }
            } catch (Exception ex) { await DisplayAlert("Campos Vacios", "Introduce tus credenciales para continuar", "Ok"); }
           
            Loading.IsVisible = false;
        }
    }
}