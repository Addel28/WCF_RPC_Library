using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WCF_RPC_Library;

namespace Client_WCF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var binding = new WSHttpBinding();
            binding.Security.Mode = SecurityMode.Message;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
            var asyncFactory = new ChannelFactory<IOrderService>(binding, new EndpointAddress("http://localhost:8733/Design_Time_Addresses/WCF_RPC_Library/IOrderService/mx"));
            //var asyncFactory = new ChannelFactory<IOrderService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8733/Design_Time_Addresses/WCF_RPC_Library/IOrderService/mx"));
            var asyncClient = asyncFactory.CreateChannel();

            // Асинхронный вызов клиента
            Task.Run(async () =>
            {
                // Добавляем новый заказ
                await asyncClient.AddOrderAsync(new Order
                {
                    OrderId = 2,
                    CustomerName = "Jane Doe",
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>
                        {
                            new OrderItem { ItemId = 1, ProductName = "Smartphone", Quantity = 1 },
                            new OrderItem { ItemId = 2, ProductName = "Headphones", Quantity = 2 }
                        }
                });

                // Получаем список всех заказов
                var asyncOrders = await asyncClient.GetOrdersAsync();
                foreach (var order in asyncOrders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.CustomerName}, Order Date: {order.OrderDate}");
                }

                // Получаем заказ по ID
                var asyncSingleOrder = await asyncClient.GetOrderByIdAsync(2);
                Console.WriteLine($"Single Order - ID: {asyncSingleOrder.OrderId}, Customer Name: {asyncSingleOrder.CustomerName}");
            }).Wait();
            Console.ReadLine();
        } 
    }
}

