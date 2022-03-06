using OrderSchemaAPI.Models;

namespace OrderSchemaAPI.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Order>> GetOrderAsync();

        Task<Order> GetOrdersById(int orderId);

        Task<bool> SaveChangesAsync(Order order);
    }
}
