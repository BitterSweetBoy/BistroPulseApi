using Module.Application.DTOs;
using Module.Core.Entities;
namespace Module.Application.Interfaces.IRepositories
{
    public interface IRestaurantRepository
    {
        Task<bool> CreateRestaurantAsync(Restaurant restaurant);
        Task<bool> ExistsByNameAsync(string restaurantName);   
    }
}
