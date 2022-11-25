using RestSharp;
using System;
using System.Threading.Tasks;
using WhatsAppApi;

namespace Arepas.Infrastructure.Notification
{
    public class Notifier
    {
        public void Send()
        {
            string Phone = "";
            string password = "";
            string name = "";
            WhatsApp wa = new WhatsApp(Phone, password, name, true);

            wa.OnConnectSuccess += () =>
            {
                wa.OnLoginSuccess += (phone, data) =>
                {
                    wa.SendMessage("no se que sea ", "tampovo");

                };
                wa.Login();
            };
            wa.Connect();
        }
    }
}
