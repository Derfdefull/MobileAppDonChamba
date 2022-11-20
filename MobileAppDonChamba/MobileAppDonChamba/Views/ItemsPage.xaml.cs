using MobileAppDonChamba.Models;
using MobileAppDonChamba.Services;
using MobileAppDonChamba.ViewModels;
using MobileAppDonChamba.Views;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileAppDonChamba.Views
{
    public partial class ItemsPage : ContentPage
    {

        public ItemsPage()
        {
            InitializeComponent();
             
            MessagingCenter.Subscribe<AddOrEditOrderDetails, OrderHandler>(this, "AddOrEditOrderDetails", (page, orderdetail) =>
            {
                ((OrderDetailsModel)BindingContext).OrderDetails.Add(orderdetail); 
                lvOrderDetails.ItemsSource = ((OrderDetailsModel)BindingContext).OrderDetails; 
                refreshInteface(); 
                lvOrderDetails.SelectionMode = (ListViewSelectionMode)SelectionMode.None;
                 
            });

            refreshInteface();
        }

        private async void refreshInteface()
        {
            Loading.IsVisible = true;
            await Task.Delay(500);
            if (((OrderDetailsModel)BindingContext).OrderDetails.Count > 0)
            {
                listTitle.Text = "Detalle de Orden";
                btnSend.IsEnabled = true;
            }
                
            else 
                listTitle.Text = "Agrega productos a esta orden...";

            lvOrderDetails.IsVisible = true;

            Loading.IsVisible = false;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddOrEditOrderDetails());
        }

        private async void btnCancel_Clicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Alerta", "¿Desea cancelar la orden?", "Si", "No");
            if(result == true) await Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

           bool result = await DisplayAlert("Alerta", "¿Desea eliminar este registro?", "Si", "No");

            if(result == true)
            {
                TappedEventArgs tappedEventArgs = (TappedEventArgs)e; 
                removeItem((int)tappedEventArgs.Parameter);
                refreshInteface();
            }

        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            Loading.IsVisible = true;

            bool result = await DisplayAlert("Alerta", "¿Está seguro de procesar esta orden?", "Si", "No");
            if( result == true)

                if (((OrderDetailsModel)BindingContext).OrderDetails.Count > 0)
                    {
                         
                        var totalOrder = 
                        ((OrderDetailsModel)BindingContext).
                            OrderDetails.Sum(st => st.OrderDetail.Subtotal);

                        var neworder = new Orders() { PkIdOrden = 0, 
                            FkIdUsuario = Login.user.PkIdUsuario, 
                            Comentario = "Sin Comentario", Estado = 0, Total = totalOrder };

                        Uri request = new Uri(App._API_URL + "api/Ordenes");

                        var client = new HttpClient();
                        var json = JsonConvert.SerializeObject(neworder);
                        var body = new StringContent(json, Encoding.UTF8, "application/json");

                        try
                        {
                            var response = await client.PostAsync(request, body);

                            if (response.StatusCode == System.Net.HttpStatusCode.Created)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                Orders savedOrder = JsonConvert.DeserializeObject<Orders>(content);

                                request = new Uri(App._API_URL + "api/OrdenesDetalles");

                                var orderDetails =  ((OrderDetailsModel)BindingContext).OrderDetails;

                                foreach (var orderdetail in orderDetails)
                                {
                                    orderdetail.OrderDetail.FkIdOrden = savedOrder.PkIdOrden; 
                                    json = JsonConvert.SerializeObject(orderdetail.OrderDetail);
                                    body = new StringContent(json, Encoding.UTF8, "application/json");

                                    response = await client.PostAsync(request, body);

                                    if (response.StatusCode != System.Net.HttpStatusCode.Created)
                                            await DisplayAlert("Alerta", 
                                                "No se pudo guardar la orden, contacta al administrador", "Ok");

                                }


                            Loading.IsVisible = false;
                            new MailSender().SendEmail(savedOrder, orderDetails);
                            await DisplayAlert("", "La orden se guardo correctamente...", "Ok");
                            await Navigation.PopAsync();

                            }
                            else
                                await DisplayAlert("Alerta", 
                                    "No se pudo guardar la orden, contacta al administrador", "Ok");


                        }
                        catch (Exception ex) { 
                        await DisplayAlert("Error de Conexion", "No se encontro el servidor", "Ok"); }

                    }

            Loading.IsVisible = false;
        }
 

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            TappedEventArgs tappedEventArgs = (TappedEventArgs)e;
            var item = (OrderDetails)tappedEventArgs.Parameter;

            //string result = await DisplayPromptAsync("Editando registro", "Introduce la cantidad: ", initialValue: item.Cantidad.ToString(), maxLength: 2, keyboard: Keyboard.Numeric);
            string result = await DisplayActionSheet("Selecciona la Cantidad", "", "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10");
            if (result != null)
                if(result.Length > 0)
                    if(decimal.Parse(result) > 0)
                    {
                        OrderHandler orderdetail =
                                ((OrderDetailsModel)BindingContext)
                        .OrderDetails.Where(st => st.Product.PkIdProducto == item.FkIdProducto).FirstOrDefault();

                        orderdetail.OrderDetail.Cantidad = decimal.Parse(result);
                        orderdetail.OrderDetail.Subtotal = item.Cantidad * item.Preciounidad;

                        removeItem(item.FkIdProducto);

                        ((OrderDetailsModel)BindingContext).OrderDetails.Add(orderdetail);
                    }
              
            refreshInteface();
        }


        private void removeItem(int index)
        {
            OrderHandler orderdetail =
                ((OrderDetailsModel)BindingContext)
                .OrderDetails.Where(st => st.Product.PkIdProducto == index).FirstOrDefault(); 

            ((OrderDetailsModel)BindingContext).OrderDetails.Remove(orderdetail);
        }

        private void ViewCell_ChildAdded(object sender, ElementEventArgs e)
        {
            lvOrderDetails.IsVisible = false;
            lvOrderDetails.IsVisible = true;
        }
    }
}