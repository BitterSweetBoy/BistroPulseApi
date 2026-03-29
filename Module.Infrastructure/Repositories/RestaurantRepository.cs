using Microsoft.Extensions.Logging;
using Module.Application.DTOs;
using Module.Application.Interfaces.IRepositories;
using Module.Core.Entities;
using Module.Infrastructure.Data;

namespace Module.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly WriteDbContext _context;
        private readonly ILogger<RestaurantRepository> _logger;
        public RestaurantRepository(WriteDbContext writeDbContext, ILogger<RestaurantRepository> logger)
        {
            _context = writeDbContext;
            _logger = logger;
        }

        public async Task<bool> CreateRestaurantAsync(Restaurant restaurantRequest)
        {
            try
            {
                await _context.Restaurants.AddAsync(restaurantRequest);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating restaurant");
                return false;
            }
        }

        Task<bool> IRestaurantRepository.ExistsByNameAsync(string restaurantName)
        {
            throw new NotImplementedException();
        }
    }
}
