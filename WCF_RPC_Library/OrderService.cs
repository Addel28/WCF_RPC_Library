using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCF_RPC_Library
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени класса "Service1" в коде и файле конфигурации.
    public class OrderService : IOrderService
    {
        private static readonly List<Order> orders = new List<Order>();

        public async Task<List<Order>> GetOrdersAsync()
        {
            await Task.Delay(100); // Имитируем задержку
            return orders;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            await Task.Delay(100); // Имитируем задержку
            return orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await Task.Delay(100); // Имитируем задержку
            orders.Add(order);
        }

        private static readonly List<Book> books = new List<Book>();

        public async Task<List<Book>> GetBooksAsync()
        {
            await Task.Delay(100); // Имитируем задержку
            return books;
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            await Task.Delay(100); // Имитируем задержку
            return books.FirstOrDefault(b => b.BookId == bookId);
        }

        public async Task AddBookAsync(Book book)
        {
            await Task.Delay(100); // Имитируем задержку
            books.Add(book);
        }

    }
}
//public interface IAsyncOrderService
//{
//    [OperationContract]
//    Task<List<Order>> GetOrdersAsync();

//    [OperationContract]
//    Task<Order> GetOrderByIdAsync(int orderId);

//    [OperationContract]
//    Task AddOrderAsync(Order order);
//}