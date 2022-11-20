using MobileAppDonChamba.Models;
using MobileAppDonChamba.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppDonChamba.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditOrderDetails : ContentPage
    { 
        private List<Products> products;
        private int Qproduct = 0;

        OrderHandler orderdetail;

        public AddOrEditOrderDetails()
        {
            InitializeComponent();
            loadProducts();
            orderdetail = new OrderHandler();   
        }

        private async void Save()
        {
            if (((AddOrEditOrderDetailsModel)BindingContext).SelectedProduct != null)
            {
                try
                { 
                    if (Qproduct > 0)
                    {
                        var currentproduct = ((AddOrEditOrderDetailsModel)BindingContext).SelectedProduct;


                        orderdetail.Product = currentproduct;
                        orderdetail.OrderDetail = new OrderDetails()
                        {
                            PkIdDetalle = 0,
                            FkIdOrden = 0,
                            FkIdProducto = currentproduct.PkIdProducto,
                            Cantidad = Qproduct,
                            Preciounidad = currentproduct.Precio,
                            Subtotal = currentproduct.Precio * Qproduct
                        };

                        ((AddOrEditOrderDetailsModel)BindingContext).OrderDetail = orderdetail;
                         MessagingCenter.Send(this, "AddOrEditOrderDetails", orderdetail);

                        await Navigation.PopAsync();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Debes introducir la cantidad antes de guardar", "", "Ok");
                }


            }
            else
            {
                await DisplayAlert("Debes seleccionar un producto antes de guardar", "", "Ok");
            }
        }
      
        public async void loadProducts()
        {
             
            listproducts.IsVisible = false;
            Loading.IsVisible = true; 
            await Task.Delay(1000);


            Uri request = new Uri(App._API_URL + "api/Productoes");
            var client = new HttpClient();

            try
            {
                var response = await client.GetAsync(request);


                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    products = JsonConvert.DeserializeObject<List<Products>>(content);
                    ((AddOrEditOrderDetailsModel)BindingContext).productList = JsonConvert.DeserializeObject<ObservableCollection<Products>>(content);
                    listproducts.ItemsSource = ((AddOrEditOrderDetailsModel)BindingContext).productList.Where(st => st.Estado == 1).ToList();
                      
                    Loading.IsVisible = false;

                }
                else
                    await DisplayAlert("Esta Categoria no tiene Productos", "", "Ok");


            }
            catch (Exception ex) { await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }
             
            listproducts.IsVisible = true;
            Loading.IsVisible = false;
        } 
 

        private async void Button_Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
  
        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            Qproduct = int.Parse(picker.SelectedItem.ToString());
            Save();

        }
    }
}