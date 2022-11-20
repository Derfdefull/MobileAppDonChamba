using MobileAppDonChamba.Models;
using MobileAppDonChamba.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppDonChamba.Views
{
    public partial class NewItemPage : ContentPage
    {
        private List<Categoria> categorias;
        private List<Products> products;

        public OrderDetails Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
            loadCategories();
        }

        public async void loadCategories()
        {
                pkerCategories.IsEnabled = false;
                pkerProducts.IsEnabled = false;
                Loading.IsVisible = true;
                await Task.Delay(1000);


                Uri request = new Uri("http://10.0.2.2:44386/api/Categorias");
                var client = new HttpClient();

                try
                {
                    var response = await client.GetAsync(request);


                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        categorias = JsonConvert.DeserializeObject<List<Categoria>>(content);

                        foreach(var categoria in categorias)
                            pkerCategories.Items.Add(categoria.Nombre);
                         
                        Loading.IsVisible = false; 

                    }
                    else
                        await DisplayAlert("No se pudo obtener las categorias", "", "Ok");


                }
                catch (Exception ex) { await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }

            pkerCategories.IsEnabled = true;
            pkerProducts.IsEnabled = true;
            Loading.IsVisible = false;
        }


        private async void pkerCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            pkerCategories.IsEnabled = false;
            Loading.IsVisible = true;
            await Task.Delay(1000);


            Uri request = new Uri(String.Format("http://10.0.2.2:44386/api/Productoes/byCategoria/{0}", categorias[pkerCategories.SelectedIndex].PkIdCategoria));
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(request);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Products>>(content);

                    foreach (var product in products)
                        pkerProducts.Items.Add(product.Nombre + ", " + product.Descripcion);

                    Loading.IsVisible = false;

                }
                else
                    await DisplayAlert("Esta Categoria no tiene Productos", "", "Ok");


            }
            catch (Exception ex) { await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }

            pkerCategories.IsEnabled = true;
            Loading.IsVisible = false;
        }
    }
}