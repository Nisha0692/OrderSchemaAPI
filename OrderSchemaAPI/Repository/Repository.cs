using Microsoft.EntityFrameworkCore;
using OrderSchemaAPI.Data;
using OrderSchemaAPI.Models;

namespace OrderSchemaAPI.Repository
{
    public class Repository : IRepository
    {
        private readonly OrdersContext _context;

        public Repository(OrdersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Order>> GetOrderAsync()
        {
            /*return await _context.Orders
                       .Include(p => p.PickupAddress)
                       .Include(d => d.DeliveryAddress)
                       .Include(i => i.Items)
                       .ToListAsync();*/
            return await _context.Orders
                .Include(p=>p.PickupAddress)
                .Include(d=>d.DeliveryAddress)
                .Include(i => i.Items).ToListAsync();
        }

        public async Task<Order> GetOrdersById(int orderId)
        {
            return await _context.Orders
                .Where(x=>x.OrderId== orderId)
                       .Include(p => p.PickupAddress)
                       .Include(d => d.DeliveryAddress)
                       .Include(i => i.Items)
                       .FirstAsync();
        }

        public async Task<bool> SaveChangesAsync(Order order)
        {
            _context.Orders.Add(order);
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
