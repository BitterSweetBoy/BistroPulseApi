using Microsoft.AspNetCore.Http;
using Module.Core.Entities;

namespace Module.Application.DTOs
{

    public class ImageFileDto
    {
        public string? KeptUrl { get; set; }
        public IFormFile? NewFile { get; set; }
        public string? RemovedUrl { get; set; }
    }

    public class GalleryImagesDto
    {
        public List<string> KeptUrls { get; set; } = new();
        public List<IFormFile> NewFiles { get; set; } = new();
        public List<string> RemovedUrls { get; set; } = new();
    }

    public class RestaurantRequestDto
    {
        public string RestaurantName { get; set; } = default!;
        public RestaurantAdmin Representative { get; set; } = new();
        public string PhoneNumber { get; set; } = default!;
        public DateTime Established { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public List<PaymentMethod> PaymentMethods { get; set; } = new();
        public string Location { get; set; } = default!;
        public ImagesDto Images { get; set; } = new();
    }

    public class ImagesDto
    {
        public ImageFileDto Cover { get; set; } = new();
        public GalleryImagesDto Gallery { get; set; } = new();
    }

}
