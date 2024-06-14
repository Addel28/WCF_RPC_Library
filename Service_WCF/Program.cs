using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_RPC_Library;

namespace Service_WCF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var hostTask = Task.Run(() =>
            //{
                using (ServiceHost host = new ServiceHost(typeof(OrderService), new Uri("http://localhost:8733/Design_Time_Addresses/WCF_RPC_Library/IOrderService/mx")))
                {
                    host.AddServiceEndpoint(typeof(IOrderService), new WSHttpBinding(), "");
                    host.Open();
                    Console.WriteLine("Sync Service is running...");
                    Console.ReadLine();
                    //using (ServiceHost asyncHost = new ServiceHost(typeof(AsyncOrderService), new Uri("http://localhost:8000/AsyncOrderService")))
                    //{
                    //    asyncHost.AddServiceEndpoint(typeof(IAsyncOrderService), new BasicHttpBinding(), "");
                    //    asyncHost.Open();
                    //    Console.WriteLine("Async Service is running...");
                    //    Console.ReadLine();
                    //}
                }
            //});
        }
    }
}
