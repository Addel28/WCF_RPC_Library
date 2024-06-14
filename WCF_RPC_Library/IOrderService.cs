using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_RPC_Library
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        Task<List<Order>> GetOrdersAsync();

        [OperationContract]
        Task<Order> GetOrderByIdAsync(int orderId);

        [OperationContract]
        Task AddOrderAsync(Order order);

        [OperationContract]
        Task<List<Book>> GetBooksAsync();

        [OperationContract]
        Task<Book> GetBookByIdAsync(int bookId);

        [OperationContract]
        Task AddBookAsync(Book book);


        // TODO: Добавьте здесь операции служб
    }

    // Используйте контракт данных, как показано в примере ниже, чтобы добавить составные типы к операциям служб.
    // В проект можно добавлять XSD-файлы. После построения проекта вы можете напрямую использовать в нем определенные типы данных с пространством имен "WCF_RPC_Library.ContractType".
    [DataContract]
    public class Order
    {
        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public DateTime OrderDate { get; set; }

        [DataMember]
        public List<OrderItem> Items { get; set; }
    }

    [DataContract]
    public class OrderItem
    {
        [DataMember]
        public int ItemId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }

    [DataContract]
    public class Book
    {
        [DataMember]
        public int BookId { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public DateTime PublishedDate { get; set; }
    }

}



//namespace WcfRpcExample
//{
//    // Контракт службы
//    [ServiceContract]
//    public interface IOrderService
//    {
//        [OperationContract]
//        List<Order> GetOrders();

//        [OperationContract]
//        Order GetOrderById(int orderId);

//        [OperationContract]
//        void AddOrder(Order order);
//    }

//    // Класс данных
//    [DataContract]
//    public class Order
//    {
//        [DataMember]
//        public int OrderId { get; set; }

//        [DataMember]
//        public string CustomerName { get; set; }

//        [DataMember]
//        public DateTime OrderDate { get; set; }

//        [DataMember]
//        public List<OrderItem> Items { get; set; }
//    }

//    [DataContract]
//    public class OrderItem
//    {
//        [DataMember]
//        public int ItemId { get; set; }

//        [DataMember]
//        public string ProductName { get; set; }

//        [DataMember]
//        public int Quantity { get; set; }
//    }

//    // Реализация службы
//    public class OrderService : IOrderService
//    {
//        private static readonly List<Order> orders = new List<Order>();

//        public List<Order> GetOrders()
//        {
//            return orders;
//        }

//        public Order GetOrderById(int orderId)
//        {
//            return orders.FirstOrDefault(o => o.OrderId == orderId);
//        }

//        public void AddOrder(Order order)
//        {
//            orders.Add(order);
//        }
//    }

//    // Асинхронный контракт службы
//    [ServiceContract]
//    public interface IAsyncOrderService
//    {
//        [OperationContract]
//        Task<List<Order>> GetOrdersAsync();

//        [OperationContract]
//        Task<Order> GetOrderByIdAsync(int orderId);

//        [OperationContract]
//        Task AddOrderAsync(Order order);
//    }

//    // Асинхронная реализация службы
//    public class AsyncOrderService : IAsyncOrderService
//    {
//        private static readonly List<Order> orders = new List<Order>();

//        public async Task<List<Order>> GetOrdersAsync()
//        {
//            await Task.Delay(100); // Имитируем задержку
//            return orders;
//        }

//        public async Task<Order> GetOrderByIdAsync(int orderId)
//        {
//            await Task.Delay(100); // Имитируем задержку
//            return orders.FirstOrDefault(o => o.OrderId == orderId);
//        }

//        public async Task AddOrderAsync(Order order)
//        {
//            await Task.Delay(100); // Имитируем задержку
//            orders.Add(order);
//        }
//    }



//    // Хостинг службы (консольное приложение)
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var hostTask = Task.Run(() =>
//            {
//                using (ServiceHost host = new ServiceHost(typeof(OrderService), new Uri("http://localhost:8000/OrderService")))
//                {
//                    host.AddServiceEndpoint(typeof(IOrderService), new BasicHttpBinding(), "");
//                    host.Open();
//                    Console.WriteLine("Sync Service is running...");

//                    using (ServiceHost asyncHost = new ServiceHost(typeof(AsyncOrderService), new Uri("http://localhost:8000/AsyncOrderService")))
//                    {
//                        asyncHost.AddServiceEndpoint(typeof(IAsyncOrderService), new BasicHttpBinding(), "");
//                        asyncHost.Open();
//                        Console.WriteLine("Async Service is running...");
//                        Console.ReadLine();
//                    }
//                }
//            });

//            var clientTask = Task.Run(() =>
//            {
//                Console.WriteLine("\nSync Client:\n");

//                var factory = new ChannelFactory<IOrderService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8000/OrderService"));
//                var client = factory.CreateChannel();

//                // Добавляем новый заказ
//                client.AddOrder(new Order
//                {
//                    OrderId = 1,
//                    CustomerName = "John Doe",
//                    OrderDate = DateTime.Now,
//                    Items = new List<OrderItem>
//                    {
//                        new OrderItem { ItemId = 1, ProductName = "Laptop", Quantity = 1 },
//                        new OrderItem { ItemId = 2, ProductName = "Mouse", Quantity = 2 }
//                    }
//                });

//                // Получаем список всех заказов
//                var orders = client.GetOrders();
//                foreach (var order in orders)
//                {
//                    Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.CustomerName}, Order Date: {order.OrderDate}");
//                }

//                // Получаем заказ по ID
//                var singleOrder = client.GetOrderById(1);
//                Console.WriteLine($"Single Order - ID: {singleOrder.OrderId}, Customer Name: {singleOrder.CustomerName}");

//                Console.WriteLine("\nAsync Client:\n");

//                var asyncFactory = new ChannelFactory<IAsyncOrderService>(new BasicHttpBinding(), new EndpointAddress("http://localhost:8000/AsyncOrderService"));
//                var asyncClient = asyncFactory.CreateChannel();

//                // Асинхронный вызов клиента
//                Task.Run(async () =>
//                {
//                    // Добавляем новый заказ
//                    await asyncClient.AddOrderAsync(new Order
//                    {
//                        OrderId = 2,
//                        CustomerName = "Jane Doe",
//                        OrderDate = DateTime.Now,
//                        Items = new List<OrderItem>
//                        {
//                            new OrderItem { ItemId = 1, ProductName = "Smartphone", Quantity = 1 },
//                            new OrderItem { ItemId = 2, ProductName = "Headphones", Quantity = 2 }
//                        }
//                    });

//                    // Получаем список всех заказов
//                    var asyncOrders = await asyncClient.GetOrdersAsync();
//                    foreach (var order in asyncOrders)
//                    {
//                        Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.CustomerName}, Order Date: {order.OrderDate}");
//                    }

//                    // Получаем заказ по ID
//                    var asyncSingleOrder = await asyncClient.GetOrderByIdAsync(2);
//                    Console.WriteLine($"Single Order - ID: {asyncSingleOrder.OrderId}, Customer Name: {asyncSingleOrder.CustomerName}");
//                }).Wait();
//            });

//            Task.WaitAll(hostTask, clientTask);
//        }
//    }
//}


