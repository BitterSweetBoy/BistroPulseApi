using Module.Application.DTOs;
using Module.Application.Exceptions;
using Module.Application.Interfaces;
using Module.Application.Interfaces.IRepositories;
using Module.Core.Entities;

namespace Module.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> CreateRestaurantAsync(RestaurantRequestDto restaurantRequest)
        {
            //Validar que no exista un restaurante con el mismo nombre
            if (await _restaurantRepository.ExistsByNameAsync(restaurantRequest.RestaurantName))
            {
                throw new RestaurantAlreadyExistsException($"El restaurante con el nombre '{restaurantRequest.RestaurantName}' ya existe.");
            }

            //Procesar imagenes: Cover y galeria:
            string? coverUrl = null;
            if (restaurantRequest.Images.Cover.NewFile != null)
            {
                //coverUrl = await _imageStorage.SaveFileAsync(restaurantRequest.Images.Cover.NewFile);
            }
            else if (!string.IsNullOrEmpty(restaurantRequest.Images.Cover.KeptUrl))
            {
                coverUrl = restaurantRequest.Images.Cover.KeptUrl;
            }

            var galleryUrls = new List<string>();
            //conservar URLs existentes
            galleryUrls.AddRange(restaurantRequest.Images.Gallery.KeptUrls);
            //Subir nuevos archivos
            foreach (var file in restaurantRequest.Images.Gallery.NewFiles)
            {
                //galleryUrls.Add(await _imageStorage.SaveFileAsync(file));
            }

            //Eliminar URLs de archivos eliminados
            foreach (var url in restaurantRequest.Images.Gallery.RemovedUrls)
            {
                //await _imageStorage.DeleteFileAsync(url);
            }

            //Crear el objeto Restaurant
            var restaurant = new Restaurant
            {
                RestaurantName = restaurantRequest.RestaurantName,
                Established = restaurantRequest.Established,
                //StartTime = restaurantRequest.StartTime,
                //EndTime = restaurantRequest.EndTime,
                PhoneNumber = restaurantRequest.PhoneNumber,
                //Location = restaurantRequest.Location,
                //CoverImageUrl = coverUrl,
                //GalleryImageUrls = galleryUrls,
                //PaymentMethods = restaurantRequest.PaymentMethods.Select(pm => pm.Value).ToList(),
                //Representatives = restaurantRequest.Representatives.Select(r => new Representative
                //{
                //    Name = r.Name,
                //    PhoneNumber = r.Phone
                //}).ToList()
            };
            return true;
        }
    }
}
