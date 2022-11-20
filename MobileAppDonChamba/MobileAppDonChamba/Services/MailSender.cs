using MobileAppDonChamba.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Text;

namespace MobileAppDonChamba.Services
{
    internal class MailSender
    {
        protected readonly string SMTPServer = "mail.smtp2go.com";
        protected readonly int SMTPServerPort = 2525;

        protected readonly string SenderMail = "donchamba@leverans.cloud";
        protected readonly string SenderMailPassword = "vrrio4XQpS7X3Qa4";

        protected readonly string ReceiverMail = "chavez.denilson1210@yahoo.com";


        public MailSender()
        {
             
        }

        public bool SendEmail(Orders order, ObservableCollection<OrderHandler> orderdetails)
        {
            try
            { 
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(this.SMTPServer);

                mail.From = new MailAddress(this.SenderMail);
                mail.To.Add(this.ReceiverMail);
                mail.Subject = "Nueva orden de " + Login.user.Nombres + " " + Login.user.Apellidos;

                mail.Body += "Contacto del emisor " + "<br>" +
                    "Télefono: " + Login.user.Telefono + "<br>" +
                    "Celular: " + Login.user.Celular + "<br> <hr> " +

                    "Orden #" + order.PkIdOrden + "<br>" +
                    "Valor total de Orden: " + String.Format("{0:C2}", order.Total) +  "<br> <hr>" +
                    "Detalle de Orden "  + "<br>";

                foreach(var orderdetail in orderdetails)
                    mail.Body += 
                        String.Format("Producto: {0} <br> Descripcion: {3} <br> " +
                        "Cantidad: {1} (Cajas) <br> Subtotal: {2:C2} <hr>",
                        orderdetail.Product.Nombre, orderdetail.OrderDetail.Cantidad,
                        orderdetail.OrderDetail.Subtotal, orderdetail.Product.Descripcion);

                mail.Body += "Información de entrega " + "<br>" +
                   "Nombre de Sucursal: " + Login.sucursal.Nombre + "<br>" +
                   "Télefono: " + Login.sucursal.Telefono + "<br> " +
                   "Dirección: " + Login.sucursal.Direccion;
                    

                mail.IsBodyHtml = true;

                SmtpServer.Port = this.SMTPServerPort;
                SmtpServer.Host = this.SMTPServer;
                SmtpServer.EnableSsl = true; 
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential(this.SenderMail, this.SenderMailPassword);

                SmtpServer.Send(mail);
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
