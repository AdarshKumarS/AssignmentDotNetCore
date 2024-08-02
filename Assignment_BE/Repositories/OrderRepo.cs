using Assignment_BE.Data;
using Assignment_BE.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_BE.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task DeleteOrderAsync(int id);
    }
    public class OrderRepo : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepo(OrderContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetOrderByIdAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
