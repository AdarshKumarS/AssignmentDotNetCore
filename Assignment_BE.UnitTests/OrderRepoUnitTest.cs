using Assignment_BE.Data;
using Assignment_BE.Models;
using Assignment_BE.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Assignment_BE.UnitTests
{
    public class OrderRepoUnitTest
    {
        private async Task<OrderContext> GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "OrderTestDb")
                .Options;

            var dbContext = new OrderContext(options);
            dbContext.Database.EnsureCreated();

            if (await dbContext.Orders.CountAsync() <= 0)
            {
                dbContext.Orders.Add(new Order { FirstName = "Satyam", LastName = "Singh", Description = "Desc1 for Testing Purposes", Quantity = 30 });
                await dbContext.SaveChangesAsync();
            }

            return dbContext;
        }

        [Fact]
        public async Task AddOrderAsync_ShouldAddOrder()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContext();
            var repository = new OrderRepo(dbContext);

            var order = new Order { FirstName = "Adarsh", LastName = "Kumar", Description = "Description For Testing Purposes", Quantity = 21 };

            // Act
            await repository.AddOrderAsync(order);
            var orders = await repository.GetAllOrdersAsync();

            // Assert
            Assert.Contains(order, orders);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnOrders()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContext();
            var repository = new OrderRepo(dbContext);

            // Act
            var orders = await repository.GetAllOrdersAsync();

            // Assert
            Assert.NotEmpty(orders);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldDeleteOrder()
        {
            // Arrange
            var dbContext = await GetInMemoryDbContext();
            var repository = new OrderRepo(dbContext);
            var order = await dbContext.Orders.FirstAsync();

            // Act
            await repository.DeleteOrderAsync(order.Id);
            var orders = await repository.GetAllOrdersAsync();

            // Assert
            Assert.DoesNotContain(order, orders);
        }
    }
}

