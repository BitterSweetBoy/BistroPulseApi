using Module.Application.DTOs;

namespace Module.Application.Interfaces
{
    public interface IRestaurantService
    {
        Task<bool> CreateRestaurantAsync(RestaurantRequestDto restaurantRequestDto);
    }
}
