using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module.Application.DTOs;
using Module.Application.Interfaces;
using Module.Core.Entities;
using Module.Shared.Response;

namespace BistroPulseApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpPost("restaurants")]
        [ProducesResponseType(typeof(ApiResponse<string>), 200)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddRestaurant([FromBody] RestaurantRequestDto createRestaurantRequest)
        {
            return Ok(new ApiResponse<string>(
                Success: true,
                Message: "Restaurante creado con éxito",
                Data: null
            ));
        }
    }
}
